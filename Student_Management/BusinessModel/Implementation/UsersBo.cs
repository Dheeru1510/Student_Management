using BusinessModel.Abstraction;
using DataModel;
using System;
using System.Linq;

namespace BusinessModel.Implementation
{
    public class UsersBo : IUsersBO
    {
        public string UserVerify(string userName, string password)
        {
            using (var db = new StudentManagementEntities())
            {
                var res = db.Users.Where(p => p.UserName == userName && p.Password == password).FirstOrDefault();
                if (res == null)
                    return string.Empty;
                else
                    return Convert.ToString(res.UID);
            }
        }
    }
}
