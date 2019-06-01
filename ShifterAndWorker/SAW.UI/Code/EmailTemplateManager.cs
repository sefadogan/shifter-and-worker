using SAW.Core;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAW.UI.Code
{
    public class EmailTemplateManager
    {
        public static AppReturn SendEmailForForgottenPassword(User user)
        {
            AppReturn result = new AppReturn();

            bool isSent = Helpers.SendEmail.SendEmailSmtp(user.Email,
                "Shifter and Worker User Informations",
                string.Format(
                    $"<p>Dear {user.FirstName + " " + user.LastName},</p>" +
                    "<p>You can see your user information details below.</p>" +
                    $"<p>Email: {user.Email}</p>" +
                    $"<p>Password: {user.Password}</p>" +
                    "<p>Regards,</p>" +
                    "<p>Shifter and Worker</p>"),
                    new List<string>() { user.Email });

            if (!isSent)
            {
                result.Success = false;
                result.Message = "An error has occured when mail sending.";
                return result;
            }

            result.Success = true;
            result.Message = "Password sent successfuly! Please check your inbox.";
            return result;
        }
    }
}