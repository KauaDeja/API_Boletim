using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Boletim.Domains;
using API_Boletim.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Boletim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        // chamar o repositório de Aluno
        AlunoRepository repo = new AlunoRepository();


        // GET: api/<AlunoController>
        [HttpGet]
        public List<Aluno> Get()
        {
            return repo.LerTodos();
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public Aluno Get(int id)
        {
            return repo.BuscarPorId(id);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public Aluno Post([FromBody] Aluno a)
        {
            return repo.Cadastrar(a);
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public Aluno Delete(Aluno a)
        {
            return repo.Deletar(a);
        }
    }
}
