using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public TarefaController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        //Criar
        [HttpPost]
        public async Task<ActionResult<Tarefa>> AddTarefa(Tarefa novaTarefa)
        {
            if (novaTarefa != null)
            {
                appDbContext.Tarefas.Add(novaTarefa);
                await appDbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTarefa), new { id = novaTarefa.Id }, novaTarefa);
            }
            return BadRequest();
        }

        //Lê todas as tarefas
        [HttpGet]
        public async Task<ActionResult<List<Tarefa>>> GetTarefas()
        {
            var tarefas = await appDbContext.Tarefas.ToListAsync();
            return Ok(tarefas);
        }

        //Lê a terfa de acordo com o Id informado
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await appDbContext.Tarefas.FirstOrDefaultAsync(e => e.Id == id);
            if (tarefa != null)
            {
                return Ok(tarefa);
            }
            return NotFound("Tarefa não encontrada!");
        }

        //Edita a tarefa que foi previamente cadastrada
        [HttpPut("{id}")]
        public async Task<ActionResult<Tarefa>> AtualizaTarefa(int id, Tarefa atualizaTarefa)
        {
            var tarefa = await appDbContext.Tarefas.FirstOrDefaultAsync(e => e.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            tarefa.Nome = atualizaTarefa.Nome;
            tarefa.Descricao = atualizaTarefa.Descricao;
            tarefa.Concluida = atualizaTarefa.Concluida;
            tarefa.DataConclusao = atualizaTarefa.DataConclusao;

            await appDbContext.SaveChangesAsync();

            return Ok(tarefa);
        }


        //Deletar tarefa com um ID específico
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Tarefa>>> DeletaTarefa(int id)
        {
            var tarefa = await appDbContext.Tarefas.FirstOrDefaultAsync(e => e.Id == id);
            if (tarefa != null)
            {
                appDbContext.Tarefas.Remove(tarefa);
                await appDbContext.SaveChangesAsync();

                var tarefas = await appDbContext.Tarefas.ToListAsync();
                return Ok(tarefas);
            }
            return NotFound();
        }
    }
}
