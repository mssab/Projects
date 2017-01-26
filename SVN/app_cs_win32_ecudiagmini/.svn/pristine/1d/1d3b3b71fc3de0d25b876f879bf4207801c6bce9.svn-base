/*
 * Object: HJS.ECU.ProduktionsDatenBlock
 * Description: production data
 *
 * $LastChangedDate: 2012-06-01 15:44:14 +0200 (Fr, 01 Jun 2012) $
 * $LastChangedRevision: 2 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden21/svn/app_cs_win32_ecudiagmini/trunk/lib_cs_win32_hjsecu/ProduktionsDatenBlock.cs $
 *
 * LastReviewDate:
 * LastReviewRevision:
 * LastReviewBy:
 */
using System;

namespace HJS.ECU
{
    /// <summary>
    /// Block of production data
    /// </summary>
    public class ProductionDataBlock:Block
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductionDataBlock()
        {
            Type = BlockId.IdProduction;
            Version = 1;
            DataSize = 10;// 16 - 6;

            mBlockData = new byte[DataSize];
            //
            mBlockData[0] = 0;  // sn 1/4
            mBlockData[1] = 0;  // sn 2/4
            mBlockData[2] = 0;  // sn 3/4
            mBlockData[3] = 0;  // sn 4/4
            mBlockData[4] = 0;  // Minute of production
            mBlockData[5] = 0;  // Hour of production
            mBlockData[6] = 0;  // Day in month of productiondate
            mBlockData[7] = 0;  // Month of production
            mBlockData[8] = 0;  // Year of production
            mBlockData[9] = 0;  //(uc) Temperature offset for PSoC
        }

        /// <summary>
        /// Accessors for ECU production date
        /// </summary>
        public DateTime ProductionDate
        {
            get
            {
                DateTime ret;
                if (mBlockData[5] == 255 && mBlockData[4] == 255)
                {
                    // kein Datum Vorhanden!
                    ret = DateTime.Now;
                }
                else
                {
                    ret = new DateTime(mBlockData[8] + 2000, mBlockData[7],
                        mBlockData[6], mBlockData[5], mBlockData[4], 0);
                }
                return ret;
            }
            set
            {
                mBlockData[4] = (byte)(value.Minute);
                mBlockData[5] = (byte)(value.Hour);
                mBlockData[6] = (byte)(value.Day);
                mBlockData[7] = (byte)(value.Month);
                mBlockData[8] = (byte)(value.Year % 100);
            }
        }

        /// <summary>
        /// Accessors for ECU serial number
        /// </summary>
        public UInt32 SerialNumber
        {
            get
            {
                UInt32 ret = (UInt32)(mBlockData[3] * 256);
                ret += mBlockData[2];
                ret = (UInt32)(ret * 256);
                ret += mBlockData[1];
                ret = (UInt32)(ret * 256);
                ret += mBlockData[0];
                return ret;
            }
            set
            {
                byte[] buffer = BitConverter.GetBytes(value);
                mBlockData[0] = buffer[0];
                if (buffer.Length > 0) mBlockData[1] = buffer[1];
                if (buffer.Length > 1) mBlockData[2] = buffer[2];
                if (buffer.Length > 2) mBlockData[3] = buffer[3];
            }
        }

        /// <summary>
        /// Accessors for ECU environmental temperature offset
        /// </summary>
        public int TempertureOffset
        {
            get
            {
                return (int)mBlockData[9];
            }
            set
            {
                mBlockData[9] = (byte)value;
            }
        }

        /// <summary>
        /// Set Production data, date is now
        /// </summary>
        /// <param name="serialNumber">Serial number of ECU</param>
        /// <param name="temperature">Temperature</param>
        public void SetData(UInt32 serialNumber, int temperature)
        {
            SerialNumber = serialNumber;
            ProductionDate = DateTime.Now;
            TempertureOffset = temperature;
        }
    }
}
