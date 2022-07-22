using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testeSistemaEcommerce.Models;
using testeSistemaEcommerce.Repository;

namespace testeSistemaEcommerce.Controllers
{
    public class RegisterController : Controller
    {
        //##################################################################################################################
        [HttpPost]
        public string GravarUser(Usuario usuario)
        {
            var resultado = new StatusResult();
            resultado.StatusCode = 0;
            resultado.StatusMessage = "";
            if (usuario.Nome == null) {
                resultado.StatusCode += 1;
                resultado.StatusMessage += "O campo Nome é obrigatório!<br>";
            }
            if (usuario.Telefone == null) { 
                resultado.StatusCode += 1;
                resultado.StatusMessage += "O campo Telefone é obrigatório!<br>";
            }
            if (usuario.Email == null) { 
                resultado.StatusCode += 1;
                resultado.StatusMessage += "O campo Email é obrigatório!<br>";
            }
            if (usuario.TipoPessoa == 0) { 
                if(usuario.Documento == null) { 
                    resultado.StatusCode += 1;
                    resultado.StatusMessage += "O campo CPF é obrigatório!<br>";
                }
            }
            else
            {
                if (usuario.Documento == null) { 
                    resultado.StatusCode += 1;
                    resultado.StatusMessage += "O campo CNPJ é obrigatório!<br>";
                }
            }
            if (resultado.StatusCode == 0)
            {
                return JsonConvert.SerializeObject(AccessRepository.GravarUser(usuario), Formatting.Indented);
            }
            else
            {
                return JsonConvert.SerializeObject(resultado);
            }

        }
    }
}