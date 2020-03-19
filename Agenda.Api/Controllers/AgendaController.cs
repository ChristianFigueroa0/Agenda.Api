using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Route("get")]
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
            if(agenda != null)
                _context.Agendas.Remove(agenda);
            else
                return BadRequest();
            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]Models.Agenda agenda)
        {
            try
            {
                await _context.Agendas.AddAsync(agenda);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(agenda);
        }

    }
}