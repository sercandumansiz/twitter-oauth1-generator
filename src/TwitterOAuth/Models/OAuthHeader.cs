namespace Twitter.OAuth.Models
{
    internal class OAuthHeader
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string Nonce { get; set; }
        public string Signature { get; set; }
        public string SignatureMethod => "HMAC-SHA1";
        public string Timestamp { get; set; }
        public string Version => "1.0";
    }
}
