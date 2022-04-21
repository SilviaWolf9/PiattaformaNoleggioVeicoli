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
    }
}
