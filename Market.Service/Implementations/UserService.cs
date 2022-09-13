using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll().ToListAsync();

                if(users != null)
                {
                    return new BaseResponse<IEnumerable<User>>
                    {
                        Data = users,
                        Status = Domain.Enum.StatusCode.Ok,                       
                    };
                }
                else
                {
                    return new BaseResponse<IEnumerable<User>>
                    {
                        Description = "В базе данных нету записей",
                        Status = Domain.Enum.StatusCode.NotFound
                    };
                }
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>
                {
                    Description = $"[GetUsers]: {ex.Message}",
                    Status = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public Task<BaseResponse<bool>> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<User>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(user => user.Id == id);

                if(user != null)
                {
                    return new BaseResponse<User>
                    {
                        Data = user,
                        Status = Domain.Enum.StatusCode.Ok
                    };
                }
                else
                {
                    return new BaseResponse<User>
                    {
                        Description = "В базе данных нету записей",
                        Status = Domain.Enum.StatusCode.Ok
                    };
                }
            }
            catch(Exception ex)
            {
                return new BaseResponse<User>
                {
                    Description = $"[GetUser]: {ex.Message}",
                    Status = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }       

        public Task<BaseResponse<bool>> UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
