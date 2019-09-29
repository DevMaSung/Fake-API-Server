using System;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FakeApiServer.Controllers
{
    [RoutePrefix("api/json")]
    public class JsonController : ApiController
    {
        string root = @"d:\dummyAPI";
        string saveJson = @"{0}\{1}.json";
        public JsonController()
        {
        }
        [HttpGet]
        [Route("list")]
        public string[] GetJSONList()
        {
            string[] result;
            try
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                result = Directory.GetFiles(root);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        [HttpGet, HttpOptions]
        [Route("Get/{KEY}")]
        public JObject GetJSON([FromUri] string KEY)
        {
            try
            {
                using (StreamReader file = File.OpenText(string.Format(saveJson, root, KEY)))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    return (JObject)JToken.ReadFrom(reader);
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
        [HttpPost]
        [Route("save")]
        public string SaveJson([FromBody] JsonModel jsonKey)
        {
            try
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                using (StreamWriter file = File.CreateText(string.Format(saveJson, root, jsonKey.key)))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    JObject.Parse(jsonKey.JSON).WriteTo(writer);
                }
                return "1";
            }catch(Exception e)
            {
                return "0";
            }
            
        }

        #region class
        public class JsonModel {
            public string key { get; set; }
            public string JSON { get; set; }
        }

        #endregion
    }
}
