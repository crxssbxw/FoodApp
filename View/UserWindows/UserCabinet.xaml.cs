using FoodApp.Model;
using FoodApp.Model.DPO;
using FoodApp.ModuleDB;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FoodApp.View.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserCabinet.xaml
    /// </summary>
    public partial class UserCabinet : Window
    {
        public bool logout;

        private ObservableCollection<Order> orders = new();
        public ObservableCollection<Order> Orders { get => orders; set => orders = value; }

        public UserCabinet(User user)
        {
            DataContext = user;
            InitializeComponent();
            using (ApplicationContext context = new ApplicationContext())
            {
                User currentUser = DataContext as User;
                List<Order> findOrders = new(context.Orders.Where(e => e.UserId == currentUser.Id).ToList());
                foreach (var item in findOrders)
                {
                    Orders.Add(item);
                }
            }
            userOrders.ItemsSource = Orders;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            logout = true;
            DialogResult = true;
        }

        private void userOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (userOrders.SelectedItem == null)
            {
                return;
            }

            OrderInfo orderInfo = new()
            {
                Owner = this
            };

            Order currentOrder = userOrders.SelectedItem as Order;
            List<PositionDPO> positions = new();

            using (ApplicationContext context = new())
            {
                var selectedOrderPositions = from o in context.Orders_Positions
                                             where o.OrderId == currentOrder.Id
                                             select o;

                List<Orders_Positions> list = new();

                foreach (var pos in selectedOrderPositions)
                {
                    list.Add(pos);
                }

                foreach (var pos in list)
                {

                    PositionDPO positionDPO = new(context.Positions.Find(pos.PositionId))
                    {
                        Count = (uint)pos.PositionCount
                    };
                    positions.Add(positionDPO);
                }
            }

            orderInfo.PositionList.ItemsSource = positions;
            orderInfo.DataContext = currentOrder;
            orderInfo.Accept.Content = "OK";
            orderInfo.Cancel.Visibility = Visibility.Collapsed;
            orderInfo.PackageTypeChoose.IsEditable = false;

            orderInfo.Show();
        }
    }
}
