using Xamarin.Forms;
using ReactiveUI.XamForms;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive;

namespace ContosoCookbook.Views
{
    public partial class MainPage : ReactiveTabbedPage<ViewModels.MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();

            this.WhenActivated(d => {
                this.OneWayBind(ViewModel, vm => vm.RecipeGroups, ui => ui.ItemsSource)
                    .DisposeWith(d);
                    
                this.WhenAnyValue(x => x.ViewModel)
                    .Where(x => x != null)
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel, vm => vm.LoadRecipies)
                    .DisposeWith(d);
            });
        }
    }
}