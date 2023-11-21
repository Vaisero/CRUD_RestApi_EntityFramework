using System.ComponentModel.DataAnnotations;

namespace EF_CRUD_REST_API.Models
{
    public class Product_OrderModel
    {
        [Key]
        public long id { get; private set; }

        public long amount { get; set; }
        public DateTime date_and_time { get; set; }
        public long status_id { get; set; }
        public long customer_id { get; set; }
    }
}



/*
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
 */