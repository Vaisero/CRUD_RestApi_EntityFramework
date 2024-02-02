# CRUD_RestApi_EntityFramework


## Техническое задание

1. Спроектировать БД для хранения следующих данных:
- Клиент (Имя, Фамилия, Дата рождения)
- Заказ (Сумма, Дата и время, Статус (Не обработан, Отменен, Выполнен), Клиент)

2. Составить запросы на получение следующей информации и оформить в виде хранимой процедуры:
- Получить сумму заказов со статусом выполнен по каждому клиенту, произведенных в день рождения клиента
- Получить список часов от 00.00 до 24.00 в порядке убывания со средним чеком за каждый час (Средний чек=Сумма заказов/Кол-во заказов) по всем заказам со статусом Выполнен

3. Создать проект с REST API к данным спроектированной БД
Методы api:
- Получение заказов (Фильтрация по всем полям, Пагинация)
- Создание, Редактирование, Удаление заказа
- Получение клиентов (Фильтрация по всем полям, Пагинация)
- Создание, Редактирование, Удаление клиента
- Получение данных из задания 2. Статус заказа, по которым производить выборку, получать из запроса
Условия:
- Использовать .NET 6 или .NET 7 
БД на выбор (MSSQL, PostgreSQL)
 В качестве ORM использовать Entity Framework (по желанию можно Dapper)

## Технологии

* [.NET (CORE) 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [PostgreSQL](https://www.postgresql.org/)
* [Entity Framework](https://learn.microsoft.com/ru-ru/ef/)

  ![1](https://i.ibb.co/mDkdxrr/Swagger.png)

## Скрипты для создания базы данных PostgreSQL


- Таблица клиентов по ТЗ (customer):
  
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

- Таблица заказа клиента по ТЗ (product_order):
  
`
CREATE TABLE IF NOT EXISTS public.product_order
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    amount bigint NOT NULL,
    date_and_time timestamp with time zone NOT NULL,
    status_id smallint NOT NULL,
    customer_id bigint NOT NULL,
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

- Таблица статуса заказа по ТЗ (status):
  
`
CREATE TABLE IF NOT EXISTS public.status
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 32767 CACHE 1 ),
    status_name text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT status_pkey PRIMARY KEY (id)
)
`

## Скрипты для создания хранимых процедур в PostgreSQL

- Получить сумму заказов со статусом выполнен по каждому клиенту, произведенных в день рождения клиента
  

`
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
`

- Получить список часов от 00.00 до 24.00 в порядке убывания со средним чеком за каждый час (Средний чек=Сумма заказов/Кол-во заказов) по всем заказам со статусом Выполнен

`
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
`
