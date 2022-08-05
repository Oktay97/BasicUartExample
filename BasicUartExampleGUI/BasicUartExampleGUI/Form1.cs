using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

#region
enum Device_Id
{
    Device_BMS         = 0xF1,
    Device_Ecu         = 0xF2,
    Device_MotorDriver = 0xF3,
    //add other Device
};

enum Main_Command
{

    MC_Setting_Device = 1,
    //add other Main Command
};

enum Sub_Command
{

    /** MC_Setting_Device */
    SC_Open_Command  = 1,
    SC_Close_Command = 2,
    SC_Device_Check  = 3,
    SC_FW_Update     = 4,
    SC_MBUS_Check    = 5,

    //add other Sub Command for Main Command

};

#endregion

namespace BasicUartExampleGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly public String[] strMsgCommand = new String[255];

        public const int UART_BUFFER_SIZE = 64;
        public const int RX_BUFFER_SIZE = UART_BUFFER_SIZE;
        public const int TX_BUFFER_SIZE = UART_BUFFER_SIZE;

        public const int PACKET_HEADER_SIZE = 7;
        public const int MIN_PACKET_LENGHT = 10;
        public const int DATA_BUFFER_SIZE = RX_BUFFER_SIZE - PACKET_HEADER_SIZE;

        public String tempHashString = "";
        public const int LRC_INDIS = 1;

        byte[] BufferIn = new byte[64];

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        // richtexboxWrite rutine with specific Text color and Srcoll bar
        public static void richtextBoxWrite(RichTextBox richBox, String msg, Color color)
        {
            richBox.SelectionColor = color;
            richBox.AppendText(msg);
            richBox.ScrollToCaret();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String[] strPortName = SerialPort.GetPortNames();
            foreach (string port in strPortName)
            {
                cmbBoxUartPort.Items.Add(port);
            }
            try
            {
                cmbBoxUartPort.SelectedIndex = 7; // seçili olarak ayarlama
            }
            catch (Exception ex)
            {

            }
        }
        // Seleciton port name , baudrate and timer interval set and open serialport bus.
        private void Open_Port_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen) serialPort1.Close();
                Port_Name.ForeColor = System.Drawing.Color.Green;
                timerSerialRx.Interval = 30;
                serialPort1.BaudRate = 2400;
                serialPort1.PortName = cmbBoxUartPort.SelectedItem.ToString();
                serialPort1.Open();
                richtextBoxWrite(LogText, "Comm Opened..\r\n", Color.Green);
                timerSerialRx.Enabled = true;
            }
            catch (Exception ex)
            {
                richtextBoxWrite(LogText, ex.ToString() + "\r\n", Color.Red);
            }
        }

        private void Close_Port_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            Port_Name.ForeColor = System.Drawing.Color.Red;
            richtextBoxWrite(LogText, "Comm Closed..\r\n", Color.Red);
            timerSerialRx.Enabled = false;

        }

        byte lrcCompute(byte[] p_data, byte size)
        {
            byte count = 0;
            byte Lrc = 0;
            for (count = 2; count < size; count++)
            {
                Lrc += p_data[count]; //uint8 oldugu icin tasma olacak
            }
            return Lrc;
        }

        bool checkLrc(byte[] p_data, byte size)
        {
            byte checklrc = 0;
            checklrc = lrcCompute(p_data, size);
            if (checklrc == p_data[LRC_INDIS]) //LRC_INDIS pakette lrcnin yeri
            {
                return true;
            }
            return false;
        }

        void UartWrite(byte[] txData, int length) //Burada bir şey yapmıyor
        {
            if (serialPort1.IsOpen == false)
            {
                richtextBoxWrite(LogText, "Serial port is Close... \r\n", Color.Red);
                return;
            }
            try
            {
                txData[LRC_INDIS] = lrcCompute(txData,(byte)length);
                serialPort1.Write(txData, 0, length);
                timerSerialRx.Stop();
                timerSerialRx.Start();
                //if (checkBoxTxView.Checked)
                //{
                    string temp_string = "";
                    for (int i = 0; i < length; i++)
                    {
                        temp_string += txData[i].ToString("X2") + " ";
                    }
                    temp_string += "\r\n";

                    richtextBoxWrite(uartText, "TX Data -> \r\n" + temp_string, Color.DarkBlue);
                //}
            }
            catch (Exception ex)
            {
                richtextBoxWrite(LogText, "UART Exception -- " + ex.ToString() + "\r\n", Color.Red);
            }
        }

        private void timerSerialRx_Tick(object sender, EventArgs e)
        {
            int uartRecvLen = 0;
            if (serialPort1.IsOpen)
            {
                if (serialPort1.BytesToRead > 0)
                {
                    try
                    {
                        uartRecvLen = serialPort1.BytesToRead;
                        serialPort1.Read(BufferIn, 0, serialPort1.BytesToRead); //gelen paket bufferine yazıldı
                        if(checkLrc(BufferIn, (byte)uartRecvLen) == true)
                        {
                            richtextBoxWrite(LogText, "Data Lrc Succes..\r\n", Color.Green);
                        }
                        else
                        {
                            richtextBoxWrite(LogText, "Data Lrc Error..", Color.Red);
                            //    serialPort1.ReadExisting();
                            //    isLrcError = false;
                        }

                        HandleUartRxPackage(BufferIn, uartRecvLen);
                        string bitString = BitConverter.ToString(BufferIn);
                        richtextBoxWrite(uartText, "RX Data -> " + bitString + "\r\n", Color.DarkGreen);
                    }
                    catch (Exception ex)
                    {
                        serialPort1.ReadExisting();
                        richtextBoxWrite(LogText, "UART Exception -- " + ex.ToString() + "\r\n", Color.Red);
                    }


                }
            }
        }


        //Stm32 tarafından gelen komutların işleneceği yer
        void HandleUartRxPackage(byte[] rxData, int length)
        {
            //string strResponsevMsg;
            //strResponsevMsg = Encoding.ASCII.GetString(rxData, 4, 3).ToString() + "\r\n"; riche basmak için



            if (rxData[3] == (byte)Sub_Command.SC_Device_Check)
            {
                switch (rxData[0]) // Struct yapısı olusturulacak //will be check
                {
                    case 0xF1:
                        richtextBoxWrite(LogText, "Device name is BMS" + "\r\n", Color.Blue);
                        break;
                    case 0xF2:
                        richtextBoxWrite(LogText, "Device name is ECU" + "\r\n", Color.Blue);
                        break;
                    case 0xF3:
                        richtextBoxWrite(LogText, "Device name is Motor Driver" + "\r\n", Color.Blue);
                        break;

                }
                

            }
            //if (rxData[2] == BL_NEW_PACKAGE)
            //{
            //    BL_STATE = (byte)BootStates.FlashToMemory_e;
            //    richtextBoxWrite(richTxtBoxComm, "Firmware Update ....!" + "\r\n", Color.Green);

            //}
            //if (rxData[2] == BL_ERASE)
            //{
            //    BL_STATE = (byte)BootStates.FlashToMemory_e;
            //    richtextBoxWrite(richTxtBoxComm, "Chip Erase ....!" + "\r\n", Color.Green);

            //}
        }
        // PC to Stm32 send data
        private void Serial_Message(Sub_Command subFunc)
        {
            byte[] TransmitMessage = new byte[64];

            switch (subFunc)
            {


                case Sub_Command.SC_Open_Command:

                    TransmitMessage[0] = 0xFF;                                   //!< 0 Device ID
                    TransmitMessage[2] = (byte)Main_Command.MC_Setting_Device;   //!< 2 Main command
                    TransmitMessage[3] = (byte)Sub_Command.SC_Open_Command;      //!< 3 Sub  command
                    TransmitMessage[4] = 0x40;                                   //!< 4 Length         
                    TransmitMessage[5] = 0x01;                                   //!< 5 6 7 Reserve
                    TransmitMessage[8] = 0x02;                                   //!< 8+ message data
                    TransmitMessage[9] = 0x03;
                    TransmitMessage[10] = 0x04;
                    //Array.Copy(fwFileBootPackages[hexcounter], 0, tempStartFwUpdtPackage, 8, 34); // fw file Total Kbytes              
                    //tempStartFwUpdtPackage[1] = lrcCompute(tempStartFwUpdtPackage, RX_BUFFER_SIZE); // uartLrcData

                    UartWrite(TransmitMessage, 64); // +4 XOR LRC MSG_LEN MSG_CMD 
                    richtextBoxWrite(LogText, "Send Open Command!\r\n", Color.Green);


                    //richTextBox_Detail.AppendText("---> Write To Firmware Msg SENT = " + frameNumber + "<--- \r\n");
                    break;

                case Sub_Command.SC_Close_Command:

                    TransmitMessage[0] = 0xFF;                                   //!< 0 Device ID
                    TransmitMessage[2] = (byte)Main_Command.MC_Setting_Device;   //!< 2 Main command
                    TransmitMessage[3] = (byte)Sub_Command.SC_Close_Command;      //!< 3 Sub  command
                    TransmitMessage[4] = 0x40;                                   //!< 4 Length         
                    TransmitMessage[5] = 0x01;                                   //!< 5 6 7 Reserve
                    TransmitMessage[8] = 0x02;                                   //!< 8+ message data
                    TransmitMessage[9] = 0x03;
                    TransmitMessage[10] = 0x04;
                    //Array.Copy(fwFileBootPackages[hexcounter], 0, tempStartFwUpdtPackage, 8, 34); // fw file Total Kbytes              
                    //tempStartFwUpdtPackage[1] = lrcCompute(tempStartFwUpdtPackage, RX_BUFFER_SIZE); // uartLrcData

                    UartWrite(TransmitMessage, 64); // +4 XOR LRC MSG_LEN MSG_CMD 
                    richtextBoxWrite(LogText, "Send Close Command!\r\n", Color.Red);


                    //richTextBox_Detail.AppendText("---> Write To Firmware Msg SENT = " + frameNumber + "<--- \r\n");
                    break;

                case Sub_Command.SC_Device_Check:

                    TransmitMessage[0] = 0xFF;                                   //!< 0 Device ID
                    TransmitMessage[2] = (byte)Main_Command.MC_Setting_Device;   //!< 2 Main command
                    TransmitMessage[3] = (byte)Sub_Command.SC_Device_Check;      //!< 3 Sub  command
                    TransmitMessage[4] = 0x40;                                   //!< 4 Length         
                    TransmitMessage[5] = 0x01;                                   //!< 5 6 7 Reserve
                    TransmitMessage[8] = 0x02;                                   //!< 8+ message data
                    TransmitMessage[9] = 0x03;
                    TransmitMessage[10] = 0x04;
                    //Array.Copy(fwFileBootPackages[hexcounter], 0, tempStartFwUpdtPackage, 8, 34); // fw file Total Kbytes              
                    //tempStartFwUpdtPackage[1] = lrcCompute(tempStartFwUpdtPackage, RX_BUFFER_SIZE); // uartLrcData

                    UartWrite(TransmitMessage, 64); // +4 XOR LRC MSG_LEN MSG_CMD 
                    richtextBoxWrite(LogText, "Send Device Check!\r\n", Color.Green);


                    //richTextBox_Detail.AppendText("---> Write To Firmware Msg SENT = " + frameNumber + "<--- \r\n");
                    break;

                case Sub_Command.SC_MBUS_Check:

                    TransmitMessage[0] = 0x10;                                   //!< 0 Device ID
                    TransmitMessage[1] = 0x7B;
                    TransmitMessage[2] = 0x02;
                    TransmitMessage[3] = 0x7D;
                    TransmitMessage[4] = 0x16;
                    //Array.Copy(fwFileBootPackages[hexcounter], 0, tempStartFwUpdtPackage, 8, 34); // fw file Total Kbytes              
                    //tempStartFwUpdtPackage[1] = lrcCompute(tempStartFwUpdtPackage, RX_BUFFER_SIZE); // uartLrcData

                    //UartWrite(TransmitMessage, 5); // +4 XOR LRC MSG_LEN MSG_CMD
                    serialPort1.Write(TransmitMessage, 0, 5); //not xor
                    richtextBoxWrite(LogText, "Send Device Check!\r\n", Color.Green);


                    //richTextBox_Detail.AppendText("---> Write To Firmware Msg SENT = " + frameNumber + "<--- \r\n");
                    break;

            }
        }

        private void Open_Command_Click(object sender, EventArgs e)
        {
            Serial_Message(Sub_Command.SC_Open_Command);
        }

        private void Close_Command_Click(object sender, EventArgs e)
        {
            Serial_Message(Sub_Command.SC_Close_Command);
        }

        private void Device_Check_Click(object sender, EventArgs e)
        {
            Serial_Message(Sub_Command.SC_Device_Check);
        }

        private void Text_Clear_Click(object sender, EventArgs e)
        {
            LogText.Clear();
        }

        private void Port_Name_DoubleClick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                richtextBoxWrite(LogText, "Timer Interval(ms) = " + timerSerialRx.Interval + "\r\n", Color.Black);
                richtextBoxWrite(LogText, "BaundRate = " + serialPort1.BaudRate + "\r\n", Color.Black);
                richtextBoxWrite(LogText, "Port Name = " + serialPort1.PortName + "\r\n", Color.Black);
            }
        }

        private void uartTextClear_Click(object sender, EventArgs e)
        {
            uartText.Clear();
        }

        private void Fw_Update_Click(object sender, EventArgs e)
        {

        }

        private void MBUS_BUTTON_Click(object sender, EventArgs e)
        {
            Serial_Message(Sub_Command.SC_Open_Command);
        }
    }
}


    
