using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Drawing;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris%20Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName.Replace("%20", " ") +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014

        //Mesmo somando manualmente não da o resultado acima...

        //Team Paris Saint - Germain scored 62 goals in 2013
        //Team Chelsea scored 47 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        return ConsumeAPIAsync(team, year).Result.data.Sum(item => int.Parse(item.team1goals));
    }


    public static async Task<Root> ConsumeAPIAsync(string _strTeamName, int _iYear)
    {
        HttpWebRequest webrequest;
        HttpWebResponse webresponse;
        string strUrl;
        string json;
        int currentPageIndex = 1;
        int totalPage = 1;
        Root Jogos = new Root();
        do
        {
            strUrl = String.Format("https://jsonmock.hackerrank.com/api/football_matches?year={0}&team1={1}&page={2}", _iYear, _strTeamName, currentPageIndex);
            webrequest = (HttpWebRequest)WebRequest.Create(strUrl);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webresponse = (HttpWebResponse)webrequest.GetResponse();

            using (var responseStream = webresponse.GetResponseStream())
            {
                using (var line = new StreamReader(responseStream, Encoding.UTF8))
                {
                    json = line.ReadToEnd();
                    if (!string.IsNullOrEmpty(json))
                    {
                        if (currentPageIndex == 1)
                        {
                            Jogos = JsonConvert.DeserializeObject<Root>(json);
                            totalPage = Jogos.total_pages;
                        }
                        else
                        {
                            Jogos.data.AddRange(JsonConvert.DeserializeObject<Root>(json).data);
                        }
                    }
                }
            }
           currentPageIndex++;
        } while (currentPageIndex <= totalPage);

        return Jogos;
    }

    
    public class Datum
    {
        public string competition { get; set; }
        public int year { get; set; }
        public string round { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string team1goals { get; set; }
        public string team2goals { get; set; }
    }

    public class Root
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Datum> data { get; set; }
    }
   
}