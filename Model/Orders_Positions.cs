using Microsoft.EntityFrameworkCore;

namespace FoodApp.Model
{
    [PrimaryKey(nameof(OrderId), nameof(PositionId))]
    public class Orders_Positions
    {

        public int OrderId { get; set; }
        public int PositionId { get; set; }
        public int PositionCount { get; set; } = 0;

        //Navigation
        public Order Order { get; set; }
        public Position Position { get; set; }
    }
}
