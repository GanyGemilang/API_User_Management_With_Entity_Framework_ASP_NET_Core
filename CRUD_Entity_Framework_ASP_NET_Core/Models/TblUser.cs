using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CRUD_Entity_Framework_ASP_NET_Core.Models
{
    public partial class TblUser
    {
        [Key]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiredToken { get; set; }
        public bool? Online { get; set; }
    }
}
