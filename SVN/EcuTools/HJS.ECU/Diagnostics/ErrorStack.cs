/*
 * Object: HJS.ECU.Diag.ErrorStack
 * Description: Error memory
 * 
 * $LastChangedDate: 2014-08-13 08:18:17 +0200 (Mi, 13 Aug 2014) $
 * $LastChangedRevision: 67 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Diagnostics/ErrorStack.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Xml;
using HJS.ECU.Protocol;

namespace HJS.ECU.Diagnostics
{
    /// <summary>Error stack of HJS-ECU</summary>
    public class ErrorStack
    {
        private const int MAX_NUMBER_OF_ERRORS = 64; //mProtocol.GetMaxErrorNumber();

        // tsFehlerspeicher
        private UInt32[] mMinuteOfFirstAppear;
        private UInt32[] mMinuteOfLastAppear;
        private UInt32[] mMinuteOfAppearedUntil;
        private UInt16[] mNumberOfAppearances;

        // tsErrorPost
        // Error/Event masks
        private UInt64 mBitmaskErrorNewSet;
        private UInt64 mBitmaskErrorSet;
        private UInt64 mBitmaskErrorActual;
        private UInt64 mBitmaskErrorActive;
        private UInt64 mBitmaskErrorReset;
        private UInt64 mBitmaskErrorNewReset;

        // tsVerhalten
        // Behave masks
        private byte[] mBehaveMaskNewSet;
        private byte[] mBehaveMaskSet;
        private byte[] mBehaveMaskActive;
        private byte[] mBehaveMaskReset;
        private byte[] mBehaveMaskNewReset;

        private ProtocolBase mProtocol;

        /// <summary>Accessors to protocol</summary>
        public ProtocolBase EcuProtocol
        {
            get { return mProtocol; }
            set { mProtocol = value; }
        }

        /// <summary>Accessors to behave bit mask
        /// Read only</summary>
        public UInt16 BitMaskBehave
        {
            get
            {
                UInt16 i = mBehaveMaskActive[1];
                i *= 256;
                i += mBehaveMaskActive[0];
                return i;
            }
        }

        /// <summary>Accessors to actual error bit mask
        /// Read only</summary>
        public UInt64 BitmaskErrorActual
        {
            get
            {
                return mBitmaskErrorActual;
            }
        }

        /// <summary>Accessors to active error bit mask
        /// Read only</summary>
        public UInt64 BitmaskErrorActive
        {
            get
            {
                return mBitmaskErrorActive;
            }
        }

        /// <summary>Constructor</summary>
        /// <param name="CurrentProtocol">Reference to connected ECU protocol</param>
        public ErrorStack(ref ProtocolBase CurrentProtocol)
        {
            mProtocol = CurrentProtocol;
        }

        /// <summary>Delete error stack on ECU</summary>
        /// <returns>True on success</returns>
        public bool Reset()
        {
            return (mProtocol.ResetErrorStack() == ReturnValue.NoError);
        }

        /// <summary>Refresh protocol reference</summary>
        /// <param name="CurrentProtocol">Reference to protocol</param>
        public void SetNewProtocol(ref ProtocolBase CurrentProtocol)
        {
            mProtocol = CurrentProtocol;
        }

        /// <summary>Read error stack from ecu</summary>
        /// <returns>True on success</returns>
        public bool ReadFromEcu()
        {
            byte[] ReadBuffer;
            int PosInBuffer = 0;
            if (mProtocol.ReadErrorStack(out ReadBuffer) == ReturnValue.NoError)
            {
                // parse
                mMinuteOfFirstAppear = new UInt32[MAX_NUMBER_OF_ERRORS];
                mMinuteOfLastAppear = new UInt32[MAX_NUMBER_OF_ERRORS];
                mMinuteOfAppearedUntil = new UInt32[MAX_NUMBER_OF_ERRORS];
                mNumberOfAppearances = new UInt16[MAX_NUMBER_OF_ERRORS];
                for (PosInBuffer = 0; PosInBuffer < (MAX_NUMBER_OF_ERRORS * 11); PosInBuffer += 11)
                {
                    mMinuteOfFirstAppear[PosInBuffer / 11] = ReadBuffer[PosInBuffer + 2];
                    mMinuteOfFirstAppear[PosInBuffer / 11] *= 256;
                    mMinuteOfFirstAppear[PosInBuffer / 11] += ReadBuffer[PosInBuffer + 1];
                    mMinuteOfFirstAppear[PosInBuffer / 11] *= 256;
                    mMinuteOfFirstAppear[PosInBuffer / 11] += ReadBuffer[PosInBuffer + 0];
                    mMinuteOfLastAppear[PosInBuffer / 11] = ReadBuffer[PosInBuffer + 5];
                    mMinuteOfLastAppear[PosInBuffer / 11] *= 256;
                    mMinuteOfLastAppear[PosInBuffer / 11] += ReadBuffer[PosInBuffer + 4];
                    mMinuteOfLastAppear[PosInBuffer / 11] *= 256;
                    mMinuteOfLastAppear[PosInBuffer / 11] += ReadBuffer[PosInBuffer + 3];
                    mMinuteOfAppearedUntil[PosInBuffer / 11] = ReadBuffer[PosInBuffer + 8];
                    mMinuteOfAppearedUntil[PosInBuffer / 11] *= 256;
                    mMinuteOfAppearedUntil[PosInBuffer / 11] += ReadBuffer[PosInBuffer + 7];
                    mMinuteOfAppearedUntil[PosInBuffer / 11] *= 256;
                    mMinuteOfAppearedUntil[PosInBuffer / 11] += ReadBuffer[PosInBuffer + 6];
                    mNumberOfAppearances[PosInBuffer / 11] = ReadBuffer[PosInBuffer + 10];
                    mNumberOfAppearances[PosInBuffer / 11] *= 256;
                    mNumberOfAppearances[PosInBuffer / 11] += ReadBuffer[PosInBuffer + 9];

                }

                mBehaveMaskNewSet = new byte[2];
                mBehaveMaskSet = new byte[2];
                mBehaveMaskActive = new byte[2];
                mBehaveMaskReset = new byte[2];
                mBehaveMaskNewReset = new byte[2];
                mBehaveMaskNewSet[0] = ReadBuffer[PosInBuffer]; PosInBuffer++;
                mBehaveMaskNewSet[1] = ReadBuffer[PosInBuffer]; PosInBuffer++;
                mBehaveMaskSet[0] = ReadBuffer[PosInBuffer]; PosInBuffer++;
                mBehaveMaskSet[1] = ReadBuffer[PosInBuffer]; PosInBuffer++;
                mBehaveMaskActive[0] = ReadBuffer[PosInBuffer]; PosInBuffer++;
                mBehaveMaskActive[1] = ReadBuffer[PosInBuffer]; PosInBuffer++;
                mBehaveMaskReset[0] = ReadBuffer[PosInBuffer]; PosInBuffer++;
                mBehaveMaskReset[1] = ReadBuffer[PosInBuffer]; PosInBuffer++;
                mBehaveMaskNewReset[0] = ReadBuffer[PosInBuffer]; PosInBuffer++;
                mBehaveMaskNewReset[1] = ReadBuffer[PosInBuffer]; PosInBuffer++;

                mBitmaskErrorSet = BitConverter.ToUInt64(ReadBuffer, PosInBuffer);
                PosInBuffer = PosInBuffer + 8;
                mBitmaskErrorReset = BitConverter.ToUInt64(ReadBuffer, PosInBuffer);
                PosInBuffer = PosInBuffer + 8;
                mBitmaskErrorActual = BitConverter.ToUInt64(ReadBuffer, PosInBuffer);
                PosInBuffer = PosInBuffer + 8;
                mBitmaskErrorActive = BitConverter.ToUInt64(ReadBuffer, PosInBuffer);
                PosInBuffer = PosInBuffer + 8;
                mBitmaskErrorNewSet = BitConverter.ToUInt64(ReadBuffer, PosInBuffer);
                PosInBuffer = PosInBuffer + 8;
                mBitmaskErrorNewReset = BitConverter.ToUInt64(ReadBuffer, PosInBuffer);
                PosInBuffer = PosInBuffer + 8;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Check if error has occured</summary>
        /// <param name="ErrorNo">Identifier of error (error number)</param>
        /// <returns>True if error is occured at least once</returns>
        public bool IsErrorOccured(byte ErrorNo)
        {
            if (ErrorNo < MAX_NUMBER_OF_ERRORS)
            {
                if (mMinuteOfFirstAppear != null)
                {
                    if ((mMinuteOfFirstAppear[ErrorNo] == 0)
                        && (mMinuteOfLastAppear[ErrorNo] == 0)
                        && (mMinuteOfAppearedUntil[ErrorNo] == 0))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("HJS.ECU.Diag.IsErrorOccured: ErrorNo must be < " + MAX_NUMBER_OF_ERRORS);
            }
        }

        /// <summary>Read date / time of first apperance</summary>
        /// <param name="ErrorNo">Identifier of error (error number)</param>
        /// <returns>Date and time of first apperance</returns>
        public DateTime GetFirstAppear(byte ErrorNo)
        {
            if (ErrorNo < MAX_NUMBER_OF_ERRORS)
            {
                return mProtocol.GetDateTimeFromMinute(mMinuteOfFirstAppear[ErrorNo]);
            }
            else
            {
                throw new ArgumentOutOfRangeException("HJS.ECU.Diag.GetFirstAppear: ErrorNo must be < " + MAX_NUMBER_OF_ERRORS);
            }
        }

        /// <summary>Read date / time of last apperance</summary>
        /// <param name="ErrorNo">Identifier of error (error number)</param>
        /// <returns>Date and time of last apperance</returns>
        public DateTime GetLastAppear(byte ErrorNo)
        {
            if (ErrorNo < MAX_NUMBER_OF_ERRORS)
            {
                return mProtocol.GetDateTimeFromMinute(mMinuteOfLastAppear[ErrorNo]);
            }
            else
            {
                throw new ArgumentOutOfRangeException("HJS.ECU.Diag.GetLastAppear: ErrorNo must be < " + MAX_NUMBER_OF_ERRORS);
            }
        }

        /// <summary>Read date / time of apperanced until</summary>
        /// <param name="ErrorNo">Identifier of error (error number)</param>
        /// <returns>Date and time of apperanced until</returns>
        public DateTime GetAppearedUntil(byte ErrorNo)
        {
            if (ErrorNo < MAX_NUMBER_OF_ERRORS)
            {
                return mProtocol.GetDateTimeFromMinute(mMinuteOfAppearedUntil[ErrorNo]);
            }
            else
            {
                throw new ArgumentOutOfRangeException("HJS.ECU.Diag.GetAppearedUntil: ErrorNo must be < " + MAX_NUMBER_OF_ERRORS);
            }
        }

        /// <summary>Read number of appearances</summary>
        /// <param name="ErrorNo">Identifier of error (error number)</param>
        /// <returns>Number of appearnces</returns>
        public ushort GetNumberOfAppearances(byte ErrorNo)
        {
            if (ErrorNo < MAX_NUMBER_OF_ERRORS)
            {
                return mNumberOfAppearances[ErrorNo];
            }
            else
            {
                throw new ArgumentOutOfRangeException("HJS.ECU.Diag.GetNumberOfAppearances: ErrorNo must be < " + MAX_NUMBER_OF_ERRORS);
            }
        }

        /// <summary>Get state of error</summary>
        /// <param name="ErrorNo">Identifier of error (error number)</param>
        /// <returns>State of error as string</returns>
        public string GetErrorState(byte ErrorNo)
        {
            string ret = " (";
            if (ErrorNo < MAX_NUMBER_OF_ERRORS)
            {
                UInt64 CurErrMask = (1LU << ErrorNo);
                // 
                if ((mBitmaskErrorNewSet & CurErrMask) != 0)
                {
                    ret += " NS";
                }
                if ((mBitmaskErrorSet & CurErrMask) != 0)
                {
                    ret += " S";
                }
                if ((mBitmaskErrorActual & CurErrMask) != 0)
                {
                    ret += " Al";
                }
                if ((mBitmaskErrorActive & CurErrMask) != 0)
                {
                    ret += " Av";
                }
                if ((mBitmaskErrorReset & CurErrMask) != 0)
                {
                    ret += " R";
                }
                if ((mBitmaskErrorNewReset & CurErrMask) != 0)
                {
                    ret += " NR";
                }
                ret += " )";
                // actual BitmaskError
                if ((BitmaskErrorActual & CurErrMask) != 0)
                {
                    // active BitmaskErrorActive
                    if ((BitmaskErrorActive & CurErrMask) != 0)
                    {
                        return "aktiv" + ret; // und aktuell
                    }
                    else
                    {
                        return "sporadisch" + ret;
                    }
                }
                else
                {
                    // active BitmaskErrorActive
                    if ((BitmaskErrorActive & CurErrMask) != 0)
                    {
                        return "aktiv" + ret;
                    }
                    else
                    {
                        return "nicht aktiv" + ret;
                    }
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("HJS.ECU.Diag.GetErrorState: ErrorNo must be < " + MAX_NUMBER_OF_ERRORS);
            }
        }

        /// <summary>Get state of behave</summary>
        /// <param name="Position">Position of behave</param>
        /// <returns>String of behave state</returns>
        public string GetBehaveState(byte Position)
        {
            string ret = "";
            if (Position < 16)
            {
                if (Position < 8)
                {
                    int mask = (1 << Position);
                    if (((int)mBehaveMaskNewSet[0] & mask) != 0)
                    {
                        ret += " NS";
                    }
                    if (((int)mBehaveMaskSet[0] & mask) != 0)
                    {
                        ret += " S";
                    }
                    if (((int)mBehaveMaskActive[0] & mask) != 0)
                    {
                        ret += " A";
                    }
                    if (((int)mBehaveMaskReset[0] & mask) != 0)
                    {
                        ret += " R";
                    }
                    if (((int)mBehaveMaskNewReset[0] & mask) != 0)
                    {
                        ret += " NR";
                    }
                }
                else
                {
                    int mask = (1 << (Position - 8));
                    if (((int)mBehaveMaskNewSet[1] & mask) != 0)
                    {
                        ret += " NS";
                    }
                    if (((int)mBehaveMaskSet[1] & mask) != 0)
                    {
                        ret += " S";
                    }
                    if (((int)mBehaveMaskActive[1] & mask) != 0)
                    {
                        ret += " A";
                    }
                    if (((int)mBehaveMaskReset[1] & mask) != 0)
                    {
                        ret += " R";
                    }
                    if (((int)mBehaveMaskNewReset[1] & mask) != 0)
                    {
                        ret += " NR";
                    }
                }
            }
            else
            {
                ret = "N/A";
            }
            return ret;
        }

        /// <summary>Export error stack to a XML file</summary>
        /// <param name="Filename">Target file name</param>
        public void XmlExport(string Filename)
        {
            XmlWriter xf = null;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = false;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.Encoding = System.Text.Encoding.UTF8;  //"iso-8859-1";
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.NewLineOnAttributes = true;
            try
            {
                xf = XmlWriter.Create(Filename, settings);
                xf.WriteStartElement("error_root");

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
                xf.WriteStartElement("Datamap");
                xf.WriteValue(string.Format("{0}.{1}.{2}", mProtocol.DatamapVersion.Hauptversion, mProtocol.DatamapVersion.Nebenversion, mProtocol.DatamapVersion.Revision));
                xf.WriteEndElement(); // KF V
                xf.WriteEndElement(); // ECU

                for (byte item = 0; item < 64; item++)
                {
                    if (IsErrorOccured(item))
                    {
                        xf.WriteStartElement("error");
                        xf.WriteAttributeString("number", String.Format("{0}", item));

                        xf.WriteStartElement("name");
                        xf.WriteValue(mProtocol.GetErrorName(item));
                        xf.WriteEndElement();

                        xf.WriteStartElement("type");
                        xf.WriteValue(mProtocol.IsEventOrError(item) ? "Event" : "Fehler");
                        xf.WriteEndElement();

                        xf.WriteStartElement("state");
                        xf.WriteValue(GetErrorState(item));
                        xf.WriteEndElement();

                        xf.WriteStartElement("first_appear");
                        xf.WriteValue(GetFirstAppear(item));
                        xf.WriteEndElement();

                        xf.WriteStartElement("last_appear");
                        xf.WriteValue(GetLastAppear(item));
                        xf.WriteEndElement();

                        xf.WriteStartElement("appeared_until");
                        xf.WriteValue(GetAppearedUntil(item));
                        xf.WriteEndElement();

                        xf.WriteStartElement("occurances");
                        xf.WriteValue(GetNumberOfAppearances(item));
                        xf.WriteEndElement();

                        xf.WriteEndElement(); // error
                    }
                }
                for (byte item = 0; item < 16; item++)
                {
                    if (IsErrorOccured(item)){
                        xf.WriteStartElement("behave");
                        xf.WriteAttributeString("number", String.Format("{0}", item));

                        xf.WriteStartElement("name");
                        xf.WriteValue(mProtocol.GetBehaveName(item));
                        xf.WriteEndElement();

                        xf.WriteStartElement("state");
                        xf.WriteValue(GetBehaveState(item));
                        xf.WriteEndElement();

                        xf.WriteEndElement(); // behave
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
        /// <summary>Export error stack to a CSV file</summary>
        /// <param name="Filename">Target file name</param>
        public void CsvExport(string Filename)
        {
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

            string _title = String.Format("\"error\"{0}\"name\"{0}\"type\"{0}\"state\"{0}\"first_appear\"{0}\"last_appear\"{0}\"appeared_until\"{0}\"occurances\"{0}",
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
            writer.WriteLine(_title);

            string _row = "";

            for (byte item = 0; item < 64; item++)
            {
                if (IsErrorOccured(item))
                {
                   _row = String.Format("\"{1}\"{0}\"{2}\"{0}\"{3}\"{0}\"{4}\"{0}\"{5}\"{0}\"{6}\"{0}\"{7}\"{0}\"{8}\"",
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        item,
                        mProtocol.GetErrorName(item),
                        mProtocol.IsEventOrError(item) ? "Event" : "Fehler",
                        GetErrorState(item),
                        GetFirstAppear(item),
                        GetLastAppear(item),
                        GetAppearedUntil(item),
                        GetNumberOfAppearances(item));
                    writer.WriteLine(_row);
                }
            }
            _title = String.Format("\"behave\"{0}\"name\"{0}\"state\"{0}",
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
            writer.WriteLine(_title);
            for (byte item = 0; item < 16; item++)
            {
                if (IsErrorOccured(item))
                {
                    _row = String.Format("\"{1}\"{0}\"{2}\"{0}\"{3}\"{0}",
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        item,
                        mProtocol.GetBehaveName(item),
                        GetBehaveState(item));
                    writer.WriteLine(_row);
                }
            }
            writer.Close();
        }

        /// <summary>Generate block from byte array</summary>
        /// <returns>Block of error stack data</returns>
        public Block ToBlock()
        {
            if (mNumberOfAppearances == null) return null;
            Block ret = new Block();

            ret.Type = Block.BlockId.IdFehlerStack;

            byte[] _buffer;
            int BytePosition = 6;
            byte[] _raw = new byte[762 + 6];
            _raw[(int)Block.HeaderPosition.Type] = (byte)Block.BlockId.IdFehlerStack;
            _raw[(int)Block.HeaderPosition.Version] = 1;
            _raw[(int)Block.HeaderPosition.SizeLowByte] = (byte)(762 % 256);
            _raw[(int)Block.HeaderPosition.SizeHighByte] = (byte)(762 / 256);

            for (int i = 0; i < MAX_NUMBER_OF_ERRORS; i++)
            {
                _buffer = BitConverter.GetBytes(mMinuteOfFirstAppear[i]);
                _raw[BytePosition] = _buffer[0]; BytePosition++;
                _raw[BytePosition] = _buffer[1]; BytePosition++;
                _raw[BytePosition] = _buffer[2]; BytePosition++;
                _buffer = BitConverter.GetBytes(mMinuteOfLastAppear[i]);
                _raw[BytePosition] = _buffer[0]; BytePosition++;
                _raw[BytePosition] = _buffer[1]; BytePosition++;
                _raw[BytePosition] = _buffer[2]; BytePosition++;
                _buffer = BitConverter.GetBytes(mMinuteOfAppearedUntil[i]);
                _raw[BytePosition] = _buffer[0]; BytePosition++;
                _raw[BytePosition] = _buffer[1]; BytePosition++;
                _raw[BytePosition] = _buffer[2]; BytePosition++;
                _buffer = BitConverter.GetBytes(mNumberOfAppearances[i]);
                _raw[BytePosition] = _buffer[0]; BytePosition++;
                _raw[BytePosition] = _buffer[1]; BytePosition++;
            }
            _raw[BytePosition] = mBehaveMaskNewSet[0]; BytePosition++;
            _raw[BytePosition] = mBehaveMaskNewSet[1]; BytePosition++;
            _raw[BytePosition] = mBehaveMaskSet[0]; BytePosition++;
            _raw[BytePosition] = mBehaveMaskSet[1]; BytePosition++;
            _raw[BytePosition] = mBehaveMaskActive[0]; BytePosition++;
            _raw[BytePosition] = mBehaveMaskActive[1]; BytePosition++;
            _raw[BytePosition] = mBehaveMaskReset[0]; BytePosition++;
            _raw[BytePosition] = mBehaveMaskReset[1]; BytePosition++;
            _raw[BytePosition] = mBehaveMaskNewReset[0]; BytePosition++;
            _raw[BytePosition] = mBehaveMaskNewReset[1]; BytePosition++;
            _buffer = BitConverter.GetBytes(mBitmaskErrorNewSet);
            for (int i = 0; i < 8; i++)
            {
                _raw[BytePosition] = _buffer[i];
                BytePosition++;
            }
            _buffer = BitConverter.GetBytes(mBitmaskErrorSet);
            for (int i = 0; i < 8; i++)
            {
                _raw[BytePosition] = _buffer[i];
                BytePosition++;
            }
            _buffer = BitConverter.GetBytes(mBitmaskErrorActual);
            for (int i = 0; i < 8; i++)
            {
                _raw[BytePosition] = _buffer[i];
                BytePosition++;
            }
            _buffer = BitConverter.GetBytes(mBitmaskErrorActive);
            for (int i = 0; i < 8; i++)
            {
                _raw[BytePosition] = _buffer[i];
                BytePosition++;
            }
            _buffer = BitConverter.GetBytes(mBitmaskErrorReset);
            for (int i = 0; i < 8; i++)
            {
                _raw[BytePosition] = _buffer[i];
                BytePosition++;
            }
            _buffer = BitConverter.GetBytes(mBitmaskErrorNewReset);
            for (int i = 0; i < 8; i++)
            {
                _raw[BytePosition] = _buffer[i];
                BytePosition++;
            }

            ret.ReadRaw(ref _raw, false);
            ret.GenerateChecksum();

            return ret;
        }
    }
}
