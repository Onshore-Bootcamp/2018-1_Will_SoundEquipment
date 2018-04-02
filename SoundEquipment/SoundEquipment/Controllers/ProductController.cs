using SoundEquipment.ErrorLog;
using SoundEquipment.Mapping;
using SoundEquipment.Models;
using SoundEquipmentBLL;
using SoundEquipmentDAL;
using SoundEquipmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace SoundEquipment.Controllers
{
    public class ProductController : Controller
    {
        //constructor business:
        public ProductController()
        {
            string connection = ConfigurationManager.ConnectionStrings["soundEquipmentDatabase"].ConnectionString;
            _dataAccess = new ProductDAO(connection);
            _businessLayerAccess = new ClassForBLLMethod(connection);
        }
        private readonly ProductDAO _dataAccess;
        private readonly ClassForBLLMethod _businessLayerAccess;

        // GET: Product
        public ActionResult Index()
        {
            List<ProductDO> allProducts = null;
            List<ProductPO> mappedProducts = null;

            try
            {
                //Getting the index:
                allProducts = _dataAccess.ReadAllProducts();
                mappedProducts = new List<ProductPO>();

                foreach (ProductDO dataObject in allProducts)
                {
                    mappedProducts.Add(ProductMappers.ProductDOtoPO(dataObject));
                }

                //For most common manufacturer:
                List<ProductDO> productsList = new List<ProductDO>();
                List<string> manufacturerList = new List<string>();
                List<string> mostCommonManufacturer;

                foreach (ProductDO dataObject in productsList = _dataAccess.ReadAllProducts())
                {
                    string manufacturerString = dataObject.Manufacturer;
                    //Add each manufacturer to the list:
                    manufacturerList.Add(manufacturerString);
                }

                mostCommonManufacturer = _businessLayerAccess.GetMostFrequentManufacturer(manufacturerList);
                ViewBag.mostCommonManufacturer = string.Join(", ", mostCommonManufacturer.ToArray());
            }

            catch (Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
            }

            finally
            { }

            return View(mappedProducts);
        }

        //Add product
        [HttpGet]
        public ActionResult CreateProduct()
        {
            ActionResult response = RedirectToAction("Index", "Home");

            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 2 || (int)Session["RoleID"] == 3))
            {
                response = View();
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        //Role 2 and higher can create a product:
        [HttpPost]
        public ActionResult CreateProduct(ProductPO form)
        {
            ActionResult response = null;

            if (ModelState.IsValid)
            {
                if (Session["RoleID"] != null && ((int)Session["RoleID"] == 2 || (int)Session["RoleID"] == 3))
                {
                    try
                    {
                        ProductDO dataObject = ProductMappers.ProductPOtoDO(form);
                        _dataAccess.CreateProduct(dataObject);
                        response = RedirectToAction("Index", "Product");
                    }

                    catch (Exception exception)
                    {
                        ErrorLogger.LogExceptions(exception);
                        response = View(form);
                    }

                    finally { }
                }

                else
                {
                    response = RedirectToAction("Index", "Home");
                }
            }

            else
            {
                response = View(form);
            }

            return response;
        }

        //Update product
        [HttpGet]
        public ActionResult UpdateProduct(int productID)
        {
            ProductDO item = null;
            ProductPO display = null;
            ActionResult response = RedirectToAction("Index", "Home");

            //Available to all roles
            if (Session["RoleID"] != null)
            {
                try
                {
                    item = _dataAccess.ReadIndividualProduct(productID);
                    display = ProductMappers.ProductDOtoPO(item);
                }

                catch (Exception exception)
                {
                    ErrorLogger.LogExceptions(exception);
                    response = View(productID);
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

        //Role 1 and higher can update a product:
        [HttpPost]
        public ActionResult UpdateProduct(ProductPO form)
        {
            ActionResult response = null;

            //Available to  all roles
            if (Session["RoleID"] != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        ProductDO dataObject = ProductMappers.ProductPOtoDO(form);
                        _dataAccess.UpdateProduct(dataObject);
                        response = RedirectToAction("Index", "Product");
                    }

                    catch (Exception exception)
                    {
                        ErrorLogger.LogExceptions(exception);
                        response = View(form);
                    }

                    finally
                    {

                    }
                }

                else
                {
                    response = View(form);
                }
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        //Delete product
        //Role 3 can delete an order
        [HttpGet]
        public ActionResult DeleteProduct(int ProductID)
        {
            ActionResult response = null;

            if (Session["RoleID"] != null && ((int)Session["RoleID"] == 3))
            {
                try
                {
                    _dataAccess.DeleteProduct(ProductID);
                }

                catch (Exception exception)
                {
                    ErrorLogger.LogExceptions(exception);
                    response = View(ProductID);
                }

                finally
                { }

                response = RedirectToAction("Index", "Product");
            }

            else
            {
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }
    }
}