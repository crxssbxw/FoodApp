using FoodApp.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodApp.Model
{
    public class Position : NotificationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public string Type { get; set; }
        public float Mass { get; set; }
        public float Calories { get; set; }
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Orders_Positions> Orders_Positions { get; set; }

        public Position() { }

        [NotMapped]
        public string ImagePath
        {
            get => GetImageByType();
        }

        public string GetImageByType()
        {
            switch (Type)
            {
                case "Ролл":
                    return "/../Images/kebab.png";
                case "Напиток":
                    return "/../Images/drink.png";
                case "Картофель":
                    return "/../Images/french_fries.png";
                case "Мясо":
                    return "/../Images/nuggets.png";
                case "Соус":
                    return "/../Images/sauce.png";
                case "Салат":
                    return "/../Images/salad.png";
                case "Десерт":
                    return "/../Images/donut.png";
                default:
                    return "/../Images/cheeseburger.png";
            }
        }
    }
}
