using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Helpers;
using Market.Domain.ViewModels;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> userRepository;

        public AccountService(IBaseRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x =>
                                                         x.Name == model.Name);
                if(user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Status = Domain.Enum.StatusCode.NotFound
                    };
                }
                else
                {
                    if (!HashPasswordHelper.VerifyHashedPassword(user.Password, model.Password))
                    {
                        return new BaseResponse<ClaimsIdentity>()
                        {
                            Description = "Passwords do not match"
                        };
                    }

                    var result = Authenticate(user);
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = result,
                        Status = Domain.Enum.StatusCode.Ok
                    };
                }
            }
            catch(Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = $"[Login]: {ex.Message}",
                    Status = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
      
        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await userRepository.GetAll()
                                               .FirstOrDefaultAsync(x => 
                                               x.Name == model.Name);
                if(user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "User with this name is already registered"
                    };
                }
                else
                {
                    user = new User()
                    {
                        Name = model.Name,
                        Password = HashPasswordHelper.HashPassword(model.Password),
                        Role = Domain.Enum.UserRole.DefaultUser
                    };
                    await userRepository.Create(user);
                    var result = Authenticate(user);

                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = result,
                        Status = Domain.Enum.StatusCode.Ok
                    };
                }             
            }
            catch(Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = $"[Register]: {ex.Message}",
                    Status = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
       
        public ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            return new ClaimsIdentity(claims, "ApplicationCookie",
                       ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
