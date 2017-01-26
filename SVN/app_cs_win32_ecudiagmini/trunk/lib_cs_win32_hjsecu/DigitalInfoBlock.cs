/*
 * Object: HJS.ECU.Protocol.DigitalInfoBlock
 * Description: Block of digital informations
 * 
 * $LastChangedDate: 2012-08-30 15:43:34 +0200 (Do, 30 Aug 2012) $
 * $LastChangedRevision: 11 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/trunk/lib_cs_win32_hjsecu/DigitalInfoBlock.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU //.Protocol
{
    /// <summary>
    /// Object class for block of digital informations of ECU
    /// </summary>
    public class DigitalInfoBlock : Block
    {
        #region Private member variables
        /// <summary>
        /// Data byte of digital info block for year of production
        /// </summary>
        private byte mProductionYear;

        /// <summary>
        /// Data byte of digital info block for month of production
        /// </summary>
        private byte mProductionMonth;

        /// <summary>
        /// Data byte of digital info block for day of production
        /// </summary>
        private byte mProductionDay;

        /// <summary>
        /// Data byte of digital info block for hour of production
        /// </summary>
        private byte mProductionHour;

        /// <summary>
        /// Data byte of digital info block for minute of production
        /// </summary>
        private byte mProductionMinute;

        /// <summary>
        /// Data byte of digital info block for serial number first byte (LSB)
        /// </summary>
        private byte mSerialNumber0;

        /// <summary>
        /// Data byte of digital info block for serial number second byte
        /// </summary>
        private byte mSerialNumber1;

        /// <summary>
        /// Data byte of digital info block for serial number third byte
        /// </summary>
        private byte mSerialNumber2;

        /// <summary>
        /// Data byte of digital info block for serial number fourth byte (MSB)
        /// </summary>
        private byte mSerialNumber3;

        /// <summary>
        /// Data byte of digital info block for low byte of hardware main version
        /// </summary>
        private byte mHardwareMainLowByte;

        /// <summary>
        /// Data byte of digital info block for high byte of hardware main version
        /// </summary>
        private byte mHardwareMainHighByte;

        /// <summary>
        /// Data byte of digital info block for hardware secondary version
        /// </summary>
        private byte mHardwareMidByte;

        /// <summary>
        /// Data byte of digital info block for hardware revision
        /// </summary>
        private byte mHardwareRevision;

        /// <summary>
        /// Data byte of digital info block for low byte of software main version
        /// </summary>
        private byte mSoftwareMainLowByte;

        /// <summary>
        /// Data byte of digital info block for high byte of software main version
        /// </summary>
        private byte mSoftwareMainHighByte;

        /// <summary>
        /// Data byte of digital info block for software secondary version
        /// </summary>
        private byte mSoftwareMidByte;

        /// <summary>
        /// Data byte of digital info block for software revision
        /// </summary>
        private byte mSoftwareRevision;

        /// <summary>
        /// Data byte of digital info block for low byte of configuration main version
        /// </summary>
        private byte mConfigurationMainLowByte;

        /// <summary>
        /// Data byte of digital info block for high byte of configuration main version
        /// </summary>
        private byte mConfigurationMainHighByte;

        /// <summary>
        /// Data byte of digital info block for configuration secondary version
        /// </summary>
        private byte mConfigurationMidByte;

        /// <summary>
        /// Data byte of digital info block for configuration revision
        /// </summary>
        private byte mConfigurationRevision;

        /// <summary>
        /// Data byte of digital info block for low byte of data map main version
        /// </summary>
        private byte mDataMapMainLowByte;

        /// <summary>
        /// Data byte of digital info block for high byte of data map main version
        /// </summary>
        private byte mDataMapMainHighByte;

        /// <summary>
        /// Data byte of digital info block for data map secondary version
        /// </summary>
        private byte mDataMapMidByte;

        /// <summary>
        /// Data byte of digital info block for data map revision
        /// </summary>
        private byte mDataMapRevision;

        /// <summary>
        /// Data byte of digital info block for real time clock second
        /// </summary>
        private byte mEcuSecond;

        /// <summary>
        /// Data byte of digital info block for real time clock minute
        /// </summary>
        private byte mEcuMinute;

        /// <summary>
        /// Data byte of digital info block for real time clock hour
        /// </summary>
        private byte mEcuHour;

        /// <summary>
        /// Data byte of digital info block for real time clock day in month
        /// </summary>
        private byte mEcuDay;

        /// <summary>
        /// Data byte of digital info block for real time clock month
        /// </summary>
        private byte mEcuMonth;

        /// <summary>
        /// Data byte of digital info block for real time clock year
        /// </summary>
        private byte mEcuYear;

        /// <summary>
        /// Data byte array of digital info block for array of acquisition sources
        /// </summary>
        private byte[] mAqSource;

        /// <summary>
        /// Data byte of digital info block for first byte (LSB) of save-bit-mask of acquisition sources
        /// </summary>
        private byte mAqSaveMask0;

        /// <summary>
        /// Data byte of digital info block for second byte of save-bit-mask of acquisition sources
        /// </summary>
        private byte mAqSaveMask1;

        /// <summary>
        /// Data byte of digital info block for third byte of save-bit-mask of acquisition sources
        /// </summary>
        private byte mAqSaveMask2;

        /// <summary>
        /// Data byte of digital info block for fourth byte (MSB) of save-bit-mask of acquisition sources
        /// </summary>
        private byte mAqSaveMask3;

        /// <summary>
        /// Data byte of digital info block for ECU temperature offset (signed)
        /// </summary>
        private byte scTecuOffset;

        /// <summary>
        /// Data byte of digital info block for compatible software revision
        /// </summary>
        private byte mCompatibleRevision;

        /// <summary>
        /// Data byte of digital info block for number of acquisition ring flash blocks
        /// </summary>
        private byte mAqSizeNumBlocks;

        /// <summary>
        /// Data byte of digital info block for number of error ring flash blocks
        /// </summary>
        private byte mErrorRingNumSectors;

        /// <summary>
        /// Data byte of digital info block for version of empirical values
        /// </summary>
        private byte mEmpiricalVersion;

        /// <summary>
        /// Data byte array of digital info block for array of environmental values matching to error numbers
        /// </summary>
        private byte[,] mEnvironmentFigures;

        #endregion

        #region Properties
        /// <summary>
        /// Accessors for ECU production date
        /// Read only
        /// </summary>
        public DateTime ProductionDate
        {
            get
            {
                DateTime ret;
                if ((mProductionHour == 255 && mProductionMinute == 255)
                    || ((mProductionYear == 0 && mProductionMonth == 0 && mProductionDay == 0 && mProductionHour == 0 && mProductionMinute == 0))
                    )
                {
                    // Achtung: wenn kein Datum Vorhanden, dann wird
                    // auch keine Seriennummer vorhanden sein!
                    ret = DateTime.Now;
                }
                else
                {
                    ret = ((mProductionYear > 99)
                        || (mProductionMonth > 12)
                        || (mProductionMonth < 1)
                        || (mProductionDay > 31)
                        || (mProductionHour > 23)
                        || (mProductionMinute > 59)
                        ) ? DateTime.Now : new DateTime(mProductionYear + 2000, mProductionMonth,
                            mProductionDay, mProductionHour, mProductionMinute, 0, DateTimeKind.Utc);
                }
                return ret;
            }
        }

        /// <summary>
        /// Accessors for ECU serial number
        /// Read only
        /// </summary>
        public UInt32 SerialNumber
        {
            get
            {
                UInt32 ret = (UInt32)(mSerialNumber3 * 256);
                ret += mSerialNumber2;
                ret = (UInt32)(ret * 256);
                ret += mSerialNumber1;
                ret = (UInt32)(ret * 256);
                ret += mSerialNumber0;
                return ret;
            }
        }

        /// <summary>
        /// Accessors for version of hardware
        /// Read only
        /// </summary>
        public Block.VersionT HardwareVersion
        {
            get
            {
                Block.VersionT ret;
                ret.Hauptversion = (UInt16)(mHardwareMainHighByte * 256 + mHardwareMainLowByte);
                ret.Nebenversion = mHardwareMidByte;
                ret.Revision = mHardwareRevision;
                return ret;
            }
        }

        /// <summary>
        /// Accessors for version of software
        /// Read only
        /// </summary>
        public Block.VersionT SoftwareVersion
        {
            get
            {
                Block.VersionT ret;
                ret.Hauptversion = (UInt16)(mSoftwareMainHighByte * 256 + mSoftwareMainLowByte);
                ret.Nebenversion = mSoftwareMidByte;
                ret.Revision = mSoftwareRevision;
                return ret;
            }
        }

        /// <summary>
        /// Accessors for version of configuration
        /// Read only
        /// </summary>
        public Block.VersionT ConfigurationVersion
        {
            get
            {
                Block.VersionT ret;
                ret.Hauptversion = (UInt16)(mConfigurationMainHighByte * 256 + mConfigurationMainLowByte);
                ret.Nebenversion = mConfigurationMidByte;
                ret.Revision = mConfigurationRevision;
                return ret;
            }
        }

        /// <summary>
        /// Accessors for version of data map
        /// Read only
        /// </summary>
        public Block.VersionT DatamapVersion
        {
            get
            {
                Block.VersionT ret;
                ret.Hauptversion = (UInt16)(mDataMapMainHighByte * 256 + mDataMapMainLowByte);
                ret.Nebenversion = mDataMapMidByte;
                ret.Revision = mDataMapRevision;
                return ret;
            }
        }

        /// <summary>
        /// Accessors for Connection date
        /// Read only
        /// </summary>
        public DateTime ConnectionDate
        {
            get
            {
                DateTime ret = new DateTime(mEcuYear, mEcuMonth, mEcuDay,
                    mEcuHour, mEcuMinute, mEcuSecond);
                return ret;
            }
        }

        /// <summary>
        /// Accessors for acquisition save bit mask
        /// Read only
        /// </summary>
        public UInt32 AcquisitionSaveMask
        {
            get
            {
                UInt32 ret = (UInt32)(mAqSaveMask3 * 256);
                ret += mAqSaveMask2;
                ret = (UInt32)(ret * 256);
                ret += mAqSaveMask1;
                ret = (UInt32)(ret * 256);
                ret += mAqSaveMask0;
                return ret;
            }
        }

        /// <summary>
        /// Accessors for ECU temperature offset (signed)
        /// Read only
        /// </summary>
        public byte EcuTemperatureOffset
        {
            get { return scTecuOffset; }
        }

        /// <summary>
        /// Accessors for compatible software version
        /// Read only
        /// </summary>
        public byte CompatibleRevision
        {
            get { return mCompatibleRevision; }
        }

        /// <summary>
        /// Accessors for acquisition number of flash blocks
        /// Read only
        /// </summary>
        public byte AqSizeNumBlocks
        {
            get { return mAqSizeNumBlocks; }
        }

        /// <summary>
        /// Accessors for error ring number of flash blocks
        /// Read only
        /// </summary>
        public byte ErrorRingNumSectors
        {
            get { return mErrorRingNumSectors; }
        }

        /// <summary>
        /// Accessors for version of empirical value structure
        /// Read only
        /// </summary>
        public byte EmpiricalVersion
        {
            get { return mEmpiricalVersion; }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public DigitalInfoBlock()
        {
            Type = BlockId.IdInfoDig;
            Version = 2;
            DataSize = 238;// 244 - 6;

            mEnvironmentFigures = new byte[3, 64];
            mAqSource = new byte[10];
        }

        // todo: Parse() Pruefung der nicht block daten auf validitaet
        // todo: Vorgabe der V15 werte bei V14

        /// <summary>
        /// Read digital information (block) from byte array
        /// </summary>
        /// <param name="SourceData">Reference to source byte array</param>
        /// <returns>0 on success, see ReturnValue</returns>
        public ReturnValue Parse(ref byte[] SourceData)
        {
            //ReturnValue ret = ReadRaw(ref SourceData, true);
            ReturnValue ret = ReadRaw(ref SourceData, false);   // sollte nur fuer kleinere versionen gelten! (hier: 1 und 2)
            if (ret == ReturnValue.BlockNotFound || ret == ReturnValue.VersionMismatch)
            {
                // pruefung ?!?


                // Block structure software 0.*.*
                mProductionYear = SourceData[0];
                mProductionMonth = SourceData[1];
                mProductionDay = SourceData[2];
                mProductionHour = SourceData[3];
                mProductionMinute = SourceData[4];

                UInt32 EcuTime = (UInt32)(SourceData[5] * 256);
                EcuTime += SourceData[6];
                EcuTime = (UInt32)(EcuTime * 256);
                EcuTime += SourceData[7];
                DateTime et = ProductionDate;
                et.AddMinutes(EcuTime);
                mEcuSecond = (byte)et.Second;
                mEcuMinute = (byte)et.Minute;
                mEcuHour = (byte)et.Hour;
                mEcuDay = (byte)et.Day;
                mEcuMonth = (byte)et.Month;
                mEcuYear = (byte)(et.Year - 2000);

                mSerialNumber0 = SourceData[8];
                mSerialNumber1 = SourceData[9];
                mSerialNumber2 = SourceData[10];
                mSerialNumber3 = SourceData[11];

                mHardwareMainLowByte = SourceData[12];
                mHardwareMainHighByte = SourceData[13];
                mHardwareMidByte = SourceData[14];
                mHardwareRevision = SourceData[15];

                mSoftwareMainLowByte = SourceData[16];
                mSoftwareMainHighByte = SourceData[17];
                mSoftwareMidByte = SourceData[18];
                mSoftwareRevision = SourceData[19];

                mConfigurationMainLowByte = SourceData[20];
                mConfigurationMainHighByte = SourceData[21];
                mConfigurationMidByte = SourceData[22];
                mConfigurationRevision = SourceData[23];

                mDataMapMainLowByte = SourceData[24];
                mDataMapMainHighByte = SourceData[25];
                mDataMapMidByte = SourceData[26];
                mDataMapRevision = SourceData[27];

                for (int i = 0; i < 10; i++)
                {
                    mAqSource[i] = SourceData[28 + i];
                }

                mAqSaveMask0 = SourceData[38];
                mAqSaveMask1 = SourceData[39];
                mAqSaveMask2 = SourceData[40];
                mAqSaveMask3 = SourceData[41];

                scTecuOffset = 20;
                mCompatibleRevision = mSoftwareRevision;   // stimmt das?
                mAqSizeNumBlocks = 29;      // oder 13 ?
                mErrorRingNumSectors = 8;   // stimmt das?
                mEmpiricalVersion = 0;

                for (int i = 0; i < 3; i++) {
                    for (int j = 0; j < 64; j++)
                    {
                        mEnvironmentFigures[i, j] = SourceData[42 + i * 64 + j];
                    }
                }
            }
            if (ret == ReturnValue.NoError || ret == ReturnValue.ChecksumMismatch) // crc nur waehrend bugfix
            {
                // Block structure software 1.0.8
                mProductionYear = mBlockData[0];
                mProductionMonth = mBlockData[1];
                mProductionDay = mBlockData[2];
                mProductionHour = mBlockData[3];
                mProductionMinute = mBlockData[4];

                mSerialNumber0 = mBlockData[5];
                mSerialNumber1 = mBlockData[6];
                mSerialNumber2 = mBlockData[7];
                mSerialNumber3 = mBlockData[8];

                mHardwareMainLowByte = mBlockData[9];
                mHardwareMainHighByte = mBlockData[10];
                mHardwareMidByte = mBlockData[11];
                mHardwareRevision = mBlockData[12];

                mSoftwareMainLowByte = mBlockData[13];
                mSoftwareMainHighByte = mBlockData[14];
                mSoftwareMidByte = mBlockData[15];
                mSoftwareRevision = mBlockData[16];

                mConfigurationMainLowByte = mBlockData[17];
                mConfigurationMainHighByte = mBlockData[18];
                mConfigurationMidByte = mBlockData[19];
                mConfigurationRevision = mBlockData[20];

                mDataMapMainLowByte = mConfigurationMainLowByte;
                mDataMapMainHighByte = mConfigurationMainHighByte;
                mDataMapMidByte = mConfigurationMidByte;
                mDataMapRevision = mConfigurationRevision;

                mEcuSecond = mBlockData[21];
                mEcuMinute = mBlockData[22];
                mEcuHour = mBlockData[23];
                mEcuDay = mBlockData[24];
                mEcuMonth = mBlockData[25];
                mEcuYear = mBlockData[26];

                for (int i = 0; i < 10; i++)
                {
                    mAqSource[i] = mBlockData[27 + i];
                }

                mAqSaveMask0 = mBlockData[37];
                mAqSaveMask1 = mBlockData[38];
                mAqSaveMask2 = mBlockData[39];
                mAqSaveMask3 = mBlockData[40];

                scTecuOffset = mBlockData[41];
                mCompatibleRevision = mBlockData[42];
                mAqSizeNumBlocks = mBlockData[43];
                mErrorRingNumSectors = mBlockData[44];
                mEmpiricalVersion = mBlockData[45];

                for (int i = 0; i < 3; i++) {
                    for (int j = 0; j < 64; j++)
                    {
                        mEnvironmentFigures[i, j] = mBlockData[46 + i * 64 + j];
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Get measurement value identifier of acquisition source
        /// </summary>
        /// <param name="Position">Position of value in acquisition array</param>
        /// <returns>Value identifier, or 255 on error</returns>
        public byte GetAcquisitionSource(byte Position)
        {
            if (Position < mAqSource.Length)
            {
                return mAqSource[Position];
            }
            else
            {
                return 255;
            }
        }

        /// <summary>
        /// Get measurement value identifier of error ring environmental figures
        /// </summary>
        /// <param name="ErrorNumber">Appropriate error number (0 .. 63)</param>
        /// <param name="ValuePosition">Position of value (0 .. 2)</param>
        /// <returns>Value identifier, or 255 on error</returns>
        public byte GetErrorRingFigure(byte ErrorNumber, byte ValuePosition)
        {
            if ((ErrorNumber < 64) && (ValuePosition < 3))
            {
                return mEnvironmentFigures[ValuePosition, ErrorNumber];
            }
            else
            {
                return 255;
            }
        }
    }
}
