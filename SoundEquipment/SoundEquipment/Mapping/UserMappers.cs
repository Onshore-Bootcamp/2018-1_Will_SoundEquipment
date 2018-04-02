using SoundEquipment.Models;
using SoundEquipmentDAL.Models;
using SoundEquipmentBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoundEquipment.Mapping
{
    public class UserMappers
    {
        //for the login controller
        public static UserDO LoginPOtoUserDO(LoginPO input)
        {
            UserDO output = new UserDO();

            output.Username = input.Username;
            output.Password = input.Password;

            return output;
        }

        //Does not map password for sake of security
        public static UserPO UserDOtoPO(UserDO input)
        {
            UserPO output = new UserPO();

            output.UserID = input.UserID;
            output.RoleID = input.RoleID;
            output.Username = input.Username;
            output.FirstName = input.FirstName;
            output.LastName = input.LastName;
            output.Email = input.Email;
            output.PhoneNumber = input.PhoneNumber;

            return output;
        }

        public static UserDO UserPOtoDO (UserPO input)
        {
            UserDO output = new UserDO();

            output.UserID = input.UserID;
            output.RoleID = input.RoleID;
            output.Username = input.Username;
            output.Password = input.Password;

            //TEST:
            output.PasswordConfirm = input.PasswordConfirm;

            output.FirstName = input.FirstName;
            output.LastName = input.LastName;
            output.Email = input.Email;
            output.PhoneNumber = input.PhoneNumber;

            return output;
        }

        //Use for updating user, no update for password
        public static UserDO UserPOtoDOWithoutPassword(UserPO input)
        {
            UserDO output = new UserDO();

            output.UserID = input.UserID;
            output.RoleID = input.RoleID;
            output.Username = input.Username;
            output.FirstName = input.FirstName;
            output.LastName = input.LastName;
            output.Email = input.Email;
            output.PhoneNumber = input.PhoneNumber;

            return output;
        }

        public static UserPO UserBOtoPO (UserBO input)
        {
            UserPO output = new UserPO();

            output.UserID = input.UserID;
            output.RoleID = input.RoleID;
            output.Username = input.Username;
            output.Password = input.Password;
            output.FirstName = input.FirstName;
            output.LastName = input.LastName;
            output.Email = input.Email;
            output.PhoneNumber = input.PhoneNumber;

            return output;
        }

        public static UserBO UserPOtoBO (UserPO input)
        {
            UserBO output = new UserBO();

            output.UserID = input.UserID;
            output.RoleID = input.RoleID;
            output.Username = input.Username;
            output.Password = input.Password;
            output.FirstName = input.FirstName;
            output.LastName = input.LastName;
            output.Email = input.Email;
            output.PhoneNumber = input.PhoneNumber;

            return output;
        }

        public static UserDO UserBOtoDO (UserBO input)
        {
            UserDO output = new UserDO();

            output.UserID = input.UserID;
            output.RoleID = input.RoleID;
            output.Username = input.Username;
            output.Password = input.Password;
            output.FirstName = input.FirstName;
            output.LastName = input.LastName;
            output.Email = input.Email;
            output.PhoneNumber = input.PhoneNumber;

            return output;
        }

        public static UserBO UserDOtoBO (UserDO input)
        {
            UserBO output = new UserBO();

            output.UserID = input.UserID;
            output.RoleID = input.RoleID;
            output.Username = input.Username;
            output.Password = input.Password;
            output.FirstName = input.FirstName;
            output.LastName = input.LastName;
            output.Email = input.Email;
            output.PhoneNumber = input.PhoneNumber;

            return output;
        }
    }
}