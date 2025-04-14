using EventRegistration.Application.DTOs;
using EventRegistration.Application.Interfaces;
using EventRegistration.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventRegistrationApp.Controllers;

public class ParticipantController : Controller
{
    private readonly IParticipantService _participantService;
    private readonly IEventParticipantService _eventParticipantService;
    private readonly IPaymentMethodService _paymentMethodService;

    public ParticipantController(IParticipantService participantService, IPaymentMethodService paymentMethodService, IEventParticipantService eventParticipantService)
    {
        _participantService = participantService;
        _paymentMethodService = paymentMethodService;
        _eventParticipantService = eventParticipantService;
    }

    public async Task<IActionResult> Create(Guid eventId)
    {
        var paymentMethods = await _paymentMethodService.GetAllAsync();

        // Find "Pangaülekanne" payment method
        var defaultPaymentMethod = paymentMethods.FirstOrDefault(p => p.Name == "Pangaülekanne");

        if (defaultPaymentMethod == null)
        {
            // Handle the case where "Pangaülekanne" does not exist in the payment methods
            ModelState.AddModelError("", "The default payment method 'Pangaülekanne' does not exist.");
            return View(new ParticipantCreateViewModel());
        }

        var viewModel = new ParticipantCreateViewModel
        {
            PaymentMethods = paymentMethods.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList(),
            Participant = new ParticipantDto
            {
                EventId = eventId,
                PaymentMethodId = defaultPaymentMethod.Id,
                PaymentMethodName = defaultPaymentMethod.Name
            }
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ParticipantCreateViewModel model)
    {
        if (model.ParticipantType == "Person")
        {
            if (model.Person == null ||
                string.IsNullOrWhiteSpace(model.Person.FirstName) ||
                string.IsNullOrWhiteSpace(model.Person.LastName) ||
                string.IsNullOrWhiteSpace(model.Person.PersonalIdCode))
            {
                ModelState.AddModelError("", "All required fields for a person must be filled.");
            }
        }
        else if (model.ParticipantType == "Company")
        {
            if (model.Company == null ||
                string.IsNullOrWhiteSpace(model.Company.LegalName) ||
                string.IsNullOrWhiteSpace(model.Company.RegistrationCode))
            {
                ModelState.AddModelError("", "All required fields for a company must be filled.");
            }
        }
        
        //if (!ModelState.IsValid)
        //{
        //    model.PaymentMethods = (await _paymentMethodService.GetAllAsync())
        //        .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
        //        .ToList();

        //    return View(model);
        //}

        if (model.ParticipantType == "Person" && model.Person != null)
        {
            model.Person.EventId = model.Participant.EventId;
            model.Person.PaymentMethodId = model.Participant.PaymentMethodId;
            model.Person.PaymentMethodName = model.Participant.PaymentMethodName;
            model.Person.AdditionalInfo = model.Participant.AdditionalInfo;
            await _participantService.AddParticipantAsync(model.Person);
            return RedirectToAction("Details", "Event", new { id = model.Person.EventId });
        }

        if (model.ParticipantType == "Company" && model.Company != null)
        {
            model.Company.EventId = model.Participant.EventId;
            model.Company.PaymentMethodId = model.Participant.PaymentMethodId;
            model.Company.PaymentMethodName = model.Participant.PaymentMethodName;
            model.Company.AdditionalInfo = model.Participant.AdditionalInfo;
            await _participantService.AddParticipantAsync(model.Company);
            return RedirectToAction("Details", "Event", new { id = model.Company.EventId });
        }

        return View(model);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var participantDto = await _participantService.GetParticipantById(id);
        if (participantDto == null)
        {
            return NotFound();
        }

        return View(participantDto);
    }

}