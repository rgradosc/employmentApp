using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;

namespace Employment.WebApi.Controllers
{
    using Repository;
    using Models;

    public class DepartmentController : ApiController
    {
        private DepartmentRepository repository;
        private MessageResponse response;
        private const int zero = 0;

        public DepartmentController()
        {
            repository = new DepartmentRepository();
            response = new MessageResponse();
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Department department = repository.GetById(id);

                if (department == null)
                {
                    response.MessageError = new MessageError()
                    {
                        Code = "E002",
                        Description = $"No se encontró el departamento con el id: {id}",
                    };
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }

                int result = repository.Delete(id);

                if (result != zero)
                {
                    response.MessageOk = new MessageOk()
                    {
                        Code = "C003",
                        Description = "El departamento se elimino correctamente.",
                    };
                    response.Data = department;

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                response.MessageError = new MessageError()
                {
                    Code = "E002",
                    Description = "Error al eliminar el departamento",
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
        public HttpResponseMessage Put(Department model)
        {
            try
            {
                Department department = repository.GetById(model.Id);

                if (department == null)
                {
                    response.MessageError = new MessageError()
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
                    response.MessageOk = new MessageOk()
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

                response.MessageError = new MessageError()
                {
                    Code = "C001",
                    Description = "Error al actualizar el departamento.",
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
        public HttpResponseMessage Post(Department model)
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
                        Description = "El departamento se creo correctamente.",
                    };
                    response.Data = model;

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                response.MessageError = new MessageError()
                {
                    Code = "C001",
                    Description = "Error al crear el departamento.",
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
                Department department = repository.GetById(id);

                if (department == null)
                {
                    response.MessageOk = new MessageOk()
                    {
                        Code = "S001",
                        Description = $"No se encontró el departamento con el id: {id}",
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
                ICollection<Department> model = repository.GetAll();
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
    }
}
