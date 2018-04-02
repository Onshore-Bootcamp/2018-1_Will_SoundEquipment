using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundEquipmentDAL.Models;

namespace SoundEquipmentDAL.Mapping
{
    class MappingUserDO
    {
        private static UserDO DatabaseToObject (DataRow databaseInput)
        {
            UserDO output = new UserDO();

            output.UserID = (int)databaseInput["UserID"];
            output.RoleID = (int)databaseInput["RoleID"];
            output.Username = databaseInput["Username"].ToString();
            output.Password = databaseInput["Password"].ToString();
            output.FirstName = databaseInput["FirstName"].ToString();
            output.LastName = databaseInput["LastName"].ToString();
            output.Email = databaseInput["Email"].ToString();
            output.PhoneNumber = databaseInput["PhoneNumber"].ToString();

            return output;
        }

        public static List<UserDO> MakeListOfObjects(DataTable input)
        {
            List<UserDO> allUsers = new List<UserDO>();

            foreach(DataRow row in input.Rows)
            {
                allUsers.Add(DatabaseToObject(row));
            }

            return allUsers;
        }
    }
}
