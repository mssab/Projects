﻿/*
 * Object: HJS.ECU.Protocol.Testerprotocol
 * Description: Protocol to programming device
 * 
 * $LastChangedDate: 2015-01-13 15:57:13 +0100 (Di, 13 Jan 2015) $
 * $LastChangedRevision: 81 $
 * $LastChangedBy: jdr $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Protocol/TesterProtocol.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using HJS.ECU.Port;

namespace HJS.ECU.Protocol
{
    /// <summary>Protocol for ECU Tester</summary>
    public class TesterProtocol : IDisposable
    {
        /// <summary>Communication object for Serial access to Tester</summary>
        private readonly SerialDirect mPort;

        private readonly static UInt16 mOrderDelay = 15;

        /// <summary>Enumeration for output values of output command</summary>
        private enum OutputValue : byte
        {
            /// <summary>Ignition off</summary>
            KL15Off = 1,
            /// <summary>Ignition on</summary>
            KL15On = 2,
            /// <summary>Battery voltage off</summary>
            KL30Off = 3,
            /// <summary>Battery voltage on</summary>
            KL30On = 4,
            /// <summary>Red Lamp on</summary>
            LedRed = 5,
            /// <summary>Green lamp on</summary>
            LedGreen = 6,
            /// <summary>All lamps off</summary>
            LedAllOff = 7,
            LedOnlyGreenOn = 8,
            LedYellowOn = 9,
            LedAllOn = 10,
            free1 = 11,
            free2 = 12,
            /// <summary>Device lock off</summary>
            EcuHoldOff = 13,
            /// <summary>Device lock on</summary>
            EcuHoldOn = 14,
            /// <summary>Red lamp off</summary>
            LedRedOff = 15,
            /// <summary>Green lamp off</summary>
            LedGreenOff = 16,
            Supply12 = 17,
            Supply24 = 18,
            LedYellowOff = 19,
            ResetTo12V = 20
        }

        /// <summary>Enumeration for different modes of red, yellow and green lamp</summary>
        public enum LampMode
        {
            /// <summary>Lamps all off</summary>
            AllOff = 0,
            /// <summary>Lamps all on</summary>
            AllOn,
            /// <summary>Lamp Yellow on</summary>
            YellowOnly,
            /// <summary>Lamp red on</summary>
            RedOnly,
            /// <summary>Lamp green on</summary>
            GreenOnly
        }

        /// <summary>Overloaded constructor</summary>
        public TesterProtocol(string strPortName, byte[] ucaKey)
        {
            byte[] Client = { 84, 69, 83, 84, 69, 82, 32 };
            mPort = new SerialDirect(strPortName, 14, ucaKey, Client);
            mPort.pVersion = 14;
        }

        /// <summary>Dispose of managed and native resouces</summary>
        /// <param name="disposing">True if managed members should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mPort != null) mPort.Dispose();
            }
            else
            {
            }
        }

        /// <summary>Dispose of instantiable members</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Connect to ECU</summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue Connect()
        {
            // versions check fehlt!
            ReturnValue ret = mPort.Connect();
            if (ret != ReturnValue.NoError) ret = mPort.Connect(); // einmal wiederholen
            if (ret == ReturnValue.NoError)
            {
                byte[] VersionBuffer;
                ret = mPort.Read(Comm.OrderByte.TesterVersion, out VersionBuffer);
                if (VersionBuffer.Length != 4)
                {
                    ret = ReturnValue.VersionMismatch;
                }
                else
                {
                    if ((VersionBuffer[0] != 1)
                        || (VersionBuffer[1] != 0)
                        || (VersionBuffer[2] != 0))
                    {
                        ret = ReturnValue.VersionMismatch;
                    }
                    else
                    {
                        ResetTo12V();
                    }
                }
            }
            return ret;

        }

        /// <summary>Disconnect from ECU</summary>
        public void Disconnect()
        {
            if (mPort != null)
            {
                mPort.Disconnect();
            }
        }

        /// <summary>Eject ECU</summary>
        /// <returns>True on success</returns>
        public bool EjectEcu()
        {
            // Switch off ignition
            ReturnValue ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL15Off, mOrderDelay);
            // Switch off supply
            if (ret == ReturnValue.NoError)
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL30Off, mOrderDelay);
            }
            // Unlock device
            if (ret == ReturnValue.NoError)
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.EcuHoldOff, mOrderDelay);
            }
            // Wait and check unlocking
            if (ret == ReturnValue.NoError)
            {
                byte[] StatusBuffer;
                System.Threading.Thread.Sleep(200);
                ret = mPort.Read(Comm.OrderByte.TesterLockState, out StatusBuffer);
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Turn on or off red, yellow and green lamp</summary>
        /// <param name="mode">Lamp mode</param>
        /// <returns>True on success</returns>
        public bool Lamps(LampMode mode)
        {
            UInt16 uiValue = 0;
            ReturnValue ret = ReturnValue.ComOrderFailed;
            switch (mode)
            {
                case LampMode.AllOff:
                    uiValue = (UInt16)OutputValue.LedAllOff;
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    if (ret == ReturnValue.Retry)
                    {
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    }
                    break;
                case LampMode.AllOn:
                    uiValue = (UInt16)OutputValue.LedAllOn;
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    if (ret == ReturnValue.Retry)
                    {
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    }
                    break;
                case LampMode.YellowOnly:
                    uiValue = (UInt16)OutputValue.LedAllOff; //LedGreenOff;
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    if (ret == ReturnValue.Retry)
                    {
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    }
                    if (ret == ReturnValue.NoError)
                    {
                        uiValue = (UInt16)OutputValue.LedYellowOn;
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                        if (ret == ReturnValue.Retry)
                        {
                            ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                        }
                    }
                    break;
                case LampMode.RedOnly:
                    uiValue = (UInt16)OutputValue.LedAllOff; //LedGreenOff;
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    if (ret == ReturnValue.Retry)
                    {
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    }
                    if (ret == ReturnValue.NoError)
                    {
                        uiValue = (UInt16)OutputValue.LedRed;
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                        if (ret == ReturnValue.Retry)
                        {
                            ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                        }
                    }
                    break;
                case LampMode.GreenOnly:
                    uiValue = (UInt16)OutputValue.LedAllOff; //LedRedOff;
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    if (ret == ReturnValue.Retry)
                    {
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    }
                    if (ret == ReturnValue.NoError)
                    {
                        uiValue = (UInt16)OutputValue.LedGreen;
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                        if (ret == ReturnValue.Retry)
                        {
                            ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                        }
                    }
                    break;
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Check if ECU is detected</summary>
        /// <returns>True if ECU is detected</returns>
        public bool IsEcuDetected()
        {
            byte[] Ecu;
            ReturnValue ret = mPort.Read(Comm.OrderByte.TesterDetectEcu, out Ecu);
            if ((ret == ReturnValue.NoError) && (Ecu.Length == 1))
            {
                // auswerten
                ret = Ecu[0] == 0 ? ReturnValue.NoError : ReturnValue.ComOrderFailed;
            }
            else
            {
                ret = ReturnValue.ComOrderFailed;
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Lock or unlock tester</summary>
        /// <param name="locked">Flag if tester locked</param>
        /// <returns>True if order was successfully</returns>
        public bool LockTester(bool locked)
        {
            ReturnValue ret = ReturnValue.ComOrderFailed;
            if (locked)
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.EcuHoldOn);
            }
            else
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.EcuHoldOff);
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Turn on supply voltages</summary>
        /// <returns>True if order was successfully</returns>
        public bool SupplyOn()
        {
            ReturnValue ret = ReturnValue.ComOrderFailed;
            ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL30On, mOrderDelay);
            if (ret == ReturnValue.Retry)
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL30On, mOrderDelay);
            }
            if (ret == ReturnValue.NoError)
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL15On, mOrderDelay);
                if (ret == ReturnValue.Retry)
                {
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL15On, mOrderDelay);
                }
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Turn off supply voltages and wait for ECU to shut down</summary>
        /// <returns>True if order was successfully</returns>
        public bool SupplyOff()
        {
            ReturnValue ret = ReturnValue.ComOrderFailed;
            ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL15Off, mOrderDelay);
            if (ret == ReturnValue.Retry)
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL15Off, mOrderDelay);
            }
            if (ret == ReturnValue.NoError)
            {
                System.Threading.Thread.Sleep(1500);
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL30Off, mOrderDelay);
                if (ret == ReturnValue.Retry)
                {
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL30Off, mOrderDelay);
                }
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Turn on flash mode
        /// Tester delay time of tester (version 0.40.8) of 250ms + 250ms + 800ms + 250ms = 1550ms</summary>
        /// <returns>True if order was successfully</returns>
        public bool FlashModeOn()
        {
            ReturnValue ret = mPort.Order(Comm.OrderByte.TesterFlash, 0, 1600);
            //System.Threading.Thread.Sleep(3000);
            //ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL15Off);
            if (ret == ReturnValue.Retry)
            {
                ret = mPort.Order(Comm.OrderByte.TesterFlash, 0, 1600);
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Turn off flash mode
        /// Tester delay time of tester (version 0.40.8) of 250ms + 250ms = 550ms</summary>
        /// <returns>True if order was successfully</returns>
        public bool FlashModeOff()
        {
            ReturnValue ret = mPort.Order(Comm.OrderByte.TesterNoFlash, 0, 500);
            if (ret == ReturnValue.Retry)
            {
                ret = mPort.Order(Comm.OrderByte.TesterNoFlash, 0, 500);
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Get version number of tester firmware</summary>
        /// <returns>Struct of firmware version</returns>
        public HJS.Block.VersionT GetVersion()
        {
            byte[] Buffer;
            HJS.Block.VersionT retV;
            ReturnValue ret = mPort.Read(Comm.OrderByte.TesterVersion, out Buffer);
            if ((ret == ReturnValue.NoError) && (Buffer.Length == 4))
            {
                // auswerten
                retV.Hauptversion = (UInt16)(Buffer[0] + Buffer[1] * 256);
                retV.Nebenversion = Buffer[2];
                retV.Revision = Buffer[3];
            }
            else
            {
                retV.Hauptversion = 0;
                retV.Nebenversion = 0;
                retV.Revision = 0;
            }
            return retV;
        }

        /// <summary>Reset Programmer I/O and set voltage to 12V</summary>
        /// <returns>True if order was successfully</returns>
        public bool ResetTo12V()
        {
            ReturnValue ret = ReturnValue.ComOrderFailed;
            ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.ResetTo12V, mOrderDelay);
            if (ret == ReturnValue.Retry)
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.ResetTo12V, mOrderDelay);
            }

            return (ret == ReturnValue.NoError);
        }

        /// <summary>Set battery voltage to 12 volt</summary>
        /// <returns>True if order was successfully</returns>
        public bool Supply12V()
        {
            ReturnValue ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.Supply12, mOrderDelay);
            if (ret == ReturnValue.Retry)
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.Supply12, mOrderDelay);
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Set battery voltage to 24 volt</summary>
        /// <returns>True if order was successfully</returns>
        public bool Supply24V()
        {
            ReturnValue ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.Supply24, mOrderDelay);
            if (ret == ReturnValue.Retry)
            {
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.Supply24, mOrderDelay);
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Send cyclic UDA CAN messages</summary>
        /// <param name="cyclicEnable">Enable flag for sending</param>
        /// <returns>True if order was successfully</returns>
        public bool CanSendCyclic(bool cyclicEnable)
        {
            ReturnValue ret = mPort.Order(Comm.OrderByte.TesterSendUdaCyclic, (cyclicEnable ? (UInt16)1 : (UInt16)0), mOrderDelay);
            if (ret == ReturnValue.Retry)
            {
                ret = mPort.Order(Comm.OrderByte.TesterSendUdaCyclic, (cyclicEnable ? (UInt16)1 : (UInt16)0), mOrderDelay);
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>Read tester temperature</summary>
        /// <param name="Temperature">Tempearture of tester</param>
        /// <returns>True if reading was successfully</returns>
        public bool ReadTemperature(out double Temperature)
        {
            byte[] buffer;
            UInt16 t;
            ReturnValue ret = mPort.Read(Comm.OrderByte.TesterTemperature, out buffer, 100);
            if (ret == ReturnValue.Retry)
            {
                ret = mPort.Read(Comm.OrderByte.TesterTemperature, out buffer, 100);
            }
            if (ret == ReturnValue.NoError)
            {
                if (buffer.Length > 1)
                {
                    t = (UInt16)((UInt16)(buffer[1]) * 256 + buffer[0]);
                    Temperature = t * 0.0977293262;
                    Temperature = Temperature - 49.9857170407;
                    Temperature = Math.Round(Temperature, 1);
                    return true;
                }
                else
                {
                    Temperature = 0;
                    return false;
                }
            }
            else
            {
                Temperature = 0;
                return false;
            }
        }
    }
}
