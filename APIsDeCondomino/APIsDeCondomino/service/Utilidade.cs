using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;
using APIsDeCondomino.model;

namespace APIsDeCondomino.service
{
    public class Utilidades
    {
        private readonly StrCaminho _conDBF;
        public Utilidades(StrCaminho conDBF)
        {
            _conDBF = conDBF;
        }

        public string IdentificarTipo(string input)
        {
            input = input.Trim();

            // Regex para email
            var regexEmail = new Regex(@"^[\w\.-]+@[\w\.-]+\.\w{2,}$");

            // Regex para CPF (somente números ou com pontuação)
            var regexCPF = new Regex(@"^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$");

            // Regex para telefone (aceita formatos com DDD e traço)
            var regexTelefone = new Regex(@"^\(?\+?\d{2}?\)?\(?\d{2,3}\)?\s?\d{4,5}-?\d{4}$");

            if (regexEmail.IsMatch(input))
                return "Email";
            else if (regexCPF.IsMatch(input))
                return "CPF";
            else if (regexTelefone.IsMatch(input))
                return "Telefone";
            else
                return "Desconhecido";
        }


        public Boolean ValidarCPF(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Distinct().Count() == 1)
            { return false; }

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (cpf[i] - '0') * (10 - i);


            }
            int digito1 = soma % 11;

            // tradução: se digito1 menor que 2, então ==0, então digito1 = 11 - digito1
            digito1 = digito1 < 2 ? 0 : 11 - digito1;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (cpf[i] - '0') * (10 - i);
            }
            int digito2 = soma % 11;

            digito2 = digito2 < 2 ? 0 : 11 - digito2;
            if (cpf[9] == digito1 & cpf[10] == digito2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<string> ListarAnosAtas(string tipoAta, string codCondominio)
        {
            List<string> anos = new List<string>();

            string caminho = $@"{_conDBF.CaminhoDoc}{codCondominio}\Atas\{tipoAta}";

            if (!Directory.Exists(caminho))
            {
                return new List<string>();
            }

            string[] arquivos = Directory.GetFiles(caminho);

            List<string> listaAno = arquivos
                .Select(arquivo => Path.GetFileNameWithoutExtension(arquivo).Substring(0, 4))
                .Distinct()
                .OrderByDescending(nome => nome) 
                .Where(nome => int.TryParse(nome, out _)) 
                .ToList();


            return listaAno;
        }
        public List<string> ListarAtas(string tipoDocumento, string codCondominio, string ano)
        {
            string caminho = $@"{_conDBF.CaminhoDoc}{codCondominio}{tipoDocumento}";

            if (!Directory.Exists(caminho))
            {
                return new List<string>();
            }
            string[] arquivos = Directory.GetFiles(caminho);

            List<string> Atas = arquivos.Where(arquivo => Path.GetFileNameWithoutExtension(arquivo).Substring(0,4) == ano)
            .Select(arquivo => Path.GetFileName(arquivo))
            .ToList();

             return Atas;
          
        }
            public List<string> ListarConvencao(string codCondominio) 
            {
                string caminho = $@"{_conDBF.CaminhoDoc}{codCondominio}\Convencao_e_Regulamentos";

                if (!Directory.Exists(caminho))
                {
                    return new List<string>();
                }

                string[] arquivoDoc = Directory.GetFiles(caminho);

                List<string> convencao = arquivoDoc.Select(arquivos => Path.GetFileName(arquivos)).ToList();

                return convencao;
            }

    }
}

