using System.Collections.Generic;

namespace PizzaMapPoint.Models
{
    public class Venue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Contact Contact { get; set; }
        public Location Location { get; set; }
        public List<object> Categories { get; set; }
        public bool Verified { get; set; }
        public Stats Stats { get; set; }
        public string Url { get; set; }
        public Specials Specials { get; set; }
        public HereNow HereNow { get; set; }
        public string StoreId { get; set; }
        public string ReferralId { get; set; }
    }
}
