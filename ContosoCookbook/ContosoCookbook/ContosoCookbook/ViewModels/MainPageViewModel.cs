using ContosoCookbook.Business;
using ContosoCookbook.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;

namespace ContosoCookbook.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService { get; }
        private IRecipeService _recipeService { get; }

        public MainPageViewModel(INavigationService navigationService, IRecipeService recipeService)
        {
            _navigationService = navigationService;
            _recipeService = recipeService;
            RecipeSelectedCommand = 
                ReactiveCommand
                    .CreateFromTask<Recipe>(
                        async recipe => await RecipeSelected(recipe),
                        this.WhenAnyObservable(x => x.RecipeSelectedCommand.IsExecuting)
                            .Select(isExecuting => !isExecuting)
                            .StartWith(true));
        }

        private ReactiveCommand<Recipe, Unit> _recipeSelectedCommand;
        public ReactiveCommand<Recipe, Unit> RecipeSelectedCommand         {
            get => _recipeSelectedCommand;
            private set => this.RaiseAndSetIfChanged(ref _recipeSelectedCommand, value);
        }

        private ObservableCollection<RecipeGroup> _recipeGroups;
        public ObservableCollection<RecipeGroup> RecipeGroups
        {
            get => _recipeGroups;
            set => this.RaiseAndSetIfChanged(ref _recipeGroups, value);
        }

        private Task RecipeSelected(Recipe recipe)
        {
            var p = new NavigationParameters
            {
                { "recipe", recipe }
            };

            return _navigationService.NavigateAsync("RecipePage", p);
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            if (RecipeGroups == null)
                RecipeGroups = new ObservableCollection<RecipeGroup>(await _recipeService.GetRecipeGroups());
        }
    }
}