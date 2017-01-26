/*
 * Object: hjs_ecu_mini.Controller
 * Description: Application controller object
 * 
 * $LastChangedDate: 2013-10-25 14:40:14 +0200 (Fr, 25 Okt 2013) $
 * $LastChangedRevision: 25 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/tags/Version_1_3_1/hjs_ecu_mini/Controller.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace hjs_ecu_mini
{
    /// <summary>
    /// Object fo controlling independent GUI and internal functionionalities
    /// </summary>
    public class Controller : ApplicationContext
    {
        private FrameForm mFrameForm;
        private EcuAdministration mEcuFunc;

        /// <summary>Accessors to protocol version (read only)</summary>
        public byte ProtocolVersion
        {
            get
            {
                if (mEcuFunc == null) return 0;
                else return mEcuFunc.ProtocolVersion;
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="frameForm">Main for object</param>
        public Controller(FrameForm frameForm)
		{
            mFrameForm = frameForm;
            mEcuFunc = new EcuAdministration();
            InitializeComponents();
            mFrameForm.TheController = this;
            mFrameForm.Show();
		}

        private void InitializeComponents()
        {
            mEcuFunc.ProgressDelegate = ReportProgress;
            mEcuFunc.StatusDelegate = ReportStatus;
        }

        /// <summary>
        /// Start connection to ECU
        /// </summary>
        /// <param name="portName">Name of port (starting with COM)</param>
        /// <param name="Language">Language (position in combobox must match enumeration)</param>
        /// <param name="LocalTime">Flag if time displayed local (or UTC)</param>
        /// <returns>True if connection is established</returns>
        public bool Connect(string portName, byte Language, bool LocalTime)
        {
            return mEcuFunc.Connect(portName, Language, LocalTime);
        }

        /// <summary>
        /// Disconnect ECU
        /// </summary>
        /// <returns>Always true</returns>
        public bool Disconnect()
        {
            if (mEcuFunc != null)
            {
                mEcuFunc.Disconnect();
                return true;
            }
            else
            {
                // can not disconnect if device does not exist
                // so return successfully disconnected
                return true;
            }
        }

        /// <summary>
        /// Get string of raw file name
        /// </summary>
        /// <returns>String of raw file name without extension</returns>
        public string GetRawFileName()
        {
            return mEcuFunc.RawFileName;
        }

        /// <summary>
        /// Get date time object of last communication time stamp
        /// </summary>
        /// <returns>String object of last communication time stamp</returns>
        public String GetLastTimeStamp()
        {
            return mEcuFunc.GetLastTimeStamp();
        }

        /// <summary>
        /// Set ecu time
        /// </summary>
        /// <param name="NewTime">Date time object of new ecu time</param>
        /// <returns>True in success</returns>
        public bool SetTime(DateTime NewTime)
        {
            return mEcuFunc.SetTime(NewTime);
        }

        /// <summary>
        /// Delegated function to report progress to GUI
        /// </summary>
        /// <param name="percentage">Progress</param>
        /// <returns>Always returns true</returns>
        public bool ReportProgress(int percentage)
        {
            mFrameForm.BeginInvoke((Action)(delegate { mFrameForm.ReportProgress(percentage); }));
            return true;
        }

        /// <summary>
        /// Delegated function to report status to GUI
        /// </summary>
        /// <param name="message">Status message</param>
        /// <returns>Always returns true</returns>
        public bool ReportStatus(string message)
        {
            mFrameForm.BeginInvoke((Action)(delegate { mFrameForm.ReportStatus(message); }));
            return true;
        }

        /// <summary>
        /// Get version string from ECU
        /// </summary>
        /// <returns></returns>
        public string GetVersionsString()
        {
            return mEcuFunc.GetVersionsString();
        }

        /// <summary>
        /// Get info text from ECU
        /// </summary>
        /// <returns></returns>
        public string GetInfoText()
        {
            return mEcuFunc.GetInfoText();
        }

        /// <summary>
        /// Get number of values from ECU
        /// </summary>
        /// <returns>Number of values that can be read from ECU</returns>
        public byte GetNumberOfValues()
        {
            return mEcuFunc.GetNumberOfValues();
        }

        /// <summary>
        /// Get strings of one value from ECU
        /// </summary>
        /// <param name="rowNum">Position of value, or row number</param>
        /// <param name="ValueName">String of value name</param>
        /// <param name="FormattedValue">String of formatted value</param>
        /// <param name="ValueUnit">String of value unit</param>
        public void GetValueRow(byte rowNum, out string ValueName, out string FormattedValue, out string ValueUnit)
        {
            mEcuFunc.GetValueRow(rowNum, out ValueName, out FormattedValue, out ValueUnit);
        }

        /// <summary>
        /// Read actual values from ECU
        /// </summary>
        /// <returns></returns>
        public bool GetActualValues()
        {
            return mEcuFunc.ReadActualValues();
        }

        /// <summary>
        /// Get hidden flag of one value
        /// </summary>
        /// <param name="Position">Position or row number of required value</param>
        /// <returns>True if value is hidden</returns>
        public bool GetActualValueHiddenFlag(byte Position)
        {
            return mEcuFunc.IsValueHidden(Position);
        }

        /// <summary>
        /// Get display flag of one value
        /// </summary>
        /// <param name="Position">Position or row number of required value</param>
        /// <returns>True if value is displayed</returns>
        public bool GetActualValueDisplayFlag(byte Position)
        {
            return mEcuFunc.IsValueDisplayed(Position);
        }

        /// <summary>
        /// Export actual values to a XML file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        /// <param name="mFileExtensionIndex">Extension identifier</param>
        public void ActualValuesSaveFile(string Filename, int mFileExtensionIndex)
        {
            mEcuFunc.ActualValuesSaveFile(Filename, mFileExtensionIndex);
        }

        /// <summary>
        /// Toggle Alternative unit and original unit
        /// </summary>
        /// <param name="NoAltUnit">True to use original unit, false for alternative unit (if available)</param>
        public void ActualValuesToggleUnit(bool NoAltUnit)
        {
            mEcuFunc.ActualValuesToggleUnit(NoAltUnit);
        }

        /// <summary>
        /// Get strings of one error / event from ECU
        /// </summary>
        /// <param name="rowNum">Position of error, or row number</param>
        /// <param name="ErrorName">Name of error</param>
        /// <param name="Event">String that indicates error or event</param>
        /// <param name="ErrorState">State of error</param>
        /// <param name="FirstA">String of first accurance</param>
        /// <param name="LastA">String of last accurance</param>
        /// <param name="UntilA">String ofaccuranced until</param>
        /// <param name="Anzahl">Number of Accurances</param>
        public void GetErrorRow(ushort rowNum, out string ErrorName, out string Event, out string ErrorState,
            out string FirstA, out string LastA, out string UntilA, out string Anzahl)
        {
            mEcuFunc.GetErrorRow(rowNum, out ErrorName, out Event, out ErrorState,
                out FirstA, out LastA, out UntilA, out Anzahl);
        }

        /// <summary>
        /// Get strings of one behave
        /// </summary>
        /// <param name="Position">Position of behave</param>
        /// <param name="Name">Name of behave</param>
        /// <param name="State">State of behave</param>
        public void GetBehaveRow(byte Position, out string Name, out string State)
        {
            mEcuFunc.GetBehaveRow(Position, out Name, out State);
        }

        /// <summary>
        /// Check if error is occured
        /// </summary>
        /// <param name="ErrorNo">Identifier of error (error number)</param>
        /// <returns>True if error is occured at least once</returns>
        public bool IsErrorOccured(byte ErrorNo)
        {
            return mEcuFunc.IsErrorOccured(ErrorNo);
        }

        /// <summary>
        /// Read error stack from ECU
        /// </summary>
        public bool GetErrorStack()
        {
            return mEcuFunc.ReadErrorStack();
        }

        /// <summary>
        /// Clear error stack
        /// </summary>
        /// <returns>True on success</returns>
        public bool ClearErrorStack()
        {
            return mEcuFunc.ClearErrorStack();
        }

        /// <summary>
        /// Export error stack and behaves to a XML file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        /// <param name="FileExtensionIndex">Extension identifier</param>
        public void ErrorBehaveSaveFile(string Filename, int FileExtensionIndex)
        {
            mEcuFunc.ErrorBehaveSaveFile(Filename, FileExtensionIndex);
        }

        /// <summary>
        /// Start reading complete acquisition from ECU
        /// </summary>
        /// <param name="FileName">File name of acquisition file</param>
        /// <param name="FileExtensionIndex">Extension identifier</param>
        public void ReadAcquisition(string FileName, int FileExtensionIndex)
        {
            mEcuFunc.ReadAcquisition(FileName, FileExtensionIndex);
        }

        /// <summary>
        /// Upload a parameter set file to ECU
        /// </summary>
        /// <param name="FileName">File name of parameterset</param>
        /// <returns>True if file is uploaded successfully</returns>
        public bool UploadConfig(string FileName)
        {
            if (mEcuFunc.UploadConfig(FileName))
            {
                return mEcuFunc.CheckConfig();
            }
            else
            {
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
            return mEcuFunc.SetProductionData(serialNumber, temperature);
        }

        /// <summary>
        /// Master reset ECU
        /// </summary>
        /// <returns>True on success</returns>
        public bool MasterReset()
        {
            return mEcuFunc.MasterReset();
        }

        /// <summary>
        /// Reboot into normal mode
        /// </summary>
        /// <param name="BootMode">Parameter for rebooting</param>
        /// <returns>True on success</returns>
        public bool Reboot(HJS.ECU.Protocol.ProtocolBase.RebootMode BootMode)
        {
            return mEcuFunc.Reboot(BootMode);
        }

        /// <summary>
        /// Start reading complete error ring from ECU
        /// </summary>
        /// <param name="FileName">File name of error ring file</param>
        /// <param name="FileExtensionIndex">Extension identifier</param>
        public void ReadErrorRing(string FileName, int FileExtensionIndex)
        {
            mEcuFunc.ReadErrorRing(FileName, FileExtensionIndex);
        }

        /// <summary>
        /// Read rtc data
        /// </summary>
        /// <returns>True on success</returns>
        public bool ReadRtc()
        {
            return mEcuFunc.ReadRtc();
        }

        /// <summary>
        /// Get number of volatiles values
        /// </summary>
        /// <returns>Number of volatiles values</returns>
        public UInt16 GetNumberOfVolatiles()
        {
            return mEcuFunc.GetNumberOfVolatiles();
        }

        /// <summary>
        /// Get volatile value as string
        /// </summary>
        /// <param name="Position">Position of volatile value</param>
        /// <returns>Volatile value as string</returns>
        public string GetVolatileValue(UInt16 Position)
        {
            return mEcuFunc.GetVolatileValue(Position);
        }

        /// <summary>
        /// Read empirical data
        /// </summary>
        /// <returns>True on success</returns>
        public bool ReadEmpiricals()
        {
            return mEcuFunc.ReadEmpiricals();
        }

        /// <summary>
        /// Get empirical group names
        /// </summary>
        /// <param name="GroupNames">Position of group</param>
        /// <returns>True on success</returns>
        public bool GetEmpiricalGroupNames(out string[] GroupNames)
        {
            return mEcuFunc.GetEmpiricalGroupNames(out GroupNames);
        }

        /// <summary>
        /// Get number of empirical values
        /// </summary>
        /// <param name="Group">Position of group</param>
        /// <returns>Number of values</returns>
        public UInt16 GetNumberOfEmpiricalValues(UInt16 Group)
        {
            return mEcuFunc.GetNumberOfEmpiricalValues(Group);
        }

        /// <summary>
        /// Get empirical value string
        /// </summary>
        /// <param name="GroupPosition">Position of group</param>
        /// <param name="ValuePosition">Position of value</param>
        /// <returns>String with name and value</returns>
        public string GetEmpiricalValue(UInt16 GroupPosition, UInt16 ValuePosition)
        {
            return mEcuFunc.GetEmpiricalValue(GroupPosition, ValuePosition);
        }

        /// <summary>
        /// Export empirical values to a XML file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        /// <param name="mFileExtensionIndex">Extension identifier</param>
        public void EmpiricalSaveFile(string Filename, int mFileExtensionIndex)
        {
            mEcuFunc.EmpiricalSaveFile(Filename, mFileExtensionIndex);
        }

        /// <summary>
        /// Read DTC from ECU
        /// </summary>
        /// <returns>True on success</returns>
        public bool ReadDtc()
        {
            return mEcuFunc.ReadDtc();
        }

        /// <summary>
        /// Get DTC info text
        /// </summary>
        /// <returns></returns>
        public string GetDtcInfo()
        {
            return mEcuFunc.GetDtcInfo();
        }

        /// <summary>
        /// Get DTC freeze frame text
        /// </summary>
        /// <returns></returns>
        public string GetDtcFF()
        {
            return mEcuFunc.GetDtcFF();
        }

        /// <summary>
        /// Get DTC derating and flags text
        /// </summary>
        /// <returns></returns>
        public string GetDtcDerateFlags()
        {
            return mEcuFunc.GetDtcDerateFlags();
        }

        /// <summary>
        /// Get number of stack items
        /// </summary>
        /// <returns></returns>
        public byte GetDtcItemCount()
        {
            return mEcuFunc.GetDtcItemCount();
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
            return mEcuFunc.GetDtcItem(Position, out strFreeWarmUps, out strErrorNumber,
            out  strOccuranceCounter, out  strPending, out  strActive, out  strPrevActive,
            out strSPN, out strFMI);
        }

        /// <summary>
        /// Send order to ecu
        /// </summary>
        /// <param name="OrderId">Order enumerator</param>
        /// <param name="OrderValue">Value of order</param>
        /// <returns>True on success</returns>
        public bool SetOrder(HJS.ECU.Diag.Diagnostics.Orders OrderId, UInt16 OrderValue)
        {
            return mEcuFunc.SetOrder(OrderId, OrderValue);
        }

        /// <summary>
        /// Send direct order to ECU
        /// </summary>
        /// <param name="OrderByte">Order byte</param>
        /// <param name="Parameter">Parameter</param>
        /// <returns>True on success</returns>
        public bool DirectOrder(Byte OrderByte, UInt16 Parameter)
        {
            return mEcuFunc.DirectOrder(OrderByte, Parameter);
        }

        /// <summary>
        /// Apply changed settings
        /// </summary>
        public void ApplySettings()
        {
            mEcuFunc.ApplySettings();
        }

        /// <summary>
        /// Save all files as XML files to certain path
        /// </summary>
        /// <param name="Path">Path to files to save</param>
        public void SaveAllFiles(string Path)
        {
            mEcuFunc.SaveAllFiles(Path);
        }

        /// <summary>Read a parameter set file from ECU</summary>
        /// <param name="FileName">File name of parameterset</param>
        /// <returns>True if file is uploaded successfully</returns>
        public bool ReadConfig(string FileName)
        {
            return mEcuFunc.ReadConfig(FileName);
        }

        /// <summary>Upload a config file to ECU </summary>
        /// <param name="FileName">File name of config</param>
        /// <returns>True if file is uploaded successfully</returns>
        public bool UploadCfgFile(string FileName)
        {
            return mEcuFunc.UploadCfgFile(FileName);
        }

        /// <summary>Upload a data map file to ECU </summary>
        /// <param name="FileName">File name of data map</param>
        /// <returns>True if file is uploaded successfully</returns>
        public bool UploadDatFile(string FileName)
        {
            return mEcuFunc.UploadDatFile(FileName);
        }

        /// <summary>Upload a language file to ECU </summary>
        /// <param name="FileName">File name of language</param>
        /// <returns>True if file is uploaded successfully</returns>
        public bool UploadLngFile(string FileName)
        {
            return mEcuFunc.UploadLngFile(FileName);
        }
    }
}
