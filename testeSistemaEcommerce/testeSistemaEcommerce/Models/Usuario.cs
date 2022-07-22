using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testeSistemaEcommerce.Models
{
    public class Usuario
    {
        public string Nome { get; set; }
        public int TipoPessoa { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

    }
}