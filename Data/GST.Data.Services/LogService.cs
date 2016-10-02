namespace GST.Data.Services
{
    using Interfaces;
    using System;
    using Models;
    using System.Linq;
    using Common.Repository;

    public class LogService : ILogService
    {
        public readonly IDeletableEntityRepository<Log> logs;
    
        public LogService(IDeletableEntityRepository<Log> logs)
        {
            this.logs = logs;
        }

        public Log AddNewLog(string category, string content)
        {
            Log log = new Log
            {
                CreatedOn = DateTime.Now,
                Category = category,
                Content = content
            };

            logs.Add(log);

            logs.SaveChanges();

            return log;
        }

        public IQueryable<Log> GetAllLogs()
        {
            return logs.All().OrderByDescending(x => x.CreatedOn).AsQueryable();
        }

        public IQueryable<Log> GetAllLogsForCategory(string categoryName)
        {
            return logs
                .All()
                .Where(x => x.Category == categoryName)
                .OrderBy(x => x.CreatedOn)
                .AsQueryable();
        }
    }
}
