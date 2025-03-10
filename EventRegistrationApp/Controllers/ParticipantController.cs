using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EventRegistration.Application.DTOs;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using EventRegistrationApp.Models;
using EventRegistration.Application.Services;

public class ParticipantController : Controller
{
    private readonly IParticipantService _participantService;

    public ParticipantController(IParticipantService participantService)
    {
        _participantService = participantService;
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var participant = await _participantService.GetParticipantById(id);
        if (participant == null)
        {
            return NotFound();
        }
        return View(participant);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var participant = await _participantService.GetParticipantById(id);
        if (participant == null)
        {
            return NotFound();
        }
        return View(participant);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, ParticipantDto model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            await _participantService.UpdateParticipant(model);
            return RedirectToAction("Details", "Event", new { id = model.EventId });
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var participant = await _participantService.GetParticipantById(id);
        if (participant == null)
        {
            return NotFound();
        }
        return View(participant);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var participant = await _participantService.GetParticipantById(id);
        if (participant == null)
        {
            return NotFound();
        }

        await _participantService.DeleteParticipant(id);
        return RedirectToAction("Details", "Event", new { id = participant.EventId });
    }
}
