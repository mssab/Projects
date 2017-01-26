/*
 * Object: HJS.ECU.Parameter.ConfigurationBlock
 * Description: Base class of configuration parameter block
 * 
 * $LastChangedDate: 2015-02-18 15:26:37 +0100 (Mi, 18 Feb 2015) $
 * $LastChangedRevision: 89 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/ConfigurationBlock.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU.Parameter
{
    /// <summary>Task vector table item</summary>
    public struct TaskVector
    {
        /// <summary>Task identifier</summary>
        public TaskIdentifier Tasknummer;
        /// <summary>Task offset in configuration block data</summary>
        public UInt16 Offset;
    }

    /// <summary>Task error item</summary>
    public struct TaskError
    {
        /// <summary>Error number</summary>
        public byte ErrNo;
        /// <summary>Flag if error storable in ring</summary>
        public bool Ring;
        /// <summary>Reserved flag</summary>
        public bool reserve;
    }

    /// <summary>Configuration parameter block base object</summary>
    public abstract class ConfigurationBlock : Block
    {
        /// <summary>Nuber of tasks</summary>
        protected byte mTaskAnzahl;

        private byte mNumberOfUsedDatamaps;
        private VersionT mConfigVersion;
        private byte mAbwaertsVersion;
        private byte mSoftwareType;
        private byte mPasswordLevel;
        private TaskVector[] mTaskVectorTable;
        private TaskError[,] mTaskErrorTable;
        //init values
        //can2
        //tasks

        /// <summary>Array of task configurations</summary>
        protected TaskConfiguration[] maTask;

        #region Accessors
        /// <summary>Accessors for number of data maps</summary>
        public byte NumberOfUsedDatamaps
        {
            get { return mNumberOfUsedDatamaps; }
            set { mNumberOfUsedDatamaps = value; }
        }
        /// <summary>Accessors for configuration version structure</summary>
        public VersionT ConfigVersion
        {
            get { return mConfigVersion; }
            set { mConfigVersion = value; }
        }
        /// <summary>Accessors for downward revision</summary>
        public byte AbwaertsVersion
        {
            get { return mAbwaertsVersion; }
            set { mAbwaertsVersion = value; }
        }
        /// <summary>Accessors for software type</summary>
        public byte SoftwareType
        {
            get { return mSoftwareType; }
            set { mSoftwareType = value; }
        }
        /// <summary>Accessors for password level</summary>
        public byte PasswordLevel
        {
            get { return mPasswordLevel; }
            set { mPasswordLevel = value; }
        }
        #endregion

        /// <summary>Default constructor</summary>
        public ConfigurationBlock()
        {
            Type = BlockId.IdKonfig;
            Version = 0;
            DataSize = 0;
        }
        
        /// <summary>Import from base block</summary>
        /// <param name="Source">Source block</param>
        /// <param name="KeepVersion">Flag if Target version should remain</param>
        /// <returns></returns>
        public abstract ReturnValue Import(ref Block Source, bool KeepVersion);

        /// <summary>Update byte array
        /// Data from header strucutres, except dynamic structures are parsed back into the byte array</summary>
        public abstract void Parse();

        /// <summary>Check task starting offsets</summary>
        /// <param name="ResultString">String of result</param>
        /// <param name="UsedBytes">Number of used bytes</param>
        /// <returns>True if check was successfully</returns>
        public abstract bool CheckTaskOffset(out String ResultString, out UInt32 UsedBytes);

        /// <summary>Get fixed task offset</summary>
        /// <param name="task">Identifier of task</param>
        /// <returns>Offset according HSL block structure</returns>
        public abstract UInt16 GetFixedOffset(TaskIdentifier task);

        /// <summary>Import initial values from base block</summary>
        public abstract void InitValueImport();

        /// <summary>Number of init value items</summary>
        /// <returns></returns>
        public abstract int InitValueGetNumber();

        /// <summary>Get name of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Name of init value item</returns>
        public abstract string InitValueGetItemName(int position);

        /// <summary>Get value of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of init value item as double</returns>
        public abstract double InitValueGetItemValue(int position);

        /// <summary>Set value of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <param name="NewValue">New Value to be set</param>
        /// <returns>True on success</returns>
        public abstract bool InitValueSetItemValue(int position, double NewValue);

        /// <summary>Get value string of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of init value item as string</returns>
        public abstract string InitValueGetItemValueString(int position);

        /// <summary>Get data type of init value item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Data type of init value item</returns>
        public abstract TaskDataType InitValueGetItemType(int position);

        /// <summary>Check initial value items versus plausibity</summary>
        /// <returns>Empty string on success, else error text</returns>
        public abstract string CheckInitValueItems();

        /// <summary>Import second can from base block</summary>
        public abstract void SecondCanImport();

        /// <summary>Number of second can items</summary>
        /// <returns>Number of second can items</returns>
        public abstract int SecondCanGetNumber();

        /// <summary>Get name of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Name of second can item</returns>
        public abstract string SecondCanGetItemName(int position);

        /// <summary>Get value of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of second can item as double</returns>
        public abstract double SecondCanGetItemValue(int position);

        /// <summary>Set value of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <param name="NewValue">New Value to be set</param>
        /// <returns>True on success</returns>
        public abstract bool SecondCanSetItemValue(int position, double NewValue);

        /// <summary>Get value string of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of second can item as string</returns>
        public abstract string SecondCanGetItemValueString(int position);

        /// <summary>Get data type of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Data type of second can item</returns>
        public abstract TaskDataType SecondCanGetItemType(int position);

        /// <summary>Get array of enumeration value names of task item type</summary>
        /// <param name="tdtype">Task item type</param>
        /// <returns>Array of values or null</returns>
        public Array GetEnumerationArray(TaskDataType tdtype)
        {
            Array ret = null;
            switch (tdtype)
            {
                case TaskDataType.type_enum_mrw_8:
                    switch (Version)
                    {
                        case 8:
                            ret = Enum.GetValues(typeof(HJS.ECU.Firmware.MessWert8));
                            break;
                        case 9:
                            ret = Enum.GetValues(typeof(HJS.ECU.Firmware.MessWert9));
                            break;
                        case 10:
                            ret = Enum.GetValues(typeof(HJS.ECU.Firmware.MessWert10));
                            break;
                        default:
                            // return empty array
                            break;
                    }
                    break;
                case TaskDataType.type_can_baudrate_8:
                    ret = Enum.GetValues(typeof(HJS.ECU.Firmware.CanBaudrate));
                    break;
                case TaskDataType.type_psoc_gain_8:
                    ret = Enum.GetValues(typeof(HJS.ECU.Firmware.PsocGain));
                    break;
                case TaskDataType.type_kf_type_8:
                    ret = Enum.GetValues(typeof(HJS.ECU.Firmware.KennfeldTyp));
                    break;
                case TaskDataType.type_tank_signal_8:
                    ret = Enum.GetValues(typeof(HJS.ECU.Firmware.TankSignal));
                    break;
            }
            return ret;
        }

        /// <summary>Get index of enumeration value in array available by GetEnumerationArray()</summary>
        /// <param name="EnumValue">Value of enumeration item</param>
        /// <param name="tdtype">Task item type</param>
        /// <param name="EnumArray"></param>
        /// <returns>Index in enumeration value array. see GetEnumerationArray</returns>
        public int GetEnumerationIndex(double EnumValue, TaskDataType tdtype, Array EnumArray)
        {
            int ret = 0;
            switch (tdtype)
            {
                case TaskDataType.type_enum_mrw_8:
                    switch (Version)
                    {
                        case 8:
                            ret = Array.IndexOf(EnumArray, (HJS.ECU.Firmware.MessWert8)EnumValue);
                            break;
                        case 9:
                            ret = Array.IndexOf(EnumArray, (HJS.ECU.Firmware.MessWert9)EnumValue);
                            break;
                        case 10:
                            ret = Array.IndexOf(EnumArray, (HJS.ECU.Firmware.MessWert10)EnumValue);
                            break;
                        default:
                            // return 0
                            break;
                    }
                    break;
                case TaskDataType.type_can_baudrate_8:
                    ret = Array.IndexOf(EnumArray, (HJS.ECU.Firmware.CanBaudrate)EnumValue);
                    break;
                case TaskDataType.type_psoc_gain_8:
                    ret = Array.IndexOf(EnumArray, (HJS.ECU.Firmware.PsocGain)EnumValue);
                    break;
                case TaskDataType.type_kf_type_8:
                    ret = Array.IndexOf(EnumArray, (HJS.ECU.Firmware.KennfeldTyp)EnumValue);
                    break;
                case TaskDataType.type_tank_signal_8:
                    ret = Array.IndexOf(EnumArray, (HJS.ECU.Firmware.TankSignal)EnumValue);
                    break;
            }
            return ret;
        }

        /// <summary>Get task offset</summary>
        /// <param name="position">Position of task in task vector table</param>
        /// <returns>Byte offset of task</returns>
        public UInt16 GetTaskOffset(int position)
        {
            if (mTaskVectorTable == null)
                return 0;
            if (position > mTaskVectorTable.Length)
                return 0;
            return mTaskVectorTable[position].Offset;
        }

        /// <summary>Get internal identifier of task</summary>
        /// <param name="position">Position of task in task vector table</param>
        /// <returns>Identifier of task</returns>
        public TaskIdentifier GetTaskIdentifier(int position)
        {
            if (mTaskVectorTable == null)
                return TaskIdentifier.taskInvalid;
            if (position > mTaskVectorTable.Length)
                return TaskIdentifier.taskInvalid;
            return mTaskVectorTable[position].Tasknummer;
        }

        /// <summary>Set task vector table</summary>
        /// <param name="tvt">New task vecor table</param>
        public void SetTaskVectorTable(TaskVector[] tvt)
        {
            mTaskVectorTable = tvt;
        }


        /// <summary>Set task error table</summary>
        /// <param name="tet">New task error table</param>
        public void SetTaskErrorTable(TaskError[,] tet)
        {
            mTaskErrorTable = tet;
        }

        /// <summary>Get task error table length</summary>
        /// <returns>Task error table length</returns>
        public int GetTaskErrorLength()
        {
            if (mTaskErrorTable == null)
                return 0;
            return mTaskErrorTable.Length;
        }

        /// <summary>Get error number</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <returns>Error number of task</returns>
        public byte GetTaskErrorNumber(int TaskPosition, int ErrorPosition)
        {
            return mTaskErrorTable[TaskPosition, ErrorPosition].ErrNo;
        }

        /// <summary>Set error number</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <param name="ErrorNumber">New error number</param>
        public void SetTaskErrorNumber(int TaskPosition, int ErrorPosition, byte ErrorNumber)
        {
            mTaskErrorTable[TaskPosition, ErrorPosition].ErrNo = ErrorNumber;
        }

        /// <summary>Get error ring flag</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <returns>Error ring flag of task</returns>
        public bool GetTaskErrorRingFlag(int TaskPosition, int ErrorPosition)
        {
            return mTaskErrorTable[TaskPosition, ErrorPosition].Ring;
        }

        /// <summary>Get error reserved flag</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <returns>Error ring flag of task</returns>
        public bool GetTaskErrorResFlag(int TaskPosition, int ErrorPosition)
        {
            return mTaskErrorTable[TaskPosition, ErrorPosition].reserve;
        }

        /// <summary>Set error ring flag</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <param name="RingFlag">New ring flag</param>
        public void SetTaskErrorRingFlag(int TaskPosition, int ErrorPosition, bool RingFlag)
        {
            mTaskErrorTable[TaskPosition, ErrorPosition].Ring = RingFlag;
        }

        /// <summary>Set error reserved flag</summary>
        /// <param name="TaskPosition">Position of task in error table</param>
        /// <param name="ErrorPosition">Position of task error in table (0..6)</param>
        /// <param name="Flag">New reserved flag</param>
        public void SetTaskErrorResFlag(int TaskPosition, int ErrorPosition, bool Flag)
        {
            mTaskErrorTable[TaskPosition, ErrorPosition].reserve = Flag;
        }

        /// <summary>Get task error string</summary>
        /// <param name="posTask">Position of task in task vector table</param>
        /// <param name="posError">Position of error in task</param>
        /// <returns>String of seven task errors, including ring marker</returns>
        public string GetTaskErrorString(int posTask, int posError)
        {
            String ret = "";
            if (GetTaskErrorLength() <= 0) return "null";
            if (posError < 0 || posError > 6) return "N/A";
            ret = String.Format("{0}{1}{2}",
                (GetTaskErrorNumber(posTask, posError) == 0) ? "" : GetTaskErrorNumber(posTask, posError).ToString(),
                GetTaskErrorRingFlag(posTask, posError) ? "(ring)" : "",
                GetTaskErrorResFlag(posTask, posError) ? "(reserve)" : "");
            return ret;
        }

        /// <summary>Get number of tasks</summary>
        /// <returns>Number of tasks</returns>
        public int GetTaskNumber()
        {
            if (maTask == null)
            {
                return 0;
            }
            else
            {
                return maTask.Length;
            }
        }

        /// <summary>Get task configuration item number</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <returns>Number of items of task</returns>
        public int GetTaskItemNumber(int taskPosition)
        {
            return maTask[taskPosition].GetNumber();
        }

        /// <summary>Get task item position by item name</summary>
        /// <param name="taskPosition">Position of task in vector table</param>
        /// <param name="itemName">Name of task item</param>
        /// <returns>Position in task item table, or -1 if not found</returns>
        public int GetTaskItemPosition(int taskPosition, string itemName)
        {
            int ret = -1;
            for (int itempos = 0; itempos < maTask[taskPosition].GetNumber(); itempos++)
            {
                if (maTask[taskPosition].GetItemName(itempos) == itemName)
                {
                    ret = itempos;
                }
            }
            return ret;
        }

        /// <summary>Get task item name</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <returns>Name of item</returns>
        public string GetTaskItemName(int taskPosition, int itemPosition)
        {
            return maTask[taskPosition].GetItemName(itemPosition);
        }

        /// <summary>Get task item value</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <returns>Value of item as double</returns>
        public double GetTaskItemValue(int taskPosition, int itemPosition)
        {
            return maTask[taskPosition].GetItemValue(itemPosition);
        }

        /// <summary>Set task item name</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <param name="NewValue">New value for task item</param>
        /// <returns>True on success</returns>
        public bool SetTaskItemValue(int taskPosition, int itemPosition, double NewValue)
        {
            bool ret = false;
            ret = maTask[taskPosition].SetItemValue(itemPosition, NewValue);
            if (ret) maTask[taskPosition].SetChecksum(mConfigVersion.Revision);
            return ret;
        }

        /// <summary>Get task item value string</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <returns>Name of item as string</returns>
        public string GetTaskItemValueString(int taskPosition, int itemPosition)
        {
            return maTask[taskPosition].GetItemValueString(itemPosition, Version);
        }

        /// <summary>Get task item type</summary>
        /// <param name="taskPosition">Position of task in task vector table</param>
        /// <param name="itemPosition">Position of task item in task configuration</param>
        /// <returns>Type of item</returns>
        public TaskDataType GetTaskItemType(int taskPosition, int itemPosition)
        {
            return maTask[taskPosition].GetItemType(itemPosition);
        }

        /// <summary>Check if all baudrates parametered in configuration are set to same baudrate</summary>
        /// <returns>True if all baudrates are same, or no baudrate set.</returns>
        public bool CheckTaskBaudrates()
        {
            byte[] brs = new byte[0];
            for (int TaskPos = 0; TaskPos < mTaskAnzahl; TaskPos++)
            {
                for (int ItemPos = 0; ItemPos < maTask[TaskPos].GetNumber(); ItemPos++)
                {
                    if (maTask[TaskPos].GetItemType(ItemPos) == TaskDataType.type_can_baudrate_8)
                    {
                        Array.Resize(ref brs, brs.Length + 1);
                        brs[brs.Length - 1] = (byte)maTask[TaskPos].GetItemValue(ItemPos);
                    }
                }
            }
            if (brs.Length > 0)
            {
                foreach (byte b in brs)
                {
                    if (b != brs[0]) return false;
                }
                return true; // all baudrates set so same frequency
            }
            else
            {
                return true; // no baudrates set in config
            }
        }

        /// <summary>Check task checksums</summary>
        /// <param name="compatibility">Compatibility required for single task sizes</param>
        /// <returns>On success: 255 = invalid task, else the task identifier of first checksum mismatch</returns>
        public TaskIdentifier CheckTaskChecksums(byte compatibility)
        {
            UInt16 crcSaved = 0;
            UInt16 crcCalced = 0;
            for (int TaskPos = 0; TaskPos < mTaskAnzahl; TaskPos++)
            {
                crcSaved = maTask[TaskPos].GetChecksum();
                crcCalced = maTask[TaskPos].GenerateChecksum(compatibility);
                //if (maTask[TaskPos].GenerateChecksum() != maTask[TaskPos].GetChecksum())
                if (crcSaved != crcCalced)
                    return maTask[TaskPos].GetTaskId();
            }
            return TaskIdentifier.taskInvalid;
        }

        /// <summary>Check if all task header values are valid</summary>
        /// <param name="task">Output of last checked position of task</param>
        /// <returns>True if headers are valid</returns>
        public bool CheckTaskHeader(out int task)
        {
            double d = 0;
            task = 0;
            for (int i = 0; i < maTask.Length; i++)
            {
                task = i;
                //pTaskCfg->uiExpire
                d = GetTaskItemValue(i, 1);
                if (d <= 0)
                    return false;
                if (d > 5000)
                    return false;
                //pTaskCfg->uiStacksize
                d = GetTaskItemValue(i, 2);
                if (d < 256)
                    return false;
                if (d > 768)
                    return false;
                //pTaskCfg->ucPrio
                d = GetTaskItemValue(i, 3);
                if (d < 30)
                    return false;
                if (d > 95)
                    return false;
                //pTaskCfg->ucTimeout
                d = GetTaskItemValue(i, 4);
                if (d <= 0)
                    return false;
                if (d > 50)
                    return false;
            }
            return true;
        }

        /// <summary>Get array of data map identifiers</summary>
        /// <returns>Array of unsigned 32-bit-values of data map identifier</returns>
        public UInt32[] GetDatamapIds()
        {
            UInt32[] ret = new UInt32[0];

            for (int TaskPos = 0; TaskPos < mTaskAnzahl; TaskPos++)
            {
                for (int ItemPos = 0; ItemPos < maTask[TaskPos].GetNumber(); ItemPos++)
                {
                    if (maTask[TaskPos].GetItemType(ItemPos) == TaskDataType.type_kf_id_32)
                    {
                        Array.Resize(ref ret, ret.Length + 1);
                        ret[ret.Length - 1] = (UInt32)maTask[TaskPos].GetItemValue(ItemPos);
                    }
                }
            }
            return ret;
        }

        /// <summary>Get array of data map types</summary>
        /// <returns>Array of unsigned 32-bit-values of data map types</returns>
        public HJS.ECU.Firmware.KennfeldTyp[] GetDatamapTypes()
        {
            HJS.ECU.Firmware.KennfeldTyp[] ret = new HJS.ECU.Firmware.KennfeldTyp[0];

            for (int TaskPos = 0; TaskPos < mTaskAnzahl; TaskPos++)
            {
                for (int ItemPos = 0; ItemPos < maTask[TaskPos].GetNumber(); ItemPos++)
                {
                    if (maTask[TaskPos].GetItemType(ItemPos) == TaskDataType.type_kf_type_8)
                    {
                        if ((maTask[TaskPos].GetTaskId() == TaskIdentifier.taskCanIn)
                            && (maTask[TaskPos].GetItemName(ItemPos) == "eTypDerating"))
                        {
                            // ignore data map type without data map identifier
                        }
                        else if ((maTask[TaskPos].GetTaskId() == TaskIdentifier.taskCanIn)
                           && (maTask[TaskPos].GetItemName(ItemPos) == "eTypNoxSensor"))
                        {
                            // ignore data map type without data map identifier
                        }
                        else
                        {
                            Array.Resize(ref ret, ret.Length + 1);
                            ret[ret.Length - 1] = (HJS.ECU.Firmware.KennfeldTyp)maTask[TaskPos].GetItemValue(ItemPos);
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>Check task items versus palausbility</summary>
        /// <returns>Empty string on success, else error text</returns>
        public string CheckTaskItems()
        {
            string ret = "";

            ret = CheckInitValueItems();
            if (!String.IsNullOrEmpty(ret)) return ret;

            for (int TaskPos = 0; TaskPos < mTaskAnzahl; TaskPos++)
            {
                ret = maTask[TaskPos].CheckItems(Version);
                if (!String.IsNullOrEmpty(ret)) return ret;
            }

            return ret;
        }

        /// <summary>Check if all task stacks fit in stack array</summary>
        /// <returns>Empty string on success, else error text</returns>
        public string CheckTaskStackSizes()
        {
            string ret = "";
            UInt32 StackNeededByCfg = 0;
            for (int TaskPos = 0; TaskPos < mTaskAnzahl; TaskPos++)
            {
                StackNeededByCfg += (UInt32)(maTask[TaskPos].GetItemValue(2));
            }
            if (StackNeededByCfg > Firmware.GetTotalStackSize(Version))
            {
                ret = String.Format("Stack Overflow! {0}>{1}", StackNeededByCfg, Firmware.GetTotalStackSize(Version));
            }
            return ret;
        }

        /// <summary>Add an empty task to block</summary>
        public bool AddTask(byte Id)
        {
            for (int TaskPos = 0; TaskPos < mTaskAnzahl; TaskPos++)
            {
                if (maTask[TaskPos].GetTaskId() == (TaskIdentifier)Id) return false;
            }
            if (maTask.Length < 25)
            {
                Array.Resize(ref maTask, maTask.Length + 1);
            }
            maTask[maTask.Length - 1] = new TaskConfiguration((TaskIdentifier)Id);
            switch (Version)
            {
                case 8:
                    maTask[maTask.Length - 1].Import8(GetFixedOffset((TaskIdentifier)Id), ref mBlockData);
                    break;
                case 9:
                    maTask[maTask.Length - 1].Import9(GetFixedOffset((TaskIdentifier)Id), ref mBlockData);
                    break;
                case 10:
                    maTask[maTask.Length - 1].Import10(GetFixedOffset((TaskIdentifier)Id), ref mBlockData);
                    break;
                default:
                    return false;
            }
            mTaskVectorTable[maTask.Length - 1].Tasknummer = (TaskIdentifier)Id;
            mTaskVectorTable[maTask.Length - 1].Offset = GetFixedOffset((TaskIdentifier)Id);
            mTaskAnzahl++;
            Parse();
            return true;
        }

        /// <summary>Check if all task errors are palusible</summary>
        /// <returns>Empty string on success, else error text</returns>
        public string CheckTaskErrors()
        {
            Firmware fw = new Firmware(Version);
            byte allow_error = 0;

            for (int TaskPos = 0; TaskPos < mTaskAnzahl; TaskPos++)
            {
                for (int ErrPos = 0; ErrPos < 7; ErrPos++)
                {
                    allow_error = fw.GetTaskError(maTask[TaskPos].GetTaskId(), ErrPos);
                    if (allow_error != 255)
                    {
                        if ((mTaskErrorTable[TaskPos, ErrPos].ErrNo != allow_error)
                            && (mTaskErrorTable[TaskPos, ErrPos].ErrNo != 0))
                        {
                            return String.Format("Fehler {0}[{1}] != {2}", maTask[TaskPos].GetTaskId(), ErrPos, allow_error);
                        }
                    }
                    if ((mTaskErrorTable[TaskPos, ErrPos].ErrNo == 0)
                        && (mTaskErrorTable[TaskPos, ErrPos].Ring))
                    {
                        return String.Format("{0}[{1}] = 0 darf nicht auf Ring", maTask[TaskPos].GetTaskId(), ErrPos);
                    }
                }
            }
            return String.Empty;
        }
    }
}
