using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArvutiKomplektid.Models
{
    public class ArvutiTellimus

    {

        public int Id { get; set; }
        public string Kirjeldus { get; set; }
        public int Komplekt { get; set; } = -1;
        [Range(0, 1)]
        public int Korpus { get; set; } = -1;
        [Range(0, 1)]
        public int Kuvar { get; set; } = -1;
        [Range(0, 1)]
        public int Pakitud { get; set; } = 0;
    }
}
