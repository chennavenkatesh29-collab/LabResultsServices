using Microsoft.EntityFrameworkCore;
using OptumLabResultsAPI.Data;
using OptumLabResultsAPI.Models;

namespace OptumLabResultsAPI.Repositories;

public sealed class SqlLabResultRepository(LabResultDbContext context) : ILabResultRepository
{
    public async Task<LabResult> AddAsync(LabResult result, CancellationToken cancellationToken = default)
    {
        await context.LabResults.AddAsync(result, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return result;
    }

    public async Task<LabResult?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.LabResults.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LabResult>> GetByPatientIdAsync(string patientId, CancellationToken cancellationToken = default)
    {
        return await context.LabResults
            .AsNoTracking()
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.ObservedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LabResult>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.LabResults
            .AsNoTracking()
            .OrderByDescending(x => x.ObservedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(LabResult result, CancellationToken cancellationToken = default)
    {
        context.LabResults.Update(result);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await context.LabResults
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (result is null)
            return false;

        context.LabResults.Remove(result);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}