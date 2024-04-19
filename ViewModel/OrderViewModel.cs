using FoodApp.Model;
using FoodApp.ModuleDB;
using System.Collections.ObjectModel;
using System.Windows;

namespace FoodApp.ViewModel
{
    public class OrderViewModel : NotificationVM
    {
        private ObservableCollection<Order> orders = new();
        public ObservableCollection<Order> Orders { get => orders; set => orders = value; }

        private ObservableCollection<Orders_Positions> orders_Positions = new();
        public ObservableCollection<Orders_Positions> Orders_Positions { get => orders_Positions; set => orders_Positions = value; }

        public OrderViewModel()
        {
            Orders = new ObservableCollection<Order>();
            using (ApplicationContext context = new ApplicationContext())
            {
                try
                {
                    foreach (Order order in context.Orders.ToList())
                    {
                        Orders.Add(order);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, ex.Message);
                }

            }
        }
    }
}
