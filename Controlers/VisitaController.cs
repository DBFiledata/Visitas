using paginaOficial.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace paginaOficial.Controlers
{
    public class VisitaController : ApiController
    {
        [HttpGet]
        public int Login(string u, string p)
        {
            BaseDatos con = new BaseDatos();

            int result = con.LoginSecurity(u, p);
            if (result > 0)
            {
                return result;
            }
            return 0;
        }

        [HttpGet]
        public List<Lugares> Lugares()
        {
            BaseDatos con = new BaseDatos();
            return con.Lugares();
        }

        [HttpGet]
        public int Exit(string cedula)
        {
            BaseDatos con = new BaseDatos();

            bool result = con.UpdateSalida(cedula);
            if (result)
            {
                return 1;
            }

            return 0;
        }

        [HttpGet]
        public Persona Padron(string cedula)
        {
            BaseDatos con = new BaseDatos();

            var result = con.BuscarPadron(cedula);
            if (result != null)
            {
                return result;
            }

            return null;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Pic(string cedula)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                BaseDatos con = new BaseDatos();
                var httpRequest = HttpContext.Current.Request;
                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.Conflict, dict);
                        }

                        else
                        {
                            try
                            {
                                //if needed write the code to update the table
                                var filePath = Path.Combine("carnetPics", cedula + extension);
                                //return Request.CreateResponse(HttpStatusCode.Accepted, dict);

                                var updatedQuery = "UPDATE persons SET foto ='" + cedula + extension + "' WHERE cedula='" + cedula + "'";
                                var valid = con.UpdateImagePath(updatedQuery);
                                //Userimage myfolder name where i want to save my image
                                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/"), filePath);
                                postedFile.SaveAs(path);
                            }
                            catch (Exception r)
                            {

                                throw;
                            }
                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                var response = Request.CreateResponse(HttpStatusCode.Continue);
                response.ReasonPhrase = ex.InnerException.ToString() + ex.Message.ToString();
                return response;
                //return Request.CreateResponse(HttpStatusCode.InternalServerError,);
            }
        }

       


    }
}