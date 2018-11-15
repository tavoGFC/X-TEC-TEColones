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
        public string CONSUMER_KEY { get; set; }

        public string CONSUMER_SECRET { get; set; }

        public string ACCESS_TOKEN { get; set; }
 
        public string ACCESS_TOKEN_SECRET { get; set; }

        public string Message { get; set; }

        //CONSUMER_KEY = "8gYBbQjhYO9JyRbbR3blQnTEd";
        //CONSUMER_SECRET = "oTFi5zD5YzBk9awJNaKIWwJ4VF3QSSo06mKTCmjmKKa0s0V7KM";
        //ACCESS_TOKEN = "1062419932820987904-KCxTUQbcQfPFJRLxun56YgnFAKO7H6";
        //ACCESS_TOKEN_SECRET =  "VkHF7piK0ntXXprmlZixySgN1a8STpoUMfEDHA9iTsuD8";

        //Auth.SetUserCredentials("8gYBbQjhYO9JyRbbR3blQnTEd", "oTFi5zD5YzBk9awJNaKIWwJ4VF3QSSo06mKTCmjmKKa0s0V7KM", "1062419932820987904-KCxTUQbcQfPFJRLxun56YgnFAKO7H6", "VkHF7piK0ntXXprmlZixySgN1a8STpoUMfEDHA9iTsuD8");
        //var user = User.GetAuthenticatedUser();
        //var tweet = Tweet.PublishTweet("HOLA CHAAAAAANG.");
    }
}


