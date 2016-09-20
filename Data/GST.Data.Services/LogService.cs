namespace GST.Data.Services
{
    using Interfaces;
    using System;
    using Models;
    using System.Linq;
    using Common.Repository;

    public class LogService : ILogService
    {
        public readonly DeletableEntityRepository<Log> _logsRepository;

        public LogService(DeletableEntityRepository<Log> logsRepository)
        {
            _logsRepository = logsRepository; 
        }

        public Log AddNewLog(string category, string content)
        {
            Log log = new Log
            {
                CreatedOn = DateTime.Now,
                Category = category,
                Content = content
            };

            _logsRepository.Add(log);

            return log;
        }

        public IQueryable<Log> GetAllLogs()
        {
            return _logsRepository.All().AsQueryable();
        }

        public IQueryable<Log> GetAllLogsForCategory(string categoryName)
        {
            return _logsRepository
                .All()
                .Where(x => x.Category == categoryName)
                .OrderBy(x => x.CreatedOn)
                .AsQueryable();
        }
    }
}
