using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClairG.TableTennisStore.Domain.Entities
{
    public class LoginUser
    {
        public int LoginUserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Passsord { get; set; }
    }
}
