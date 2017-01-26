/*
 * Object: HJS.ECU.Port.Comm
 * Description: Base class of communication object for RS232
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Port/Comm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU.Port
{
    /// <summary>Interface to port object</summary>
    public abstract class Comm
    {
        /// <summary>Enumeration for protocol status byte contents</summary>
        public enum StatusByte : byte
        {
            /// <summary>No error in communication</summary>
            NoError = 0,
            /// <summary>Status No 2nd Flash device 0x78</summary>
            NoDecive = 120,
            /// <summary>Status ECU mode does not match STB 0x79</summary>
            ModeFailed = 121,
            /// <summary>Status CRC16 error 0x7A</summary>
            ChecksumError = 122,
            /// <summary>Status Could not execute STB 0x7B</summary>
            OrderFailed = 123,
            /// <summary>Status Password error 0x7C</summary>
            PasswordError = 124,
            /// <summary>Status RS232 Protocol error 0x7D</summary>
            VersionError = 125,
            /// <summary>Status Unknown STB 0x7E</summary>
            UnknownOrder = 126,
            /// <summary>Status Un synchronous (1. 10Byte wrong) 0x7F</summary>
            SyncError = 127
        }

        /// <summary>Enumeration for orders (formally known as STB)</summary>
        public enum OrderByte : byte
        {
            /// <summary>Null enumeration, invalid command</summary>
            Invalid = 0,
            /// <summary>Read compatibility information</summary>
            GetCompatibility = 7,
            /// <summary>Read measurement values</summary>
            ReadMeasuresOnly = 8,
            /// <summary>Read calculated values</summary>
            ReadCalculatedOnly = 9,
            /// <summary>Reboot ECU</summary>
            Reboot = 16,
            /// <summary>Write configuration</summary>
            WriteConfiguration = 17,
            /// <summary>initilize block writing</summary>
            SetInitBlockWriting = 18,
            /// <summary>Write parameter block</summary>
            WriteBlock = 19,
            /// <summary>Read block</summary>
            ReadBlock = 20,
            /// <summary>Read DTC identifiers</summary>
            ReadDTCIds = 21,
            /// <summary>Read configuration</summary>
            ReadConfiguration = 22,
            /// <summary>Read values (measurement and calculated)</summary>
            ReadActualValues = 23,
            /// <summary>Check communication</summary>
            Alive = 24,
            /// <summary>Read error ring</summary>
            ReadErrorRing = 25,
            /// <summary>Read error ring sector</summary>
            ReadErrorRingSector = 26,
            /// <summary>Write configuration and datamap</summary>
            WriteCfgKf = 27,
            /// <summary>Read acquisition sector from device 1</summary>
            ReadAcquiDev1Sector = 28,
            /// <summary>Read acquisition sector from device 2</summary>
            ReadAcquiDev2Sector = 29,
            /// <summary>Read behavior</summary>
            ReadBehavior = 30,
            /// <summary>Read info string</summary>
            ReadInfoText = 31,
            /// <summary>Write block free1</summary>
            WriteFree = 32,
            /// <summary>read language</summary>
            ReadLanguage = 33,
            /// <summary>Write block free2</summary>
            WriteFree2 = 34,
            /// <summary>Read error stack</summary>
            ReadErrorStack = 35,
            /// <summary>Write KWP2000 buffer</summary>
            WriteKWPBuffer = 36,
            /// <summary>Read data map</summary>
            ReadDataMap = 37,
            /// <summary>Write data map</summary>
            WriteDataMap = 38,
            /// <summary>Read digital information block</summary>
            ReadInfoBlock = 39,
            /// <summary>Write german language</summary>
            WriteLanguageDe = 40,
            /// <summary>Write english language</summary>
            WriteLanguageEn = 41,
            /// <summary>Write french Language</summary>
            WriteLanguageFr = 42,
            /// <summary>Write italian language</summary>
            WriteLanguageIt = 43,
            /// <summary>Delete error ring data</summary>
            ResetErrorRing = 44,
            /// <summary>Delete acquisition data</summary>
            ResetAcquisition = 45,
            /// <summary>Read behave ring</summary>
            ReadBehaveToggleRing = 46,
            /// <summary>Read real time clock data</summary>
            ReadRealTimeClockData = 47,
            /// <summary>Read acquisition data 1</summary>
            ReadAcquisitionSector_01 = 48,
            /// <summary>Read acquisition data 2</summary>
            ReadAcquisitionSector_02 = 49,
            /// <summary>Read acquisition data 3</summary>
            ReadAcquisitionSector_03 = 50,
            /// <summary>Read acquisition data 4</summary>
            ReadAcquisitionSector_04 = 51,
            /// <summary>Read acquisition data 5</summary>
            ReadAcquisitionSector_05 = 52,
            /// <summary>Read acquisition data 6</summary>
            ReadAcquisitionSector_06 = 53,
            /// <summary>Read acquisition data 7</summary>
            ReadAcquisitionSector_07 = 54,
            /// <summary>Read acquisition data 8</summary>
            ReadAcquisitionSector_08 = 55,
            /// <summary>Read acquisition data 9</summary>
            ReadAcquisitionSector_09 = 56,
            /// <summary>Read acquisition data 10</summary>
            ReadAcquisitionSector_10 = 57,
            /// <summary>Read acquisition data 11</summary>
            ReadAcquisitionSector_11 = 58,
            /// <summary>Read acquisition data 12</summary>
            ReadAcquisitionSector_12 = 59,
            /// <summary>Read acquisition data 13</summary>
            ReadAcquisitionSector_13 = 60,
            /// <summary>Check second device assembled</summary>
            CheckFlash2ndDevice = 61,
            /// <summary>Read empirical data</summary>
            ReadEmpiricalValues = 62,
            /// <summary>Read acquisition and error ring definitions and info</summary>
            ReadRingInfo = 63,
            /// <summary>Set additive refilled</summary>
            SetAdditiveRefilled = 64,
            /// <summary>Calibrate rpm</summary>
            SetTurnScale = 65,
            /// <summary>Set time</summary>
            SetTime = 66,
            /// <summary>Delete errors</summary>
            ResetErrors = 67,
            /// <summary>Master reset</summary>
            ResetMaster = 68,
            /// <summary>Set dosing pulses</summary>
            SetDosingPulses = 69,
            /// <summary>Start regeneration</summary>
            StartRegeneration = 70,
            /// <summary>Set filter changed</summary>
            SetFilterChanged = 71,
            /// <summary>Toggle EGR</summary>
            SetEGRToggle = 72,
            /// <summary>Start pressure averaging</summary>
            StartPressureMeasurement = 73,
            /// <summary>Set warning level pressure</summary>
            SetWarningPressure = 74,
            /// <summary>Set alarm level pressure</summary>
            SetAlarmPressure = 75,
            /// <summary>Set alarm level time</summary>
            SetAlarmTimeDelay = 76,
            /// <summary>Calibrate fuel sensor gain</summary>
            SetFuelSensorAmplifier = 77,
            /// <summary>Start heater</summary>
            StartHeating = 78,
            /// <summary>Erase secured block</summary>
            EraseSaveBlock = 79,
            /// <summary>Set EOS</summary>
            ProdEOS = 80,
            /// <summary>Set LMM</summary>
            ProdLMM = 81,
            /// <summary>Toggle fuel sensor</summary>
            ProdToggleTank = 82,
            /// <summary>Set heater values</summary>
            ProdHeat = 83,
            /// <summary>Set serial number</summary>
            SetSerialNumber = 84,
            /// <summary>Set SBC</summary>
            ProdSBC_WUR_INTR = 85,
            /// <summary>Calibrate ambient temperature sensor</summary>
            SetTemperatureCalibration = 86,
            /// <summary>Delete drive pattern</summary>
            ResetDrivePatternDetection = 87,
            /// <summary>Write drive pattern 1</summary>
            WriteDrivePatternDetection1 = 88,
            /// <summary>Write drive pattern 2</summary>
            WriteDrivePatternDetection2 = 89,
            /// <summary>Write empirical values 1</summary>
            WriteValue1 = 90,
            /// <summary>write empirical values 2</summary>
            WriteValue2 = 91,
            /// <summary>Write behave ring 1</summary>
            WriteBehaveRing1 = 92,
            /// <summary>Write behave ring 2</summary>
            WriteBehaveRing2 = 93,
            /// <summary>Write behave ring 3</summary>
            WriteBehaveRing3 = 94,
            /// <summary>Write secured block</summary>
            WriteSaveBlock = 95,
            /// <summary>Read acquisition data 14</summary>
            ReadAcquisitionSector_14 = 96,
            /// <summary>Read acquisition data 15</summary>
            ReadAcquisitionSector_15 = 97,
            /// <summary>Read acquisition data 16</summary>
            ReadAcquisitionSector_16 = 98,
            /// <summary>Read acquisition data 17</summary>
            ReadAcquisitionSector_17 = 99,
            /// <summary>Read acquisition data 18</summary>
            ReadAcquisitionSector_18 = 100,
            /// <summary>Read acquisition data 19</summary>
            ReadAcquisitionSector_19 = 101,
            /// <summary>Read acquisition data 20</summary>
            ReadAcquisitionSector_20 = 102,
            /// <summary>Read acquisition data 21</summary>
            ReadAcquisitionSector_21 = 103,
            /// <summary>Read acquisition data 22</summary>
            ReadAcquisitionSector_22 = 104,
            /// <summary>Read acquisition data 23</summary>
            ReadAcquisitionSector_23 = 105,
            /// <summary>Read acquisition data 24</summary>
            ReadAcquisitionSector_24 = 106,
            /// <summary>Read acquisition data 25</summary>
            ReadAcquisitionSector_25 = 107,
            /// <summary>Read acquisition data 26</summary>
            ReadAcquisitionSector_26 = 108,
            /// <summary>Read acquisition data 27</summary>
            ReadAcquisitionSector_27 = 109,
            /// <summary>Read acquisition data 28</summary>
            ReadAcquisitionSector_28 = 110,
            /// <summary>Read acquisition data 29</summary>
            ReadAcquisitionSector_29 = 111,
            /// <summary>Delete DTC</summary>
            EraseDTC = 112,
            /// <summary>Read DTC</summary>
            ReadDTC = 113,
            /// <summary>Calibrate differential pressure sensor</summary>
            StartPressureSensorCalibration = 114,
            /// <summary>Calibrate ambient pressure sensor</summary>
            StartAmbientPressureSensorCalibration = 115,
            /// <summary>Read Mask with availabe Languages</summary>
            ReadLanguageMask = 116,
            /// <summary>Calibrate Venturi pressure sensor</summary>
            StartVenturiSensorCalibration = 117,
            /// <summary>Set warning level time</summary>
            SetWarningTimeDelay = 118,
            /// <summary>Set updatable empirical value</summary>
            SetUpdatableValue = 119,
            /// <summary>Read updatable empirical values</summary>
            ReadUpdatables = 120,
            /// <summary>Check if task ist started on ECU</summary>
            IsTaskActive = 121,
            /// <summary>Read report block</summary>
            ReadReportBlock = 122,
            /// <summary>Tester detect ECU</summary>
            TesterDetectEcu = 128,
            /// <summary>Tester read version</summary>
            TesterVersion = 129,
            /// <summary>Tester set lock</summary>
            TesterLockState = 130,
            /// <summary>Tester eject</summary>
            TesterEjectEcu = 132,
            /// <summary>Tester send cyclic UDA CAN messages</summary>
            TesterSendUdaCyclic = 134,
            /// <summary>Tester set outputs</summary>
            TesterOutputs = 135,
            /// <summary>Tester enable flashing</summary>
            TesterFlash = 137,
            /// <summary>Tester disable flashing</summary>
            TesterNoFlash = 138,
            /// <summary>Read tester temperature</summary>
            TesterTemperature = 139
        }

        /// <summary>Enumeration for type of port</summary>
        public enum PortType
        {
            /// <summary>Direct communication with RS232</summary>
            Direct = 0,
            /// <summary>Indirect communication with modem</summary>
            Modem
        }

        /// <summary>Server identification</summary>
        public enum ServerByte : byte
        {
            /// <summary>Null enumeration</summary>
            Invalid = 0,
            /// <summary>Diagnistics</summary>
            Diagnostics = (byte)'d',
            /// <summary>Production</summary>
            Production = (byte)'p',
            /// <summary>Display</summary>
            Display = (byte)'m'
        }

        /// <summary>Protocol version</summary>
        private byte mVersion;

        /// <summary>Name of current serial port</summary>
        private string mPortName;

        /// <summary>Identifier of server</summary>
        protected byte mServer;

        /// <summary>ECU keyword</summary>
        protected byte[] mKey;

        /// <summary>Accessors for current protocol version
        /// Get from header to PC or set to header to ECU</summary>
        public byte pVersion
        {
            get { return mVersion; }
            set { mVersion = value; }
        }

        /// <summary>Accessors for string of current port name</summary>
        public string PortName
        {
            get { return mPortName; }
            set { mPortName = value; }
        }

        /// <summary>Accessors for server identification (100 = d, or 112 = p)</summary>
        public ServerByte Server
        {
            set { mServer = (byte)value; }
        }

        /// <summary>Checksum of last received communication</summary>
        public abstract UInt16 Checksum
        {
            get;
        }

        /// <summary>Timestamp</summary>
        public abstract UInt32 TimeStamp
        {
            get;
        }

        /// <summary>Accessors for Keyword
        /// Write only</summary>
        public void SetKey(byte[] ucaKey)
        {
            if (ucaKey.Length != 8)
                throw new ArgumentException("Comm: Size of key mismatch!");
            else
            {
                mKey[0] = ucaKey[0];
                mKey[1] = ucaKey[1];
                mKey[2] = ucaKey[2];
                mKey[3] = ucaKey[3];
                mKey[4] = ucaKey[4];
                mKey[5] = ucaKey[5];
                mKey[6] = ucaKey[6];
                mKey[7] = ucaKey[7];
            }
        }

        /// <summary>Open connection to ECU</summary>
        public abstract ReturnValue Connect();

        /// <summary>Close Connection to ECU</summary>
        public abstract void Disconnect();

        /// <summary>Read data from ECU</summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Data">Target data byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue Read(OrderByte STB, out byte[] Data);

        /// <summary>Override Read data from ECU with value for Size parameter</summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Value">Special value to be send in Size parameter</param>
        /// <param name="Data">Target data byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue Read(OrderByte STB, UInt16 Value, out byte[] Data);

        /// <summary>Read data from ECU</summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Data">Target data byte array</param>
        /// <param name="ExecutionTime">Wait execution time of ECU in ms</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue Read(OrderByte STB, out byte[] Data, UInt16 ExecutionTime);

        /// <summary>Send an order to ECU</summary>
        /// <param name="STB">Communication order identifier</param>
        /// <returns>0 on success, else error (see ReturnValue)</returns>
        public abstract ReturnValue Order(OrderByte STB);

        /// <summary>Send an order with an 16-bit-value to ECU</summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Value">Special value to be send in Size parameter</param>
        /// <returns>0 on success, else error (see ReturnValue)</returns>
        public abstract ReturnValue Order(OrderByte STB, UInt16 Value);

        /// <summary>Send an order to ECU with an 16-bit-value and wait for ECU to execute command</summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Value">Special value to be send in Size parameter</param>
        /// <param name="ExecutionTime">Wait execution time of ECU in ms</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public abstract ReturnValue Order(OrderByte STB, UInt16 Value, UInt16 ExecutionTime);

        /// <summary>Write data to ECU</summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Value">Parameter or size of data</param>
        /// <param name="Data">BYte array of data</param>
        /// <param name="ExecutionTime">Execution time between sending request and response</param>
        /// <returns>0 on success, else error (see ReturnValue)</returns>
        public abstract ReturnValue Write(OrderByte STB, UInt16 Value, ref byte[] Data, UInt16 ExecutionTime);

        /// <summary>Write data to ECU after erasing target area</summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Data">BYte array of data</param>
        /// <param name="ExecutionTime">Execution time between sending request and response</param>
        /// <param name="EraseTime">Time required for erasing between sending header and sending data</param>
        /// <returns>0 on success, else error (see ReturnValue)</returns>
        public abstract ReturnValue EraseAndWrite(OrderByte STB, ref byte[] Data, UInt16 ExecutionTime, UInt16 EraseTime);
    }
}
