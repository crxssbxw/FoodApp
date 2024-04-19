using FoodApp.ViewModel;
using System.Windows.Controls;

namespace FoodApp.View.AdminControls
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        public Orders()
        {
            InitializeComponent();
            DataContext = new OrderViewModel();
        }
    }
}
