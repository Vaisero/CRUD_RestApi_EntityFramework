using Microsoft.EntityFrameworkCore;

namespace EF_CRUD_REST_API.Models
{
    [Keyless]
    public class AvgSummPerHour_procedureModel
    {
        public string hour { get; set; }
        public long avg_summ { get; set; }
    }
}

