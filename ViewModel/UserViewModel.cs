using FoodApp.Model;
using FoodApp.ModuleDB;
using System.Collections.ObjectModel;

namespace FoodApp.ViewModel
{
    public class UserViewModel : NotificationVM
    {
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users { get => users; set => users = value; }

        public UserViewModel()
        {
            Users = new();
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (User user in context.Users.ToList())
                {
                    Users.Add(user);
                }
            }
        }
    }
}
