﻿using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId => Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string Email => _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

        public string Fullname => _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }
}
