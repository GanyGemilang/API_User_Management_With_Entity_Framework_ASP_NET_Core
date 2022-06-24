using CRUD_Entity_Framework_ASP_NET_Core.Models;
using CRUD_Entity_Framework_ASP_NET_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Entity_Framework_ASP_NET_Core.Repositories
{
    public class UserRepository
    {
        private readonly UserManagementContext _context;
        public UserRepository(UserManagementContext _context)
        {
            this._context = _context;
        }
        public List<TblUser> CekUser(string username, string email)
        {
            var data = _context.TblUsers.Where(a => a.Username == username && a.Email == email);

            return data.ToList();
        }

        public int InsertUser(inputUserVM user)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    TblUser insertUser = (new TblUser
                    {
                        Username = user.username,
                        Name = user.name,
                        Password = user.password,
                        Role = user.role,
                        Email = user.email,
                        Token = null,
                        ExpiredToken = null,
                        Online = false
                    });

                    var result = _context.TblUsers.Add(insertUser);
                    _context.SaveChanges();
                    
                    transaction.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public List<TblUser> GetUserbyusername(string username)
        {
            var data = _context.TblUsers.Where(a => a.Username == username);
            return data.ToList();
        }

        public List<TblUser> GetUser()
        {
            var data = _context.TblUsers.ToList();
            return data.ToList();
        }
        public int Login(insertLoginVM login)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = _context.TblUsers.FirstOrDefault(a => a.Username == login.username);
                    data.Online = login.online;
                    data.Token = login.token;
                    data.ExpiredToken = login.expiredToken;

                    _context.SaveChanges();

                    transaction.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public int Logout(string username)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = _context.TblUsers.FirstOrDefault(a => a.Username == username);
                    data.Token = null;
                    data.ExpiredToken = null;
                    data.Online = false;
                    _context.SaveChanges();

                    transaction.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public dynamic DeleteUser(string username)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = _context.TblUsers.Where(a => a.Username == username);
                    var delData = data.FirstOrDefault();
                    if (data.Count() < 1)
                    {
                        return new { Code = "404", Message = "Data Not Found" };
                    }
                    _context.TblUsers.Remove(delData);
                    _context.SaveChanges();

                    transaction.Commit();
                    return new { Code = "200", Message = "Delete User Successful" };
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
    }
}
