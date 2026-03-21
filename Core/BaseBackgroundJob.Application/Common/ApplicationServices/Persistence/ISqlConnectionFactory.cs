namespace BaseBackgroundJob.Application.Common.ApplicationServices.Persistence;

using System.Data;


public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
