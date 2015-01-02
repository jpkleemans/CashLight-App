using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CashLight_App.Views.Dashboard
{
    public sealed partial class IncomeMarker : UserControl
    {
        #region Properties
        public static readonly DependencyProperty TransactionProperty = DependencyProperty.Register(
            "Transaction",
            typeof(Transaction),
            typeof(IncomeMarker),
            new PropertyMetadata(null)
        );

        public Transaction Transaction
        {
            get { return (Transaction)GetValue(TransactionProperty); }
            set { SetValue(TransactionProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(IncomeMarker),
            new PropertyMetadata(null)
        );

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter",
            typeof(object),
            typeof(IncomeMarker),
            new PropertyMetadata(null)
        );

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        public IncomeMarker()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;

            this.Tapped += IncomeMarker_Tapped;
        }

        void IncomeMarker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Command != null)
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}
