using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositores;
using GPTServer.Common.DataAccess.Repositories.Interfaces;

namespace GPTServer.Common.DataAccess.Repositories;
public class ClientIPRepo : AsyncRepo<ClientIP>, IClientIPRepo
{
	public ClientIPRepo(GPTDbContext dbContext) : base(dbContext)
	{
	}
}
