// using Carter;
// using ContactsApi.Models;
// using FluentValidation;

// namespace VCardValidators;

// // FluentValidation Validators
// public class VCardValidator : AbstractValidator<VCard>
// {
// 	public VCardValidator()
// 	{
// 		RuleFor(x => x.Language)
// 			.SetValidator(new LanguageValidator())
// 			.When(x => x.Language != null);

// 		RuleFor(x => x.Organization)
// 			.SetValidator(new OrganizationValidator())
// 			.When(x => x.Organization != null);

// 		RuleFor(x => x.Address)
// 			.SetValidator(new AddressValidator())
// 			.When(x => x.Address != null);

// 		RuleFor(x => x.Telephones).NotNull().WithMessage("Telephones list cannot be null");

// 		RuleForEach(x => x.Telephones).SetValidator(new TelephoneValidator());

// 		RuleFor(x => x.Email).SetValidator(new EmailValidator()).When(x => x.Email != null);

// 		RuleFor(x => x.Geography)
// 			.SetValidator(new GeographyValidator())
// 			.When(x => x.Geography != null);

// 		// RuleFor(x => x.PublicKey)
// 		//     .SetValidator(new PublicKeyValidator())
// 		//     .When(x => x.PublicKey != null);

// 		RuleFor(x => x.Timezone)
// 			.NotEmpty()
// 			.When(x => !string.IsNullOrWhiteSpace(x.Timezone))
// 			.WithMessage("Timezone must not be empty if provided");

// 		RuleFor(x => x.Url).SetValidator(new UrlValidator()).When(x => x.Url != null);
// 	}
// }

// public class LanguageValidator : AbstractValidator<Language>
// {
// 	public LanguageValidator()
// 	{
// 		RuleFor(x => x.Value)
// 			.NotEmpty()
// 			.WithMessage("Language value is required")
// 			.Matches(@"^[a-z]{2}(-[A-Z]{2})?$")
// 			.WithMessage("Language must be in format 'en' or 'en-US'");

// 		RuleFor(x => x.Preference)
// 			.InclusiveBetween(1, 100)
// 			.When(x => x.Preference.HasValue)
// 			.WithMessage("Preference must be between 1 and 100");
// 	}
// }

// public class OrganizationValidator : AbstractValidator<Organization>
// {
// 	public OrganizationValidator()
// 	{
// 		RuleFor(x => x.Name)
// 			.NotEmpty()
// 			.WithMessage("Organization name is required")
// 			.MaximumLength(255)
// 			.WithMessage("Organization name cannot exceed 255 characters");

// 		RuleFor(x => x.Type)
// 			.MaximumLength(50)
// 			.When(x => !string.IsNullOrWhiteSpace(x.Type))
// 			.WithMessage("Organization type cannot exceed 50 characters");
// 	}
// }

// public class AddressValidator : AbstractValidator<Address>
// {
// 	public AddressValidator()
// 	{
// 		RuleFor(x => x.Type)
// 			.NotEmpty()
// 			.WithMessage("Address type is required")
// 			.Must(type =>
// 				new[] { "work", "home", "postal", "domestic", "international" }.Contains(
// 					type.ToLower()
// 				)
// 			)
// 			.WithMessage(
// 				"Address type must be one of: work, home, postal, domestic, international"
// 			);

// 		RuleFor(x => x.StreetAddress)
// 			.MaximumLength(255)
// 			.When(x => !string.IsNullOrWhiteSpace(x.StreetAddress))
// 			.WithMessage("Street address cannot exceed 255 characters");

// 		RuleFor(x => x.Locality)
// 			.MaximumLength(100)
// 			.When(x => !string.IsNullOrWhiteSpace(x.Locality))
// 			.WithMessage("Locality cannot exceed 100 characters");

// 		RuleFor(x => x.Region)
// 			.MaximumLength(100)
// 			.When(x => !string.IsNullOrWhiteSpace(x.Region))
// 			.WithMessage("Region cannot exceed 100 characters");

// 		RuleFor(x => x.PostalCode)
// 			.MaximumLength(20)
// 			.When(x => !string.IsNullOrWhiteSpace(x.PostalCode))
// 			.WithMessage("Postal code cannot exceed 20 characters");

// 		RuleFor(x => x.Country)
// 			.MaximumLength(100)
// 			.When(x => !string.IsNullOrWhiteSpace(x.Country))
// 			.WithMessage("Country cannot exceed 100 characters");
// 	}
// }

// public class TelephoneValidator : AbstractValidator<Telephone>
// {
// 	public TelephoneValidator()
// 	{
// 		RuleFor(x => x.Value)
// 			.NotEmpty()
// 			.WithMessage("Telephone value is required")
// 			.Matches(@"^[\+]?[0-9\-\s\(\)\.]{7,20}$")
// 			.WithMessage("Telephone value must be a valid phone number");

// 		RuleFor(x => x.ValueType)
// 			.Must(type => new[] { "uri", "text" }.Contains(type?.ToLower()))
// 			.When(x => !string.IsNullOrWhiteSpace(x.ValueType))
// 			.WithMessage("ValueType must be 'uri' or 'text'");

// 		RuleFor(x => x.Types).NotNull().WithMessage("Type list cannot be null");

// 		RuleFor(x => x.Types)
// 			.Must(type =>
// 				new[]
// 				{
// 						"work",
// 						"home",
// 						"text",
// 						"voice",
// 						"fax",
// 						"cell",
// 						"video",
// 						"pager",
// 						"textphone",
// 				}.Contains(type?.ToLower())
// 			)
// 			.When(x => !string.IsNullOrWhiteSpace(x.Types))
// 			.WithMessage(
// 				"Each type must be one of: work, home, text, voice, fax, cell, video, pager, textphone"
// 			);

