using EMS.Data;
using EMS.Models;
using EMS.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace EMS.Service
{
    public class UserRepository : IRepository<TblUser>, IUser
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblUser>> GetAllAsync()
        {
            return await _context.Users
                    .Where(u => u.Status == 1)
                    .ToListAsync();
        }

        public async Task<TblUser> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id && u.Status == 1);
        }

        public async Task AddAsync(TblUser entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(TblUser entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }

        public async void Delete(int id)
        {
            TblUser user = await GetByIdAsync(id);
            user.Status = 0;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public async Task<TblUser> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username && u.Status == 1);
        }
    }
}