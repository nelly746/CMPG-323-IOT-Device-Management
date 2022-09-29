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
    public class _DeviceRepository
    {
        private readonly ConnectedOfficeContext _context;

        public _DeviceRepository(ConnectedOfficeContext context)
        {
            _context = context;
        }


        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }

        public Task<List<Device>> DevicesGetAll(){
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return connectedOfficeContext.ToListAsync();
        }

        public Task<Device> getOneDevice(Guid? id){
            return _context.Device.FirstOrDefaultAsync(m => m.DeviceId == id);

        }

        public Task<Device> DeviceGetDetails(Guid? id){
            return _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
        }

        public async void CreateNewDevice(Device device){
            device.DeviceId = Guid.NewGuid();
            _context.Add(device);
            await _context.SaveChangesAsync();
        }

        public async void DeviceEdit(Guid id, Device device){
            try
            {
                if (DeviceExists(device.DeviceId))
                {
                    _context.Update(device);
                await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
               throw;
        
            }
        }

        public Task<Device> DeviceDeleteById(Guid? id){
            return _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
        }

        public async void Devicedelete(Guid id){
             var device = await _context.Device.FindAsync(id);
            _context.Device.Remove(device);
            await _context.SaveChangesAsync();
        }

    }
}