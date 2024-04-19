using System.Collections.ObjectModel;
using System.Windows;

namespace FoodApp.View.AdminControls
{
    /// <summary>
    /// Логика взаимодействия для PositionAdd.xaml
    /// </summary>
    public partial class PositionAdd : Window
    {
        private ObservableCollection<string> types = new() { "Бургер", "Ролл", "Напиток", "Картофель", "Мясо", "Соус", "Салат", "Десерт" };
        public ObservableCollection<string> Types { get => types; }

        public PositionAdd()
        {
            InitializeComponent();
            TypesBox.ItemsSource = Types;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
