using System.ComponentModel;

using Microsoft.AspNetCore.Authorization;

namespace BizCover.Cars.Api.Extensions;

/// <summary>
/// 
/// </summary>
[ImmutableObject(true)]
public sealed partial class AppServiceRequirement : IAuthorizationRequirement
{
}
