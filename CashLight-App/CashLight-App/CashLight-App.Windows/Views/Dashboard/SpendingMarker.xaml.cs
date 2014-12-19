using CashLight_App.Models;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CashLight_App.Views.Dashboard
{
    public sealed partial class SpendingMarker : UserControl
    {
        public static readonly DependencyProperty TransactionProperty = DependencyProperty.Register(
            "Transaction",
            typeof(Transaction),
            typeof(SpendingMarker),
            new PropertyMetadata(null)
        );

        public Transaction Transaction
        {
            get { return (Transaction)GetValue(TransactionProperty); }
            set { SetValue(TransactionProperty, value); }
        }

        public SpendingMarker()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }
    }
}
