using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace подготовка
{
    internal class Login
    {
        private string userId;
        private string password;

        public Login(string userId, string password)
        {
            this.userId = userId;
            this.password = password;
        }

        

        public string GetFullName()
        {
            var user = Logining();
            return user.full_name;
        }
        public string GetRole()
        {
            var role = Logining();
            return role.role;
        }


        public users Logining()
        {

            using (var db = ПодготовкаEntities.GetContext())
            {
                // ищем пользователя по id и паролю
                var user = db.users
                             .FirstOrDefault(u => u.id == userId && u.pasword == password);

                return user;    // если null — логин неуспешен
            }
        }
    }
}
