using EMS.Models;

namespace EMS.Service.Interface
{
    public interface IUser
    {
        Task<TblUser> GetByUsernameAsync(string username);
    }
}
