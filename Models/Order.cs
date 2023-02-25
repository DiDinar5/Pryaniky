using Microsoft.Build.Framework;

namespace Pryaniky.Models
{
    public class Order
    {
        public Order(List<Pryanik> pryaniks)
        {
            Pryaniks = pryaniks;
            DateTime = DateTime.Now;
            Sum = pryaniks.Sum(x => x.Price);
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public List<Pryanik> Pryaniks { get; set; }  
        public decimal Sum { get; }
        public Order()
        {
            DateTime= DateTime.Now;
            Pryaniks= new List<Pryanik>();
        }
    }
}
