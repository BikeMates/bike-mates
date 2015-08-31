using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Policy;
using System.Collections;

namespace BikeMates.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User GetUser(string id)
        {
            return this.userRepository.Get(id);
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

        public User getUserByEmail(string email)
        {
            return this.userRepository.getUserByEmail(email);
        }
        public void forgotPassword(string id, string host)
        {
            string resetToken = this.userRepository.forgotPassword(id);
            resetToken = System.Web.HttpUtility.UrlEncode(resetToken);

            string resetUrl = "http://" + host +
                "?userId=" + id +
                "&code=" + resetToken+"#resetpassword";

            string message = "Please reset your password by clicking <a href=\"" + resetUrl + "\">Reset Password</a><br/>" + 
            @"Or click on the copy the following link on the browser:" + resetUrl;

            sendMail(id, message);
        }

        public IdentityResult ChangePassword(string oldPassword, string newPassword, string newPassConfirmation, string id)
        {
            if (!(String.IsNullOrEmpty(oldPassword)) && !(String.IsNullOrEmpty(newPassword)) && !(String.IsNullOrEmpty(newPassConfirmation))
                && newPassword == newPassConfirmation)
            {
                return this.userRepository.ChangePassword(oldPassword, newPassword, id);
            }
            //Cause all fields are empty means user do not want to change password
            return IdentityResult.Success;
        }

        private void sendMail(string userId, string message)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("BikeMatesUkraine@gmail.com");
            msg.To.Add(new MailAddress(GetUser(userId).Email));
            msg.Subject = "Reset Password";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html));
            msg.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("BikeMatesUkraine@gmail.com", "Qwerty1#");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }


        public IdentityResult resetPassword(string id, string code, string password)
        {
            code = System.Web.HttpUtility.UrlDecode(code);
            return userRepository.resetPassword(id, code, password);
        }


        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll();
        }


    }
}
