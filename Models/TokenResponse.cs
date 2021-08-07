using System.Collections.Generic;

namespace Models
{
    /// <summary>
    ///     Token响应
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        ///     Gets or sets the access token.
        /// </summary>
        /// <value>
        ///     The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Gets or sets the custom entries.
        /// </summary>
        /// <value>
        ///     The custom entries.
        /// </value>
        public Dictionary<string, object> Custom { get; set; } = new();
    }
}