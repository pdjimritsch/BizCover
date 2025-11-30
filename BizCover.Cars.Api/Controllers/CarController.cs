using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using BizCover.Cars.Service.Abstraction;
using BizCover.Cars.Models;
using BizCover.Cars.Models.Abstraction;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Antiforgery;
using BizCover.Cars.Configuration.Abstraction;
using BizCover.Cars.Api.Middleware;
using Microsoft.IdentityModel.Tokens;

namespace BizCover.Cars.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly IHttpContextAccessor? _contextAccessor;

    /// <summary>
    /// 
    /// </summary>
    private readonly IAntiforgery? _antiForgery;

    /// <summary>
    /// 
    /// </summary>
    private readonly IContextConfiguration? _configuration;

    /// <summary>
    /// 
    /// </summary>
    private readonly ICarService? _service;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contextAccessor"></param>
    public CarController(
        IHttpContextAccessor contextAccessor, 
        IAntiforgery? antiForgery, 
        IContextConfiguration? configuration, 
        ICarService? service) : base()
    {
        _contextAccessor = contextAccessor;
        _antiForgery = antiForgery;
        _configuration = configuration;
        _service = service;
    }

    #endregion

    #region Actions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    [Route("/api/[controller]/add")]
    [HttpPost, EnableCors]
    [Produces("application/json")]
    [RequestTimeout(90000 /* override: allow up to 90 seconds for the I/O based operation. */)]
    public async Task<IActionResult> AddAsync([FromBody] Car car)
    {
        IActionResult response = Unauthorized(false);

        if (car != null && _service != null && _antiForgery != null && _configuration != null)
        {
            var tokens = _antiForgery.GetAndStoreTokens(HttpContext);

            if ((tokens != null) && !string.IsNullOrEmpty(tokens.HeaderName) && 
                tokens.HeaderName.Equals(ApiKeyMiddleware.HEADER_KEY_NAME, StringComparison.Ordinal))
            {
                var apiKey = Request.Headers[ApiKeyMiddleware.HEADER_KEY_NAME].FirstOrDefault();

                if (!string.IsNullOrEmpty(apiKey) && 
                    (_configuration != null) && !string.IsNullOrEmpty(_configuration.ApiKey))
                {
                    if (_configuration.ApiKey.Equals(apiKey, StringComparison.Ordinal))
                    {
                        var registered = await _service.AddCarAsync(car);

                        response = registered ? Ok(registered) : Unauthorized(registered);
                    }
                }
            }
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Route("/api/[controller]/list")]
    [HttpGet, EnableCors]
    [Produces("application/json")]
    [RequestTimeout(90000 /* override: allow up to 90 seconds for the I/O based operation */)]
    public async Task<IActionResult> ListAsync()
    {
        List<ICar> cars = [];

        if (_service != null)
        {
            cars = await _service.GetAllCarsAsync();
        }

        return Ok(cars);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    [Route("/api/[controller]/update")]
    [HttpPost, EnableCors]
    [Produces("application/json")]
    [RequestTimeout(90000 /* overide: allow up to 90 seconds for I/O based operation */)]
    public async Task<IActionResult> UpdateAsync([FromBody] Car car)
    {
        IActionResult response = Unauthorized(false);

        if (car != null && _service != null && _antiForgery != null && _configuration != null)
        {
            var tokens = _antiForgery.GetAndStoreTokens(HttpContext);

            if ((tokens != null) && !string.IsNullOrEmpty(tokens.HeaderName) &&
                tokens.HeaderName.Equals(ApiKeyMiddleware.HEADER_KEY_NAME, StringComparison.Ordinal))
            {
                var apiKey = Request.Headers[ApiKeyMiddleware.HEADER_KEY_NAME].FirstOrDefault();

                if (!string.IsNullOrEmpty(apiKey) &&
                    (_configuration != null) && !string.IsNullOrEmpty(_configuration.ApiKey))
                {
                    if (_configuration.ApiKey.Equals(apiKey, StringComparison.Ordinal))
                    {
                        var registered = await _service.UpdateCarAsync(car);

                        response = registered ? Ok(registered) : Unauthorized(registered);
                    }
                }
            }
        }

        return await Task.FromResult(response);
    }

    #endregion
}
