using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        public string ConnStr { get; private set; }

        public TacoParser()
        {
            var configBuilder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.debug.json")
#else
                .AddJsonFile("appsettings.release.json")
#endif
                .Build();
           ConnStr = configBuilder.GetConnectionString("DefaultConnection");
        }

        public List<string> GetDatafromDB()
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            List<string> myData = new List<string>();
            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT latitude, longitude, description FROM locations;";
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string lat = dr["latitude"].ToString();
                    string lon = dr["longitude"].ToString();
                    string name = dr["description"].ToString();
                    string data = "" + lat + "," + lon + "," + name;
                    myData.Add(data);
                }
                return myData;
            }
        }

        public ITrackable Parse(string line)
        {
            var cells = line.Split(',');

            // If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                logger.LogWarning("Not enough values");
                return null;
                // Log that and return null
            }
            logger.LogInfo("Begin parsing");
            TacoBell company = new TacoBell();
            Point myTacoLocal = new Point();
            double lat;
            double lon;
            if(!double.TryParse(cells[0], out lat) || !double.TryParse(cells[1], out lon))
            {
                return null;
            }
            if(Math.Abs(lon) > 180 || Math.Abs(lat) > 90)
            {
                return null;
            }
            for (int i = 0; i < cells.Length; i++)
            {
                
                if (String.IsNullOrWhiteSpace(cells[i]))
                {
                    if(cells[i].Length > 0)
                    {
                        logger.LogInfo("Incomplete Data");
                        return null;
                    }
                    else
                    {
                        logger.LogFatal(null);
                        return null;
                    }
                }
                
                else if(i == 0)
                {
                    
                    myTacoLocal.Latitude = double.Parse(cells[i]);
                    
                }
                else if(i == 1)
                {
                   
                    myTacoLocal.Longitude = Convert.ToDouble(cells[i]);
                }
                else
                {
                    
                    company.SetName(cells[i]);
                }
                
               
            }
            company.SetLocation(myTacoLocal);
            return company;
            // grab the latitude from your array at index 0
            // grab the longitude from your array at index 1
            // grab the name from your array at index 2

            // Your going to need to parse your string as a `double`
            // which is similar to parsing a string as an `int`

            // You'll need to create a TacoBell class
            // that conforms to ITrackable

            // Then, you'll need an instance of the TacoBell class
            // With the name and point set correctly

            // Then, return the instance of your TacoBell class
            // Since it conforms to ITrackable



            // Do not fail if one record parsing fails, return null

             // TODO Implement
        }
    }
}