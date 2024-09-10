using Festivos.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivos.Application.Services
{
    public class FestivosService
    {
        private readonly IFestivoRepository _festivoRepository;
        public FestivosService(IFestivoRepository festivoRepository)
        {
            _festivoRepository = festivoRepository;
        }
        public async Task<bool> isHoliday(DateTime date)
        {
            var result = _festivoRepository.IsHoliday(date);
            return await result;
        }
    }
}
