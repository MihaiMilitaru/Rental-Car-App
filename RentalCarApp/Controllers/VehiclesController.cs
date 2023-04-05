using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Drawing;

using RentalCarApp.Data;
using RentalCarApp.Models;
using System.Data;
using System.IO;
using System.Net;

namespace RentalCarApp.Controllers
{
    public class VehiclesController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        public VehiclesController(
        ApplicationDbContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IWebHostEnvironment env
        )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _env = env;
        }

        public IActionResult Index()
        {
            var vehicles = db.Vehicles.Include("Make");
            ViewBag.Vehicles = vehicles;
            return View();
        }


        [Authorize(Roles = "User,Admin")]
        public IActionResult Show(int id)
        {
            ViewBag.StartDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            DateTime startdate=DateTime.Now;
            DateTime enddate=DateTime.Now;

            DateTime.TryParse(HttpContext.Request.Query["startdate"], out startdate);

            

            DateTime.TryParse(HttpContext.Request.Query["enddate"], out enddate);

            Vehicle vehicle = db.Vehicles.Include("Make")
                                         .Where(vh => vh.Id == id)
                                         .First();

            //if(startdate < DateTime.Now || enddate < DateTime.Now || startdate >= enddate)
            //{
            //    return View(vehicle);
            //} 

            ViewBag.Ammount = (enddate - startdate).Days * vehicle.PricePerDay;

            if(startdate.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                startdate = DateTime.Now;
            }

            if (enddate.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                enddate = DateTime.Now.AddDays(1);
            }

            ViewBag.StartDate = startdate;
            ViewBag.EndDate = enddate;

            

            

            ViewBag.Authenticated = true;

            


            SetAccessRights();

            vehicle.Ratings = GetRatings();

            ViewBag.UserCurent = _userManager.GetUserId(User);



            return View(vehicle);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult New()
        {
            Vehicle vehicle = new Vehicle();

            vehicle.Mk = GetAllMakes();

            return View(vehicle);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult New(Vehicle vehicle)
        {
            vehicle.Rating = 0;


            if (ModelState.IsValid)
            {

                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                vehicle.Mk = GetAllMakes();
                return View(vehicle);
            }
        }

        //public IActionResult Calculate(DateTime start, DateTime end, double price, int idcar)
        //{
        //    var ammount = (start.Day - end.Day) * price;
        //    ViewBag.Ammount = ammount;

        //    return RedirectToAction("Show", "Vehicles", new {@id=idcar});

        //}


        private void SetAccessRights()
        {
            ViewBag.AfisareButoane = false;

            if (User.IsInRole("Admin"))
            {
                ViewBag.AfisareButoane = true;
            }

            ViewBag.EsteAdmin = User.IsInRole("Admin");

            ViewBag.UserCurent = _userManager.GetUserId(User);
        }


        [NonAction]
        public IEnumerable<SelectListItem> GetAllMakes()
        {
            var selectList = new List<SelectListItem>();

            var makes = from mk in db.Makes
                        select mk;

            foreach (var make in makes)
            {

                selectList.Add(new SelectListItem
                {
                    Value = make.Id.ToString(),
                    Text = make.Name.ToString()
                });
            }

            return selectList;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetRatings()
        {
            var selectList = new List<SelectListItem>();

            for (int i = 0; i <= 5; ++i)
            {

                selectList.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }

            return selectList;

        }



        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(Vehicle vehicle, IFormFile vehicleImage)
        {
            if (ModelState.IsValid)
            {

                if (vehicleImage.Length > 0)
                {
                    var storagePath = Path.Combine(
                        _env.WebRootPath, // calea folderului wwwroot
                        "images", //calea folderului images
                        vehicleImage.FileName
                        );


                    var databaseFileName = "/images" + vehicleImage.FileName;
                    using (var fileStream = new FileStream(storagePath, FileMode.Create))
                    {
                        await vehicleImage.CopyToAsync(fileStream);
                    }

                    vehicle.Picture = databaseFileName;
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View();
        }



    }
}
