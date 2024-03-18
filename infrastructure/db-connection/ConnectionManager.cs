using Npgsql;

namespace infrastructure.db_connection;

public class ConnectionManager
{
    public static readonly Uri Uri;
    public static readonly String ProperlyFormattedConnectionString;

    static ConnectionManager()
    {
        const String envVarKey = "db_connection";
        
        var rawConnectionString = Environment.GetEnvironmentVariable(envVarKey);
        if (String.IsNullOrEmpty(rawConnectionString))
        {
            throw new Exception("Environment variable for the database connection string is not set!");
        }

        try
        {
            Uri = new Uri(rawConnectionString);
            ProperlyFormattedConnectionString = String.Format(
                "Host={0};Database={1};Username={2};Password={3};",
                Uri.Host,
                Uri.AbsolutePath.Trim('/'),
                Uri.UserInfo.Split(':')[0],
                Uri.UserInfo.Split(':')[1]);

        }
        catch
        {
            throw new Exception("Connection string found but could not be formatted correctly!");
        }
        
    }
    public NpgsqlDataSource GetConnection()
    {
        return new NpgsqlDataSourceBuilder(ProperlyFormattedConnectionString).Build();
    }


}