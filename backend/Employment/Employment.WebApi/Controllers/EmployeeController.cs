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

    public class EmployeeController : ApiController
    {
        private EmployeeRepository repository;
        private MessageResponse response;
        private const int zero = 0;

        public EmployeeController()
        {
            repository = new EmployeeRepository();
            response = new MessageResponse();
        }

        [HttpDelete]
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

        [HttpPut]
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

        [HttpPost]
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

        [HttpGet]
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

                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpGet]
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

                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpPost]
        [Route("api/employee/upload")]
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

                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

    }
}
