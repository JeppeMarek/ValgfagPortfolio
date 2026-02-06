using ValgfagPortfolio.Model;

namespace ValgfagPortfolio.Services;

public interface IReferenceService
{
    Task<bool> CreateReferenceAsync(Reference reference);
    Task<List<Reference>> GetAllReferencesAsync();
    Task<Reference> GetReferenceByIdAsync(int id);
    Task<bool> UpdateReferenceAsync(Reference reference);
    Task<bool> DeleteReferenceAsync(Reference reference);
}