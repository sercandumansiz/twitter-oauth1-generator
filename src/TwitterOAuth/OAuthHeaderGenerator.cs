using Twitter.OAuth.Models;
using Twitter.OAuth.Helpers;
using System.Collections.Generic;
using System;

namespace Twitter.OAuth
{
    public class OAuthHeaderGenerator
    {
        private OAuthHeader _oauthHeader;

        public OAuthHeaderGenerator(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            if (string.IsNullOrEmpty(consumerKey) || string.IsNullOrEmpty(consumerSecret) || string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(accessTokenSecret))
            {
                throw new ArgumentException();
            }

            _oauthHeader = new OAuthHeader();
            _oauthHeader.ConsumerKey = consumerKey;
            _oauthHeader.ConsumerSecret = consumerSecret;
            _oauthHeader.AccessToken = accessToken;
            _oauthHeader.AccessTokenSecret = accessTokenSecret;
        }

        public string GenerateOAuthHeader(string requestMethod, string requestUrl)
        {
            // TODO : validation
            GenerateHMACSHA1Signature(requestMethod, requestUrl);

            string oAuthHeader = string.Empty;

            oAuthHeader += $"oauth_consumer_key={EncodingHelper.PercentEncode(_oauthHeader.ConsumerKey)}, ";
            oAuthHeader += $"oauth_nonce={EncodingHelper.PercentEncode(_oauthHeader.Nonce)}, ";
            oAuthHeader += $"oauth_signature={EncodingHelper.PercentEncode(_oauthHeader.Signature)}, ";
            oAuthHeader += $"oauth_signature_method={EncodingHelper.PercentEncode(_oauthHeader.SignatureMethod)}, ";
            oAuthHeader += $"oauth_timestamp={EncodingHelper.PercentEncode(_oauthHeader.Timestamp)}, ";
            oAuthHeader += $"oauth_token={EncodingHelper.PercentEncode(_oauthHeader.AccessToken)}, ";
            oAuthHeader += $"oauth_version={EncodingHelper.PercentEncode(_oauthHeader.Version)}";

            return oAuthHeader;
        }

        private void GenerateHMACSHA1Signature(string requestMethod, string requestUrl)
        {
            string signatureBase = GenerateSignatureBase(requestMethod, requestUrl);

            string key = $"{EncodingHelper.PercentEncode(_oauthHeader.ConsumerSecret)}&{EncodingHelper.PercentEncode(_oauthHeader.AccessTokenSecret)}";

            _oauthHeader.Signature = CryptoHelper.HMACSHA1Hash(key, signatureBase);
        }

        private string GenerateSignatureBase(string requestMethod, string requestUrl)
        {
            SortedDictionary<string, string> sortedParameters = GetOAuthHeaderParameters(requestUrl);

            string parameterString = string.Empty;

            foreach (var sortedParameter in sortedParameters)
            {
                parameterString += $"{sortedParameter.Key}={sortedParameter.Value}&";
            }

            parameterString = parameterString.Substring(0, parameterString.Length - 1);

            string signatureBase = $"{requestMethod.ToUpper()}&";
            signatureBase += $"{EncodingHelper.PercentEncode(UriHelper.GetBaseUrl(requestUrl))}&";
            signatureBase += $"{EncodingHelper.PercentEncode(parameterString)}";

            return signatureBase;
        }

        private SortedDictionary<string, string> GetOAuthHeaderParameters(string requestUrl)
        {
            SortedDictionary<string, string> sortedParameters = new SortedDictionary<string, string>();

            _oauthHeader.Nonce = OAuthHelper.GenerateNonce();
            _oauthHeader.Timestamp = OAuthHelper.GenerateTimeStamp();

            sortedParameters.Add("oauth_consumer_key", _oauthHeader.ConsumerKey);
            sortedParameters.Add("oauth_token", _oauthHeader.AccessToken);
            sortedParameters.Add("oauth_signature_method", _oauthHeader.SignatureMethod);
            sortedParameters.Add("oauth_version", _oauthHeader.Version);
            sortedParameters.Add("oauth_timestamp", _oauthHeader.Timestamp);
            sortedParameters.Add("oauth_nonce", _oauthHeader.Nonce);

            Dictionary<string, string> queryParameters = UriHelper.GetQueryParameters(requestUrl);

            foreach (var queryParameter in queryParameters)
            {
                sortedParameters.Add(queryParameter.Key, queryParameter.Value);
            }

            return sortedParameters;
        }
    }
}
