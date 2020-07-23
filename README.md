# **Twitter OAuth**

Helps you to generate OAuth1.0a authentication header value for Twitter API.

[![NuGet version](https://badge.fury.io/nu/Twitter.OAuth.svg)](https://badge.fury.io/nu/Twitter.OAuth)

### NuGet
``` 
dotnet add package Twitter.OAuth --version 1.0.2
```

### Supports
.NET Standard 2.1

## Usage
``` 
OAuthHeaderGenerator oAuthHeaderGenerator = new OAuthHeaderGenerator(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret);

string url = $"https://api.twitter.com/1.1/friendships/destroy.json?screen_name={inactiveUser.Name}";

string oAuth = oAuthHeaderGenerator.GenerateOAuthHeader("POST", url);

_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", oAuth);

```



