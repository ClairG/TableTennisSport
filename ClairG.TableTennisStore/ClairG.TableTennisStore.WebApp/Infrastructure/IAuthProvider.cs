using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClairG.TableTennisStore.WebApp.Infrastructure
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}