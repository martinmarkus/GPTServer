using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositores;
using GPTServer.Common.DataAccess.Repositories.Interfaces;

namespace GPTServer.Common.DataAccess.Repositories;
public class AdminKeyRepo : AsyncRepo<AdminKey>, IAdminKeyRepo
{
	public AdminKeyRepo(GPTDbContext dbContext) : base(dbContext)
	{
	}
}
