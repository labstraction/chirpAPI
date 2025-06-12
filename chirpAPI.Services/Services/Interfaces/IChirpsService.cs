using chirpAPI.model;
using chirpAPI.Services.Model.DTOs;
using chirpAPI.Services.Model.Filters;
using chirpAPI.Services.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chirpAPI.Services.Services.Interfaces
{
    public interface IChirpsService
    {
        Task<List<ChirpViewModel>> GetChirpsByFilter(ChirpFilter filter);
        Task<List<ChirpViewModel>> GetAllChirps();
        Task<ChirpViewModel> GetChirpById(int id);
        Task<bool> UpdateChirp(int id, ChirpUpdateDTO chirp);
        Task<int?> CreateChirp(ChirpCreateDTO chirp);
        Task<int?> DeleteChirp(int id);
    }
}
