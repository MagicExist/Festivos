using Festivos.API.Models;
using Festivos.Domain.Entities;
using Festivos.Domain.Enum;
using Festivos.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Festivos.Persistence.DbHandlers;
namespace Festivos.Persistence.Repositories
{
    public class FestivoRepository : IFestivoRepository
    {
        private readonly FestivosContext _ctx;
        private Festivo[] _list;
        public FestivoRepository(FestivosContext ctx) 
        {
            _ctx = ctx;
        }
        public async Task<bool> IsHoliday(DateTime date)
        {
            _list = _ctx.Festivos.ToArray();
            DateTime pascuaDate;
            NextMondayDto? dateResult;

            Console.WriteLine(date);

            foreach (var holiDay in _list) 
            {
                switch ((HoliDayEnum)holiDay.IdTipo)
                {
                    case HoliDayEnum.Fijo:break;
                    case HoliDayEnum.Bridge:
                        dateResult = HolidayStoredProceduresHandler.NextMondayProcedure(_ctx,date);
                        if (dateResult != null)
                        {
                            holiDay.Dia = dateResult.NextMondayDate.Day;
                            holiDay.Mes = dateResult.NextMondayDate.Month;
                        }
                        break;
                    case HoliDayEnum.PascuaSunday:
                        pascuaDate = HolidayStoredProceduresHandler.GetDateByPascuaSunday(_ctx, date, holiDay);
                        holiDay.Dia = pascuaDate.Day;
                        holiDay.Mes = pascuaDate.Month;
                        break;
                    case HoliDayEnum.PascuaSundayBridge:

                        pascuaDate = HolidayStoredProceduresHandler.GetDateByPascuaSunday(_ctx,date,holiDay);

                        dateResult = HolidayStoredProceduresHandler.NextMondayProcedure(_ctx, pascuaDate);
                        if (dateResult != null)
                        {
                            holiDay.Dia = dateResult.NextMondayDate.Day;
                            holiDay.Mes = dateResult.NextMondayDate.Month;
                        }
                        break;
                }
                if(date.Day == holiDay.Dia && date.Month == holiDay.Mes)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
