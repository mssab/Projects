/*
 * Object: HJS.ECU.Port.SerialModem
 * Description: Communication object for connection to HJS-ECU via modem
 * 
 * $LastChangedDate: 2012-06-01 15:44:14 +0200 (Fr, 01 Jun 2012) $
 * $LastChangedRevision: 2 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/tags/Version_1_3_1/lib_cs_win32_hjsecu/SerialModem.cs $
 * 
 * LastReviewDate: 
 * LastReviewDRevision: 
 * LastReviewDBy: 
 */
using System;

namespace HJS.ECU.Port
{
    /// <summary>
    /// Communication object class for serial modem
    /// </summary>
    public class SerialModem : Comm
    {
        //private byte mVersion;

        //public byte Version
        //{
        //    get { return mVersion;}
        //    set { mVersion = value; }
        //}

        /// <summary>
        /// Connect
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Connect()
        {
            return 1;// ReturnValue.General;
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public override void Disconnect()
        {
        }

        /// <summary>
        /// Read data
        /// </summary>
        /// <param name="STB">Order</param>
        /// <param name="Data">Target byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Read(OrderByte STB, out byte[] Data)
        {
            Data = new byte[2];
            return 1;// ReturnValue.General;
        }

        /// <summary>
        /// Read data
        /// </summary>
        /// <param name="STB">Order</param>
        /// <param name="Value">Value of order</param>
        /// <param name="Data">Target byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Read(OrderByte STB, UInt16 Value, out byte[] Data)
        {
            Data = new byte[2];
            return 1;// ReturnValue.General;
        }

        /// <summary>
        /// Send order
        /// </summary>
        /// <param name="STB">Order</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Order(OrderByte STB)
        {
            return 1;// ReturnValue.General;
        }
    }
}
