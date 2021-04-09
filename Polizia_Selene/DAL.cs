using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Polizia_Selene
{
    static class DAL
    {
        static string _connectionString = ConfigurationManager.ConnectionStrings["Polizia_Selene"].ConnectionString;

        public static List<Agente> MostraTuttiGliAgenti()
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(@"Select * from AgenteDiPolizia", conn);
            {
                //Apro prima di tutto la connessione
                conn.Open();
                //Utilizzo il reader per leggere la select
                SqlDataReader reader = cmd.ExecuteReader();

                //Creo una nuova lista agente dove andare ad inserire la lista di agenti recuperata dalla select
                List<Agente> elencoAgenti = new List<Agente>();

                //Leggo tutti gli agenti e li inserisco nella lista
                while (reader.Read())
                {
                    elencoAgenti.Add(new Agente(
                        (int)reader["IdAgente"],
                        reader["Nome"].ToString(),
                        reader["Cognome"].ToString(),
                        reader["CodiceFiscale"].ToString(),
                        (DateTime)reader["DataNascita"],
                        (int)reader["AnniServizio"]
                        ));
                }

                //Ritorno una lista di agenti 
                return elencoAgenti;
            }

        }

        public static List<Agente> FiltraAgenti(int inputUtente)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(@"Select * from AgenteDiPolizia where AnniServizio >= @inputUtente", conn);
            {
                //Associo il parametro input utente per inserirlo come parametro nella mia select
                cmd.Parameters.AddWithValue("@inputUtente", inputUtente);

                //Apro prima di tutto la connessione
                conn.Open();
                //Utilizzo il reader per leggere la select
                SqlDataReader reader = cmd.ExecuteReader();

                //Creo una nuova lista agente dove andare ad inserire la lista di agenti recuperata dalla select
                List<Agente> FiltroAgenti = new List<Agente>();

                //Leggo tutti gli agenti e li inserisco nella lista
                while (reader.Read())
                {
                    FiltroAgenti.Add(new Agente(
                        (int)reader["IdAgente"],
                        reader["Nome"].ToString(),
                        reader["Cognome"].ToString(),
                        reader["CodiceFiscale"].ToString(),
                        (DateTime)reader["DataNascita"],
                        (int)reader["AnniServizio"]
                        ));
                }
                //Ritorno una lista di agenti 
                return FiltroAgenti;
            }


        }

        public static List<Agente> AgentiPerArea(string codiceArea)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            //Sarebbe meglio usare una store Procedure qui ma non sicura di come passare poi il singolo file 
            //assieme al progetto. Sono stati recuperati anche i dati per RISCHIO AREA ->implementabile 
            using SqlCommand cmd = new SqlCommand(@"
            SELECT  AgenteDiPolizia.IdAgente, AgenteDiPolizia.Nome, AgenteDiPolizia.Cognome, AgenteDiPolizia.CodiceFiscale, AgenteDiPolizia.DataNascita, AgenteDiPolizia.AnniServizio, AreaMetropolitana.RischioArea
            FROM    AgenteDiPolizia
            INNER JOIN
                   Associazioni ON AgenteDiPolizia.IdAgente = Associazioni.IdAgente
            INNER JOIN
                   AreaMetropolitana ON Associazioni.IdArea = AreaMetropolitana.IdArea
            WHERE  (AreaMetropolitana.CodiceArea = @codiceArea)
            GROUP BY AgenteDiPolizia.IdAgente, AgenteDiPolizia.Nome, AgenteDiPolizia.Cognome, AgenteDiPolizia.CodiceFiscale, AgenteDiPolizia.DataNascita, AgenteDiPolizia.AnniServizio, AreaMetropolitana.RischioArea",
                         conn);
            {
                //Associo il parametro input utente per inserirlo come parametro nella mia select
                cmd.Parameters.AddWithValue("@codiceArea", codiceArea);

                //Apro prima di tutto la connessione
                conn.Open();
                //Utilizzo il reader per leggere la select
                SqlDataReader reader = cmd.ExecuteReader();

                //Creo una nuova lista agente dove andare ad inserire la lista di agenti recuperata dalla select
                List<Agente> AgentiPerArea = new List<Agente>();

                //Leggo tutti gli agenti e li inserisco nella lista
                while (reader.Read())
                {
                    AgentiPerArea.Add(new Agente(
                        (int)reader["IdAgente"],
                        reader["Nome"].ToString(),
                        reader["Cognome"].ToString(),
                        reader["CodiceFiscale"].ToString(),
                        (DateTime)reader["DataNascita"],
                        (int)reader["AnniServizio"]
                        ));
                }

                //Ritorno una lista di agenti 
                return AgentiPerArea;
            }

        }


        public static void InserisciAgente(string nome, string cognome, string codicefiscale, DateTime dataNascita, int anniDiServizio)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlDataAdapter da = new SqlDataAdapter(@"Select * from AgenteDiPolizia", conn);

            {
                DataSet ds = new DataSet();

                //inizializzo e riempio un dataset. E' fill ad aprire e chiudere la connessione

                da.Fill(ds, "AgenteDiPolizia");
                //in questo DataSet è contenuto tutta la tabella agenti
                DataTable tabellaAgenti = ds.Tables["AgenteDiPolizia"];

                //DA qui in poi siamo disconnessi!

                //Aggiungo i vari capi
                tabellaAgenti.Rows.Add(0, nome , cognome, codicefiscale, dataNascita, anniDiServizio);

                //genera in automatico i command di updat, delete e insert necessari
                //creo i comandi per l'update del db
                new SqlCommandBuilder(da);
                //Riporta le modifiche sul database

                //CONNESSO
                da.Update(tabellaAgenti);
                //DISCONNESSO

                //TODO Inserire qui una riapertura/chiususa per il recupero dell'ID



            }

        }
    }
}