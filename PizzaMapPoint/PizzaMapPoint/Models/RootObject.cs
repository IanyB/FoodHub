using System.Collections.Generic;

namespace PizzaMapPoint.Models
{
    public class RootObject
    {
        public Meta Meta { get; set; }
        public List<Notification> Notifications { get; set; }
        public Response Response { get; set; }
    }
}
