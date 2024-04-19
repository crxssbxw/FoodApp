using System.Windows;

namespace FoodApp.View
{
    /// <summary>
    /// Логика взаимодействия для OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        private List<string> packageTypes = new() { "С собой", "В ресторане" };

        public List<string> PackageTypes { get => packageTypes; set => packageTypes = value; }

        public OrderInfo()
        {
            InitializeComponent();
            PackageTypeChoose.ItemsSource = PackageTypes;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = true;
            }
            catch (Exception ex)
            {
                this.Close();
            }
        }
    }
}
