using Oracle.ManagedDataAccess.Client;
using ReportGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Class
{
    class COracle
    {
        String m_server;
        String m_SID;
        private String m_user;
        private String m_pass;
        OracleConnection m_OracleDB;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public string Server { get => m_server; set => m_server = value; }
        public string SID { get => m_SID; set => m_SID = value; }

        public COracle (String serv, String Sid)
        {
            m_server = serv;
            m_SID = Sid;
            m_user = "apps";
            m_pass = "apps";
            m_OracleDB = GetDBConnection(Server, 0, SID, m_user, m_pass);
            m_OracleDB.Open();
        }

        private OracleConnection GetDBConnection(string host, int port, String sid, String user, String password)
        {


            Console.WriteLine("Getting Connection ...");

            // 'Connection string' to connect directly to Oracle.
            string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
                 + Server + ")(PORT = " + "1521" + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
                 + SID + ")));Password=" + m_pass + ";User ID=" + m_user + ";Enlist=false;Pooling=true";

            OracleConnection conn = new OracleConnection();
            try
            {
                conn.ConnectionString = connString;
            }
            catch(Exception ex)
            {
                conn = null;
                logger.Error(ex, "Error al conectarse a base de datos de Oracle");
            }

            return conn;
        }
        public  bool QuerySerial(String serial, ref int resultTest)
        {
            bool result = false;
            string sql = "SELECT * FROM insp_result_summary_info where board_barcode in ('" + serial.ToUpper() + "')"; ;

            try
            {
                // Create command.
                OracleCommand cmd = new OracleCommand();

                // Set connection for command.
                cmd.Connection = m_OracleDB;
                cmd.CommandText = sql;


                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        result = true;

                        while (reader.Read())
                        {
                            int IRCODEIndex = reader.GetOrdinal("INSP_RESULT_CODE");
                            int VLRCODEIndex = reader.GetOrdinal("VC_LAST_RESULT_CODE");

                            long? INSP_RESULT_CODE = null;
                            long? VC_LAST_RESULT_CODE = null;

                            if (!reader.IsDBNull(IRCODEIndex))
                                INSP_RESULT_CODE = Convert.ToInt64(reader.GetValue(IRCODEIndex));
                            if (!reader.IsDBNull(VLRCODEIndex))
                                VC_LAST_RESULT_CODE = Convert.ToInt64(reader.GetValue(VLRCODEIndex));

                            if (INSP_RESULT_CODE == 0 && VC_LAST_RESULT_CODE == null)
                                resultTest = 1;   //// OK
                            if (INSP_RESULT_CODE != 0 && VC_LAST_RESULT_CODE != 0)
                                resultTest = 2;   //// NG
                            if (INSP_RESULT_CODE != 0 && VC_LAST_RESULT_CODE == 0)
                                resultTest = 3;   //// FALSE CALL (OK)
                            if (INSP_RESULT_CODE != 0 && VC_LAST_RESULT_CODE == null)
                                resultTest = 4;   //// NO JUZGADA

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "[QuerySerial] Error en serial: " + serial);
                result = false;
            }
            return result;

        }

        public bool QuerySerials(List<String> serials, ref int resultTest)
        {
            bool result = false;
            String qSerials = "";

            foreach(String serial in serials)
            {
                qSerials += "'" + serial.ToUpper() + "',";
            }

            string sql = "SELECT * FROM insp_result_summary_info where board_barcode in (" + qSerials.Substring(0,qSerials.Length-1) + ")"; ;

            try
            {
                // Create command.
                OracleCommand cmd = new OracleCommand();

                // Set connection for command.
                cmd.Connection = m_OracleDB;
                cmd.CommandText = sql;


                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        result = true;
                        
                        while (reader.Read())
                        {
                            int IRCODEIndex = reader.GetOrdinal("INSP_RESULT_CODE");
                            int VLRCODEIndex = reader.GetOrdinal("VC_LAST_RESULT_CODE");

                            long? INSP_RESULT_CODE = null;
                            long? VC_LAST_RESULT_CODE = null;

                            if (!reader.IsDBNull(IRCODEIndex))
                                INSP_RESULT_CODE = Convert.ToInt64(reader.GetValue(IRCODEIndex));
                            if (!reader.IsDBNull(VLRCODEIndex))
                                VC_LAST_RESULT_CODE = Convert.ToInt64(reader.GetValue(VLRCODEIndex));

                            if (INSP_RESULT_CODE == 0 && VC_LAST_RESULT_CODE == null)
                                resultTest = 1;   //// OK
                            if (INSP_RESULT_CODE != 0 && VC_LAST_RESULT_CODE != 0)
                                resultTest = 2;   //// NG
                            if (INSP_RESULT_CODE != 0 && VC_LAST_RESULT_CODE == 0)
                                resultTest = 3;   //// FALSE CALL (OK)
                            if (INSP_RESULT_CODE != 0 && VC_LAST_RESULT_CODE == null)
                                resultTest = 4;   //// NO JUZGADA
                            //break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[QuerySerial] Error ");
                result = false;
            }
            return result;

        }
        public bool getSimosOnHand()
        {
            String fileName = "InvOH_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            bool result = false;
            String pathReport = "";
            excel m_excel = new excel();

            String query = "SELECT A.ITEM_NAME, A.SUBINVENTORY_CODE, A.LOT_NUMBER, A.TOTAL_QOH, A.BATCH_NO, A.LOCATOR, " +
					"A.ITEM_DESCRIPTION, TO_CHAR(B.CREATED_DT, 'DD-Mon-YYYY') RECEIPT_DATE, A.RECEIPT_NUM, " + 
					"A.EXPIRY_DATE, A.SRC, A.ITEM_TYPE, A.UOM, A.ITEM_CATEGORY, A.PROJECT_CODE, A.SUPPLIER_LOT_NUMBER, " +
					"A.MKR_PRT_CD, TO_CHAR(B.UPLOAD_SUCCESS_DATE, 'DD-Mon-YYYY HH24:MI:SS') UPLOAD_ORACLE, " +
					"	( CASE A.IQC_STATUS " +
					"	WHEN 1 THEN 'Pending For Storage' " +
					"	WHEN 2 THEN 'Rejected' " +
					"	WHEN 3 THEN 'Not Completed' " +
					"	WHEN 4 THEN 'Accept' " +
					"	END ) AS IQC_STATUS_DECODEING_CODE, B.STORED " +
				"FROM SIIXSEM.V_SIMOS_STOCK_ENQUIRY A, SIIXSEM.INCOMING_LOT_DETAILS B " +
				"WHERE  A.ORGANIZATION_ID = 81 AND A.LOT_NUMBER = B.LOT_NUMBER AND A.SRC <> 'IN-TRANSIT'";

            try
            {
                DataTable data = new DataTable();
                // Create command.
                OracleCommand cmd = new OracleCommand();

                // Set connection for command.
                cmd.Connection = m_OracleDB;
                cmd.CommandText = query;


                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        result = true;

                        data.Load(reader);
                        m_excel.write_fileOLE(data, fileName, @"G:\EVERYONE\Simos_Inventory_OnHand\2020_06_Stocktake", ref pathReport);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[getSimosOnHand] Error ");
                result = false;
            }
            return result;

        }
        public bool preStocktake()
        {
            String fileName = "Pre_Stock_take_data_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";
            bool result = false;
            String pathReport = "";
            excel m_excel = new excel();

            //SELECT 
            //       A.ITEM_NAME,
            //       A.ITEM_DESC,
            //       A.LOT_NUMBER,
            //       A.SUBINVENTORY_CODE SUBINVENTORY,
            //       A.LOCATOR,
            //       A.SCANNED_QTY,
            //       A.BATCH_NO,
            //       A.SCANNED_BY,
            //       A.SCANNED_DT,
            //       B.SUBINVENTORY_CODE ACT_SUBINVENTORY,
            //      B.LOCATOR ACT_LOCATOR,
            //      B.TOTAL_QOH ACT_QTY
            //FROM 
            //       SIIXSEM.PRE_STOCK_TAKE_DATA A,
            //       SIIXSEM.V_SIMOS_STOCK_ENQUIRY B
            //WHERE
            //       A.LOT_NUMBER = B.LOT_NUMBER


            //String query = "SELECT A.ITEM_NAME, A.ITEM_DESC, A.LOT_NUMBER, A.SUBINVENTORY_CODE SUBINVENTORY, A.LOCATOR, A.SCANNED_QTY, A.BATCH_NO, A.SCANNED_BY, A.SCANNED_DT, " +
            //                        "B.SUBINVENTORY_CODE ACT_SUBINVENTORY, B.LOCATOR ACT_LOCATOR, B.TOTAL_QOH ACT_QTY " +
            //                "FROM   SIIXSEM.PRE_STOCK_TAKE_DATA A, SIIXSEM.V_SIMOS_STOCK_ENQUIRY B " +
            //                "WHERE A.LOT_NUMBER = B.LOT_NUMBER";
            String query = "SELECT SUBINVENTORY_CODE, LOCATOR, ITEM_NAME, ITEM_DESC, LOT_NUMBER, BATCH_NO, BOX_BARCODE, SCANNED_QTY, SCANNED_BY, TO_CHAR(SCANNED_DT, 'DD-Mon-YYYY HH24:MI:SS') SCANNED_DATE FROM SIIXSEM.PRE_STOCK_TAKE_DATA";
            //String query = "SELECT ITEM_NAME, ITEM_DESC, LOT_NUMBER, SCANNED_QTY, SUBINVENTORY_CODE, LOCATOR, BATCH_NO, SCANNED_BY, TO_CHAR(SCANNED_DT, 'DD-Mon-YYYY HH24:MI:SS') SCANNED_TIME FROM SIIXSEM.PRE_STOCK_TAKE_DATA";

            try
            {
                DataTable data = new DataTable();
                // Create command.
                OracleCommand cmd = new OracleCommand();

                // Set connection for command.
                cmd.Connection = m_OracleDB;
                cmd.CommandText = query;


                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        result = true;

                        data.Load(reader);
                        m_excel.write_fileOLE(data, fileName, @"G:\EVERYONE\Simos_Inventory_OnHand\2024_Stock_take\2024_02_Stocktake\Pre_Stock_take", ref pathReport);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[getSimosOnHand] Error ");
                result = false;
            }
            return result;

        }
        public void Close()
        {
            m_OracleDB.Dispose();
            m_OracleDB.Close();
            OracleConnection.ClearPool(m_OracleDB);
        }
    }
}
