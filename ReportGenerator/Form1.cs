using MetroFramework.Forms;
using ReportGenerator.Class;
using ReportGenerator.Models;
using smtLocations.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportGenerator
{
    public partial class Form1 : MetroForm
    {
        CReports m_reports;
        private BackgroundWorker _bwPreStocktake;
        private BackgroundWorker _bwOnHand;
        private BackgroundWorker _bwgetGB_information;
        private BackgroundWorker _bwSendMailSMKT;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        COracle m_oracle;
        CMySQL m_mysql;
        bool m_isMysqlActive;
        public Form1()
        {
            InitializeComponent();
            metroProgressSpinner1.Visible = false;
            spinnerPrestock.Visible = false;
            m_reports = new CReports();
            m_oracle = new COracle("192.168.0.25", "SEMPROD");
            m_mysql = new CMySQL();
            m_isMysqlActive = false;
            initThreads();
            
        }
        private void initThreads()
        {
            try
            {
                _bwSendMailSMKT = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                _bwSendMailSMKT.DoWork += sendMailSMKT;
                //_bwAOIWaiting.ProgressChanged += bw_ProgressChanged;
                _bwSendMailSMKT.RunWorkerCompleted += bw_RunWorkerCompleted;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[initThreadsAOIWaiting] Error...");
            }

            try
            {
                _bwgetGB_information = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                _bwgetGB_information.DoWork += getGB_information;
                //_bwAOIWaiting.ProgressChanged += bw_ProgressChanged;
                _bwgetGB_information.RunWorkerCompleted += bw_RunWorkerCompleted;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[initThreadsAOIWaiting] Error...");
            }

            try
            {
                _bwPreStocktake = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                _bwPreStocktake.DoWork += get_Prestocktake;
                //_bwAOIWaiting.ProgressChanged += bw_ProgressChanged;
                _bwPreStocktake.RunWorkerCompleted += bw_RunWorkerCompleted;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[initThreadsAOIWaiting] Error...");
            }
            try
            {
                _bwOnHand = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                _bwOnHand.DoWork += get_Onhand;
                //_bwAOIWaiting.ProgressChanged += bw_ProgressChanged;
                _bwOnHand.RunWorkerCompleted += bw_RunWorkerCompleted;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[initThreadsAOIActive] Error...");
            }
        }
        static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                    Console.WriteLine("You canceled!");
                else if (e.Error != null)
                    Console.WriteLine("Worker exception: " + e.Error.ToString());
                else
                    Console.WriteLine("Complete: " + e.Result);      // from DoWork
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[bw_RunWorkerCompleted] Error...");
            }
        }

        static void bw_ProgressChanged(object sender,
                                        ProgressChangedEventArgs e)
        {
            Console.WriteLine("Reached " + e.ProgressPercentage + "%");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //_bwgetGB_information.RunWorkerAsync();
            //_bwOnHand.RunWorkerAsync();
            m_oracle.preStocktake();
            //_bwPreStocktake.RunWorkerAsync();

        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonCheckBox1.Checked) { 
                t_prestock.Start();
                //t_prestock_esp.Start();
            }
            else { 
                t_prestock.Stop();
                ///t_prestock_esp.Stop();
            }
        }

        private void get_Prestocktake(object sender, DoWorkEventArgs e)
        {

            //m_reports.prestocktake();
            //spinnerPrestock.Visible = false;
            m_oracle.preStocktake();
        }
        private void get_Onhand(object sender, DoWorkEventArgs e)
        {
            m_oracle.getSimosOnHand();
        }

        private void getGB_information(object sender, DoWorkEventArgs e)
        {
            m_mysql.executeSPWhitoutP("siixsem_getGB_information");
            //m_isMysqlActive = false;
            
        }

        private void sendMailSMKT(object sender, DoWorkEventArgs e)
        {
            sendMail();
        }

        private static void sendMail()
        {
            siixsem_smtLocations_dbEntities m_db = new siixsem_smtLocations_dbEntities();
            excel m_excel = new excel();
            String pathReportEven = "";
            String pathReportOdd = "";
            CUtils utils = new CUtils();


            ///////// obtiene el reporte de pares actualizado
            DataTable evens = utils.ToDataTable(m_db.getEvenReels().ToList());

            String fileNameEven = "Par_Reels_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";

            m_excel.write_fileOLE(evens, fileNameEven, Directory.GetCurrentDirectory() + "\\Reports\\Even", ref pathReportEven);

            ///////// obtiene el reporte de impares actualizado
            evens = utils.ToDataTable(m_db.getOddReels().ToList());

            String fileNameOdd = "Impar_Reels_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";

            m_excel.write_fileOLE(evens, fileNameOdd, Directory.GetCurrentDirectory() + "\\Reports\\Odd", ref pathReportOdd);

            List<string> lstArchivos = new List<string>();
            lstArchivos.Add(pathReportEven);
            lstArchivos.Add(pathReportOdd);
            String mails = "sem.warehouse@SIIX-SEM.com.mx;warehouse.components@SIIX-SEM.com.mx;ruben.regis@SIIX-SEM.com.mx;kenny.manzanilla@SIIX-SEM.com.mx;nicolas.delangel@SIIX-SEM.com.mx;" +
                           "christian.gonzalez@SIIX-SEM.com.mx;cristobal.munoz@siix-sem.com.mx;antonio.hernandez@siix-sem.com.mx;daniel.gomez@SIIX-SEM.com.mx;pavel.vera@SIIX-SEM.com.mx;" +
                           "joseluis.castaneda@siix.mx;karla.herrera@siix.mx;smt.supervisor1@siix.mx;smt.supervisor2@siix.mx";        
           //creamos nuestro objeto de la clase que hicimos
           CMail oMail = new CMail("smtlocations@siix.mx", mails,
                                "Even & Odd Reports", "Even & Odd Reports", lstArchivos);

            oMail.Message = "Se adjuntan Reportes de Locaciones pares e impares.";

            //y enviamos
            if (oMail.enviaMail())
            {


            }
            else
            {
                //Console.Write("no se envio el mail: " + oMail.error);

            }
        }

        private void t_prestock_Tick(object sender, EventArgs e)
        {
            //spinnerPrestock.Visible = true;
            //DateTime day = DateTime.Now;

            //if (day.Day > 21)
            _bwPreStocktake.RunWorkerAsync();
        }
        private void t_prestock_esp_Tick(object sender, EventArgs e)
        {
            try
            {
                //DateTime day = DateTime.Now;
                //if (day.Day >= 23 && day.Day <= 26 && (day.Hour == 15 || day.Hour == 7 || day.Hour == 23) && day.Minute == 20)
                //    if (!_bwPreStocktake.IsBusy) _bwPreStocktake.RunWorkerAsync();
                //if (day.Day >= 26 && day.Day <= 26 &&(day.Hour == 9 || day.Hour == 7 || day.Hour == 11 || day.Hour == 13 || day.Hour == 15 || day.Hour == 17 || day.Hour == 19 || day.Hour == 21 || day.Hour == 23) 
                //   && day.Minute == 20)
                //    if (!_bwPreStocktake.IsBusy) _bwPreStocktake.RunWorkerAsync();
                //if (day.Day == 27 && (day.Hour == 2 || day.Hour == 4 || day.Hour == 6) && day.Minute == 20)
                    if (!_bwPreStocktake.IsBusy) _bwPreStocktake.RunWorkerAsync();

            }
            catch(Exception ex)
            {

            }
        }
        private void t_onhand_Tick(object sender, EventArgs e)
        {
            _bwOnHand.RunWorkerAsync();
        }

        private void t_getGB_information_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute == 0 && DateTime.Now.Second == 0 /*&& m_isMysqlActive == false && DateTime.Now.Millisecond == 0*/)
            {
                _bwgetGB_information.RunWorkerAsync();
                //m_isMysqlActive = true;
            }
        }

        private void kryptonCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonCheckBox3.Checked) t_getGB_information.Start();
            else t_getGB_information.Stop();
        }

        private void kryptonCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            sendMail();
            if (kryptonCheckBox4.Checked) t_sendMailSMKT.Start();
            else t_sendMailSMKT.Stop();
        }

        private void t_sendMailSMKT_Tick(object sender, EventArgs e)
        {
            _bwSendMailSMKT.RunWorkerAsync();
        }


    }
}
