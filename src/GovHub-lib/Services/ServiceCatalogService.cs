using GovHub.Lib.Models;

namespace GovHub.Lib.Services;

/// <summary>
/// In-memory implementation of the service catalog with seeded Jamaican government services
/// </summary>
public class ServiceCatalogService : IServiceCatalogService
{
    private readonly List<Service> _services;

    public ServiceCatalogService()
    {
        _services = new List<Service>
        {
            new Service
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Title = "Vehicle Fitness & Registration",
                ShortDescription = "Complete your vehicle fitness inspection and registration renewal. Includes motor vehicle examination, insurance verification, and licensing.",
                Tags = new[] { "vehicle", "fitness", "registration", "motor", "car", "inspection", "license", "transport" },
                CanonicalDeepLink = "https://www.gov.jm/services/apply-renew-motor-vehicle-fitness-certificate",
                WizardTemplateId = Guid.Parse("a1111111-1111-1111-1111-111111111111")
            },
            new Service
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Title = "NHT Benefits & Loans",
                ShortDescription = "Access National Housing Trust benefits including housing loans, contributor refunds, and mortgage assistance programs.",
                Tags = new[] { "nht", "housing", "loan", "mortgage", "benefits", "refund", "property", "home" },
                CanonicalDeepLink = "https://www.nht.gov.jm/benefits-and-services/",
                WizardTemplateId = Guid.Parse("b2222222-2222-2222-2222-222222222222")
            },
            new Service
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Title = "TAJ Registration & Tax Compliance",
                ShortDescription = "Register for Tax Administration Jamaica services, file returns, and maintain tax compliance including TRN application and payment processing.",
                Tags = new[] { "taj", "tax", "trn", "registration", "filing", "return", "compliance", "revenue", "income" },
                CanonicalDeepLink = "https://www.jamaicatax.gov.jm/individuals/registration",
                WizardTemplateId = Guid.Parse("c3333333-3333-3333-3333-333333333333")
            }
        };
    }

    public Task<List<Service>> SearchServicesAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Task.FromResult(_services.ToList());
        }

        var lowerQuery = query.ToLowerInvariant();
        var results = _services
            .Where(s =>
                s.Title.ToLowerInvariant().Contains(lowerQuery) ||
                s.ShortDescription.ToLowerInvariant().Contains(lowerQuery) ||
                s.Tags.Any(t => t.ToLowerInvariant().Contains(lowerQuery)))
            .ToList();

        return Task.FromResult(results);
    }

    public Task<Service?> GetServiceByIdAsync(Guid serviceId)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);
        return Task.FromResult(service);
    }

    public Task<List<Service>> GetAllServicesAsync()
    {
        return Task.FromResult(_services.ToList());
    }
}
