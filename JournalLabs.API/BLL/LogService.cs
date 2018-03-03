using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.BLL
{
    public class LogService
    {
        private LogRepository _logRepository;

        public LogService()
        {
            _logRepository = new LogRepository();
        }
        public void CreateLog(Log logModel)
        {
            _logRepository.CreateLog(logModel);
        }
        public List<Log> GetLogsByType(string type)
        {
            return _logRepository.GetLogsByType(type);
        }
    }
}