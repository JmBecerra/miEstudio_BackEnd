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
    public class UsuarioController : Controller
    {

        private readonly miestudioDBContext _context;

        public UsuarioController(miestudioDBContext context)
        {
            _context = context;
        }

        // GET: api/<Usuario>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var listUsuario = await _context.Usuarios.Where(x=> x.Alta == 1).ToListAsync();

                return Ok(listUsuario);
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

        [HttpGet("GetLastId")]
        public async Task<ActionResult<int>> GetLastId()
        {
            try
            {
                var lastUser = await _context.Usuarios.OrderByDescending(x => x.IdUsuario).FirstOrDefaultAsync();


                return Ok(lastUser.IdUsuario);
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
                var usuario = await _context.Usuarios.Where(x => x.IdUsuario == id)
                                                     .Include(x => x.ActividadesUsuarios)
                                                     .Include(x => x.Pagos)
                                                     .Include(x => x.MedicionUsuarios).FirstOrDefaultAsync();
                if (usuario == null)
                {
                    NotFound();
                }
                return Ok(usuario);
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
        [HttpGet("GetUserByEmail/{email}")]
        public async Task<ActionResult> GetUserByEmail(string email)
        {
            try
            {
                var usuario = await _context.Usuarios.Where(x => x.Email == email).Select(x => new { x.IdUsuario, x.Nombre, x.TipoUsuario, x.Email, x.Password}).FirstOrDefaultAsync();
                                      
                if (usuario == null)
                {
                    NotFound();
                }
                return Ok(usuario);
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
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
               
                return Ok(usuario);

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
        public async Task<IActionResult> Put(int id, [FromBody] Usuario usuario)
        {

            try
            {
                if (id != usuario.IdUsuario)
                {
                    return NotFound();
                }
                _context.Update(usuario);
                await _context.SaveChangesAsync();
              
                return Ok(new { message = "El usuario se actualizado con exito" });

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
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return NotFound();

                }

                usuario.Alta = 0;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
               
                return Ok(new { message = "El usuario fue eliminado con éxito" });

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
