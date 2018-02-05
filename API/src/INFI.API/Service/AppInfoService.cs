using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INFI.API.Models;
using INFI.API.Context;
using System.Data.SqlClient;
using Dapper;

namespace INFI.API.Service
{
    public interface IAppInfoService
    {
        Task<IEnumerable<FruitDto>> FruitQuery();
    }

    public class AppInfoService : IAppInfoService
    {
        public async Task<IEnumerable<FruitDto>> FruitQuery()
        {
            string sqlText = @"Select * from Fruit";
            using (var conn = new SqlConnection(DapperContext.Current.Configuration["Data:DefaultConnection:ConnectionString"]))
            {
                return await conn.QueryAsync<FruitDto>(sqlText);
            }
        }
    }
}
