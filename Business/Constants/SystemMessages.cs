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
        public static string AccessTokenCreated = "Giriş token-i yaradıldı";
        public static string UserNotFound = "İstifadəçi tapılmadı";
        public static string PasswordError = "Şifrə xətası";
        public static string SuccessfulLogin = "Uğurlu giriş";
        public static string UserRegistered = "İstifadəçi sistemə əlavə olundu";
        public static string UserAlreadyExists = "Belə istifadəçi artıq mövcuddur";
    }
}
