using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.BLL
{
    public class GroupService
    {
        private GroupRepository _groupRepository;

        public GroupService()
        {
            _groupRepository = new GroupRepository();
        }
        public List<Group> GetGroups()
        {
            return _groupRepository.Groups();
        }
        public void CreateGroup(Group groupModel)
        {
            groupModel.Id = Guid.NewGuid();
            _groupRepository.CreateGroup(groupModel);
        }

        public void UpdateGroup(Group groupModel)
        {
            _groupRepository.UpdateGroup(groupModel);
        }

        public Group GetGroupById(string groupId)
        {
            return _groupRepository.GetGroupById(groupId);
        }
        public bool DeleteGroupById(string id)
        {
            return _groupRepository.DeleteGroupById(id);
        }
    }
}