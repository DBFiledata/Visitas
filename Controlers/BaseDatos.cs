using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.UI;
using System.Configuration;
using paginaOficial.Models;

namespace paginaOficial.Controlers
{
    public class BaseDatos
    {
        MySqlConnection Cn;//variable que almacenara la conexion
        MySqlCommand Cmd;//variable que almacenara los comandos
        MySqlDataAdapter Adap;
        DataSet ds;
        public BaseDatos()//constructor que realiza la connexion 
        {
            Cn = new MySqlConnection();//enstanciamos la variable cn
            Cn.ConnectionString = ConfigurationManager.ConnectionStrings["security_controlConnectionString"].ConnectionString;
            Cmd = new MySqlCommand();
            Cmd.Connection = Cn;
        }

        public bool RegistrarUsuario(string user, string passw, string nombre, string apellido, string tipo)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "crearUsuario";
                Cmd.Parameters.Add(new MySqlParameter("@user_n", user));
                Cmd.Parameters.Add(new MySqlParameter("@passw", passw));
                Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
                Cmd.Parameters.Add(new MySqlParameter("@apellido", apellido));
                Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public bool ActualizarUsuario(string idsusario, string idnuevo, string nombre, string apellido, string tipo)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "actualizarUsuario";
                Cmd.Parameters.Add(new MySqlParameter("@user_n", idsusario));
                Cmd.Parameters.Add(new MySqlParameter("@user_nuevo", idnuevo));
                Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
                Cmd.Parameters.Add(new MySqlParameter("@apellido", apellido));
                Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();


                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }

        }

        public bool ImprimirVisitas(int numero)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "ImprimirVisitas";
                Cmd.Parameters.Add(new MySqlParameter("@visita", numero));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();


                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }

        }

        public bool EliminarUsuario(string idususario)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "eliminarUsuario";
                Cmd.Parameters.Add(new MySqlParameter("@user_n", idususario));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }

        }

        public bool ActualizarPassw(string idsusario, string passnueva)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "actualizarPassword";
                Cmd.Parameters.Add(new MySqlParameter("@user_n", idsusario));
                Cmd.Parameters.Add(new MySqlParameter("@passw", passnueva));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }

        }

        public bool login(string user, string pass)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "loginPagina";
                Cmd.Parameters.Add(new MySqlParameter("@user_n", user));
                Cmd.Parameters.Add(new MySqlParameter("@passw", pass));
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
                string resultado = Convert.ToString(ds.Tables[0].Rows[0]["user_name"]);//se asingna el resultado del dataset 
                if (resultado != null)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }

        }

        public string[] infoCuenta(string id)
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "buscarUsuario";
                Cmd.Parameters.Add(new MySqlParameter("@user_n", id));
                ds = new DataSet();//instancia dataset

                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
                string[] resultado = new string[4];
                resultado[0] = Convert.ToString(ds.Tables[0].Rows[0]["user_name"]);//se asingna el resultado del dataset 
                resultado[1] = Convert.ToString(ds.Tables[0].Rows[0]["nombres"]);
                resultado[2] = Convert.ToString(ds.Tables[0].Rows[0]["apellidos"]);
                resultado[3] = Convert.ToString(ds.Tables[0].Rows[0]["tipos"]);


                //int d = Cmd.ExecuteNonQuery();

                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    resultado = null;
                }
                Cmd.Connection.Close();
                return resultado;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return null;

            }

        }

        public string[] getDatosSession(string user, string passw)
        {
            try
            {
                string[] ejecucion;
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "loginPagina";
                Cmd.Parameters.Add(new MySqlParameter("@user_n", user));
                Cmd.Parameters.Add(new MySqlParameter("@passw", passw));
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
                string idc = Convert.ToString(ds.Tables[0].Rows[0]["user_name"]);//se asingna el resultado del dataset
                string[] resultado = new string[3];
                resultado[0] = Convert.ToString(ds.Tables[0].Rows[0]["nombres"]);
                resultado[1] = Convert.ToString(ds.Tables[0].Rows[0]["tipos"]);
                resultado[2] = Convert.ToString(ds.Tables[0].Rows[0]["id_user"]);
                if (resultado[0] != null | resultado[1] != null)
                {
                    ejecucion = resultado;
                }
                else
                {
                    ejecucion = null;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return null;

            }

        }

        public DataSet ListaTotal()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaUsuariosTotal";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public DataSet listaImprecion()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaImprecion";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public DataSet listaVisitas()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaVisitas";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public DataSet listaVisitasActivas()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaVisitasActivas";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }
        
        public string[] infoVisitante(string id)
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "datosPersons";
                Cmd.Parameters.Add(new MySqlParameter("@ced", id));
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
                string[] resultado = new string[8];
                resultado[0] = Convert.ToString(ds.Tables[0].Rows[0]["nombre_completo"]);//se asingna el resultado del dataset 
                resultado[1] = Convert.ToString(ds.Tables[0].Rows[0]["cedula"]);
                resultado[2] = Convert.ToString(ds.Tables[0].Rows[0]["nacionalidad"]);
                resultado[3] = Convert.ToString(ds.Tables[0].Rows[0]["nacimiento"]);
                resultado[4] = Convert.ToString(ds.Tables[0].Rows[0]["sexo"]);
                resultado[5] = Convert.ToString(ds.Tables[0].Rows[0]["expiracion"]);
                resultado[6] = Convert.ToString(ds.Tables[0].Rows[0]["foto"]);
                resultado[7] = Convert.ToString(ds.Tables[0].Rows[0]["id_persona"]);

                //int d = Cmd.ExecuteNonQuery();

                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    resultado = null;
                }
                Cmd.Connection.Close();
                return resultado;
            }
            catch (Exception e)
            {
                return null;

            }

        }
        public string[] infoPadron(string id)
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "datosPadron";
                Cmd.Parameters.Add(new MySqlParameter("@ced", id));
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
                string[] resultado = new string[7];
                resultado[0] = Convert.ToString(ds.Tables[0].Rows[0]["nombre_completo"]);//se asingna el resultado del dataset 
                resultado[1] = Convert.ToString(ds.Tables[0].Rows[0]["cedula"]);
                resultado[2] = Convert.ToString(ds.Tables[0].Rows[0]["nacionalidad"]);
                resultado[3] = Convert.ToString(ds.Tables[0].Rows[0]["nacimiento"]);
                resultado[4] = Convert.ToString(ds.Tables[0].Rows[0]["sexo"]);
                //int d = Cmd.ExecuteNonQuery();

                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    resultado = null;
                }
                Cmd.Connection.Close();
                return resultado;
            }
            catch (Exception e)
            {
                return null;

            }

        }



        public bool registroPersons(string cedulas, string nombre, string nacionalidades, string nacimientos, string sexos)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "registroPersons";
                Cmd.Parameters.Add(new MySqlParameter("@cedulas", cedulas));
                Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
                Cmd.Parameters.Add(new MySqlParameter("@nacionalidades", nacionalidades));
                Cmd.Parameters.Add(new MySqlParameter("@nacimientos", nacimientos));
                Cmd.Parameters.Add(new MySqlParameter("@sexos", sexos));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public bool crearVisitas(int area, int motivos, int lugar, string descripciones, string userName, string cedulas)
        {
            try
            {

                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "crearVisitas";
                Cmd.Parameters.Add(new MySqlParameter("@area", area));
                Cmd.Parameters.Add(new MySqlParameter("@motivos", motivos));
                Cmd.Parameters.Add(new MySqlParameter("@lugar", lugar));
                Cmd.Parameters.Add(new MySqlParameter("@descripciones", descripciones));
                Cmd.Parameters.Add(new MySqlParameter("@userName", userName));
                Cmd.Parameters.Add(new MySqlParameter("@cedulas", cedulas));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        // Visitas anteriores de una persona (por cedula, mismo criterio que datosPersons).
        public DataSet historialVisitasPersona(string cedula)
        {
            ds = new DataSet();
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "historialVisitasPersona";
                Cmd.Parameters.Add(new MySqlParameter("@p_cedula", cedula));
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
            }
            return ds;
        }

        // Visita grupal (tabla group_visits, independiente de visits). area -> lugar_id (places),
        // lugar -> idtypeOfPlaces (typeofplaces), motivo -> idreasonsForVisits, igual que crearVisitas.
        public bool crearVisitaGrupal(string entidad, int cantidad, int area, int lugar, int motivo, string userName)
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "crearVisitaGrupal";
                Cmd.Parameters.Add(new MySqlParameter("@p_entidad", entidad));
                Cmd.Parameters.Add(new MySqlParameter("@p_cantidad", cantidad));
                Cmd.Parameters.Add(new MySqlParameter("@p_area", area));
                Cmd.Parameters.Add(new MySqlParameter("@p_lugar", lugar));
                Cmd.Parameters.Add(new MySqlParameter("@p_motivo", motivo));
                Cmd.Parameters.Add(new MySqlParameter("@p_userName", userName));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                return d > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //vericar procedure
        public DataSet filtroUsuario(string nombre, string apellido, string tipo)
        {
            try
            {

                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "filtroUsuario";
                Cmd.Parameters.Add(new MySqlParameter("@nombre", nombre));
                Cmd.Parameters.Add(new MySqlParameter("@apellido", apellido));
                Cmd.Parameters.Add(new MySqlParameter("@tipo", tipo));
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }
        

        public DataSet listaAreas()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaAreas";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }
        
        public DataSet listaAreasVisitas(int numero)
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaAreasVisitas";
                Cmd.Parameters.Add(new MySqlParameter("@id", numero));
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public DataSet listaRazonesVisitas()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaRazonesVisitas";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }


        public DataSet listaTipoAreas()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaTipoAreas";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        // ===== Catalogos CRUD (Area) - sdd/crud-catalogos-area-lugar-motivo, Slice 1 =====

        public bool CrearArea(string nombre)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "crearArea";
                Cmd.Parameters.Add(new MySqlParameter("@p_nombre", nombre));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public bool ActualizarArea(string id, string nombre)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "actualizarArea";
                Cmd.Parameters.Add(new MySqlParameter("@p_id", id));
                Cmd.Parameters.Add(new MySqlParameter("@p_nombre", nombre));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public bool EliminarArea(string id)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "eliminarArea";
                Cmd.Parameters.Add(new MySqlParameter("@p_id", id));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public DataSet ListaAreasAdmin()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaAreasAdmin";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        // ===== Catalogos CRUD (Lugar) - sdd/crud-catalogos-area-lugar-motivo, Slice 2 =====

        public bool CrearLugar(string nombre, string idArea)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "crearLugar";
                Cmd.Parameters.Add(new MySqlParameter("@p_nombre", nombre));
                Cmd.Parameters.Add(new MySqlParameter("@p_idArea", idArea));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public bool ActualizarLugar(string id, string nombre, string idArea)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "actualizarLugar";
                Cmd.Parameters.Add(new MySqlParameter("@p_id", id));
                Cmd.Parameters.Add(new MySqlParameter("@p_nombre", nombre));
                Cmd.Parameters.Add(new MySqlParameter("@p_idArea", idArea));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public bool EliminarLugar(string id)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "eliminarLugar";
                Cmd.Parameters.Add(new MySqlParameter("@p_id", id));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public DataSet ListaLugaresAdmin()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaLugaresAdmin";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        // ===== Catalogos CRUD (Motivo) - sdd/crud-catalogos-area-lugar-motivo, Slice 3 =====

        public bool CrearMotivo(string nombre)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "crearMotivo";
                Cmd.Parameters.Add(new MySqlParameter("@p_nombre", nombre));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public bool ActualizarMotivo(string id, string nombre)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "actualizarMotivo";
                Cmd.Parameters.Add(new MySqlParameter("@p_id", id));
                Cmd.Parameters.Add(new MySqlParameter("@p_nombre", nombre));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public bool EliminarMotivo(string id)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "eliminarMotivo";
                Cmd.Parameters.Add(new MySqlParameter("@p_id", id));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public DataSet ListaMotivosAdmin()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "listaMotivosAdmin";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public DataSet filtroColaImprsion(string estado, int numero)
        {
            try
            {

                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "filtroColaImprsion";
                Cmd.Parameters.Add(new MySqlParameter("@estado", estado));
                Cmd.Parameters.Add(new MySqlParameter("@numero", numero));
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public string totalVisitasMes()
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "totalVisitasMes";
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
                string resultado;
                resultado = Convert.ToString(ds.Tables[0].Rows[0]["Cantidad"]);//se asingna el resultado del dataset 

                //int d = Cmd.ExecuteNonQuery();

                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    resultado = null;
                }
                Cmd.Connection.Close();
                return resultado;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return null;

            }

        }

        public string totalVisitasDia()
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "totalVisitasDia";
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
                string resultado;
                resultado = Convert.ToString(ds.Tables[0].Rows[0]["Cantidad"]);//se asingna el resultado del dataset 

                //int d = Cmd.ExecuteNonQuery();

                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    resultado = null;
                }
                Cmd.Connection.Close();
                return resultado;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return null;

            }

        }

        public DataSet filtroVisitas(int area, int lugar, int motivo, string cedula, int numero, string desde, string hasta)
        {
            try
            {

                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "filtroVisitas";
                Cmd.Parameters.Add(new MySqlParameter("@area", area));
                Cmd.Parameters.Add(new MySqlParameter("@lugar", lugar));
                Cmd.Parameters.Add(new MySqlParameter("@motivo", motivo));
                Cmd.Parameters.Add(new MySqlParameter("@cedula", cedula));
                Cmd.Parameters.Add(new MySqlParameter("@numero", numero));
                Cmd.Parameters.Add(new MySqlParameter("@desde", desde));
                Cmd.Parameters.Add(new MySqlParameter("@hasta", hasta));
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public DataSet ultimasVisitas(int topN)
        {
            try
            {

                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "ultimasVisitas";
                Cmd.Parameters.Add(new MySqlParameter("@topN", topN));
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public DataSet EstadisticaAreaMensual()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "estadisticaAreasMensual";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public DataSet estadisticaAreasDiaria()
        {
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "estadisticaAreasDiaria";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;

            }
            return ds;
        }

        public bool finalizarVisita(int id)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "finalizarVisita";
                Cmd.Parameters.Add(new MySqlParameter("@numero", id));
                ds = new DataSet();//instancia dataset
                Cmd.Connection.Open();
                int resultado = Cmd.ExecuteNonQuery();

                if (resultado > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        public string buscarVisitaDatos(string id)
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "buscarVisitaDatos";
                Cmd.Parameters.Add(new MySqlParameter("@numero", id));
                ds = new DataSet();//instancia dataset
                Adap = new MySqlDataAdapter(Cmd);//adapter se pasa el cmd
                Adap.Fill(ds);//se asigna el dataset al adapter
                string resultado;
                resultado = Convert.ToString(ds.Tables[0].Rows[0]["id_visita"]);
                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    resultado = null;
                }
                Cmd.Connection.Close();
                return resultado;
            }
            catch (Exception e)
            {

                return null;

            }

        }


        #region Method for API
        public int LoginSecurity(string user, string pass)
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "app_Login";
                Cmd.Parameters.Add(new MySqlParameter("@u", user));
                Cmd.Parameters.Add(new MySqlParameter("@p", pass));
                ds = new DataSet();

                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
                var existe = ds.Tables[0].Rows[0]["existe"] + "";
                Cmd.Connection.Close();
                if (!string.IsNullOrEmpty(existe))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return 0;
            }
        }

        public List<Lugares> Lugares()
        {
            var lugares = new List<Lugares>();
            try
            {
                ds = new DataSet();
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "app_Lugares";
                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var lugarId = Convert.ToInt32(row["id_lugar"].ToString());
                        var lugar = new Lugares()
                        {
                            IdLugar = lugarId,
                            Lugar = row["lugar"].ToString()
                        };
                        lugares.Add(lugar);
                    }
                }
                return lugares;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UpdateSalida(string c)
        {
            try
            {
                string p;
                bool ejecucion;
                //SP app_ExistPerson
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "app_ExistPerson";
                Cmd.Parameters.Add(new MySqlParameter("@c", c));
                ds = new DataSet();

                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
                var existe = ds.Tables[0].Rows[0]["existe"] + "";
                var id_persona = ds.Tables[0].Rows[0]["id_persona"] + "";
                Cmd.Connection.Close();

                //SP app_UpdateSalida
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "app_UpdateSalida";
                Cmd.Parameters.Add(new MySqlParameter("@p", id_persona));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Persona BuscarPadron(string c)
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "app_BuscarPadron";
                Cmd.Parameters.Add(new MySqlParameter("@c", c));
                ds = new DataSet();

                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
                var datos = ds.Tables[0].Rows[0]["cedula"] + "";
                Cmd.Connection.Close();
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var persona = new Persona()
                        {
                            Cedula = row["cedula"].ToString() + "",
                            NombreCompleto = row["nombre"].ToString() + " " + row["apellido"].ToString() + "",
                            Sexo = row["sexo"].ToString() + ""
                        };
                        return persona;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Boolean UpdateImagePath(string query)
        {
            try
            {
                Cmd.CommandText = query;
                Cmd.Connection = Cn;
                Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SavePersonaOrVisita(Persona persona, Visita visit)
        {
            var existePersona = this.existePersonaId(persona.Cedula);
            if (existePersona != 0)
            {
                return InsertVisita(visit, existePersona);
            }
            else
            {
                var wasInsert = InsertPersona(persona);
                if (wasInsert)
                {
                    var newPersona = this.existePersonaId(persona.Cedula);
                    return InsertVisita(visit, newPersona);
                }
            }

            return false;
        }

        private int existePersonaId(string cedula)
        {
            try
            {
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "app_ExistPerson";
                Cmd.Parameters.Add(new MySqlParameter("@c", cedula));
                ds = new DataSet();

                Adap = new MySqlDataAdapter(Cmd);
                Adap.Fill(ds);
                var existe = ds.Tables[0].Rows[0]["id_persona"] + "";
                Cmd.Connection.Close();
                if (!string.IsNullOrEmpty(existe))
                {
                    return Convert.ToInt32(existe);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return 0;
            }
        }

        private bool InsertPersona(Persona person)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "app_AddPersona";
                Cmd.Parameters.Add(new MySqlParameter("@n", person.NombreCompleto));
                Cmd.Parameters.Add(new MySqlParameter("@c", person.Cedula));
                Cmd.Parameters.Add(new MySqlParameter("@na", person.Nacionalidad));
                Cmd.Parameters.Add(new MySqlParameter("@fn", person.FechaNacimiento));
                Cmd.Parameters.Add(new MySqlParameter("@s", person.Sexo));
                Cmd.Parameters.Add(new MySqlParameter("@fe", person.FechaExpiracion));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }

        private bool InsertVisita(Visita visit, int personId)
        {
            try
            {
                bool ejecucion;
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "app_AddVisita";
                Cmd.Parameters.Add(new MySqlParameter("@m", visit.Motivo));
                Cmd.Parameters.Add(new MySqlParameter("@l", visit.Lugar_id));
                Cmd.Parameters.Add(new MySqlParameter("@d", visit.Descripcion));
                //Cmd.Parameters.Add(new MySqlParameter("@u", userName));
                Cmd.Parameters.Add(new MySqlParameter("@p", personId));
                Cmd.Connection.Open();
                int d = Cmd.ExecuteNonQuery();

                if (d > 0)
                {
                    ejecucion = true;
                }
                else
                {
                    ejecucion = false;
                }
                Cmd.Connection.Close();
                return ejecucion;
            }
            catch (Exception e)
            {
                //OError.Write_error(e.Message + "-" + e.StackTrace);
                //OError.Error_msg = error_con;
                return false;

            }
        }
        #endregion
    }
}
