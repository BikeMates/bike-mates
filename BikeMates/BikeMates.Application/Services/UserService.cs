using System;
using System.Collections.Generic;
using System.Web;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace BikeMates.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMailService mailSender;


        public UserService(IUserRepository userRepository, IMailService mailSender)
        {
            this.userRepository = userRepository;
            this.mailSender = mailSender;
        }

        public User GetUser(string id)
        {
            return this.userRepository.Find(id);
        }

        public void Delete(string id)
        {
            this.userRepository.Delete(id);
        }

        public void Update(User entity)
        {
            this.userRepository.Update(entity);
        }

        public IdentityResult Register(User user, string password)
        {
            return this.userRepository.Register(user, password);
        }

        public User Login(string email, string password)
        {
            return this.userRepository.Login(email, password);
        }

        public User GetUserByEmail(string email)
        {
            return this.userRepository.GetUserByEmail(email);
        }

        public void ForgotPassword(string id, string host)
        {
            string resetToken = this.userRepository.ForgotPassword(id);
            string message = CreateMessage(id, host, resetToken);
            mailSender.Send(GetUser(id).Email, message);
        }

        private static string CreateMessage(string id, string host, string resetToken)
        {
            resetToken = HttpUtility.UrlEncode(resetToken);
            string resetUrl = string.Format("http://{0}?userId={1}&code={2}#resetpassword", host, id, resetToken);
            string message = string.Format("Please reset your password by clicking <a href=\"{0}\">Reset Password</a><br/> @Or click on the copy the following link on the browser: {0}", resetUrl);

            return message;
        }

        public IdentityResult ChangePassword(string oldPassword, string newPassword, string newPassConfirmation, string id)
        {
            if (!String.IsNullOrEmpty(oldPassword) && !String.IsNullOrEmpty(newPassword) && !String.IsNullOrEmpty(newPassConfirmation)
                && newPassword == newPassConfirmation)
            {
                return this.userRepository.ChangePassword(oldPassword, newPassword, id);
            }

            return IdentityResult.Success;
        }

        public IdentityResult ResetPassword(string id, string code, string password)
        {
            code = HttpUtility.UrlDecode(code);
            return userRepository.ResetPassword(id, code, password);
        }

        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public void UnbanUsers(List<string> userIds)
        {
            userRepository.UnbanUsers(userIds);
        }




        public void BanUser(string userId)
        {
            userRepository.BanUser(userId);
        }
    }
}
