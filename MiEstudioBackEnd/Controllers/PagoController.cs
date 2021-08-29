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
    public class PagoController : Controller
    {

        private readonly miestudioDBContext _context;

        public PagoController(miestudioDBContext context)
        {
            _context = context;
        }

        // GET: api/<Pago>
        [HttpGet("GetPagoByIdUsuario/{idUsuario}")]
        public async Task<ActionResult> GetPagoByIdUsuario(int idUsuario)
        {
            try
            { 
               

                var listPagoUsuario = await _context.Pagos.Where(x => x.IdUsuario == idUsuario)
                                                          .Include(y => y.IdTarifaNavigation)
                                                          .ThenInclude(z => z.IdPeriodoNavigation)
                                                          .Select(x => new {
                                                              x.IdPago,
                                                              x.FechaAct,
                                                              x.FechaCobro,
                                                              x.IdTarifaNavigation.NumActividades,
                                                              x.IdTarifaNavigation.IdPeriodoNavigation.NombrePeriodo,
                                                              x.IdTarifaNavigation.Precio,
                                                              x.Pagado,
                                                              x.Metodo,
                                                              x.IdTarifa


                                                              })

                                                          .ToListAsync();
                
                return Ok(listPagoUsuario.OrderByDescending(x=> x.FechaAct));
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

        //[HttpGet("GetLastId")]
        //public async Task<ActionResult<int>> GetLastId()
        //{
        //    try
        //    {
        //        var lastUser = await _context.Usuarios.OrderByDescending(x => x.IdUsuario).FirstOrDefaultAsync();


        //        return Ok(lastUser.IdUsuario);
        //    }

        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
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

        //}

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Pago pago)
        {
            try
            {
                _context.Add(pago);
                await _context.SaveChangesAsync();

                return Ok(pago);

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

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] Pago pago)
        {

            try
            {
                if(id != pago.IdPago)
                {
                    return NotFound();
                }

                _context.Update(pago);
                await _context.SaveChangesAsync();

                return Ok(new { message = "El pago se actualizado con exito" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var pago = await _context.Pagos.FindAsync(id);

                if (pago == null)
                {
                    return NotFound();

                }

                
                _context.Pagos.Remove(pago);
                await _context.SaveChangesAsync();
                return Ok(new { message = "El pago fue eliminado con éxito" });

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
