using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweetinvi;
using X_TEC.TEColones.Models.AdminModels;

namespace X_TEC.TEColones.Persistence
{
    class TwitterConnection
    {
         
        public void SetCredentials()
        {
            //string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret
            //AdminModel AdminModel = (AdminModel)TempData["admin"];

        }

        /// <summary>
        /// Make a publication on the Twitter account with a given mesaage.
        /// </summary>
        /// <param name="mensaje"></param>
        public void Publish(string mensaje)
        {
            //Auth.SetUserCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            Auth.SetUserCredentials("8gYBbQjhYO9JyRbbR3blQnTEd", "oTFi5zD5YzBk9awJNaKIWwJ4VF3QSSo06mKTCmjmKKa0s0V7KM", "1062419932820987904-KCxTUQbcQfPFJRLxun56YgnFAKO7H6", "VkHF7piK0ntXXprmlZixySgN1a8STpoUMfEDHA9iTsuD8");
            var user = User.GetAuthenticatedUser();
            var tweet = Tweet.PublishTweet(mensaje);
        }
    }
}


