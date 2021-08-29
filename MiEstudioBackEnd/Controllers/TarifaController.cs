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
    public class TarifaController : Controller
    {

        private readonly miestudioDBContext _context;

        public TarifaController(miestudioDBContext context)
        {
            _context = context;
        }

        // GET: api/<Pago>
        [HttpGet("GetTarifasByIdUsuario/{idUsuario}")]
        public async Task<ActionResult> GetTarifasByIdUsuario(int idUsuario)
        {
            try
            {
                var actividadesUsuario = _context.ActividadesUsuarios.Where(x => x.IdUsuario == idUsuario).ToList();
                


                var listTarifaUsuario = _context.Tarifas.Where(x => x.NumActividades == actividadesUsuario.Count())
                                                        .Include(y=> y.IdPeriodoNavigation)
                                                        .Select(x=>new  { x.IdTarifa, x.NumActividades, x.Precio, x.Activa,x.IdPeriodoNavigation.IdPeriodo,x.IdPeriodoNavigation.NombrePeriodo })
                                                        .ToList();
               
                return Ok(listTarifaUsuario);
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

        // POST api/values
 
        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] Usuario usuario)
        //{

        //    try
        //    {
        //        if (id != usuario.IdUsuario)
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

       
    }
}
