using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;

namespace JournalLabs.API.BLL
{
    public class LabBlockService
    {
        private LabBlockRepository _labBlockRepository;

        public LabBlockService()
        {
            _labBlockRepository = new LabBlockRepository();
        }

        public void CreateLabBlock(LabBlock labBlockModel)
        {
            _labBlockRepository.CreateLabBlock(labBlockModel);
        }

        public void UpdateLabBlock(LabBlock labBlockModel)
        {
            _labBlockRepository.UpdateLabBlock(labBlockModel);
        }

        public LabBlock GetLabBlockById(string labBlockId)
        {
            return _labBlockRepository.GetLabBlockById(labBlockId);
        }
        public bool DeleteLabBlockById(string id)
        {
            return _labBlockRepository.DeleteLabBlockById(id);
        }
    }
}