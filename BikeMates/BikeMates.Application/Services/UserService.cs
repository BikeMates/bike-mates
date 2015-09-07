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

        public IEnumerable<string> ChangePassword(string oldPassword, string newPassword, string newPasswordConfirmation, string id)
        {
            List<string> passwordErrors = new List<string>();
            passwordErrors.AddRange(this.CheckUserPassword(oldPassword, newPassword, newPasswordConfirmation));

            if (!String.IsNullOrWhiteSpace(oldPassword) && !String.IsNullOrWhiteSpace(newPassword) && !String.IsNullOrWhiteSpace(newPasswordConfirmation)
                && newPassword == newPasswordConfirmation)
            {
                IdentityResult passwordResult = this.userRepository.ChangePassword(oldPassword, newPassword, id);
                if (!passwordResult.Succeeded)
                    {
                        List<string> identityResultErrors = new List<string>(passwordResult.Errors);
                        passwordErrors.AddRange(identityResultErrors);
                    }
            }
            return passwordErrors;
           
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

        public IEnumerable<string> CheckUserName(User entity)
        { 
            List<string> nameErrors = new List<string>();
          
            if (String.IsNullOrWhiteSpace(entity.FirstName))
            { nameErrors.Add("First name field cant be empty"); }

            if (String.IsNullOrWhiteSpace(entity.SecondName))
            {nameErrors.Add("Second name field cant be empty"); }

            return nameErrors;
        }

        public IEnumerable<string> CheckUserInfo(string firstName, string secondName, string about, string userId)
        {
            List<string> informationStatus = new List<string>();
            User user = this.GetUser(userId);

            if ( user.FirstName != firstName  )
                {
                    informationStatus.Add("First name changed");
                }
            if (user.SecondName != secondName)
                {
                    informationStatus.Add("Second name changed");
                }
            if (user.About != about)
                {
                    informationStatus.Add("About changed");
                }

            return informationStatus;
        }

        public IEnumerable<string> CheckUserName(string firstName, string secondName)
        {
            List<string> nameErrors = new List<string>();

            if ( String.IsNullOrWhiteSpace(firstName))
                {
                    nameErrors.Add("First name cannot be empty");
                }
            if (String.IsNullOrWhiteSpace(secondName))
                {
                    nameErrors.Add("Second name cannot be empty");
                }

            return nameErrors;
        }

        public IEnumerable<string> CheckUserPassword(string oldPassword, string newPassword, string newPasswordConfirmation)
        {
            List<string> passwordErrors = new List<string>();
            if (!String.IsNullOrWhiteSpace(oldPassword) && !String.IsNullOrWhiteSpace(newPassword) && !String.IsNullOrWhiteSpace(newPasswordConfirmation))
            {
                if ( newPassword  != newPasswordConfirmation)
                    {
                      passwordErrors.Add("New password and password confirmation does not match");
                    }
                if ( newPassword.Length < 6 )
                    {
                        passwordErrors.Add("New password is less than 6 symbols");
                    }
                if ( newPasswordConfirmation.Length < 6 )
                    {
                        passwordErrors.Add("New password confirmation is less than 6 symbols");
                    }
            }
            return passwordErrors;
        }


        public void BanUser(string userId)
        {
            userRepository.BanUser(userId);
        }
    }
}
