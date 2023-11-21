using EF_CRUD_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EF_CRUD_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedureController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProcedureController(AppDBContext context)
        {
            _context = context;
        }

        //GET: api/status
        [HttpGet]
        [Route("OrderSummFromBirthday")]
        public async Task<ActionResult<IEnumerable<OrderSummFromBirthday_procedureModel>>> GetOrderSummFromBirthday(short statusNameOfOrderSummFromBirthday_procedure = 0)
        {
            try
            {
                NpgsqlParameter parameter = new NpgsqlParameter("@_status_name", (Int16)statusNameOfOrderSummFromBirthday_procedure);
                var result = await _context.orderSummFromBirthday.FromSqlRaw("select * from public.ordersummfrombirthday_select (@_status_name)", parameter).ToListAsync();
                if (result != null && result.Count > 0)  
                {
                    return Ok(result);
                }
                else
                    return BadRequest("Таблица пуста");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //GET: api/status
        [HttpGet]
        [Route("AvgSummPerHour")]
        public async Task<ActionResult<IEnumerable<AvgSummPerHour_procedureModel>>> GetAvgSummPerHour(short statusNameOfAvgSummPerHour_procedure = 0)
        {
            try
            {
                NpgsqlParameter parameter = new NpgsqlParameter("@_status_name", (Int16)statusNameOfAvgSummPerHour_procedure);
                var result = await _context.avgSummPerHour.FromSqlRaw("select * from public.avgsummperhour_select (@_status_name)", parameter).ToListAsync();
                if (result != null && result.Count > 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest("Таблица пуста");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
