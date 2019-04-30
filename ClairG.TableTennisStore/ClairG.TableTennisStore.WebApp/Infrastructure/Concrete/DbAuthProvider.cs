using ClairG.TableTennisStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ClairG.TableTennisStore.WebApp.Infrastructure.Concrete
{
    public class DbAuthProvider : IAuthProvider 
    {
        private EFDbContext _dbContext; 

        public DbAuthProvider(EFDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public bool Authenticate(string username, string password)
        {
            var loginUser = _dbContext.loginUsers.FirstOrDefault(i => i.Username == username);

            if (loginUser == null)
            {
                return false;
            }
            else
            {
                if (loginUser.Passsord == password) 
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return true;
                }
            }

            return false;
        }
    }
}