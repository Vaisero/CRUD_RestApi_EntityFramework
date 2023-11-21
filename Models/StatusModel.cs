using System.ComponentModel.DataAnnotations;

namespace EF_CRUD_REST_API.Models
{
    public class StatusModel
    {
        [Key]
        public short id { get; private set; }

        public string status_name { get; }
    }
}