using System;
using System.Threading.Tasks;
using BeckhoffMiddleware.Models;
using BeckhoffMiddleware.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BeckhoffMiddleware.Controllers;

[ApiController]
[Route("api/plc")]
public class PlcController : ControllerBase
{
    private readonly IPlcClient _plcClient;
    private readonly ILogger<PlcController> _logger;

    public PlcController(IPlcClient plcClient, ILogger<PlcController> logger)
    {
        _plcClient = plcClient;
        _logger = logger;
    }

    [HttpGet("variables/{symbol}")]
    public async Task<ActionResult<PlcReadResponse>> ReadVariable(string symbol)
    {
        try
        {
            var value = await _plcClient.ReadAsync<object>(symbol);
            return Ok(new PlcReadResponse
            {
                Symbol = symbol,
                Value = value
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to read symbol {Symbol}", symbol);
            return NotFound(new { message = $"Failed to read symbol '{symbol}': {ex.Message}" });
        }
    }

    [HttpPost("variables/{symbol}")]
    public async Task<IActionResult> WriteVariable(string symbol, [FromBody] PlcWriteRequest request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Request body is required." });
        }

        try
        {
            await _plcClient.WriteAsync(symbol, request.Value);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write symbol {Symbol}", symbol);
            return BadRequest(new { message = $"Failed to write symbol '{symbol}': {ex.Message}" });
        }
    }

    [HttpPost("snapshot")]
    public async Task<ActionResult<PlcSnapshotResponse>> ReadSnapshot([FromBody] PlcSnapshotRequest request)
    {
        if (request == null || request.Symbols.Count == 0)
        {
            return BadRequest(new { message = "At least one symbol must be specified." });
        }

        var values = await _plcClient.ReadSnapshotAsync(request.Symbols);
        var response = new PlcSnapshotResponse
        {
            Values = values
        };

        return Ok(response);
    }
}
