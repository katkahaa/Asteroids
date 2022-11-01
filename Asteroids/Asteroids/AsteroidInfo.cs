using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Asteroids
{
    public class AsteroidInfo
    {
        [JsonProperty("name_limited")]
            public string Name { get; set; }
        [JsonProperty("absolute_magnitude_h")]
        public double Magnitude { get; set; }
        [JsonProperty("is_potentially_hazardous_asteroid")]
        public bool IsDangerous { get; set; }

        public override string ToString()
        {
            string dangerous = IsDangerous ? "dangerous" : "not dangerous";
            return $"{Name}, Magnitude: {Magnitude}, {dangerous}";
        }

    }

    public class RootObject
    {
        public List<AsteroidInfo> Children { get; set; }
    }
    
}
