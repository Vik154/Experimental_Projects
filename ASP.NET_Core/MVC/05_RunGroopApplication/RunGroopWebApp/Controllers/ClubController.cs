using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;
using System;

namespace RunGroopWebApp.Controllers;

public class ClubController : Controller {

    private readonly IClubRepository _clubRepository;
    private readonly IPhotoService _photoService;

    public ClubController(IClubRepository clubRepository, IPhotoService photoService) {
        _clubRepository = clubRepository;
        _photoService = photoService;
    }

    public async Task<IActionResult> Index() {
        IEnumerable<Club> clubs = await _clubRepository.GetAll();
        return View(clubs);
    }

    public async Task<IActionResult> Detail(int id) {
        Club? club = await _clubRepository.GetByIdAsync(id);
        return club != null ? View(club) : NotFound("Club not found");
    }

    public IActionResult Create() { 
        return View(); 
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClubViewModel clubViewModel) {
        if (ModelState.IsValid) {
            
            string res = await _photoService.AddPhotoAsync(clubViewModel.Image);
           
            byte[]? imageData = null;

            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(clubViewModel.Image.OpenReadStream())) {
                imageData = binaryReader.ReadBytes((int)clubViewModel.Image.Length);
            }

            var club = new Club {
                Title = clubViewModel.Title,
                Description = clubViewModel.Description,
                Image = imageData,
                ClubCategory = clubViewModel.ClubCategory,
                // AppUserId = clubViewModel.AppUserId,
                Address = new Address {
                    Street = clubViewModel.Address.Street,
                    City = clubViewModel.Address.City,
                    State = clubViewModel.Address.State,
                }
            };
            _clubRepository.Add(club);
            return RedirectToAction("Index");
        }
        else {
            ModelState.AddModelError("", "Photo upload failed");
        }
        return View(clubViewModel);
    }
}
