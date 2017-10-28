﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JournalLabs.API.DAL.Repositories;
using JournalLabs.API.Models;

namespace JournalLabs.API.BLL
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public void CreateUser(User userModel)
        {
            userModel.Id = Guid.NewGuid();
            _userRepository.CreateUser(userModel);
        }

        public void UpdateUser(User userModel)
        {
            _userRepository.UpdateUser(userModel);
        }

        public User GetUserById(string userId)
        {
            return _userRepository.GetUserById(userId);
        }
        public bool DeleteUserById(string id)
        {
            return _userRepository.DeleteUserById(id);
        }
    }
}