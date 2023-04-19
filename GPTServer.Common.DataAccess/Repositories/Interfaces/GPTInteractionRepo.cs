using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTServer.Common.DataAccess.Repositories.Interfaces;
public class GPTInteractionRepo : AsyncRepo<GPTInteraction>, IGPTInteractionRepo
{
	public GPTInteractionRepo(GPTDbContext dbContext) : base(dbContext)
	{
	}
}
