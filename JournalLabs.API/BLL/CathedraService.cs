using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.BLL
{
    public class CathedraService
    {
        private CathedraRepository _cathedraRepository;

        public CathedraService()
        {
            _cathedraRepository = new CathedraRepository();
        }

        public void CreateCathedra(Cathedra cathedraModel)
        {
            cathedraModel.Id = Guid.NewGuid();
            _cathedraRepository.CreateCathedra(cathedraModel);
        }

        public void UpdateCathedra(Cathedra cathedraModel)
        {
            _cathedraRepository.UpdateCathedra(cathedraModel);
        }

        public Cathedra GetCathedraById(string cathedraId)
        {
            return _cathedraRepository.GetCathedraById(cathedraId);
        }
        public bool DeleteCathedraById(string id)
        {
            return _cathedraRepository.DeleteCathedraById(id);
        }
    }
}