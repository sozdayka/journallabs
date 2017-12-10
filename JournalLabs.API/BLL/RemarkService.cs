using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.BLL
{
    public class RemarkService
    {
        private RemarkRepository _remarkRepository;

        public RemarkService()
        {
            _remarkRepository = new RemarkRepository();
        }
        public void UpdateRemark(Remark remark)
        {
            _remarkRepository.UpdateRemark(remark);
        }
    }
}