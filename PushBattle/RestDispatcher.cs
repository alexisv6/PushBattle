using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushBattle
{
    public class RestDispatcher
    {
        private static string url = "http://localhost:63131/api/";
        private static RestClient client;

        
        public static IRestResponse ExecuteRequest(RestRequest request)
        {
            if (client == null)
            {
                client = new RestClient(url);
            }
//            IRestResponse resp = client.Execute(request);
            
            return client.Execute(request);
        }

        public static T ExecuteRequest<T>(RestRequest request)
        {
            if (client == null)
                client = new RestClient(url);

            IRestResponse resp = client.Execute(request);

            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RestSharp.Serializers.JsonSerializer serial = new RestSharp.Serializers.JsonSerializer();
                
                return SimpleJson.DeserializeObject<T>(resp.Content);

            }
            return default(T);
        }

        public static IRestResponse ExecuteRequest(string resource, Method how)
        {
            return ExecuteRequest(new RestRequest(resource, how));
        }

        public static T ExecuteRequest<T>(string resource, Method how)
        {
            return ExecuteRequest<T>(new RestRequest(resource, how));
        }

        /// <summary>
        /// Deserializes the object from a previously returned response.
        /// </summary>
        /// <typeparam name="T">A Json deserializable object retrieved from a rest api call</typeparam>
        /// <param name="response"></param>
        /// <returns>The deserialized object or null if no object in response.</returns>
        public static T Deserialize<T>(IRestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return SimpleJson.DeserializeObject<T>(response.Content);
            }
            return default(T);
        }

    }
}