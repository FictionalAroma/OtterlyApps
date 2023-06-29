using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Otterly.API.DataObjects.Bingo;
using Otterly.API.Handlers.Interfaces;
using Otterly.API.ManualMapper;
using Otterly.Database.UserData;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.Handlers;

public class AccountHandler : IAccountHandler
{
    private readonly OtterlyAppsContext _context;
	private readonly IWebHostEnvironment _env;

	public AccountHandler(OtterlyAppsContext context, IWebHostEnvironment env)
    {
        _context = context;
		_env = env;
	}

    public async Task<OtterlyAppsUserDTO?> GetUserProfile(Guid userID)
    {
        var user = await _context.OtterlyAppsUsers.FindAsync(userID);

		if (_env.IsDevelopment() && user == null)
		{
			user = await CreateUser(userID);
		}
        
        return user != null ? UserMapper.Map(user) : null;
    }

    public async Task<OtterlyAppsUser> CreateUser(Guid userID)
    {
        var user = new OtterlyAppsUser
        {
            UserID = userID,
            Test = Random.Shared.Next(1, 10),
        };

        await _context.OtterlyAppsUsers.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }
}

