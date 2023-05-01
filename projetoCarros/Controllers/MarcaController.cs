using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projetoCarros.DTO;
using projetoCarros.Models;

namespace projetoCarros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly LocacaoContext _locacaoContext;

    public MarcaController(LocacaoContext locacaoContext)
    {
        _locacaoContext = locacaoContext;
    }

    [HttpPost]
    public ActionResult Post([FromBody] MarcaDTO marcaDTO)
    {
        MarcaModel marcaModel = new();

        marcaModel.Nome = marcaDTO.Nome;

        _locacaoContext.Add(marcaModel);
        _locacaoContext.SaveChanges();

        return Ok("Marca salva com sucesso!");
    }

    [HttpPut]
    public ActionResult Put([FromBody] MarcaDTO marcaDto)
    {
        MarcaModel marcaModel = _locacaoContext.Marca.Find(marcaDto.Codigo);

        if (marcaModel != null)
        {
            marcaModel.Nome = marcaDto.Nome;

            _locacaoContext.Attach(marcaModel);
            _locacaoContext.SaveChanges();
            return Ok("Marca alterada com sucesso!");
        }

        return BadRequest("Marca não encontrada!");
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        MarcaModel marcaModel = _locacaoContext.Marca.Find(id);

        if (marcaModel != null)
        {
            _locacaoContext.Remove(marcaModel);
            _locacaoContext.SaveChanges();
            return Ok("Marca removida com sucesso!");
        }

        return BadRequest("Marca não encontrada!");
    }

    [HttpGet]
    public ActionResult<List<MarcaDTO>> GetTodas()
    {
        // todos os registros de Marca que se encontram na _locacaoContext.Marca
        var listaMarcaModel = _locacaoContext.Marca;

        // onde vai armazenar os registros para retornar em forma de lista
        List<MarcaDTO> listaMarcaDtos = new();

        foreach (var item in listaMarcaModel)
        {
            MarcaDTO marcaDto = new();

            marcaDto.Nome = item.Nome;
            marcaDto.Codigo = item.Id;

            listaMarcaDtos.Add(marcaDto);
        }

        return Ok(listaMarcaDtos);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult GetPorId([FromRoute] int id)
    {
        var marcaModel = _locacaoContext.Marca.Find(id);
        MarcaDTO marcaDto = new();

        if (marcaModel == null)
        {
            return BadRequest("Marca não encontrada!");
        }

        marcaDto.Codigo = marcaModel.Id;
        marcaDto.Nome = marcaModel.Nome;

        return Ok(marcaDto);
    }
}

}
