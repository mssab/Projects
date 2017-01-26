/*
 * Object: HJS.ECU.Parameter.ParameterSet
 * Description: Interface class for complete set of parameter blocks
 * 
 * $LastChangedDate: 2015-02-18 15:26:37 +0100 (Mi, 18 Feb 2015) $
 * $LastChangedRevision: 89 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/ParameterSet.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;


namespace HJS.ECU.Parameter
{
    /// <summary>Interface class for complete set of parameter blocks</summary>
    public class ParameterSet
    {
        private ConfigurationBlock cfg;
        private Datamap2_Block kf;
        private LanguageBlock[] maLng = new LanguageBlock[0];
        private AuthorBlock mAuthor;
        private ReportBlock mReport;
        private string mLastError;

        /// <summary>Accessors to last error string
        /// read only</summary>
        public string LastError
        {
            get { return mLastError; }
        }

        /// <summary>Default constructor</summary>
        public ParameterSet()
        {
            mLastError = "";
        }

        /// <summary>Open parameter file</summary>
        /// <param name="Filename">Path and filename of parameter set</param>
        /// <returns>True on success, else see last error string</returns>
        public bool Open(string Filename)
        {
            ReturnValue ret = ReturnValue.NoError;
            BlockFile bf = new BlockFile();
            Block bl;
            bool bRet = false;
            ret = bf.Open(HJS.BlockFile.FileIdentifier.ParameterSet, Filename);
            if (ret != HJS.ReturnValue.NoError)
            {
                 mLastError = String.Format("Could not open file! {0}", ret);
            }
            else
            {
                ret = bf.GetBlock(out bl, HJS.Block.BlockId.IdKonfig, false);
                if (ret != HJS.ReturnValue.NoError)
                {
                     mLastError = String.Format("Could not load cfg! {0}", ret);
                }
                else
                {
                    switch (bl.Version)
                    {
                        case 8:
                            cfg = new HJS.ECU.Parameter.Configuration8_Block();
                            break;
                        case 9:
                            cfg = new HJS.ECU.Parameter.Configuration9_Block();
                            break;
                        case 10:
                            cfg = new HJS.ECU.Parameter.Configuration10_Block();
                            break;
                        default:
                            ret = ReturnValue.VersionMismatch;
                            mLastError = "No compatible configuartion in bockfile";
                            break;
                    }
                    if (ret == HJS.ReturnValue.NoError)
                    {
                        ret = cfg.Import(ref bl, true);
                        if (ret != HJS.ReturnValue.NoError)
                        {
                            mLastError = String.Format("Could not parse cfg! {0}", ret);
                        }
                        else
                        {
                            // author
                            ret = bf.GetBlock(out bl, HJS.Block.BlockId.IdAuthor, false);
                            if (ret == ReturnValue.BlockNotFound)
                            {
                                mAuthor = null;
                            }
                            else
                            {
                                mAuthor = new HJS.AuthorBlock(bl);
                            }

                            //kf
                            ret = bf.GetBlock(out bl, HJS.Block.BlockId.IdKennfld, false);
                            if (ret == HJS.ReturnValue.NoError)
                            {
                                kf = new HJS.ECU.Parameter.Datamap2_Block(bl);

                                //lng
                                maLng = new HJS.ECU.LanguageBlock[0];
                                for (byte b = 30; b < 50; b++)
                                {
                                    ret = bf.GetBlock(out bl, (HJS.Block.BlockId)b, false);
                                    if (ret == HJS.ReturnValue.NoError)
                                    {
                                        Array.Resize(ref maLng, maLng.Length + 1);
                                        maLng[maLng.Length - 1] = new HJS.ECU.LanguageBlock(bl);
                                    }
                                    else
                                    {
                                        if (ret == ReturnValue.BlockNotFound)
                                        {
                                            // ignore missing languages
                                            ret = ReturnValue.NoError;
                                        }
                                        else
                                        {
                                            mLastError = String.Format("Could not load language {0}! {1}",
                                                ((HJS.Block.BlockId)b).ToString(), ret);
                                        }
                                    }
                                }
                                bRet = (ret == HJS.ReturnValue.NoError);
                                //report
                                ret = bf.GetBlock(out bl, HJS.Block.BlockId.IdReport, false);
                                if (ret == ReturnValue.BlockNotFound)
                                {
                                    mReport = null;
                                }
                                else
                                {
                                    mReport = new HJS.ReportBlock(bl);
                                }
                            }
                            else
                            {
                                mLastError = String.Format("Could not load data map! {0}", ret);
                            }
                        }
                    }
                }
            }
            bf.Close();

            return bRet;
        }

        /// <summary>Save parameter file</summary>
        /// <param name="Filename">Path and filename of parameter set</param>
        /// <returns>True on success, else see last error string</returns>
        public bool Save(string Filename)
        {
            ReturnValue ret = ReturnValue.NoError;
            BlockFile bf = new BlockFile();
            bool bRet = false;
            ret = bf.Create(BlockFile.FileIdentifier.ParameterSet, Filename, true);
            if (ret != HJS.ReturnValue.NoError)
            {
                mLastError = String.Format("Could not open file! {0}", ret);
            }
            else
            {
                ret = bf.PutBlock(cfg);
                if (ret != HJS.ReturnValue.NoError)
                {
                    mLastError = String.Format("Could save cfg! {0}", ret);
                }
                else
                {
                    ret = bf.PutBlock(kf);
                    if (ret == ReturnValue.NoError)
                    {
                        foreach (HJS.Block lb in maLng)
                        {
                            ret = bf.PutBlock(lb);
                            if (ret != ReturnValue.NoError) break;
                        }
                    }
                    bRet = true;
                }
                bf.Close();
            }
            return bRet;
        }

        /// <summary>Generate filename from config id and date</summary>
        /// <returns>Suggested filename for save dialog as string</returns>
        public string GenerateFilename()
        {
            if (cfg != null)
            {
                return String.Format("{0}_{1}_{2}_{3}",
                    cfg.ConfigVersion.Hauptversion,
                    cfg.ConfigVersion.Nebenversion,
                    cfg.ConfigVersion.Revision,
                    DateTime.Now.ToString("yyMMdd"));
            }
            else
            {
                return "";
            }
        }

        /// <summary>Export parameter file to CSV</summary>
        /// <param name="Filename">Path and filename of target file</param>
        /// <returns>True on success, else see last error string</returns>
        public bool CsvExport(string Filename)
        {
            if (cfg == null)
            {
                mLastError = "Keine Daten vorhanden";
                return false;
            }
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Filename,false,System.Text.Encoding.UTF8);

            // general infos
            string row = String.Format("\"Hauptversion\"{0}{1}",
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                cfg.ConfigVersion.Hauptversion);
            writer.WriteLine(row);
            row = String.Format("\"Nebenversion\"{0}{1}",
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                cfg.ConfigVersion.Nebenversion);
            writer.WriteLine(row);
            row = String.Format("\"Revision\"{0}{1}",
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                 cfg.ConfigVersion.Revision);
            writer.WriteLine(row);
            row = String.Format("\"Abwaertsversion\"{0}{1}",
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                cfg.AbwaertsVersion);
            writer.WriteLine(row);
            row = String.Format("\"Softwaretyp\"{0}{1}",
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                cfg.SoftwareType);
            writer.WriteLine(row);
            row = String.Format("\"Passwortlevel\"{0}{1}",
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                cfg.PasswordLevel);
            writer.WriteLine(row);
            row = String.Format("\"Kompatibilitaet\"{0}{1}",
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                cfg.Version);
            writer.WriteLine(row);
            row = String.Format("\"Taskanzahl\"{0}{1}",
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                cfg.GetTaskNumber());
            writer.WriteLine(row);
            row = String.Format("\"Kennfelderanzahl\"{0}{1}",
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                cfg.NumberOfUsedDatamaps);
            writer.WriteLine(row);

            // initial values
            for (int i = 0; i < cfg.InitValueGetNumber(); i++)
            {
                row = String.Format("\"Init.{0}\"{1}\"{2}\"", cfg.InitValueGetItemName(i),
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.InitValueGetItemValueString(i));
                writer.WriteLine(row);
            }

            // second can
            for (int i = 0; i < cfg.SecondCanGetNumber(); i++)
            {
                row = String.Format("\"{0}\"{1}\"{2}\"", cfg.SecondCanGetItemName(i),
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.SecondCanGetItemValueString(i));
                writer.WriteLine(row);
            }

            // tasks
            for (int j = 0; j < cfg.GetTaskNumber(); j++)
            {
                row = String.Format("\"TaskName Pos[{0}]\"{1}\"{2}\"", j,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.GetTaskIdentifier(j));
                writer.WriteLine(row);
                row = String.Format("\"TaskOffset Pos[{0}]\"{1}\"{2}\"", j,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.GetTaskOffset(j));
                writer.WriteLine(row);
                row = String.Format("\"TaskFehler 0 Pos[{0}]\"{1}\"{2}\"", j,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.GetTaskErrorString(j, 0));
                writer.WriteLine(row);
                row = String.Format("\"TaskFehler 1 Pos[{0}]\"{1}\"{2}\"", j,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.GetTaskErrorString(j, 1));
                writer.WriteLine(row);
                row = String.Format("\"TaskFehler 2 Pos[{0}]\"{1}\"{2}\"", j,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.GetTaskErrorString(j, 2));
                writer.WriteLine(row);
                row = String.Format("\"TaskFehler 3 Pos[{0}]\"{1}\"{2}\"", j,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.GetTaskErrorString(j, 3));
                writer.WriteLine(row);
                row = String.Format("\"TaskFehler 4 Pos[{0}]\"{1}\"{2}\"", j,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.GetTaskErrorString(j, 4));
                writer.WriteLine(row);
                row = String.Format("\"TaskFehler 5 Pos[{0}]\"{1}\"{2}\"", j,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.GetTaskErrorString(j, 5));
                writer.WriteLine(row);
                row = String.Format("\"TaskFehler 6 Pos[{0}]\"{1}\"{2}\"", j,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    cfg.GetTaskErrorString(j, 6));
                writer.WriteLine(row);
                for (int i = 0; i < cfg.GetTaskItemNumber(j); i++)
                {
                    row = String.Format("\"{0}.{1}\"{2}\"{3}\"", cfg.GetTaskIdentifier(j), cfg.GetTaskItemName(j, i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        cfg.GetTaskItemValueString(j, i));
                    writer.WriteLine(row);
                }
            }

            // data maps
            Int16[] start = new Int16[3];
            Int16[] stepsize = new Int16[3];
            UInt16[] steps = new UInt16[3];
            for (int i = 0; i < cfg.NumberOfUsedDatamaps; i++)
            {
                row = String.Format("\"DatamapIdentifier[{0}]\"{1}\"{2}\"", i,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    GetDatamapIdentifier(i).ToString());
                writer.WriteLine(row);
                row = String.Format("\"DatamapType[{0}]\"{1}\"{2}\"", i,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    ((HJS.ECU.Firmware.KennfeldTyp)GetDatamapType(i)).ToString());
                writer.WriteLine(row);
                row = String.Format("\"DatamapDimension[{0}]\"{1}\"{2}\"", i,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    (GetDatamapDimension(i) < 1) ? "1D" : "2D");
                writer.WriteLine(row);
                row = String.Format("\"DatamapOffset[{0}]\"{1}\"0x{2}\"", i,
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                    GetDatamapOffset(i).ToString("X"));
                writer.WriteLine(row);

                GetDatamapAxis(i, out start[0], out stepsize[0], out steps[0], out start[1],
                    out stepsize[1], out steps[1], out start[2], out stepsize[2], out steps[2]);

                if (GetDatamapDimension(i) > 0)
                {
                    //2D
                    for (UInt16 x = 0; x < steps[0] + 1; x++)
                    {
                        for (UInt16 y = 0; y < steps[1] + 1; y++)
                        {
                            row = String.Format("\"Datamap[{0}]Value[{1};{2}]\"{3}\"{4}\"",
                                i, (start[0] + (x * stepsize[0])).ToString(),
                                (start[1] + (y * stepsize[1])).ToString(),
                                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                                (GetDatamapValue(i, x, y)).ToString());
                            writer.WriteLine(row);
                        }
                    }
                }
                else
                {
                    //1D
                    for (UInt16 x = 0; x < steps[0] + 1; x++)
                    {
                        row = String.Format("\"Datamap[{0}]Value[{1}]\"{2}\"{3}\"",
                            i, (start[0] + (x * stepsize[0])).ToString(),
                            System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                            (GetDatamapValue(i, x, 0)).ToString());
                        writer.WriteLine(row);
                    }
                }
            }

            // languages
            HJS.ECU.Firmware fw = new HJS.ECU.Firmware(GetConfigCompatibility());
            for (int l = 0; l < GetUsedLanguages(); l++)
            {
                row = String.Format("\"Language[{0}]\"{2}\"{1}\"", l, GetLanguageId(l),
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
                writer.WriteLine(row);
                for (int i = 0; i < GetNumberOfUsedValues(l); i++)
                {
                    row = String.Format("\"ValueName[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        GetValueName(l, i));
                    writer.WriteLine(row);
                    row = String.Format("\"ValueDisplayed[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        IsValueDisplayed(l, i) ? "1" : "0");
                    writer.WriteLine(row);
                    row = String.Format("\"ValueHidden[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        IsValueHidden(l, i) ? "1" : "0");
                    writer.WriteLine(row);
                    row = String.Format("\"ValueGroup[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        IsValueGroup(l, i) ? "RW" : "MW");
                    writer.WriteLine(row);
                    row = String.Format("\"ValueSigned[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        IsValueSigned(l, i) ? "1" : "0");
                    writer.WriteLine(row);
                    row = String.Format("\"ValueDecimals[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        GetValueDecimals(l, i).ToString());
                    writer.WriteLine(row);
                    row = String.Format("\"ValueUnit[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        GetValueUnit(l, i, false));
                    writer.WriteLine(row);
                    row = String.Format("\"ValueFaktor[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        (GetValueFaktor(l, i) == 0) ? "-" : GetValueFaktor(l, i).ToString());
                    writer.WriteLine(row);
                    row = String.Format("\"ValueDivisor[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        (GetValueDivisor(l, i) == 1) ? "-" : GetValueDivisor(l, i).ToString());
                    writer.WriteLine(row);
                    row = String.Format("\"ValueOffset[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        GetValueOffset(l, i));
                    writer.WriteLine(row);
                    row = String.Format("\"ValueAltUnit[{0}]\"{1}\"{2}\"", fw.GetMessWertString(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        GetValueUnit(l, i, true));
                    writer.WriteLine(row);
                }
                for (int i = 0; i < GetNumberOfUsedErrors(l); i++)
                {
                    row = String.Format("\"EventName[{0}]\"{1}\"{2}\"", i.ToString(),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        GetErrorName(l, i));
                    writer.WriteLine(row);
                    row = String.Format("\"EventDisplayed[{0}]\"{1}\"{2}\"", i.ToString(),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        IsEventDisplayed(l, i) ? "1" : "0");
                    writer.WriteLine(row);
                    row = String.Format("\"EventHidden[{0}]\"{1}\"{2}\"", i.ToString(),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        IsEventHidden(l, i) ? "1" : "0");
                    writer.WriteLine(row);
                    row = String.Format("\"EventType[{0}]\"{1}\"{2}\"", i.ToString(),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        IsEventOrError(l, i) ? "Event" : "Error");
                    writer.WriteLine(row);
                    row = String.Format("\"EventBluLED[{0}]\"{1}\"{2}\"", i.ToString(),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        IsEventBlueLed(l, i) ? "1" : "0");
                    writer.WriteLine(row);
                }
                for (int i = 0; i < GetNumberOfUsedBehaves(l); i++)
                {
                    row = String.Format("\"Behave[{0}]\"{1}\"{2}\"", i.ToString(),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        GetBehaveName(l, i));
                    writer.WriteLine(row);
                }
            }

            // report
            if (HasReportBlock())
            {
                for (int i = 0; i < GetReportItemNumber(); i++)
                {
                    row = String.Format("\"Report[{0}]\"{1}{2}", GetReportItemName(i),
                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator,
                        GetReportItemValue(i));
                    writer.WriteLine(row);
                }
            }
            writer.Close();
            return true;
        }

        /// <summary>Open file of former compatibility and upgrade to newer compatibility</summary>
        /// <param name="Filename">Filename of old version</param>
        /// <param name="NewVersion">New Version</param>
        /// <returns></returns>
        public bool Upgrade(string Filename, byte NewVersion)
        {
            ReturnValue ret = ReturnValue.NoError;
            BlockFile bf = new BlockFile();
            Block bl;
            byte OldVersion = 255;
            ret = bf.Open(HJS.BlockFile.FileIdentifier.ParameterSet, Filename);
            if (ret != HJS.ReturnValue.NoError)
            {
                mLastError = String.Format("Could not open file! {0}", ret);
            }
            else
            {
                ret = bf.GetBlock(out bl, HJS.Block.BlockId.IdKonfig, false);
                if (ret != HJS.ReturnValue.NoError)
                {
                    mLastError = String.Format("Could not load cfg! {0}", ret);
                }
                else
                {
                    switch (NewVersion)
                    {
                        case 8:
                            ret = ReturnValue.VersionMismatch;
                            mLastError = "Upgrade to compatibility 8 not available!";
                            break;
                        case 9:
                            cfg = new HJS.ECU.Parameter.Configuration9_Block();
                            break;
                        case 10:
                            cfg = new HJS.ECU.Parameter.Configuration10_Block();
                            break;
                        default:
                            ret = ReturnValue.VersionMismatch;
                            mLastError = "No upgrade to unknown compatibilities!";
                            break;
                    }
                    if (ret == HJS.ReturnValue.NoError)
                    {
                        ret = cfg.Import(ref bl, true);
                        OldVersion = bl.Version;
                        if (ret != HJS.ReturnValue.NoError)
                        {
                            mLastError = String.Format("Could not parse cfg! {0}", ret);
                        }
                        //cfg.WriteRaw();

                        // author
                        ret = bf.GetBlock(out bl, HJS.Block.BlockId.IdAuthor, false);
                        if (ret == ReturnValue.BlockNotFound)
                        {
                            mAuthor = null;
                        }
                        else
                        {
                            mAuthor = new HJS.AuthorBlock(bl);
                        }

                        //kf
                        ret = bf.GetBlock(out bl, HJS.Block.BlockId.IdKennfld, false);
                        if (ret == HJS.ReturnValue.NoError)
                        {
                            kf = new HJS.ECU.Parameter.Datamap2_Block(bl);
                            kf.ConfigVersion = cfg.ConfigVersion;
                            kf.GenerateChecksum();

                            //lng
                            maLng = new HJS.ECU.LanguageBlock[0];
                            for (byte b = 30; b < 50; b++)
                            {
                                ret = bf.GetBlock(out bl, (HJS.Block.BlockId)b, false);
                                if (ret == HJS.ReturnValue.NoError)
                                {
                                    Array.Resize(ref maLng, maLng.Length + 1);
                                    maLng[maLng.Length - 1] = new HJS.ECU.LanguageBlock(bl);
                                    if ((OldVersion < 9) && (NewVersion > 8))
                                    {
                                        ret = maLng[maLng.Length - 1].Upgrade8to9();
                                        if (ret != ReturnValue.NoError)
                                        {
                                            if (ret == ReturnValue.SizeMismatch)
                                            {
                                                mLastError = String.Format("Size mismatch after upgrading language {0}! {1}\r\nReduce the size of strings!",
                                                    ((HJS.Block.BlockId)b).ToString(), ret);
                                                break;
                                            }
                                            else
                                            {
                                                mLastError = String.Format("Could not upgrade language {0}! {1}",
                                                    ((HJS.Block.BlockId)b).ToString(), ret);
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (ret == ReturnValue.BlockNotFound)
                                    {
                                        // ignore missing languages
                                        ret = ReturnValue.NoError;
                                    }
                                    else
                                    {
                                        mLastError = String.Format("Could not load language {0}! {1}",
                                            ((HJS.Block.BlockId)b).ToString(), ret);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            mLastError = String.Format("Could not load data map! {0}", ret);
                        }
                    }
                }
            }
            bf.Close();
            return (ret == HJS.ReturnValue.NoError);
        }

        #region cfg
        /// <summary>Get version string of configuration</summary>
        /// <returns>Version string of configuration</returns>
        public string GetConfigVersion()
        {
            return String.Format("{0}.{1}.{2}",
                cfg.ConfigVersion.Hauptversion,
                cfg.ConfigVersion.Nebenversion,
                cfg.ConfigVersion.Revision);
        }

        /// <summary>Set config and datamap version, if compatibility is matching</summary>
        /// <param name="newVersion">New version number</param>
        /// <returns>True on success</returns>
        public bool SetConfigVersion(HJS.Block.VersionT newVersion)
        {
            if (cfg.Version != newVersion.Revision) return false;
            cfg.ConfigVersion = newVersion;
            cfg.Parse();
            kf.ConfigVersion = newVersion;
            kf.GenerateByteArray();
            return true;
        }

        /// <summary>Get downward compatibility of configuration to software</summary>
        /// <returns>Downward compatibility of configuration to software</returns>
        public byte GetConfigDownwardVersion()
        {
            return cfg.AbwaertsVersion;
        }

        /// <summary>Get type of useable software</summary>
        /// <returns>Type of useable software</returns>
        public byte GetConfigSoftwareType()
        {
            return cfg.SoftwareType;
        }

        /// <summary>Get level of required password</summary>
        /// <returns>Level of required password</returns>
        public byte GetConfigPasswordLevel()
        {
            return cfg.PasswordLevel;
        }

        /// <summary>Set level of required password</summary>
        /// <param name="NewPasswordLevel">Level of required password</param>
        /// <returns>True on success</returns>
        public bool SetConfigPasswordLevel(byte NewPasswordLevel)
        {
            if (NewPasswordLevel > 3) return false;
            cfg.PasswordLevel = NewPasswordLevel;
            cfg.Parse();
            return true;
        }

        /// <summary>Get compatibility of configuration</summary>
        /// <returns>Compatibility of configuration</returns>
        public byte GetConfigCompatibility()
        {
            return cfg.Version;
        }

        /// <summary>Number of init value items</summary>
        /// <returns></returns>
        public int InitValueGetNumber()
        {
            return cfg.InitValueGetNumber();
        }

        /// <summary>Get name of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Name of init value item</returns>
        public string InitValueGetItemName(int position)
        {
            return cfg.InitValueGetItemName(position);
        }

        /// <summary>Get value string of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of init value item as string</returns>
        public string InitValueGetItemValueString(int position)
        {
            return cfg.InitValueGetItemValueString(position);
        }

        /// <summary>Get data type of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Data type of init value item</returns>
        public TaskDataType InitValueGetItemType(int position)
        {
            return cfg.InitValueGetItemType(position);
        }

        /// <summary>Get value of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of init value item as double</returns>
        public double InitValueGetItemValue(int position)
        {
            return cfg.InitValueGetItemValue(position);
        }

        /// <summary>Set value of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <param name="newValue">New value of init item</param>
        /// <returns>True on success</returns>
        public bool InitValueSetItemValue(int position, double newValue)
        {
            return cfg.InitValueSetItemValue(position, newValue);
        }

        /// <summary>Get index of enumeration value in array available by GetEnumerationArray()</summary>
        /// <param name="EnumValue">Value of enumeration item</param>
        /// <param name="tdtype">Task item type</param>
        /// <param name="EnumArray"></param>
        /// <returns>Index in enumeration value array. see GetEnumerationArray</returns>
        public int GetEnumerationIndex(double EnumValue, TaskDataType tdtype, Array EnumArray)
        {
            return cfg.GetEnumerationIndex(EnumValue, tdtype, EnumArray);
        }

        /// <summary>Get array of enumeration value names of task item type</summary>
        /// <param name="tdtype">Task item type</param>
        /// <returns>Array of values or null</returns>
        public Array GetEnumerationArray(TaskDataType tdtype)
        {
            return cfg.GetEnumerationArray(tdtype);
        }

        /// <summary>Get number of second can items</summary>
        /// <returns>Number of second can items</returns>
        public int SecondCanGetNumber()
        {
            return cfg.SecondCanGetNumber();
        }

        /// <summary>Get name of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Name of second can item</returns>
        public string SecondCanGetItemName(int position)
        {
            return cfg.SecondCanGetItemName(position);
        }

        /// <summary>Get value string of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of second can item as string</returns>
        public string SecondCanGetItemValueString(int position)
        {
            return cfg.SecondCanGetItemValueString(position);
        }

        /// <summary>Get data type of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Data type of second can item</returns>
        public TaskDataType SecondCanGetItemType(int position)
        {
            return cfg.SecondCanGetItemType(position);
        }

        /// <summary>Get value of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of second can item as double</returns>
        public double SecondCanGetItemValue(int position)
        {
            return cfg.SecondCanGetItemValue(position);
        }

        /// <summary>Set value of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <param name="newValue">New value of second can item</param>
        /// <returns>True on success</returns>
        public bool SecondCanSetItemValue(int position, double newValue)
        {
            return cfg.SecondCanSetItemValue(position, newValue);
        }

        /// <summary>Get used bytes of configuration</summary>
        /// <returns>Used bytes of configuration</returns>
        public UInt16 ConfigDataSize()
        {
            return (UInt16)(cfg.DataSize + HJS.Block.BLOCK_HEADER_SIZE);
        }

        /// <summary>Check if all baudrates parametered in configuration are set to same baudrate</summary>
        /// <returns>True if all baudrates are same, or no baudrate set.</returns>
        public bool CheckTaskBaudrates()
        {
            return cfg.CheckTaskBaudrates();
        }

        /// <summary>Check if all task header values are valid</summary>
        /// <param name="task">Output of last checked position of task</param>
        /// <returns></returns>
        public bool CheckTaskHeader(out int task)
        {
            return cfg.CheckTaskHeader(out task);
        }

        /// <summary>Generate configuration block checksum and repace sum in header</summary>
        public void GenerateChecksum()
        {
            cfg.GenerateChecksum();
        }

        /// <summary>Get number of used data maps</summary>
        /// <returns>Number of used data maps</returns>
        public byte GetUsedMaps()
        {
            return cfg.NumberOfUsedDatamaps;
        }

        /// <summary>Check task items versus palausbility</summary>
        /// <returns>Empty string on success, else message of first mismatch found</returns>
        public string CheckTaskItems()
        {
            return cfg.CheckTaskItems();
        }

        /// <summary>Check if all task stacks fit in stack array</summary>
        /// <returns>Empty string on success, else error text</returns>
        public string CheckTaskStackSizes()
        {
            return cfg.CheckTaskStackSizes();
        }

        /// <summary>Check if all task errors are palusible</summary>
        /// <returns>Empty string on success, else error text</returns>
        public string CheckTaskErrors()
        {
            return cfg.CheckTaskErrors();
        }
        #endregion

        #region cfg_tasks
        /// <summary>Get number of tasks</summary>
        /// <returns>Number of tasks</returns>
        public int GetTaskNumber()
        {
            return cfg.GetTaskNumber();
        }

        /// <summary>Get internal identifier of task</summary>
        /// <param name="position">Position of task in task vector table</param>
        /// <returns>Identifier of task</returns>
        public string GetTaskIdentifier(int position)
        {
            return cfg.GetTaskIdentifier(position).ToString();
        }

        /// <summary>Get value of identifier of task</summary>
        /// <param name="position">Position of task in task vector table</param>
        /// <returns>Value of identifier of task</returns>
        public UInt16 GetTaskIdentifierValue(int position)
        {
            return (UInt16)cfg.GetTaskIdentifier(position);
        }

        /// <summary>Get task offset</summary>
        /// <param name="position">Position of task in task vector table</param>
        /// <returns>Byte offset of task</returns>
        public UInt16 GetTaskOffset(int position)
        {
            return cfg.GetTaskOffset(position);
        }

        /// <summary>Get task errors</summary>
        /// <param name="posTask">Position of task in task vector table</param>
        /// <param name="posError">Position of error in task</param>
        /// <returns>String of seven task errors, including ring marker</returns>
        public string GetTaskErrorString(int posTask, int posError)
        {
            return cfg.GetTaskErrorString(posTask, posError);
        }

        /// <summary>Get error number</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <returns>Error number of task</returns>
        public byte GetTaskErrorNumber(int TaskPosition, int ErrorPosition)
        {
            return cfg.GetTaskErrorNumber(TaskPosition, ErrorPosition);
        }

        /// <summary>Set error number</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <param name="ErrorNumber">New error number</param>
        public void SetTaskErrorNumber(int TaskPosition, int ErrorPosition, byte ErrorNumber)
        {
            cfg.SetTaskErrorNumber(TaskPosition, ErrorPosition, ErrorNumber);
            cfg.Parse();
        }

        /// <summary>Get error ring flag</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <returns>Error ring flag of task</returns>
        public bool GetTaskErrorRingFlag(int TaskPosition, int ErrorPosition)
        {
            return cfg.GetTaskErrorRingFlag(TaskPosition, ErrorPosition);
        }

        /// <summary>Set error ring flag</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <param name="RingFlag">New ring flag</param>
        public void SetTaskErrorRingFlag(int TaskPosition, int ErrorPosition, bool RingFlag)
        {
            cfg.SetTaskErrorRingFlag(TaskPosition, ErrorPosition, RingFlag);
            cfg.Parse();
        }

        /// <summary>Get error reserved flag</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <returns>Error ring flag of task</returns>
        public bool GetTaskErrorResFlag(int TaskPosition, int ErrorPosition)
        {
            return cfg.GetTaskErrorResFlag(TaskPosition, ErrorPosition);
        }

        /// <summary>Set error reserved flag</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <param name="Flag">New ring flag</param>
        public void SetTaskErrorResFlag(int TaskPosition, int ErrorPosition, bool Flag)
        {
            cfg.SetTaskErrorResFlag(TaskPosition, ErrorPosition, Flag);
            cfg.Parse();
        }

        /// <summary>Get task item position by item name</summary>
        /// <param name="taskPosition">Position of task in vector table</param>
        /// <param name="itemName">Name of task item</param>
        /// <returns>Position in task item table, or -1 if not found</returns>
        public int GetTaskItemPosition(int taskPosition, string itemName)
        {
            return cfg.GetTaskItemPosition(taskPosition, itemName);
        }

        /// <summary>Get task configuration item number</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <returns>Number of items of task</returns>
        public int GetTaskItemNumber(int taskPosition)
        {
            return cfg.GetTaskItemNumber(taskPosition);
        }

        /// <summary>Get task item name</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <returns>Name of item</returns>
        public string GetTaskItemName(int taskPosition, int itemPosition)
        {
            return cfg.GetTaskItemName(taskPosition, itemPosition);
        }

        /// <summary>Get task item value string</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <returns>Name of item as string</returns>
        public string GetTaskItemValueString(int taskPosition, int itemPosition)
        {
            return cfg.GetTaskItemValueString(taskPosition, itemPosition);
        }

        /// <summary>Get task item value</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <returns>Value of item as double</returns>
        public double GetTaskItemValue(int taskPosition, int itemPosition)
        {
            return cfg.GetTaskItemValue(taskPosition, itemPosition);
        }

        /// <summary>Get task item data type</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <returns>Task item data type</returns>
        public TaskDataType GetTaskItemType(int taskPosition, int itemPosition)
        {
            return cfg.GetTaskItemType(taskPosition, itemPosition);
        }

        /// <summary>Set task item value</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <param name="NewValue">New value of task item</param>
        /// <returns>True on success</returns>
        public bool SetTaskItemValue(int taskPosition, int itemPosition, double NewValue)
        {
            return cfg.SetTaskItemValue(taskPosition, itemPosition, NewValue);
        }

        /// <summary>Check task starting sequence</summary>
        /// <param name="ResultString">String of result</param>
        /// <returns>True if check was successfully</returns>
        public bool CheckTaskSequence(out String ResultString)
        {
            //check first four tasks
            if ((cfg.GetTaskNumber() < 4)
                || (cfg.GetTaskIdentifier(0) != TaskIdentifier.taskIo)
                || (cfg.GetTaskIdentifier(1) != TaskIdentifier.taskPreDiagnose)
                || (cfg.GetTaskIdentifier(2) != TaskIdentifier.taskPostDiagnose)
                || (cfg.GetTaskIdentifier(3) != TaskIdentifier.taskKommunikation)
                )
            {
                ResultString = "Systemtasks falsch (ersten vier)";
                return false;
            }
            for (int i = 0; i < cfg.GetTaskNumber(); i++)
            {
                //find double tasks
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (GetTaskIdentifier(i) == GetTaskIdentifier(j))
                        {
                            ResultString = String.Format("Task doppelt gestartet ({0})", GetTaskIdentifier(i));
                            return false;
                        }
                    }
                }
                //find unknown task id
            }
            ResultString = "Reihenfolge plausibel";
            return true;
        }

        /// <summary>Check task starting offsets</summary>
        /// <param name="ResultString">String of result</param>
        /// <param name="UsedBytes">Number of used bytes</param>
        /// <returns>True if check was successfully</returns>
        public bool CheckTaskOffset(out String ResultString, out UInt32 UsedBytes)
        {
            return cfg.CheckTaskOffset(out ResultString, out UsedBytes);
        }

        /// <summary>Check task checksums</summary>
        /// <returns>On success: 255 = invalid task
        /// else the task identifier of first checksum mismatch</returns>
        public TaskIdentifier CheckTaskChecksums()
        {
            return cfg.CheckTaskChecksums(cfg.Version);
        }

        /// <summary>Get array of task identifiers</summary>
        /// <returns>Array of task identifiers</returns>
        public Array GetTaskIdentifierArray()
        {
            return Enum.GetValues(typeof(TaskIdentifier));
        }

        /// <summary>Add task to parameter set</summary>
        public bool AddTask(byte Id)
        {
            return cfg.AddTask(Id);
        }
        #endregion

        #region kf
        /// <summary>Get version string of configuration</summary>
        /// <returns>Version string of configuration</returns>
        public string GetDatamapVersion()
        {
            return String.Format("{0}.{1}.{2}", kf.ConfigVersion.Hauptversion, kf.ConfigVersion.Nebenversion, kf.ConfigVersion.Revision);
        }

        /// <summary>Get number of stored data maps</summary>
        /// <returns>Number of used data maps</returns>
        public byte GetStoredMaps()
        {
            return kf.NumberOfStoredDatamaps;
        }

        /// <summary>Get identifier of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Identifier of map, or -1 on error</returns>
        public UInt32 GetDatamapIdentifier(int position)
        {
            return kf.GetDatamapIdentifier(position);
        }

        /// <summary>Check data map ids in config and datamaps for missing, or unneccessary ids</summary>
        /// <param name="resultString">Message to certify return value</param>
        /// <returns>True on valid datamap identifiers</returns>
        public bool CheckDatamapIDs(out string resultString)
        {
            UInt32[] ids = cfg.GetDatamapIds();
            UInt32 i1 = 0;
            UInt32 i2 = 0;
            UInt32 i3 = 0;
            bool bRet = false;
            HJS.ECU.Firmware.KennfeldTyp[] types = cfg.GetDatamapTypes();
            for (int i = 0; i < kf.NumberOfStoredDatamaps; i++)
            {
                i1 = kf.GetDatamapIdentifier(i);
                bRet = false;
                for (int j = 0; j < ids.Length; j++)
                {
                    if (i1 == ids[j])
                    {
                        ids[j] = 0xFFFFFFFF; // dieses kf ist vorhanden
                        bRet = true;
                        if ((byte)types[j] != kf.GetDatamapType(i))
                        {
                            resultString = String.Format("Kennfeld Typ falsch: cfg={0} kf={1}",
                                types[j].ToString(), ((HJS.ECU.Firmware.KennfeldTyp)kf.GetDatamapType(i)).ToString());
                            return false;
                        }
                    }
                }
                if (bRet == false)
                {
                    i2 = i1; // kennfeld nicht in cfg
                }
            }
            i1 = 0;
            for (int j = 0; j < ids.Length; j++)
            {
                if (ids[j] != 0xFFFFFFFF) i1 = ids[j]; // kennfeld nicht in kf
            }
            if (kf.NumberOfStoredDatamaps > 1)
            {
                for (int k = 0; k < kf.NumberOfStoredDatamaps; k++)
                {
                    for (int l = 1; l < kf.NumberOfStoredDatamaps; l++)
                    {
                        if ((kf.GetDatamapIdentifier(k) == kf.GetDatamapIdentifier(l)) && (k != l))
                        {
                            i3 = kf.GetDatamapIdentifier(k);
                            bRet = false;
                        }
                    }
                }
            }
            if ((i1 == 0) && (i2 == 0) && (i3 == 0))
            {
                resultString = "Cfg enthält identische IDs wie KF";
                bRet = true;
            }
            else
            {
                if (i3 != 0)
                {
                    resultString = "Kennfeld doppelt: " + i3.ToString();
                    bRet = false;
                }
                else
                {
                    if (i1 != 0)
                    {
                        resultString = "Kennfeld fehlt: " + i1.ToString();
                        bRet = false;
                    }
                    else
                    {
                        resultString = "Kennfeld zu viel: " + i2.ToString();
                        bRet = false;
                    }
                }
            }
            if (bRet != false)
            {
                if (cfg.NumberOfUsedDatamaps != kf.NumberOfStoredDatamaps)
                {
                    resultString = "Kennfelder zu viel: " + kf.NumberOfStoredDatamaps + " > " + cfg.NumberOfUsedDatamaps;
                    bRet = false;
                }

            }
            return bRet;
        }

        /// <summary>Check data map version is same to config version</summary>
        /// <param name="resultString">Message to certify return value</param>
        /// <returns>True on valid datamap version</returns>
        public bool CheckDatamapVersion(out string resultString)
        {
            bool bRet = false;
            resultString = String.Format("Cfg={0}.{1}.{2} KF={3}.{4}.{5}",
                cfg.ConfigVersion.Hauptversion, cfg.ConfigVersion.Nebenversion, cfg.ConfigVersion.Revision,
                kf.ConfigVersion.Hauptversion, kf.ConfigVersion.Nebenversion, kf.ConfigVersion.Revision);
            if ((cfg.ConfigVersion.Hauptversion == kf.ConfigVersion.Hauptversion) &&
                (cfg.ConfigVersion.Nebenversion == kf.ConfigVersion.Nebenversion) &&
                (cfg.ConfigVersion.Revision == kf.ConfigVersion.Revision))
            {
                bRet = true;
            }
            else
            {
                bRet = false;
            }
            return bRet;
        }

        /// <summary>Get number of used bytes in block</summary>
        /// <param name="gaps">Number of gapping bytes</param>
        /// <param name="overlapp">Number of overlapping bytes</param>
        /// <returns>Number of totaly used bytes</returns>
        public UInt16 GetUsedSize(out UInt32 gaps, out UInt32 overlapp)
        {
            return kf.GetUsedSize(out gaps, out overlapp);
        }

        /// <summary>Get type of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Type of data map</returns>
        public byte GetDatamapType(int position)
        {
            return kf.GetDatamapType(position);
        }

        /// <summary>Get dimension of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Dimansion of data map</returns>
        public byte GetDatamapDimension(int position)
        {
            return kf.GetDimension(position);
        }

        /// <summary>Get offset of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Offset of data map</returns>
        public UInt32 GetDatamapOffset(int position)
        {
            return kf.GetOffset(position);
        }

        /// <summary>Get start values as string</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Start values as string</returns>
        public string GetDatamapStartString(int position)
        {
            return kf.GetStartString(position);
        }

        /// <summary>Get step sizes as string</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Step sizes as string</returns>
        public string GetDatamapStepsizeString(int position)
        {
            return kf.GetStepsizeString(position);
        }

        /// <summary>Get number of steps as string</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Number of steps as string</returns>
        public string GetDatamapStepsString(int position)
        {
            return kf.GetStepsString(position);
        }

        /// <summary>Get axis parameters</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="XStart">Start value of x-axis</param>
        /// <param name="XSize">Size of stes on x-axis</param>
        /// <param name="XSteps">Number of steps on x-axis</param>
        /// <param name="YStart">Start value of y-axis</param>
        /// <param name="YSize">Size of stes on y-axis</param>
        /// <param name="YSteps">Number of steps on y-axis</param>
        /// <param name="ZStart">Start value of z-axis</param>
        /// <param name="ZSize">Size of stes on z-axis</param>
        /// <param name="ZSteps">Number of steps on z-axis</param>
        public void GetDatamapAxis(int position,
            out Int16 XStart, out Int16 XSize, out UInt16 XSteps,
            out Int16 YStart, out Int16 YSize, out UInt16 YSteps,
            out Int16 ZStart, out Int16 ZSize, out UInt16 ZSteps)
        {
            kf.GetAxis(position, out XStart, out XSize, out XSteps, out YStart,
                out YSize, out YSteps, out ZStart, out ZSize, out ZSteps);
        }

        /// <summary>Get value of data map cell</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="x">Position on x-axis</param>
        /// <param name="y">Position on y-axis</param>
        /// <returns>Value of cell, or missing on error (65533)</returns>
        public UInt16 GetDatamapValue(int position, UInt16 x, UInt16 y)
        {
            return kf.GetDatamapValue(position, x, y);
        }

        /// <summary>Set value of data map cell</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="x">Position on x-axis</param>
        /// <param name="y">Position on y-axis</param>
        /// <param name="Content">New content of data map cell</param>
        /// <returns>True on success</returns>
        public bool SetDatamapValue(int position, UInt16 x, UInt16 y, UInt16 Content)
        {
            bool ret = false;
            ret = kf.SetDatamapValue(position, x, y, Content);
            if (ret) kf.GenerateChecksum();
            return ret;
        }

        /// <summary>Set data map ID an type</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="Id">New Identifier</param>
        /// <param name="Type">New type</param>
        /// <param name="Dimension">New dimension</param>
        public void SetDatamapId(int position, UInt32 Id, byte Type, byte Dimension)
        {
            kf.SetDatamapIdentifier(position, Id);
            kf.SetDatamapType(position, Type);
            kf.SetDimension(position, Dimension);
            kf.GenerateByteArray();
        }

        /// <summary>Set axis parameters</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="XStart">Start value of x-axis</param>
        /// <param name="XSize">Size of stes on x-axis</param>
        /// <param name="XSteps">Number of steps on x-axis</param>
        /// <param name="YStart">Start value of y-axis</param>
        /// <param name="YSize">Size of stes on y-axis</param>
        /// <param name="YSteps">Number of steps on y-axis</param>
        /// <param name="ZStart">Start value of z-axis</param>
        /// <param name="ZSize">Size of stes on z-axis</param>
        /// <param name="ZSteps">Number of steps on z-axis</param>
        public void SetDatamapAxis(int position,
            Int16 XStart, Int16 XSize, UInt16 XSteps,
            Int16 YStart, Int16 YSize, UInt16 YSteps,
            Int16 ZStart, Int16 ZSize, UInt16 ZSteps)
        {
            kf.SetAxis(position, XStart, XSize, XSteps, YStart,
                YSize, YSteps, ZStart, ZSize, ZSteps);
            kf.GenerateByteArray();
        }

        /// <summary>Add an empty data map to parameter set</summary>
        public void AddDataMap()
        {
            kf.AddMap();
        }
        #endregion

        #region lng
        /// <summary>Get number of used languages</summary>
        /// <returns>Number of used languages</returns>
        public int GetUsedLanguages()
        {
            return maLng.Length;
        }

        /// <summary>Get number of used bytes of languages</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <returns>Number of used bytes of languages</returns>
        public UInt16 GetUsedLanguagesBytes(int laguagePosition)
        {
            return maLng[laguagePosition].getParsedBytes();
        }

        /// <summary>Get language block identifier as string</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <returns>String of language enumeration</returns>
        public string GetLanguageId(int laguagePosition)
        {
            return maLng[laguagePosition].Type.ToString();
        }

        /// <summary>Get number of used values</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <returns>Number of used values</returns>
        public UInt16 GetNumberOfUsedValues(int laguagePosition)
        {
            return maLng[laguagePosition].getNumberOfUsedValues();
        }

        /// <summary>Get number of used errors</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <returns>Number of used errord</returns>
        public UInt16 GetNumberOfUsedErrors(int laguagePosition)
        {
            return maLng[laguagePosition].getNumberOfUsedErrors();
        }

        /// <summary>Get number of used behaves</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <returns>Number of used behaves</returns>
        public UInt16 GetNumberOfUsedBehaves(int laguagePosition)
        {
            return maLng[laguagePosition].getNumberOfUsedBehaves();
        }

        /// <summary>Get name of error</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <returns>Name of error</returns>
        public string GetErrorName(int laguagePosition, int errorPosition)
        {
            return maLng[laguagePosition].GetErrorName((UInt16)errorPosition);
        }

        /// <summary>Get name of behave</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="behavePosition">Position of behave</param>
        /// <returns>Name of behave</returns>
        public string GetBehaveName(int laguagePosition, int behavePosition)
        {
            return maLng[laguagePosition].GetBehaveName((UInt16)behavePosition);
        }

        /// <summary>Get Value name depending on position in value table</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>String of value name, empty string on error</returns>
        public string GetValueName(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].GetValueName((UInt16)valuePosition);
        }

        /// <summary>Get display flag for value</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>true if value is displayed, if position is greater than value array, the flag is set to false</returns>
        public bool IsValueDisplayed(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].IsValueDisplayed((UInt16)valuePosition);
        }

        /// <summary>Get hidden flag for value</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>true if value is hidden, if position is greater than value array, the flag is set to false</returns>
        public bool IsValueHidden(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].IsValueHidden((UInt16)valuePosition);
        }

        /// <summary>Get group flag for value
        /// (0 = measured value, 1=calculated value)</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>False = measured value, true = calculated value</returns>
        public bool IsValueGroup(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].IsValueGroup((UInt16)valuePosition);
        }

        /// <summary>Get signed flag for value</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>true if value is signed, if position is greater than value array, the flag is set to false</returns>
        public bool IsValueSigned(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].IsValueSigned((UInt16)valuePosition);
        }

        /// <summary>Get hex flag for value</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>true if value is hexadecimal, if position is greater than value array, the flag is set to false</returns>
        public bool IsValueHexadecimal(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].IsValueHexadecimal((UInt16)valuePosition);
        }

        /// <summary>Get number of decimal</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>Number of decimal</returns>
        public byte GetValueDecimals(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].GetValueDecimals((UInt16)valuePosition);
        }

        /// <summary>Get Value unit depending on position in value table</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="altUnit">Flag if alternative unit must be used</param>
        /// <returns>String of value unit, empty string on error</returns>
        public string GetValueUnit(int laguagePosition, int valuePosition, bool altUnit)
        {
            return maLng[laguagePosition].GetValueUnit((UInt16)valuePosition, altUnit);
        }

        /// <summary>Get Factor of value</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>Factor of value</returns>
        public byte GetValueFaktor(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].GetValueFaktor((UInt16)valuePosition);
        }

        /// <summary>Get Divisor of value</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>Divisor of value</returns>
        public byte GetValueDivisor(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].GetValueDivisor((UInt16)valuePosition);
        }

        /// <summary>Get Offset of value</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <returns>Offset of value</returns>
        public string GetValueOffset(int laguagePosition, int valuePosition)
        {
            return maLng[laguagePosition].GetValueOffset((UInt16)valuePosition);
        }

        /// <summary>Get flag if event is shown on display</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <returns>True if event is shown on display</returns>
        public bool IsEventDisplayed(int laguagePosition, int errorPosition)
        {
            return maLng[laguagePosition].IsEventDisplayed((UInt16)errorPosition);
        }

        /// <summary>Get flag if event is hidden</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <returns>True if event is hidden</returns>
        public bool IsEventHidden(int laguagePosition, int errorPosition)
        {
            return maLng[laguagePosition].IsEventHidden((UInt16)errorPosition);
        }

        /// <summary>Get flag if error is only an event</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <returns>True if event only</returns>
        public bool IsEventOrError(int laguagePosition, int errorPosition)
        {
            return maLng[laguagePosition].IsEventOrError((UInt16)errorPosition);
        }

        /// <summary>Get flag if event ligts blue led on displays</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <returns>True if event ligts blue led on displays</returns>
        public bool IsEventBlueLed(int laguagePosition, int errorPosition)
        {
            return maLng[laguagePosition].IsEventBlueLed((UInt16)errorPosition);
        }

        /// <summary>Set value name</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueName">New value name</param>
        /// <returns>True on success</returns>
        public bool SetValueName(int laguagePosition, int valuePosition, string valueName)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueName((UInt16)valuePosition, valueName))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value unit</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueUnit">New value unit</param>
        /// <returns>True on success</returns>
        public bool SetValueUnit(int laguagePosition, int valuePosition, string valueUnit)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueUnit((UInt16)valuePosition, valueUnit))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value factor</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueFactor">New value factor</param>
        /// <returns>True on success</returns>
        public bool SetValueFactor(int laguagePosition, int valuePosition, byte valueFactor)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueFactor((UInt16)valuePosition, valueFactor))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value divisor</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueDivisor">New value divisor</param>
        /// <returns>True on success</returns>
        public bool SetValueDivisor(int laguagePosition, int valuePosition, byte valueDivisor)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueDivisor((UInt16)valuePosition, valueDivisor))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value Offset</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueOffset">New value Offset</param>
        /// <returns>True on success</returns>
        public bool SetValueOffset(int laguagePosition, int valuePosition, string valueOffset)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueOffset((UInt16)valuePosition, valueOffset))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value alternative unit</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueAltUnit">New alterantive unit</param>
        /// <returns>True on success</returns>
        public bool SetValueAltUnit(int laguagePosition, int valuePosition, string valueAltUnit)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueAltUnit((UInt16)valuePosition, valueAltUnit))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value display flag</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueDisplay">New value display flag</param>
        /// <returns>True on success</returns>
        public bool SetValueDisplayed(int laguagePosition, int valuePosition, bool valueDisplay)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueHiddenInDisplay((UInt16)valuePosition, valueDisplay))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value hidden flag</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueHidden">New value hidden flag</param>
        /// <returns>True on success</returns>
        public bool SetValueHidden(int laguagePosition, int valuePosition, bool valueHidden)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueHidden((UInt16)valuePosition, valueHidden))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value group flag</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueCalculated">Flag if value is in calculated group</param>
        /// <returns>True on success</returns>
        public bool SetValueGroup(int laguagePosition, int valuePosition, bool valueCalculated)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueGroup((UInt16)valuePosition, valueCalculated))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value signed flag</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueSigned">New value signed flag</param>
        /// <returns>True on success</returns>
        public bool SetValueSigned(int laguagePosition, int valuePosition, bool valueSigned)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueSigned((UInt16)valuePosition, valueSigned))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

       /// <summary>Set value hex flag</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="valueHex">New value hex flag</param>
        /// <returns>True on success</returns>
        public bool SetValueHex(int laguagePosition, int valuePosition, bool valueHex)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueHex((UInt16)valuePosition, valueHex))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set value number of decimals</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="valuePosition">Position of value</param>
        /// <param name="dezStellen">New number of values deciamls</param>
        /// <returns>True on success</returns>
        public bool SetValueDecimals(int laguagePosition, int valuePosition, int dezStellen)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetValueDecimals((UInt16)valuePosition, (byte)dezStellen))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set error displayed flag</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <param name="errorDisplay">Flag if error is shown on displays</param>
        /// <returns>True on success</returns>
        public bool SetEventDisplayed(int laguagePosition, int errorPosition, bool errorDisplay)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetErrorLcdShow((UInt16)errorPosition, errorDisplay))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set error or event flag</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <param name="errorEvent">Error or event flag</param>
        /// <returns>True on success</returns>
        public bool SetEventOrError(int laguagePosition, int errorPosition, bool errorEvent)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetErrorEvent((UInt16)errorPosition, errorEvent))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set hidden flag of error</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <param name="errorHidden">Hidden flag of error</param>
        /// <returns>True on success</returns>
        public bool SetEventHidden(int laguagePosition, int errorPosition, bool errorHidden)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetErrorHidden((UInt16)errorPosition, errorHidden))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set blue LED flag of error</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <param name="errorBlueLed">Blue LED flag</param>
        /// <returns>True on success</returns>
        public bool SetEventBlueLed(int laguagePosition, int errorPosition, bool errorBlueLed)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetErrorBlue((UInt16)errorPosition, errorBlueLed))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set name of error</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="errorPosition">Position of error</param>
        /// <param name="errorName">Name of error</param>
        /// <returns>True on success</returns>
        public bool SetErrorName(int laguagePosition, int errorPosition, string errorName)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetErrorName((UInt16)errorPosition, errorName))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Set name of behave</summary>
        /// <param name="laguagePosition">Position in block file</param>
        /// <param name="behavePosition">Position of behave</param>
        /// <param name="behaveName">Behave name</param>
        /// <returns>True on success</returns>
        public bool SetBehaveName(int laguagePosition, int behavePosition, string behaveName)
        {
            bool ret = false;
            if (maLng[laguagePosition].SetBehaveName((UInt16)behavePosition, behaveName))
            {
                if (maLng[laguagePosition].PreCheckSize() <= LanguageBlock.MAX_BLOCK_SIZE)
                {
                    maLng[laguagePosition].ConvertBuffer();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Check languages by comparing value and event flags</summary>
        /// <returns>Empty string on success, else message of first mismatch found</returns>
        public string CheckLanguageFlags()
        {
            string ret = "";
            Firmware fw = new Firmware(cfg.Version);
            // Vergleich der messwert flags
            if (maLng.Length < 2) return "less than 2 languages";
            for (UInt16 valPos = 0; valPos < maLng[0].getNumberOfUsedValues(); valPos++)
            {
                for (int lngPos = 1; lngPos < maLng.Length; lngPos++)
                {
                    if ((maLng[lngPos].IsValueHidden(valPos))
                        && (maLng[lngPos].IsValueDisplayed(valPos))
                        && (String.Equals("-", maLng[lngPos].GetValueName(valPos))))
                    {
                        // Ingore flags of hidden and unused values
                        break;
                    }
                    if (maLng[lngPos].IsValueDisplayed(valPos) != maLng[0].IsValueDisplayed(valPos))
                    {
                        ret = String.Format("LCD {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                        break;
                    }
                    if (maLng[lngPos].IsValueHidden(valPos) != maLng[0].IsValueHidden(valPos))
                    {
                        ret = String.Format("Hidden {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                        break;
                    }
                    if (maLng[lngPos].IsValueGroup(valPos) != maLng[0].IsValueGroup(valPos))
                    {
                        ret = String.Format("M/RW {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                        break;
                    }
                    if (maLng[lngPos].IsValueSigned(valPos) != maLng[0].IsValueSigned(valPos))
                    {
                        ret = String.Format("Signed {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                        break;
                    }
                    if (maLng[lngPos].GetValueDecimals(valPos) != maLng[0].GetValueDecimals(valPos))
                    {
                        ret = String.Format("No. decimals {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                        break;
                    }
                }
                if (!String.IsNullOrEmpty(ret)) return ret;
            }
            // Vergleich der fehler / Event flags
            for (UInt16 errPos = 0; errPos < 64; errPos++)
            {
                for (int lngPos = 1; lngPos < maLng.Length; lngPos++)
                {
                    if ((maLng[lngPos].IsEventHidden(errPos))
                        && (String.Equals("-", maLng[lngPos].GetErrorName(errPos))))
                    {
                        // Ingore flags of hidden and unused values
                        break;
                    }
                    if (maLng[lngPos].IsEventDisplayed(errPos) != maLng[0].IsEventDisplayed(errPos))
                    {
                        ret = String.Format("LCD {0}.Event[{1}]", maLng[lngPos].Type.ToString(), errPos);
                        break;
                    }
                    if (maLng[lngPos].IsEventHidden(errPos) != maLng[0].IsEventHidden(errPos))
                    {
                        ret = String.Format("hidden {0}.Event[{1}]", maLng[lngPos].Type.ToString(), errPos);
                        break;
                    }
                    if (maLng[lngPos].IsEventOrError(errPos) != maLng[0].IsEventOrError(errPos))
                    {
                        ret = String.Format("EventError {0}.Event[{1}]", maLng[lngPos].Type.ToString(), errPos);
                        break;
                    }
                    if (maLng[lngPos].IsEventBlueLed(errPos) != maLng[0].IsEventBlueLed(errPos))
                    {
                        ret = String.Format("Blue LED {0}.Event[{1}]", maLng[lngPos].Type.ToString(), errPos);
                        break;
                    }
                }
                if (!String.IsNullOrEmpty(ret)) break;
            }
            return ret;
        }

        /// <summary>Check languages by comparing value signed flags</summary>
        /// <returns>Empty string on success, else message of first mismatch found</returns>
        public string CheckLanguageSignedFlags()
        {
            string ret = "";
            Firmware fw = new Firmware(cfg.Version);
            // Vergleich der Messwerte Vorzeichen
            for (UInt16 valPos = 0; valPos < maLng[0].getNumberOfUsedValues(); valPos++)
            {
                for (int lngPos = 0; lngPos < maLng.Length; lngPos++)
                {
                    if ((maLng[lngPos].IsValueHidden(valPos))
                        && (maLng[lngPos].IsValueDisplayed(valPos))
                        && (String.Equals("-", maLng[lngPos].GetValueName(valPos))))
                    {
                        // Ingore flags of hidden and unused values
                        break;
                    }
                    if ((fw.GetMessWertString(valPos).StartsWith("MRW_S"))
                        && (maLng[lngPos].IsValueSigned(valPos) == false))
                    {
                        ret = String.Format("signed {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                        break;
                    }
                    if ((fw.GetMessWertString(valPos).StartsWith("MRW_U"))
                        && (maLng[lngPos].IsValueSigned(valPos) != false))
                    {
                        ret = String.Format("unsigned {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                        break;
                    }
                }
                if (!String.IsNullOrEmpty(ret)) return ret;
            }
            // Pruefung der Flags
            byte flag;
            for (UInt16 valPos = 0; valPos < maLng[0].getNumberOfUsedValues(); valPos++)
            {
                for (int lngPos = 0; lngPos < maLng.Length; lngPos++)
                {
                    if ((maLng[lngPos].IsValueHidden(valPos))
                        && (maLng[lngPos].IsValueDisplayed(valPos))
                        && (String.Equals("-", maLng[lngPos].GetValueName(valPos))))
                    {
                        // Ingore flags of hidden and unused values
                        break;
                    }
                    flag = 64;
                    if (maLng[lngPos].IsValueSigned(valPos)) { flag += 8; }
                    if (maLng[lngPos].IsValueGroup(valPos)) { flag += 16; }
                    if (maLng[lngPos].IsValueHexadecimal(valPos))
                    {
                        flag += 7;
                    }
                    else
                    {
                        flag += maLng[lngPos].GetValueDecimals(valPos);
                    }
                    switch (cfg.Version)
                    {
                        case 8:
                            Firmware.MessWert8 id8 = fw.GetValueIdentifier8(valPos);
                            if (!Firmware.CheckMesswertFlag(id8, flag))
                                ret = String.Format("Flag {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                            break;
                        case 9:
                            Firmware.MessWert9 id9 = fw.GetValueIdentifier9(valPos);
                            if (!Firmware.CheckMesswertFlag(id9, flag))
                                ret = String.Format("Flag {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                            break;
                        case 10:
                            Firmware.MessWert10 id10 = fw.GetValueIdentifier10(valPos);
                            if (!Firmware.CheckMesswertFlag(id10, flag))
                                ret = String.Format("Flag {0}.{1}",
                            maLng[lngPos].Type.ToString(), fw.GetMessWertString(valPos));
                            break;
                    }
                    if (!String.IsNullOrEmpty(ret)) break;
                }
                if (!String.IsNullOrEmpty(ret)) break;
            }
            return ret;
        }
        #endregion

        #region author
        /// <summary>Flag if parameter set includes an author block</summary>
        /// <returns>True, if author block is included</returns>
        public bool HasAuthorBlock()
        {
            return (mAuthor != null);
        }
        /// <summary>Get time stamp of file creation</summary>
        /// <returns>Time stamp of file creation</returns>
        public DateTime GetAuthorCreationTime()
        {
            DateTime crt = new DateTime();
            crt = DateTime.FromFileTime(mAuthor.CreationTime);
            return crt;
        }

        /// <summary>Get computer name of author</summary>
        /// <returns>Computer name of author</returns>
        public string GetAuthorComputername()
        {
            return mAuthor.Computername;
        }

        /// <summary>Get user name of author</summary>
        /// <returns>User name of author</returns>
        public string GetAuthorUsername()
        {
            return mAuthor.Username;
        }
        #endregion

        #region report
        /// <summary>Flag if parameter set includes an report block</summary>
        /// <returns>True, if report block is included</returns>
        public bool HasReportBlock()
        {
            return (mReport != null);
        }

        /// <summary>Get number of report items</summary>
        /// <returns>Number of report items</returns>
        public int GetReportItemNumber()
        {
            return mReport.GetItemNumber();
        }

        /// <summary>Get name of report item</summary>
        /// <param name="Position">Position of report item</param>
        /// <returns>Name of report item</returns>
        public string GetReportItemName(int Position)
        {
            return mReport.GetItemName(Position);
        }

        /// <summary>Get value of report item</summary>
        /// <param name="Position">Position of report item</param>
        /// <returns>Value of report item</returns>
        public string GetReportItemValue(int Position)
        {
            return mReport.GetItemValue(Position);
        }
        #endregion
    }
}
