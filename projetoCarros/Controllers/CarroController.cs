using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoCarros.DTO;
using projetoCarros.Models;

namespace projetoCarros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarroController : ControllerBase
    {
        private readonly LocacaoContext _locacaoContext;

    public CarroController(LocacaoContext locacaoContext)
    {
        _locacaoContext = locacaoContext;
    }

    [HttpPost]
    public ActionResult Post([FromBody] CarroDTO carroDto)
    {
        CarroModel carroModel = new();
        MarcaModel marcaModel = _locacaoContext.Marca.Find(carroDto.CodigoMarca);

        if (marcaModel == null)
        {
            return NotFound("Marca não encontrada!");
        }

        carroModel.Id = carroDto.Codigo;
        carroModel.Nome = carroDto.Nome;
        carroModel.MarcaId = marcaModel.Id;

        _locacaoContext.Add(carroModel);
        _locacaoContext.SaveChanges();

        return Ok("Carro salvo com sucesso!");
    }

    [HttpPut]
    public ActionResult Put([FromBody] CarroDTO carroDto)
    {
        CarroModel carroModel = _locacaoContext.Carro.Find(carroDto.Codigo);
        MarcaModel marcaModel = _locacaoContext.Marca.Find(carroDto.CodigoMarca);

        if (marcaModel == null)
        {
            return NotFound("Marca não encontrada!");
        }

        if (carroModel == null)
        {
            return NotFound("Carro não encontrado!");
        }

        carroModel.Id = carroDto.Codigo;
        carroModel.Nome = carroDto.Nome;
        carroModel.MarcaId = marcaModel.Id;

        _locacaoContext.Attach(carroModel);
        _locacaoContext.SaveChanges();

        return Ok("Carro atualizado com sucesso!");
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        CarroModel carroModel = _locacaoContext.Carro.Find(id);

        if (carroModel != null)
        {
            _locacaoContext.Remove(carroModel);
            _locacaoContext.SaveChanges();

            return Ok("Carro removido com sucesso!");
        }

        return BadRequest("Carro não existe.");
    }

    [HttpGet]
    public ActionResult<List<CarroDTO>> GetTodos()
    {
        // todos os registros de Carro que se encontram na _locacaoContext.Carro e _locacaoContext.Marca
        var listaCarroModel = _locacaoContext.Carro.Include(c => c.Marca);

        // onde vai armazenar os registros para retornar em forma de lista
        List<CarroDTO> listaCarrosDto = new();

        foreach (var carro in listaCarroModel)
        {
            var carroDto = new CarroDTO();

            carroDto.Codigo = carro.Id;
            carroDto.Nome = carro.Nome;
            carroDto.CodigoMarca = carro.MarcaId;
            listaCarrosDto.Add(carroDto);
        }

        return Ok(listaCarrosDto);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult GetPorId([FromRoute]int id)
    {

        var carroModel = _locacaoContext.Carro.Include(c => c.Marca).FirstOrDefault(x => x.Id == id);

        CarroDTO carroDto = new();
        
        if (carroModel.Id == null)
        {
            BadRequest("Carro não encontrado!");
        }

        carroDto.Codigo = carroModel.Id;
        carroDto.Nome = carroModel.Nome;
        carroDto.CodigoMarca = carroModel.MarcaId;

        return Ok(carroDto);
    }
    }
}