/*
 * Object: HJS.ECU.Tester.Testerprotocol
 * Description: Protocol to programming device
 * 
 * $LastChangedDate: 2013-03-05 13:39:53 +0100 (Di, 05 Mrz 2013) $
 * $LastChangedRevision: 19 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/tags/Version_1_3_1/lib_cs_win32_hjsecu/TesterProtocol.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using HJS.ECU.Port;

namespace HJS.ECU.Tester
{
    /// <summary>
    /// Protocol for ECU Tester
    /// </summary>
    public class TesterProtocol : IDisposable
    {
        /// <summary>
        /// Communication object for Serial access to Tester
        /// </summary>
        private readonly Comm mPort;

        private readonly static UInt16 mOrderDelay = 15;    //10;

        /// <summary>
        /// Enumeration for output values of output command
        /// </summary>
        private enum OutputValue : byte
        {
            KL15Off = 1,
            KL15On = 2,
            KL30Off = 3,
            KL30On = 4,
            LedRed = 5,
            LedGreen = 6,
            LedBothOff = 7,
            LedAlternating = 8,
            LedFlashing = 9,
            LedBothOn = 10,
            free1 = 11,
            free2 = 12,
            EcuHoldOff = 13,
            EcuHoldOn = 14,
            LedRedOff = 15,
            LedGreenOff = 16
        }

        /// <summary>
        /// Enumeration for different modes of red and green lamp
        /// </summary>
        public enum LampMode
        {
            /// <summary>
            /// Lamps both off
            /// </summary>
            BothOff = 0,
            /// <summary>
            /// Lamps both on
            /// </summary>
            BothOn,
            /// <summary>
            /// Lamps flashing alternate
            /// </summary>
            AltenateFlashing,
            /// <summary>
            /// Lamps flashing synchronous
            /// </summary>
            SynchronousFlashing,
            /// <summary>
            /// Lamp red on
            /// </summary>
            RedOnly,
            /// <summary>
            /// Lamp green on
            /// </summary>
            GreenOnly
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        public TesterProtocol(string strPortName, byte[] ucaKey)
        {
            byte[] Client = { 84, 69, 83, 84, 69, 82, 32 };
            mPort = new SerialDirect(strPortName, 14, ucaKey, Client);
            mPort.pVersion = 14;
        }

        /// <summary>
        /// Dispose of managed and native resouces
        /// </summary>
        /// <param name="disposing">True if managed members should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mPort != null) mPort.Disconnect();
            }
            else
            {
            }
        }

        /// <summary>
        /// Dispose of instantiable members
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Connect to ECU
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue Connect()
        {
            // versions check fehlt!
            ReturnValue ret = mPort.Connect();
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
                    if ((VersionBuffer[0] != 0)
                        && (VersionBuffer[1] != 0)
                        && (VersionBuffer[2] != 40)
                        && (VersionBuffer[3] != 8))
                    {
                        ret = ReturnValue.VersionMismatch;
                    }
                }
            }
            return ret;

        }

        /// <summary>
        /// Disconnect from ECU
        /// </summary>
        public void Disconnect()
        {
            if (mPort != null)
            {
                mPort.Disconnect();
            }
        }

        /// <summary>
        /// Eject ECU
        /// </summary>
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

                // todo: rety fehlt!
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>
        /// Turn on / off red and green lamp
        /// </summary>
        /// <param name="mode">Lamp mode</param>
        /// <returns>True on success</returns>
        public bool Lamps(LampMode mode)
        {
            UInt16 uiValue = 0;
            ReturnValue ret = ReturnValue.ComOrderFailed;
            switch (mode)
            {
                case LampMode.BothOff:
                    uiValue = (UInt16)OutputValue.LedBothOff;
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    if (ret == ReturnValue.Retry){
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    }
                    break;
                case LampMode.BothOn:
                    uiValue = (UInt16)OutputValue.LedBothOn;
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    if (ret == ReturnValue.Retry)
                    {
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    }
                    break;
                case LampMode.AltenateFlashing:
                    uiValue = (UInt16)OutputValue.LedAlternating;
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    if (ret == ReturnValue.Retry)
                    {
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    }
                    break;
                case LampMode.SynchronousFlashing:
                    uiValue = (UInt16)OutputValue.LedFlashing;
                    if (ret == ReturnValue.Retry)
                    {
                        ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    }
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, uiValue, mOrderDelay);
                    break;
                case LampMode.RedOnly:
                    uiValue = (UInt16)OutputValue.LedBothOff; //LedGreenOff;
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
                    uiValue = (UInt16)OutputValue.LedBothOff; //LedRedOff;
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

        /// <summary>
        /// Check if ECU is detected
        /// </summary>
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

        /// <summary>
        /// Lock or unlock tester
        /// </summary>
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

        /// <summary>
        /// Turn on supply voltages
        /// </summary>
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

        /// <summary>
        /// Turn off supply voltages and wait for ECU to shut down
        /// </summary>
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
                System.Threading.Thread.Sleep(1000);
                ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL30Off, mOrderDelay);
                if (ret == ReturnValue.Retry)
                {
                    ret = mPort.Order(Comm.OrderByte.TesterOutputs, (UInt16)OutputValue.KL30Off, mOrderDelay);
                }
            }
            return (ret == ReturnValue.NoError);
        }

        /// <summary>
        /// Turn on flash mode
        /// Tester delay time of tester (version 0.40.8) of 250ms + 250ms + 800ms + 250ms = 1550ms
        /// </summary>
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

        /// <summary>
        /// Turn off flash mode
        /// Tester delay time of tester (version 0.40.8) of 250ms + 250ms = 550ms
        /// </summary>
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

        /// <summary>
        /// Get version number of tester firmware
        /// </summary>
        /// <returns>Struct of firmware version</returns>
        public HJS.Block.VersionT GetVersion()
        {
            byte[] Buffer;
            HJS.Block.VersionT retV;
            ReturnValue ret = mPort.Read(Comm.OrderByte.TesterVersion, out Buffer);
            if ((ret == ReturnValue.NoError) && (Buffer.Length == 4))
            {
                // auswerten
                retV.Hauptversion = (UInt16)(Buffer[0] * 256 + Buffer[1]);
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
    }
}
