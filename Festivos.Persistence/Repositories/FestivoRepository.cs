using Festivos.API.Models;
using Festivos.Domain.Entities;
using Festivos.Domain.Enum;
using Festivos.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
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
            SqlParameter? outputParam;
            SqlParameter? yearParam;
            DateTime pascuaDate;
            NextMondayDto? dateResult;

            Console.WriteLine(date);

            foreach (var holiDay in _list) 
            {
                switch ((HoliDayEnum)holiDay.IdTipo)
                {
                    case HoliDayEnum.Fijo:break;
                    case HoliDayEnum.Bridge:
                        dateResult = _ctx.Database
                        .SqlQueryRaw<NextMondayDto>("EXEC ObtenerProximoLunes @p0", new SqlParameter("@p0", date))
                        .AsEnumerable()
                        .FirstOrDefault();
                        if (dateResult != null)
                        {
                            holiDay.Dia = dateResult.NextMondayDate.Day;
                            holiDay.Mes = dateResult.NextMondayDate.Month;
                        }
                        
                        break;
                    case HoliDayEnum.PascuaSunday:
                        outputParam = new SqlParameter("@PASCUADATE", System.Data.SqlDbType.Date)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        yearParam = new SqlParameter("@Year", date.Year);
                        _ctx.Database.ExecuteSqlRaw("EXEC SundayPascua @Year, @PASCUADATE OUTPUT",
                                         yearParam, outputParam);
                        pascuaDate = (DateTime)outputParam.Value;
                        pascuaDate = pascuaDate.AddDays(holiDay.DiasPascua);
                        holiDay.Dia = pascuaDate.Day;
                        holiDay.Mes = pascuaDate.Month;
                        break;
                    case HoliDayEnum.PascuaSundayBridge:
                        outputParam = new SqlParameter("@PASCUADATE", System.Data.SqlDbType.Date)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        yearParam = new SqlParameter("@Year", date.Year);
                        _ctx.Database.ExecuteSqlRaw("EXEC SundayPascua @Year, @PASCUADATE OUTPUT",
                                         yearParam, outputParam);
                        pascuaDate = (DateTime)outputParam.Value;
                        pascuaDate = pascuaDate.AddDays(holiDay.DiasPascua);


                        dateResult = _ctx.Database
                            .SqlQueryRaw<NextMondayDto>("EXEC ObtenerProximoLunes @p0", new SqlParameter("@p0", pascuaDate))
                            .AsEnumerable()
                            .FirstOrDefault();
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
