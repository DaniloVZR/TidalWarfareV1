using System;
using System.Collections.Generic;
using System.Configuration; 
using System.Data.SqlClient; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TidalWarfareV1
{
    // Clase estática para gestionar las operaciones con la base de datos
    public static class GestionDB
    {
     
        static SqlConnection conn; 
        static SqlCommand cmd;     
        static SqlDataReader reader;  
        static string strCon;      


        static GestionDB()
        {

        }

        // Método privado para conectar a la base de datos
        static void conectar()
        {
            try
            {
                // Lee la cadena de conexión desde el archivo de configuración
                strCon = ConfigurationManager.ConnectionStrings["strCon"].ToString();

                // Crea una nueva conexión con la base de datos
                conn = new SqlConnection(strCon);

                // Abre la conexión
                conn.Open();
            }
            catch (Exception ex)
            {
       
                string msg = ex.Message;
            }
        }

        // Registra un nuevo jugador en la base de datos
        public static int RegistrarJugador(string nombreJugador)
        {
            int result = 0; // Almacena el número de filas afectadas
            string query = "insert into jugador values (@nombreJugador, 0, 0, 0)"; 

            conectar(); 
            try
            {
               
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombreJugador", nombreJugador); 

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
             
                string msg = ex.Message;
            }
            conn.Close();
            return result;
        }

        // Agrega una victoria al jugador indicado
        public static int AgregarVictoria(string nombreJugador)
        {
            int result = 0; // Número de filas afectadas
            string query = "update jugador set victorias = victorias + 1, partidas_jugadas = partidas_jugadas + 1 where nickname = @nombreJugador"; // Incrementa victorias y partidas jugadas

            conectar(); 
            try
            {
                
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombreJugador", nombreJugador);

                
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
           
                string msg = ex.Message;
            }
            conn.Close(); 
            return result; 
        }

        // Agrega una derrota al jugador indicado
        public static int AgregarDerrota(string nombreJugador)
        {
            int result = 0; 
            string query = "update jugador set derrotas = derrotas + 1, partidas_jugadas = partidas_jugadas + 1 where nickname = @nombreJugador"; // Incrementa derrotas y partidas jugadas

            conectar(); 
            try
            {
            
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombreJugador", nombreJugador);

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
           
                string msg = ex.Message;
            }
            conn.Close(); 
            return result; 
        }

        // Busca un jugador específico y devuelve el número de coincidencias
        public static int BuscarJugadores(string nombreJugador)
        {
            int result = 0; 
            string query = "SELECT COUNT(*) FROM jugador WHERE nickname = @nombreJugador"; 

            conectar();
            try
            {
           
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombreJugador", nombreJugador);

            
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
              
                string msg = ex.Message;
            }
            conn.Close();
            return result; 
        }

        // Recupera una lista de todos los jugadores, ordenados por victorias en orden descendente
        public static List<Jugador> BuscarJugadores()
        {
            List<Jugador> jugadores = new List<Jugador>(); 

            string query = "select * from jugador order by victorias desc"; // Consulta para obtener jugadores ordenados por victorias

            conectar();
            try { 
         
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();

      
                while (reader.Read())
                {
                    // Crea un nuevo objeto jugador y llena sus propiedades desde la base de datos
                    Jugador jugador = new Jugador();
                    jugador.Id_jugador = reader.GetInt32(0); 
                    jugador.Nickname = reader.GetString(1); 
                    jugador.Victorias = reader.GetInt32(2); 
                    jugador.Derrotas = reader.GetInt32(3);  
                    jugador.Partidas = reader.GetInt32(4);  

                   
                    jugadores.Add(jugador);
                }
                reader.Close(); 
            }
            catch (Exception ex)
            {
               
                string msg = ex.Message;
            }
            conn.Close(); 
            return jugadores;
        }
    }
}
