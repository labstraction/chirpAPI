﻿using chirpAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chirpAPI.Services.Model.ViewModel
{
    public class ChirpViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string? ExtUrl { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
