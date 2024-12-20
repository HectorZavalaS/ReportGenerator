namespace ReportGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonCheckBox4 = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonCheckBox3 = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.spinnerPrestock = new MetroFramework.Controls.MetroProgressSpinner();
            this.kryptonCheckBox2 = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonCheckBox1 = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.t_prestock = new System.Windows.Forms.Timer(this.components);
            this.t_onhand = new System.Windows.Forms.Timer(this.components);
            this.t_getGB_information = new System.Windows.Forms.Timer(this.components);
            this.t_sendMailSMKT = new System.Windows.Forms.Timer(this.components);
            this.t_prestock_esp = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(37, 82);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonCheckBox4);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonCheckBox3);
            this.kryptonGroupBox1.Panel.Controls.Add(this.metroProgressSpinner1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.spinnerPrestock);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonCheckBox2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonCheckBox1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(349, 301);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "Available reports";
            // 
            // kryptonCheckBox4
            // 
            this.kryptonCheckBox4.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonCheckBox4.Location = new System.Drawing.Point(47, 121);
            this.kryptonCheckBox4.Name = "kryptonCheckBox4";
            this.kryptonCheckBox4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonCheckBox4.Size = new System.Drawing.Size(198, 20);
            this.kryptonCheckBox4.TabIndex = 5;
            this.kryptonCheckBox4.Values.ExtraText = "Every 2 hours";
            this.kryptonCheckBox4.Values.Text = "Send Mail SMKT ";
            this.kryptonCheckBox4.CheckedChanged += new System.EventHandler(this.kryptonCheckBox4_CheckedChanged);
            // 
            // kryptonCheckBox3
            // 
            this.kryptonCheckBox3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonCheckBox3.Location = new System.Drawing.Point(47, 95);
            this.kryptonCheckBox3.Name = "kryptonCheckBox3";
            this.kryptonCheckBox3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonCheckBox3.Size = new System.Drawing.Size(188, 20);
            this.kryptonCheckBox3.TabIndex = 4;
            this.kryptonCheckBox3.Values.Text = "getGB_information (MySQL)";
            this.kryptonCheckBox3.CheckedChanged += new System.EventHandler(this.kryptonCheckBox3_CheckedChanged);
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(24, 72);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(16, 16);
            this.metroProgressSpinner1.TabIndex = 3;
            this.metroProgressSpinner1.UseSelectable = true;
            // 
            // spinnerPrestock
            // 
            this.spinnerPrestock.Location = new System.Drawing.Point(25, 45);
            this.spinnerPrestock.Maximum = 100;
            this.spinnerPrestock.Name = "spinnerPrestock";
            this.spinnerPrestock.Size = new System.Drawing.Size(16, 16);
            this.spinnerPrestock.TabIndex = 2;
            this.spinnerPrestock.UseCustomBackColor = true;
            this.spinnerPrestock.UseCustomForeColor = true;
            this.spinnerPrestock.UseSelectable = true;
            this.spinnerPrestock.UseStyleColors = true;
            // 
            // kryptonCheckBox2
            // 
            this.kryptonCheckBox2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonCheckBox2.Location = new System.Drawing.Point(47, 69);
            this.kryptonCheckBox2.Name = "kryptonCheckBox2";
            this.kryptonCheckBox2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonCheckBox2.Size = new System.Drawing.Size(248, 20);
            this.kryptonCheckBox2.TabIndex = 1;
            this.kryptonCheckBox2.Values.ExtraText = "L - V 6:00 AM";
            this.kryptonCheckBox2.Values.Text = "Simos Inventory OnHand";
            // 
            // kryptonCheckBox1
            // 
            this.kryptonCheckBox1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonCheckBox1.Location = new System.Drawing.Point(47, 42);
            this.kryptonCheckBox1.Name = "kryptonCheckBox1";
            this.kryptonCheckBox1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonCheckBox1.Size = new System.Drawing.Size(221, 20);
            this.kryptonCheckBox1.TabIndex = 0;
            this.kryptonCheckBox1.Values.ExtraText = "Every 2 hours";
            this.kryptonCheckBox1.Values.Text = "Prestocktake Report";
            this.kryptonCheckBox1.CheckedChanged += new System.EventHandler(this.kryptonCheckBox1_CheckedChanged);
            // 
            // t_prestock
            // 
            this.t_prestock.Interval = 3600000;
            this.t_prestock.Tick += new System.EventHandler(this.t_prestock_Tick);
            // 
            // t_onhand
            // 
            this.t_onhand.Interval = 1000;
            this.t_onhand.Tick += new System.EventHandler(this.t_onhand_Tick);
            // 
            // t_getGB_information
            // 
            this.t_getGB_information.Interval = 1000;
            this.t_getGB_information.Tick += new System.EventHandler(this.t_getGB_information_Tick);
            // 
            // t_sendMailSMKT
            // 
            this.t_sendMailSMKT.Interval = 14400000;
            this.t_sendMailSMKT.Tick += new System.EventHandler(this.t_sendMailSMKT_Tick);
            // 
            // t_prestock_esp
            // 
            this.t_prestock_esp.Tick += new System.EventHandler(this.t_prestock_esp_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 430);
            this.Controls.Add(this.kryptonGroupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Report Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox kryptonCheckBox1;
        private System.Windows.Forms.Timer t_prestock;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox kryptonCheckBox2;
        private System.Windows.Forms.Timer t_onhand;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private MetroFramework.Controls.MetroProgressSpinner spinnerPrestock;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox kryptonCheckBox3;
        private System.Windows.Forms.Timer t_getGB_information;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox kryptonCheckBox4;
        private System.Windows.Forms.Timer t_sendMailSMKT;
        private System.Windows.Forms.Timer t_prestock_esp;
    }
}

