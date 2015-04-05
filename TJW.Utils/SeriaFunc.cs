/* ======================================================================== 
* Author：Cass 
* Time：8/12/2014 9:00:01 AM 
* Description:  Object serialization
* ======================================================================== 
*/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using TJW.Model;


namespace TJW.Utils
{
    public class SeriaFunc
    {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="Li"></param>
        /// <returns></returns>
        public static string SerializeFun(LoginInfo Li)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, Li);
            byte[] objbyte = ms.ToArray();
            return Convert.ToBase64String(objbyte, 0, objbyte.Length);
        }

        /// <summary>
        /// Deseralize
        /// </summary>
        /// <param name="SerializeStr"></param>
        /// <returns></returns>
        public static LoginInfo DnSerializeFun(string SerializeStr)
        {
            byte[] byt = Convert.FromBase64String(SerializeStr);
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(byt, 0, byt.Length);
            return bf.Deserialize(ms) as LoginInfo;
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// JSON Serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {   
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        /// <summary>  
        /// 获取Json的Model  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="szJson"></param>  
        /// <returns></returns>  
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }  
    }
}
