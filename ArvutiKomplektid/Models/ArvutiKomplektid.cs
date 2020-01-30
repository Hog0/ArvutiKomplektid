using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArvutiKomplektid.Models
{
    public class ArvutiKomplektid : ApiController
    {

        public int Id { get; set; }
        public string Kirjeldus { get; set; }
        [Range(0, 1)]
        public int Korpus { get; set; }
        [Range(0, 1)]
        public int Kuvar { get; set; }
        [Range(0, 1)]
        public int Pakitud { get; set; }


    }
}
