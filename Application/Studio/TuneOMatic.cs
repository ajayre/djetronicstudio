using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Solid.Arduino.Firmata;
using Solid.Arduino;

namespace DJetronicStudio
{
    public class TuneOMatic
    {
        public delegate void OnConnectedHandler(object sender, string PortName, int Baudrate, byte FirmwareMajorVersion, byte FirmwareMinorVersion);
        public event OnConnectedHandler OnConnected = null;

        public delegate void OnDisconnectedHandler(object sender);
        public event OnDisconnectedHandler OnDisconnected = null;

        public delegate void OnReceivedProductHandler(object sender, byte ProductUID, byte FirmwareMajorVersion, byte FirmwareMinorVersion);
        public event OnReceivedProductHandler OnReceivedProduct = null;

        public delegate void OnReceivedPulseWidthHandler(object sender, UInt16 PulseWidth);
        public event OnReceivedPulseWidthHandler OnReceivedPulseWidth = null;

        public delegate void OnReceivedPressureHandler(object sender, double Pressure);
        public event OnReceivedPressureHandler OnReceivedPressure = null;

        private ArduinoSession Session = null;
        private ISerialConnection Connection = null;

        private const byte SysExStart = 0xF0;
        private const byte SysExEnd = 0xF7;

        private enum MessageIds
        {
            RequestProduct = 0xF0,
            CurrentProduct = 0xF1,

            RequestPulseWidth = 0x02,
            CurrentPulseWidth = 0x03,
            RequestPressure = 0x04,
            CurrentPressure = 0x05,
            StartContinuousMeasurement = 0x06,
            StopContinuousMeasurement = 0x07
        }

        /// <summary>
        /// true if connected to the tester
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return (Connection != null) && Connection.IsOpen;
            }
        }

        /// <summary>
        /// Finds and connects to the tester
        /// </summary>
        /// <param name="ProductUID">The unique ID of the Tune-o-Matic</param>
        public void Connect
            (
            byte ProductUID
            )
        {
            if (Connection != null && Connection.IsOpen)
            {
                return;
            }

            Connection = EnhancedSerialConnection.Find();
            if (Connection == null)
            {
                throw new Exception("No MPS Tune-o-Matic found. Is it connected to the PC?");
            }

            Firmware firmware;
            try
            {
                Session = new ArduinoSession(Connection);
                Session.MessageReceived += Session_MessageReceived;

                firmware = Session.GetFirmware();
            }
            catch
            {
                throw new Exception("Unable to connect to MPS Tune-o-Matic. Please try again");
            }

            bool ProductCheckCompleted = false;
            bool ProductCheckFailed = false;
            byte FirmwareMajorVersion = 0;
            byte FirmwareMinorVersion = 0;
            OnReceivedProduct += (sender, pid, major, minor) =>
            {
                if (pid != ProductUID)
                {
                    ProductCheckFailed = true;
                }
                else
                {
                    FirmwareMajorVersion = major;
                    FirmwareMinorVersion = minor;
                }

                ProductCheckCompleted = true;
            };
            RequestProduct();
            while (!ProductCheckCompleted)
            {
                Thread.Sleep(50);
            }
            if (ProductCheckFailed)
            {
                Disconnect();
                throw new Exception("Unrecognized hardware. Is the MPS Tune-o-Matic connected to the PC?");
            }

            if (OnConnected != null)
            {
                OnConnected(this, Connection.PortName, Connection.BaudRate, FirmwareMajorVersion, FirmwareMinorVersion);
            }
        }

        /// <summary>
        /// Disconnects from the tester
        /// </summary>
        public void Disconnect
            (
            )
        {
            if (Connection == null || !Connection.IsOpen)
            {
                return;
            }

            RequestStopContinuousMeasurement();
            Session.MessageReceived -= Session_MessageReceived;
            Connection.Close();
            Connection = null;
            Session = null;

            if (OnDisconnected != null)
            {
                OnDisconnected(this);
            }
        }

        /// <summary>
        /// Tells the tune-o-matic to send the product details
        /// </summary>
        private void RequestProduct
            (
            )
        {
            byte[] Buffer = new byte[3] { SysExStart, (byte)MessageIds.RequestProduct, SysExEnd };
            Connection.Write(Buffer, 0, 3);
        }

        /// <summary>
        /// Tells the tune-o-matic to send the current atmospheric pressure
        /// </summary>
        public void RequestPressure
            (
            )
        {
            byte[] Buffer = new byte[3] { SysExStart, (byte)MessageIds.RequestPressure, SysExEnd };
            Connection.Write(Buffer, 0, 3);
        }

        /// <summary>
        /// Tells the tune-o-matic to send the current pulse width
        /// </summary>
        public void RequestPulseWidth
            (
            )
        {
            byte[] Buffer = new byte[3] { SysExStart, (byte)MessageIds.RequestPulseWidth, SysExEnd };
            Connection.Write(Buffer, 0, 3);
        }

        /// <summary>
        /// Tells the tune-o-matic to continually send the pulse width
        /// </summary>
        public void RequestStartContinuousMeasurement
            (
            )
        {
            byte[] Buffer = new byte[3] { SysExStart, (byte)MessageIds.StartContinuousMeasurement, SysExEnd };
            Connection.Write(Buffer, 0, 3);
        }

        /// <summary>
        /// Tells the tune-o-matic to continually send the pulse width
        /// </summary>
        public void RequestStopContinuousMeasurement
            (
            )
        {
            byte[] Buffer = new byte[3] { SysExStart, (byte)MessageIds.StopContinuousMeasurement, SysExEnd };
            Connection.Write(Buffer, 0, 3);
        }

        /// <summary>
        /// Called when a message is received from the tester
        /// </summary>
        /// <param name="sender">Sending session</param>
        /// <param name="eventArgs">Received message</param>
        private void Session_MessageReceived(object sender, FirmataMessageEventArgs eventArgs)
        {
            if (eventArgs.Value.Type == MessageType.StringData)
            {
                // not used
            }
            else if (eventArgs.Value.Type == MessageType.SysEx)
            {
                byte[] Buffer = eventArgs.Value.Value as byte[];

                if ((Buffer[0] == (byte)MessageIds.CurrentProduct) && (Buffer.Length == 4))
                {
                    if (OnReceivedProduct != null)
                    {
                        OnReceivedProduct(this, Buffer[1], Buffer[2], Buffer[3]);
                    }
                }
                else if ((Buffer[0] == (byte)MessageIds.CurrentPressure) && (Buffer.Length == 5))
                {
                    double Pressure = BitConverter.ToSingle(Buffer, 1);
                    
                    if (OnReceivedPressure != null)
                    {
                        OnReceivedPressure(this, Pressure);
                    }
                }
                else if ((Buffer[0] == (byte)MessageIds.CurrentPulseWidth) && (Buffer.Length == 3))
                {
                    UInt16 PulseWidth = (UInt16)BitConverter.ToInt16(Buffer, 1);

                    if (OnReceivedPulseWidth != null)
                    {
                        OnReceivedPulseWidth(this, PulseWidth);
                    }
                }
            }
        }
    }
}
