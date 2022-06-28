using System;
using System.Collections.Generic;

namespace Tools.Connections;

public class Command
{
    internal string Query { get; }
    internal bool IsStoredProcedure { get; }
    internal Dictionary<string, object> Parameters { get; }

    public Command(string query, bool isStoredProcedure = false)
    {
        Query = query;
        IsStoredProcedure = isStoredProcedure;
        Parameters = new Dictionary<string, object>();
    }

    public void AddParameter(string parameterName, object? value)
    {
        Parameters.Add(parameterName, value ?? DBNull.Value);
    }
}