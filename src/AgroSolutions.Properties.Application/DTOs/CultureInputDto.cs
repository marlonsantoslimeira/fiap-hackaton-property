using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSolutions.Properties.Application.DTOs
{
    public record CultureInputDto
    {
        public string Name { get; init; } = string.Empty;
        public int MaxTemperature { get; set; }
        public int MinTemperature { get; set; }
        public int MaxMoist { get; set; }
        public int MinMoist { get; set; }
        public int MaxPrecipitation { get; set; }
        public int MinPrecipitation { get; set; }
    }
}
