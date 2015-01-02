using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace CashLight_App.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        private INavigationService _navigator;

        public RelayCommand BackButtonCommand { get; set; }

        public HeaderViewModel(INavigationService navigator)
        {
            _navigator = navigator;

            BackButtonCommand = new RelayCommand(GoToPreviousView);
        }

        public void GoToPreviousView()
        {
            _navigator.GoBack();
        }
    }
}
