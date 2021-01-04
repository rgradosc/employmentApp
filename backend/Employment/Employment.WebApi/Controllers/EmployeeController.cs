using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;

namespace Employment.WebApi.Controllers
{
    using Repository;
    using Models;
    using System.Web;
    using System.Web.Http.Description;

    /// <summary>
    /// 
    /// </summary>
    public class EmployeeController : ApiController
    {
        private EmployeeRepository repository;
        private MessageResponse response;
        private const int zero = 0;
        
        /// <summary>
        /// 
        /// </summary>
        public EmployeeController()
        {
            repository = new EmployeeRepository();
            response = new MessageResponse();
        }
        
        /// <summary>
        /// Elimina un objeto de la base de datos.
        /// </summary>
        /// <param name="id">Id del objeto.</param>
        /// <response code="200">OK. El objeto fue eliminado.</response>
        /// <response code="400">BadRequest. No se elimino el objeto. Formato de los datos incorrecto.</response>
        /// <response code="404">NotFound. El objeto a eliminar no existe.</response>
        /// <response code="500">InternalServerError. Error interno del servidor.</response>
        /// <returns>Objeto con la información del estado y datos de la solicitud.</returns>
        [HttpDelete]
        [ResponseType(typeof(MessageResponse))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Employee department = repository.GetById(id);

                if (department == null)
                {
                    response.MessageError = new MessageError()
                    {
                        Code = "E002",
                        Description = $"No se encontró al empleado con el id: {id}",
                    };
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }

                int result = repository.Delete(id);

                if (result != zero)
                {
                    response.MessageOk = new MessageOk()
                    {
                        Code = "C003",
                        Description = "El empleado se elimino correctamente.",
                    };
                    response.Data = department;

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                response.MessageError = new MessageError()
                {
                    Code = "E002",
                    Description = "Error al eliminar el empleado",
                };
                response.Data = department;

                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            catch (Exception ex)
            {
                response.MessageError = new MessageError()
                {
                    Code = "E001",
                    Description = "La petición se devolvio con errores.",
                };
                response.Data = ex;

                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

        /// <summary>
        /// Actualiza un objeto de la base de datos.
        /// </summary>
        /// <remarks>
        /// El método devuelve información del objeto creado y de los errores ocurridos. Además, 
        /// devuelve el código y descripción del estado de la petición.
        /// </remarks>
        /// <param name="model">Objeto a actualizar.</param>
        /// <response code="200">OK. El objeto se actualizó correctamente.</response>
        /// <response code="400">BadRequest. No se actualizó el objeto. Formato de los datos incorrectos.</response>
        /// <response code="404">NotFound. El objeto a actualizar no existe.</response>
        /// <response code="500">InternalServerError. Error producido en el servidor.</response>
        /// <returns>Objeto con la información del estado y datos de la solicitud.</returns>
        [HttpPut]
        [ResponseType(typeof(MessageResponse))]
        public HttpResponseMessage Put(Employee model)
        {
            try
            {
                Employee department = repository.GetById(model.Id);

                if (department == null)
                {
                    response.MessageError = new MessageError()
                    {
                        Code = "E002",
                        Description = $"No se encontró el empleado con el id: {model.Id}",
                    };

                    response.Data = model;
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }

                int result = repository.Update(model);
                if (result != zero)
                {
                    response.MessageOk = new MessageOk()
                    {
                        Code = "C002",
                        Description = "El empleado se actualizo correctamente.",
                    };

                    var data = new
                    {
                        Update = model,
                        Old = department,
                    };

                    response.Data = data;

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                response.MessageError = new MessageError()
                {
                    Code = "C001",
                    Description = "Error al actualizar el empleado.",
                };
                response.Data = model;

                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            catch (Exception ex)
            {
                response.MessageError = new MessageError()
                {
                    Code = "E001",
                    Description = "La petición se devolvio con errores.",
                };
                response.Data = ex;

                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

        /// <summary>
        /// Crea un objeto en la base de datos.
        /// </summary>
        /// <remarks>
        /// El método devuelve información del objeto creado y de los errores ocurridos. Además, 
        /// devuelve el código y descripción del estado de la petición.
        /// </remarks>
        /// <param name="model">Contiene los datos del objeto a crear.</param>
        /// <response code="201">Ok. El objeto fue creaado correctamente.</response>
        /// <response code="400">BadRequest. No se creo el objeto. Formarto de los datos incorrectos.</response>
        /// <response code="500">InternalServerError. Error producido en el servidor.</response>
        /// <returns>Objeto con la información del estado y datos de la solicitud.</returns>
        [HttpPost]
        [ResponseType(typeof(MessageResponse))]
        public HttpResponseMessage Post(Employee model)
        {
            try
            {
                int id = repository.Create(model);
                if (id != zero)
                {
                    model.Id = id;
                    response.MessageOk = new MessageOk()
                    {
                        Code = "C001",
                        Description = "El empleado se creo correctamente.",
                    };
                    response.Data = model;

                    return Request.CreateResponse(HttpStatusCode.Created, response);
                }

                response.MessageError = new MessageError()
                {
                    Code = "C001",
                    Description = "Error al crear el empleado.",
                };
                response.Data = model;

                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            catch (Exception ex)
            {
                response.MessageError = new MessageError()
                {
                    Code = "E001",
                    Description = "La petición se devolvio con errores.",
                };
                response.Data = ex;

                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

        /// <summary>
        /// Obtiene un objeto por su Id de la base de datos.
        /// </summary>
        /// <remarks>
        /// Los datos del departamento se encuentran dentro de la propiedad Data del objeto devuelto. 
        /// El código y descripción del estado también son devueltos.
        /// </remarks>
        /// <response code="200">Ok. Información devuelta correctamente. </response>
        /// <response code="404">NotFound. El objeto solicitado no existe.</response>
        /// <response code="500">InternalServerError. Error producido en el servidor.</response>
        /// <param name="id">Id del objeto.</param>
        /// <returns>Objeto con la información del estado y datos de la solicitud.</returns>
        [HttpGet]
        [ResponseType(typeof(MessageResponse))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Employee department = repository.GetById(id);

                if (department == null)
                {
                    response.MessageOk = new MessageOk()
                    {
                        Code = "S001",
                        Description = $"No se encontró el empleado con el id: {id}",
                    };

                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }

                response.Data = department;
                response.MessageOk = new MessageOk()
                {
                    Code = "S001",
                    Description = $"La consulta devolvío un resultado."
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response = new MessageResponse()
                {
                    Data = ex.Message,
                    MessageError = new MessageError()
                    {
                        Code = "E001",
                        Description = "La petición se devolvio con errores",
                    },
                    MessageOk = null,
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

        /// <summary>
        /// Obtiene una lista de objetos de la base de datos.
        /// </summary>
        /// <remarks>
        /// Los datos de los departamentos se encuentran dentro de la propiedad "data" del objeto devuelto.
        /// El código y descripción del estado también son devueltos.
        /// </remarks>
        /// <response code="200">Ok. Información devuelta correctamente. </response>
        /// <response code="500">InternalServerError. Error producido en el servidor.</response>
        /// <returns>Objeto con la información del estado y datos de la solicitud.</returns>
        [HttpGet]
        [ResponseType(typeof(MessageResponse))]
        public HttpResponseMessage Get()
        {
            try
            {
                ICollection<Employee> model = repository.GetAll();
                response = new MessageResponse()
                {
                    Data = model,
                    MessageError = null,
                    MessageOk = new MessageOk()
                    {
                        Code = "S001",
                        Description = $"La lista devolvío { model.Count } registros."
                    }
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response.Data = ex.Message;
                response.MessageError = new MessageError()
                {
                    Code = "E001",
                    Description = "La petición se devolvio con errores",
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

        /// <summary>
        /// Carga al servidor el avatar del empleado.
        /// </summary>
        /// <response code="201">Created. Avatar cargado correctamente.</response>
        /// <response code="500">InternalServerError. Error producido en el servidor.</response>
        /// <returns>Objeto con la información del estado y datos de la solicitud.</returns>
        [HttpPost]
        [Route("api/employee/upload")]
        [ResponseType(typeof(MessageResponse))]
        public HttpResponseMessage Upload()
        {
            try
            {
                HttpRequest httpRequest = HttpContext.Current.Request;
                HttpPostedFile postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                string physicalPath = HttpContext.Current.Server.MapPath($"~/Content/Employee/{fileName}");
                postedFile.SaveAs(physicalPath);

                response.Data = fileName;
                response.MessageOk = new MessageOk()
                {
                    Code = "I001",
                    Description = "La imagen se cargo correctamente."
                };

                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.MessageError = new MessageError()
                {
                    Code = "E001",
                    Description = "Error al cargar la imagen.",
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

    }
}
