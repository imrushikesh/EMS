﻿using EMS.Data;
using EMS.Models;
using EMS.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace EMS.Service
{
    public class EmployeeRepository : IRepository<TblEmployee>
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblEmployee>> GetAllAsync()
        {
            return await _context.Employees
                    .Where(u => u.Status == 1)
                    .ToListAsync();
        }

        public async Task<TblEmployee> GetByIdAsync(int id)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(u => u.EmpId == id && u.Status == 1);
        }

        public async Task AddAsync(TblEmployee entity)
        {
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(TblEmployee entity)
        {
            _context.Employees.Update(entity);
            _context.SaveChanges();
        }

        public async void Delete(int id)
        {
            TblEmployee emp = await GetByIdAsync(id);
            emp.Status = 0;
            _context.Employees.Update(emp);
            _context.SaveChanges();
        }
    }
}