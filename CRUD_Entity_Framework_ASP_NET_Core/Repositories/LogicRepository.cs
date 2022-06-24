using CRUD_Entity_Framework_ASP_NET_Core.Utilities;
using CRUD_Entity_Framework_ASP_NET_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Entity_Framework_ASP_NET_Core.Repositories
{
    public class LogicRepository
    {
        public readonly UserRepository userRepository;
        public readonly jwtToken jwtToken;
        public LogicRepository(UserRepository userRepository, jwtToken jwtToken)
        {
            this.userRepository = userRepository;
            this.jwtToken = jwtToken;
        }

        public dynamic LogicRegister(inputUserVM user)
        {
            //Cek Username and Email
            var getuser = userRepository.CekUser(user.username, user.email);

            if (getuser.Count > 0)
            {
                return new { Code = "400", Hasil = "Username or email already exists, please change it" };
            }

            //Encrypt Password
            string passwordhash = BCrypt.Net.BCrypt.HashPassword(user.password);
            return new { Code = "200", Hasil = passwordhash };

        }

        public dynamic LogicLogin(inputLoginVM login)
        {
            //Get data By Username
            var getuser = userRepository.GetUserbyusername(login.username);
            var user = getuser.FirstOrDefault();
            if (getuser.Count < 1)
            {
                return new { Code = "404", Hasil = "Username Not Found" };
            }
            //Cek Password
            if (BCrypt.Net.BCrypt.Verify(login.password, user.Password) == false)
            {
                return new { Code = "404", Hasil = "Password is wrong" };
            }
            else if (user.Online == true)
            {
                if (user.ExpiredToken >= Utilities.Time.timezone(DateTime.Now))
                {
                    return new { Code = "404", Hasil = "user is online" };
                }

            }
            //Token JWT
            var jwt = jwtToken.jwt(login.username);
            //Update tbl_user
            insertLoginVM data = new insertLoginVM();
            data.username = user.Username;
            data.token = jwt.Token;
            data.expiredToken = jwt.timejwt;
            data.online = true;
            return new { Code = "200", Hasil = data, Token = jwt.Token };
        }

    }
}
