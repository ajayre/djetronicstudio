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

        private bool Connected = false;
        private ArduinoSession Session = null;
        private ISerialConnection Connection = null;

        private const byte SysExStart = 0xF0;
        private const byte SysExEnd = 0xF7;

        private enum MessageIds
        {
            RequestProduct = 0xF0,
            CurrentProduct = 0xF1,
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
        /// Tells the tester to send the product details
        /// </summary>
        private void RequestProduct
            (
            )
        {
            byte[] Buffer = new byte[3] { SysExStart, (byte)MessageIds.RequestProduct, SysExEnd };
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
            }
        }
    }
}
