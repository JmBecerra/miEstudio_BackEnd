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
    public class MedicionController : Controller
    {

        private readonly miestudioDBContext _context;

        public MedicionController(miestudioDBContext context)
        {
            _context = context;
        }

        // GET: api/<Medicion>
        //[HttpGet]
        //public async Task<ActionResult> Get()
        //{
        //    try
        //    {
        //        var listUsuario = await _context.Usuarios.Where(x=> x.Alta == 1).ToListAsync();

        //        return Ok(listUsuario);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    finally
        //    {
        //        _context.Dispose();
        //    }
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult> Get(int id)
        //{
        //    try
        //    {
        //        var usuario = await _context.Usuarios.Where(x => x.IdUsuario == id)
        //                                             .Include(x => x.ActividadesUsuarios)
        //                                             .Include(x => x.Pagos)
        //                                             .Include(x => x.MedicionUsuarios).FirstOrDefaultAsync();
        //        if (usuario == null)
        //        {
        //            NotFound();
        //        }
        //        return Ok(usuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    finally
        //    {
        //        _context.Dispose();
        //    }

        //}

        // GET: api/<Pago>
        [HttpGet("GetMedicionesUsuario/{idUsuario}")]
        public async Task<ActionResult> GetMedicionesUsuario(int idUsuario)
        {
            try
            {


                var listMedicionUsuario = await _context.MedicionUsuarios.Where(x => x.IdUsuario == idUsuario)
                                                          .Include(y => y.IdMedicionNavigation)
                                                          .Select(x => new {
                                                              x.IdMedicionUsuario,
                                                              x.IdMedicion,
                                                              x.Fecha,
                                                              x.IdMedicionNavigation.Peso,
                                                              x.IdMedicionNavigation.Altura,
                                                              x.IdMedicionNavigation.Grasa,
                                                              x.IdMedicionNavigation.Musculo,
                                                              x.IdMedicionNavigation.Agua,
                                                              x.IdMedicionNavigation.Abdomen,
                                                              x.IdMedicionNavigation.Cintura,
                                                           

                                                          })

                                                          .ToListAsync();

                return Ok(listMedicionUsuario.OrderByDescending(x => x.Fecha));
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

        // POST api/values
        [HttpPost("{id}")]
        public async Task<ActionResult> Post(int id, [FromBody] Medicion medicion)
        {
            try
            {
                _context.Add(medicion);
                await _context.SaveChangesAsync();

                var nuevaMedicionUsuario = new MedicionUsuario();
                nuevaMedicionUsuario.IdMedicion = medicion.IdMedicion;
                nuevaMedicionUsuario.IdUsuario = id;

                _context.MedicionUsuarios.Add(nuevaMedicionUsuario);
                await _context.SaveChangesAsync();

                return Ok(medicion);

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

        //PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Medicion medicion)
        {

            try
            {
                if (id != medicion.IdMedicion)
                {
                    return NotFound();
                }
                _context.Update(medicion);
                await _context.SaveChangesAsync();

                return Ok(new { message = "La medicón se ha actualizado con exito" });

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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var medicion = await _context.Medicions.FindAsync(id);

                if (medicion == null)
                {
                    return NotFound();

                }
               var medicionUsuario =  await _context.MedicionUsuarios.Where(x => x.IdMedicion == medicion.IdMedicion).FirstOrDefaultAsync();

                if(medicionUsuario == null)
                {
                    return NotFound();
                }
                _context.MedicionUsuarios.Remove(medicionUsuario);
                
                _context.Medicions.Remove(medicion);

                await _context.SaveChangesAsync();

                return Ok(new { message = "La medición fue eliminada con éxito" });

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
