namespace FoodApp.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string PackageType { get; set; }
        public float TotalCost { get; set; }
        public DateTime DateTime { get; set; }
        public int? UserId { get; set; }

        //Navigations
        public ICollection<Position> Positions { get; set; }
        public ICollection<Orders_Positions> Orders_Positions { get; set; }
        public User? User { get; set; }

    }
}
