using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using ProjectLab.Cookies;
using System;

namespace ProjectLab.Services
{
    /// <summary>
    /// Provides methods for setting, getting, deleting, and protecting cookies in the current HTTP context.
    /// </summary>
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtector _protector;

        /// <summary>
        /// Initializes a new instance of the <see cref="CookieService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Accessor for the current HTTP context.</param>
        /// <param name="dataProtectionProvider">Provider for data protection services.</param>
        public CookieService(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _protector = dataProtectionProvider.CreateProtector("ProjectLab.CookieProtection.v1");
        }

        /// <summary>
        /// Sets a cookie with the specified key and value.
        /// </summary>
        /// <param name="key">The cookie key.</param>
        /// <param name="value">The cookie value.</param>
        /// <param name="expireDays">Optional expiration in days. Defaults to 30 days.</param>
        /// <param name="httpOnly">Indicates if the cookie is HTTP only. Defaults to true.</param>
        public void Set(string key, string value, int? expireDays = 30, bool httpOnly = true)
        {
            var options = new CookieOptions
            {
                HttpOnly = httpOnly,
                Expires = expireDays.HasValue ? DateTimeOffset.UtcNow.AddDays(expireDays.Value) : (DateTimeOffset?)null,
                SameSite = SameSiteMode.Lax,
                Secure = _httpContextAccessor.HttpContext?.Request.IsHttps ?? false
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value ?? string.Empty, options);
        }

        /// <summary>
        /// Gets the value of a cookie by key.
        /// </summary>
        /// <param name="key">The cookie key.</param>
        /// <returns>The cookie value, or null if not found.</returns>
        public string? Get(string key)
        {
            if (_httpContextAccessor.HttpContext?.Request?.Cookies == null) return null;
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(key, out var value);
            return value;
        }

        /// <summary>
        /// Deletes a cookie by key.
        /// </summary>
        /// <param name="key">The cookie key.</param>
        public void Delete(string key)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// Sets a protected (encrypted) cookie with the specified key and value.
        /// </summary>
        /// <param name="key">The cookie key.</param>
        /// <param name="value">The cookie value.</param>
        /// <param name="expireDays">Optional expiration in days. Defaults to 30 days.</param>
        public void SetProtected(string key, string value, int? expireDays = 30)
        {
            var protectedValue = _protector.Protect(value ?? string.Empty);
            Set(key, protectedValue, expireDays, httpOnly: true);
        }

        /// <summary>
        /// Gets and unprotects (decrypts) the value of a protected cookie by key.
        /// </summary>
        /// <param name="key">The cookie key.</param>
        /// <returns>The unprotected cookie value, or null if not found or tampered.</returns>
        public string? GetProtected(string key)
        {
            var protectedValue = Get(key);
            if (string.IsNullOrEmpty(protectedValue)) return null;
            try
            {
                return _protector.Unprotect(protectedValue);
            }
            catch
            {
                // tampering or expired protection — treat as missing
                return null;
            }
        }
    }
}