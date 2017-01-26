/*
 * Object: HJS.ECU.Diag.Diagnostics
 * Description: Diagnostic interface to HJS-ECU
 * 
 * $LastChangedDate: 2013-10-25 14:40:14 +0200 (Fr, 25 Okt 2013) $
 * $LastChangedRevision: 25 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/trunk/lib_cs_win32_hjsecu/Diagnostics.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Xml;
using HJS.ECU.Protocol;

namespace HJS.ECU.Diag
{
    /// <summary>
    /// Diagnostics class
    /// </summary>
    public class Diagnostics
    {
        #region Private members
        /// <summary>
        /// Interface of ECU communication protocol
        /// </summary>
        private ProtocolBase mProtocol;

        /// <summary>
        /// String of current or last used serial port
        /// </summary>
        private string mPortname;

        /// <summary>
        /// Byte array of ECU keyword
        /// </summary>
        private byte[] mKey;

        /// <summary>
        /// Language identifier
        /// </summary>
        private ProtocolBase.LanguageId mLanguage;

        /// <summary>
        /// Acquisition block object
        /// </summary>
        private readonly Acquisition mAcquisition;

        /// <summary>
        /// Error stack object
        /// </summary>
        private readonly ErrorStack mErrorStack;

        /// <summary>
        /// Actual values object
        /// </summary>
        private readonly ActualValues mActualValues;

        /// <summary>
        /// Error ring object
        /// </summary>
        private readonly ErrorRing mErrorRing;

        /// <summary>
        /// Diagnostic trouble codes
        /// </summary>
        private readonly TroubleCodes mDtcStack;

        /// <summary>
        /// Local copy of configuration block; used only for verifying
        /// </summary>
        private Block mConfig;

        /// <summary>
        /// Local copy of datamap block; used only for verifying
        /// </summary>
        private Block mDatamap;

        /// <summary>
        /// Local copy of first language block; used only for verifying
        /// </summary>
        private Block mLanguage1;

        /// <summary>
        /// Local copy of second language block; used only for verifying
        /// </summary>
        private Block mLanguage2;

        /// <summary>
        /// Local copy of third language block; used only for verifying
        /// </summary>
        private Block mLanguage3;

        /// <summary>
        /// Local copy of fourth language block; used only for verifying
        /// </summary>
        private Block mLanguage4;

        /// <summary>
        /// Last occured error identifier
        /// </summary>
        private ReturnValue mLastReturnValue;
        #endregion

        #region Accessors
        /// <summary>
        /// Accessors to current or last used serial port string
        /// </summary>
        public string PortName
        {
            get { return mPortname; }
            set { mPortname = value; }
        }

        /// <summary>
        /// Accessors to current language identifier
        /// </summary>
        public ProtocolBase.LanguageId Language
        {
            get { return mLanguage; }
            set { mLanguage = value; }
        }

        /// <summary>
        /// Accessors for hardware version as string
        /// Read only
        /// </summary>
        public string HardwareVersion
        {
            get
            {
                return String.Format("{0}.{1}{2}",
                    mProtocol.HardwareVersion.Hauptversion,
                    mProtocol.HardwareVersion.Nebenversion,
                    (Char)(mProtocol.HardwareVersion.Revision + 0x61));
            }
        }

        /// <summary>
        /// Accessors for software version as string
        /// Read only
        /// </summary>
        public string SoftwareVersion
        {
            get
            {
                return String.Format("{0}.{1}.{2}",
                    mProtocol.SoftwareVersion.Hauptversion,
                    mProtocol.SoftwareVersion.Nebenversion,
                    mProtocol.SoftwareVersion.Revision);
            }
        }

        /// <summary>
        /// Accessors for serial number as string
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return mProtocol.SerialNumber.ToString();
            }
        }

        /// <summary>
        /// Accessors for production date
        /// Read only
        /// </summary>
        public DateTime ProductionDate
        {
            get
            {
                return mProtocol.DateOfProgramming;
            }
        }

        /// <summary>
        /// Accessors for configuration version as string
        /// </summary>
        public string ConfigVersion
        {
            get
            {
                return String.Format("{0}.{1}.{2}",
                    mProtocol.ConfigurationVersion.Hauptversion,
                    mProtocol.ConfigurationVersion.Nebenversion,
                    mProtocol.ConfigurationVersion.Revision);
            }
        }

        /// <summary>Accessors for data map version as string</summary>
        public string DatamapVersion
        {
            get
            {
                return String.Format("{0}.{1}.{2}",
                    mProtocol.DatamapVersion.Hauptversion,
                    mProtocol.DatamapVersion.Nebenversion,
                    mProtocol.DatamapVersion.Revision);
            }
        }

        /// <summary>
        /// Accessors to last occured error or return value
        /// </summary>
        public ReturnValue LastReturnValue
        {
            get
            {
                return mLastReturnValue;
            }
        }

        /// <summary>
        /// Accessors to current protocol version
        /// </summary>
        public byte ProtocolVersion
        {
            get
            {
                return mProtocol.ProtocolVersion;
            }
        }

        /// <summary>
        /// Description of internal values of info block
        /// </summary>
        public string InternalDescr
        {
            get
            {
                return String.Format("Akquisitions Bloecke:{0} Fehlerring Sektoren:{1} Lernwertestruktur Version:{2}",
                    mProtocol.AcquisitionSectors,
                    mProtocol.ErrorRingSectors,
                    mProtocol.EmpiricalVersion);
            }
        }
        #endregion

        /// <summary>
        /// Enumeration of diagnostic orders
        /// </summary>
        public enum Orders
        {
            /// <summary>
            /// Additive reset
            /// </summary>
            additiveReset,
            /// <summary>
            /// Filter changed
            /// </summary>
            filterChanged,
            /// <summary>
            /// Dosing pulses
            /// </summary>
            dosingPulses,
            /// <summary>
            /// Regeneration start / stop
            /// </summary>
            regenerationStart,
            /// <summary>
            /// Set pre alarm pressure
            /// </summary>
            preAlarmPressure,
            /// <summary>
            /// Set pre alarm time
            /// </summary>
            preAlarmTime,
            /// <summary>
            /// Set main alarm pressure
            /// </summary>
            mainAlarmPressure,
            /// <summary>
            /// Set main alarm time
            /// </summary>
            mainAlarmTime,
            /// <summary>
            /// Calibrate differential pressure sensor
            /// </summary>
            calibrateDiffPSensor,
            /// <summary>
            /// Calibrate ambient pressuer sensor
            /// </summary>
            calibrateAmbPSensor,
            /// <summary>
            /// Set turns factor
            /// </summary>
            turnsFactor,
            /// <summary>
            /// Set fuel sensor signal
            /// </summary>
            fuelSensorSignal,
            /// <summary>
            /// Set fuel sensor gain
            /// </summary>
            fuelSensorGain,
            /// <summary>
            /// Toggle EGR
            /// </summary>
            toggleEgr
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="PortName">Name of port</param>
        /// <param name="Key">Byte array of ECU key. (If not array of 8 bytes, an exception is thrown!)</param>
        /// <param name="LanguageIdentifier">Identifier of language</param>
        public Diagnostics(string PortName, byte[] Key, ProtocolBase.LanguageId LanguageIdentifier)
        {
            mProtocol = new Protocol15(PortName, Key, HJS.ECU.Port.Comm.PortType.Direct);    // Default protocol version
            mPortname = PortName;
            if (Key.Length != 8)
            {
                throw new ArgumentException("Diagnostic Keylength invalid");
            }
            mKey = new byte[8];
            mKey[0] = Key[0];
            mKey[1] = Key[1];
            mKey[2] = Key[2];
            mKey[3] = Key[3];
            mKey[4] = Key[4];
            mKey[5] = Key[5];
            mKey[6] = Key[6];
            mKey[7] = Key[7];
            mLanguage = LanguageIdentifier;
            mAcquisition = new Acquisition();
            mErrorStack = new ErrorStack(ref mProtocol);
            mActualValues = new ActualValues(ref mProtocol);
            mErrorRing = new ErrorRing();
            mDtcStack = new TroubleCodes();
        }

        /// <summary>
        /// Get string of last error (return value)
        /// </summary>
        /// <returns>String in english naming the last error</returns>
        public string GetLastReturnValue()
        {
            return mLastReturnValue.ToString();
        }

        /// <summary>
        /// Get date time object of last communication time stamp
        /// </summary>
        /// <returns>Date time object of last communication time stamp</returns>
        public DateTime GetLastTimeStamp()
        {
            return mProtocol.GetLastTimeStamp();
        }

        /// <summary>
        /// Get compatibility number of ecu software
        /// </summary>
        /// <returns>Compatibility number of ecu software</returns>
        public byte GetCompatibility()
        {
            return mProtocol.SoftwareVersion.Revision;
        }

        /// <summary>
        /// Set ecu time
        /// </summary>
        /// <param name="NewTime">Date time object of new ecu time</param>
        /// <returns>True in success</returns>
        public bool SetTime(DateTime NewTime)
        {
            ReturnValue ret = mProtocol.SetTime(NewTime);
            if (ret == ReturnValue.NoError)
            {
                return true;
            }
            else
            {
                mLastReturnValue = ret;
                return false;
            }
        }

        /// <summary>
        /// Set Production data (SN, date, temperature)
        /// </summary>
        /// <param name="serialNumber">New serialnumber</param>
        /// <param name="temperature">New temperature</param>
        /// <returns>True in success</returns>
        public bool SetProductionData(UInt32 serialNumber, Int16 temperature)
        {
            HJS.ECU.ProductionDataBlock pb = new ProductionDataBlock();
            pb.SetData(serialNumber, temperature);
            pb.GenerateChecksum();
            byte[] pb_buff;
            ReturnValue ret = pb.WriteRaw(out pb_buff);
            if (ret != ReturnValue.NoError)
            {
                mLastReturnValue = ret;
                return false;
            }
            ret = mProtocol.WriteProductionData(ref pb_buff);
            if (ret != ReturnValue.NoError)
            {
                mLastReturnValue = ret;
                return false;
            }
            else
            {
                ret = mProtocol.Reboot(ProtocolBase.RebootMode.RebootNormal);
                return true;
            }
        }

        /// <summary>
        /// Change protocol server identifier
        /// </summary>
        /// <param name="ServerByte">New server identifier</param>
        public void ChangeServerIdentifier(HJS.ECU.Port.Comm.ServerByte ServerByte)
        {
            if (mProtocol != null)
            {
                mProtocol.ChangeServer(ServerByte);
            }
        }

        /// <summary>
        /// Start connection to ECU
        /// </summary>
        /// <param name="ConnectionType">Port connection type</param>
        /// <param name="LocalTime">Flag if time displayed local (or UTC)</param>
        /// <returns>True if connected successfully</returns>
        public bool Connect(HJS.ECU.Port.Comm.PortType ConnectionType, bool LocalTime)
        {
            if (mProtocol == null)
                mProtocol = new Protocol15(mPortname, mKey, ConnectionType);
            mProtocol.ChangePort(mPortname);
            ReturnValue ret = mProtocol.Connect(mLanguage);

            if (ret == ReturnValue.VersionMismatch)
            {
                mProtocol.Disconnect();
                mProtocol = new Protocol14(mPortname, mKey, ConnectionType);
                ret = mProtocol.Connect(mLanguage);
            }
            mProtocol.LocalTime = LocalTime;
            mActualValues.SetNewProtocol(ref mProtocol);
            mDtcStack.ReadTroubleCodeTable(ref mProtocol);
            mErrorStack.SetNewProtocol(ref mProtocol);
            if (ret == ReturnValue.NoError)
            {
                mActualValues.RefreshLanguage(false);
            }
            if (ret == ReturnValue.LanguageNotValid)
            {
                ret = ReturnValue.NoError;  // ignore missing languages!
            }
            mLastReturnValue = ret;
            return (ret == ReturnValue.NoError);
        }

        /// <summary>
        /// Start connection to ECU without reading language!
        /// </summary>
        /// <returns>True if connected successfully</returns>
        public bool FastConnect(HJS.ECU.Port.Comm.PortType ConnectionType)
        {
            if (mProtocol == null)
                mProtocol = new Protocol15(mPortname, mKey, ConnectionType);
            mProtocol.ChangePort(mPortname);
            ReturnValue ret = mProtocol.FastConnect();

            if (ret == ReturnValue.VersionMismatch)
            {
                mProtocol.Disconnect();
                mProtocol = new Protocol14(mPortname, mKey, ConnectionType);
                ret = mProtocol.FastConnect();
            }
            mLastReturnValue = ret;
            mActualValues.SetNewProtocol(ref mProtocol);
            mDtcStack.ReadTroubleCodeTable(ref mProtocol);
            mErrorStack.SetNewProtocol(ref mProtocol);
            return (ret == ReturnValue.NoError);
        }

        /// <summary>
        /// Disconnect ECU
        /// </summary>
        public void Disconnect()
        {
            mProtocol.Disconnect();
            mLastReturnValue = ReturnValue.NoError;
            // Delete content
            mAcquisition.DeleteItems();
            mErrorRing.DeleteItems();
        }

        /// <summary>
        /// Get string of all versions of connected ECU
        /// </summary>
        /// <returns>String, including Version of hardware, software, configuration and serial number</returns>
        public string EcuVersions()
        {
            string HardwareRevision;
            if (mProtocol.HardwareVersion.Revision <= 26)
            {
                HardwareRevision = String.Format("{0}", (Char)(mProtocol.HardwareVersion.Revision + 0x61));
            }
            else
            {
                // warning: revision does not fit into letter!
                HardwareRevision = mProtocol.HardwareVersion.Revision.ToString();
            }
            mLastReturnValue = ReturnValue.NoError;
            return String.Format("HW: {0}.{1}{2} SW: {3}.{4}.{5} CFG: {6}.{7}.{8} (KF: {9}.{10}.{11}) SN: {12}",
                mProtocol.HardwareVersion.Hauptversion,
                mProtocol.HardwareVersion.Nebenversion,
                HardwareRevision,       //ProtocolBase.HardwareVersion.Revision
                mProtocol.SoftwareVersion.Hauptversion,
                mProtocol.SoftwareVersion.Nebenversion,
                mProtocol.SoftwareVersion.Revision,
                mProtocol.ConfigurationVersion.Hauptversion,
                mProtocol.ConfigurationVersion.Nebenversion,
                mProtocol.ConfigurationVersion.Revision,
                mProtocol.DatamapVersion.Hauptversion,
                mProtocol.DatamapVersion.Nebenversion,
                mProtocol.DatamapVersion.Revision,
                mProtocol.SerialNumber);
        }

        /// <summary>
        /// Get info text fom ECU
        /// </summary>
        /// <returns>String of info text</returns>
        public string GetInfoText()
        {
            string ret;
            ReturnValue rv = mProtocol.ReadInfoText(out ret);
            mLastReturnValue = rv;
            return ret;
        }

        /// <summary>
        /// Reset all empirical values and erase all flash ring memory
        /// </summary>
        /// <returns>True on success</returns>
        public bool MasterReset()
        {
            Console.WriteLine("Masterreset..");
            ReturnValue ret = mProtocol.MasterReset();
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>
        /// Reboot into normal mode
        /// </summary>
        /// <param name="BootMode">Parameter for rebooting</param>
        /// <returns>True on success</returns>
        public bool Reboot(ProtocolBase.RebootMode BootMode)
        {
            ReturnValue ret = mProtocol.Reboot(BootMode);
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>
        /// Send order to ecu
        /// </summary>
        /// <param name="OrderId">Order enumerator</param>
        /// <param name="OrderValue">Value of order</param>
        /// <returns>Treu on success</returns>
        public bool Order(Orders OrderId, UInt16 OrderValue)
        {
            ReturnValue ret = ReturnValue.ComOrderFailed;
            switch (OrderId)
            {
                case Orders.additiveReset:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetAdditiveRefilled, 0);
                    break;
                case Orders.filterChanged:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetFilterChanged, 0);
                    break;
                case Orders.dosingPulses:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetDosingPulses, OrderValue);
                    break;
                case Orders.regenerationStart:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.StartRegeneration, 0);
                    break;
                case Orders.preAlarmPressure:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetWarningPressure, OrderValue);
                    break;
                case Orders.preAlarmTime:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetWarningTimeDelay, OrderValue);
                    break;
                case Orders.mainAlarmPressure:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetAlarmPressure, OrderValue);
                    break;
                case Orders.mainAlarmTime:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetAlarmTimeDelay, OrderValue);
                    break;
                case Orders.calibrateDiffPSensor:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.StartPressureSensorCalibration, 0);
                    break;
                case Orders.calibrateAmbPSensor:
                    //
                    break;
                case Orders.turnsFactor:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetTurnScale, OrderValue);
                    break;
                case Orders.fuelSensorSignal:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.ProdToggleTank, OrderValue);
                    break;
                case Orders.fuelSensorGain:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetFuelSensorAmplifier, OrderValue);
                    break;
                case Orders.toggleEgr:
                    ret = mProtocol.SendOrder(HJS.ECU.Port.Comm.OrderByte.SetEGRToggle, OrderValue);
                    break;
            }
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>
        /// Send direct order to ECU
        /// </summary>
        /// <param name="OrderByte">Order byte</param>
        /// <param name="Parameter">Parameter</param>
        /// <returns>True on success</returns>
        public bool DirectOrder(Byte OrderByte, UInt16 Parameter)
        {
            ReturnValue ret = ReturnValue.ComOrderFailed;
            ret = mProtocol.SendOrder((HJS.ECU.Port.Comm.OrderByte)OrderByte, Parameter);
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filename"></param>
        public void SaveBlocks(string Filename)
        {
            BlockFile bf = new BlockFile();
            bf.Create(BlockFile.FileIdentifier.ReadData, Filename, true);
            bf.PutBlock(mProtocol.mInfo);
            bf.PutBlock(mProtocol.mLanguage);
            //bf.PutBlock(
            bf.Close();
        }

        #region Interface to parameter set
        /// <summary>
        /// Check if current software version is updateable
        /// If PSoC version 8 is on ECU, the ECU can be updated
        /// </summary>
        /// <param name="Version">Version string to be updated to</param>
        /// <returns>True if software is updatebale</returns>
        public bool IsUpdateableTo(string Version)
        {
            return mProtocol.IsUpdateableTo(Version);
        }

        /// <summary>
        /// Start multithreaded uploading of parameterset with up to 8 languages
        /// </summary>
        /// <param name="Config">Reference to configuration block</param>
        /// <param name="Datamap">Reference to data map block</param>
        /// <param name="Language1">Reference to first language block</param>
        /// <param name="Language2">Reference to second language block</param>
        /// <param name="Language3">Reference to third language block</param>
        /// <param name="Language4">Reference to fourth language block</param>
        /// <param name="Language5">Reference to fifth language block</param>
        /// <param name="Language6">Reference to sixth language block</param>
        /// <param name="Language7">Reference to seventh language block</param>
        /// <param name="Language8">Reference to eigth language block</param>
        public ReturnValue StartUpdateParameterset(ref Block Config, ref Block Datamap,
            ref Block Language1, ref Block Language2, ref Block Language3, ref Block Language4,
            ref Block Language5, ref Block Language6, ref Block Language7, ref Block Language8)
        {
            mConfig = Config;
            mDatamap = Datamap;
            mLanguage1 = Language1;
            mLanguage2 = Language2;
            mLanguage3 = Language3;
            mLanguage4 = Language4;
            Block _Language5 = Language5;
            Block _Language6 = Language6;
            Block _Language7 = Language7;
            Block _Language8 = Language8;
            byte IstAnzahlBloecke = 0;
            byte SollAnzahlBloecke = 4 + 2;
            if (_Language5 != null)
            {
                if (_Language5.DataSize != 0)
                    SollAnzahlBloecke++;
            }
            if (_Language6 != null)
            {
                if (_Language6.DataSize != 0)
                    SollAnzahlBloecke++;
            }
            if (_Language7 != null)
            {
                if (_Language7.DataSize != 0)
                    SollAnzahlBloecke++;
            }
            if (_Language8 != null)
            {
                if (_Language8.DataSize != 0)
                    SollAnzahlBloecke++;
            }

            // vorher protokoll pruefen, damit bei 14 keine exception kommt!
            if (mProtocol.ProtocolVersion == 14)
            {
                return ReturnValue.VersionMismatch;
            }
            Console.WriteLine("Rebooting..");
            ReturnValue ret = mProtocol.Reboot(ProtocolBase.RebootMode.RebootNotConfig);
            if (ret == ReturnValue.NoError)
            {
                Console.WriteLine("Start Erasing..");
                ret = mProtocol.EraseParamBlocks();
                if (ret == ReturnValue.NoError)
                {
                    IstAnzahlBloecke++;
                    Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                    ret = mProtocol.WriteParamBlock(ref mConfig);
                    if (ret != ReturnValue.NoError)
                    {
                        mLastReturnValue = ret;
                        return ret;
                    }
                    IstAnzahlBloecke++;
                    Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                    ret = mProtocol.WriteParamBlock(ref mDatamap);
                    if (ret != ReturnValue.NoError)
                    {
                        mLastReturnValue = ret;
                        return ret;
                    }
                    IstAnzahlBloecke++;
                    Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                    ret = mProtocol.WriteParamBlock(ref mLanguage1);
                    if (ret != ReturnValue.NoError)
                    {
                        mLastReturnValue = ret;
                        return ret;
                    }
                    IstAnzahlBloecke++;
                    Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                    ret = mProtocol.WriteParamBlock(ref mLanguage2);
                    if (ret != ReturnValue.NoError)
                    {
                        mLastReturnValue = ret;
                        return ret;
                    }
                    IstAnzahlBloecke++;
                    Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                    ret = mProtocol.WriteParamBlock(ref mLanguage3);
                    if (ret != ReturnValue.NoError)
                    {
                        mLastReturnValue = ret;
                        return ret;
                    }
                    IstAnzahlBloecke++;
                    Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                    ret = mProtocol.WriteParamBlock(ref mLanguage4);
                    if (ret != ReturnValue.NoError)
                    {
                        mLastReturnValue = ret;
                        return ret;
                    }
                    if (_Language5 != null){
                        if (_Language5.DataSize != 0)
                        {
                            IstAnzahlBloecke++;
                            Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                            ret = mProtocol.WriteParamBlock(ref _Language5);
                            if (ret != ReturnValue.NoError)
                            {
                                mLastReturnValue = ret;
                                return ret;
                            }
                        }
                    }
                    if (_Language6 != null){
                        if (_Language6.DataSize != 0)
                        {
                            IstAnzahlBloecke++;
                            Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                            ret = mProtocol.WriteParamBlock(ref _Language6);
                            if (ret != ReturnValue.NoError)
                            {
                                mLastReturnValue = ret;
                                return ret;
                            }
                        }
                    }
                    if (_Language7 != null){
                        if (_Language7.DataSize != 0)
                        {
                            IstAnzahlBloecke++;
                            Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                            ret = mProtocol.WriteParamBlock(ref _Language7);
                            if (ret != ReturnValue.NoError)
                            {
                                mLastReturnValue = ret;
                                return ret;
                            }
                        }
                    }
                    if (_Language8 != null){
                        if (_Language8.DataSize != 0)
                        {
                            IstAnzahlBloecke++;
                            Console.WriteLine(String.Format("Start Downloading {0}/{1}", IstAnzahlBloecke, SollAnzahlBloecke));
                            ret = mProtocol.WriteParamBlock(ref _Language8);
                            if (ret != ReturnValue.NoError)
                            {
                                mLastReturnValue = ret;
                                return ret;
                            }
                        }
                    }
                    Console.WriteLine("Rebooting..");
                    ret = mProtocol.Reboot(ProtocolBase.RebootMode.RebootNormal);
                    if (ret == ReturnValue.NoError)
                    {
                        //reconnect
                        mProtocol.Disconnect();
                        ret = mProtocol.Connect(mLanguage);
                    }
                }
            }
            mLastReturnValue = ret;
            return ret;
        }

        /// <summary>
        /// Read language and check checksum
        /// </summary>
        /// <param name="Language">Language identifier</param>
        /// <param name="Checksum">Reference checksum</param>
        /// <returns>True is language is loaded and checksums matching</returns>
        public bool CheckLanguage(Block.BlockId Language, UInt16 Checksum)
        {
            return mProtocol.CheckLanguageChecksum(Language, Checksum);
        }

        /// <summary>Read parameter block from ecu</summary>
        /// <param name="Identifier">Identifier of block to read</param>
        /// <returns>Parameter block, or null on error</returns>
        public Block GetBlock(Block.BlockId Identifier)
        {
            Block ret = new Block();
            if (mProtocol.ReadParamBlock(Identifier, ref ret) != ReturnValue.NoError)
            {
                return null;
            }
            else
            {
                return ret;
            }
        }
        #endregion

        #region Interface to actual values

        /// <summary>
        /// Get number of actual values
        /// </summary>
        /// <returns>Number of actual value items</returns>
        public byte GetActualValueNumber()
        {
            return mProtocol.GetValueNumber();
        }

        /// <summary>
        /// Get name of actual value by position
        /// </summary>
        /// <param name="Position">Position of actual value</param>
        /// <returns>String of value name</returns>
        public string GetActualValueName(byte Position)
        {
            if (mActualValues.Item[Position] == null)
            {
                return String.Format("Value_{0}", Position);
            }
            else
            {
                if (mActualValues.Item[Position].Name == null)
                {
                    return String.Format("Value_{0}", Position);
                }
                else
                {
                    return mActualValues.Item[Position].Name;
                }
            }
        }

        /// <summary>
        /// Get value string of actual value by position
        /// </summary>
        /// <param name="Position">Position of actual value</param>
        /// <returns>Value string</returns>
        public string GetActualValueString(byte Position)
        {
            if (mActualValues.Item[Position] == null)
            {
                return "N/A";
            }
            else
            {
                if (mActualValues.Item[Position].ValueString == null)
                {
                    return "N/A";
                }
                else
                {
                    return mActualValues.Item[Position].ValueString;
                }
            }
        }

        /// <summary>
        /// Get unit name of actual value by position
        /// </summary>
        /// <param name="Position">Position of actual value</param>
        /// <returns>String of unit of actual value</returns>
        public string GetActualValueUnit(byte Position)
        {
            if (mActualValues.Item[Position] == null)
            {
                return String.Format("Unit_{0}", Position);
            }
            else
            {
                return mActualValues.Item[Position].Unit;
            }
        }

        /// <summary>
        /// Get hidden flag of actual value by position
        /// </summary>
        /// <param name="Position">Position of actual value</param>
        /// <returns>True if actual value is hidden</returns>
        public bool GetActualValueHidden(byte Position)
        {
            if (mActualValues.Item[Position] == null)
            {
                return false;
            }
            else
            {
                return mActualValues.Item[Position].Hidden;
            }
        }

        /// <summary>
        /// Get display flag of actual value by position
        /// </summary>
        /// <param name="Position">Position of actual value</param>
        /// <returns>True if actual value is displayed</returns>
        public bool GetActualValueDisplayed(byte Position)
        {
            if (mActualValues.Item[Position] == null)
            {
                return false;
            }
            else
            {
                return mActualValues.Item[Position].Display;
            }
        }

        /// <summary>
        /// Read actual values from ECU
        /// </summary>
        /// <returns>True on success</returns>
        public bool ReadActualValues()
        {
            mLastReturnValue = mActualValues.RefreshValues();
            if (mLastReturnValue != ReturnValue.NoError)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Export actual values to a XML file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        public void ActualValuesXmlExport(string Filename)
        {
            mActualValues.XmlExport(Filename);
        }

        /// <summary>
        /// Export actual values to a CSVL file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        public void ActualValuesCsvExport(string Filename)
        {
            mActualValues.CsvExport(Filename);
        }

        /// <summary>
        /// Toggle Alternative unit and original unit
        /// </summary>
        /// <param name="NoAltUnit">True to use original unit, false for alternative unit (if available)</param>
        public void ActualValuesToggleUnit(bool NoAltUnit)
        {
            mActualValues.RefreshLanguage(NoAltUnit);
        }
        #endregion

        #region Interface to acquisition

        /// <summary>
        /// Routine for multi threaded acquisition reading to raw file
        /// </summary>
        //private void bgWork_DoWork()
        public void AcquiStep(UInt16 Position)
        {
            //for (byte i = 0; i < mAcquisition.NumberOfBlocks; i++)
            //{
                ReturnValue ret = mProtocol.ReadAcquisitionData(Position, out mAcquisition.ReadBuffer);
                if (ret == ReturnValue.NoError)
                {
                    mAcquisition.ImportSector(ref mAcquisition.ReadBuffer, ref mProtocol);
                }
                mLastReturnValue = ret;
                //else
                //{
                //    return;
                //}
            //}
            //mAcquisition.XmlExport("Acqui.xml", ref mProtocol);
        }

        /// <summary>
        /// Get number of error ring items
        /// </summary>
        /// <returns>Number of error ring items</returns>
        public int AcquiItems()
        {
            return mAcquisition.NumberOfItems();
        }

        /// <summary>
        /// Delete error ring items
        /// </summary>
        public void AcquiDeleteItems()
        {
            mAcquisition.DeleteItems();
        }

        /// <summary>
        /// Multi threaded read out of acquisition data to raw file
        /// </summary>
        public byte Acqui()
        {
            mAcquisition.SetConfiguration(ref mProtocol);
            return mAcquisition.NumberOfBlocks;
        }

        /// <summary>
        /// Export acquisition data to a XML file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        public void AcquiXmlExport(string Filename)
        {
            mAcquisition.XmlExport(Filename, ref mProtocol);
        }

        /// <summary>
        /// Export acquisition data to a CSV file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        public void AcquiCsvExport(string Filename)
        {
            mAcquisition.CsvExport(Filename, ref mProtocol);
        }

        #endregion

        #region Interface to error stack and behaves
        /// <summary>
        /// Get recent error stack data from ecu
        /// </summary>
        public void ErrStack()
        {
            mErrorStack.ReadFromEcu();
        }

        /// <summary>
        /// Get name of error by position
        /// </summary>
        /// <param name="Position">Position of error (same as ID)</param>
        /// <returns>String of error name</returns>
        public string GetErrorName(ushort Position)
        {
            return mProtocol.GetErrorName(Position);
        }

        /// <summary>
        /// Get flag if error is only an event
        /// </summary>
        /// <param name="Position">Position in error table</param>
        /// <returns>True if event only</returns>
        public bool IsErrorOrEvent(ushort Position)
        {
            return mProtocol.IsEventOrError(Position);
        }

        /// <summary>
        /// Get state of error by position
        /// </summary>
        /// <param name="Position">Position of error (same as ID)</param>
        /// <returns>String of error state</returns>
        public string GetErrorState(ushort Position)
        {
            return mErrorStack.GetErrorState((byte)Position);
        }

        /// <summary>
        /// Read date / time of first apperance
        /// </summary>
        /// <param name="Position">Identifier of error (error number)</param>
        /// <returns>Date and time of first apperance</returns>
        public DateTime GetErrorFirstAppear(ushort Position)
        {
            return mErrorStack.GetFirstAppear((byte)Position);
        }

        /// <summary>
        /// Read date / time of last apperance
        /// </summary>
        /// <param name="Position">Identifier of error (error number)</param>
        /// <returns>Date and time of last apperance</returns>
        public DateTime GetErrorLastAppear(ushort Position)
        {
            return mErrorStack.GetLastAppear((byte)Position);
        }

        /// <summary>
        /// Read date / time of apperanced until
        /// </summary>
        /// <param name="Position">Identifier of error (error number)</param>
        /// <returns>Date and time of apperanced until</returns>
        public DateTime GetErrorAppearedUntil(ushort Position)
        {
            return mErrorStack.GetAppearedUntil((byte)Position);
        }

        /// <summary>
        /// Read number of appearances
        /// </summary>
        /// <param name="Position">Identifier of error (error number)</param>
        /// <returns>Number of appearances</returns>
        public ushort GetNumberOfAppearances(ushort Position)
        {
            return mErrorStack.GetNumberOfAppearances((byte)Position);
        }

        /// <summary>
        /// Accessors to behave mask from error stack
        /// </summary>
        public UInt16 ErrorBehaveMask
        {
            get
            {
                return mErrorStack.BitMaskBehave;
            }
        }

        /// <summary>
        /// Get name of behave
        /// </summary>
        /// <param name="Position">Position of behave</param>
        /// <returns>String of behave name</returns>
        public string GetBehaveName(byte Position)
        {
            return mProtocol.GetBehaveName(Position);
        }

        /// <summary>
        /// Get state of behave
        /// </summary>
        /// <param name="Position">Position of behave</param>
        /// <returns>String of behave state</returns>
        public string GetBehaveState(byte Position)
        {
            return mErrorStack.GetBehaveState(Position);
        }

        /// <summary>
        /// Accessors to actual error mask from error stack
        /// </summary>
        public UInt64 ErrorActualMask
        {
            get
            {
                return mErrorStack.BitmaskError;
            }
        }

        /// <summary>
        /// Accessors to active error mask from error stack
        /// </summary>
        public UInt64 ErrorActiveMask
        {
            get
            {
                return mErrorStack.BitmaskErrorActive;
            }
        }

        /// <summary>
        /// Check if error is occured
        /// </summary>
        /// <param name="ErrorNo">Identifier of error (error number)</param>
        /// <returns>True if error is occured at least once</returns>
        public bool IsErrorOccured(byte ErrorNo)
        {
            return mErrorStack.IsErrorOccured(ErrorNo);
        }

        /// <summary>
        /// Clear error stack
        /// </summary>
        /// <returns>True on success</returns>
        public bool ResetErrorStack()
        {
            Console.WriteLine("Errorreset..");
            ReturnValue ret = mProtocol.ResetErrorStack();
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>
        /// Export error stack and behaves to a XML file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        public void ErrorBehaveXmlExport(string Filename)
        {
            mErrorStack.XmlExport(Filename);
        }

        /// <summary>
        /// Export error stack and behaves to a CSV file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        public void ErrorBehaveCsvExport(string Filename)
        {
            mErrorStack.CsvExport(Filename);
        }
        #endregion

        #region Interface to error ring
        /// <summary>
        /// Error ring
        /// </summary>
        public void ErrRingStep(byte Position)
        {
            byte[] ErrRingBuffer;// = new byte[32768];
            ReturnValue ret = mProtocol.ReadErrorRing(Position, out ErrRingBuffer);
            if (ret == ReturnValue.NoError)
            {
                mErrorRing.ImportSector(ref ErrRingBuffer, ref mProtocol);
            }
            //else
            //{
            //    return;
            //}

            mLastReturnValue = ret;
        }

        /// <summary>
        /// Get number of error ring items
        /// </summary>
        /// <returns>Number of error ring items</returns>
        public int ErrRingItems()
        {
            return mErrorRing.NumberOfItems();
        }

        /// <summary>
        /// Delete error ring items
        /// </summary>
        public void ErrRingDeleteItems()
        {
            mErrorRing.DeleteItems();
        }

        /// <summary>
        /// Get number of error ring blocks / sectors
        /// </summary>
        /// <returns></returns>
        public byte GetErrorRingBlockNumber()
        {
            // hier stehen die 4KB Sektoren, nicht die 32KB Bloecke
            return (byte)(mProtocol.ErrorRingSectors / 8);
        }

        /// <summary>
        /// Export error ring data to xml file
        /// </summary>
        /// <param name="Filename">Filename</param>
        public void ErrorRingXmlExport(string Filename)
        {
             mErrorRing.XmlExport(Filename, ref mProtocol);
        }

        /// <summary>
        /// Export error ring data to csv file
        /// </summary>
        /// <param name="Filename">Filename</param>
        public void ErrorRingCsvExport(string Filename)
        {
            mErrorRing.CsvExport(Filename, ref mProtocol);
        }

        #endregion

        #region Interface to RTC
        /// <summary>
        /// Read rtc data
        /// </summary>
        /// <returns>True on success</returns>
        public bool ReadRtc()
        {
            ReturnValue ret = mProtocol.ReadRtc();
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>
        /// Get number of volatiles values
        /// </summary>
        /// <returns>Number of volatiles values</returns>
        public UInt16 GetNumberOfVolatiles()
        {
            return mProtocol.GetNumberOfVolatiles();
        }

        /// <summary>
        /// Get volatile value as string
        /// </summary>
        /// <param name="Position">Position of volatile value</param>
        /// <returns>Volatile value as string</returns>
        public string GetVolatileValue(UInt16 Position)
        {
            return mProtocol.GetVolatileValue(Position);
        }
        #endregion

        #region Interface to empirical values
        /// <summary>
        /// Read empirical data
        /// </summary>
        /// <returns>True on success</returns>
        public bool ReadEmpiricals()
        {
            ReturnValue ret = mProtocol.ReadEmpiricals();
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>
        /// Get empirical group names
        /// </summary>
        /// <param name="GroupNames">Position of group</param>
        /// <returns>True on success</returns>
        public bool GetEmpiricalGroupNames(out string[] GroupNames)
        {
            return mProtocol.GetEmpiricalGroupNames(out GroupNames);
        }

        /// <summary>
        /// Get number of empirical values
        /// </summary>
        /// <param name="Group">Position of group</param>
        /// <returns>Number of values</returns>
        public UInt16 GetNumberOfEmpiricalValues(UInt16 Group)
        {
            return mProtocol.GetNumberOfEmpiricalValues((EmpiricalDataBlock.GroupName)Group);
        }

        /// <summary>
        /// Get empirical value string
        /// </summary>
        /// <param name="GroupPosition">Position of group</param>
        /// <param name="ValuePosition">Position of value</param>
        /// <returns>String with name and value</returns>
        public string GetEmpiricalValue(UInt16 GroupPosition, UInt16 ValuePosition)
        {
            return mProtocol.GetEmpiricalValue(GroupPosition, ValuePosition);
        }

        /// <summary>
        /// Export empirical values to a XML file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        public void EmpiricalXmlExport(string Filename)
        {
            UInt16 _groups = 0;
            string _value = "N/A";
            XmlWriter xf = null;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = false;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.Encoding = System.Text.Encoding.UTF8;  //"iso-8859-1";
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.NewLineOnAttributes = true;
            // zaehlen wenn ein oder mehr eintraege
            foreach (EmpiricalDataBlock.GroupName grp in Enum.GetValues(typeof(EmpiricalDataBlock.GroupName)))
            {
                if (mProtocol.GetNumberOfEmpiricalValues(grp) > 0) _groups++;
            }
            try
            {
                xf = XmlWriter.Create(Filename, settings);
                xf.WriteStartElement("empirical_rtc_root");

                xf.WriteStartElement("ECU");
                xf.WriteStartElement("Serialnumber");
                xf.WriteValue(mProtocol.SerialNumber);
                xf.WriteEndElement(); // S/N
                xf.WriteStartElement("Hardware");
                xf.WriteValue(string.Format("{0}.{1}.{2}", mProtocol.HardwareVersion.Hauptversion, mProtocol.HardwareVersion.Nebenversion, mProtocol.HardwareVersion.Revision));
                xf.WriteEndElement(); // HW V
                xf.WriteStartElement("Software");
                xf.WriteValue(string.Format("{0}.{1}.{2}", mProtocol.SoftwareVersion.Hauptversion, mProtocol.SoftwareVersion.Nebenversion, mProtocol.SoftwareVersion.Revision));
                xf.WriteEndElement(); // SW V
                xf.WriteStartElement("Configuration");
                xf.WriteValue(string.Format("{0}.{1}.{2}", mProtocol.ConfigurationVersion.Hauptversion, mProtocol.ConfigurationVersion.Nebenversion, mProtocol.ConfigurationVersion.Revision));
                xf.WriteEndElement(); // CFG V
                xf.WriteEndElement(); // ECU

                UInt16 _total = 0;
                for (UInt16 _itm = 0; _itm < mProtocol.GetNumberOfVolatiles(); _itm++)
                {
                    xf.WriteStartElement("item");
                    xf.WriteAttributeString("row", String.Format("{0}", _total));

                    xf.WriteStartElement("name");
                    _value = String.Format("RTC.{0}", mProtocol.GetVolatileValue(_itm));
                    xf.WriteValue(_value);
                    xf.WriteEndElement();

                    xf.WriteEndElement(); // item
                    _total++;
                }
                for (UInt16 _grp = 0; _grp < _groups; _grp++)
                {
                    // Group
                    int _items = mProtocol.GetNumberOfEmpiricalValues((EmpiricalDataBlock.GroupName)_grp);
                    for (UInt16 _itm = 0; _itm < _items; _itm++)
                    {
                        xf.WriteStartElement("item");
                        xf.WriteAttributeString("row", String.Format("{0}", _total));

                        xf.WriteStartElement("name");
                        _value = mProtocol.GetEmpiricalValue(_grp, _itm);
                        xf.WriteValue(_value);
                        xf.WriteEndElement();

                        xf.WriteEndElement(); // item
                        _total++;
                    }
                }

                xf.WriteEndElement(); // root
                xf.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during opening XML file: {0}", ex.Message);
            }
            finally
            {
                xf.Close();
            }
        }

        /// <summary>
        /// Export empirical values to a Csv file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        public void EmpiricalCsvExport(string Filename)
        {
            UInt16 _groups = 0;
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Filename, false);
            string _ProgDate = "";

            if (mProtocol.LocalTime)
            {
                _ProgDate = mProtocol.DateOfProgramming.ToLocalTime().ToString("dd.MM.yyyy HH:mm");
            }
            else
            {
                _ProgDate = mProtocol.DateOfProgramming.ToUniversalTime().ToString("dd.MM.yyyy HH:mm UTC");
            }

            string _header = String.Format("Production date: {0} SN: {1} HW:V{2}.{3}.{4} SW:V{5}.{6}.{7} CFG:V{8}.{9}.{10}",
                _ProgDate,
                mProtocol.SerialNumber,
                mProtocol.HardwareVersion.Hauptversion,
                mProtocol.HardwareVersion.Nebenversion,
                mProtocol.HardwareVersion.Revision,
                mProtocol.SoftwareVersion.Hauptversion,
                mProtocol.SoftwareVersion.Nebenversion,
                mProtocol.SoftwareVersion.Revision,
                mProtocol.ConfigurationVersion.Hauptversion,
                mProtocol.ConfigurationVersion.Nebenversion,
                mProtocol.ConfigurationVersion.Revision

            );
            writer.WriteLine(_header);

            string _title = String.Format("\"name\"{0}",
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
            writer.WriteLine(_title);

            string _row = "";
            for (UInt16 _itm = 0; _itm < mProtocol.GetNumberOfVolatiles(); _itm++)
            {
                _row = String.Format("\"RTC.{1}\"{0}",
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    mProtocol.GetVolatileValue(_itm));
                writer.WriteLine(_row);
            }
            foreach (EmpiricalDataBlock.GroupName grp in Enum.GetValues(typeof(EmpiricalDataBlock.GroupName)))
            {
                if (mProtocol.GetNumberOfEmpiricalValues(grp) > 0) _groups++;
            }
            for (UInt16 _grp = 0; _grp < _groups; _grp++)
            {
                // Group
                int _items = mProtocol.GetNumberOfEmpiricalValues((EmpiricalDataBlock.GroupName)_grp);
                for (UInt16 _itm = 0; _itm < _items; _itm++)
                {
                    _row = String.Format("\"{1}\"{0}",
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        mProtocol.GetEmpiricalValue(_grp, _itm));
                    writer.WriteLine(_row);
                }
            }
            writer.Close();
        }
#endregion

        #region Interface to DTC
        /// <summary>
        /// Read DTC data
        /// </summary>
        /// <returns>True on success</returns>
        public bool ReadDtc()
        {
            ReturnValue ret = mDtcStack.Read(ref mProtocol);
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>
        /// Info text of diagnostic trouble codes
        /// </summary>
        public string DtcInfo
        {
            get
            {
                return mDtcStack.Info;
            }
        }

        /// <summary>
        /// Freeze frame text of diagnostic trouble codes
        /// </summary>
        public string DtcFF
        {
            get
            {
                return mDtcStack.FreezeFrame;
            }
        }

        /// <summary>
        /// Get DTC derating and flags text
        /// </summary>
        public string GetDtcDerateFlags
        {
            get
            {
                return mDtcStack.DeratingAndFlags;
            }
        }

        /// <summary>
        /// Get number of stack items
        /// </summary>
        /// <returns></returns>
        public byte GetDtcItemCount()
        {
            return mDtcStack.GetStackItemCount();
        }

        /// <summary>
        /// Get stack item strings
        /// </summary>
        /// <param name="Position">Position of stack item</param>
        /// <param name="strFreeWarmUps">Free warm ups of stack item</param>
        /// <param name="strErrorNumber">Error number of stack item</param>
        /// <param name="strOccuranceCounter">Occurance counter of stack item</param>
        /// <param name="strPending">Pending flag of stack item</param>
        /// <param name="strActive">Active flag of stack item</param>
        /// <param name="strPrevActive">Previously active flag of stack item</param>
        /// <param name="strSPN">Suspect parameter number of stack item</param>
        /// <param name="strFMI">Failure mode identifier of stack item</param>
        /// <returns>True if stack item strings are set</returns>
        public bool GetDtcItem(byte Position, out string strFreeWarmUps, out string strErrorNumber,
            out string strOccuranceCounter, out string strPending, out string strActive,
            out string strPrevActive, out string strSPN, out string strFMI)
        {
            return mDtcStack.GetStackItem(Position, out strFreeWarmUps, out strErrorNumber,
            out  strOccuranceCounter, out  strPending, out  strActive, out  strPrevActive
            , out strSPN, out strFMI);
        }
        #endregion

        #region Interface to CFG DAT LNG
        /// <summary>Write configuration in old mode (erase and write for HW 1.34)</summary>
        /// <param name="Data">Source byte array</param>
        /// <returns>True on success</returns>
        public bool WriteConfig(byte[] Data)
        {
            ReturnValue ret = ReturnValue.ComOrderFailed;
            ret = mProtocol.WriteOldConfig(Data);
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>Write datamap in old mode (erase and write for HW 1.34)</summary>
        /// <param name="Data">Source byte array</param>
        /// <returns>True on success</returns>
        public bool WriteDatamap(byte[] Data)
        {
            ReturnValue ret = ReturnValue.ComOrderFailed;
            ret = mProtocol.WriteOldDatamap(Data);
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }

        /// <summary>Write one language in old mode (erase and write for HW 1.34)</summary>
        /// <param name="Position">Position of language in ecu</param>
        /// <param name="Data">Source byte array</param>
        /// <returns>True on success</returns>
        public bool WriteLanguage(int Position, byte[] Data)
        {
            ReturnValue ret = ReturnValue.ComOrderFailed;
            ret = mProtocol.WriteOldLanguage(Position, Data);
            mLastReturnValue = ret;
            return ret == ReturnValue.NoError ? true : false;
        }
        #endregion
    }
}
