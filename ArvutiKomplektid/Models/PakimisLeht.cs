using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArvutiKomplektid.Models
{
    public class PakimisLeht : ApiController
    {
        public int Id { get; set; }
        public string Kirjeldus { get; set; }
        public int Komplekt { get; set; }
        public string Korpus { get; set; }
        public string Kuvar { get; set; }
        public string Pakitud { get; set; }
    }
}
