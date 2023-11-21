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



/*
 CREATE OR REPLACE FUNCTION public.avgsummperhour_select(
	_status_name smallint)
    RETURNS TABLE(hour text, avg_summ bigint) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
	declare
    _startTime time = '00:00'::time;
    _counter int = 0;
begin
    create temp table _tmps(hour text, avgSumm bigint) on commit drop;

    while (_counter <> 24) loop
        insert into _tmps
        select _startTime::text  || ' - ' ||  _startTime + interval '1 hour', coalesce (sum(o.amount)/count(o.id),0)
        from public.product_order as o
        inner join public.status as s on s.id = o.status_id
        where s.id = _status_name
        and o.date_and_time::time between _startTime and _startTime + interval '1 hour';
        _startTime = _startTime + interval '1 hour';
        _counter = _counter + 1;
    end loop;

    return query select * from _tmps;
end;
$BODY$;
 */