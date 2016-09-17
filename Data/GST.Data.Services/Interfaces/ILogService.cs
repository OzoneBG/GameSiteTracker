namespace GST.Data.Services.Interfaces
{
    using Models;
    using System.Linq;

    public interface ILogService
    {
        IQueryable<Log> GetAllLogs();

        IQueryable<Log> GetAllLogsForCategory(string CategoryName);

        Log AddNewLog(string category, string content);
    }
}
