using SoundEquipment.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using SoundEquipmentDAL.Models;
using SoundEquipmentDAL;
using System.Configuration;
using SoundEquipment.Mapping;
using System;
using SoundEquipment.ErrorLog;

namespace SoundEquipment.Controllers
{
    public class UserController : Controller
    {
        //constructor business:
        public UserController()
        {
            string connection = ConfigurationManager.ConnectionStrings["soundEquipmentDatabase"].ConnectionString;
            _dataAccess = new UserDAO(connection);
        }
        private readonly UserDAO _dataAccess;

        //Available for role 3:
        public ActionResult Index()
        {
            List<UserDO> allUsers = null;
            List<UserPO> mappedUsers = null;
            ActionResult response = RedirectToAction("Index", "Home");


            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 3))
            {
                try
                {
                    allUsers = _dataAccess.ReadAllUsers();
                    mappedUsers = new List<UserPO>();

                    foreach (UserDO dataObject in allUsers)
                    {
                        mappedUsers.Add(UserMappers.UserDOtoPO(dataObject));
                    }

                    response = View(mappedUsers);
                }

                catch (Exception exception)
                {
                    ErrorLogger.LogExceptions(exception);
                }

                finally
                { }
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        //LOG IN
        [HttpGet]
        public ActionResult Login()
        {
            ActionResult response = null;

            if (Session["RoleID"] == null)
            {
                response = View();
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        [HttpPost]
        public ActionResult Login(LoginPO form)
        {
            ActionResult result = null;

            if (Session["RoleID"] == null)
            {
                if (ModelState.IsValid)
                {
                    UserDO userDataObject = _dataAccess.UserLogIn(form.Username);
                    //Checking if password matches the entered username
                    if (userDataObject != null && form.Password == userDataObject.Password)
                    {
                        try
                        {
                            //Assigning session
                            Session["Username"] = userDataObject.Username;
                            Session["RoleID"] = userDataObject.RoleID;

                            //Login success message:
                            TempData["SuccessMessage"] = "Successful login.";
                            //Redirect:
                            result = RedirectToAction("Index", "Home");
                        }

                        catch (Exception exception)
                        {
                            ErrorLogger.LogExceptions(exception);
                            result = View(form);
                        }

                        finally
                        { }
                    }

                    else
                    {
                        result = View(form);
                    }
                }

                else
                {
                    result = View(form);
                }
            }

            else
            {
                result = RedirectToAction("Index", "Home");
            }

            return result;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            ActionResult response = null;

            if (Session["RoleID"] == null)
            {
                response = View();
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        [HttpPost]
        public ActionResult Register(UserPO form)
        {
            ActionResult response = null;

            if (Session["RoleID"] == null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Default RoleID is 1 because a guest shouldn't be able to create an account with any higher privileges:
                        form.RoleID = 1;
                        UserDO dataObject = UserMappers.UserPOtoDO(form);

                        _dataAccess.CreateUser(dataObject);
                        response = RedirectToAction("Index", "Home");
                    }

                    catch (Exception exception)
                    {
                        ErrorLogger.LogExceptions(exception);
                        response = View(form);
                    }

                    finally
                    { }
                }

                else
                {
                    response = View(form);
                }
            }

            else
            {
                RedirectToAction("Index", "Home");
            }

            return response;
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            ActionResult response = RedirectToAction("Index", "Home");

            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 3))
            {
                response = View();
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        //Available for role 3:
        [HttpPost]
        public ActionResult CreateUser(UserPO form)
        {
            ActionResult response = null;

            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 3))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        UserDO dataObject = UserMappers.UserPOtoDO(form);
                        _dataAccess.CreateUser(dataObject);
                        response = RedirectToAction("Index", "User");
                    }

                    catch (Exception exception)
                    {
                        ErrorLogger.LogExceptions(exception);
                        response = View(form);
                    }

                    finally
                    { }
                }

                else
                {
                    response = View(form);
                }
            }

            else
            {
                RedirectToAction("Index", "Home");
            }

            return response;
        }


        //Update user
        [HttpGet]
        public ActionResult UpdateUser(int UserID)
        {
            UserDO item = null;
            UserPO display = null;
            ActionResult response = RedirectToAction("Index", "Home");

            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 3))
            {
                try
                {
                    //Make sure password is not being called
                    item = _dataAccess.ReadIndividualUserByID(UserID);
                    display = UserMappers.UserDOtoPO(item);
                }

                catch (Exception exception)
                {
                    ErrorLogger.LogExceptions(exception);
                    response = View(UserID);
                }

                finally
                { }

                response = View(display);
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        //Available for role 3:
        [HttpPost]
        public ActionResult UpdateUser(UserPO form)
        {
            ActionResult response = null;

            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 3))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        UserDO dataObject = UserMappers.UserPOtoDOWithoutPassword(form);
                        _dataAccess.UpdateUser(dataObject);
                        response = RedirectToAction("Index", "User");
                    }

                    catch (Exception exception)
                    {
                        ErrorLogger.LogExceptions(exception);
                        response = View(form);
                    }

                    finally
                    { }
                }

                else
                {
                    response = View(form);
                }
            }

            return response;
        }

        //Delete user. Available for role 3:
        [HttpGet]
        public ActionResult DeleteUser(int UserID)
        {
            ActionResult response = null;

            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 3))
            {
                try
                {
                    _dataAccess.DeleteUser(UserID);
                }

                catch (Exception exception)
                {
                    ErrorLogger.LogExceptions(exception);
                    response = View(UserID);
                }

                finally
                { }

                response = RedirectToAction("Index", "User");
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }
    }
}