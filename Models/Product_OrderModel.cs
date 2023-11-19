namespace EF_CRUD_REST_API.Models
{
    public class Product_OrderModel
    {
        public long id { get; set; }
        public long amount { get; set; }
        public DateTime date_and_time { get; set; }
        public short status_id { get; set; }
        public long customer_id { get; set; }
    }
}
