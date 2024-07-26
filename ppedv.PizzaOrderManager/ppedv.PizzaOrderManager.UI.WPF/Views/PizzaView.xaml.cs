using Microsoft.Extensions.DependencyInjection;
using ppedv.PizzaOrderManager.UI.WPF.ViewModels;
using System.Windows.Controls;

namespace ppedv.PizzaOrderManager.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for PizzaView.xaml
    /// </summary>
    public partial class PizzaView : UserControl
    {
        public PizzaView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetServices<PizzaViewModel>();
        }
    }
}
