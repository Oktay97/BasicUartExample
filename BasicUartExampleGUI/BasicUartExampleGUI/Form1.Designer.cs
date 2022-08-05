
namespace BasicUartExampleGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmbBoxUartPort = new System.Windows.Forms.ComboBox();
            this.Serial_Port = new System.Windows.Forms.GroupBox();
            this.Port_Name = new System.Windows.Forms.Label();
            this.Close_Port = new System.Windows.Forms.Button();
            this.Open_Port = new System.Windows.Forms.Button();
            this.Command_Box = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Device_Check = new System.Windows.Forms.Button();
            this.Fw_Update = new System.Windows.Forms.Button();
            this.Close_Command = new System.Windows.Forms.Button();
            this.Open_Command = new System.Windows.Forms.Button();
            this.LogText = new System.Windows.Forms.RichTextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timerSerialRx = new System.Windows.Forms.Timer(this.components);
            this.Text_Clear = new System.Windows.Forms.Button();
            this.uartText = new System.Windows.Forms.RichTextBox();
            this.uartTextClear = new System.Windows.Forms.Button();
            this.MBUS_BUTTON = new System.Windows.Forms.Button();
            this.Serial_Port.SuspendLayout();
            this.Command_Box.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBoxUartPort
            // 
            this.cmbBoxUartPort.FormattingEnabled = true;
            this.cmbBoxUartPort.Location = new System.Drawing.Point(79, 19);
            this.cmbBoxUartPort.Name = "cmbBoxUartPort";
            this.cmbBoxUartPort.Size = new System.Drawing.Size(134, 21);
            this.cmbBoxUartPort.TabIndex = 0;
            // 
            // Serial_Port
            // 
            this.Serial_Port.Controls.Add(this.Port_Name);
            this.Serial_Port.Controls.Add(this.Close_Port);
            this.Serial_Port.Controls.Add(this.Open_Port);
            this.Serial_Port.Controls.Add(this.cmbBoxUartPort);
            this.Serial_Port.Location = new System.Drawing.Point(12, 12);
            this.Serial_Port.Name = "Serial_Port";
            this.Serial_Port.Size = new System.Drawing.Size(230, 112);
            this.Serial_Port.TabIndex = 1;
            this.Serial_Port.TabStop = false;
            this.Serial_Port.Text = "Serial Port";
            // 
            // Port_Name
            // 
            this.Port_Name.AutoSize = true;
            this.Port_Name.Location = new System.Drawing.Point(13, 22);
            this.Port_Name.Name = "Port_Name";
            this.Port_Name.Size = new System.Drawing.Size(59, 13);
            this.Port_Name.TabIndex = 3;
            this.Port_Name.Text = "Port Status";
            this.Port_Name.DoubleClick += new System.EventHandler(this.Port_Name_DoubleClick);
            // 
            // Close_Port
            // 
            this.Close_Port.Location = new System.Drawing.Point(126, 46);
            this.Close_Port.Name = "Close_Port";
            this.Close_Port.Size = new System.Drawing.Size(87, 47);
            this.Close_Port.TabIndex = 2;
            this.Close_Port.Text = "Close Port";
            this.Close_Port.UseVisualStyleBackColor = true;
            this.Close_Port.Click += new System.EventHandler(this.Close_Port_Click);
            // 
            // Open_Port
            // 
            this.Open_Port.Location = new System.Drawing.Point(16, 46);
            this.Open_Port.Name = "Open_Port";
            this.Open_Port.Size = new System.Drawing.Size(87, 47);
            this.Open_Port.TabIndex = 1;
            this.Open_Port.Text = "Open Port";
            this.Open_Port.UseVisualStyleBackColor = true;
            this.Open_Port.Click += new System.EventHandler(this.Open_Port_Click);
            // 
            // Command_Box
            // 
            this.Command_Box.Controls.Add(this.MBUS_BUTTON);
            this.Command_Box.Controls.Add(this.pictureBox1);
            this.Command_Box.Controls.Add(this.Device_Check);
            this.Command_Box.Controls.Add(this.Fw_Update);
            this.Command_Box.Controls.Add(this.Close_Command);
            this.Command_Box.Controls.Add(this.Open_Command);
            this.Command_Box.Location = new System.Drawing.Point(558, 12);
            this.Command_Box.Name = "Command_Box";
            this.Command_Box.Size = new System.Drawing.Size(230, 393);
            this.Command_Box.TabIndex = 2;
            this.Command_Box.TabStop = false;
            this.Command_Box.Text = "Command Box";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(111, 291);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 102);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Device_Check
            // 
            this.Device_Check.Location = new System.Drawing.Point(22, 118);
            this.Device_Check.Name = "Device_Check";
            this.Device_Check.Size = new System.Drawing.Size(191, 38);
            this.Device_Check.TabIndex = 4;
            this.Device_Check.Text = "Device Check";
            this.Device_Check.UseVisualStyleBackColor = true;
            this.Device_Check.Click += new System.EventHandler(this.Device_Check_Click);
            // 
            // Fw_Update
            // 
            this.Fw_Update.Location = new System.Drawing.Point(22, 162);
            this.Fw_Update.Name = "Fw_Update";
            this.Fw_Update.Size = new System.Drawing.Size(191, 38);
            this.Fw_Update.TabIndex = 3;
            this.Fw_Update.Text = "FW Update";
            this.Fw_Update.UseVisualStyleBackColor = true;
            this.Fw_Update.Click += new System.EventHandler(this.Fw_Update_Click);
            // 
            // Close_Command
            // 
            this.Close_Command.Location = new System.Drawing.Point(22, 74);
            this.Close_Command.Name = "Close_Command";
            this.Close_Command.Size = new System.Drawing.Size(191, 38);
            this.Close_Command.TabIndex = 2;
            this.Close_Command.Text = "Close Command";
            this.Close_Command.UseVisualStyleBackColor = true;
            this.Close_Command.Click += new System.EventHandler(this.Close_Command_Click);
            // 
            // Open_Command
            // 
            this.Open_Command.Location = new System.Drawing.Point(22, 30);
            this.Open_Command.Name = "Open_Command";
            this.Open_Command.Size = new System.Drawing.Size(191, 38);
            this.Open_Command.TabIndex = 1;
            this.Open_Command.Text = "Open Command";
            this.Open_Command.UseVisualStyleBackColor = true;
            this.Open_Command.Click += new System.EventHandler(this.Open_Command_Click);
            // 
            // LogText
            // 
            this.LogText.Location = new System.Drawing.Point(261, 12);
            this.LogText.Name = "LogText";
            this.LogText.Size = new System.Drawing.Size(273, 355);
            this.LogText.TabIndex = 3;
            this.LogText.Text = "";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.PortName = "COM3";
            // 
            // timerSerialRx
            // 
            this.timerSerialRx.Interval = 20;
            this.timerSerialRx.Tick += new System.EventHandler(this.timerSerialRx_Tick);
            // 
            // Text_Clear
            // 
            this.Text_Clear.Location = new System.Drawing.Point(261, 367);
            this.Text_Clear.Name = "Text_Clear";
            this.Text_Clear.Size = new System.Drawing.Size(273, 38);
            this.Text_Clear.TabIndex = 4;
            this.Text_Clear.Text = "Clear ";
            this.Text_Clear.UseVisualStyleBackColor = true;
            this.Text_Clear.Click += new System.EventHandler(this.Text_Clear_Click);
            // 
            // uartText
            // 
            this.uartText.Location = new System.Drawing.Point(12, 130);
            this.uartText.Name = "uartText";
            this.uartText.Size = new System.Drawing.Size(230, 237);
            this.uartText.TabIndex = 5;
            this.uartText.Text = "";
            // 
            // uartTextClear
            // 
            this.uartTextClear.Location = new System.Drawing.Point(12, 367);
            this.uartTextClear.Name = "uartTextClear";
            this.uartTextClear.Size = new System.Drawing.Size(230, 38);
            this.uartTextClear.TabIndex = 6;
            this.uartTextClear.Text = "Clear ";
            this.uartTextClear.UseVisualStyleBackColor = true;
            this.uartTextClear.Click += new System.EventHandler(this.uartTextClear_Click);
            // 
            // MBUS_BUTTON
            // 
            this.MBUS_BUTTON.Location = new System.Drawing.Point(22, 206);
            this.MBUS_BUTTON.Name = "MBUS_BUTTON";
            this.MBUS_BUTTON.Size = new System.Drawing.Size(191, 38);
            this.MBUS_BUTTON.TabIndex = 5;
            this.MBUS_BUTTON.Text = "MBUS";
            this.MBUS_BUTTON.UseVisualStyleBackColor = true;
            this.MBUS_BUTTON.Click += new System.EventHandler(this.MBUS_BUTTON_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uartTextClear);
            this.Controls.Add(this.uartText);
            this.Controls.Add(this.Text_Clear);
            this.Controls.Add(this.LogText);
            this.Controls.Add(this.Command_Box);
            this.Controls.Add(this.Serial_Port);
            this.Name = "Form1";
            this.Text = "Turk Mekatronik";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Serial_Port.ResumeLayout(false);
            this.Serial_Port.PerformLayout();
            this.Command_Box.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBoxUartPort;
        private System.Windows.Forms.GroupBox Serial_Port;
        private System.Windows.Forms.Label Port_Name;
        private System.Windows.Forms.Button Close_Port;
        private System.Windows.Forms.Button Open_Port;
        private System.Windows.Forms.GroupBox Command_Box;
        private System.Windows.Forms.Button Fw_Update;
        private System.Windows.Forms.Button Close_Command;
        private System.Windows.Forms.Button Open_Command;
        private System.Windows.Forms.Button Device_Check;
        private System.Windows.Forms.RichTextBox LogText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timerSerialRx;
        private System.Windows.Forms.Button Text_Clear;
        private System.Windows.Forms.RichTextBox uartText;
        private System.Windows.Forms.Button uartTextClear;
        private System.Windows.Forms.Button MBUS_BUTTON;
    }
}

