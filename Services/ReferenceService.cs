using Microsoft.IdentityModel.Tokens;
using ValgfagPortfolio.Model;
using ValgfagPortfolio.Persistence.Interfaces;

namespace ValgfagPortfolio.Services;

public class ReferenceService : IReferenceService
{
    private readonly IRepository<Reference> repository;
    private List<Reference> references = new();

    public ReferenceService(IRepository<Reference> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> CreateReferenceAsync(Reference reference)
    {
        var success = false;
        if (reference == null || reference.Title.IsNullOrEmpty())
        {
            success = false;
        }
        else
        {
            await repository.CreateEntityAsync(reference);
            success = true;
        }

        return success;
    }

    public async Task<List<Reference>> GetAllReferencesAsync()
    {
        references = await repository.GetAllEntitiesAsync();
        return references.Count <= 0 ? new List<Reference>() : references;
    }

    public async Task<Reference> GetReferenceByIdAsync(int id)
    {
        if (id <= 0) return null;
        return await repository.GetEntityByIdAsync(id);
    }

    public async Task<bool> UpdateReferenceAsync(Reference reference)
    {
        var success = reference != null || reference.Id != -1;
        if (success) await repository.UpdateEntityAsync(reference);
        return success;
    }

    public async Task<bool> DeleteReferenceAsync(Reference reference)
    {
        var success = reference != null || reference.Id != -1;
        if (success) await repository.DeleteEntityAsync(reference);
        return success;
    }
}