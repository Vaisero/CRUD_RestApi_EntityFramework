using System.ComponentModel.DataAnnotations;

namespace EF_CRUD_REST_API.Models
{
    public class Product_OrderModel
    {
        [Key]
        public long id { get; private set; }

        public long amount { get; set; }
        public DateTime date_and_time { get; set; }
        public long status_id { get; set; }
        public long customer_id { get; set; }
    }
}
