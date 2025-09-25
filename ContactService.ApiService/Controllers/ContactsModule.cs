using Carter;
using ContactsApi.Models;
using ContactsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Modules;

public class ContactsModule : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/contacts").WithTags("Contacts").WithOpenApi();

		// GET /api/contacts - Get all contacts
		group
			.MapGet("/", GetAllContacts)
			.WithName("GetAllContacts")
			.WithSummary("Get all contacts")
			.Produces<IEnumerable<VCardDto>>(200);

		// GET /api/contacts/{id} - Get contact by ID
		group
			.MapGet("/{id:guid}", GetContactById)
			.WithName("GetContactById")
			.WithSummary("Get contact by ID")
			.Produces<VCardDto>(200)
			.Produces(404);

		// POST /api/contacts - Create new contact
		group
			.MapPost("/", CreateContact)
			.WithName("CreateContact")
			.WithSummary("Create new contact")
			.Produces<VCardDto>(201)
			.Produces<ValidationProblemDetails>(400);

		// PUT /api/contacts/{id} - Update contact
		group
			.MapPut("/{id:guid}", UpdateContact)
			.WithName("UpdateContact")
			.WithSummary("Update contact")
			.Produces<VCardDto>(200)
			.Produces(404)
			.Produces<ValidationProblemDetails>(400);

		// DELETE /api/contacts/{id} - Delete contact
		group
			.MapDelete("/{id:guid}", DeleteContact)
			.WithName("DeleteContact")
			.WithSummary("Delete contact")
			.Produces(204)
			.Produces(404);

		// GET /api/contacts/search - Search contacts
		group
			.MapGet("/search", SearchContacts)
			.WithName("SearchContacts")
			.WithSummary("Search contacts")
			.Produces<IEnumerable<VCardDto>>(200);
	}

	private static async Task<IResult> GetAllContacts(IContactService contactService)
	{
		try
		{
			var contacts = await contactService.GetAllContactsAsync();
			return Results.Ok(contacts);
		}
		catch (Exception ex)
		{
			return Results.Problem(
				detail: ex.Message,
				statusCode: 500,
				title: "An error occurred while retrieving contacts"
			);
		}
	}

	private static async Task<IResult> GetContactById(Guid id, IContactService contactService)
	{
		try
		{
			var contact = await contactService.GetContactByIdAsync(id);

			if (contact == null)
			{
				return Results.NotFound(new { Message = $"Contact with ID {id} not found." });
			}

			return Results.Ok(contact);
		}
		catch (Exception ex)
		{
			return Results.Problem(
				detail: ex.Message,
				statusCode: 500,
				title: "An error occurred while retrieving the contact"
			);
		}
	}

	private static async Task<IResult> CreateContact(
		CreateVCardRequest request,
		IContactService contactService
	)
	{
		try
		{
			// Basic validation
			if (
				request.Email != null
				&& !string.IsNullOrEmpty(request.Email.Address)
				&& !IsValidEmail(request.Email.Address)
			)
			{
				return Results.ValidationProblem(
					new Dictionary<string, string[]>
					{
						["Email.Address"] = new[] { "Invalid email format" },
					}
				);
			}

			var contact = await contactService.CreateContactAsync(request);

			return Results.Created($"/api/contacts/{contact.Id}", contact);
		}
		catch (Exception ex)
		{
			return Results.Problem(
				detail: ex.Message,
				statusCode: 500,
				title: "An error occurred while creating the contact"
			);
		}
	}

	private static async Task<IResult> UpdateContact(
		Guid id,
		UpdateVCardRequest request,
		IContactService contactService
	)
	{
		try
		{
			// Basic validation
			if (
				request.Email != null
				&& !string.IsNullOrEmpty(request.Email.Address)
				&& !IsValidEmail(request.Email.Address)
			)
			{
				return Results.ValidationProblem(
					new Dictionary<string, string[]>
					{
						["Email.Address"] = new[] { "Invalid email format" },
					}
				);
			}

			var contact = await contactService.UpdateContactAsync(id, request);

			if (contact == null)
			{
				return Results.NotFound(new { Message = $"Contact with ID {id} not found." });
			}

			return Results.Ok(contact);
		}
		catch (Exception ex)
		{
			return Results.Problem(
				detail: ex.Message,
				statusCode: 500,
				title: "An error occurred while updating the contact"
			);
		}
	}

	private static async Task<IResult> DeleteContact(Guid id, IContactService contactService)
	{
		try
		{
			var deleted = await contactService.DeleteContactAsync(id);

			if (!deleted)
			{
				return Results.NotFound(new { Message = $"Contact with ID {id} not found." });
			}

			return Results.NoContent();
		}
		catch (Exception ex)
		{
			return Results.Problem(
				detail: ex.Message,
				statusCode: 500,
				title: "An error occurred while deleting the contact"
			);
		}
	}

	private static async Task<IResult> SearchContacts(string? q, IContactService contactService)
	{
		try
		{
			var searchTerm = q ?? string.Empty;
			var contacts = await contactService.SearchContactsAsync(searchTerm);

			return Results.Ok(contacts);
		}
		catch (Exception ex)
		{
			return Results.Problem(
				detail: ex.Message,
				statusCode: 500,
				title: "An error occurred while searching contacts"
			);
		}
	}

	private static bool IsValidEmail(string email)
	{
		try
		{
			var addr = new System.Net.Mail.MailAddress(email);
			return addr.Address == email;
		}
		catch
		{
			return false;
		}
	}
}
