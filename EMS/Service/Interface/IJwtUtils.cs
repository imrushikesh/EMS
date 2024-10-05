using EMS.Models;

namespace EMS.Service.Interface
{
    public interface IJwtUtils
    {
        string GetJwtToken(TblUser user);
    }
}
