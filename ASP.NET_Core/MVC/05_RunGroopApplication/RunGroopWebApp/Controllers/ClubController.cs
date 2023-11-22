using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Extensions;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers;

public class ClubController : Controller {

    private readonly IClubRepository _clubRepository;
    private readonly IPhotoService _photoService;
    private readonly IHttpContextAccessor _contextAccessor;

    public ClubController(IClubRepository clubRepository, 
                          IPhotoService photoService, 
                          IHttpContextAccessor contextAccessor)
    {
        _clubRepository = clubRepository;
        _photoService = photoService;
        _contextAccessor = contextAccessor;
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
        var curUserId = _contextAccessor.HttpContext?.User.GetUserId();
        var createClubVM = new CreateClubViewModel { AppUserId = curUserId };
        return View(createClubVM); 
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
                AppUserId = clubViewModel.AppUserId,
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

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM) {
        if (!ModelState.IsValid) {
            ModelState.AddModelError("", "Failed to edit club");
            return View("Edit", clubVM);
        }

        var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

        if (userClub == null) {
            return View("Error");
        }

        var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

        var club = new Club {
            Id = id,
            Title = clubVM.Title,
            Description = clubVM.Description,
            Image = photoResult,
            AddressId = clubVM.AddressId,
            Address = clubVM.Address,
        };

        _clubRepository.Update(club);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id) {
        var clubDetails = await _clubRepository.GetByIdAsync(id);

        if (clubDetails == null) {
            return View("Error");
        }

        _clubRepository.Delete(clubDetails);
        return RedirectToAction("Index", "Club");
    }
}
