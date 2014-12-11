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
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label",
            typeof(string),
            typeof(IncomeMarker),
            new PropertyMetadata(null)
        );
        public static readonly DependencyProperty AmountProperty = DependencyProperty.Register(
            "Amount",
            typeof(double),
            typeof(IncomeMarker),
            new PropertyMetadata(null)
        );
        public static readonly DependencyProperty DateProperty = DependencyProperty.Register(
            "Date",
            typeof(DateTime),
            typeof(IncomeMarker),
            new PropertyMetadata(null)
        );

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public double Amount
        {
            get { return (double)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public SpendingMarker()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }
    }
}
