using System.ComponentModel.DataAnnotations;

namespace EF_CRUD_REST_API.Models
{
    public class CustomerModel
    {
        [Key]
        public long id { get; private set; }

        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateOnly date_of_birth { get; set; }
    }
}




/*
 CREATE TABLE IF NOT EXISTS public.customer
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    first_name text COLLATE pg_catalog."default" NOT NULL,
    last_name text COLLATE pg_catalog."default" NOT NULL,
    date_of_birth date NOT NULL,
    CONSTRAINT "Customer_pkey" PRIMARY KEY (id)
)
 */