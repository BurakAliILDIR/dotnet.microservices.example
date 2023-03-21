using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.Service
{
    public class SharedIdentityService : ISharedIdentityService
    {
        // TODO : Önemli.
        private IHttpContextAccessor _httpContextAccessor;

        // public string GetUserId => _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}