using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chirpAPI.Services.Model.DTOs
{
    public class ChirpUpdateDTO
    {
        public string? Text { get; set; }

        public string? ExtUrl { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }
    }
}
