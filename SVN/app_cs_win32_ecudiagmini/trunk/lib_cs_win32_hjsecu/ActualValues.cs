/*
 * Object: HJS.ECU.Diag.ActualValues
 * Description: Measurement values
 * 
 * $LastChangedDate: 2012-09-03 13:45:27 +0200 (Mo, 03 Sep 2012) $
 * $LastChangedRevision: 12 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/trunk/lib_cs_win32_hjsecu/ActualValues.cs $
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
    /// Item of actual values
    /// </summary>
    public class ActualValueItem
    {
        /// <summary>
        /// Name of actual value
        /// </summary>
        public string Name;

        /// <summary>
        /// String of value of actual value
        /// </summary>
        public string ValueString;

        /// <summary>
        /// Unit string of actual value
        /// </summary>
        public string Unit;

        /// <summary>
        /// Flag if actual value is hidden
        /// </summary>
        public bool Hidden;

        /// <summary>
        /// Flag if actual value is shown in displays
        /// </summary>
        public bool Display;
    }

    /// <summary>
    /// Class for object list of actual values
    /// </summary>
    public class ActualValues
    {
        /// <summary>
        /// Byte array of communicated raw data
        /// </summary>
        private byte[] mReadBuffer;

        /// <summary>
        /// Array of actual value items
        /// </summary>
        public ActualValueItem[] Item;

        /// <summary>
        /// Number of items
        /// </summary>
        private byte mValueNumber;

        /// <summary>
        /// Connection to protocol
        /// </summary>
        private ProtocolBase mProtocol;

        /// <summary>
        /// Accessors of protocol connection
        /// </summary>
        public ProtocolBase EcuProtocol
        {
            get { return mProtocol; }
            set { mProtocol = value; }
        }

        /// <summary>
        /// Accessors of number of values
        /// </summary>
        public byte ValueNumber
        {
            get { return mValueNumber; }
            set { mValueNumber = value; }
        }

        /// <summary>
        /// Constructor of actual value object
        /// </summary>
        /// <param name="CurrentProtocol">Reference to current protocol</param>
        public ActualValues(ref ProtocolBase CurrentProtocol)
        {
            mProtocol = CurrentProtocol;
            // Get number of items
            mValueNumber = 255; // CurrentProtocol.GetValueNumber();
            // Prepare arrays
            mReadBuffer = new byte[2 * mValueNumber];
            Item = new ActualValueItem[(int)mValueNumber];
        }

        /// <summary>
        /// Prepare array sizes to new protocol
        /// </summary>
        /// <param name="CurrentProtocol"></param>
        public void SetNewProtocol(ref ProtocolBase CurrentProtocol)
        {
            mProtocol = CurrentProtocol;
            // Get number of items
            ValueNumber = CurrentProtocol.GetValueNumber();
        }

        /// <summary>
        /// Get new data from ECU
        /// </summary>
        /// <returns>True on success, false on error, then old data will be available</returns>
        public ReturnValue RefreshValues()
        {
            ReturnValue ret = mProtocol.ReadActualValues(out mReadBuffer);
            if (ret == ReturnValue.NoError)
            {
                for (byte i = 0; i < mValueNumber; i++)
                {
                    UInt16 uiValue = (UInt16)(mReadBuffer[(i * 2) + 1] * 256);
                    uiValue += mReadBuffer[(i * 2)];
                    if (Item[i] == null)
                    {
                        Item[i] = new ActualValueItem();
                    }
                    Item[i].ValueString = mProtocol.GetValueString(i, uiValue);
                }
            }
            return ret;
        }

        /// <summary>
        /// Get strings for actual values from language read by connection
        /// The text for unit is automatically alternative unit, if factor is set
        /// <param name="NoAltUnit">True to use original unit, false for alternative unit (if available)</param>
        /// </summary>
        public void RefreshLanguage(bool NoAltUnit)
        {
            mProtocol.SetValueAltUnitFlag(NoAltUnit);
            for (byte i = 0; i < mValueNumber; i++)
            {
                Item[i] = new ActualValueItem();
                Item[i].Name = mProtocol.GetValueName(i);
                Item[i].Unit = mProtocol.GetValueUnit(i);
                Item[i].Hidden = mProtocol.GetValueHiddenFlag(i);
                Item[i].Display = mProtocol.GetValueDisplayFlag(i);
            }
        }

        /// <summary>
        /// Export actual values to a XML file
        /// </summary>
        /// <param name="Filename">Target file name</param>
        public void XmlExport(string Filename)
        {
            UInt16 uiValue;
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
                xf.WriteStartElement("actual_root");

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

                for (byte item = 0; item < mValueNumber; item++)
                {
                    xf.WriteStartElement("item");
                    xf.WriteAttributeString("row", String.Format("{0}", item));

                    xf.WriteStartElement("name");
                    xf.WriteValue(mProtocol.GetValueName(item));
                    xf.WriteEndElement();

                    xf.WriteStartElement("value");
                    uiValue = (UInt16)(mReadBuffer[(item * 2) + 1] * 256);
                    uiValue += mReadBuffer[(item * 2)];
                    xf.WriteValue(mProtocol.GetValueString(item, uiValue));
                    xf.WriteEndElement();

                    xf.WriteStartElement("unit");
                    xf.WriteValue(mProtocol.GetValueUnit(item));
                    xf.WriteEndElement();

                    xf.WriteEndElement(); // item
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
        /// Export actual values to a CSV file
        /// </summary>
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

            string _title = String.Format("\"name\"{0}\"value\"{0}\"unit\"{0}",
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
            writer.WriteLine(_title);

            UInt16 uiValue = 0;
            string _row = "";
            for (byte item = 0; item < mValueNumber; item++)
            {

                uiValue = (UInt16)(mReadBuffer[(item * 2) + 1] * 256);
                uiValue += mReadBuffer[(item * 2)];

                _row = String.Format("\"{1}\"{0}\"{2}\"{0}\"{3}\"{0}",
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    mProtocol.GetValueName(item),
                    mProtocol.GetValueString(item, uiValue),
                    mProtocol.GetValueUnit(item));

                writer.WriteLine(_row);
            }
            writer.Close();
        }
    }
}
