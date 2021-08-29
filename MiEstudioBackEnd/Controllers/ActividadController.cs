using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiEstudioBackEnd.Entity;
using MiEstudioBackEnd.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiEstudioBackEnd.Controllers
{
    [Route("api/[controller]")]
    public class ActividadController : Controller
    {

        private readonly miestudioDBContext _context;

        public ActividadController(miestudioDBContext context)
        {
            _context = context;
        }

        // GET: api/<Actividad>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var listActividad = await (from acts in _context.Actividads 
                                          join tipo in _context.Tipos on acts.Tipo equals tipo.IdTipo
                                          join nivel in _context.Nivels on acts.IdNivel equals nivel.IdNivel
                                          select new
                                          {
                                              idActividad = acts.IdActividad,
                                              tipo = tipo.Nombre,
                                              nivel = nivel.Nombre,
                                              dia = acts.Dia,
                                              horario = acts.Horario,
                                              ocupacion = acts.Ocupacion
                                          }).ToListAsync();
                _context.Dispose();

                return Ok(listActividad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            finally
            {
                _context.Dispose();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var actividad = await _context.Actividads.Where(x => x.IdActividad == id).FirstOrDefaultAsync();
                if (actividad == null)
                {
                    NotFound();
                }
               
                return Ok(actividad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                _context.Dispose();
            }

        }

        // GET api/values/5
        [HttpGet("GetActividadUsuario/{idUsuario}")]
        public async Task<ActionResult> GetActividadUsuario(int idUsuario)
        {
            try
            {

                var actividadUsuario = await (from actUser in _context.ActividadesUsuarios
                                              join acts in _context.Actividads on  actUser.IdActividad equals acts.IdActividad
                                              join tipo in _context.Tipos on acts.Tipo equals tipo.IdTipo
                                              join nivel in _context.Nivels on acts.IdNivel equals nivel.IdNivel
                                              where actUser.IdUsuario == idUsuario
                                              select new
                                              {
                                                  idActUser= actUser.IdActsUsuario,
                                                  idActividad = acts.IdActividad,
                                                  tipo = tipo.Nombre,
                                                  nivel = nivel.Nombre,
                                                  dia = acts.Dia,
                                                  horario = acts.Horario,
                                                  ocupacion = acts.Ocupacion
                                              }).ToListAsync();

                if (actividadUsuario == null)
                {
                    NotFound();
                }
                return Ok(actividadUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                _context.Dispose();
            }

        }

        // GET api/values/5
        [HttpGet("GetNoActividadUsuario/{idUsuario}")]
        public async Task<ActionResult> GetNoActividadUsuario(int idUsuario)
        {
            try
            {
                var listActividades = (from acts in _context.Actividads
                                     join tipo in _context.Tipos on acts.Tipo equals tipo.IdTipo
                                     join nivel in _context.Nivels on acts.IdNivel equals nivel.IdNivel
                                     select new
                                     {
                                         IdActividad = acts.IdActividad,
                                         tipo = tipo.Nombre,
                                         nivel = nivel.Nombre,
                                         dia = acts.Dia,
                                         horario = acts.Horario,
                                         ocupacion = acts.Ocupacion
                                     }).ToList();


                var listActivdadesCliente =(from acts in _context.Actividads
                                              join actUser in _context.ActividadesUsuarios on acts.IdActividad equals actUser.IdActividad into actsUSer
                                              from actUser in actsUSer.DefaultIfEmpty()
                                              join tipo in _context.Tipos on acts.Tipo equals tipo.IdTipo
                                              join nivel in _context.Nivels on acts.IdNivel equals nivel.IdNivel
                                              where actUser.IdUsuario == idUsuario
                                              select new
                                              {
                                                  IdActividad = acts.IdActividad,
                                                  tipo = tipo.Nombre,
                                                  nivel = nivel.Nombre,
                                                  dia = acts.Dia,
                                                  horario = acts.Horario,
                                                  ocupacion = acts.Ocupacion
                                              }).Distinct().ToList();

                
                foreach (var actCliente in listActivdadesCliente)
                {
                    var find = listActividades.Single(x => x.IdActividad == actCliente.IdActividad);
                    if (find != null)
                    {
                        listActividades.Remove(find);
                    }
                }


             
                return Ok(listActividades); //TODO: Hacer que la lista ordene por dia de la semana
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                _context.Dispose();
            }

        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Actividad actividad)
        {
            try
            {
                _context.Add(actividad);
                await _context.SaveChangesAsync();
              
                return Ok(actividad);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                _context.Dispose();
            }
        }

        // POST api/values
        [HttpPost("CreateActividadUsuario/{idUsuario}")]
        public async Task<ActionResult> CreateActividadUsuario(int idUsuario, [FromBody]int idActividad)
        {
            try
            {
                var actividad = await _context.Actividads.FindAsync(idActividad);
                if(actividad == null){
                    return NotFound("no se ha encontrado la actividad");
                }

                var usuario = await _context.Usuarios.FindAsync(idUsuario);
                if (usuario == null)
                {
                    return NotFound("no se ha encontrado el usuario");
                }

                ActividadesUsuario actividadUsuario = new ActividadesUsuario();
                actividadUsuario.IdActividad = actividad.IdActividad;
                actividadUsuario.IdUsuario = usuario.IdUsuario;
                _context.ActividadesUsuarios.Add(actividadUsuario);

                await _context.SaveChangesAsync();
                
                return Ok(actividadUsuario);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                _context.Dispose();
            }
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] Actividad actividad)
        //{

        //    try
        //    {
        //        if(id != usuario.IdUsuario)
        //        {
        //            return NotFound();
        //        }
        //        _context.Update(usuario);
        //        await _context.SaveChangesAsync();
        //        return Ok(new { message = "El usuario se actualizado con exito" });

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        // DELETE api/values/5
        [HttpDelete("DeleteActividadUsuario/{idActUser}")]
        public async Task<IActionResult> Delete(int idActUser)
        {

            try
            {
                var actividadUsuario = await _context.ActividadesUsuarios.FindAsync(idActUser);

                if (actividadUsuario == null)
                {
                    return NotFound();

                }
                _context.ActividadesUsuarios.Remove(actividadUsuario);
                await _context.SaveChangesAsync();
               
                return Ok(new { message = "La actividad fue eliminado con éxito" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                _context.Dispose();
            }
        }
    }
}
