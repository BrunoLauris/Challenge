using System;
using System.Collections.Generic;
using ProjectSW.DTO;

namespace ProjectSW.Business
{
    public class StarshipBusiness
    {
        /// <summary>
        /// Method description here 
        /// </summary>
        /// <param name="baseApiUrl"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public List<StarshipDistanceDTO> CalculateStarshipsStops(string baseApiUrl, long distance)
        {
            var starshipDistanceList = new List<StarshipDistanceDTO>();
            var api = new ApiConnector();
            var startships = api.GetAllStarsips(baseApiUrl);

            foreach (var starship in startships)
            {
                var maxTimeTravelWithSupply = getConsumableInMgltHours(starship);
                var hasMgltInformation = long.TryParse(starship.MGLT, out var startshipSpeeInMglt);
                var hasAllInfoCalculateStops = hasMgltInformation && maxTimeTravelWithSupply != 0;

                var starshipDistance = new StarshipDistanceDTO
                {
                    Name = starship.name,
                    AmountStops = hasAllInfoCalculateStops
                        ? (distance / startshipSpeeInMglt / maxTimeTravelWithSupply).ToString()
                        : "Unknown"
                };
                Console.WriteLine(starshipDistance.Name + ": " + starshipDistance.AmountStops);
                starshipDistanceList.Add(starshipDistance);
            }

            return starshipDistanceList;
        }

        private static int HoursInADay = 24;
        private static int DaysInAWeek = 7;
        private static int DaysInAMonth = 30;
        private static int DaysInAYear = 365; 

        /// <summary>
        /// Create a simple method description here....
        /// </summary>
        /// <param name="starship"></param>
        /// <returns></returns>
        private static int getConsumableInMgltHours(StarshipDTO starship)
        {
  
            var consumablesSplit = starship.consumables.Split(' ');

            //starship has unknown consumable information
            if (consumablesSplit.Length != 2) return 0;

            var unitOfConsumable = Convert.ToInt32(consumablesSplit[0]);
            var strTime = consumablesSplit[1];
            var maxTimeTravelWithSupply = 0;

           switch (strTime)
            {
                case "weeks":
                case "week":
                    maxTimeTravelWithSupply = unitOfConsumable * DaysInAWeek * HoursInADay;
                    break;
                case "months":
                case "month":
                    maxTimeTravelWithSupply = unitOfConsumable * DaysInAMonth * HoursInADay;
                    break;
                case "years":
                case "year":
                    maxTimeTravelWithSupply = unitOfConsumable * DaysInAYear * HoursInADay;
                    break;
            }

            return maxTimeTravelWithSupply;
        }
    }
}