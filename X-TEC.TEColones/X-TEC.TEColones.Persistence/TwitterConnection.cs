using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweetinvi;
using X_TEC.TEColones.Models.AdminModels;

namespace X_TEC.TEColones.Persistence
{
    public class TwitterConnection
    {

        public static string CONSUMER_KEY { get; set; }
        public static string CONSUMER_SECRET { get; set; }
        public static string ACCESS_TOKEN { get; set; }
        public static string ACCESS_TOKEN_SECRET { get; set; }

        /// <summary>
        /// Set new credentials (two keys and two tokens) for the account of the publications.
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="accessToken"></param>
        /// <param name="accessTokenSecret"></param>
        public static void SetCredentials()
        {
            DBConnection.GetTwitterData();
        }
        
        /// <summary>
         /// Set new credentials (two keys and two tokens) for the account of the publications.
         /// </summary>
         /// <param name="consumerKey"></param>
         /// <param name="consumerSecret"></param>
         /// <param name="accessToken"></param>
         /// <param name="accessTokenSecret"></param>
        public static void SetCredentials(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            CONSUMER_KEY = consumerKey;
            CONSUMER_SECRET = consumerSecret;
            ACCESS_TOKEN = accessToken;
            ACCESS_TOKEN_SECRET = accessTokenSecret;
        }

        /// <summary>
        /// Make a publication on the Twitter account with a given mesaage.
        /// </summary>
        /// <param name="mensaje"></param>
        public static void Publish(string mensaje)
        {
            Auth.SetUserCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            var user = User.GetAuthenticatedUser();
            var tweet = Tweet.PublishTweet(mensaje);
        }
    }
}


