using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeckhoffMiddleware.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TwinCAT.Ads;

namespace BeckhoffMiddleware.Services
{
    public class AdsPlcClient : IPlcClient
    {
        private readonly AdsPlcOptions _options;
        private readonly ILogger<AdsPlcClient> _logger;

        public AdsPlcClient(IOptions<AdsPlcOptions> options, ILogger<AdsPlcClient> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public Task<T> ReadAsync<T>(string symbolName)
        {
            using var client = CreateClient();
            client.Connect(_options.AmsNetId, _options.Port);

            uint handle = client.CreateVariableHandle(symbolName);
            try
            {
                object value = client.ReadAny(handle, typeof(T));
                return Task.FromResult((T)value);
            }
            finally
            {
                client.DeleteVariableHandle(handle);
            }
        }

        public Task WriteAsync<T>(string symbolName, T value)
        {
            using var client = CreateClient();
            client.Connect(_options.AmsNetId, _options.Port);

            uint handle = client.CreateVariableHandle(symbolName);
            try
            {
                client.WriteAny(handle, value!);
                return Task.CompletedTask;
            }
            finally
            {
                client.DeleteVariableHandle(handle);
            }
        }

        public Task<IDictionary<string, object?>> ReadSnapshotAsync(IEnumerable<string> symbolNames)
        {
            using var client = CreateClient();
            client.Connect(_options.AmsNetId, _options.Port);

            IDictionary<string, object?> result = new Dictionary<string, object?>();

            foreach (var symbol in symbolNames)
            {
                try
                {
                    uint handle = client.CreateVariableHandle(symbol);
                    try
                    {
                        object value = client.ReadAny(handle, typeof(object));
                        result[symbol] = value;
                    }
                    finally
                    {
                        client.DeleteVariableHandle(handle);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to read symbol {Symbol}", symbol);
                    result[symbol] = null;
                }
            }

            return Task.FromResult(result);
        }

        private AdsClient CreateClient()
        {
            var client = new AdsClient
            {
                Timeout = _options.TimeoutMs
            };
            return client;
        }
    }
}
