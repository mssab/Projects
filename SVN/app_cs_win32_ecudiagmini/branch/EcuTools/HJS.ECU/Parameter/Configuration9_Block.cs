/*
 * Object: HJS.ECU.Parameter.Configuration9_Block
 * Description: Configuration parameter block compatibility 9
 * 
 * $LastChangedDate: 2016-06-07 14:15:37 +0200 (Di, 07 Jun 2016) $
 * $LastChangedRevision: 103 $
 * $LastChangedBy: wtr $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/Configuration9_Block.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU.Parameter
{
    /// <summary>Configuration parameter block compatibility 9 class</summary>
    public partial class Configuration9_Block : ConfigurationBlock
    {
        /// <summary>Default constructor</summary>
        public Configuration9_Block()
        {
            Version = 9;
            DataSize = 4090;
        }

        /// <summary>Import from base block</summary>
        /// <param name="Source">Source block</param>
        /// <param name="KeepVersion">Flag if Target version should remain</param>
        /// <returns></returns>
        public override ReturnValue Import(ref Block Source, bool KeepVersion)
        {
            ReturnValue _ret = ReturnValue.NoError;
            byte b = 0;
            //Import block header
            if (Source.Type != Type)
            {
                _ret = ReturnValue.BlockNotFound;
            }
            else
            {
                if (KeepVersion)
                {
                    if (Source.Version != Version)
                    {
                        _ret = Upgrade(Source);
                    }
                    else
                    {
                        Source.GetData(out mBlockData);
                        if (DataSize != (UInt16)mBlockData.Length)
                        {
                            _ret = ReturnValue.SizeMismatch;
                        }
                        mChecksum = Source.Checksum;
                    }
                }
                else
                {
                    Version = Source.Version;
                    Source.GetData(out mBlockData);
                    if (DataSize != (UInt16)mBlockData.Length)
                    {
                        _ret = ReturnValue.SizeMismatch;
                    }
                }
            }
            if (_ret == ReturnValue.NoError && mBlockData != null)
            {
                //Import configuration header
                mTaskAnzahl = mBlockData[0];
                NumberOfUsedDatamaps = mBlockData[1];
                VersionT ver;
                ver.Hauptversion = (UInt16)(mBlockData[2] + (mBlockData[3] * 256));
                ver.Nebenversion = mBlockData[4];
                ver.Revision = mBlockData[5];
                ConfigVersion = ver;
                AbwaertsVersion = mBlockData[6];
                SoftwareType = mBlockData[7];
                PasswordLevel = mBlockData[8];
                //Import task arrays
                TaskVector[] tvt = new TaskVector[25];
                TaskError[,] tet = new TaskError[25,7];
                for (int i = 0; i < 25; i++)
                {
                    tvt[i].Tasknummer = (TaskIdentifier)mBlockData[9 + (i * 3)];
                    tvt[i].Offset = BitConverter.ToUInt16(mBlockData, 10 + (i * 3));
                    b = mBlockData[84 + (i * 7)];
                    tet[i, 0].ErrNo = (byte)(b / 4);
                    tet[i, 0].Ring = (b & 0x01) > 0;
                    tet[i, 0].reserve = (b & 0x02) > 1;
                    b = mBlockData[85 + (i * 7)];
                    tet[i, 1].ErrNo = (byte)(b / 4);
                    tet[i, 1].Ring = (b & 0x01) > 0;
                    tet[i, 1].reserve = (b & 0x02) > 1;
                    b = mBlockData[86 + (i * 7)];
                    tet[i, 2].ErrNo = (byte)(b / 4);
                    tet[i, 2].Ring = (b & 0x01) > 0;
                    tet[i, 2].reserve = (b & 0x02) > 1;
                    b = mBlockData[87 + (i * 7)];
                    tet[i, 3].ErrNo = (byte)(b / 4);
                    tet[i, 3].Ring = (b & 0x01) > 0;
                    tet[i, 3].reserve = (b & 0x02) > 1;
                    b = mBlockData[88 + (i * 7)];
                    tet[i, 4].ErrNo = (byte)(b / 4);
                    tet[i, 4].Ring = (b & 0x01) > 0;
                    tet[i, 4].reserve = (b & 0x02) > 1;
                    b = mBlockData[89 + (i * 7)];
                    tet[i, 5].ErrNo = (byte)(b / 4);
                    tet[i, 5].Ring = (b & 0x01) > 0;
                    tet[i, 5].reserve = (b & 0x02) > 1;
                    b = mBlockData[90 + (i * 7)];
                    tet[i, 6].ErrNo = (byte)(b / 4);
                    tet[i, 6].Ring = (b & 0x01) > 0;
                    tet[i, 6].reserve = (b & 0x02) > 1;
                }
                SetTaskVectorTable(tvt);
                SetTaskErrorTable(tet);
                //Import initial values
                InitValueImport();
                //Import second can
                SecondCanImport();

                //Import tasks
                maTask = new TaskConfiguration[mTaskAnzahl];
                for (int i = 0; i < mTaskAnzahl; i++)
                {
                    maTask[i] = new TaskConfiguration(GetTaskIdentifier(i));
                    if (!maTask[i].Import9(GetTaskOffset(i), ref mBlockData))
                    {
                        _ret = ReturnValue.SizeMismatch;
                    }
                }
            }
            return _ret;
        }

        /// <summary>Update byte array
        /// Data from header strucutres, except dynamic structures are parsed back into the byte array</summary>
        public override void Parse()
        {
            // Configuration header
            mBlockData[0] = mTaskAnzahl;
            mBlockData[1] = NumberOfUsedDatamaps;
            mBlockData[2] = (byte)(ConfigVersion.Hauptversion % 256);
            mBlockData[3] = (byte)(ConfigVersion.Hauptversion / 256);
            mBlockData[4] = ConfigVersion.Nebenversion;
            mBlockData[5] = ConfigVersion.Revision;
            mBlockData[6] = AbwaertsVersion;
            mBlockData[7] = SoftwareType;
            mBlockData[8] = PasswordLevel;

            // Task arrays
            TaskVector[] tvt = new TaskVector[25];
            TaskError[,] tet = new TaskError[25,7];
            for (int i = 0; i < 25; i++)
            {
                mBlockData[9 + (i * 3)] = (byte)(GetTaskIdentifier(i));
                mBlockData[10 + (i * 3)] = (byte)(GetTaskOffset(i) % 256);
                mBlockData[11 + (i * 3)] = (byte)(GetTaskOffset(i) / 256);
                mBlockData[84 + (i * 7)] = (byte)((GetTaskErrorNumber(i, 0) * 4) + (GetTaskErrorRingFlag(i, 0) ? 1 : 0) + (GetTaskErrorResFlag(i, 0) ? 2 : 0));
                mBlockData[85 + (i * 7)] = (byte)((GetTaskErrorNumber(i, 1) * 4) + (GetTaskErrorRingFlag(i, 1) ? 1 : 0) + (GetTaskErrorResFlag(i, 1) ? 2 : 0));
                mBlockData[86 + (i * 7)] = (byte)((GetTaskErrorNumber(i, 2) * 4) + (GetTaskErrorRingFlag(i, 2) ? 1 : 0) + (GetTaskErrorResFlag(i, 2) ? 2 : 0));
                mBlockData[87 + (i * 7)] = (byte)((GetTaskErrorNumber(i, 3) * 4) + (GetTaskErrorRingFlag(i, 3) ? 1 : 0) + (GetTaskErrorResFlag(i, 3) ? 2 : 0));
                mBlockData[88 + (i * 7)] = (byte)((GetTaskErrorNumber(i, 4) * 4) + (GetTaskErrorRingFlag(i, 4) ? 1 : 0) + (GetTaskErrorResFlag(i, 4) ? 2 : 0));
                mBlockData[89 + (i * 7)] = (byte)((GetTaskErrorNumber(i, 5) * 4) + (GetTaskErrorRingFlag(i, 5) ? 1 : 0) + (GetTaskErrorResFlag(i, 5) ? 2 : 0));
                mBlockData[90 + (i * 7)] = (byte)((GetTaskErrorNumber(i, 6) * 4) + (GetTaskErrorRingFlag(i, 6) ? 1 : 0) + (GetTaskErrorResFlag(i, 6) ? 2 : 0));
            }
            GenerateChecksum();
        }


        /// <summary>Get fixed task offset</summary>
        /// <param name="task">Identifier of task</param>
        /// <returns>Offset according HSL block structure</returns>
        public override UInt16 GetFixedOffset(TaskIdentifier task)
        {
            switch (task)
            {
                case TaskIdentifier.taskIo:
                    return 0x02B6;
                case TaskIdentifier.taskPreDiagnose:
                    return 0x0313;
                case TaskIdentifier.taskPostDiagnose:
                    return 0x032F;
                case TaskIdentifier.taskKommunikation:
                    return 0x04A8;
                case TaskIdentifier.taskCanCom:
                    return 0x04C4;
                case TaskIdentifier.taskDosieren:
                    return 0x0573;
                case TaskIdentifier.taskTurnspeed:
                    return 0x05B0;
                case TaskIdentifier.taskTankgeber:
                    return 0x05D4;
                case TaskIdentifier.taskAgr:
                    return 0x0600;
                case TaskIdentifier.taskCanIn:
                    return 0x0634;
                case TaskIdentifier.taskHeizen:
                    return 0x0738;
                case TaskIdentifier.taskAcquisition:
                    return 0x0762;
                case TaskIdentifier.taskBeladMittel:
                    return 0x0793;
                case TaskIdentifier.taskRegenerieren:
                    return 0x07D5;
                case TaskIdentifier.taskAdditivierung:
                    return 0x080D;
                case TaskIdentifier.taskVertWatch:
                    return 0x0846;
                case TaskIdentifier.taskBeladPro:
                    return 0x0824;
                case TaskIdentifier.taskDrivePattern:
                    return 0x089B;
                case TaskIdentifier.taskSaeComm:
                    return 0x08B8;
                case TaskIdentifier.taskAplSae:
                    return 0x08D8;
                case TaskIdentifier.taskBeladCRT:
                    return 0x08F4;
                case TaskIdentifier.taskIcDosing:
                    return 0x093B;
                case TaskIdentifier.taskBeladLuftmasse:
                    return 0x0979;
                case TaskIdentifier.taskBeladKennfeld:
                    return 0x09AB;
                case TaskIdentifier.taskMassAirFlow:
                    return 0x09DB;
                case TaskIdentifier.taskGrundfos:
                    return 0x0A8C;
                case TaskIdentifier.taskStaudruck:
                    return 0x0ADE;
                case TaskIdentifier.taskScrHeiz:
                    return 0x0B2C;
                case TaskIdentifier.taskCAN2Com:
                    return 0x0B6C;
                case TaskIdentifier.taskInvalid:
                default:
                    return 0xFFFF;
            }
        }

        /// <summary>Check task starting offsets</summary>
        /// <param name="ResultString">String of result</param>
        /// <param name="UsedBytes">Number of used bytes</param>
        /// <returns>True if check was successfully</returns>
        public override bool CheckTaskOffset(out String ResultString, out UInt32 UsedBytes)
        {
            int[] ucaTaskPos = new int[mTaskAnzahl];
            int merker;
            UInt32 GapBytes = 0;
            UInt32 OverlappingBytes = 0;
            UInt32 TaskOffset = 0;
            UInt32 TaskBytes = 0;
            UsedBytes = 6 + 9 + 75 + 175 + 172 + 195;
            // Block header(6) Verionen+Anzahl(9), TVT(25*3=75)
            // + taskerrors(25*7 = 175) + initvalues(172) + can2(195)

            for (int i = 0; i < mTaskAnzahl; i++) ucaTaskPos[i] = i; // ursprungsreihenfolge
            // reihenfolge nach aufsteigenden offsets sortieren
            for (int j = 0; j < (mTaskAnzahl - 1); j++)
                for (int i = 0; i < (mTaskAnzahl - 1); i++)
                {
                    if (GetTaskOffset(i) == 0)
                    {
                        ResultString = String.Format("Task mit Offset 0 wurde gestartet ({0})", GetTaskIdentifier(i));
                        return false;
                    } if (GetTaskOffset(ucaTaskPos[i]) > GetTaskOffset(ucaTaskPos[i + 1]))
                    {
                        merker = ucaTaskPos[i];
                        ucaTaskPos[i] = ucaTaskPos[i + 1];
                        ucaTaskPos[i + 1] = merker;
                    }
                }
            // Luecken, ueberlappungen suchen
            for (int TaskPos = 0; TaskPos < mTaskAnzahl; TaskPos++)
            {
                TaskOffset = GetTaskOffset(ucaTaskPos[TaskPos]);
                TaskBytes = maTask[ucaTaskPos[TaskPos]].GetByteUsage(Version);
                if (TaskOffset > UsedBytes)
                {
                    // gap gefunden
                    GapBytes += TaskOffset - UsedBytes;
                    UsedBytes = TaskOffset;
                }
                if (TaskOffset < UsedBytes)
                {
                    // overlap gefunden
                    OverlappingBytes += UsedBytes - TaskOffset;
                }
                UsedBytes += TaskBytes;
            }

            ResultString = "Gap " + GapBytes + " / Overlapping " + OverlappingBytes;
            return (OverlappingBytes == 0);
        }

        /// <summary>Create new configuration and fill content and byte array
        /// from old compatibility by item names</summary>
        /// <param name="OldBlock">Configuration block in old compatibility</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        private ReturnValue Upgrade(Block OldBlock)
        {
            ConfigurationBlock Source;
            ReturnValue ret = ReturnValue.NoError;

            switch (OldBlock.Version)
            {
                case 8:
                    Source = new Configuration8_Block();
                    break;
                default:
                    return ReturnValue.VersionMismatch;
            }
            ret = Source.Import(ref OldBlock, true);
            if (ret != ReturnValue.NoError)
            {
                return ret;
            }

            mBlockData = new byte[4090];

            //Import configuration header
            mTaskAnzahl = (byte)Source.GetTaskNumber();
            mBlockData[0] = mTaskAnzahl;
            NumberOfUsedDatamaps = Source.NumberOfUsedDatamaps;
            mBlockData[1] = NumberOfUsedDatamaps;
            VersionT ver;
            ver.Hauptversion = Source.ConfigVersion.Hauptversion;
            ver.Nebenversion = 0;
            ver.Revision = Version;
            ConfigVersion = ver;
            mBlockData[2] = (byte)(ConfigVersion.Hauptversion % 256);
            mBlockData[3] = (byte)(ConfigVersion.Hauptversion / 256);
            mBlockData[4] = ConfigVersion.Nebenversion;
            mBlockData[5] = ConfigVersion.Revision;
            AbwaertsVersion = Version;
            mBlockData[6] = Version;
            SoftwareType = Source.SoftwareType;
            mBlockData[7] = SoftwareType;
            PasswordLevel = Source.PasswordLevel;
            mBlockData[8] = PasswordLevel;
            //Import task arrays
            TaskVector[] tvt = new TaskVector[25];
            TaskError[,] tet = new TaskError[25, 7];
            for (int i = 0; i < 25; i++)
            {
                if (i < mTaskAnzahl)
                {
                    tvt[i].Tasknummer = Source.GetTaskIdentifier(i);
                    mBlockData[9 + (i * 3)] = (byte)tvt[i].Tasknummer;
                    tvt[i].Offset = GetFixedOffset(tvt[i].Tasknummer);
                    mBlockData[10 + (i * 3)] = (byte)(tvt[i].Offset % 256);
                    mBlockData[11 + (i * 3)] = (byte)(tvt[i].Offset / 256);
                    tet[i, 0].ErrNo = Source.GetTaskErrorNumber(i, 0);
                    tet[i, 0].Ring = Source.GetTaskErrorRingFlag(i, 0);
                    tet[i, 0].Ring = Source.GetTaskErrorRingFlag(i, 0);
                    tet[i, 0].reserve = Source.GetTaskErrorResFlag(i, 0);
                    mBlockData[84 + (i * 7)] = (byte)(tet[i, 0].ErrNo * 4);
                    if (tet[i, 0].Ring) { mBlockData[84 + (i * 7)] = (byte)(mBlockData[84 + (i * 7)] + 1); }
                    if (tet[i, 0].reserve) { mBlockData[84 + (i * 7)] = (byte)(mBlockData[84 + (i * 7)] + 2); }
                    tet[i, 1].ErrNo = Source.GetTaskErrorNumber(i, 1);
                    tet[i, 1].Ring = Source.GetTaskErrorRingFlag(i, 1);
                    tet[i, 1].reserve = Source.GetTaskErrorResFlag(i, 1);
                    mBlockData[85 + (i * 7)] = (byte)(tet[i, 1].ErrNo * 4);
                    if (tet[i, 1].Ring) { mBlockData[85 + (i * 7)] = (byte)(mBlockData[85 + (i * 7)] + 1); }
                    if (tet[i, 1].reserve) { mBlockData[85 + (i * 7)] = (byte)(mBlockData[85 + (i * 7)] + 2); }
                    tet[i, 2].ErrNo = Source.GetTaskErrorNumber(i, 2);
                    tet[i, 2].Ring = Source.GetTaskErrorRingFlag(i, 2);
                    tet[i, 2].reserve = Source.GetTaskErrorResFlag(i, 2);
                    mBlockData[86 + (i * 7)] = (byte)(tet[i, 2].ErrNo * 4);
                    if (tet[i, 2].Ring) { mBlockData[86 + (i * 7)] = (byte)(mBlockData[86 + (i * 7)] + 1); }
                    if (tet[i, 2].reserve) { mBlockData[86 + (i * 7)] = (byte)(mBlockData[86 + (i * 7)] + 2); }
                    tet[i, 3].ErrNo = Source.GetTaskErrorNumber(i, 3);
                    tet[i, 3].Ring = Source.GetTaskErrorRingFlag(i, 3);
                    tet[i, 3].reserve = Source.GetTaskErrorResFlag(i, 3);
                    mBlockData[87 + (i * 7)] = (byte)(tet[i, 3].ErrNo * 4);
                    if (tet[i, 3].Ring) { mBlockData[87 + (i * 7)] = (byte)(mBlockData[87 + (i * 7)] + 1); }
                    if (tet[i, 3].reserve) { mBlockData[87 + (i * 7)] = (byte)(mBlockData[87 + (i * 7)] + 2); }
                    tet[i, 4].ErrNo = Source.GetTaskErrorNumber(i, 4);
                    tet[i, 4].Ring = Source.GetTaskErrorRingFlag(i, 4);
                    tet[i, 4].reserve = Source.GetTaskErrorResFlag(i, 4);
                    mBlockData[88 + (i * 7)] = (byte)(tet[i, 4].ErrNo * 4);
                    if (tet[i, 4].Ring) { mBlockData[88 + (i * 7)] = (byte)(mBlockData[88 + (i * 7)] + 1); }
                    if (tet[i, 4].reserve) { mBlockData[88 + (i * 7)] = (byte)(mBlockData[88 + (i * 7)] + 2); }
                    tet[i, 5].ErrNo = Source.GetTaskErrorNumber(i, 5);
                    tet[i, 5].Ring = Source.GetTaskErrorRingFlag(i, 5);
                    tet[i, 5].reserve = Source.GetTaskErrorResFlag(i, 5);
                    mBlockData[89 + (i * 7)] = (byte)(tet[i, 5].ErrNo * 4);
                    if (tet[i, 5].Ring) { mBlockData[89 + (i * 7)] = (byte)(mBlockData[89 + (i * 7)] + 1); }
                    if (tet[i, 5].reserve) { mBlockData[89 + (i * 7)] = (byte)(mBlockData[89 + (i * 7)] + 2); }
                    tet[i, 6].ErrNo = Source.GetTaskErrorNumber(i, 6);
                    tet[i, 6].Ring = Source.GetTaskErrorRingFlag(i, 6);
                    tet[i, 6].reserve = Source.GetTaskErrorResFlag(i, 6);
                    mBlockData[90 + (i * 7)] = (byte)(tet[i, 6].ErrNo * 4);
                    if (tet[i, 6].Ring) { mBlockData[90 + (i * 7)] = (byte)(mBlockData[90 + (i * 7)] + 1); }
                    if (tet[i, 6].reserve) { mBlockData[90 + (i * 7)] = (byte)(mBlockData[90 + (i * 7)] + 2); }

                }
                else
                {
                    tvt[i].Tasknummer = TaskIdentifier.taskInvalid;
                    mBlockData[9 + (i * 3)] = 0xFF;
                    tvt[i].Offset = 0xFFFF;
                    mBlockData[10 + (i * 3)] = 0xFF;
                    mBlockData[11 + (i * 3)] = 0xFF;
                    tet[i, 0].ErrNo = 0;
                    tet[i, 0].Ring = false;
                    tet[i, 0].reserve = false;
                    mBlockData[84 + (i * 7)] = 0xFF;
                    tet[i, 1].ErrNo = 0;
                    tet[i, 1].Ring = false;
                    tet[i, 1].reserve = false;
                    mBlockData[85 + (i * 7)] = 0xFF;
                    tet[i, 2].ErrNo = 0;
                    tet[i, 2].Ring = false;
                    tet[i, 2].reserve = false;
                    mBlockData[86 + (i * 7)] = 0xFF;
                    tet[i, 3].ErrNo = 0;
                    tet[i, 3].Ring = false;
                    tet[i, 3].reserve = false;
                    mBlockData[87 + (i * 7)] = 0xFF;
                    tet[i, 4].ErrNo = 0;
                    tet[i, 4].Ring = false;
                    tet[i, 4].reserve = false;
                    mBlockData[88 + (i * 7)] = 0xFF;
                    tet[i, 5].ErrNo = 0;
                    tet[i, 5].Ring = false;
                    tet[i, 5].reserve = false;
                    mBlockData[89 + (i * 7)] = 0xFF;
                    tet[i, 6].ErrNo = 0;
                    tet[i, 6].Ring = false;
                    tet[i, 6].reserve = false;
                    mBlockData[90 + (i * 7)] = 0xFF;
                }
            }
            SetTaskVectorTable(tvt);
            SetTaskErrorTable(tet);

            //Import initial values
            bool value_found = false;
            InitValueImport();
            for (int i = 0; i < InitValueGetNumber(); i++)
            {
                value_found = false;
                for (int j = 0; j < Source.InitValueGetNumber(); j++)
                {
                    if (InitValueGetItemName(i) == Source.InitValueGetItemName(j))
                    {
                        InitValueSetItemValue(i, Source.InitValueGetItemValue(j));
                        value_found = true;
                    }
                }
                if (!value_found)
                {
                    string iniItmName = InitValueGetItemName(i).Substring(InitValueGetItemName(i).LastIndexOf('.'));
                    if (iniItmName == ".ulFree[0]")
                    {
                        InitValueSetItemValue(i, (double)0xFFFFFFFF);
                    }
                    else if (iniItmName == ".ulFree[1]")
                    {
                        InitValueSetItemValue(i, (double)0xFFFFFFFF);
                    }
                    else if (iniItmName == ".ulFree[2]")
                    {
                        InitValueSetItemValue(i, (double)0xFFFFFFFF);
                    }
                    else if (iniItmName == ".ulFree[3]")
                    {
                        InitValueSetItemValue(i, (double)0xFFFFFFFF);
                    }
                    // Changes from 8 to 9
                    else if (InitValueGetItemName(i) == "sVert.siTnachAlarmValue")
                    {
                        InitValueSetItemValue(i, (double)8000);
                    }
                    else if (InitValueGetItemName(i) == "sVert.uiTnachAlarmTime")
                    {
                        InitValueSetItemValue(i, (double)0);
                    }
                    else if (InitValueGetItemName(i) == "sVert.uiTnachAlarmResetDelay")
                    {
                        InitValueSetItemValue(i, (double)0);
                    }
                    else if (InitValueGetItemName(i) == "sVert.siTnachWarnValue")
                    {
                        InitValueSetItemValue(i, (double)8000);
                    }
                    else if (InitValueGetItemName(i) == "sVert.uiTnachWarnTime")
                    {
                        InitValueSetItemValue(i, (double)0);
                    }
                    else if (InitValueGetItemName(i) == "sVert.uiTnachWarnResetDelay")
                    {
                        InitValueSetItemValue(i, (double)0);
                    }
                    else if (InitValueGetItemName(i) == "sIO.uiAmbPressureCalibration")
                    {
                        InitValueSetItemValue(i, (double)97);
                    }
                    else if (InitValueGetItemName(i) == "sGrundfos.ulReserved[0]")
                    {
                        InitValueSetItemValue(i, (double)0xFFFFFFFF);
                    }
                    else if (InitValueGetItemName(i) == "sGrundfos.ulReserved[1]")
                    {
                        InitValueSetItemValue(i, (double)0xFFFFFFFF);
                    }
                    else if (InitValueGetItemName(i) == "sGrundfos.ulReserved[2]")
                    {
                        InitValueSetItemValue(i, (double)0xFFFFFFFF);
                    }
                    else
                    {
                        Console.WriteLine("Upgrade Error: InitValue not found: " + InitValueGetItemName(i));
                    }
                }
            }

            //Fill new second can
            SecondCanImport();
            for (int i = 431; i < (431 + 195); i++)
            {
                mBlockData[i] = 0xFF;
            }

            //Import tasks
            int posSource = -1;
            int posTarget = -1;
            double pump = -1;
            maTask = new TaskConfiguration[mTaskAnzahl];
            for (int i = 0; i < mTaskAnzahl; i++)
            {
                maTask[i] = new TaskConfiguration(GetTaskIdentifier(i));
                maTask[i].Import9(GetFixedOffset(GetTaskIdentifier(i)), ref mBlockData);
                for (int t = 0; t < GetTaskItemNumber(i); t++)
                {
                    value_found = false;
                    posTarget = GetTaskItemPosition(i, GetTaskItemName(i, t));
                    if (posTarget >= 0)
                    {
                        for (int s = 0; s < Source.GetTaskItemNumber(i); s++)
                        {
                            if (GetTaskItemName(i, t) == Source.GetTaskItemName(i, s))
                            {
                                posSource = Source.GetTaskItemPosition(i, Source.GetTaskItemName(i, s));
                                if (posSource >= 0)
                                {
                                    if (Source.GetTaskItemType(i, posSource) == TaskDataType.type_enum_mrw_8)
                                    {
                                        maTask[i].SetItemValue(posTarget,
                                            Firmware.UpgradeMessWert8to9((byte)Source.GetTaskItemValue(i, s)));
                                    }
                                    else
                                    {
                                        if (GetTaskItemName(i, t) == "ulAschepulse")
                                        {
                                            for(int tp = 0; tp < Source.GetTaskNumber(); tp++){
                                                if(Source.GetTaskIdentifier(tp) == TaskIdentifier.taskDosieren){
                                                    //int ip = Source.GetTaskItemPosition(tp, "ulPumpenhubvolumen");
                                                    //double pump = Source.GetTaskItemValue(tp, ip);
                                                    pump = Source.GetTaskItemValue(tp, Source.GetTaskItemPosition(tp, "ulPumpenhubvolumen"));
                                                    pump = pump / 1000;
                                                    pump = pump * Source.GetTaskItemValue(i, s);
                                                    pump = pump / 1000;
                                                    pump = pump + .5;
                                                }
                                            }
                                            if (pump > 0)
                                            {
                                                maTask[i].SetItemValue(posTarget, pump);
                                            }
                                            else
                                            {
                                                maTask[i].SetItemValue(posTarget, Source.GetTaskItemValue(i, s));
                                            }
                                        }
                                        else
                                        {
                                            maTask[i].SetItemValue(posTarget, Source.GetTaskItemValue(i, s));
                                        }
                                    }
                                    value_found = true;
                                }
                            }
                        }
                    }
                    if (!value_found)
                    {
                        if(GetTaskItemName(i, t) == "ulFree[0]")
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFFFFFF);
                        }
                        else if (GetTaskItemName(i, t) == "ulFree[1]")
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFFFFFF);
                        }
                        else if (GetTaskItemName(i, t) == "ulFree[2]")
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFFFFFF);
                        }
                        else if (GetTaskItemName(i, t) == "ulFree[3]")
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFFFFFF);
                        }
                        else if (GetTaskItemName(i, t) == "ulFree[4]")
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFFFFFF);
                        }
                        //  Changes from 8 to 9
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskIo)
                            && (GetTaskItemName(i, t) == "uiADLastErrorLimit"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)200);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskIo)
                            && (GetTaskItemName(i, t) == "uiFree"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskTurnspeed)
                            && (GetTaskItemName(i, t) == "ucUseAirMass"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskTurnspeed)
                            && (GetTaskItemName(i, t) == "eMrwAirMassSource"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskAcquisition)
                            && (GetTaskItemName(i, t) == "ucAcquisitionSize"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)60);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "ucLeerlaufStrategie"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "siTempSchwellwertOben"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)3000);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "siTempSchwellwertUnten"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)2000);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "uiTempNoiseMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)30);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "uiTempIntervalDiffMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)30);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "uiTempIntervalDiffTime"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)50);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "uiDruckSchwellwert"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)50);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "uiDruckAbweichungMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)1);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "uiZeitTempFenster"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)180);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskBeladMittel)
                            && (GetTaskItemName(i, t) == "uiZeitDruckFenster"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)150);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskAdditivierung)
                            && (GetTaskItemName(i, t) == "ucUnterspannungMessCntMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)128);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[0].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[0].uiValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[0].uiValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[1].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[1].uiValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[1].uiValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[2].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[2].uiValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[2].uiValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[3].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[3].uiValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[3].uiValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[4].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[4].uiValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[4].uiValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[5].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[5].uiValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[5].uiValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[6].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[6].uiValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[6].uiValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[7].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[7].uiValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "unsignedMesswertParameter[7].uiValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[0].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[0].siValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[0].siValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[1].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[1].siValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[1].siValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[2].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[2].siValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[2].siValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[3].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[3].siValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[3].siValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[4].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[4].siValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[4].siValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[5].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[5].siValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[5].siValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[6].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[6].siValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[6].siValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[7].ucPlausibilitaetscheckFlag"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[7].siValueMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "signedMesswertParameter[7].siValueMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0xFFFF);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskCanIn)
                            && (GetTaskItemName(i, t) == "ulSystemStartErrorDelay"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)15000);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskVertWatch)
                            && (GetTaskItemName(i, t) == "uiF37acceptCounterLimit"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)65535);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskVertWatch)
                            && (GetTaskItemName(i, t) == "uiF37acceptTimeWindow"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[0].eDefMeasureValue"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[0].siPlausibilityMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)600);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[0].siPlausibilityMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)250);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[0].uiTimeDebounceActive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)300);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[0].uiTimeDebounceInactive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)50);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[1].eDefMeasureValue"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[1].siPlausibilityMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)600);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[1].siPlausibilityMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)250);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[1].uiTimeDebounceActive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)300);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[1].uiTimeDebounceInactive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)50);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[2].eDefMeasureValue"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[2].siPlausibilityMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)6000);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[2].siPlausibilityMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)2500);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[2].uiTimeDebounceActive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)300);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[2].uiTimeDebounceInactive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)50);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[3].eDefMeasureValue"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[3].siPlausibilityMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)5000);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[3].siPlausibilityMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)800);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[3].uiTimeDebounceActive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)50);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[3].uiTimeDebounceInactive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)20);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[4].eDefMeasureValue"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[4].siPlausibilityMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)3000);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[4].siPlausibilityMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)900);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[4].uiTimeDebounceActive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)300);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[4].uiTimeDebounceInactive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)100);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[5].eDefMeasureValue"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[5].siPlausibilityMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)500);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[5].siPlausibilityMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)0);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[5].uiTimeDebounceActive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)300);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[5].uiTimeDebounceInactive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)100);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[6].eDefMeasureValue"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[6].siPlausibilityMax"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)15000);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[6].siPlausibilityMin"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)800);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[6].uiTimeDebounceActive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)300);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eMeasureObservation[6].uiTimeDebounceInactive"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)100);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "uiFailSafeSensor"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)35);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eAirPressSource"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "eAirTempSource"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[0]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[1]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[2]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[3]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[4]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[5]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[6]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[7]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[8]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[9]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[10]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[11]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[12]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[13]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[14]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[15]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[16]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[17]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[18]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[19]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[20]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[21]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[22]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[23]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[24]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[25]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[26]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[27]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[28]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[29]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[30]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[31]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else if ((GetTaskIdentifier(i) == TaskIdentifier.taskMassAirFlow)
                            && (GetTaskItemName(i, t) == "ucReserved[32]"))
                        {
                            maTask[i].SetItemValue(posTarget, (double)255);
                        }
                        else 
                        {
                            Console.WriteLine("Upgrade Error: Taskitem not found: (" + GetTaskIdentifier(i).ToString() + ") " + GetTaskItemName(i, t));
                        }
                    }
                }
                maTask[i].SetChecksum(Version);
            }

            GenerateChecksum();
            return ret;
        }
    }
}
