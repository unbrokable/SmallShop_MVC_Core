using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Extension
{
    public static class SessionHelper
    {
        public static async  Task SetObjectAsJsonAsync(this ISession session, string key, object value)
        {
           await Task.Run(() => session.SetString(key, JsonConvert.SerializeObject(value)));
        }

        public static  Task<T> GetObjectFromJsonAsync<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return Task.FromResult(value == null ? default : JsonConvert.DeserializeObject<T>(value));
        }
    }
}
