using chirpAPI.model;
using chirpAPI.Services.Services.Interfaces;
using chirpAPI.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chirpAPI.Services.Services
{
    public class VasylChirpsService : IChirpsService
    {
        private readonly CinguettioContext _context;

        public VasylChirpsService(CinguettioContext context)
        {
            _context = context;
        }

        public async Task<List<ChirpViewModel>> GetChirpsByFilter(ChirpFilter filter)
        {
            IQueryable<Chirp> query = _context.Chirps.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                query = query.Where(x => x.Text == filter.Text);
            }

            var result = await query.Select(x => new ChirpViewModel
            {
                Id = x.Id,
                Text = x.Text,
                ExtUrl = x.ExtUrl,
                Lat = x.Lat,
                Lng = x.Lng,
                CreationTime = x.CreationTime
            }).ToListAsync();

            return result;
        }
    }
}
