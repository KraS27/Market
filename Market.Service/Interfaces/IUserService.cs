using Market.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<User>>> GetUsers();

        Task<BaseResponse<User>> GetUser(int id);

        Task<BaseResponse<bool>> DeleteUser(int id);

        Task<BaseResponse<bool>> UpdateUser(int id, User user);
    }
}
