using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJetronicStudio
{
    public class Simulator
    {
        public delegate void OnConnectedHandler(object sender);
        public event OnConnectedHandler OnConnected = null;

        public delegate void OnDisconnectedHandler(object sender);
        public event OnDisconnectedHandler OnDisconnected = null;

        public delegate void OnShowMessageHandler(object sender, string Message);
        public event OnShowMessageHandler OnShowMessage = null;

        private bool Connected = false;

        public Simulator
            (
            )
        {
        }

        /// <summary>
        /// true if connected to the simulation
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return Connected;
            }
        }

        /// <summary>
        /// Finds and connects to the simulation
        /// </summary>
        public void Connect
            (
            )
        {
            if (Connected)
            {
                return;
            }

            Connected = true;

            if (OnConnected != null)
            {
                OnConnected(this);
            }
        }

        /// <summary>
        /// Disconnects from the tester
        /// </summary>
        public void Disconnect
            (
            )
        {
            if (!Connected)
            {
                return;
            }

            Connected = false;

            if (OnDisconnected != null)
            {
                OnDisconnected(this);
            }
        }
    }
}
