/*
 * Object: HJS.ECU.Protocol.ProtocolBase
 * Description: Base class of protocol layer for communication to HJS-ECU
 * 
 * $LastChangedDate: 2013-10-25 14:40:14 +0200 (Fr, 25 Okt 2013) $
 * $LastChangedRevision: 25 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/tags/Version_1_3_1/lib_cs_win32_hjsecu/ProtocolBase.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using HJS.ECU.Port;

namespace HJS.ECU.Protocol
{
    /// <summary>
    /// Abstact base class for protocols
    /// </summary>
    public abstract class ProtocolBase
    {
        private bool mLocalTime;

        /// <summary>
        /// Flag for display time format
        /// </summary>
        public bool LocalTime
        {
            get
            {
                return mLocalTime;
            }
            set
            {
                mLocalTime = value;
            }
        }

        /// <summary>
        /// Maximum size of data that can be received by one communication
        /// </summary>
        public const int MAX_READ_BUFFER_SIZE = 32768;

        /// <summary>
        /// Enumeration for supported languages
        /// </summary>
        public enum LanguageId
        {
            /// <summary>
            /// Identifier for german language
            /// </summary>
            German = 0,
            /// <summary>
            /// Identifier for english language
            /// </summary>
            English = 1,
            /// <summary>
            /// Identifier for french language
            /// </summary>
            French = 2,
            /// <summary>
            /// Identifier for italian language
            /// </summary>
            Italian = 3,
            /// <summary>
            /// Identifier for spanish language
            /// </summary>
            Spanish = 4,
            /// <summary>
            /// Identifier for polish language
            /// </summary>
            Polish = 5,
            /// <summary>
            /// Identifier for dutch language
            /// </summary>
            Dutch = 6
        }

        /// <summary>
        /// Enumeration for reboot modes
        /// </summary>
        public enum RebootMode
        {
            /// <summary>
            /// Boot in normal configuration
            /// </summary>
            RebootNormal = 0,
            /// <summary>
            /// Boot in emergency configuration
            /// </summary>
            RebootNotConfig = 1,
            /// <summary>
            /// Reset reconfigurable values and reboot
            /// </summary>
            ReconfigReset = 2
        }

        /// <summary>
        /// Version of this protocol
        /// </summary>
        private byte mVersion = 15;

        /// <summary>
        /// Accessors for protocol version
        /// Read only
        /// </summary>
        public byte ProtocolVersion
        {
            get { return mVersion; }
            set { mVersion = value; }
        }

        /// <summary>
        /// Communication object for Serial access to ECU
        /// </summary>
        public static Comm mEcu;

        /// <summary>
        /// Object of digital information block
        /// </summary>
        public DigitalInfoBlock mInfo;

        /// <summary>
        /// Semaphore for locking during active communication
        /// </summary>
        protected static bool mBusy;

        /// <summary>
        /// Object of active language block
        /// </summary>
        public LanguageBlock mLanguage;

        /// <summary>
        /// Object of ECU software dependencies
        /// </summary>
        public static Firmware mFirmware;

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="strPortName">Name of new serial port</param>
        /// <param name="ucaKey">Byte array of new ECU key</param>
        /// <param name="ConnectionType">Enumeration of connection type</param>
        public ProtocolBase(string strPortName, byte[] ucaKey, Comm.PortType ConnectionType)
        {
            byte[] Client = { 72, 74, 83, 45, 69, 67, 85 };
            switch (ConnectionType)
            {
                case Comm.PortType.Modem:
                    throw new Exception("The method or operation is not implemented.");
                //    mEcu = new SerialModem(/*strPortName, mVersion, ucaKey*/);
                //    mEcu.pVersion = mVersion;
                //    break;
                case Comm.PortType.Direct:
                default:
                    mEcu = new SerialDirect(strPortName, mVersion, ucaKey, Client);
                    break;
            }
            mFirmware = new Firmware();
            mBusy = false;
        }

        /// <summary>
        /// Connect to ECU
        /// </summary>
        /// <param name="language">Language: 0=german, 1=english, 2=french, 3=italian</param>
        /// <returns>0 on success, else see Error</returns>
        public abstract ReturnValue Connect(LanguageId language);

        /// <summary>
        /// Connect to ECU without reading language!
        /// </summary>
        /// <returns>0 on success, else see Error</returns>
        public abstract ReturnValue FastConnect();

        /// <summary>
        /// Disconnect from ECU
        /// </summary>
        public void Disconnect()
        {
            if (mEcu != null)
            {
                mEcu.Disconnect();
            }
        }

        /// <summary>
        /// Change port name
        /// A connection is disconnected, but not connected after changeing port name
        /// </summary>
        /// <param name="NewPortName">New port name</param>
        public void ChangePort(string NewPortName)
        {
            if (mEcu != null)
            {
                mEcu.Disconnect();
            }
            mEcu.PortName = NewPortName;
        }

        /// <summary>
        /// Change server identfication
        /// </summary>
        /// <param name="NewServer">New server identification</param>
        public void ChangeServer(Comm.ServerByte NewServer)
        {
            if (mEcu != null)
            {
                mEcu.Server = NewServer;
            }
        }

        /// <summary>
        /// Get last communication time stamp
        /// </summary>
        /// <returns>Minutes since programming of last communication time stamp</returns>
        public DateTime GetLastTimeStamp()
        {
            if (mInfo != null)
            {
                DateTime ret = mInfo.ProductionDate;
                if (mEcu.TimeStamp != 0xFFFFFFFF)
                {
                    ret = ret.AddMinutes((double)mEcu.TimeStamp);
                }
                return ret;
            }
            else
            {
                throw new NullReferenceException("Digital info block not initialized");
            }
        }

        /// <summary>
        /// Set ecu time
        /// </summary>
        /// <param name="NewTime">Date time object of new ecu time</param>
        /// <returns>True in success</returns>
        public ReturnValue SetTime(DateTime NewTime)
        {
            byte[] TimeBuffer = new byte[7];
            // Achtung ! Uhrzeit ist in UTC (ohne DST) !
            NewTime = NewTime.ToUniversalTime();
            TimeBuffer[0] = BinaryCodedDecimals.ByteToBCD((byte)NewTime.Second);  // Sekunde in BCD  
            TimeBuffer[1] = BinaryCodedDecimals.ByteToBCD((byte)NewTime.Minute);  // Minute in BCD  
            TimeBuffer[2] = BinaryCodedDecimals.ByteToBCD((byte)NewTime.Hour);  // Stunde in BCD  
            TimeBuffer[3] = BinaryCodedDecimals.ByteToBCD((byte)NewTime.DayOfWeek);  // Wochentag in BCD  
            TimeBuffer[4] = BinaryCodedDecimals.ByteToBCD((byte)NewTime.Day);  // Tag (im Monat) in BCD  
            TimeBuffer[5] = BinaryCodedDecimals.ByteToBCD((byte)NewTime.Month);  // Monat in BCD  
            TimeBuffer[6] = BinaryCodedDecimals.ByteToBCD((byte)(NewTime.Year%100));  // Jahr (ohne Jahrzehnt) in BCD  
            return mEcu.Write(Comm.OrderByte.SetTime, (UInt16)TimeBuffer.Length, ref TimeBuffer, 10);
        }

        /// <summary>
        /// Reboot ECU
        /// </summary>
        /// <param name="Mode">Mode of reboot</param>
        /// <returns>0 on success, else see Error</returns>
        public abstract ReturnValue Reboot(RebootMode Mode);

        /// <summary>
        /// Reset all empirical values and erase all flash ring memory
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue MasterReset();

        /// <summary>
        /// Erase blocks in ECU flash for parameters
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue EraseParamBlocks();

        /// <summary>
        /// Write parameter block to ECU
        /// </summary>
        /// <param name="ParameterBlock">Reference of parameter block</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue WriteParamBlock(ref Block ParameterBlock);

        /// <summary>Read parameter block from ecu</summary>
        /// <param name="Identifier">Block identifier</param>
        /// <param name="ParameterBlock">Referece of block to read to</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue ReadParamBlock(Block.BlockId Identifier, ref Block ParameterBlock);

        /// <summary>
        /// Check if connection is established
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue IsAlive()
        {
            return mEcu.Order(Comm.OrderByte.Alive);
        }

        /// <summary>Write configuration in old mode (erase and write for HW 1.34)</summary>
        /// <param name="Data">Source byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue WriteOldConfig(byte[] Data)
        {
            return mEcu.EraseAndWrite(Comm.OrderByte.WriteConfiguration, ref Data, 1500, 1500);
        }

        /// <summary>Write datamap in old mode (erase and write for HW 1.34)</summary>
        /// <param name="Data">Source byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue WriteOldDatamap(byte[] Data)
        {
            return mEcu.EraseAndWrite(Comm.OrderByte.WriteDataMap, ref Data, 1500, 500);
        }

        /// <summary>Write one language in old mode (erase and write for HW 1.34)</summary>
        /// <param name="Position">Position of language in ecu</param>
        /// <param name="Data">Source byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue WriteOldLanguage(int Position, byte[] Data)
        {
            switch (Position)
            {
                case 0:
                    return mEcu.EraseAndWrite(Comm.OrderByte.WriteLanguageDe, ref Data, 1500, 500);
                case 1:
                    return mEcu.EraseAndWrite(Comm.OrderByte.WriteLanguageEn, ref Data, 1500, 500);
                case 2:
                    return mEcu.EraseAndWrite(Comm.OrderByte.WriteLanguageFr, ref Data, 1500, 500);
                case 3:
                    return mEcu.EraseAndWrite(Comm.OrderByte.WriteLanguageIt, ref Data, 1500, 500);
                default:
                    return ReturnValue.LanguageNotValid;
            }
        }

        /// <summary>
        /// Write production data (SN, date, temperature)
        /// </summary>
        /// <param name="Data">Byte array of production data</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue WriteProductionData(ref byte[] Data);

        #region Digital Informations (DigiInfoBlock)

        /// <summary>
        /// Accessors for date of ECU programming (production date)
        /// Read only
        /// </summary>
        public DateTime DateOfProgramming
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.ProductionDate;
                }
                else
                {
                    DateTime ret = new DateTime(0);
                    return ret;
                }
            }
        }

        /// <summary>
        /// Accessors for date of connection to ECU (Time of reading digital info block)
        /// Read only)
        /// </summary>
        public DateTime DateOfConnection
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.ConnectionDate;
                }
                else
                {
                    DateTime ret = new DateTime(0);
                    return ret;
                }
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
                if (mInfo != null)
                {
                    return mInfo.SerialNumber;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Accessors for ECU hardware version
        /// Read only
        /// </summary>
        public Block.VersionT HardwareVersion
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.HardwareVersion;
                }
                else
                {
                    Block.VersionT ret;
                    ret.Hauptversion = 0;
                    ret.Nebenversion = 0;
                    ret.Revision = 0;
                    return ret;
                }
            }
        }

        /// <summary>
        /// Accessors for ECU software version
        /// Read only
        /// </summary>
        public Block.VersionT SoftwareVersion
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.SoftwareVersion;
                }
                else
                {
                    Block.VersionT ret;
                    ret.Hauptversion = 0;
                    ret.Nebenversion = 0;
                    ret.Revision = 0;
                    return ret;
                }
            }
        }

        /// <summary>
        /// Check if current software version is updateable
        /// If PSoC version 8 is on ECU, the ECU can be updated
        /// </summary>
        /// <param name="Version">Version string to be updated to</param>
        /// <returns>True if software is updatebale</returns>
        public bool IsUpdateableTo(string Version)
        {
            if (mInfo != null)
            {
                return mFirmware.IsUpdateableTo(Version);//_mInfo.SoftwareVersion);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Accessors for version of ECU configuration
        /// Read only
        /// </summary>
        public Block.VersionT ConfigurationVersion
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.ConfigurationVersion;
                }
                else
                {
                    Block.VersionT ret;
                    ret.Hauptversion = 0;
                    ret.Nebenversion = 0;
                    ret.Revision = 0;
                    return ret;
                    throw new NullReferenceException("Digital info block not initialized");
                }
            }
        }

        /// <summary>
        /// Accessors for version of ECU data map
        /// Read only
        /// </summary>
        public Block.VersionT DatamapVersion
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.DatamapVersion;
                }
                else
                {
                    Block.VersionT ret;
                    ret.Hauptversion = 0;
                    ret.Nebenversion = 0;
                    ret.Revision = 0;
                    return ret;
                }
            }
        }

        /// <summary>
        /// Accessors for ECU temperature offset
        /// Read only
        /// </summary>
        public byte EcuTemperatureOffset
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.EcuTemperatureOffset;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Accessors for compatible software version
        /// Read only
        /// </summary>
        public byte CompatibleRevision
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.CompatibleRevision;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Accessors for bit mask of acquisition saves
        /// Read only
        /// </summary>
        public UInt32 AcquisitionSaveMask
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.AcquisitionSaveMask;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Accessors for acquisition ring flash sectors 
        /// Read only
        /// </summary>
        public byte AcquisitionSectors
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.AqSizeNumBlocks;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get measurement value identifier of acquisition source
        /// </summary>
        /// <param name="Position">Position of value in acquisition array</param>
        /// <returns>Value identifier, or 255 on error</returns>
        public byte AcquisitionSource(byte Position)
        {
            if (mInfo != null)
            {
                return mInfo.GetAcquisitionSource(Position);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Accessors for number of error ring flash sectors
        /// Read only
        /// </summary>
        public byte ErrorRingSectors
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.ErrorRingNumSectors;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get measurement value identifier of error ring environmental figures
        /// </summary>
        /// <param name="ErrorNumber">Appropriate error number (0 .. 63)</param>
        /// <param name="ValuePosition">Position of value (0 .. 2)</param>
        /// <returns>Value identifier, or 255 on error</returns>
        public byte ErrorRingFigure(byte ErrorNumber, byte ValuePosition)
        {
            if (mInfo != null)
            {
                return mInfo.GetErrorRingFigure(ErrorNumber, ValuePosition);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Accessors for version of empirical values
        /// Read only
        /// </summary>
        public byte EmpiricalVersion
        {
            get
            {
                if (mInfo != null)
                {
                    return mInfo.EmpiricalVersion;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Convert minute after programming to date (in UTC w/o DST)
        /// </summary>
        /// <param name="MinuteAfterProgDate">Minute since programming</param>
        /// <returns>Date</returns>
        public DateTime GetDateTimeFromMinute(UInt32 MinuteAfterProgDate)
        {
            if (mInfo != null)
            {
                DateTime ret = mInfo.ProductionDate;
                if (MinuteAfterProgDate != 0xFFFFFFFF)
                {
                    ret = ret.AddMinutes((double)MinuteAfterProgDate);
                }
                return ret;
            }
            else
            {
                DateTime ret = new DateTime(0);
                return ret;
            }
        }

        #endregion

        /// <summary>
        /// Read info text from ECU
        /// </summary>
        /// <param name="info">Output of info text</param>
        /// <returns></returns>
        public ReturnValue ReadInfoText(out string info)
        {
            byte[] buffer;
            ReturnValue ret = mEcu.Read(Comm.OrderByte.ReadInfoText, out buffer);
            if (ret == ReturnValue.NoError)
            {
                info = System.Text.ASCIIEncoding.ASCII.GetString(buffer);
            }
            else
            {
                info = "";
            }
            return ret;
        }

        #region Firmware

        /// <summary>
        /// Get position of value in communication table by identifier
        /// </summary>
        /// <param name="ValueId">Identifier of value</param>
        /// <returns>Position in table, or 255 if not found</returns>
        public byte GetValuePosition(byte ValueId)
        {
            if (mFirmware != null)
            {
               return mFirmware.GetValuePosition(ValueId);
            }
            else
            {
                throw new NullReferenceException("Firmware object not initialized");
            }
        }

        /// <summary>
        /// Get number of actual values
        /// </summary>
        /// <returns>Number of actual values</returns>
        public byte GetValueNumber()
        {
            if (mFirmware != null)
            {
                if (mFirmware.SoftwareRevision == 0)
                {
                    // noch nicht verbunden!
                    return 0;
                }
                else
                {
                    return mFirmware.GetValueNumber();
                }
            }
            else
            {
                throw new NullReferenceException("Firmware object not initialized");
            }
        }

        #endregion

        #region Language
        /// <summary>
        /// Check checksum of language block
        /// </summary>
        /// <param name="Language">Language identifier</param>
        /// <param name="Checksum">Checksum</param>
        /// <returns>True if checksum matches</returns>
        public abstract bool CheckLanguageChecksum(Block.BlockId Language, UInt16 Checksum);

        /// <summary>
        /// Get Name of value by position in communication table
        /// </summary>
        /// <param name="ValuePosition">Position in table</param>
        /// <returns>Name of value (empty string, if not found)</returns>
        public string GetValueName(byte ValuePosition)
        {
            if (mLanguage != null)
            {
                return mLanguage.GetValueName(ValuePosition);
            }
            else
            {
                throw new NullReferenceException("Language block not initialized");
            }
        }

        /// <summary>
        /// Get string of value by position in communication table
        /// </summary>
        /// <param name="ValuePosition">Position in table</param>
        /// <param name="uiValue">Value as unsigned 16 bit number</param>
        /// <returns>String of value (empty string, if not found)</returns>
        public string GetValueString(byte ValuePosition, UInt16 uiValue)
        {
            if (mLanguage != null)
            {
                return mLanguage.GetValue((UInt16)ValuePosition, uiValue);
            }
            else
            {
                throw new NullReferenceException("Language block not initialized");
            }
        }

        /// <summary>
        /// Get unit name of value by position in communication table
        /// </summary>
        /// <param name="ValuePosition">Position in table</param>
        /// <returns>Unit name of value (empty string, if not found)</returns>
        public string GetValueUnit(byte ValuePosition)
        {
            if (mLanguage != null)
            {
                return mLanguage.GetValueUnit(ValuePosition);
            }
            else
            {
                throw new NullReferenceException("Language block not initialized");
            }
        }

        /// <summary>
        /// Get alternative unit name of value by position in communication table
        /// </summary>
        /// <param name="ValuePosition">Position in table</param>
        /// <returns>Alternative unit name of value (empty string, if not found)</returns>
        public string GetValueAltUnit(byte ValuePosition)
        {
            if (mLanguage != null)
            {
                return mLanguage.GetValueUnit(ValuePosition);
            }
            else
            {
                throw new NullReferenceException("Language block not initialized");
            }
        }

        /// <summary>
        /// Set altenative unit flag
        /// </summary>
        /// <param name="NoAltUnit">True to use original unit, false for alternative unit (if available)</param>
        public void SetValueAltUnitFlag(bool NoAltUnit)
        {
            mLanguage.NoAltUnit = NoAltUnit;
        }

        /// <summary>
        /// Get hidden flag of value by position in communication table
        /// </summary>
        /// <param name="ValuePosition">Position in table</param>
        /// <returns>True if value is hidden</returns>
        public bool GetValueHiddenFlag(byte ValuePosition)
        {
            if (mLanguage != null)
            {
                return mLanguage.IsValueHidden(ValuePosition);
            }
            else
            {
                throw new NullReferenceException("Language block not initialized");
            }
        }

        /// <summary>
        /// Get display flag of value by position in communication table
        /// </summary>
        /// <param name="ValuePosition">Position in table</param>
        /// <returns>True if value is displayed</returns>
        public bool GetValueDisplayFlag(byte ValuePosition)
        {
            if (mLanguage != null)
            {
                return mLanguage.IsValueDisplayed(ValuePosition);
            }
            else
            {
                throw new NullReferenceException("Language block not initialized");
            }
        }

        /// <summary>
        /// Get block identifier of matching language identifier
        /// </summary>
        /// <param name="lId">Language identifier</param>
        /// <returns>Blockidentifier matching to language identifier</returns>
        public static Block.BlockId GetLanguageBlockId(LanguageId lId)
        {
            Block.BlockId ret = Block.BlockId.IdLngEN;
            switch (lId)
            {
                case LanguageId.German:
                    ret = Block.BlockId.IdLngDE;
                    break;
                case LanguageId.English:
                    ret = Block.BlockId.IdLngEN;
                    break;
                case LanguageId.French:
                    ret = Block.BlockId.IdLngFR;
                    break;
                case LanguageId.Italian:
                    ret = Block.BlockId.IdLngIT;
                    break;
                case LanguageId.Spanish:
                    ret = Block.BlockId.IdLngES;
                    break;
                case LanguageId.Polish:
                    ret = Block.BlockId.IdLngPO;
                    break;
                case LanguageId.Dutch:
                    ret = Block.BlockId.IdLngNL;
                    break;
            }
            return ret;
        }

        #endregion

        #region Acquisition

        /// <summary>
        /// Read one flash block (32kByte) of acquisition raw data
        /// </summary>
        /// <param name="Position">Position of acquisition data in ring</param>
        /// <param name="Data">Target byte array for flash data</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue ReadAcquisitionData(UInt16 Position, out byte[] Data);

        #endregion

        #region Actual values

        /// <summary>
        /// Read actual values data
        /// </summary>
        /// <param name="Data">Target byte array of actual value data</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue ReadActualValues(out byte[] Data)
        {
            if (mBusy)
            {
                Data = new byte[0];
                return ReturnValue.ThreadingBusy;
            }
            else
            {
                //Data = new byte[0]; // s.u.ReturnValue.FileEmpty;//
                return mEcu.Read(Comm.OrderByte.ReadActualValues, out Data);
            }
        }

        #endregion

        #region Error stack

        /// <summary>
        ///  Read actual error stack data
        /// </summary>
        /// <param name="Data">Target byte array of error stack data</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue ReadErrorStack(out byte[] Data)
        {
            ReturnValue ret = ReturnValue.NoError;
            if (mBusy)
            {
                Data = new byte[0];
                ret = ReturnValue.ThreadingBusy;
            }
            else
            {
                ret = mEcu.Read(Comm.OrderByte.ReadErrorStack, out Data);
                if (ret == ReturnValue.Retry)
                {
                    ret = mEcu.Read(Comm.OrderByte.ReadErrorStack, out Data);
                }
            }
            return ret;
        }

        /// <summary>
        /// Get name of error by position
        /// </summary>
        /// <param name="Position">Position of error (same as ID)</param>
        /// <returns>String of error name</returns>
        public string GetErrorName(ushort Position)
        {
            if (Position > 63 || mLanguage == null)
            {
                return "N/A";
            }
            else
            {
                return mLanguage.GetErrorName(Position);
            }
        }

        /// <summary>
        /// Get flag if error is only an event
        /// </summary>
        /// <param name="Position">Position in error table</param>
        /// <returns>True if event only</returns>
        public bool IsEventOrError(ushort Position)
        {
            return mLanguage.IsEventOrError(Position);
        }

        /// <summary>
        /// Reset actual error stack
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue ResetErrorStack()
        {
            if (mBusy)
            {
                return ReturnValue.ThreadingBusy;
            }
            else
            {
                return mEcu.Order(Comm.OrderByte.ResetErrors);
            }
        }

        #endregion

        /// <summary>
        /// Get name of behave
        /// </summary>
        /// <param name="Position">Position of behave</param>
        /// <returns>String of behave name</returns>
        public string GetBehaveName(byte Position)
        {
            if (Position < 16)
            {
                return mLanguage.GetBehaveName(Position);
            }
            else
            {
                return "N/A";
            }
        }

        #region Error ring

        /// <summary>
        /// Read error history ring data unit
        /// </summary>
        /// <param name="Position">Position of data unit in ring</param>
        /// <param name="Data">Target byte array of error ring data</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue ReadErrorRing(byte Position, out byte[] Data);

        #endregion

        #region Volatile values
        /// <summary>
        /// Read rtc data
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue ReadRtc();

        /// <summary>
        /// Get number of volatiles values
        /// </summary>
        /// <returns>Number of volatiles values</returns>
        public abstract UInt16 GetNumberOfVolatiles();

        /// <summary>
        /// Get volatile value as string
        /// </summary>
        /// <param name="Position">Position of volatile value</param>
        /// <returns>Volatile value as string</returns>
        public abstract string GetVolatileValue(UInt16 Position);
        #endregion

        #region Empirical values
        /// <summary>
        /// Read empirical data
        /// </summary>
        /// <returns>0 on sucess, else see ReturnValue</returns>
        public abstract ReturnValue ReadEmpiricals();

        /// <summary>
        /// Get empirical group names
        /// </summary>
        /// <param name="GroupNames">Position of group</param>
        /// <returns>True on success</returns>
        public abstract bool GetEmpiricalGroupNames(out string[] GroupNames);

        /// <summary>
        /// Get number of empirical values
        /// </summary>
        /// <param name="Group">Enumeration of group</param>
        /// <returns>Number of values</returns>
        public abstract UInt16 GetNumberOfEmpiricalValues(EmpiricalDataBlock.GroupName Group);

        /// <summary>
        /// Get empirical value string
        /// </summary>
        /// <param name="GroupPosition">Position of group</param>
        /// <param name="ValuePosition">Position of value</param>
        /// <returns>String with name and value</returns>
        public abstract string GetEmpiricalValue(UInt16 GroupPosition, UInt16 ValuePosition);
        #endregion

        /// <summary>
        /// Read DTC data
        /// </summary>
        /// <param name="target">Output byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue ReadDtc(out byte[] target);

        /// <summary>
        /// Read DTC assignement table
        /// </summary>
        /// <param name="target">Output byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue ReadDtcTable(out byte[] target);

        /// <summary>
        /// Send order to ecu
        /// </summary>
        /// <param name="Order">Enumeration of order</param>
        /// <param name="Value">Value of order</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue SendOrder(Comm.OrderByte Order, UInt16 Value);
    }
}
