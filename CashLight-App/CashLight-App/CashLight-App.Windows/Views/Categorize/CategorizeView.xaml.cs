using CashLight_App.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashLight_App.Views.Categorize
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategorizeView : Page
    {
        private MenuViewModel _viewModel;
        public CategorizeView()
        {
            this.InitializeComponent();
            _viewModel = (MenuViewModel)Menu.DataContext;
        }
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ExitApplication();
        }

        private void Categorize_Menu(object sender, RoutedEventArgs e)
        {
            _viewModel.NavigateToCategorize();
        }

        private void GoToDashboard(object sender, RoutedEventArgs e)
        {
            _viewModel.GoToDashboard();
        }
    }
}
