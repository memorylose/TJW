/* ======================================================================== 
* Author：Cass 
* Time：9/28/2014 8:22:52 AM 
* Description:  
* ======================================================================== 
*/

using System.IO;

namespace TJW.Utils
{
    public class LogTools
    {
        /// <summary>
        /// Log写入记事本中
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="path"></param>
        public void WriteLog(string contents, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(contents);

            }
        }

        /// <summary>
        /// 读取记事本，并且加入换行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadFile(string path)
        {
            StreamReader m_SW = new StreamReader(path);
            string m_Data = m_SW.ReadToEnd();
            string newData = m_Data.Replace("\r\n", "<br>");
            m_SW.Close();
            return newData;
        }

        /// <summary>
        /// 检查LOG文件是否存在，存在即删除
        /// </summary>
        /// <param name="path"></param>
        public void CheckFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
