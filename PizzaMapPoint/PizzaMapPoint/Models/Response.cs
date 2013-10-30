using System.Collections.Generic;

namespace PizzaMapPoint.Models
{
    public class Response
    {
        public List<Venue> Venues { get; set; }
        public double Confidence { get; set; }
    }
}
