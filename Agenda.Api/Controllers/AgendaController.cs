using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Agenda.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        private readonly Context _context;
         
        public AgendaController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Get()
        {
            var agendas = await _context.Agendas.ToListAsync();
            return Ok(agendas);
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<IActionResult> GetAgenda(int id)
        {
            var agenda = await _context.Agendas.FirstOrDefaultAsync(a => a.Id == id);
            return Ok(agenda);
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteAgenda(int id)
        {
            var agenda = await _context.Agendas.FirstOrDefaultAsync(a => a.Id == id);
            if(agenda == null)
                return BadRequest();

            _context.Agendas.Remove(agenda);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]Models.Agenda agenda)
        {
            try
            {
                agenda.FechaCreado = DateTime.Now;
                await _context.Agendas.AddAsync(agenda);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return Ok(agenda);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agenda"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromBody]Models.Agenda agenda)
        {
            var agenda1 = await _context.Agendas.FirstOrDefaultAsync(a => a.Id == agenda.Id);
            agenda1.Titulo = agenda.Titulo;
            agenda1.Descripcion = agenda.Descripcion;

            _context.Agendas.Update(agenda1);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Agenda"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("status")]
        public async Task<IActionResult> CambiarEstatus([FromBody]Models.Agenda Agenda)
        {
            var agenda1 = await _context.Agendas.FirstOrDefaultAsync(a => a.Id == Agenda.Id);
            agenda1.Completado = Agenda.Completado;
            _context.Agendas.Update(agenda1);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}