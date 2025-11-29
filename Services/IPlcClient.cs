using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeckhoffMiddleware.Services;

public interface IPlcClient
{
    Task<T> ReadAsync<T>(string symbolName);
    Task WriteAsync<T>(string symbolName, T value);
    Task<IDictionary<string, object?>> ReadSnapshotAsync(IEnumerable<string> symbolNames);
}
