using chirpAPI.Services.Model.DTOs;
using chirpAPI.Services.Model.Filters;
using chirpAPI.Services.Model.ViewModel;
using chirpAPI.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chirpAPI.Services.Services
{
    public class PippoChirpsService : IChirpsService
    {
        public Task<int?> CreateChirp(ChirpCreateDTO chirp)
        {
            throw new NotImplementedException();
        }

        public Task<int?> DeleteChirp(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChirpViewModel>> GetAllChirps()
        {
            throw new NotImplementedException();
        }

        public Task<ChirpViewModel> GetChirpById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChirpViewModel>> GetChirpsByFilter(ChirpFilter filter)
        {
            //to.......
            throw new NotImplementedException();
        }

        public Task<bool> UpdateChirp(int id, ChirpUpdateDTO chirp)
        {
            throw new NotImplementedException();
        }
    }
}
