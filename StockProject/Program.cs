using System;
using System.IO;
using System.Net;
using Microsoft.Data.Analysis;

namespace StockProject
{
    class Program
    {
        static void Main(string[] args)
        {
            AVConnection conn = new AVConnection("demo");
            conn.SaveCSVFromURL("IBM");
            DataFrame df = DataFrame.LoadCsv("stockdata.csv");
            Console.WriteLine(df);
        }
    }

    public class AVConnection
    {
        private readonly string _apiKey;

        public AVConnection(string apiKey)
        {
            this._apiKey = apiKey;
        }

        public void SaveCSVFromURL(string symbol)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://" + $@"www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={this._apiKey}&datatype=csv");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            File.WriteAllText("stockdata.csv", results);
        }
    }
}
