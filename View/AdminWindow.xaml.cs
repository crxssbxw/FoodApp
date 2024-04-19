using FoodApp.ViewModel;
using System.Windows;

namespace FoodApp.View
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        AdminViewModel viewModel = new();
        public static OrderViewModel orderViewModel = new();
        public static PositionViewModel positionViewModel = new();
        public static UserViewModel userViewModel = new();

        public AdminWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public void Orders_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentViewModel = orderViewModel;
        }

        public void Positions_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentViewModel = positionViewModel;
        }

        public void Users_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentViewModel = userViewModel;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
