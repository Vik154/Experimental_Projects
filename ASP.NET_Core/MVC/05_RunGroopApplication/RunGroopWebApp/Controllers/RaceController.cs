using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using RunGroopWebApp.Services;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers;

public class RaceController : Controller {

    private readonly IRaceRepository _raceRepository;
    private readonly IPhotoService _photoService;

    public RaceController(IRaceRepository raceRepository, IPhotoService photoService) {
        _raceRepository = raceRepository;
        _photoService = photoService;
    }

    public async Task<IActionResult> Index() {
        IEnumerable<Race> races = await _raceRepository.GetAll();
        return View(races);
    }

    public async Task<IActionResult> Detail(int id) {
        Race? race = await _raceRepository.GetByIdAsync(id);
        return race != null ? View(race) : NotFound("Race not found");
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRaceViewModel raceViewModel) {
        if (ModelState.IsValid) {

            byte[] imageData = await _photoService.AddPhotoAsync(raceViewModel.Image);

            var club = new Race {
                Title = raceViewModel.Title,
                Description = raceViewModel.Description,
                Image = imageData,
                RaceCategory = raceViewModel.RaceCategory,
                // AppUserId = clubViewModel.AppUserId,
                Address = new Address {
                    Street = raceViewModel.Address.Street,
                    City = raceViewModel.Address.City,
                    State = raceViewModel.Address.State,
                }
            };
            _raceRepository.Add(club);
            return RedirectToAction("Index");
        }
        else {
            ModelState.AddModelError("", "Photo upload failed");
        }
        return View(raceViewModel);
    }
}
