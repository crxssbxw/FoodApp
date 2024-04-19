using FoodApp.Model.DPO;
using System.Collections.ObjectModel;


namespace FoodApp.ViewModel
{
    public class BasketViewModel : NotificationVM
    {
        private ObservableCollection<PositionDPO> basket = new();

        public BasketViewModel()
        {
            Basket.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(TotalCost));
                OnPropertyChanged(nameof(TotalCalories));
                OnPropertyChanged(nameof(TotalProteins));
                OnPropertyChanged(nameof(TotalFats));
                OnPropertyChanged(nameof(TotalCarbohydrates));
            };
        }

        public ObservableCollection<PositionDPO> Basket { get => basket; set => basket = value; }

        public uint Count
        {
            get => CountTotalCount();
        }

        public float TotalCost
        {
            get => CountTotalCost();
        }

        public float TotalCalories
        {
            get => CountTotalCalories();
        }

        public float TotalProteins
        {
            get => CountTotalProteins();
        }

        public float TotalFats
        {
            get => CountTotalFats();
        }

        public float TotalCarbohydrates
        {
            get => CountTotalCarbohydrates();
        }

        public float CountTotalCalories()
        {
            float calories = 0;
            foreach (var item in Basket)
            {
                calories += (item.Calories * (item.Mass / 100)) * item.Count;
            }
            return calories;
        }

        public float CountTotalProteins()
        {
            float proteins = 0;
            foreach (var item in Basket)
            {
                proteins += (item.Proteins * (item.Mass / 100)) * item.Count;
            }
            return proteins;
        }

        public float CountTotalFats()
        {
            float fats = 0;
            foreach (var item in Basket)
            {
                fats += (item.Fats * (item.Mass / 100)) * item.Count;
            }
            return fats;
        }

        public float CountTotalCarbohydrates()
        {
            float carbohydrates = 0;
            foreach (var item in Basket)
            {
                carbohydrates += (item.Carbohydrates * (item.Mass / 100)) * item.Count;
            }
            return carbohydrates;
        }

        public float CountTotalCost()
        {
            float totalCost = 0;
            foreach (var item in basket)
            {
                totalCost += item.Cost * item.Count;
            }
            return totalCost;
        }

        public uint CountTotalCount()
        {
            uint totalCount = 0;
            foreach (var item in basket)
            {
                totalCount += item.Count;
            }
            return totalCount;
        }
    }
}
