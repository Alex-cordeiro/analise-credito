using AnaliseCredito.Application.Analises.Commands;
using AnaliseCredito.Application.Analises.Queries;
using AnaliseCredito.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnaliseCredito.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnaliseController : ControllerBase
{
    private readonly IAnaliseAppService  _analiseAppService;
    public AnaliseController(IAnaliseAppService analiseAppService)
    {
        _analiseAppService = analiseAppService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Novo")]
    public async Task<IActionResult> CriarAnalise([FromBody] AnaliseCreateCommand command)
    {
        var result = await _analiseAppService.CriateAnalise(command);
        if (!result.Success)
            return BadRequest(result);
        
        return Ok(result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Consultar")]
    public async Task<IActionResult> SolicitarAnalise([FromBody] AnalisePesquisaQuery query)
    {
        var result = await _analiseAppService.ConsultaAnalise(query);
        if (!result.Success)
            return BadRequest(result);
        
        return Ok(result);
    }
}
