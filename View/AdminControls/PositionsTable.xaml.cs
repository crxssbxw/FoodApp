using FoodApp.Model;
using FoodApp.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace FoodApp.View.AdminControls
{
    /// <summary>
    /// Логика взаимодействия для Positions.xaml
    /// </summary>
    public partial class PositionsTable : UserControl
    {
        public PositionViewModel positionViewModel = AdminWindow.positionViewModel;
        public PositionsTable()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            PositionAdd position = new();
            position.Title = "Add Position";

            Position pos = new();

            position.DataContext = pos;

            if (position.ShowDialog() == true)
            {
                try
                {
                    positionViewModel.AddPosition(pos); 
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка при добавлении");
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            PositionAdd edit = new();
            edit.Title = "Edit Position";

            Position current = lvPositions.SelectedItem as Position;
            if (current != null)
            {
                edit.DataContext = current;

                if (edit.ShowDialog() == true)
                {
                    positionViewModel.EditPosition(current, current.Id);
                }
            }
            else
            {
                MessageBox.Show("Выберите позицию", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Position current = lvPositions.SelectedItem as Position;

            if (current != null)
            {
                positionViewModel.RemovePosition(current);
            }
        }
    }
}
