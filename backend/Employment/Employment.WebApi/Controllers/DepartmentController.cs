using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace Employment.WebApi.Controllers
{
    using Repository;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class DepartmentController : ApiController
    {
        private DepartmentRepository repository;
        private Response response;
        private const int zero = 0;

        /// <summary>
        /// 
        /// </summary>
        public DepartmentController()
        {
            repository = new DepartmentRepository();
            response = new Response();
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
        [ResponseType(typeof(Response))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Department department = repository.GetById(id);

                if (department == null)
                {
                    response.Message = new Message()
                    {
                        Code = "E002",
                        Description = $"No se encontró el departamento con el id: {id}",
                    };
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }

                int result = repository.Delete(id);

                if (result != zero)
                {
                    response.Message = new Message()
                    {
                        Code = "C003",
                        Description = "El departamento se elimino correctamente.",
                    };
                    response.Data = department;

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                response.Message = new Message()
                {
                    Code = "E002",
                    Description = "Error al eliminar el departamento",
                };
                response.Data = department;

                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            catch (Exception ex)
            {
                response.Message = new Message()
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
        [ResponseType(typeof(Response))]
        public HttpResponseMessage Put(Department model)
        {
            try
            {
                Department department = repository.GetById(model.Id);

                if (department == null)
                {
                    response.Message = new Message()
                    {
                        Code = "E002",
                        Description = $"No se encontró el departamento con el id: {model.Id}",
                    };

                    response.Data = model;
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }

                int result = repository.Update(model);
                if (result != zero)
                {
                    response.Message = new Message()
                    {
                        Code = "C002",
                        Description = "El departamento se actualizo correctamente.",
                    };

                    var data = new
                    {
                        Update = model,
                        Old = department,
                    };

                    response.Data = data;

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                response.Message = new Message()
                {
                    Code = "C001",
                    Description = "Error al actualizar el departamento.",
                };
                response.Data = model;

                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            catch (Exception ex)
            {
                response.Message = new Message()
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
        [ResponseType(typeof(Response))]
        public HttpResponseMessage Post(Department model)
        {
            try
            {
                int id = repository.Create(model);
                if (id != zero)
                {
                    model.Id = id;
                    response.Message = new Message()
                    {
                        Code = "C001",
                        Description = "El departamento se creo correctamente.",
                    };
                    response.Data = model;

                    return Request.CreateResponse(HttpStatusCode.Created, response);
                }

                response.Message = new Message()
                {
                    Code = "C001",
                    Description = "Error al crear el departamento.",
                };
                response.Data = model;

                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            catch (Exception ex)
            {
                response.Message = new Message()
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
        [ResponseType(typeof(Response))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Department department = repository.GetById(id);

                if (department == null)
                {
                    response.Message = new Message()
                    {
                        Code = "S001",
                        Description = $"No se encontró el departamento con el id: {id}",
                    };

                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }

                response.Data = department;
                response.Message = new Message()
                {
                    Code = "S001",
                    Description = $"La consulta devolvío un resultado."
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Data = ex.Message,
                    Message = new Message()
                    {
                        Code = "E001",
                        Description = "La petición se devolvio con errores",
                    },
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
        [ResponseType(typeof(Response))]
        public HttpResponseMessage Get()
        {
            try
            {
                ICollection<Department> model = repository.GetAll();
                response = new Response()
                {
                    Data = model,
                    Message = new Message()
                    {
                        Code = "S001",
                        Description = $"La lista devolvío { model.Count } registros."
                    }
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Data = ex.Message,
                    Message = new Message()
                    {
                        Code = "E001",
                        Description = "La petición se devolvio con errores",
                    },
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
