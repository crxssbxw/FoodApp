using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Model.DPO
{
    public class PositionDPO : Position, INotifyPropertyChanged
    {
        private float totalCost;
        public float TotalCost 
        {
            set
            {
                totalCost = value;
                OnPropertyChanged(nameof(TotalCost));
            }
            get => totalCost; 

        }

        private uint count;
        public uint Count 
        {
            get => count;
            set 
            { 
                count = value;
                OnPropertyChanged(nameof(Count)); 
            }
            
        }

        public PositionDPO(Position position)
        {
            this.Id = position.Id;
            this.Name = position.Name;
            this.Cost = position.Cost;
            this.Mass = position.Mass;
            this.Proteins = position.Proteins;
            this.Calories = position.Calories;
            this.Carbohydrates = position.Carbohydrates;
            this.Fats = position.Fats;
            this.Type = position.Type;
            this.Count++;
            this.TotalCost = Count * Cost;
        }

        public PositionDPO(uint count) : base()
        {
            Count = count;
        }

        public PositionDPO()
        { }

        public float CountCost() => Count * Cost; 


        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
