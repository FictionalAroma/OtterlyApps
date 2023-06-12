﻿using System;
using System.Threading.Tasks;
using Otterly.API.ClientLib.DTO;
using Otterly.Database.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface IAccountHandler
{
	Task<OtterlyAppsUserDTO> GetUserProfile(Guid userID);
	Task<OtterlyAppsUser> CreateUser(Guid userID);
}