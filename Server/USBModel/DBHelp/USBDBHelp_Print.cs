using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolsCommon;

namespace USBModel
{
    public partial class USBDBHelp
    {
        // PerPrintJob

        #region + public async Task PrintJobLog_Insert(Tbl_PerPrintJob printJob)
        public async Task PrintJobLog_Insert(Tbl_PrintJobLog printJob)
        {
            try
            {
                await _db.Insertable(printJob).ExecuteCommandAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region + public async Task<(int total,List<Tbl_PerPrintJob> list)> PrintJobLog_Get_List_ByComputerIdentity(string comIdentity, int pageIndex, int size)
        public async Task<(int total, List<Tbl_PrintJobLog> list)> PrintJobLog_Get_List_ByComputerIdentity(string comIdentity, int pageIndex, int size)
        {
            try
            {
                RefAsync<int> total = new RefAsync<int>();
                var list = await _db.Queryable<Tbl_PrintJobLog>()
                                    .Where(p => p.ComputerIdentity == comIdentity)
                                    .OrderBy(p => p.PrintingTime, OrderByType.Desc)
                                    .ToPageListAsync(pageIndex, size, total);

                if (list == null || list.Count <= 0)
                {
                    throw new Exception("PrintJobLog is empty.");
                }

                return (total.Value, list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
