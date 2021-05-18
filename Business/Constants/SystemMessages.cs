using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class SystemMessages
    {
        public static string AuthorizationDenied = "İcazə yoxdur";

        public static string AccessTokenCreated { get; internal set; }
        public static User UserNotFound { get; internal set; }
        public static User PasswordError { get; internal set; }
        public static string SuccessfulLogin { get; internal set; }
        public static string UserRegistered { get; internal set; }
        public static string UserAlreadyExists { get; internal set; }
    }
}
