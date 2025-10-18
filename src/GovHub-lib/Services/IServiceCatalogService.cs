using GovHub.Lib.Models;

namespace GovHub.Lib.Services;

/// <summary>
/// Service for managing and searching the government services catalog
/// </summary>
public interface IServiceCatalogService
{
    /// <summary>
    /// Search for services by query string (searches titles, descriptions, and tags)
    /// </summary>
    Task<List<Service>> SearchServicesAsync(string query);

    /// <summary>
    /// Get a specific service by ID
    /// </summary>
    Task<Service?> GetServiceByIdAsync(Guid serviceId);

    /// <summary>
    /// Get all available services
    /// </summary>
    Task<List<Service>> GetAllServicesAsync();
}
