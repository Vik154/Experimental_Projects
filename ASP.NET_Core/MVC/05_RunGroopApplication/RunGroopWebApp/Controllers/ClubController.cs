using Microsoft.AspNetCore.Mvc;
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

            var img = clubViewModel.Image;

            string str = "123";
            
            
            var result = await _photoService.AddPhotoAsync(clubViewModel.Image);

            var club = new Club {
                Title = clubViewModel.Title,
                Description = clubViewModel.Description,
                Image = result.FileDownloadName.ToString(),
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
