using System;
using System.Collections.Generic;
using System.Linq;
using web.Models;

namespace web.Repository
{
    public static class Repository
    {
        public static List<UserModel> GetAllUsers()
        {
            return new List<UserModel>
            {
                new UserModel
                {
                    Id=101,
                    UserName="john",
                    Password="test",
                    DateOfBirth=DateTime.UtcNow,
                    Role="Admin",
                    FavColor="Red"
                },
                new UserModel
                {
                    Id=102,
                    UserName="dave",
                    Password="test1",
                    DateOfBirth=DateTime.UtcNow,
                    Role="Guest",
                    FavColor="Blue"
                },
                new UserModel
                {
                    Id=103,
                    UserName="mary",
                    Password="test2",
                    DateOfBirth=DateTime.UtcNow,
                    Role="User",
                    FavColor="Green"
                },
                new UserModel
                {
                    Id=104,
                    UserName="sheela",
                    Password="test3",
                    DateOfBirth=DateTime.UtcNow,
                    Role="Guest",
                    FavColor="Black"
                }
            };
        }

        public static UserModel GetUser(LoginViewModel model)
        {
            return GetAllUsers()
                .Where(x => x.UserName == model.UserName && x.Password == model.Password)
                .FirstOrDefault();
        }
    }
}
