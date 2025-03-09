//using EventRegistration.Domain.Models;
//using EventRegistration.Infra.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//public class ParticipantController : Controller
//{
//    private readonly IParticipantService _participantService;

//    public ParticipantController(IParticipantService participantService)
//    {
//        _participantService = participantService;
//    }

//    public IActionResult Create(int eventId)
//    {
//        ViewData["EventId"] = eventId;
//        return View(new Participant { EventId = eventId });
//    }

//    [HttpPost]
//    public IActionResult Create(Participant participant)
//    {
//        if (ModelState.IsValid)
//        {
//            _participantService.AddAsync(participant);
//            return RedirectToAction("ManageParticipants", "Event", new { id = participant.EventId });
//        }
//        ViewData["EventId"] = participant.EventId;
//        return View(participant);
//    }

//    public IActionResult Edit(int id)
//    {
//        var participant = _participantService.GetByIdAsync(id);
//        if (participant == null)
//        {
//            return NotFound();
//        }
//        return View(participant.Result);
//    }

//    [HttpPost]
//    public IActionResult Edit(Participant participant)
//    {
//        if (ModelState.IsValid)
//        {
//            _participantService.UpdateAsync(participant);
//            return RedirectToAction("ManageParticipants", "Event", new { id = participant.EventId });
//        }
//        return View(participant);
//    }

//    public IActionResult Delete(int id)
//    {
//        var participant = _participantService.GetByIdAsync(id);
//        if (participant != null)
//        {
//            _participantService.DeleteAsync(participant.Result);
//            return RedirectToAction("ManageParticipants", "Event", new { id = participant.Result.EventId });
//        }
//        return NotFound();
//    }
//}