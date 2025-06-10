using APIsDeCondomino.model;
using APIsDeCondomino.service;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIsDeCondomino.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class ClienteController : ControllerBase
    {
        Utilidades _ferramentas;

        private readonly StrCaminho _conDBF;
        private readonly DBFReader _dbfReader;

        public ClienteController(StrCaminho conDBF)
        {
            _conDBF = conDBF;
            _dbfReader = new DBFReader(conDBF);
            _ferramentas = new Utilidades(conDBF);


        }

        [HttpGet("Identifica")]
        public ActionResult IdentificarCliente(string valorEntrada)
        {
            
            // a variavel valorEntrada, pode ser um número de telefone, cpf ou email
            string TipoDado = _ferramentas.IdentificarTipo(valorEntrada);

            if (TipoDado == "Telefone")
            {

                List<Clientes> clienteTel = _dbfReader.DBFIdentificarClienteTelefone(valorEntrada);

                return Ok(clienteTel);
            }
            else
            {
                return BadRequest("Dado invalido");
            }
        }

    }
}
