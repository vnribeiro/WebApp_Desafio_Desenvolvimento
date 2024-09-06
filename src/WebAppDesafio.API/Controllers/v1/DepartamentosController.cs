using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebAppDesafio.API.Controllers.v1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{apiVersion:apiVersion}/departamentos")]
public class DepartamentosController : MainController
{

}

