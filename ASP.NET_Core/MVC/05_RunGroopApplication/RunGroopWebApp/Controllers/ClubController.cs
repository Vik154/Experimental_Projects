using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Extensions;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

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
                       
            byte[] imageData = await _photoService.AddPhotoAsync(clubViewModel.Image);
            
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

    [HttpGet]
    public async Task<IActionResult> Edit(int id) {
        var club = await _clubRepository.GetByIdAsync(id);
        if (club == null) { return View("Error"); }

        var clubViewModel = new EditClubViewModel {
            Title = club.Title,
            Description = club.Description,
            AddressId = club.AddressId,
            Address = club.Address,
            Image = ImageConverter.ByteArrayToImage(club.Image),
            ClubCategory = club.ClubCategory
        };
        return View(clubViewModel);
    }
}
