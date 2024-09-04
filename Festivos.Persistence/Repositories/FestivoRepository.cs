using Festivos.API.Models;
using Festivos.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivos.Persistence.Repositories
{
    internal class FestivoRepository : IFestivoRepository
    {
        private readonly FestivosContext _ctx;
        public FestivoRepository(FestivosContext ctx) 
        {
            _ctx = ctx;
        }
        public Task<string> IsHoliday(int year, int month, int day)
        {
            return null;
        }
    }
}
