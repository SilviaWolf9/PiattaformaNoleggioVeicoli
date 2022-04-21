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
        }

        public bool InserisciNoleggio(NoleggiModelView noleggiModel)     // Inserisce noleggio su db
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
                        //sqlCommand.Parameters.AddWithValue("@IdVeicolo", noleggiModel.IdVeicolo);
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
            return isInserito;
        }

        private bool IsNoleggioModelValido(object noleggio)       // Fa un controllo sull'oggetto noleggio ed evita di spaccarsi in caso ClienteModel fosse null
        {
            if (noleggio == null)
            {
                return false;
            }
            var verificaNoleggio = (NoleggiModelView)noleggio;

            if (!verificaNoleggio.IdVeicolo.HasValue)
            {
                return false;
            }
            if (!verificaNoleggio.IdCliente.HasValue)
            {
                return false;
            }            
            return true;
        }

        public bool TerminaNoleggio(NoleggiModelView noleggiModel)     // Inserisce noleggio su db
        {
            if (!IsNoleggioModelValido(noleggiModel))
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
            return isInserito;
        }
    
        public NoleggiModelView GetDatiNoleggio(NoleggiModel noleggio)
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
    }
}
