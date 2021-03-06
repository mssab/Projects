/*
 * Object: HJS.ECU.Parameter.TaskConfigurationItem
 * Description: Single item of task configuration
 * 
 * $LastChangedDate: 2015-01-26 09:18:35 +0100 (Mo, 26 Jan 2015) $
 * $LastChangedRevision: 83 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/TaskConfigurationItem.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU.Parameter
{
    /// <summary>Configuration item</summary>
    public class TaskConfigurationItem
    {
        private string mName;
        private TaskDataType mType;
        private UInt16 mOffset;
        private double mMin;
        private bool mCheckMin;
        private double mMax;
        private bool mCheckMax;
        private HJS.ECU.Firmware.KennfeldTyp[] mDataMapTypes;

        /// <summary>Accessors for name
        /// Read only</summary>
        public string Name
        {
            get { return mName; }
        }

        /// <summary>Accessors for data type
        /// Read only</summary>
        public TaskDataType DataType
        {
            get { return mType; }
        }

        /// <summary>Accessors for data item offsets
        /// Read only</summary>
        public UInt16 Offset
        {
            get { return mOffset; }
        }

        /// <summary>Constructor</summary>
        /// <param name="name">Item name</param>
        /// <param name="type">Item data type</param>
        /// <param name="offset">Offset in task data</param>
        public TaskConfigurationItem(string name, TaskDataType type, UInt16 offset)
        {
            mName = name;
            mType = type;
            mOffset = offset;
            mCheckMin = false;
            mCheckMax = false;

        }

        /// <summary>Get value</summary>
        /// <param name="Data">Reference to task byte array</param>
        /// <returns>value as double</returns>
        public double GetValue(ref byte[] Data)
        {
            double ret = 0;
            switch (mType)
            {
                case TaskDataType.type_uint_8:
                case TaskDataType.type_kf_type_8:
                case TaskDataType.type_hex_8:
                case TaskDataType.type_enum_mrw_8:
                case TaskDataType.type_psoc_gain_8:
                case TaskDataType.type_tank_signal_8:
                case TaskDataType.type_can_baudrate_8:
                    ret = (byte)Data[mOffset];
                    break;
                case TaskDataType.type_int_8:
                    ret = (char)Data[mOffset];
                    break;
                case TaskDataType.type_uint_16:
                case TaskDataType.type_hex_16:
                    ret = BitConverter.ToUInt16(Data, mOffset);
                    break;
                case TaskDataType.type_int_16:
                    ret = BitConverter.ToInt16(Data, mOffset);
                    break;
                case TaskDataType.type_uint_32:
                case TaskDataType.type_can_id_32:
                case TaskDataType.type_kf_id_32:
                case TaskDataType.type_hex_32:
                    ret = BitConverter.ToUInt32(Data, mOffset);
                    break;
                case TaskDataType.type_int_32:
                    ret = BitConverter.ToInt32(Data, mOffset);
                    break;
                case TaskDataType.type_float_32:
                    ret = BitConverter.ToSingle(Data, mOffset);
                    break;
            }
            return ret;
        }

        /// <summary>Set value</summary>
        /// <param name="Data">Reference to task byte array</param>
        /// <param name="NewValue">New value to be set</param>
        /// <returns>True on success</returns>
        public bool SetValue(ref byte[] Data, double NewValue)
        {
            bool ret = false;
            byte[] _buffer = null;
            switch (mType)
            {
                case TaskDataType.type_uint_8:
                case TaskDataType.type_kf_type_8:
                case TaskDataType.type_hex_8:
                case TaskDataType.type_enum_mrw_8:
                case TaskDataType.type_psoc_gain_8:
                case TaskDataType.type_tank_signal_8:
                case TaskDataType.type_can_baudrate_8:
                    _buffer = new byte[1];
                    _buffer[0] = (byte)NewValue;
                    break;
                case TaskDataType.type_int_8:
                    _buffer = BitConverter.GetBytes((char)(NewValue));
                    Array.Resize(ref _buffer, 1); // Kein unicode! nur erste byte (hoffentlich ascii) verwenden!
                    break;
                case TaskDataType.type_uint_16:
                case TaskDataType.type_hex_16:
                    _buffer = BitConverter.GetBytes((UInt16)(NewValue));
                    break;
                case TaskDataType.type_int_16:
                    _buffer = BitConverter.GetBytes((Int16)(NewValue));
                    break;
                case TaskDataType.type_uint_32:
                case TaskDataType.type_can_id_32:
                case TaskDataType.type_kf_id_32:
                case TaskDataType.type_hex_32:
                    _buffer = BitConverter.GetBytes((UInt32)(NewValue));
                    break;
                case TaskDataType.type_int_32:
                    _buffer = BitConverter.GetBytes((Int32)(NewValue));
                    break;
                case TaskDataType.type_float_32:
                    _buffer = BitConverter.GetBytes((Single)(NewValue));
                    break;
            }
            if (_buffer != null)
            {
                if (_buffer.Length > 0)
                {
                    for (int i = 0; i < _buffer.Length; i++)
                    {
                        Data[mOffset + i] = _buffer[i];
                    }
                    ret = true;
                }
            }
            // copy buffer to data
            return ret;
        }

        /// <summary>Get value</summary>
        /// <param name="Data">Reference to task byte array</param>
        /// <param name="Compatibility">Compatibility of firmware</param>
        /// <returns>value as string</returns>
        public string GetValueString(ref byte[] Data, byte Compatibility)
        {
            string ret = "";
            switch (mType)
            {
                case TaskDataType.type_uint_8:
                    ret = ((byte)Data[mOffset]).ToString("D");
                    break;
                case TaskDataType.type_kf_type_8:
                    ret = ((HJS.ECU.Firmware.KennfeldTyp)Data[mOffset]).ToString();
                    break;
                case TaskDataType.type_hex_8:
                    ret = String.Format("0x{0}",((byte)Data[mOffset]).ToString("X2"));
                    break;
                case TaskDataType.type_enum_mrw_8:
                    switch (Compatibility)
                    {
                        case 8:
                            ret = ((HJS.ECU.Firmware.MessWert8)Data[mOffset]).ToString();
                            break;
                        case 9:
                            ret = ((HJS.ECU.Firmware.MessWert9)Data[mOffset]).ToString();
                            break;
                        case 10:
                            ret = ((HJS.ECU.Firmware.MessWert10)Data[mOffset]).ToString();
                            break;
                        default:
                            ret = String.Format("NO_ENUM_{0}", Data[mOffset]);
                            break;
                    }
                    break;
                case TaskDataType.type_int_8:
                    //todo: char
                    ret = ((byte)Data[mOffset]).ToString("D");
                    break;
                case TaskDataType.type_psoc_gain_8:
                    ret = ((HJS.ECU.Firmware.PsocGain)Data[mOffset]).ToString();
                    break;
                case TaskDataType.type_tank_signal_8:
                    ret = ((HJS.ECU.Firmware.TankSignal)Data[mOffset]).ToString();
                    break;
                case TaskDataType.type_can_baudrate_8:
                    ret = ((HJS.ECU.Firmware.CanBaudrate)Data[mOffset]).ToString();
                    break;
                case TaskDataType.type_uint_16:
                    ret = (BitConverter.ToUInt16(Data, mOffset)).ToString("D");
                    break;
                case TaskDataType.type_hex_16:
                    ret = String.Format("0x{0}",((BitConverter.ToUInt16(Data, mOffset)).ToString("X4")));
                    break;
                case TaskDataType.type_int_16:
                    ret = (BitConverter.ToInt16(Data, mOffset)).ToString("D");
                    break;
                case TaskDataType.type_uint_32:
                    if (BitConverter.ToUInt32(Data, mOffset) == 0xFFFFFFFF)
                    {
                        ret = "0xFFFFFFFF";
                    }
                    else
                    {
                        ret = (BitConverter.ToUInt32(Data, mOffset)).ToString("D");
                    }
                    break;
                case TaskDataType.type_can_id_32:
                    UInt32 id = (UInt32)(BitConverter.ToUInt16(Data, Offset) << 16);
                    id = id + BitConverter.ToUInt16(Data, (Offset + 2));
                    UInt32 val = BitConverter.ToUInt32(Data, mOffset);
                    if ((id & 0x80000000) != 0)
                    {
                        // 29-Bit-Id
                        ret = String.Format("0x{0:X8} = ID0x{1:X8}", val, id & 0x7FFFFFFF);
                    }else{
                        // 11-Bit-Id
                        ret = String.Format("0x{0:X8} = ID0x{1:X3}", val, (id & 0x7FFFFFFF) / 0x40000);
                    }
                    break;
                case TaskDataType.type_kf_id_32:
                    if (BitConverter.ToUInt32(Data, mOffset) == 0xFFFFFFFF)
                    {
                        ret = "0xFFFFFFFF";
                    }
                    else
                    {
                        ret = (BitConverter.ToUInt32(Data, mOffset)).ToString("D");
                    }
                    break;
                case TaskDataType.type_hex_32:
                    ret = String.Format("0x{0}",((BitConverter.ToUInt32(Data, mOffset)).ToString("X8")));
                    break;
                case TaskDataType.type_int_32:
                    ret = (BitConverter.ToInt32(Data, mOffset)).ToString("D");
                    break;
                case TaskDataType.type_float_32:
                    ret = (BitConverter.ToSingle(Data, mOffset)).ToString("F7");
                    break;
            }
            return ret;
        }

        /// <summary>Set mimimum plausiblity of task item</summary>
        /// <param name="minimum">Mimimum plausiblity value</param>
        public void SetPlausibilityMin(double minimum)
        {
            mCheckMin = true;
            mMin = minimum;
        }

        /// <summary>Set maximum plausiblity of task item</summary>
        /// <param name="maximum">Maximum plausiblity value</param>
        public void SetPlausibilityMax(double maximum)
        {
            mCheckMax = true;
            mMax = maximum;
        }

        /// <summary>Check items versus palausbility</summary>
        /// <param name="Data">Reference to data byte array</param>
        /// <param name="Compatibility">Compatibility of task</param>
        /// <returns>Empty string on success, else error text</returns>
        public string CheckValue(ref byte[] Data, byte Compatibility)
        {
            string ret = "";

            switch (mType)
            {
                    // Enumerations
                case TaskDataType.type_kf_type_8:
                    if (!Enum.IsDefined(typeof(HJS.ECU.Firmware.KennfeldTyp), (HJS.ECU.Firmware.KennfeldTyp)Data[mOffset]))
                    {
                        ret = String.Format("{0} = {1}", mName, Data[mOffset]);
                    }
                    break;
                case TaskDataType.type_enum_mrw_8:
                    switch (Compatibility)
                    {
                        case 8:
                            if (!Enum.IsDefined(typeof(HJS.ECU.Firmware.MessWert8), (HJS.ECU.Firmware.MessWert8)Data[mOffset]))
                            {
                                ret = String.Format("{0} = {1}", mName, Data[mOffset]);
                            }
                            break;
                        case 9:
                            if (!Enum.IsDefined(typeof(HJS.ECU.Firmware.MessWert9), (HJS.ECU.Firmware.MessWert9)Data[mOffset]))
                            {
                                ret = String.Format("{0} = {1}", mName, Data[mOffset]);
                            }
                            break;
                        case 10:
                            if (!Enum.IsDefined(typeof(HJS.ECU.Firmware.MessWert10), (HJS.ECU.Firmware.MessWert10)Data[mOffset]))
                            {
                                ret = String.Format("{0} = {1}", mName, Data[mOffset]);
                            }
                            break;
                        default:
                            ret = "Kompatibilität wird nicht unterstützt!";
                            break;
                    }
                    break;
                case TaskDataType.type_psoc_gain_8:
                    if (!Enum.IsDefined(typeof(HJS.ECU.Firmware.PsocGain), (HJS.ECU.Firmware.PsocGain)Data[mOffset]))
                    {
                        ret = String.Format("{0} = {1}", mName, Data[mOffset]);
                    }
                    break;
                case TaskDataType.type_tank_signal_8:
                    if (!Enum.IsDefined(typeof(HJS.ECU.Firmware.TankSignal), (HJS.ECU.Firmware.TankSignal)Data[mOffset]))
                    {
                        ret = String.Format("{0} = {1}", mName, Data[mOffset]);
                    }
                    break;
                case TaskDataType.type_can_baudrate_8:
                    if (!Enum.IsDefined(typeof(HJS.ECU.Firmware.CanBaudrate), (HJS.ECU.Firmware.CanBaudrate)Data[mOffset]))
                    {
                        ret = String.Format("{0} = {1}", mName, Data[mOffset]);
                    }
                    break;

                    // Plausibility min, max
                case TaskDataType.type_uint_8:
                    if (mCheckMin && ((byte)Data[mOffset] < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && ((byte)Data[mOffset] > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_hex_8:
                    if (mCheckMin && ((byte)Data[mOffset] < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && ((byte)Data[mOffset] > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_int_8:
                    //todo: char
                    if (mCheckMin && ((byte)Data[mOffset] < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && ((byte)Data[mOffset] > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_uint_16:
                    if (mCheckMin && (BitConverter.ToUInt16(Data, mOffset) < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && (BitConverter.ToUInt16(Data, mOffset) > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_hex_16:
                    if (mCheckMin && (BitConverter.ToUInt16(Data, mOffset) < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && (BitConverter.ToUInt16(Data, mOffset) > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_int_16:
                    if (mCheckMin && (BitConverter.ToInt16(Data, mOffset) < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && (BitConverter.ToInt16(Data, mOffset) > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_uint_32:
                    if (mCheckMin && (BitConverter.ToUInt32(Data, mOffset) < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && (BitConverter.ToUInt32(Data, mOffset) > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_can_id_32:
                    // todo: can id
                    //UInt32 id = (UInt32)(BitConverter.ToUInt16(Data, Offset) << 16);
                    //id = id + BitConverter.ToUInt16(Data, (Offset + 2));
                    //UInt32 val = BitConverter.ToUInt32(Data, mOffset);
                    //if ((id & 0x80000000) != 0)
                    //{
                    //    // 29-Bit-Id
                    //    ret = String.Format("0x{0:X8} = ID0x{1:X8}", val, id & 0x7FFFFFFF);
                    //}
                    //else
                    //{
                    //    // 11-Bit-Id
                    //    ret = String.Format("0x{0:X8} = ID0x{1:X3}", val, (id & 0x7FFFFFFF) / 0x40000);
                    //}
                    break;
                case TaskDataType.type_kf_id_32:
                    if (mCheckMin && (BitConverter.ToUInt32(Data, mOffset) < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && (BitConverter.ToUInt32(Data, mOffset) > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_hex_32:
                    if (mCheckMin && (BitConverter.ToUInt32(Data, mOffset) < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && (BitConverter.ToUInt32(Data, mOffset) > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_int_32:
                    if (mCheckMin && (BitConverter.ToInt32(Data, mOffset) < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && (BitConverter.ToInt32(Data, mOffset) > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
                case TaskDataType.type_float_32:
                    if (mCheckMin && (BitConverter.ToSingle(Data, mOffset) < mMin))
                    {
                        ret = String.Format("{0} < {1}", mName, mMin);
                    }
                    if (mCheckMax && (BitConverter.ToSingle(Data, mOffset) > mMax))
                    {
                        ret = String.Format("{0} > {1}", mName, mMax);
                    }
                    break;
            }

            return ret;
        }

        /// <summary>Allow data map type</summary>
        /// <param name="type">Allowed data map type</param>
        public void AllowDataMapType(HJS.ECU.Firmware.KennfeldTyp type)
        {
            if (mDataMapTypes == null)
            {
                mDataMapTypes = new Firmware.KennfeldTyp[1];
                mDataMapTypes[0] = type;
            }
            else
            {
                Array.Resize(ref mDataMapTypes, mDataMapTypes.Length + 1);
                mDataMapTypes[mDataMapTypes.Length - 1] = type;
            }
        }

        /// <summary>Check if data map type is valid for task item</summary>
        /// <param name="type">Required typ</param>
        /// <returns>True if required type is valid for task item</returns>
        public bool IsDataMapTypeValid(HJS.ECU.Firmware.KennfeldTyp type)
        {
            if (mDataMapTypes == null) return false;
            for (int i = 0; i < mDataMapTypes.Length; i++)
            {
                if (mDataMapTypes[i] == type) return true;
            }
            return false;
        }
    }
}
