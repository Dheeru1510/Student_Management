using Newtonsoft.Json;
using System;

namespace Student_Management.Common
{
    public static class ObjectExtensions
    {
        private const string Null = "null";
        private const string Exception = "Exception";

        public static string ToJson(this object value, Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None)
        {
            if (value == null) return Null;
            try
            {
                return JsonConvert.SerializeObject(value, formatting);
            }
            catch (Exception e)
            {
                FileLogger.LogException(e);
                return Exception;
            }
        }
    }
}