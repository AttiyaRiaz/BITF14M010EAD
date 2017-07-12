using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EAD_Project.Models;
using System.IO;

namespace EAD_Project.Controllers
{
    public class HomeController : Controller
    {
        Entities5 db = new Entities5();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View(db.AspNetUsers.ToList());
        }

        public ActionResult Update(String id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult updateConform(String id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            String email = Request["t1"];
            user.Email = email;
            db.SaveChanges();
            return RedirectToAction("admin");
        }


        public ActionResult Delete(String id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(user);
            db.SaveChanges();
            return RedirectToAction("admin");
        }


        public ActionResult Classes()
        {
            return View(db.Classes.ToList());
        }

        public ActionResult UpdateClass(int id)
        {
            Class clas = db.Classes.Find(id);
            return View(clas);
        }

        [HttpPost]
        public ActionResult updateConformClass(int id)
        {
            Class clas = db.Classes.Find(id);
            String name = Request["t1"];
            clas.Name = name;
            db.SaveChanges();
            return RedirectToAction("Classes");
        }
        [HttpPost]
        public ActionResult updateUserProfile(String id)
        {
            AspNetUser clas = db.AspNetUsers.Find(id);
            String name = Request["t2"];
            clas.UserName = name;
            db.SaveChanges();
            return RedirectToAction("UserView");
        }

        public ActionResult DeleteClass(int id)
        {
            Class clas = db.Classes.Find(id);
            db.Classes.Remove(clas);
            db.SaveChanges();
            return RedirectToAction("Classes");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        

        public ActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddConform()
        {
            Class clas = new Class();
            String name = Request["t1"];
            clas.Name = name;
            db.Classes.Add(clas);
            db.SaveChanges();
            return RedirectToAction("UserView");
        }

        public ActionResult UserView(LoginViewModel model)
        {
            ViewData["email"] =model.Email;
            AspNetUser user = db.AspNetUsers.Where(x => x.Email == model.Email).SingleOrDefault();
            return View(user);
        }
        public ActionResult AdminHome()
        {
           
            return View();
        }
        public ActionResult LogOff()
        {

            return RedirectToAction("Index");
        }
        public ActionResult SelectClass(AspNetUser user)
        {
            AspNetUser id = db.AspNetUsers.Find(user.Id);
            List<Class> gal = db.Classes.ToList();
            var tuple = new Tuple<List<Class>, AspNetUser >(gal, id);
            return View(tuple);
        }
        [HttpPost]
        public ActionResult ClassSelected(String id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            String name = Request["Class"];
            user.Class = name;
            db.SaveChanges();
            return RedirectToAction("../Account/Login");
        }
        public ActionResult UploadImage(String id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            List<Gallery> gal = db.Galleries.ToList();
            var tuple = new Tuple<AspNetUser, List<Gallery>>(user, gal);
            return View(tuple);
        }
        public ActionResult UploadBook(String id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            List<Book> book = db.Books.ToList();
            var tuple = new Tuple<AspNetUser, List<Book>>(user, book);
            return View(tuple);
        }
     
        [HttpPost]
        public ActionResult UploadFiles(String id)
        {
            String fileName = null;
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fileName = fname;
                        }
                        else
                        {
                            fname = file.FileName;
                            fileName = fname;
                        }

                        // Get the complete folder path and store the file inside it.  
                        
                        

                        string extensioin = fname.Substring(fname.LastIndexOf(".") + 1);
                        string fileType = null;
                        //set the file type based on File Extension
                        switch (extensioin)
                        {
                            case "doc":
                                fileType = "application/vnd.ms-word";
                                break;
                            case "docx":
                                fileType = "application/vnd.ms-word";
                                break;
                            case "xls":
                                fileType = "application/vnd.ms-excel";
                                break;
                            case "xlsx":
                                fileType = "application/vnd.ms-excel";
                                break;
                            case "jpg":
                                fileType = "image/jpg";
                                break;
                            case "png":
                                fileType = "image/png";
                                break;
                            case "gif":
                                fileType = "image/gif";
                                break;
                            case "pdf":
                                fileType = "application/pdf";
                                break;
                        }
                        if(fileType == "image/jpg" || fileType == "image/png" || fileType == "image/gif")
                        {

                            fname = Path.Combine(Server.MapPath("~/Gallery/"), fname);
                            file.SaveAs(fname);
                            Gallery gal = new Gallery();
                            gal.Name = fileName;
                            gal.Path = "/Gallery/"+fileName;
                            gal.UploaderID = id.ToString() ;
                            gal.Extension = extensioin;
                            db.Galleries.Add(gal);
                            db.SaveChanges();
                        }
                        else
                        {
                            fname = Path.Combine(Server.MapPath("~/Books/"), fname);
                            file.SaveAs(fname);
                            Book book = new Book();
                            book.Name = fileName;
                            book.Path = "/Books/" + fileName;
                            book.UploaderID = id.ToString();
                            book.Extension = extensioin;
                            db.Books.Add(book);
                            db.SaveChanges();
                        }

                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }





    }
}