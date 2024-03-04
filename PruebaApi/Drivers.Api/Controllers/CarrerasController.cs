using System.Text.RegularExpressions;
using Drivers.Api.Models;
using Drivers.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarrerasController: ControllerBase
{
    private readonly ILogger<CarrerasController> _logger;
    private readonly CarreraServices _carreraServices;
    public CarrerasController(ILogger<CarrerasController> logger, CarreraServices carreraServices)
    {
        _logger = logger;
        _carreraServices = carreraServices;        
    }
    [HttpGet]
    public async Task<IActionResult> GetCarreras()
    {
        var carreras = await _carreraServices.GetAsync();
        return Ok(carreras);
    }
    [HttpPost]
    public async Task<IActionResult> InsertCarrera([FromBody] Carrera carreraToInsert)
    {
        if(carreraToInsert == null)
            return BadRequest();
        if(carreraToInsert.Name == string.Empty)
            ModelState.AddModelError("Name","El driver no debe estar vacio");

        await _carreraServices.InsertCarrera(carreraToInsert);

        return Created("Created",true);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeleteCarrera(string idToDelete)
    {
        if(idToDelete == null)
            return BadRequest();
        if(idToDelete == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");

        await _carreraServices.DeleteCarrera(idToDelete);

        return Ok();
    }

    [HttpPut("CarreraToUpdate")]
    public async Task<IActionResult> UpdateDriver(Carrera carreraToUpdate)
    {
        if(carreraToUpdate == null)
            return BadRequest();
        if(carreraToUpdate.Id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");
        if(carreraToUpdate.Name == string.Empty)
            ModelState.AddModelError("Name","No debe dejar el nombre vacio");
        await _carreraServices.UpdateCarrera(carreraToUpdate);

        return Ok();
    }

    [HttpGet("ID")]
    public async Task<IActionResult> GetCarreraById(string idToSearch)
    {
        var drivers = await _carreraServices.GetCarreraById(idToSearch);
        return Ok(drivers);
    }
}