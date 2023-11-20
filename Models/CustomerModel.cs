using System.ComponentModel.DataAnnotations;

namespace EF_CRUD_REST_API.Models
{
    public class CustomerModel
    {
        [Key]
        public long id { get; private set; }

        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_of_birth { get; set; }
    }
}
