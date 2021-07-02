using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public partial class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Create()
        {
            //get list category
            BigSchoolDBContext Cou = new BigSchoolDBContext();
            Course objCourse = new Course();
            objCourse.ListCategory = Cou.Categories.ToList();
            return View(objCourse);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objcourse)
        {
            BigSchoolDBContext Cou = new BigSchoolDBContext();

            ModelState.Remove("LecturerId"); 
            if (!ModelState.IsValid) 
            { 
                objcourse.ListCategory = Cou.Categories.ToList(); 
                return View("Create", objcourse); 
            }

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objcourse.LecturerId = user.Id;

            Cou.Courses.Add(objcourse);
            Cou.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}