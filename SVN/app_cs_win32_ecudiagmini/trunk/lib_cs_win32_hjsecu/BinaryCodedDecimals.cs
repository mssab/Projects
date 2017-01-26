/*
 * Object: HJS.BinaryCodedDecimals
 * Description: Binary coded decimals
 *
 * $LastChangedDate: 2012-06-01 15:44:14 +0200 (Fr, 01 Jun 2012) $
 * $LastChangedRevision: 2 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/trunk/lib_cs_win32_hjsecu/BinaryCodedDecimals.cs $
 *
 * LastReviewDate:
 * LastReviewRevision:
 * LastReviewBy:
 */
using System;

namespace HJS
{
    /// <summary>
    /// Class for calculating binary coded decimals
    /// </summary>
    public class BinaryCodedDecimals
    {
        /// <summary>
        /// Convert decimal byte to BCD byte
        /// </summary>
        /// <param name="decimalByte">Decimal byte</param>
        /// <returns>BCD byte</returns>
        public static byte ByteToBCD(byte decimalByte)
        {
            byte ret = (byte)(decimalByte / 10);
            ret = (byte)(ret * 16);
            ret = (byte)(ret + (decimalByte % 10));
            return ret;
        }

        /// <summary>
        /// Convert BCD byte to decimal byte
        /// </summary>
        /// <param name="binaryCodedDecimal">BCD byte</param>
        /// <returns>Decimal byte</returns>
        public static byte BCDToByte(byte binaryCodedDecimal)
        {
            byte ret = (byte)(binaryCodedDecimal & 0xF0);
            ret = (byte)(ret / 16);
            ret = (byte)(ret * 10);
            ret = (byte)(ret + (binaryCodedDecimal & 0x0F));
            return ret;
        }
    }
}
