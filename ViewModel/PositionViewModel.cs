using FoodApp.Model;
using FoodApp.ModuleDB;
using System.Collections.ObjectModel;

namespace FoodApp.ViewModel
{
    public class PositionViewModel : NotificationVM
    {
        ApplicationContext context;
        private ObservableCollection<Position> positions;
        public ObservableCollection<Position> Positions { get => positions; set => positions = value; }

        public void AddPosition(Position position)
        {
            if (position.Name == string.Empty || position.Name == null || position.Type == null || position.Type == string.Empty)
                throw new Exception("Не все данные были введены");

            Positions.Add(position);
            using (ApplicationContext context = new())
            {
                context.Positions.Add(position);
                context.SaveChanges();
            }
        }

        public void EditPosition(Position position, int Id)
        {
            var editable = Positions.ToList().Find(p => position.Id == Id);
            editable = position;
            using (ApplicationContext context = new())
            {
                var dbeditable = context.Positions.Find(position.Id);
                context.Positions.Entry(dbeditable).CurrentValues.SetValues(position);
                context.SaveChanges();
            }
        }

        public void RemovePosition(Position position)
        {
            Positions.Remove(position);
            using (ApplicationContext context = new())
            {
                context.Positions.Remove(position);
                context.SaveChanges();
            }
        }

        public PositionViewModel()
        {
            Positions = new();
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (Position position in context.Positions.ToList())
                {
                    Positions.Add(position);
                }
            }
        }
    }
}
