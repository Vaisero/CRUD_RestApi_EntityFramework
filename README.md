customer: 
`
CREATE TABLE IF NOT EXISTS public.customer
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    first_name text COLLATE pg_catalog."default" NOT NULL,
    last_name text COLLATE pg_catalog."default" NOT NULL,
    date_of_birth date NOT NULL,
    CONSTRAINT "Customer_pkey" PRIMARY KEY (id)
)
`

product_order:
`
CREATE TABLE IF NOT EXISTS public.product_order
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    amount bigint NOT NULL,
    date_and_time timestamp with time zone NOT NULL,
    status_id integer NOT NULL,
    customer_id integer NOT NULL,
    CONSTRAINT "Order_pkey" PRIMARY KEY (id),
    CONSTRAINT product_order_customer_id_fkey FOREIGN KEY (customer_id)
        REFERENCES public.customer (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT product_order_status_id_fkey FOREIGN KEY (status_id)
        REFERENCES public.status (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
`

status:
`
CREATE TABLE IF NOT EXISTS public.status
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 32767 CACHE 1 ),
    status_name text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT status_pkey PRIMARY KEY (id)
)
`

Хранимые процедуры:

- Получить сумму заказов со статусом выполнен по каждому клиенту, произведенных в день рождения клиента
`
create or replace function public.OrderSummFromBirthday_select()
returns bigint
language 'plpgsql'
as $$
begin
	return sum(o.amount) from product_order as o
	inner join public.customer as c on c.id = o.customer_id
	inner join public.status as s on s.id = o.status_id
	where s.id = 3
	and extract (month from o.date_and_time::date) = extract (month from c.date_of_birth)
	and extract (day from o.date_and_time::date) = extract (day from c.date_of_birth);
end;
$$;
`
