using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiattaformaNoleggioVeicoli.Business.Managers
{
    public class ClientiManager
    {
        private string ConnectionString { get; }
        public ClientiManager()     // Costruttore per richiamare la connection string messa su proprietà
        {
            ConnectionString = Properties.Settings.Default.DBSilvia;
            //ConnectionString = Properties.Settings.Default.DBAzure;
            //ConnectionString = Properties.Settings.Default.ARCAConnectionString;
        }
        public ClientiModel InsertCliente(ClientiModel cliente)     // Inserisce cliente su db
        {
            if (!IsClienteModelValido(cliente))
            {
                throw new DataException();
            }
            if (EsistenzaCodiceFiscale(cliente.CodiceFiscale).HasValue)
            {
                return null;
            }
            int? idInserito = null;
            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[Clienti] (");
            sb.AppendLine("[Cognome]");
            sb.AppendLine(",[Nome]");
            sb.AppendLine(",[DataNascita]");
            sb.AppendLine(",[CodiceFiscale]");
            sb.AppendLine(",[Patente]");
            sb.AppendLine(",[Telefono]");
            sb.AppendLine(",[Email]");
            sb.AppendLine(",[Indirizzo]");
            sb.AppendLine(",[NumeroCivico]");
            sb.AppendLine(",[Cap]");
            sb.AppendLine(",[Citta]");
            sb.AppendLine(",[Comune]");
            sb.AppendLine(",[Provincia]");
            sb.AppendLine(",[Nazione]");
            sb.AppendLine(",[Note]");
            sb.AppendLine(") VALUES (");
            sb.AppendLine("@Cognome");
            sb.AppendLine(",@Nome");
            sb.AppendLine(",@DataNascita");
            sb.AppendLine(",@CodiceFiscale");
            sb.AppendLine(",@Patente");
            sb.AppendLine(",@Telefono");
            sb.AppendLine(",@Email");
            sb.AppendLine(",@Indirizzo");
            sb.AppendLine(",@NumeroCivico");
            sb.AppendLine(",@Cap");
            sb.AppendLine(",@Citta");
            sb.AppendLine(",@Comune");
            sb.AppendLine(",@Provincia");
            sb.AppendLine(",@Nazione");
            sb.AppendLine(",@Note");
            sb.AppendLine(")");
            sb.AppendLine("SELECT SCOPE_IDENTITY()");

            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString(), sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                    sqlCommand.Parameters.AddWithValue("@Nome", cliente.Nome);
                    sqlCommand.Parameters.AddWithValue("@DataNascita", cliente.DataNascita);
                    sqlCommand.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                    sqlCommand.Parameters.AddWithValue("@Patente", cliente.Patente);
                    sqlCommand.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    sqlCommand.Parameters.AddWithValue("@Email", cliente.Email);
                    sqlCommand.Parameters.AddWithValue("@Indirizzo", cliente.Indirizzo);
                    sqlCommand.Parameters.AddWithValue("@NumeroCivico", cliente.NumeroCivico);
                    sqlCommand.Parameters.AddWithValue("@Cap", cliente.Cap);
                    sqlCommand.Parameters.AddWithValue("@Citta", cliente.Citta);
                    sqlCommand.Parameters.AddWithValue("@Comune", cliente.Comune);
                    sqlCommand.Parameters.AddWithValue("@Comune", cliente.Provincia);
                    sqlCommand.Parameters.AddWithValue("@Nazione", cliente.Nazione);
                    if (!string.IsNullOrEmpty(cliente.Note))
                    {
                        sqlCommand.Parameters.AddWithValue("@Note", cliente.Note);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@Note", DBNull.Value);
                    }                    
                    object value = sqlCommand.ExecuteScalar();
                    if (value != null && value != DBNull.Value)
                    {
                        idInserito = Convert.ToInt32(value);
                    }
                }
            }
            if (!idInserito.HasValue)
            {
                return null;
            }
            var clienteInserito = cliente;
            clienteInserito.Id = idInserito.Value;
            return clienteInserito;
        }
        public int? EsistenzaCodiceFiscale(string codiceFiscale)        // Controlla l'esistenza del codice fiscale
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT [Id]");
            sb.AppendLine("FROM [Clienti]");
            sb.AppendLine("WHERE [CodiceFiscale] = @Cf");
            var dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString(), sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Cf", codiceFiscale);
                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        sqlDataAdapter.SelectCommand.Connection = sqlConnection;
                        sqlDataAdapter.Fill(dataTable);
                    }
                }
            }
            if (dataTable.Rows.Count == 0)      // controlla che ci sia almeno una riga
            {
                return null;
            }
            var id = dataTable.Rows[0].Field<int>("Id");
            return id;
        }                       
        public bool ModificaCliente(ClientiModel cliente)      // Modifica dati Cliente sul db e utilizza la transaction per evitare che vengano modificati contemporaneamente più id per errore
        {
            if (!IsClienteModelValido(cliente))
            {
                throw new DataException();
            }
            var sb = new StringBuilder();
            sb.AppendLine("UPDATE[dbo].[Clienti]");
            sb.AppendLine("SET");
            sb.AppendLine("[Cognome] = @Cognome,");
            sb.AppendLine("[Nome] = @Nome,");
            sb.AppendLine("[DataNascita] = @DataNascita,");
            sb.AppendLine("[CodiceFiscale] = @CodiceFiscale,");
            sb.AppendLine("[Patente] = @Patente,");
            sb.AppendLine("[Telefono] = @Telefono,");
            sb.AppendLine("[Email] = @Email,");
            sb.AppendLine("[Indirizzo] = @Indirizzo,");
            sb.AppendLine("[NumeroCivico] = @NumeroCivico,");
            sb.AppendLine("[Cap] = @Cap,");
            sb.AppendLine("[Citta] = @Citta,");
            sb.AppendLine("[Comune] = @Comune,");
            sb.AppendLine("[Provincia] = @Provincia,");
            sb.AppendLine("[Nazione] = @Nazione,");
            sb.AppendLine("[Note] = @Note");
            sb.AppendLine("WHERE Id = @Id");
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                var modificaTransaction = sqlConnection.BeginTransaction();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString(), sqlConnection, modificaTransaction))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", cliente.Id);
                    sqlCommand.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                    sqlCommand.Parameters.AddWithValue("@Nome", cliente.Nome);
                    sqlCommand.Parameters.AddWithValue("@DataNascita", cliente.DataNascita);
                    sqlCommand.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                    sqlCommand.Parameters.AddWithValue("@Patente", cliente.Patente);
                    sqlCommand.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    sqlCommand.Parameters.AddWithValue("@Email", cliente.Email);
                    sqlCommand.Parameters.AddWithValue("@Indirizzo", cliente.Indirizzo);
                    sqlCommand.Parameters.AddWithValue("@NumeroCivico", cliente.NumeroCivico);
                    sqlCommand.Parameters.AddWithValue("@Cap", cliente.Cap);
                    sqlCommand.Parameters.AddWithValue("@Citta", cliente.Citta);
                    sqlCommand.Parameters.AddWithValue("@Comune", cliente.Comune);
                    sqlCommand.Parameters.AddWithValue("@Comune", cliente.Provincia);
                    sqlCommand.Parameters.AddWithValue("@Nazione", cliente.Nazione);
                    if (!string.IsNullOrEmpty(cliente.Note))
                    {
                        sqlCommand.Parameters.AddWithValue("@Note", cliente.Note);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@Note", DBNull.Value);
                    }

                    int nRowModificate = sqlCommand.ExecuteNonQuery();
                    if (nRowModificate != 1)
                    {
                        modificaTransaction.Rollback();
                        return false;
                    }
                    modificaTransaction.Commit();
                }
            }
            // messaggio successo
            return true;
        }
        private bool IsClienteModelValido(object cliente)       // Fa un controllo sull'oggetto cliente ed evita di spaccarsi in caso ClienteModel fosse null
        {
            if (cliente == null)
            {                
                return false;
            }
            var verificaCliente = (ClientiModel)cliente;            
            if (string.IsNullOrWhiteSpace(verificaCliente.Cognome))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Nome))
            {
                return false;
            }
            if (!verificaCliente.DataNascita.HasValue)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.CodiceFiscale))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Patente))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Telefono))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Email))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Indirizzo))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.NumeroCivico))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Cap))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Citta))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Comune))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Provincia))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Nazione))
            {
                return false;
            }            
            return true;
        }
        public ClientiModel GetCliente(int id)     // Restituisce i dettagli di un determinato cliente ricercato tramite id
        {
            var clientiModel = new ClientiModel();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("\t[Id]");
            sb.AppendLine("\t,[Cognome]");
            sb.AppendLine("\t,[Nome]");
            sb.AppendLine("\t,[DataNascita]");
            sb.AppendLine("\t,[CodiceFiscale]");
            sb.AppendLine("\t,[Patente]");
            sb.AppendLine("\t,[Telefono]");
            sb.AppendLine("\t,[Email]");
            sb.AppendLine("\t,[Indirizzo]");
            sb.AppendLine("\t,[NumeroCivico]");
            sb.AppendLine("\t,[Cap]");
            sb.AppendLine("\t,[Citta]");
            sb.AppendLine("\t,[Comune]");
            sb.AppendLine("\t,[Provincia]");
            sb.AppendLine("\t,[Nazione]");
            sb.AppendLine("\t,[Note]");
            sb.AppendLine("\tFROM [dbo].[Clienti]");
            sb.AppendLine("\tWHERE [Id] = @Id");

            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        sqlDataAdapter.SelectCommand.Connection = sqlConnection;
                        sqlDataAdapter.Fill(dataTable);
                    }
                }
            }
            if (dataTable.Rows.Count == 0)      // controlla che ci sia almeno una riga
            {
                return new ClientiModel();
            }
            DataRow row = dataTable.Rows[0];
            clientiModel.Id = row.Field<int>("Id");
            clientiModel.Cognome = row.Field<string>("Cognome");
            clientiModel.Nome = row.Field<string>("Nome");
            clientiModel.DataNascita = row.Field<DateTime?>("DataNascita");
            clientiModel.CodiceFiscale = row.Field<string>("CodiceFiscale");
            clientiModel.Patente = row.Field<string>("Patente");
            clientiModel.Telefono = row.Field<string>("Telefono");
            clientiModel.Email = row.Field<string>("Email");
            clientiModel.Indirizzo = row.Field<string>("Indirizzo");
            clientiModel.NumeroCivico = row.Field<string>("NumeroCivico");
            clientiModel.Cap = row.Field<string>("Cap");
            clientiModel.Citta = row.Field<string>("Citta");
            clientiModel.Comune = row.Field<string>("Comune");
            clientiModel.Provincia = row.Field<string>("Provincia");
            clientiModel.Nazione = row.Field<string>("Nazione");
            clientiModel.Note = row.Field<string>("Note");
            return clientiModel;
        }
        public class RicercaClientiModel
        {           
            public string Cognome { get; set; }
            public string Nome { get; set; }
            public string CodiceFiscale { get; set; }           
        }
        public List<ClientiTrovatiModelView> RicercaClienti(RicercaClientiModel ricercaClientiModel)        // Ricerca clienti tramite i campi Cognome, Nome e CodiceFiscale
        {
            var clientiTrovatiList = new List<ClientiTrovatiModelView>();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("\t[Id]");
            sb.AppendLine("\t,[Cognome]");
            sb.AppendLine("\t,[Nome]");
            sb.AppendLine("\t,[CodiceFiscale]");
            sb.AppendLine("FROM [dbo].[Clienti]");
            sb.AppendLine("WHERE 1=1");            
            if (!string.IsNullOrWhiteSpace(ricercaClientiModel.Cognome))
            {
                sb.AppendLine("And Cognome like '%'+@Cognome+'%'");
            }
            if (!string.IsNullOrWhiteSpace(ricercaClientiModel.Nome))
            {
                sb.AppendLine("And Nome like '%'+@Nome+'%'");
            }
            if (!string.IsNullOrWhiteSpace(ricercaClientiModel.CodiceFiscale))
            {
                sb.AppendLine("And CodiceFiscale like '%'+@CodiceFiscale+'%'");
            }
            var dataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {                    
                    if (!string.IsNullOrEmpty(ricercaClientiModel.Cognome))
                    {
                        sqlCommand.Parameters.AddWithValue("@Cognome", ricercaClientiModel.Cognome);
                    }
                    if (!string.IsNullOrEmpty(ricercaClientiModel.Nome))
                    {
                        sqlCommand.Parameters.AddWithValue("@Nome", ricercaClientiModel.Nome);
                    }
                    if (!string.IsNullOrEmpty(ricercaClientiModel.CodiceFiscale))
                    {
                        sqlCommand.Parameters.AddWithValue("@CodiceFiscale", ricercaClientiModel.CodiceFiscale);
                    }

                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        sqlDataAdapter.SelectCommand.Connection = sqlConnection;
                        sqlDataAdapter.Fill(dataSet);
                    }
                }
            }

            if (dataSet.Tables.Count < 0)       // controlla che esista almeno una tabella net dataset
            {
                return new List<ClientiTrovatiModelView>();
            }

            var dataTable = dataSet.Tables[0];

            if (dataTable == null || dataTable.Rows.Count <= 0)     // controlla che il dataTable sia diverso da null e contenga almeno una riga
            {
                return new List<ClientiTrovatiModelView>();
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var clientiTrovati = new ClientiTrovatiModelView();
                clientiTrovati.Id = dataRow.Field<int>("Id");
                clientiTrovati.Cognome = dataRow.Field<string>("Cognome");
                clientiTrovati.Nome = dataRow.Field<string>("Nome");
                clientiTrovati.CodiceFiscale = dataRow.Field<string>("CodiceFiscale");
                clientiTrovatiList.Add(clientiTrovati);
            }
            return clientiTrovatiList;
        }
    }
}

