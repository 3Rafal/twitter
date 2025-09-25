using Microsoft.AspNetCore.Mvc;
using TwitterClone.Api.Data;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;
    private readonly AppDbContext _dbContext;

    public HealthController(ILogger<HealthController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"
        });
    }

    [HttpGet("database")]
    public async Task<IActionResult> GetDatabaseHealth()
    {
        try
        {
            // Try to connect to the database
            var canConnect = await _dbContext.Database.CanConnectAsync();

            if (canConnect)
            {
                return Ok(new
                {
                    Status = "Healthy",
                    Database = "Connected",
                    Timestamp = DateTime.UtcNow
                });
            }
            else
            {
                return StatusCode(503, new
                {
                    Status = "Unhealthy",
                    Database = "Disconnected",
                    Timestamp = DateTime.UtcNow
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database health check failed");
            return StatusCode(503, new
            {
                Status = "Unhealthy",
                Database = "Error",
                Error = ex.Message,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}