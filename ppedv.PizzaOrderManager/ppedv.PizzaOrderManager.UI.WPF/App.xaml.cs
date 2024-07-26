using Microsoft.Extensions.DependencyInjection;
using ppedv.PizzaOrderManager.Data.EfCore;
using ppedv.PizzaOrderManager.Logic;
using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.UI.WPF.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ppedv.PizzaOrderManager.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            string conString = "Server=(localdb)\\mssqllocaldb;Database=PizzaOrderManager_test;Trusted_Connection=true;";

            var services = new ServiceCollection();

            services.AddTransient<IRepository>(x => new EfContextIRepositoryAdapter(conString));
            services.AddSingleton<OrderService>();

            services.AddSingleton<PizzaViewModel>();


            return services.BuildServiceProvider();
        }
    }

}
