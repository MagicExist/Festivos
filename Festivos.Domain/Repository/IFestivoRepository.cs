using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivos.Domain.Repository
{
    public interface IFestivoRepository
    {
        public Task<bool> IsHoliday(DateTime date);
    }
}
