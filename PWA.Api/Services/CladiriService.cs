﻿using Microsoft.EntityFrameworkCore;
using PWA.Api.Data;
using PWA.Api.Data.Models;
using PWA.Api.Migrations;

namespace PWA.Api.Services
{
    public sealed class CladiriService
    {
        private readonly AppDbContext _context;
        public CladiriService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cladire?> GetCladireAsync(int id)
        {
            var cladire = await _context.Cladiri.FirstOrDefaultAsync(c=>c.Id == id);
            return cladire;
        }
        public async Task AddCladireAsync(Cladire cladire)
        {
            _context.Cladiri.Add(cladire);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCladireAsync(Cladire cladire)
        {
            _context.Cladiri.Update(cladire);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveCladireAsync(int id)
        {
            var cladire = await GetCladireAsync(id);
            _context.Cladiri.Remove(cladire);
            await _context.SaveChangesAsync();
        }

    }
}
