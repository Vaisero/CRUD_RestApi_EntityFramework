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


/*
 CREATE TABLE IF NOT EXISTS public.status
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 32767 CACHE 1 ),
    status_name text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT status_pkey PRIMARY KEY (id)
)
 */