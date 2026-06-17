using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using IBBS.Infrastructure.Persistence.Adapters.Helpers;
using IBBS.Infrastructure.Persistence.Adapters.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.Persistence.Adapters.Repositories;

/// <summary>
/// The implementation of the master data repository.
/// </summary>
/// <remarks>This class provides methods for executing AI SQL queries and retrieving master data from the database.</remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <seealso cref="IMasterDataRepository"/>
public sealed class MasterDataRepository(
    IUnitOfWork unitOfWork,
    SqlDbContext dbContext) : IMasterDataRepository
{
    /// <inheritdoc />
    public async Task<string> ExecuteAISQLQueryAsync(
        string aiSqlQuery,
        CancellationToken cancellationToken = default
    )
    {
        var result = await this.ExecuteSqlQueryRawAsync<List<object>>(
            sqlQuery: aiSqlQuery,
            cancellationToken
        ).ConfigureAwait(false);
        return JsonConvert.SerializeObject(result);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<LookupMasterEntity>> GetLookupMasterDataAsync(
        CancellationToken cancellationToken = default
    ) =>
        await unitOfWork.Repository<LookupMasterEntity>()
            .GetAllAsync(filter: x => x.IsActive, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IEnumerable<LookupMasterEntity>> GetSamplePromptsForChatbotAsync(
        CancellationToken cancellationToken = default
    ) =>
        await unitOfWork.Repository<LookupMasterEntity>()
            .GetAllAsync(filter: x => x.Type == DatabaseConstants.SamplePromptsConstant && x.IsActive, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<bool> SubmitBugReportDataAsync(
        BugReportEntity newBugReportData,
        CancellationToken cancellationToken = default
    )
    {
        await unitOfWork.Repository<BugReportEntity>()
            .AddAsync(entity: newBugReportData, cancellationToken)
            .ConfigureAwait(false);

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    #region PRIVATE METHODS

    /// <summary>
    /// Executes the SQL query raw asynchronous.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="sqlQuery">The SQL query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response from sql.</returns>
    private async Task<TResponse> ExecuteSqlQueryRawAsync<TResponse>(
        string sqlQuery,
        CancellationToken cancellationToken = default
    )
    {
        using var connection = dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        using var command = connection.CreateCommand();
        command.CommandText = sqlQuery;

        using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(List<>))
        {
            var elementType = typeof(TResponse).GetGenericArguments()[0];
            var list = (System.Collections.IList)Activator.CreateInstance<TResponse>()!;

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                var item = reader.MapReaderToObjectOrDictionary(elementType);
                list.Add(item);
            }

            return (TResponse)list;
        }
        else
        {
            if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                var result = reader.MapReaderToObjectOrDictionary(typeof(TResponse));
                return (TResponse)result;
            }

            return default!;
        }
    }

    #endregion
}