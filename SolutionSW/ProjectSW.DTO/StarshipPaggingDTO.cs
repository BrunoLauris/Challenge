using System.Collections.Generic;

namespace ProjectSW.DTO
{

    /// <summary>
    //"$schema": "http://json-schema.org/draft-04/schema"
    /// </summary>
    public class StarshipPaggingDTO
    {
        public string count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<StarshipDTO> results { get; set; }
    }
}
