using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushBattle
{
    public class RestDispatcher
    {
        private string url = "http://localhost:63131/api/";
        private RestClient client;

        public RestDispatcher()
        {
            client = new RestClient(url);
        }



    }
}