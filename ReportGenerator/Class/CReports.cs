using MySql.Data.MySqlClient;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Class
{
    class CReports
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        CUtils m_utils = new CUtils();
        excel m_excel = new excel();

        public void prestocktake()
        {
            String json = "{";
            String html = "";
            DataTable list = new DataTable();
            String pathReport = "";

            try
            {

                var results = m_db.preStocktakeReport();

                list = m_utils.ToDataTable<preStocktakeReport_Result>(results.ToList());

                String fileName = "Pre_Stock_take_data_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";

                //m_excel.write_fileOLE(list, fileName, context.Server.MapPath("~/Reports/"),ref pathReport);
                m_excel.write_fileOLE(list, fileName, @"G:\EVERYONE\Simos_Inventory_OnHand\2020_09_Stocktake", ref pathReport);

                string embed = "Boton";

                //foreach (preStocktakeReport_Result row in results)
                //{
                //    html += "<option value='" + row.idModel + "'><b>" + row.DJ + "</b>, " + row.MODELDESCR + ", " + row.FECHA.Value.ToShortDateString() + "</option>";
                //}
                json += "\"result\":\"true\",";
                json += "\"html\":\"" + embed + "\"";
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.InnerException.ToString().Replace("\"", "'") + "\"";
            }
            json += "}";

        }
    }
}
