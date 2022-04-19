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
        }
        public bool InsertCliente(Models.ClientiModel clientiModel)     // Inserisce cliente su db
        {
            if (!IsClienteModelValido(clientiModel))
            {
                throw new DataException();
            }
            bool isInserito = false;
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
            sb.AppendLine(",[Nazione]");
            sb.AppendLine(",[Note]");
            sb.AppendLine(") VALUES (");
            sb.AppendLine(",@Cognome");
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
            sb.AppendLine(",@Nazione");
            sb.AppendLine(",@Note");
            sb.AppendLine(")");

            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString(), sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Cognome", clientiModel.Cognome);
                    sqlCommand.Parameters.AddWithValue("@Nome", clientiModel.Nome);
                    sqlCommand.Parameters.AddWithValue("@DataNascita", clientiModel.DataNascita);
                    sqlCommand.Parameters.AddWithValue("@CodiceFiscale", clientiModel.CodiceFiscale);
                    sqlCommand.Parameters.AddWithValue("@Patente", clientiModel.Patente);
                    sqlCommand.Parameters.AddWithValue("@Telefono", clientiModel.Telefono);
                    sqlCommand.Parameters.AddWithValue("@Email", clientiModel.Email);
                    sqlCommand.Parameters.AddWithValue("@Indirizzo", clientiModel.Indirizzo);
                    sqlCommand.Parameters.AddWithValue("@NumeroCivico", clientiModel.NumeroCivico);
                    sqlCommand.Parameters.AddWithValue("@Cap", clientiModel.Cap);
                    sqlCommand.Parameters.AddWithValue("@Citta", clientiModel.Citta);
                    sqlCommand.Parameters.AddWithValue("@Comune", clientiModel.Comune);
                    sqlCommand.Parameters.AddWithValue("@Nazione", clientiModel.Nazione);
                    if (!string.IsNullOrEmpty(clientiModel.Note))
                    {
                        sqlCommand.Parameters.AddWithValue("@Note", clientiModel.Note);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@Note", DBNull.Value);
                    }                    
                    var numRigheInserite = sqlCommand.ExecuteNonQuery();
                    if (numRigheInserite >= 1)
                    {
                        isInserito = true;
                    }
                }
            }
            return isInserito;
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
            if (string.IsNullOrWhiteSpace(verificaCliente.Nazione))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaCliente.Note))
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
            clientiModel.Nazione = row.Field<string>("Nazione");
            clientiModel.Note = row.Field<string>("Note");
            return clientiModel;
        }
    }
}
