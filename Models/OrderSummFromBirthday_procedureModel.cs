using Microsoft.EntityFrameworkCore;

namespace EF_CRUD_REST_API.Models
{
    [Keyless]
    public class OrderSummFromBirthday_procedureModel
    {
        public long summ { get; set; }
    }
}



/*
 CREATE OR REPLACE FUNCTION public.ordersummfrombirthday_select(
	_status_name smallint)
    RETURNS bigint
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
begin
	return coalesce (sum(o.amount),0) from product_order as o
	inner join public.customer as c on c.id = o.customer_id
	inner join public.status as s on s.id = o.status_id
	where s.id = _status_name
	and extract (month from o.date_and_time::date) = extract (month from c.date_of_birth)
	and extract (day from o.date_and_time::date) = extract (day from c.date_of_birth);
	
	
end;
$BODY$;
 */