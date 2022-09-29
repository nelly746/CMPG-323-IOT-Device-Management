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
    public class _CategoryRepository
    {
        private readonly ConnectedOfficeContext _context;

        public _CategoryRepository(ConnectedOfficeContext context)
        {
            _context = context;
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }


        public Task<List<Category>> categoriesGetAll(){
            return _context.Category.ToListAsync();
        }

        public Task<Category> getOneCategory(Guid? id){
            return _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public Task<Category> getCategoryDetails(Guid? id){
            return _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);

        }

        public async void createNewCategory(Category category){
            category.CategoryId = Guid.NewGuid();
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async void CategoryEdit(Guid id, Category category){
            try
            {
                if (CategoryExists(category.CategoryId))
                {
                    _context.Update(category);
                await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
               
                throw;
            }
        }

        public async void confirmDeleteCategory(Guid id){
             var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}