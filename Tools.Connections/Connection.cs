using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Tools.Connections;

public class Connection
{
    private readonly string _connectionString;
    private readonly DbProviderFactory _factory;

    public Connection(DbProviderFactory factory, string connectionString)
    {
        _connectionString = connectionString;
        _factory = factory;

        using DbConnection dbConnection = CreateConnection();
        dbConnection.Open();
    }

    public object? ExecuteScalar(Command command)
    {
        using DbConnection dbConnection = CreateConnection();
        using DbCommand dbCommand = CreateCommand(dbConnection, command);
        try
        {
            dbConnection.Open();
            object? result = dbCommand.ExecuteScalar();
            return result is DBNull ? null : result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public int ExecuteNonQuery(Command command)
    {
        using DbConnection dbConnection = CreateConnection();
        using DbCommand dbCommand = CreateCommand(dbConnection, command);
        dbConnection.Open();
        return dbCommand.ExecuteNonQuery();
    }

    public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector, bool immediately)
    {
        return immediately ? ExecuteReader(command, selector).ToList() : ExecuteReader(command, selector);
    }

    private IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector)
    {
        using DbConnection dbConnection = CreateConnection();
        using DbCommand dbCommand = CreateCommand(dbConnection, command);
        dbConnection.Open();
        using DbDataReader reader = dbCommand.ExecuteReader();
        {
            while (reader.Read())
            {
                yield return selector(reader);
            }
        }
    }

    public DataTable GetDataTable(Command command)
    {
        using DbConnection dbConnection = CreateConnection();
        using DbCommand dbCommand = CreateCommand(dbConnection, command);
        using DbDataAdapter? dbDataAdapter = _factory.CreateDataAdapter();
        if (dbDataAdapter is null)
            throw new InvalidOperationException();
        dbDataAdapter.SelectCommand = dbCommand;
        DataTable dataTable = new();
        dbDataAdapter.Fill(dataTable);
        return dataTable;
    }

    private static DbCommand CreateCommand(DbConnection dbConnection, Command command)
    {
        DbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = command.Query;
        if (command.IsStoredProcedure)
            dbCommand.CommandType = CommandType.StoredProcedure;

        foreach (KeyValuePair<string, object> kvp in command.Parameters)
        {
            DbParameter dbParameter = dbCommand.CreateParameter();
            dbParameter.ParameterName = kvp.Key;
            dbParameter.Value = kvp.Value;
            dbCommand.Parameters.Add(dbParameter);
        }

        return dbCommand;
    }

    private DbConnection CreateConnection()
    {
        DbConnection? dbConnection = _factory.CreateConnection();

        if (dbConnection is null)
            throw new InvalidOperationException();

        dbConnection.ConnectionString = _connectionString;
        return dbConnection;
    }
}