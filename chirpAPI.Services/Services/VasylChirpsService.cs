using chirpAPI.model;
using chirpAPI.Services.Model.DTOs;
using chirpAPI.Services.Model.Filters;
using chirpAPI.Services.Model.ViewModel;
using chirpAPI.Services.Services.Interfaces;
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
            if (!string.IsNullOrWhiteSpace(filter.ExtUrl))
            {
                query = query.Where(x => x.ExtUrl.Contains(filter.ExtUrl));
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

        public async Task<List<ChirpViewModel>> GetAllChirps()
        {
            return await _context.Chirps.Select(x => new ChirpViewModel
            {
                Id = x.Id,
                Text = x.Text,
                ExtUrl = x.ExtUrl,
                Lat = x.Lat,
                Lng = x.Lng,
                CreationTime = x.CreationTime
            }).ToListAsync();
        }

        public async Task<ChirpViewModel> GetChirpById(int id)
        {
            var entity = await _context.Chirps.FindAsync(id);

            if (entity == null)
            {
                return null;
            }
            return new ChirpViewModel
            {
                Id = entity.Id,
                Text = entity.Text,
                ExtUrl = entity.ExtUrl,
                Lat = entity.Lat,
                Lng = entity.Lng,
                CreationTime = entity.CreationTime
            };
        }

        public async Task<bool> UpdateChirp(int id, ChirpUpdateDTO chirp)
        {
            var entity = await _context.Chirps.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(chirp.ExtUrl))
                entity.ExtUrl = chirp.ExtUrl;

            if (!string.IsNullOrWhiteSpace(chirp.Text))
                entity.Text = chirp.Text;

            if (chirp.Lng != null)
                entity.Lng = chirp.Lng;

            if (chirp.Lat != null)
                entity.Lat = chirp.Lat;

            _context.Entry(chirp).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int?> CreateChirp(ChirpCreateDTO chirp)
        {
            if (string.IsNullOrWhiteSpace(chirp.Text))
            {
                return null;
            }

            var entity = new Chirp
            {
                Text = chirp.Text,
                ExtUrl = chirp.ExtUrl,
                Lat = chirp.Lat,
                Lng = chirp.Lng
            };

            _context.Chirps.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<int?> DeleteChirp(int id)
        {
            Chirp? entity = await _context.Chirps.Include(x => x.Comments)
                                                    .Where(x => x.Id == id)
                                                    .SingleOrDefaultAsync();
            if (entity == null)
            {
                return null;
            }
            if (entity.Comments != null || entity.Comments.Count > 0)
            {
                return -1;
            }

            _context.Chirps.Remove(entity);
            await _context.SaveChangesAsync();

            return id;
        }
    }
}
