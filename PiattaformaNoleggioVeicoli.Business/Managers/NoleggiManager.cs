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
    public class NoleggiManager
    {
        private string ConnectionString { get; }
        public NoleggiManager()     // Costruttore per richiamare la connection string messa su proprietà
        {
            ConnectionString = Properties.Settings.Default.DBSilvia;
            //ConnectionString = Properties.Settings.Default.DBAzure;
            //ConnectionString = Properties.Settings.Default.ARCAConnectionString;
        }

        public bool InserisciNoleggio(NoleggiModel noleggiModel)     // Inserisce noleggio su db
        {
            if (!IsNoleggioModelValido(noleggiModel))
            {
                throw new DataException();
            }
            bool isInserito = false;
            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[Noleggi] (");
            sb.AppendLine("[IdVeicolo]");
            sb.AppendLine(",[IdCliente]");
            sb.AppendLine(",[DataInizio]");
            sb.AppendLine(",[DataFine]");
            sb.AppendLine(",[IsInCorso]");
            sb.AppendLine(") VALUES (");
            sb.AppendLine("@IdVeicolo");
            sb.AppendLine(",@IdCliente");
            sb.AppendLine(",@DataInizio");
            sb.AppendLine(",@DataFine");
            sb.AppendLine(",@IsInCorso");
            sb.AppendLine(")");
            var sqlCambiaDisponibilitaVeicolo = new StringBuilder();
            sqlCambiaDisponibilitaVeicolo.AppendLine("UPDATE [Veicoli] SET");
            sqlCambiaDisponibilitaVeicolo.AppendLine("[IsDisponibile] = @Disponibilita");
            sqlCambiaDisponibilitaVeicolo.AppendLine("WHERE [Id] = @IdVeicolo");

            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                var transaction = sqlConnection.BeginTransaction();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString(), sqlConnection, transaction))
                {
                    try
                    {
                        sqlCommand.Parameters.AddWithValue("@IdVeicolo", noleggiModel.IdVeicolo);
                        sqlCommand.Parameters.AddWithValue("@IdCliente", noleggiModel.IdCliente);
                        sqlCommand.Parameters.AddWithValue("@DataInizio", DateTime.Now);
                        sqlCommand.Parameters.AddWithValue("@DataFine", DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@IsInCorso", true);
                        
                        var numRigheInserite = sqlCommand.ExecuteNonQuery();
                        if (numRigheInserite <= 0)
                        {
                            transaction.Rollback();                            
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    try
                    {
                        sqlCommand.CommandText = sqlCambiaDisponibilitaVeicolo.ToString();                        
                        sqlCommand.Parameters.AddWithValue("@Disponibilita", false);
                        var righeAggiornate = sqlCommand.ExecuteNonQuery();
                        if (righeAggiornate!=1)         // evita di aggiornare più righe
                        {
                            transaction.Rollback();
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();

                        throw;
                    }
                    isInserito = true;
                    transaction.Commit();
                }
            }
            // messaggio successo
            return isInserito;
        }

        private bool IsNoleggioModelValido(object noleggio)       // Fa un controllo sull'oggetto noleggio ed evita di spaccarsi in caso ClienteModel fosse null
        {
            if (noleggio == null)
            {
                // messaggio errore
                return false;
            }
            var verificaNoleggio = (NoleggiModelView)noleggio;

            if (!verificaNoleggio.IdVeicolo.HasValue)
            {
                // messaggio errore
                return false;
            }
            if (!verificaNoleggio.IdCliente.HasValue)
            {
                // messaggio errore
                return false;
            }            
            return true;
        }

        public bool TerminaNoleggio(NoleggiModel noleggiModel)     // Inserisce noleggio su db
        {
            if (!IsNoleggioModelValido(noleggiModel) && noleggiModel.Id <= 0)
            {
                throw new DataException();
            }
            bool isInserito = false;
            var sb = new StringBuilder();
            sb.AppendLine("UPDATE [dbo].[Noleggi] SET");            
            sb.AppendLine("[DataFine] = @DataFine");
            sb.AppendLine(",[IsInCorso] = @IsInCorso");
            sb.AppendLine("WHERE [Id] = @IdNoleggio");

            var sqlCambiaDisponibilitaVeicolo = new StringBuilder();
            sqlCambiaDisponibilitaVeicolo.AppendLine("UPDATE [Veicoli] SET");
            sqlCambiaDisponibilitaVeicolo.AppendLine("[IsDisponibile] = @Disponibilita");
            sqlCambiaDisponibilitaVeicolo.AppendLine("WHERE [Id] = @IdVeicolo");

            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                var transaction = sqlConnection.BeginTransaction();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString(), sqlConnection, transaction))
                {
                    try
                    {
                        sqlCommand.Parameters.AddWithValue("@DataFine", DateTime.Now);                        
                        sqlCommand.Parameters.AddWithValue("@IsInCorso", false);
                        sqlCommand.Parameters.AddWithValue("@IdNoleggio", noleggiModel.Id);

                        var numRigheInserite = sqlCommand.ExecuteNonQuery();
                        if (numRigheInserite <= 0)
                        {
                            transaction.Rollback();
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    try
                    {
                        sqlCommand.CommandText = sqlCambiaDisponibilitaVeicolo.ToString();
                        sqlCommand.Parameters.AddWithValue("@IdVeicolo", noleggiModel.IdVeicolo);
                        sqlCommand.Parameters.AddWithValue("@Disponibilita", true);
                        var righeAggiornate = sqlCommand.ExecuteNonQuery();
                        if (righeAggiornate != 1)         // evita di aggiornare più righe
                        {
                            transaction.Rollback();
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();

                        throw;
                    }
                    isInserito = true;
                    transaction.Commit();
                }
            }
            //messaggio successo
            return isInserito;
        }

        public NoleggiModel RecuperaNoleggio(VeicoliModel veicolo)      // Recupera l'ultimo noleggio di un determinato veicolo
        {
            var noleggio = new NoleggiModel();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT TOP(1)");
            sb.AppendLine("[Id]");
            sb.AppendLine(",[IdVeicolo]");
            sb.AppendLine(",[IdCliente]");
            sb.AppendLine(",[DataInizio]");
            sb.AppendLine(",[IsInCorso]");
            sb.AppendLine("FROM [Noleggi]");
            sb.AppendLine("WHERE [IdVeicolo] = @IdVeicolo");
            sb.AppendLine("ORDER BY [Id] DESC");

            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {
                    sqlCommand.Parameters.AddWithValue("@IdVeicolo", veicolo.Id);                    

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
                return new NoleggiModel();
            }
            DataRow row = dataTable.Rows[0];
            noleggio.Id = row.Field<int>("Id");
            noleggio.IdVeicolo = row.Field<int>("IdVeicolo");
            noleggio.IdCliente = row.Field<int>("IdClinte");
            noleggio.DataInizio = row.Field<DateTime>("DataInizio");
            noleggio.IsInCorso = row.Field<bool>("IsInCorso");

            return noleggio;
        }

        public NoleggiTrovatiModelView GetNoleggio(int id)     // Restituisce i dettagli di un determinato noleggio ricercato tramite id
        {
            var dettaglioNoleggio = new NoleggiTrovatiModelView();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("\t[Noleggi].[Id]");
            sb.AppendLine("\t,[MarcheVeicoli].[Descrizione] as Marca");
            sb.AppendLine("\t,[Modello]");
            sb.AppendLine("\t,[Targa]");
            sb.AppendLine("\t,[IsInCorso]");
            sb.AppendLine("\t,[DataInizio]");
            sb.AppendLine("\t,[DataFine]");
            sb.AppendLine("\t,[Clienti].[Cognome]");
            sb.AppendLine("\t,[Clienti].[Nome]");
            sb.AppendLine("\t,[Clienti].[CodiceFiscale]");
            sb.AppendLine("\tFROM [dbo].[Noleggi]");
            sb.AppendLine("\tINNER JOIN [dbo].[Veicoli]");
            sb.AppendLine("\tON [dbo].[Noleggi].[IdVeicolo] = [dbo].[Veicoli].[Id]");
            sb.AppendLine("\tLEFT JOIN [dbo].[Clienti]");
            sb.AppendLine("\tON [dbo].[Noleggi].[IdCliente] = [dbo].[Clienti].[Id]");
            sb.AppendLine("\tINNER JOIN [dbo].[MarcheVeicoli]");
            sb.AppendLine("\tON [dbo].[Veicoli].[IdMarca] = [dbo].[MarcheVeicoli].[Id]");
            sb.AppendLine("\tWHERE [Noleggi].[Id] = @Id");

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
                return new NoleggiTrovatiModelView();
            }
            DataRow row = dataTable.Rows[0];
            dettaglioNoleggio.Id = row.Field<int>("Id");
            dettaglioNoleggio.Marca = row.Field<string>("Marca");
            dettaglioNoleggio.Modello = row.Field<string>("Modello");
            dettaglioNoleggio.Targa = row.Field<string>("Targa");
            bool inCorso = row.Field<bool>("IsInCorso");
            dettaglioNoleggio.IsInCorso = inCorso ? "si" : "no";       // Serve per vedere "si" o "no" al posto di true o false
            dettaglioNoleggio.DataInizio = row.Field<DateTime?>("DataInizio");
            dettaglioNoleggio.DataFine = row.Field<DateTime?>("DataFine");
            dettaglioNoleggio.Cognome = row.Field<string>("Cognome");
            dettaglioNoleggio.Nome = row.Field<string>("Nome");
            dettaglioNoleggio.CodiceFiscale = row.Field<string>("CodiceFiscale");
            return dettaglioNoleggio;
        }
        public NoleggiModelView GetDatiNoleggio(NoleggiModel noleggio)      // Restituisce i dati di un noleggio comprendendo anche i dati del veicolo e del cliente
        {
            var noleggiModelView = new NoleggiModelView();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("[Noleggi].[Id]");
            sb.AppendLine(",[Noleggi].[IdVeicolo]");
            sb.AppendLine(",[MarcheVeicoli].[Descrizione] as Marca");
            sb.AppendLine(",[Veicoli].[Modello]");
            sb.AppendLine(",[Veicoli].[Targa]");
            sb.AppendLine(",[Veicoli].[IsDisponibile]");
            sb.AppendLine(",[Noleggi].[DataInizio]");
            sb.AppendLine(",[Noleggi].[DataFine]");
            sb.AppendLine(",[Noleggi].[IsInCorso]");
            sb.AppendLine(",[Noleggi].[IdCliente]");
            sb.AppendLine(",[Clienti].[Cognome]");
            sb.AppendLine(",[Clienti].[Nome]"); 
            sb.AppendLine(",[Clienti].[CodiceFiscale]");
            sb.AppendLine("FROM [Noleggi]");
            sb.AppendLine("LEFT JOIN [Veicoli]");
            sb.AppendLine("ON [Veicoli].[Id] = [Noleggi].[IdVeicolo]");
            sb.AppendLine("LEFT JOIN [Clienti]");
            sb.AppendLine("ON [Clienti].[Id] = [Noleggi].[IdCliente]");
            sb.AppendLine("LEFT JOIN [MarcheVeicoli]");
            sb.AppendLine("ON [MarcheVeicoli].[Id] = [Veicoli].[IdMarca]");
            sb.AppendLine("WHERE [Noleggi].[Id] = @Id");

            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", noleggio.Id);

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
                return new NoleggiModelView();
            }
            DataRow row = dataTable.Rows[0];
            noleggiModelView.Id = row.Field<int>("Id");
            noleggiModelView.IdVeicolo = row.Field<int>("IdVeicolo");
            noleggiModelView.Marca = row.Field<string>("Marca");
            noleggiModelView.Modello = row.Field<string>("Modello");
            noleggiModelView.Targa = row.Field<string>("Targa");
            noleggiModelView.IsDisponibile = row.Field<bool>("IsDisponibile");
            noleggiModelView.DataInizio = row.Field<DateTime>("DataInizio");
            noleggiModelView.DataFine = row.Field<DateTime>("DataFine");
            noleggiModelView.IsInCorso = row.Field<bool>("IsInCorso");
            noleggiModelView.IdVeicolo = row.Field<int>("IdCliente");
            noleggiModelView.Cognome = row.Field<string>("Cognome");
            noleggiModelView.Nome = row.Field<string>("Nome");
            noleggiModelView.CodiceFiscale = row.Field<string>("CodiceFiscale");
            return noleggiModelView; 
        }

        public class RicercaNoleggiModel
        {
            public int Id { get; set; }
            public int? IdMarca { get; set; }
            public int? IdVeicolo { get; set; }
            public string Marca { get; set; }
            public string Modello { get; set; }
            public string Targa { get; set; }
            public bool IsDisponibile { get; set; }
            public DateTime? DataInizio { get; set; }
            public DateTime? DataFine { get; set; }
            public bool? IsInCorso { get; set; }
            public int? IdCliente { get; set; }
            public string Cognome { get; set; }
            public string Nome { get; set; }
            public string CodiceFiscale { get; set; }
        }
        public List<NoleggiTrovatiModelView> RicercaNoleggi(RicercaNoleggiModel ricerca)        // Ricerca noleggi tramite i campi Marca, Modello, Targa, DataInizio, DataFine, IsInCorso, Cognome, Nome e CodiceFiscale
        {
            var noleggiTrovatiList = new List<NoleggiTrovatiModelView>();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("\t[Noleggi].[Id]");
            sb.AppendLine("\t,[MarcheVeicoli].[Descrizione] as Marca");
            sb.AppendLine("\t,[Modello]");
            sb.AppendLine("\t,[Targa]");
            sb.AppendLine("\t,[IsInCorso]");
            sb.AppendLine("\t,[DataInizio]");
            sb.AppendLine("\t,[DataFine]");
            sb.AppendLine("\t,[Cognome]");
            sb.AppendLine("\t,[Nome]");
            sb.AppendLine("\t,[CodiceFiscale]");
            sb.AppendLine("FROM [dbo].[Noleggi]");
            sb.AppendLine("\tINNER JOIN [dbo].[Veicoli]");
            sb.AppendLine("\tON [dbo].[Veicoli].[Id] = [dbo].[Noleggi].[IdVeicolo]");
            sb.AppendLine("\tINNER JOIN [dbo].[MarcheVeicoli]");
            sb.AppendLine("\tON [dbo].[Veicoli].[IdMarca] = [dbo].[MarcheVeicoli].[Id]");
            sb.AppendLine("\tINNER JOIN [dbo].[Clienti]");
            sb.AppendLine("\tON [dbo].[Noleggi].[IdCliente] = [dbo].[Clienti].[Id]");
            sb.AppendLine("WHERE 1=1");       

            if (ricerca.IdMarca.HasValue)
            {
                sb.AppendLine("And IdMarca = @IdMarca");
            }
            if (!string.IsNullOrWhiteSpace(ricerca.Modello))
            {
                sb.AppendLine("And Modello like '%'+@Modello+'%'");
            }
            if (!string.IsNullOrWhiteSpace(ricerca.Targa))
            {
                sb.AppendLine("And Targa like '%'+@Targa+'%'");
            }
            if (ricerca.IsInCorso.HasValue)
            {
                sb.AppendLine("And IsInCorso = @IsInCorso");
            }
            if (ricerca.DataInizio.HasValue)
            {
                sb.AppendLine("And DataInizio >= @DataInizio");
            }
            if (ricerca.DataFine.HasValue)
            {
                sb.AppendLine("And DataFine <= @DataFine");
            }
            if (!string.IsNullOrWhiteSpace(ricerca.Cognome))
            {
                sb.AppendLine("And Cognome like '%'+@Cognome+'%'");
            }
            if (!string.IsNullOrWhiteSpace(ricerca.Nome))
            {
                sb.AppendLine("And Nome like '%'+@Nome+'%'");
            }
            if (!string.IsNullOrWhiteSpace(ricerca.CodiceFiscale))
            {
                sb.AppendLine("And CodiceFiscale = @CodiceFiscale");
            }

            var dataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {
                    if (ricerca.IdMarca.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@IdMarca", ricerca.IdMarca);
                    }
                    if (!string.IsNullOrEmpty(ricerca.Modello))
                    {
                        sqlCommand.Parameters.AddWithValue("@Modello", ricerca.Modello);
                    }
                    if (!string.IsNullOrEmpty(ricerca.Targa))
                    {
                        sqlCommand.Parameters.AddWithValue("@Targa", ricerca.Targa);
                    }
                    if (ricerca.IsInCorso.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@IsDisponibile", ricerca.IsDisponibile);
                    }
                    if (ricerca.DataInizio.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@DataInizio", ricerca.DataInizio);
                    }
                    if (ricerca.DataFine.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@DataFine", ricerca.DataFine);
                    }
                    if (!string.IsNullOrEmpty(ricerca.Cognome))
                    {
                        sqlCommand.Parameters.AddWithValue("@Cognome", ricerca.Cognome);
                    }
                    if (!string.IsNullOrEmpty(ricerca.Nome))
                    {
                        sqlCommand.Parameters.AddWithValue("@Nome", ricerca.Nome);
                    }
                    if (!string.IsNullOrEmpty(ricerca.CodiceFiscale))
                    {
                        sqlCommand.Parameters.AddWithValue("@CodiceFiscale", ricerca.CodiceFiscale);
                    }
                    if (ricerca.IsInCorso.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@IsInCorso", ricerca.IsInCorso);

                    }

                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        sqlDataAdapter.SelectCommand.Connection = sqlConnection;
                        sqlDataAdapter.Fill(dataSet);
                    }
                }
            }

            if (dataSet.Tables.Count < 0)       // controlla che esista almeno una tabella nel dataset
            {
                return new List<NoleggiTrovatiModelView>();
            }

            var dataTable = dataSet.Tables[0];

            if (dataTable == null || dataTable.Rows.Count <= 0)     // controlla che il dataTable sia diverso da null e contenga almeno una riga
            {
                return new List<NoleggiTrovatiModelView>();
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var noleggiTrovatiModelView = new NoleggiTrovatiModelView();
                noleggiTrovatiModelView.Id = dataRow.Field<int>("Id");
                noleggiTrovatiModelView.Marca = dataRow.Field<string>("Marca");
                noleggiTrovatiModelView.Modello = dataRow.Field<string>("Modello");
                noleggiTrovatiModelView.Targa = dataRow.Field<string>("Targa");
                bool inCorso = dataRow.Field<bool>("IsInCorso");
                noleggiTrovatiModelView.IsInCorso = inCorso ? "si" : "no";       // Serve per vedere "si" o "no" al posto di true o false
                //noleggiTrovatiModelView.IsInCorso = dataRow.Field<bool?>("IsInCorso");
                noleggiTrovatiModelView.DataInizio = dataRow.Field<DateTime?>("DataInizio");
                noleggiTrovatiModelView.DataFine = dataRow.Field<DateTime?>("DataFine");
                noleggiTrovatiModelView.Cognome = dataRow.Field<string>("Cognome");
                noleggiTrovatiModelView.Nome = dataRow.Field<string>("Nome");
                noleggiTrovatiModelView.CodiceFiscale = dataRow.Field<string>("CodiceFiscale");
                noleggiTrovatiList.Add(noleggiTrovatiModelView);
            }
            return noleggiTrovatiList;
        }
    }
}
