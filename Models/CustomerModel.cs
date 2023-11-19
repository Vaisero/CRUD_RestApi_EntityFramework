namespace EF_CRUD_REST_API.Models
{
    public class CustomerModel
    {
        public long id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateOnly date_of_birth { get; set; }
    }
}
