using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//base

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {

            try
            {
                if (repository.Insert(entity) > 0)
                {
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        //data = personRepository.Get(person.NIK),
                        message = "Data berhasil Di Tambahkan"
                    });
                }
                else if (repository.Insert(entity) == 0)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Id tidak boleh kosong"
                    });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return BadRequest(new
            {
                status = HttpStatusCode.BadRequest,
                message = "Data Sudah Ada"
            });


        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            if (repository.Get() != null)
            {

                /* return Ok(new
                 {
                     status = HttpStatusCode.OK,
                     data = repository.Get(),
                     message = "Data berhasil Di tampilkan"
                 });*/
                return Ok(repository.Get());
            }
            else
            {
                return NotFound("Data Tidak Ada");
            }
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            if (repository.Get(key) != null)
            {

                //return Ok(personRepository.Get(NIK));
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = repository.Get(key),
                    message = "Data berhasil Di tampilkan"
                });
            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                message = "Data dengan Id tersebut Tidak Ditemukan"
                //return Ok(personRepository.Get(NIK));
            });
        }

        [HttpPut]
        public ActionResult Update(Entity entity, Key key)
        {
            try
            {
                if (repository.Update(entity) != 0)
                {
                    //return Ok("Data Berhasil di Update");
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        //data = repository.Get(entity.key),
                        message = "Data berhasil Di Update"
                    });
                }
                else {
                    return BadRequest("Data Gagal diupdate");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                message = "Data dengan Id tersebut Tidak Ditemukan"

            });
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            if (repository.Get(key) != null)
            {
                //personRepository.Delete(NIK);
                //return Ok("Data Berhasil Dihapus");  
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = repository.Get(key),
                    deletedata = repository.Delete(key),
                    message = "Data berhasil Di Hapus"
                });
            }
            else
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Data dengan Id tersebut Tidak Ditemukan"
                });
            }

        }

    }
}
