using CRUD_Entity_Framework_ASP_NET_Core.Repositories;
using CRUD_Entity_Framework_ASP_NET_Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Entity_Framework_ASP_NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly LogicRepository logicRepository;
        public readonly UserRepository userRepository;
        public UserController(LogicRepository logicRepository, UserRepository userRepository)
        {
            this.logicRepository = logicRepository;
            this.userRepository = userRepository;
        }

        //Create
        [HttpPost("Registrasi")]
        public IActionResult Registrasi(inputUserVM user)
        {
            try
            {
                if (user.username == null)
                {
                    return StatusCode(400, Utilities.Response.ResponseMessage("400", "False", "username cannot be null", user));
                }
                else if (user.password == null)
                {
                    return StatusCode(400, Utilities.Response.ResponseMessage("400", "False", "password cannot be null", user));
                }

                //Cek Logic Register
                var logic = logicRepository.LogicRegister(user);
                if (logic.Code != "200")
                {
                    return StatusCode(int.Parse(logic.Code), Utilities.Response.ResponseMessage(logic.Code, "False", logic.Hasil, null));
                }
                user.password = logic.Hasil;

                //Insert Data User
                var insertUser = userRepository.InsertUser(user);
                if (insertUser < 1)
                {
                    return StatusCode(404, Utilities.Response.ResponseMessage("404", "False", "Insert User Failed", user));
                }
                return Ok(Utilities.Response.ResponseMessage("200", "True", "Insert User Successful", user));
            }
            catch (Exception e)
            {
                return StatusCode(500, Utilities.Response.ResponseMessage("500", "False", e.Message, null));
            }

        }
        //Create
        [HttpPost("Login")]
        public IActionResult Login(inputLoginVM login)
        {
            try
            {
                if (login.username == null)
                {
                    return StatusCode(400, Utilities.Response.ResponseMessage("400", "False", "username cannot be null", login));
                }
                else if (login.password == null)
                {
                    return StatusCode(400, Utilities.Response.ResponseMessage("400", "False", "password cannot be null", login));
                }

                var logic = logicRepository.LogicLogin(login);
                if (logic.Code != "200")
                {
                    return StatusCode(int.Parse(logic.Code), Utilities.Response.ResponseMessage(logic.Code, "False", logic.Hasil, null));
                }

                //Update ke tbl_user
                var updateLogin = userRepository.Login(logic.Hasil);
                if (updateLogin < 1)
                {
                    return StatusCode(404, Utilities.Response.ResponseMessage("404", "False", "Filed Update Login", login));
                }
                return Ok(Utilities.Response.ResponseMessage("200", "True", "Login Successful", new { Token = logic.Token }));
            }
            catch (Exception e)
            {
                return StatusCode(500, Utilities.Response.ResponseMessage("500", "False", e.Message, null));
            }
        }
        //Update
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("Logout")]
        public IActionResult Logout(logoutVM logout)
        {
            try
            {
                if (logout.username == null)
                {
                    return StatusCode(400, Utilities.Response.ResponseMessage("400", "False", "username cannot be null", logout));
                }

                var Logout = userRepository.Logout(logout.username);
                if (Logout < 1)
                {
                    return StatusCode(404, Utilities.Response.ResponseMessage("404", "False", "Filed Update Login", Logout));
                }
                return Ok(Utilities.Response.ResponseMessage("200", "True", "Logout Successful", Logout));
            }
            catch (Exception e)
            {
                return StatusCode(500, Utilities.Response.ResponseMessage("500", "False", e.Message, null));
            }
        }

        //GetByUsername
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetUserByUsername")]
        public IActionResult GetUserByUsername(string username)
        {
            try
            {
                if (username == null)
                {
                    return StatusCode(400, Utilities.Response.ResponseMessage("400", "False", "username cannot be null", null));
                }

                var data = userRepository.GetUserbyusername(username);
                if (data.Count < 1)
                {
                    return StatusCode(404, Utilities.Response.ResponseMessage("404", "False", "Filed Update Login", null));
                }
                return Ok(Utilities.Response.ResponseMessage("200", "True", "Get data User Successful", data.FirstOrDefault()));
            }
            catch (Exception e)
            {
                return StatusCode(500, Utilities.Response.ResponseMessage("500", "False", e.Message, null));
            }
        }

        //Get All
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            try
            {
                var data = userRepository.GetUser();
                if (data.Count() < 1)
                {
                    return StatusCode(404, Utilities.Response.ResponseMessage("404", "False", "Data Not Found", null));
                }
                else
                {
                    return Ok(Utilities.Response.ResponseMessage("200", "True", "Get data User Successful", data));
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, Utilities.Response.ResponseMessage("500", "False", e.Message, null));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(string username)
        {
            try
            {
                if (username == null)
                {
                    return StatusCode(400, Utilities.Response.ResponseMessage("400", "False", "username cannot be null", username));
                }

                var data = userRepository.DeleteUser(username);
                var Hasil = data;
                if (Hasil.Code != "200")
                {
                    return StatusCode(int.Parse(Hasil.Code), Utilities.Response.ResponseMessage(Hasil.Code, "False", Hasil.Message, null));
                }
                return Ok(Utilities.Response.ResponseMessage("200", "True", Hasil.Message, username));
            }
            catch (Exception e)
            {
                return StatusCode(500, Utilities.Response.ResponseMessage("500", "False", e.Message, null));
            }
        }

    }
}
