using SoundEquipment.ErrorLog;
using SoundEquipment.Mapping;
using SoundEquipment.Models;
using SoundEquipmentBLL.Models;
using SoundEquipmentDAL;
using SoundEquipmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoundEquipment.Controllers
{
    public class OrderController : Controller
    {
        //Constructor:
        public OrderController()
        {
            string connection = ConfigurationManager.ConnectionStrings["soundEquipmentDatabase"].ConnectionString;
            _orderDataAccess = new OrderDAO(connection);
            _productDataAccess = new ProductDAO(connection);
        }
        private readonly OrderDAO _orderDataAccess;
        private readonly ProductDAO _productDataAccess;

        // GET: Order
        public ActionResult Index()
        {
            List<OrderPO> mappedOrders = null;
            List<OrderDO> allOrders = null;

            try
            {
                allOrders = _orderDataAccess.ReadAllOrders();

                mappedOrders = new List<OrderPO>();

                foreach (OrderDO dataObject in allOrders)
                {
                    mappedOrders.Add(OrderMappers.OrderDOtoPO(dataObject));
                }
            }

            catch (Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
            }

            finally
            { }

            return View(mappedOrders);
        }

        //Add order.
        [HttpGet]
        public ActionResult CreateOrder()
        {
            OrderPO order = new OrderPO();
            ActionResult response = RedirectToAction("Index", "Home");

            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 2 || (int)Session["RoleID"] == 3))
            {
                //Filling selectlist
                foreach (ProductDO dataObject in _productDataAccess.ReadAllProducts())
                {
                    //declaring a selectlistitem for the list in the OrderPO property ProductsDropDown
                    SelectListItem listItem = new SelectListItem();
                    //Assigning the product's name to the listitem's text
                    listItem.Text = dataObject.Name;
                    //Assigning the product's ID to the listitem's value
                    listItem.Value = dataObject.ProductID.ToString();
                    //Adding the listitem, with its text and value, to the ProductsDropDown property of the OrderPO object
                    order.ProductsDropDown.Add(listItem);
                }

                //Could instead make quantity not an int but make it required that it's an int? So as to not have a character
                //show up when the form loads.
                order.Quantity = 1;

                response = View(order);
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        //Role 2 and higher can create an order:
        [HttpPost]
        public ActionResult CreateOrder(OrderPO form)
        {
            ActionResult response = null;


            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 2 || (int)Session["RoleID"] == 3))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        OrderDO dataObject = OrderMappers.OrderPOtoDO(form);
                        _orderDataAccess.CreateOrder(dataObject);
                        response = RedirectToAction("Index", "Order");
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
                    //Returning a list of products from the readall function in the DAL
                    foreach (ProductDO dataObject in _productDataAccess.ReadAllProducts())
                    {
                        //declaring a selectlistitem for the list in the OrderPO property ProductsDropDown
                        SelectListItem listItem = new SelectListItem();
                        //Assigning the product's name to the listitem's text
                        listItem.Text = dataObject.Name;
                        //Assigning the product's ID to the listitem's value
                        listItem.Value = dataObject.ProductID.ToString();

                        //Adding the listitem, with its text and value, to the ProductsDropDown property of the OrderPO object
                        form.ProductsDropDown.Add(listItem);
                    }

                    response = View(form);
                }
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        //Update order.
        [HttpGet]
        public ActionResult UpdateOrder(int orderID)
        {
            OrderDO item = null;
            OrderPO display = null;
            ActionResult response = RedirectToAction("Index", "Home");

            //Available to all roles
            if (Session["RoleID"] != null)
            {
                try
                {
                    item = _orderDataAccess.ReadIndividualOrder(orderID);
                    display = OrderMappers.OrderDOtoPO(item);

                    //Filling selectlist
                    foreach (ProductDO dataObject in _productDataAccess.ReadAllProducts())
                    {
                        //declaring a selectlistitem for the list in the OrderPO property ProductsDropDown
                        SelectListItem listItem = new SelectListItem();
                        //Assigning the product's name to the listitem's text
                        listItem.Text = dataObject.Name;
                        //Assigning the product's ID to the listitem's value
                        listItem.Value = dataObject.ProductID.ToString();

                        //Adding the listitem, with its text and value, to the ProductsDropDown property of the OrderPO object
                        display.ProductsDropDown.Add(listItem);
                    }

                    //Setting the dropdown list to the correct product:
                    display.ProductID = item.ProductID;
                }

                catch (Exception exception)
                {
                    ErrorLogger.LogExceptions(exception);
                    response = View(orderID);
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

        //Role 1 and higher can update an order:
        [HttpPost]
        public ActionResult UpdateOrder(OrderPO form)
        {
            ActionResult response = null;

            //Available to all roles
            if (Session["RoleID"] != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        OrderDO dataObject = OrderMappers.OrderPOtoDO(form);
                        _orderDataAccess.UpdateOrder(dataObject);
                        response = RedirectToAction("Index", "Order");
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
                    try
                    {
                        OrderDO item = _orderDataAccess.ReadIndividualOrder(form.OrderID);
                        //OrderPO display = OrderMappers.OrderDOtoPO(item);

                        //GETTING LIST OF PRODUCTS
                        foreach (ProductDO dataObject in _productDataAccess.ReadAllProducts())
                        {
                            //declaring a selectlistitem for the list in the OrderPO property ProductsDropDown
                            SelectListItem listItem = new SelectListItem();
                            //Assigning the product's name to the listitem's text
                            listItem.Text = dataObject.Name;
                            //Assigning the product's ID to the listitem's value
                            listItem.Value = dataObject.ProductID.ToString();

                            //Adding the listitem, with its text and value, to the ProductsDropDown property of the OrderPO object

                            //Previous was:
                            //display.ProductsDropDown.Add(listItem);
                            //Now trying:
                            form.ProductsDropDown.Add(listItem);
                        }
                    }

                    catch (Exception exception)
                    {
                        ErrorLogger.LogExceptions(exception);
                        response = View(form);
                    }

                    finally
                    { }

                    response = View(form);
                }
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        //Delete order. Role 3 can delete an order:
        [HttpGet]
        public ActionResult DeleteOrder(int OrderID)
        {
            ActionResult response = null;

            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 3))
            {
                try
                {
                    _orderDataAccess.DeleteOrder(OrderID);
                }

                catch (Exception exception)
                {
                    ErrorLogger.LogExceptions(exception);
                    response = View(OrderID);
                }

                finally
                { }

                response = RedirectToAction("Index", "Order");
            }

            else
            {
                response = RedirectToAction("Login", "User");
            }

            return response;
        }

        //Get product names for dropdown menu in Order view:
        public List<ProductPO> GetListOfProducts()
        {
            List<ProductDO> allProducts = _productDataAccess.ReadAllProducts();
            List<ProductPO> mappedProducts = new List<ProductPO>();

            //Maps all properties, not just the name:
            foreach (ProductDO dataObject in allProducts)
            {
                mappedProducts.Add(ProductMappers.ProductDOtoPO(dataObject));
            }

            return mappedProducts;
        }

    }
}