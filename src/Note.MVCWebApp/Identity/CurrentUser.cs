using Microsoft.AspNetCore.Http;
using Note.Core.Identity;
using System.Security.Claims;

namespace Note.MVCWebApp.Identity
{
    public class CurrentUser : ICurrentUser
    {
        #region Props

        protected readonly IHttpContextAccessor _httpContextAccessor;
        
        private ClaimsPrincipal _currentUserClaims;

        private ClaimsPrincipal Claims
        {
            get
            {
                if (_currentUserClaims == null)
                {
                    _currentUserClaims = _httpContextAccessor.HttpContext.User;
                }
                return _currentUserClaims;
            }
        }

        #endregion

        #region Ctor

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        #endregion

        #region ICurrentUser implementation

        public bool IsAuthenticated => Claims.Identity.IsAuthenticated;
        public string Login => Claims.FindFirst(ClaimTypes.Name)?.Value;
        public string FirstName => Claims.FindFirst(ClaimTypes.GivenName)?.Value;
        public string LastName => Claims.FindFirst(ClaimTypes.Surname)?.Value;
        public string Email => Claims.FindFirst(ClaimTypes.Email)?.Value;
        public bool HasRole(string role) => Claims.IsInRole(role);
        
        #endregion
    }
}
