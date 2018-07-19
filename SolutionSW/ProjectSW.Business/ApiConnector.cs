using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using ProjectSW.DTO;

namespace ProjectSW.Business
{
    public class ApiConnector
    {
        public List<StarshipDTO> GetAllStarsips(string baseApiUrl)
        {
            //Initiate the starship list
            var starshipList = new List<StarshipDTO>();

            //Get the first page of starship            
            var page = GetStarshipPagging($"{baseApiUrl}starships/");

            //While there is another next page, get the results
            while (page.next != null)
            {
                starshipList.AddRange(page.results);
                page = GetStarshipPagging(page.next);
            }
            return starshipList;
        }

        private StarshipPaggingDTO GetStarshipPagging(string url)
        {

            var request = WebRequest.Create(url);

            request.Credentials = CredentialCache.DefaultCredentials;

            var response = request.GetResponse();
            Console.WriteLine("Server Status: " + ((HttpWebResponse)response).StatusDescription);
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var starshipsJson = reader.ReadToEnd();

            var starshipsPagging = JsonConvert.DeserializeObject<StarshipPaggingDTO>(starshipsJson);

            reader.Close();
            response.Close();
            return starshipsPagging;
        }
    }
}