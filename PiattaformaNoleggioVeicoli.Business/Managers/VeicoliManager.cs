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
    public class VeicoliManager
    {
        private string ConnectionString { get; }
        public VeicoliManager()     // Costruttore per richiamare la connection string messa su proprietà
        {
            ConnectionString = Properties.Settings.Default.DBSilvia;
        }
        
        public bool InsertVeicolo(VeicoliModel veicoloModel)     // Inserisce veicolo su db
        {
            if (!IsVeicoloModelValido(veicoloModel))
            {
                throw new DataException();
            }
            bool isInserito = false;
            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[Veicoli] (");
            sb.AppendLine("[IdMarca]");
            sb.AppendLine(",[Modello]");
            sb.AppendLine(",[Targa]");
            sb.AppendLine(",[DataImmatricolazione]");
            sb.AppendLine(",[IdTipoAlimentazione]");
            sb.AppendLine(",[Note]");
            sb.AppendLine(",[IsDisponibile]");
            sb.AppendLine(",[IdTipoStato]");
            sb.AppendLine(") VALUES (");
            sb.AppendLine("@IdMarca");
            sb.AppendLine(",@Modello");
            sb.AppendLine(",@Targa");
            sb.AppendLine(",@DataImmatricolazione");
            sb.AppendLine(",@IdTipoAlimentazione");
            sb.AppendLine(",@Note");
            sb.AppendLine(",@IsDisponibile");
            sb.AppendLine(",@IdTipoStato");
            sb.AppendLine(")");

            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString(), sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@IdMarca", veicoloModel.IdMarca);
                    sqlCommand.Parameters.AddWithValue("@Modello", veicoloModel.Modello);
                    sqlCommand.Parameters.AddWithValue("@Targa", veicoloModel.Targa);
                    sqlCommand.Parameters.AddWithValue("@DataImmatricolazione", veicoloModel.DataImmatricolazione);
                    sqlCommand.Parameters.AddWithValue("@IdTipoAlimentazione", veicoloModel.IdTipoAlimentazione);
                    if (!string.IsNullOrEmpty(veicoloModel.Note))
                    {
                        sqlCommand.Parameters.AddWithValue("@Note", veicoloModel.Note);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@Note", DBNull.Value);
                    }
                    sqlCommand.Parameters.AddWithValue("@IsDisponibile", veicoloModel.IsDisponibile);
                    sqlCommand.Parameters.AddWithValue("@IdTipoStato", 1);
                    var numRigheInserite = sqlCommand.ExecuteNonQuery();
                    if (numRigheInserite >= 1)
                    {
                        isInserito = true;
                    }
                }
            }
            return isInserito;
        }        
        
        public bool ModificaVeicolo(VeicoliModel veicolo)      // Modifica dati Veicolo sul db e utilizza la transaction per evitare che vengano modificati contemporaneamente più id per errore
        {
            if (!IsVeicoloModelValido(veicolo))
            {
                throw new DataException();
            }
            var sb = new StringBuilder();
            sb.AppendLine("UPDATE[dbo].[Veicoli]");
            sb.AppendLine("SET");
            sb.AppendLine("[IdMarca] = @IdMarca,");
            sb.AppendLine("[Modello] = @Modello,");
            sb.AppendLine("[Targa] = @Targa,");
            sb.AppendLine("[DataImmatricolazione] = @DataImmatricolazione,");
            sb.AppendLine("[IdTipoAlimentazione] = @IdTipoAlimentazione,");
            sb.AppendLine("[Note] = @Note");
            sb.AppendLine("WHERE Id = @Id");
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                var modificaTransaction = sqlConnection.BeginTransaction();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString(), sqlConnection, modificaTransaction))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", veicolo.Id);
                    sqlCommand.Parameters.AddWithValue("@IdMarca", veicolo.IdMarca);
                    sqlCommand.Parameters.AddWithValue("@Modello", veicolo.Modello);
                    sqlCommand.Parameters.AddWithValue("@Targa", veicolo.Targa);
                    sqlCommand.Parameters.AddWithValue("@DataImmatricolazione", veicolo.DataImmatricolazione);
                    sqlCommand.Parameters.AddWithValue("@IdTipoAlimentazione", veicolo.IdTipoAlimentazione);

                    if (!string.IsNullOrEmpty(veicolo.Note))
                    {
                        sqlCommand.Parameters.AddWithValue("@Note", veicolo.Note);
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
        
        public bool EliminaVeicolo(VeicoliModel veicolo)        // Invece di eliminare fisicamente il veicolo dal db cambia lo stato da attivo a non attivo così al cliente rimane uno storico dei veicoli che ha posseduto
        {
            if (!IsVeicoloModelValido(veicolo))
            {
                throw new DataException();
            }
            var sb = new StringBuilder();
            sb.AppendLine("UPDATE [dbo].[Veicoli]");
            sb.AppendLine("SET ");
            sb.AppendLine("\t [IdTipoStato] = @IdTipoStato");
            sb.AppendLine("\t WHERE");
            sb.AppendLine("\t Id=@Id");

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                var disattivaVeicoloTransaction = sqlConnection.BeginTransaction();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString(), sqlConnection, disattivaVeicoloTransaction))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", veicolo.Id);
                    sqlCommand.Parameters.AddWithValue("@IdTipoStato", 2);
                    int nRowModificate = sqlCommand.ExecuteNonQuery();
                    if (nRowModificate != 1)
                    {
                        disattivaVeicoloTransaction.Rollback();
                        return false;
                    }
                    disattivaVeicoloTransaction.Commit();
                    return true;
                }
            }
        }

        private bool IsVeicoloModelValido(object veicolo)       // Fa un controllo sull'oggetto veicolo ed evita di spaccarsi in caso VeicoloModel fosse null
        {
            if (veicolo == null)
            {
                return false;
            }
            var verificaVeicolo = (VeicoliModel)veicolo;
            if (!verificaVeicolo.IdMarca.HasValue)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaVeicolo.Modello))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(verificaVeicolo.Targa))
            {
                return false;
            }
            if (!verificaVeicolo.DataImmatricolazione.HasValue)
            {
                return false;
            }
            if (!verificaVeicolo.IdTipoAlimentazione.HasValue)
            {
                return false;
            }
            return true;
        }
        
        public List<MarcheVeicoliModel> GetMarcheVeicoliList()      // Restituisce una lista di marche dei veicoli
        {
            var MarcheVeicoliList = new List<MarcheVeicoliModel>();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("\t[Id]");
            sb.AppendLine("\t,[Descrizione]");
            sb.AppendLine("FROM [dbo].[MarcheVeicoli]");

            var dataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {
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
                return new List<MarcheVeicoliModel>();
            }

            var dataTable = dataSet.Tables[0];

            if (dataTable == null || dataTable.Rows.Count <= 0)     // controlla che il dataTable sia diverso da null e contenga almeno una riga
            {
                return new List<MarcheVeicoliModel>();
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var MarcheVeicoli = new MarcheVeicoliModel();
                MarcheVeicoli.Id = dataRow.Field<int>("Id");
                MarcheVeicoli.Descrizione = dataRow.Field<string>("Descrizione");
                MarcheVeicoliList.Add(MarcheVeicoli);
            }
            return MarcheVeicoliList;
        }

        public List<TipoAlimentazioneModel> GetTipoAlimentazioneList()      // Restituisce una lista di tipi di alimentazione
        {
            var TipoAlimentazioneList = new List<TipoAlimentazioneModel>();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("\t[Id]");
            sb.AppendLine("\t,[Descrizione]");
            sb.AppendLine("FROM [dbo].[TipoAlimentazione]");

            var dataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {
                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        sqlDataAdapter.SelectCommand.Connection = sqlConnection;
                        sqlDataAdapter.Fill(dataSet);
                    }
                }
            }
            if (dataSet.Tables.Count < 0)         // controlla che esista almeno una tabella net dataset
            {
                return new List<TipoAlimentazioneModel>();
            }
            var dataTable = dataSet.Tables[0];
            if (dataTable == null || dataTable.Rows.Count <= 0)     // controlla che il dataTable sia diverso da null e contenga almeno una riga
            {
                return new List<TipoAlimentazioneModel>();
            }
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var TipoAlimentazione = new TipoAlimentazioneModel();
                TipoAlimentazione.Id = dataRow.Field<int>("Id");
                TipoAlimentazione.Descrizione = dataRow.Field<string>("Descrizione");
                TipoAlimentazioneList.Add(TipoAlimentazione);
            }
            return TipoAlimentazioneList;
        }

        public DettaglioVeicoloModelView GetVeicolo(int id)     // Restituisce i dettagli di un determinato veicolo ricercato tramite id
        {
            var dettaglioVeicoloModelView = new DettaglioVeicoloModelView();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("\t[Veicoli].[Id]");
            sb.AppendLine("\t,[IdMarca]");
            sb.AppendLine("\t,[MarcheVeicoli].[Descrizione] as Marca");
            sb.AppendLine("\t,[Modello]");
            sb.AppendLine("\t,[Targa]");
            sb.AppendLine("\t,[DataImmatricolazione]");
            sb.AppendLine("\t,[IdTipoAlimentazione]");
            sb.AppendLine("\t,[Veicoli].[Note]");
            sb.AppendLine("\t,[IsDisponibile]");
            sb.AppendLine("\t,[Clienti].[Cognome]");
            sb.AppendLine("\t,[Clienti].[Nome]");
            sb.AppendLine("\t,[Clienti].[CodiceFiscale]");
            sb.AppendLine("\tFROM [dbo].[Veicoli]");
            sb.AppendLine("\tLEFT JOIN [dbo].[Noleggi]");
            sb.AppendLine("\tON [dbo].[Noleggi].[IdVeicolo] = [dbo].[Veicoli].[Id]");
            sb.AppendLine("\tLEFT JOIN [dbo].[Clienti]");
            sb.AppendLine("\tON [dbo].[Noleggi].[IdCliente] = [dbo].[Clienti].[Id]");
            sb.AppendLine("\tINNER JOIN [dbo].[MarcheVeicoli]");
            sb.AppendLine("\tON [dbo].[Veicoli].[IdMarca] = [dbo].[MarcheVeicoli].[Id]");
            sb.AppendLine("\tWHERE [Veicoli].[Id] = @Id");
            sb.AppendLine("\tAND [Veicoli].[IdTipoStato] = @Attivo");

            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    sqlCommand.Parameters.AddWithValue("@Attivo", 1);

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
                return new DettaglioVeicoloModelView();
            }
            DataRow row = dataTable.Rows[0];
            dettaglioVeicoloModelView.Id = row.Field<int>("Id");
            dettaglioVeicoloModelView.IdMarca = row.Field<int>("IdMarca");
            dettaglioVeicoloModelView.Marca = row.Field<string>("Marca");
            dettaglioVeicoloModelView.Modello = row.Field<string>("Modello");
            dettaglioVeicoloModelView.Targa = row.Field<string>("Targa");
            dettaglioVeicoloModelView.DataImmatricolazione = row.Field<DateTime>("DataImmatricolazione");
            dettaglioVeicoloModelView.IdTipoAlimentazione = row.Field<int>("IdTipoAlimentazione");
            dettaglioVeicoloModelView.Note = row.Field<string>("Note");
            dettaglioVeicoloModelView.IsDisponibile = row.Field<bool>("IsDisponibile");
            dettaglioVeicoloModelView.Cognome = row.Field<string>("Cognome");
            dettaglioVeicoloModelView.Nome = row.Field<string>("Nome");
            dettaglioVeicoloModelView.CodiceFiscale = row.Field<string>("CodiceFiscale");
            return dettaglioVeicoloModelView;
        }
                
        public class RicercaVeicoliModel
        {
            public int? IdMarca { get; set; }
            public string Modello { get; set; }
            public string Targa { get; set; }
            public DateTime? InizioDataImmatricolazione { get; set; }
            public DateTime? FineDataImmatricolazione { get; set; }
            public bool? IsDisponibile { get; set; }
        }

        public List<VeicoliTrovatiModelView> RicercaVeicoli(RicercaVeicoliModel ricercaVeicoliModel)        // Ricerca Veicoli tramite i campi Marca, Modello, Targa, Periodo Immatricolazione e disponibilità
        {
            var veicoliTrovatiList = new List<VeicoliTrovatiModelView>();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("\t[Veicoli].[Id]");
            sb.AppendLine("\t,[MarcheVeicoli].[Descrizione] as Marca");
            sb.AppendLine("\t,[Modello]");
            sb.AppendLine("\t,[DataImmatricolazione]");
            sb.AppendLine("\t,[IsDisponibile]");
            sb.AppendLine("FROM [dbo].[Veicoli]");
            sb.AppendLine("\tINNER JOIN [dbo].[MarcheVeicoli]");
            sb.AppendLine("\tON [dbo].[Veicoli].[IdMarca] = [dbo].[MarcheVeicoli].[Id]");
            sb.AppendLine("WHERE [IdTipoStato]=@IdTipoStato");       // filtra i veicoli prendendo solo quelli attivi
            
            if (ricercaVeicoliModel.IdMarca.HasValue)
            {
                sb.AppendLine("And IdMarca = @IdMarca");
            }
            if (!string.IsNullOrWhiteSpace(ricercaVeicoliModel.Modello))
            {
                sb.AppendLine("And Modello like '%'+@Modello+'%'");
            }
            if (!string.IsNullOrWhiteSpace(ricercaVeicoliModel.Targa))
            {
                sb.AppendLine("And Targa like '%'+@Targa+'%'");
            }
            if (ricercaVeicoliModel.InizioDataImmatricolazione.HasValue)
            {
                sb.AppendLine("And InizioDataImmatricolazione >= @InizioDataImmatricolazione");
            }
            if (ricercaVeicoliModel.FineDataImmatricolazione.HasValue)
            {
                sb.AppendLine("And FineDataImmatricolazione <= @FineDataImmatricolazione");
            }
            if (ricercaVeicoliModel.IsDisponibile.HasValue)
            {
                sb.AppendLine("And IsDisponibile = @IsDisponibile");
            }

            var dataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {
                    if (ricercaVeicoliModel.IdMarca.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@IdMarca", ricercaVeicoliModel.IdMarca);
                    }
                    if (!string.IsNullOrEmpty(ricercaVeicoliModel.Modello))
                    {
                        sqlCommand.Parameters.AddWithValue("@Modello", ricercaVeicoliModel.Modello);
                    }
                    if (!string.IsNullOrEmpty(ricercaVeicoliModel.Targa))
                    {
                        sqlCommand.Parameters.AddWithValue("@Targa", ricercaVeicoliModel.Targa);
                    }                    
                    if (ricercaVeicoliModel.InizioDataImmatricolazione.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@InizioDataImmatricolazione", ricercaVeicoliModel.InizioDataImmatricolazione);
                    }
                    if (ricercaVeicoliModel.FineDataImmatricolazione.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@FineDataImmatricolazione", ricercaVeicoliModel.FineDataImmatricolazione);
                    }
                    if (ricercaVeicoliModel.IsDisponibile.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@IsDisponibile", ricercaVeicoliModel.IsDisponibile);
                    }
                    sqlCommand.Parameters.AddWithValue("@IdTipoStato", 1);

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
                return new List<VeicoliTrovatiModelView>();
            }

            var dataTable = dataSet.Tables[0];

            if (dataTable == null || dataTable.Rows.Count <= 0)     // controlla che il dataTable sia diverso da null e contenga almeno una riga
            {
                return new List<VeicoliTrovatiModelView>();
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var veicoliTrovatiModelView = new VeicoliTrovatiModelView();
                veicoliTrovatiModelView.Id = dataRow.Field<int>("Id");
                veicoliTrovatiModelView.Marca = dataRow.Field<string>("Marca");
                veicoliTrovatiModelView.Modello = dataRow.Field<string>("Modello");
                veicoliTrovatiModelView.DataImmatricolazione = dataRow.Field<DateTime>("DataImmatricolazione");
                bool disponibilita = dataRow.Field<bool>("IsDisponibile");
                veicoliTrovatiModelView.IsDisponibile = disponibilita ? "Disponibile" : "Noleggiato";       // Serve per vedere "disponibile" o "noleggiato" al posto di true o false
                veicoliTrovatiList.Add(veicoliTrovatiModelView);
            }
            return veicoliTrovatiList;
        }
    }
}
