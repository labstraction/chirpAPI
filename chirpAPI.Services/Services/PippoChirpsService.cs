using chirpAPI.model;
using chirpAPI.Services.Services.Interfaces;
using chirpAPI.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chirpAPI.Services.Services
{
    public class PippoChirpsService : IChirpsService
    {
        public Task<List<ChirpViewModel>> GetChirpsByFilter(ChirpFilter filter)
        {
            //to.......
            throw new NotImplementedException();
        }
    }
}
