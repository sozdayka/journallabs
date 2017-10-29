using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;

namespace JournalLabs.API.BLL
{
    public class KindOfWorkService
    {
        private KindOfWorkRepository _kindOfWorkRepository;

        public KindOfWorkService()
        {
            _kindOfWorkRepository = new KindOfWorkRepository();
        }

        public void CreateKindOfWork(KindOfWork kindOfWorkModel)
        {
            kindOfWorkModel.Id = Guid.NewGuid();
            _kindOfWorkRepository.CreateKindOfWork(kindOfWorkModel);
        }

        public void UpdateKindOfWork(KindOfWork kindOfWorkModel)
        {
            _kindOfWorkRepository.UpdateKindOfWork(kindOfWorkModel);
        }

        public KindOfWork GetKindOfWorkById(string kindOfWorkId)
        {
            return _kindOfWorkRepository.GetKindOfWorkById(kindOfWorkId);
        }
        public bool DeleteKindOfWorkById(string id)
        {
            return _kindOfWorkRepository.DeleteKindOfWorkById(id);
        }
    }
}