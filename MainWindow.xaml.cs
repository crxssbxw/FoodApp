using FoodApp.Model;
using FoodApp.Model.DPO;
using FoodApp.ModuleDB;
using FoodApp.View;
using FoodApp.View.UserWindows;
using FoodApp.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FoodApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static User currentUser;
        public static User CurrentUser
        {
            get => currentUser;
            set => currentUser = value;
        }

        private static OrderViewModel orderViewModel;
        public static OrderViewModel OrderViewModel
        {
            get => orderViewModel;
            set => orderViewModel = value;
        }


        private static PositionViewModel positions;
        public static PositionViewModel Positions
        {
            get => positions;
            set => positions = value;
        }

        private static BasketViewModel basket;
        public static BasketViewModel Basket
        {
            get => basket;
            set => basket = value;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            positions = new();
            positionList.ItemsSource = Positions.Positions;
            basket = new();
            basketList.ItemsSource = Basket.Basket;
            orderViewModel = new();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(positionList.ItemsSource);
            PropertyGroupDescription groupDescription = new("Type");
            view.GroupDescriptions.Add(groupDescription);

            if (currentUser == null)
            {
                AdminPanel.Visibility = Visibility.Collapsed;
                Cabinet.Visibility = Visibility.Collapsed;
            }
            else
            {
                LogIn.Visibility = Visibility.Collapsed;
                if (currentUser.Login == "admin") AdminPanel.Visibility = Visibility.Visible;
            }
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            if (adminWindow.ShowDialog() == true)
            {
                Positions = AdminWindow.positionViewModel;

                positionList.ItemsSource = Positions.Positions;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(positionList.ItemsSource);
                PropertyGroupDescription groupDescription = new("Type");
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(groupDescription);

                basketList.ItemsSource = Basket.Basket;
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Button senderbtn = sender as Button;
            var datacontext = senderbtn.DataContext;
            PositionDPO positionDPO;

            if (typeof(Position) == datacontext.GetType())
            {
                positionDPO = new(datacontext as Position);
            }
            else
            {
                positionDPO = datacontext as PositionDPO;
            }

            if (Basket.Basket.Any
                (
                    e => e.Id == positionDPO.Id
                ))
            {
                var pos = Basket.Basket.ToList().Find(e => e.Id == positionDPO.Id);
                pos.Count++;
                pos.TotalCost = pos.Count * pos.Cost;
                basketList.ItemsSource = Basket.Basket;
            }
            else
                Basket.Basket.Add(positionDPO);


            positionDPO.PropertyChanged += (sender, args) =>
            {
                Basket.OnPropertyChanged("Count");
                Basket.OnPropertyChanged("TotalCost");
                Basket.OnPropertyChanged("TotalCalories");
                Basket.OnPropertyChanged("TotalProteins");
                Basket.OnPropertyChanged("TotalFats");
                Basket.OnPropertyChanged("TotalCarbohydrates");
            };
        }
        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            Button senderbtn = sender as Button;
            var datacontext = senderbtn.DataContext;
            PositionDPO positionDPO;

            if (typeof(Position) == datacontext.GetType())
            {
                positionDPO = new(datacontext as Position);
            }
            else
            {
                positionDPO = datacontext as PositionDPO;
            }

            if (Basket.Basket.Any
                (
                    e => e.Id == positionDPO.Id
                ))
            {
                var pos = Basket.Basket.ToList().Find(e => e.Id == positionDPO.Id);
                if (pos.Count == 1)
                {
                    pos.Count--;
                    Basket.Basket.Remove(pos);
                    return;
                }
                pos.Count--;
                pos.TotalCost = pos.Count * pos.Cost;
                basketList.ItemsSource = Basket.Basket;
            }

            positionDPO.PropertyChanged += (sender, args) =>
            {
                Basket.OnPropertyChanged("Count");
                Basket.OnPropertyChanged("TotalCost");
                Basket.OnPropertyChanged("TotalCalories");
                Basket.OnPropertyChanged("TotalProteins");
                Basket.OnPropertyChanged("TotalFats");
                Basket.OnPropertyChanged("TotalCarbohydrates");
            };
        }

        private void BasketClear_Click(object sender, RoutedEventArgs e)
        {
            Basket.Basket.Clear();
            Basket.Basket.Clear();
        }

        private void OrderCreate_Click(object sender, RoutedEventArgs e)
        {
            if (Basket.Basket.Count == 0)
            {
                return;
            }

            Order order = new()
            {
                DateTime = DateTime.Now,
                TotalCost = basket.TotalCost
            };

            if (currentUser != null)
            {
                order.UserId = currentUser.Id;
            }

            ObservableCollection<Orders_Positions> orders_Positions = new();

            foreach (var position in Basket.Basket)
            {
                Orders_Positions op = new()
                {
                    OrderId = order.Id,
                    PositionId = position.Id,
                    PositionCount = (int)position.Count
                };

                orders_Positions.Add(op);
            }

            order.Orders_Positions = (ICollection<Orders_Positions>)orders_Positions;

            OrderInfo orderInfo = new()
            {
                DataContext = order,
                Owner = this
            };
            orderInfo.PositionList.ItemsSource = basket.Basket;

            if (orderInfo.ShowDialog() == true)
            {
                orderViewModel.Orders.Add(order);
                foreach (var ord in orders_Positions)
                {
                    orderViewModel.Orders_Positions.Add(ord);
                }

                using (ApplicationContext context = new ApplicationContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }

                basket.Basket.Clear();
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new()
            {
                Title = "Вход",
                DataContext = currentUser
            };

            loginWindow.WinTitle.Text = "Войдите в систему";

            if (loginWindow.ShowDialog() == true)
            {
                LogIn.Visibility = Visibility.Collapsed;
                Cabinet.Visibility = Visibility.Visible;

                if (currentUser.Login == "admin") AdminPanel.Visibility = Visibility.Visible;
            }
        }

        private void Cabinet_Click(object sender, RoutedEventArgs e)
        {
            UserCabinet userCabinet = new(currentUser)
            {
                Owner = this,
                Title = "Личный кабинет"
            };

            if (userCabinet.ShowDialog() == true)
            {
                if (userCabinet.logout)
                {
                    currentUser = null;

                    LogIn.Visibility = Visibility.Visible;
                    Cabinet.Visibility = Visibility.Collapsed;
                    AdminPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    using (ApplicationContext context = new())
                    {
                        var editedUser = context.Users.Where(e => e.Login == currentUser.Login).FirstOrDefault();
                        context.Users.Entry(editedUser).CurrentValues.SetValues(currentUser);

                        context.SaveChanges();
                    }
                }
            }
        }

        private void positionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PositionInfo positionInfo = new()
            {
                Owner = this
            };
            positionInfo.DataContext = positionList.SelectedItem;

            positionInfo.Show();
        }
    }
}
