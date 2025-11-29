using System.Collections.Generic;

namespace BeckhoffMiddleware.Models;

public class PlcWriteRequest
{
    public object? Value { get; set; }
}

public class PlcReadResponse
{
    public string Symbol { get; set; } = string.Empty;
    public object? Value { get; set; }
}

public class PlcSnapshotRequest
{
    public List<string> Symbols { get; set; } = new();
}

public class PlcSnapshotResponse
{
    public IDictionary<string, object?> Values { get; set; } =
        new Dictionary<string, object?>();
}
