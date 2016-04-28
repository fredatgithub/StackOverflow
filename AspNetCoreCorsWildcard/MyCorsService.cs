using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;

public class MyCorsService : CorsService, ICorsService
{
    private ILogger _logger;
    
    public MyCorsService(IOptions<CorsOptions> options, ILogger<MyCorsService> logger)
        : base(options)
    {
        _logger = logger;
        _logger.LogInformation("MyCorsService");
    }

    public override void ApplyResult(
        CorsResult result, HttpResponse response)
    {
        _logger.LogInformation("ApplyResult");
        base.ApplyResult(result, response);
    }

    public override void EvaluateRequest(
        HttpContext context, CorsPolicy policy, CorsResult result)
    {
        _logger.LogInformation("EvaluateRequest");
        base.EvaluateRequest(context, policy, result);
    }

    public override void EvaluatePreflightRequest(
        HttpContext context, CorsPolicy policy, CorsResult result)
    {
        _logger.LogInformation("EvaluatePreflightRequest");
        base.EvaluatePreflightRequest(context, policy, result);
    }
}