using Prism.Mvvm;
using Prism.Navigation;
using ReactiveUI;

namespace ContosoCookbook.ViewModels
{
    public class ViewModelBase : ReactiveObject, INavigationAware
    {
        public ViewModelBase()
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }
    }
}
