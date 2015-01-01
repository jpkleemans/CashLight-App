using CashLight_App.Models;
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

namespace CashLight_App.Views.Dashboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardView : Page
    {
        private MenuViewModel _menuViewModel;

        public DashboardView()
        {
            this.InitializeComponent();
            _menuViewModel = (MenuViewModel)Menu.DataContext;
        }

        private void GoToCategorize_Click(object sender, RoutedEventArgs e)
        {
            _menuViewModel.NavigateToCategorize();
        }

        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            _menuViewModel.ExitApplication();
        }
    }
}
