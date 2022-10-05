﻿using API.Controllers.Base;
using Domain.Dto.Account;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract.Auth;

namespace API.Controllers.Auth
{
    [Route("api/register")]
    public class RegistrationController : AppBaseController
    {
        private IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<UserModel> RegisterAsync([FromBody] RegistrationDto registrationData,CancellationToken cancellationToken)
        {
            UserModel response = await _registrationService.RegisterAsync(registrationData,cancellationToken);
            return response;
        }
    }
}