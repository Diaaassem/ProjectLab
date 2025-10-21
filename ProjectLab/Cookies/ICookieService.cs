namespace ProjectLab.Cookies
{
    /// <summary>
    /// Provides methods for setting, retrieving, and deleting cookies, including support for protected cookies.
    /// </summary>
    public interface ICookieService
    {
        /// <summary>
        /// Sets a cookie with the specified key and value.
        /// </summary>
        /// <param name="key">The name of the cookie.</param>
        /// <param name="value">The value to store in the cookie.</param>
        /// <param name="expireDays">The number of days until the cookie expires. Defaults to 30 days.</param>
        /// <param name="httpOnly">Indicates whether the cookie is accessible only via HTTP requests. Defaults to true.</param>
        void Set(string key, string value, int? expireDays = 30, bool httpOnly = true);

        /// <summary>
        /// Retrieves the value of the cookie with the specified key.
        /// </summary>
        /// <param name="key">The name of the cookie.</param>
        /// <returns>The value of the cookie, or null if not found.</returns>
        string? Get(string key);

        /// <summary>
        /// Deletes the cookie with the specified key.
        /// </summary>
        /// <param name="key">The name of the cookie to delete.</param>
        void Delete(string key);

        /// <summary>
        /// Sets a protected cookie with the specified key and value.
        /// The value will be encrypted or otherwise protected.
        /// </summary>
        /// <param name="key">The name of the cookie.</param>
        /// <param name="value">The value to store in the protected cookie.</param>
        /// <param name="expireDays">The number of days until the cookie expires. Defaults to 30 days.</param>
        void SetProtected(string key, string value, int? expireDays = 30);

        /// <summary>
        /// Retrieves the value of a protected cookie with the specified key.
        /// </summary>
        /// <param name="key">The name of the protected cookie.</param>
        /// <returns>The decrypted value of the cookie, or null if not found.</returns>
        string? GetProtected(string key);
    }
}
