using EventRegistration.Application.DTOs;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventRegistrationApp.Controllers;

public class ParticipantController : Controller
{
    private readonly IParticipantService _participantService;
    private readonly IPaymentMethodService _paymentMethodService;

    public ParticipantController(IParticipantService participantService, IPaymentMethodService paymentMethodService)
    {
        _participantService = participantService;
        _paymentMethodService = paymentMethodService;
    }

    public async Task<IActionResult> Create(Guid eventId)
    {
        // Fetch the available payment methods
        var paymentMethods = await _paymentMethodService.GetAllAsync();

        // Pass the payment methods to the view
        ViewBag.PaymentMethods = new SelectList(paymentMethods, "Id", "Name");

        var participantDto = new ParticipantDto
        {
            // Set the EventId here if you need it
            EventId = eventId
        };

        return View(participantDto);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var participantDto = await _participantService.GetParticipantById(id);
        if (participantDto == null)
        {
            return NotFound();
        }

        return View(participantDto); // Use the same view for both Details and Edit
    }



    [HttpPost]
    public async Task<IActionResult> Create(ParticipantDto participantDto)
    {
        if (ModelState.IsValid)
        {
            // Add the participant to the database
            await _participantService.AddParticipantAsync(participantDto);

            return RedirectToAction("Details", "Event", new { id = participantDto.EventId });
        }

        // If validation failed, re-populate payment methods
        ViewBag.PaymentMethods = new SelectList(await _paymentMethodService.GetAllAsync(), "Id", "Name");
        return View(participantDto);
    }


}