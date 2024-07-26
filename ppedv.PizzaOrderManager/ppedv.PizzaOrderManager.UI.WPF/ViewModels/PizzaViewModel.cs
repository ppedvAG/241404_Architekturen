using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

namespace ppedv.PizzaOrderManager.UI.WPF.ViewModels
{
    public class PizzaViewModel : ObservableObject
    {
        public ObservableCollection<Pizza> PizzaList { get; set; } = new ObservableCollection<Pizza>();

        private Pizza selectedPizza;
        private readonly IRepository repo;

        public Pizza SelectedPizza
        {
            get => selectedPizza;
            set
            {
                selectedPizza = value;
                //PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedPizza"));
                //PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PreisMitSteuer"));
                //PropertyChanged.Invoke(this, new PropertyChangedEventArgs(""));
                OnPropertyChanged(nameof(SelectedPizza));
                OnPropertyChanged(nameof(PreisMitSteuer));
            }
        }

        public string PreisMitSteuer
        {
            get
            {
                if (SelectedPizza == null)
                    return "---";

                return (SelectedPizza.Price * 1.19m).ToString("c", new CultureInfo("de-DE"));
            }
        }

        public AddNewCommand AddNewCommand { get; set; }
        public ICommand NewAddNewCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public PizzaViewModel(IRepository repo)
        {
            //PizzaList.Add(new Pizza() { Name = "Salami", Toppings = [new Topping() { Name = "Käse" }, new Topping() { Name = "Salami" }] });
            //PizzaList.Add(new Pizza() { Name = "Hawai", Toppings = [new Topping() { Name = "Käse" }, new Topping() { Name = "Ananas" }, new Topping() { Name = "Schinken" }] });

            AddNewCommand = new AddNewCommand(PizzaList);
            this.repo = repo;
            PizzaList = new ObservableCollection<Pizza>(repo.GetAll<Pizza>());

            NewAddNewCommand = new RelayCommand(UserWantsToAddANewPizza);
            SaveCommand = new RelayCommand(() => repo.SaveChanges());
        }

        private void UserWantsToAddANewPizza()
        {
            var newP = new Pizza() { Name = "NEU", Price = 7.77m };
            repo.Add(newP);
            PizzaList.Add(newP);
            SelectedPizza = newP;
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
