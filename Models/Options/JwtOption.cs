namespace Models.Options
{
    public class JwtOption
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }

        /// <summary>
        ///     有效期
        /// </summary>
        public int Expires { get; set; }
    }
}