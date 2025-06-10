using APIsDeCondomino.model;
using System.Data.OleDb;

namespace APIsDeCondomino.service
{
    public class DBFReader
    {
        string _strconDCONCON;

        public DBFReader(StrCaminho strCon)
        {
            _strconDCONCON = strCon.CaminhoDBF;
        }

        public List<Clientes> DBFIdentificarClienteTelefone(string telefone)
        {
            string connStr = @$"String de Conexão aqui";
            var registros = new List<Clientes>(); 
            try
            {
                var connection = new OleDbConnection(connStr);

                string query = $"Sua query Aqui";
                // string query = $"select * from sample";
                using var cmd = new OleDbCommand(query, connection); 
                using var reader = cmd.ExecuteReader(); 

                while (reader.Read())
                {
                
                    var item = new Clientes
                    {
                        nome = reader["COLUNA"].ToString(),

                        telefone = reader["COLUNA"].ToString()

                    }; 
                    registros.Add(item);
                }
                
                return registros;
            }
            catch (Exception ex)
            {
                Logger.Info(ex.Message);
                throw;
            }
        }

        public List<Unidade> DBFListarUnidades(string telefone)
        {
            var Unidades = new List<Unidade>();
            string connStr = @$"String de Conexão aqui";

            try
            {
                using var connection = new OleDbConnection(connStr);
                connection.Open();
                
                string query = $"Sua query Aqui";
                using var cmd = new OleDbCommand(query, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())

                {

                    var item = new Unidade
                    {
                        codigoCon = reader["COLUNA"].ToString(),
                        codigoUnidade = reader["COLUNA"].ToString(),
                        blocoUnidade = reader["COLUNA"].ToString(),
                        nomeCon = reader["COLUNA"].ToString()
                    };
                    Unidades.Add(item);
                }
                return Unidades;
            }
            catch (Exception ex)
            {
                Logger.Info(ex.Message);
                throw;
            }
        }
        public List<string> DBFListarBoleto(string codCondominio, string unidade , string bloco)
        {
            List<string> numRec = new List<string>();
            string connStr = $@"String de Conexão aqui";

            try
            {
                using var connection = new OleDbConnection(connStr);
                connection.Open();
                
                string query = $@"Sua query Aqui";
                
                
                using var cmd = new OleDbCommand(query, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    numRec.Add(reader["COLUNA"].ToString());
                }
                return numRec;
            }
            catch (Exception ex)
            {
                Logger.Info(ex.Message);
                throw;
            }
        }
    }
}
