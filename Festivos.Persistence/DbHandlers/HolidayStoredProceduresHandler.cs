﻿using Festivos.API.Models;
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
        
        public static DateTime GetDateByPascuaSunday(FestivosContext ctx, DateTime date,Festivo holiDay)
        {
            SqlParameter? outputParam;
            SqlParameter? yearParam;
            DateTime pascuaDate;
            outputParam = new SqlParameter("@PASCUADATE", System.Data.SqlDbType.Date)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            yearParam = new SqlParameter("@Year", date.Year);
            ctx.Database.ExecuteSqlRaw("EXEC SundayPascua @Year, @PASCUADATE OUTPUT",
                             yearParam, outputParam);
            pascuaDate = (DateTime)outputParam.Value;
            pascuaDate = pascuaDate.AddDays(holiDay.DiasPascua);
            return pascuaDate;
        }
    }
}