// 		RuleFor(x => x.Preference)
// 			.InclusiveBetween(1, 100)
// 			.When(x => x.Preference.HasValue)
// 			.WithMessage("Preference must be between 1 and 100");
// 	}
// }

// public class EmailValidator : AbstractValidator<Email>
// {
// 	public EmailValidator()
// 	{
// 		RuleFor(x => x.Address)
// 			.NotEmpty()
// 			.WithMessage("Email address is required")
// 			.EmailAddress()
// 			.WithMessage("Email address must be valid");

// 		RuleFor(x => x.Type)
// 			.Must(type => new[] { "work", "home", "internet" }.Contains(type?.ToLower()))
// 			.When(x => !string.IsNullOrWhiteSpace(x.Type))
// 			.WithMessage("Email type must be one of: work, home, internet");
// 	}
// }

// public class GeographyValidator : AbstractValidator<Geography>
// {
// 	public GeographyValidator()
// 	{
// 		RuleFor(x => x.Type)
// 			.Must(type => new[] { "work", "home" }.Contains(type?.ToLower()))
// 			.When(x => !string.IsNullOrWhiteSpace(x.Type))
// 			.WithMessage("Geography type must be 'work' or 'home'");

// 		RuleFor(x => x.Latitude)
// 			.InclusiveBetween(-90.0, 90.0)
// 			.When(x => x.Latitude.HasValue)
// 			.WithMessage("Latitude must be between -90 and 90 degrees");

// 		RuleFor(x => x.Longitude)
// 			.InclusiveBetween(-180.0, 180.0)
// 			.When(x => x.Longitude.HasValue)
// 			.WithMessage("Longitude must be between -180 and 180 degrees");
// 	}
// }

// public class PublicKeyValidator : AbstractValidator<PublicKey>
// {
// 	public PublicKeyValidator()
// 	{
// 		RuleFor(x => x.Type)
// 			.Must(type => new[] { "work", "home" }.Contains(type?.ToLower()))
// 			.When(x => !string.IsNullOrWhiteSpace(x.Type))
// 			.WithMessage("PublicKey type must be 'work' or 'home'");

// 		RuleFor(x => x.ValueType)
// 			.Must(type => new[] { "uri", "text" }.Contains(type?.ToLower()))
// 			.When(x => !string.IsNullOrWhiteSpace(x.ValueType))
// 			.WithMessage("ValueType must be 'uri' or 'text'");

// 		RuleFor(x => x.Uri)
// 			.NotEmpty()
// 			.WithMessage("PublicKey URI is required")
// 			.Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
// 			.WithMessage("PublicKey URI must be a valid absolute URI");
// 	}
// }

// public class UrlValidator : AbstractValidator<Url>
// {
// 	public UrlValidator()
// 	{
// 		RuleFor(x => x.Type)
// 			.Must(type => new[] { "work", "home" }.Contains(type?.ToLower()))
// 			.When(x => !string.IsNullOrWhiteSpace(x.Type))
// 			.WithMessage("URL type must be 'work' or 'home'");

// 		RuleFor(x => x.Address)
// 			.NotEmpty()
// 			.WithMessage("URL address is required")
// 			.Must(address => Uri.TryCreate(address, UriKind.Absolute, out _))
// 			.WithMessage("URL address must be a valid absolute URL");
// 	}
// }

// // Carter Module
// public class VCardModule : ICarterModule
// {
// 	public void AddRoutes(IEndpointRouteBuilder app)
// 	{
// 		var group = app.MapGroup("/api/vcards").WithTags("VCards");

// 		group
// 			.MapPost("/", CreateVCard)
// 			.WithName("CreateVCard")
// 			.WithSummary("Create a new VCard")
// 			.WithDescription("Creates a new VCard with validation");

// 		group
// 			.MapPut("/{id:guid}", UpdateVCard)
// 			.WithName("UpdateVCard")
// 			.WithSummary("Update an existing VCard")
// 			.WithDescription("Updates an existing VCard with validation");

// 		group
// 			.MapGet("/{id:guid}", GetVCard)
// 			.WithName("GetVCard")
// 			.WithSummary("Get VCard by ID")
// 			.WithDescription("Retrieves a VCard by its unique identifier");
// 	}

// 	private static async Task<IResult> CreateVCard(VCard vcard, IValidator<VCard> validator)
// 	{
// 		var validationResult = await validator.ValidateAsync(vcard);

// 		if (!validationResult.IsValid)
// 		{
// 			return Results.ValidationProblem(validationResult.ToDictionary());
// 		}

// 		// Here you would typically save to database
// 		var id = Guid.NewGuid();

// 		return Results.Created($"/api/vcards/{id}", new { Id = id, VCard = vcard });
// 	}

// 	private static async Task<IResult> UpdateVCard(
// 		Guid id,
// 		VCard vcard,
// 		IValidator<VCard> validator
// 	)
// 	{
// 		var validationResult = await validator.ValidateAsync(vcard);

// 		if (!validationResult.IsValid)
// 		{
// 			return Results.ValidationProblem(validationResult.ToDictionary());
// 		}

// 		// Here you would typically check if exists and update in database
// 		// For demo purposes, we'll assume it exists

// 		return Results.Ok(new { Id = id, VCard = vcard });
// 	}

// 	private static async Task<IResult> GetVCard(Guid id)
// 	{
// 		// Here you would typically retrieve from database
// 		// For demo purposes, we'll return a sample VCard

// 		var sampleVCard = new VCard
// 		{
// 			Organization = new Organization { Name = "Sample Corp", Type = "work" },
// 			Email = new Email { Address = "john.doe@example.com", Type = "work" },
// 		};

// 		return Results.Ok(new { Id = id, VCard = sampleVCard });
// 	}
// }


