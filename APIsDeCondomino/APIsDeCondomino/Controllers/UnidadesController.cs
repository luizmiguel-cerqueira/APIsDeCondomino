using System;
using APIsDeCondomino.model;
using APIsDeCondomino.service;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

namespace APIsDeCondomino.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadesController : ControllerBase
    {

        private readonly StrCaminho _conDBF;
        private readonly DBFReader _dbfReader;
        private readonly Utilidades _servicos;

        public UnidadesController(StrCaminho conDBF)
        {
            _conDBF = conDBF;
            _dbfReader = new DBFReader(conDBF);
            _servicos = new Utilidades(conDBF);
        }

        [HttpGet("ListarUnidade")]

        public IActionResult ApiListaUnidade(string telefone)
        {
            List<Unidade> listaUnidade = _dbfReader.DBFListarUnidades(telefone);
            if (listaUnidade.Count == 0)
            {
                return NotFound("Nenhuma unidade vinculada ao cliente informado");
            }
            return Ok(listaUnidade);

        }
        [HttpGet("ListarBoleto")]
        public IActionResult ApiListarBoletos(string codCondominio, string unidade, string? bloco)
        {
            List<string> numRec = _dbfReader.DBFListarBoleto(codCondominio, unidade, bloco);
            string urlBoleto = "URL de boleto";

            for (int Num = 0; Num < numRec.Count; Num++)
            {
                numRec[Num] = urlBoleto + numRec[Num].Trim();
            }

            return Ok(numRec);
        }
        [HttpGet("ListarAnosAtas")]
        public IActionResult ApiListarAnosAtas(string tipoAta, string codCondominio)
        {
            List<string> anosAtas = _servicos.ListarAnosAtas(tipoAta, codCondominio);

            return Ok(anosAtas);

        }
        [HttpGet("ListarAtas")]
        public IActionResult ApiListarAtas(string tipoAta, string codCondominio, string ano)
        {
            List<string> ata = _servicos.ListarAtas(tipoAta, codCondominio, ano);

            string urlAta = $@"URL de atas Aqui";

            for (int i = 0; i < ata.Count; i++) 
            {
                ata[i] = urlAta + ata[i];
            }

            return Ok(ata);

        }
        [HttpGet("ListarConvencao")]
        public IActionResult ApiListarConvencao(string codCondominio) 
        {
            List<string> convencao = _servicos.ListarConvencao(codCondominio);

            string urlConv = $@"URL de convenção aqui";

            for (int i = 0; i < convencao.Count; i++)
            {
                convencao[i] = urlConv + convencao[i];
                convencao[i] = Uri.EscapeUriString(convencao[i]);
            }

            return Ok(convencao);
        }
        
    }
}
