using AutoMapper;
using Newtonsoft.Json;
using SAW.BLL.UnitOfWork;
using SAW.DAL.Context;
using SAW.WebApi.Dtos;
using SAW.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace SAW.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private const string apiAccessToken = "saw-api-xd-bx-ipa-was";
        private IUnitOfWork _uow;

        public UsersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IHttpActionResult Users(string accessToken)
        {
            #region Control access token

            if (accessToken != apiAccessToken)
                return Unauthorized();

            #endregion

            #region Mapping proccess for return

            var users = _uow.UserManager.ListAll(x => x.IsActive);

            List<UserForListDto> usersToReturn = new List<UserForListDto>();
            Mapper.Map(users, usersToReturn);

            return Ok(usersToReturn);

            #endregion
        }

        [HttpGet]
        [Route("api/{users}/{id}")]
        public IHttpActionResult UserDetail(int id, string accessToken)
        {
            #region Control access token 

            if (accessToken != apiAccessToken)
                return Unauthorized();

            #endregion

            #region Mapping proccess for return

            var user = _uow.UserManager.Get(x => x.UserId == id);

            UserDto userToReturn = new UserDto();
            Mapper.Map(user, userToReturn);

            return Ok(userToReturn);

            #endregion
        }

        [HttpGet]
        [Route("api/{users}/{id}/role")]
        public IHttpActionResult UserRole(int id, string accessToken)
        {
            #region Control access token 

            if (accessToken != apiAccessToken)
            {
                throw new HttpException(401, "Unauthorized access");
            }

            #endregion

            #region Mapping proccess for return

            var role = _uow.UserManager.Get(x => x.UserId == id).Role;

            UserRoleDto userRoleToReturn = new UserRoleDto();
            Mapper.Map(role, userRoleToReturn);

            return Ok(userRoleToReturn);

            #endregion
        }

        [HttpPost]
        [Route("api/{users}/create")]
        public IHttpActionResult Create(User model, string accessToken)
        {
            #region Control access token 

            if (accessToken != apiAccessToken)
                return Unauthorized();

            #endregion

            #region Insertion proccess

            var result = _uow.UserManager.Add(model);
            if (!_uow.SaveChanges())
                return BadRequest();

            return Ok();

            #endregion
        }

        [HttpPost]
        [Route("api/{users}/update")]
        public IHttpActionResult Update(User model, string accessToken)
        {
            #region Control access token 

            if (accessToken != apiAccessToken)
                return Unauthorized();

            #endregion

            #region Update proccess

            var result = _uow.UserManager.Update(model);
            if (!_uow.SaveChanges())
                return BadRequest();

            return Ok();

            #endregion
        }

        [HttpGet]
        [Route("api/{users}/delete")]
        public IHttpActionResult Delete(int id, string accessToken)
        {
            #region Control access token 

            if (accessToken != apiAccessToken)
                return Unauthorized();

            #endregion

            #region Delete proccess

            var result = _uow.UserManager.Delete(id);
            if (!_uow.SaveChanges())
                return BadRequest();

            return Ok();

            #endregion
        }

        [HttpPost]
        [Route("api/users/login")]
        public ApiReturn Login([FromBody]UserLogin model, string accessToken)
        {
            #region Control access token and user

            if (accessToken != apiAccessToken)
                return ApiReturn.Unauthorized();

            var user = _uow.UserManager.Get(x => x.Email == model.Email);
            if (user == null)
                return ApiReturn.NotFound("There is no user with this email in our system.");

            user = _uow.UserManager.Get(x => x.Email == model.Email && x.Password == model.Password);
            if (user == null)
                return ApiReturn.NotFound("Your email or password is incorrect.");

            #endregion

            #region Mapping proccess for return

            user = _uow.UserManager.Get(x => x.Email == model.Email && x.Password == model.Password);
            UserDto userToReturn = new UserDto();
            Mapper.Map(user, userToReturn);

            return ApiReturn.Successful(userToReturn);
            //return userToReturn;

            #endregion
        }
    }
}
