using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositores;
using GPTServer.Common.DataAccess.Repositories.Interfaces;

namespace GPTServer.Common.DataAccess.Repositories;

public class LogRepo : AsyncRepo<Log>, ILogRepo
{
	public LogRepo(GPTDbContext dbContext) : base(dbContext)
	{
	}
}
