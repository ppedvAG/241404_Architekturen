using ppedv.PizzaOrderManager.Model.DomainModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

namespace ppedv.PizzaOrderManager.UI.WPF.ViewModels
{
    public class PizzaViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Pizza> PizzaList { get; set; } = new ObservableCollection<Pizza>();
        
        private Pizza selectedPizza;
        public Pizza SelectedPizza
        {
            get => selectedPizza;
            set
            {
                selectedPizza = value;
                //PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedPizza"));
                //PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PreisMitSteuer"));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(""));
            }
        }

        public string PreisMitSteuer
        {
            get
            {
                if (SelectedPizza == null)
                    return "---";
                
                return (SelectedPizza.Price * 1.19m).ToString("c",new CultureInfo("de-DE")); 
            }
        }

        public AddNewCommand AddNewCommand { get; set; }

        public PizzaViewModel()
        {
            PizzaList.Add(new Pizza() { Name = "Salami", Toppings = [new Topping() { Name = "Käse" }, new Topping() { Name = "Salami" }] });
            PizzaList.Add(new Pizza() { Name = "Hawai", Toppings = [new Topping() { Name = "Käse" }, new Topping() { Name = "Ananas" }, new Topping() { Name = "Schinken" }] });

            AddNewCommand = new AddNewCommand(PizzaList);
        }

    }

    public class AddNewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public ObservableCollection<Pizza> PizzaList;
        public AddNewCommand(ObservableCollection<Pizza> pizzaList)
        {
            PizzaList = pizzaList;
        }

        public void Execute(object? parameter)
        {
            PizzaList.Add(new Pizza() { Name = "NEU" });
        }
    }
}
