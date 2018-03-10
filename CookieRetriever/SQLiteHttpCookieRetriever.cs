using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Data.SQLite;
using System.Configuration;

namespace CookieDetector
{
    class SQLiteHttpCookieRetriever : ILocalCookieRetriever
    {
        private string dbPath;

        public SQLiteHttpCookieRetriever()
        {
            dbPath = ConfigurationManager.AppSettings["HttpCookieDirectory"];
        }

        public SQLiteHttpCookieRetriever(string path)
        {
            dbPath = path;
        }

        CookieJar ICookieRetriever.GetCookies(string url)
        {
            
            if (url == null) throw new ArgumentNullException("url");
            CookieJar cookieJar = new CookieJar();
            string domain = new Uri(url).Host.Trim().Replace("www.", "").Trim();
         
            var dbPath = ConfigurationManager.AppSettings["HttpCookieDirectory"];

            if (!System.IO.File.Exists(dbPath))
                return new CookieJar();

            var connectionString = "Data Source=" + dbPath + ";";
            string commandText = "SELECT name,encrypted_value, host_key, expires_utc, path, secure, httponly FROM cookies WHERE host_key LIKE %@domain";
            SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(commandText, conn);
            cmd.Parameters.AddWithValue("@domain", domain);
            SQLiteDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var encryptedData = (byte[])reader[1];
                    var decodedData = System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                    var plainText = Encoding.ASCII.GetString(decodedData);
                    HttpCookie c = new HttpCookie();
                    c.Name = reader["name"].ToString();
                    c.Domain = reader["host_key"].ToString();
                    c.Expiry = new DateTime((long)reader["expires_utc"]).ToString();
                    c.Path = reader["path"].ToString();
                    c.Type = "http";
                    c.IsSecure = (reader["secure"].ToString() == "1");
                    c.IsHttpOnly = (reader["httponly"].ToString() == "1");
                    c.DateCreated = DateTime.Now.Ticks;
                    c.Content = plainText;

                    cookieJar.Add(c);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                conn.Close();
            }
            return cookieJar;
        }

        void ILocalCookieRetriever.setPath(string path)
        {
            dbPath = path;
        }
    }
}

