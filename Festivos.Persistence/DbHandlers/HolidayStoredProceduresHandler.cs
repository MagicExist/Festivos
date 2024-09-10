using Festivos.API.Models;
using Festivos.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Festivos.Persistence.DbHandlers
{
    public static class HolidayStoredProceduresHandler
    {
        public static NextMondayDto? NextMondayProcedure(FestivosContext ctx,DateTime date)
        {
             return
             ctx.Database
            .SqlQueryRaw<NextMondayDto>("EXEC ObtenerProximoLunes @p0", new SqlParameter("@p0", date))
            .AsEnumerable()
            .FirstOrDefault();
        }
    }
}
