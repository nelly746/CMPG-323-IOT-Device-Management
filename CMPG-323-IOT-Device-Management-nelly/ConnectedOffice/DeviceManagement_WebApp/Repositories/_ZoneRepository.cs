using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.repositories
{
    public class _ZoneRepository
    {
        private readonly ConnectedOfficeContext _context;

        public _ZoneRepository(ConnectedOfficeContext context)
        {
            _context = context;
        }

        private bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
        }

        public Task<List<Zone>> getAllZones(){
            return _context.Zone.ToListAsync();
        }

        public Task<Zone> OneZoneGet(Guid? id){
            return _context.Zone.FirstOrDefaultAsync(m => m.ZoneId == id);
        }

        public Task<Zone> getZoneDetails(Guid? id){
            return  _context.Zone
                .FirstOrDefaultAsync(m => m.ZoneId == id);
        }

        public async void ZoneCreate(Zone zone){
            zone.ZoneId = Guid.NewGuid();
            _context.Add(zone);
            await _context.SaveChangesAsync();
        }

        public async void editZoneById(Guid id, Zone zone){
            try
            {
                if (ZoneExists(zone.ZoneId))
                {
                    _context.Update(zone);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
               throw;
            }
        }

        public async void deleteZoneConfirm(Guid id){
             var category = await _context.Zone.FindAsync(id);
            _context.Zone.Remove(category);
            await _context.SaveChangesAsync();
        }

    }
}