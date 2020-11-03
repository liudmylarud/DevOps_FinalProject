using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kotiko.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Kotiko.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.CodeDom;
using System.ComponentModel.DataAnnotations;

namespace Kotiko.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private PetsContext _petsContext;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, PetsContext petsContext, IWebHostEnvironment env)
        {
            _logger = logger;
            _petsContext = petsContext;
            _env = env;
        }

        [HttpGet]
        public IActionResult MyPetsDelete (int id)
        {
            foreach (var pet in _petsContext.Pets)
            {
                if (pet.Id == id)
                {
                    _petsContext.Pets.Remove(pet);
                }
            }
            _petsContext.SaveChanges();
            return RedirectToAction("MyPets", new { Phone = HttpContext.Request.Cookies["Login"]});
        }

        [HttpGet]
        public IActionResult MyPetsRemove(int id)
        {
            foreach (var pet in _petsContext.Pets)
            {
                if (pet.Id == id)
                {
                    pet.IsPicked = true;
                }
            }
            _petsContext.SaveChanges();
            return RedirectToAction("MyPets", new { Phone = HttpContext.Request.Cookies["Login"] });
        }

        [HttpGet]
        public IActionResult MyPets(string Phone)
        {
            List<Pet> PetList = new List<Pet>();
            foreach (var pet in _petsContext.Pets)
            {
                if (pet.Phone == Phone)
                {
                    PetList.Add(pet);
                }
            }
            return View(PetList);
        }

        [HttpGet]
        public IActionResult PetInfo(int id)
        {
            foreach (var pet in _petsContext.Pets)
            {
                if (pet.Id == id)
                {
                    return View(pet);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetPets(PetTypes? type, string town, Gender? gender)
        {
            List<Pet> PetList = new List<Pet>();
            if (type == null && gender == null && string.IsNullOrEmpty(town))
                return View(_petsContext.Pets);

            foreach (var pet in _petsContext.Pets)
            {
                if (!string.IsNullOrEmpty(town) && pet.Town != town)
                    continue;
                if (type != null && pet.PetType != type)
                {
                    continue;
                }
                if (gender != null && pet.Gender != gender)
                {
                    continue;
                }
                PetList.Add(pet);
            }
            return View(PetList);
        }

        [HttpPost]
        public IActionResult Create(PetViewModel petView)
        {
            Pet pet = new Pet { Name = petView.Name, Age = petView.Age, Gender = petView.Gender, Description=petView.Description, PetType = petView.PetType, Town = petView.Town, PetSize = petView.PetSize, Phone = HttpContext.Request.Cookies["Login"] };
             
            if (petView.Avatar != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(petView.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)petView.Avatar.Length);
                }
                // установка массива байтов
                pet.Avatar = imageData;
            }
            _petsContext.Pets.Add(pet);
            _petsContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var pickedPets = new List<Pet>();
            foreach (var pet in _petsContext.Pets)
            {
                if (pet.IsPicked == true)
                {
                    pickedPets.Add(pet);
                }
            }
            return View(pickedPets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
