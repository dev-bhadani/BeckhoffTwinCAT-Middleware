using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeckhoffMiddleware.Services
{
    public class FakePlcClient : IPlcClient
    {
        private readonly Dictionary<string, object?> _store = new(StringComparer.OrdinalIgnoreCase)
        {
            ["MAIN.myRealVar"] = 25.0,
            ["MAIN.myIntVar"] = 10
        };

        public Task<T> ReadAsync<T>(string symbolName)
        {
            if (!_store.TryGetValue(symbolName, out var value))
            {
                throw new InvalidOperationException($"Symbol '{symbolName}' not found.");
            }

            return Task.FromResult((T)value!);
        }

        public Task WriteAsync<T>(string symbolName, T value)
        {
            _store[symbolName] = value;
            return Task.CompletedTask;
        }

        public Task<IDictionary<string, object?>> ReadSnapshotAsync(IEnumerable<string> symbolNames)
        {
            IDictionary<string, object?> result = new Dictionary<string, object?>();

            foreach (var s in symbolNames)
            {
                _store.TryGetValue(s, out var value);
                result[s] = value;
            }

            return Task.FromResult(result);
        }
    }
}
