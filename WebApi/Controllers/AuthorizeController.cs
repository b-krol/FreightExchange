﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    
    [Authorize]
    public class AuthorizeController : ControllerBase
    {

    }
}
