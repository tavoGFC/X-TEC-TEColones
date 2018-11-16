using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace X_TEC.TEColones.Persistence
{
    class TwitterConnection
    {
        public string CONSUMER_KEY;
        public string CONSUMER_SECRET;
        public string ACCESS_TOKEN;
        public string ACCESS_TOKEN_SECRET;

        public void SetCredentials(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            CONSUMER_KEY = consumerKey;
            CONSUMER_SECRET = consumerSecret;
            ACCESS_TOKEN = accessToken;
            ACCESS_TOKEN_SECRET = accessTokenSecret;
        }

        public void Publish(string mensaje)
        {
            //Auth.SetUserCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            Auth.SetUserCredentials("8gYBbQjhYO9JyRbbR3blQnTEd", "oTFi5zD5YzBk9awJNaKIWwJ4VF3QSSo06mKTCmjmKKa0s0V7KM", "1062419932820987904-KCxTUQbcQfPFJRLxun56YgnFAKO7H6", "VkHF7piK0ntXXprmlZixySgN1a8STpoUMfEDHA9iTsuD8");
            var user = User.GetAuthenticatedUser();
            var tweet = Tweet.PublishTweet(mensaje);
        }
    }
}


