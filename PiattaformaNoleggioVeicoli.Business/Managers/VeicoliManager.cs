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

                        var dataTable = dataSet.Tables[0];


                        if (dataTable == null || dataTable.Rows.Count <= 0)
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
                    }
                }
            }
            return TipoAlimentazioneList;
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

                        var dataTable = dataSet.Tables[0];


                        if (dataTable == null || dataTable.Rows.Count <= 0)
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
                    }
                }
            }
            return MarcheVeicoliList;
        }

        public bool InsertVeicolo(Models.VeicoliModel veicoloModel)     // Inserisce veicolo
        {
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
            sb.AppendLine("@Codice");
            sb.AppendLine(",@IdMarca");
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
                    sqlCommand.Parameters.AddWithValue("@IdTipoStato", veicoloModel.IdTipoStato);
                    var numRigheInserite = sqlCommand.ExecuteNonQuery();
                    if (numRigheInserite >= 1)
                    {
                        isInserito = true;
                    }
                }
            }
            return isInserito;
        }

        public DettaglioVeicoloModelView GetVeicolo(int id)     // Restituisce i dettagli di un determinato veicolo ricercato tramite id
        {
            var dettaglioVeicoloModelView = new DettaglioVeicoloModelView();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("\t[Veicoli].[Id]");
            sb.AppendLine("\t,[IdMarca]");
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
            sb.AppendLine("\tINNER JOIN [dbo].[Noleggi]");
            sb.AppendLine("\tON [dbo].[Noleggi].[IdVeicolo] = [dbo].[Veicoli].[Id]");
            sb.AppendLine("\tINNER JOIN [dbo].[Clienti]");
            sb.AppendLine("\tON [dbo].[Noleggi].[IdCliente] = [dbo].[Clienti].[Id]");
            sb.AppendLine("\tWHERE [Veicoli].[Id] = @Id");
            sb.AppendLine("\tAND [Veicoli].[IdTipoStato] = @Attivo");

            var dataSet = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sb.ToString()))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    sqlCommand.Parameters.AddWithValue("@Attivo", 13);


                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        sqlDataAdapter.SelectCommand.Connection = sqlConnection;
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count == 0)
                        {
                            return null;
                        }
                        DataRow row = dataTable.Rows[0];
                        dettaglioVeicoloModelView.Id = row.Field<int>("Id");
                        dettaglioVeicoloModelView.IdMarca = row.Field<int>("IdMarca");
                        dettaglioVeicoloModelView.Modello = row.Field<string>("Modello");
                        dettaglioVeicoloModelView.Targa = row.Field<string>("Targa");
                        dettaglioVeicoloModelView.DataImmatricolazione = row.Field<DateTime>("DataImmatricolazione");
                        dettaglioVeicoloModelView.IdTipoAlimentazione = row.Field<int>("IdTipoAlimentazione");
                        dettaglioVeicoloModelView.Note = row.Field<string>("Note");
                        dettaglioVeicoloModelView.IsDisponibile = row.Field<bool>("IsDisponibile");
                        dettaglioVeicoloModelView.Cognome = row.Field<string>("Cognome");
                        dettaglioVeicoloModelView.Nome = row.Field<string>("Nome");
                        dettaglioVeicoloModelView.CodiceFiscale = row.Field<string>("CodiceFiscale");
                    }
                }
            }
            return dettaglioVeicoloModelView;
        }



    }
}
