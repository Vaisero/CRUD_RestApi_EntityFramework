using Microsoft.EntityFrameworkCore;

namespace EF_CRUD_REST_API
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }


    }
}
