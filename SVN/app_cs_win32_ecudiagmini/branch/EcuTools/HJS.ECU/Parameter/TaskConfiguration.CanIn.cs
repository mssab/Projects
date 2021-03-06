/*
 * Object: HJS.ECU.Parameter.TaskConfiguration.CanIn
 * Description: task configuration for CanIn
 * 
 * $LastChangedDate: 2014-11-24 11:34:22 +0100 (Mo, 24 Nov 2014) $
 * $LastChangedRevision: 75 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/TaskConfiguration.CanIn.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU.Parameter
{
    public partial class TaskConfiguration
    {
        private UInt16 SizeCanIn8() { return 160; }
        private bool ImportCanIn8(UInt16 Offset, ref byte[] Data)
        {
            String msg = "";
            int i = 0;
            UInt16 ItemPosition = 0;
            UInt16 DataPosition = 0;
            if (Offset > 6)
            {
                DataPosition = (UInt16)(Offset - 6);   // block header = 6 abziehen
            }
            else { return false; }
            mData = Data;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiCRC", TaskDataType.type_hex_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiExpire", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(1000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiStacksize", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(512);
            mItem[ItemPosition].SetPlausibilityMax(512);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucPrio", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(80);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucBaudrate", TaskDataType.type_can_baudrate_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 8; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ulMBID", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_can_id_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucDLC", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMin(1);
                //mItem[ItemPosition].SetPlausibilityMax(8);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].eMRW", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_enum_mrw_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucStartbit", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(63);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].fFaktor", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_float_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].iOffset", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].iPlausibel", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucBitMask", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucSendRate", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(254);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfWertMask", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfSwap", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfEnable", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfDirection", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[0]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[1]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bOrderActive", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypDerating", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypNoxSensor", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            return ((DataPosition + 6) == (Offset + SizeCanIn8()));
        }
        private UInt16 SizeCanIn9() { return 260; }
        private bool ImportCanIn9(UInt16 Offset, ref byte[] Data)
        {
            String msg = "";
            int i = 0;
            UInt16 ItemPosition = 0;
            UInt16 DataPosition = 0;
            if (Offset > 6)
            {
                DataPosition = (UInt16)(Offset - 6);   // block header = 6 abziehen
            }
            else { return false; }
            mData = Data;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiCRC", TaskDataType.type_hex_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiExpire", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(1000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiStacksize", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(512);
            mItem[ItemPosition].SetPlausibilityMax(512);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucPrio", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(80);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucBaudrate", TaskDataType.type_can_baudrate_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(3);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 8; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ulMBID", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_can_id_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucDLC", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMin(1);
                //mItem[ItemPosition].SetPlausibilityMax(8);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].eMRW", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_enum_mrw_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucStartbit", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(63);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].fFaktor", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_float_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].iOffset", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].iPlausibel", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucBitMask", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucSendRate", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(254);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfWertMask", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfSwap", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfEnable", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfDirection", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[0]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[1]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bOrderActive", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypDerating", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypNoxSensor", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 8; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("unsignedMesswertParameter[{0}].ucPlausibilitaetscheckFlag", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(1);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("unsignedMesswertParameter[{0}].uiValueMax", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("unsignedMesswertParameter[{0}].uiValueMin", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;
            }

            for (i = 0; i < 8; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("signedMesswertParameter[{0}].ucPlausibilitaetscheckFlag", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(1);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("signedMesswertParameter[{0}].siValueMax", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("signedMesswertParameter[{0}].siValueMin", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulSystemStartErrorDelay", TaskDataType.type_uint_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(59999);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulFree[0]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulFree[1]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulFree[2]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulFree[3]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            return ((DataPosition + 6) == (Offset + SizeCanIn9()));
        }
        private UInt16 SizeCanIn10() { return 372; }
        private bool ImportCanIn10(UInt16 Offset, ref byte[] Data)
        {
            String msg = "";
            int i = 0;
            UInt16 ItemPosition = 0;
            UInt16 DataPosition = 0;
            if (Offset > 6)
            {
                DataPosition = (UInt16)(Offset - 6);   // block header = 6 abziehen
            }
            else { return false; }
            mData = Data;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiCRC", TaskDataType.type_hex_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiExpire", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(1000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiStacksize", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(512);
            mItem[ItemPosition].SetPlausibilityMax(512);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucPrio", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(80);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucBaudrate", TaskDataType.type_can_baudrate_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(3);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 12; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ulMBID", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_can_id_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucDLC", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMin(1);
                //mItem[ItemPosition].SetPlausibilityMax(8);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].eMRW", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_enum_mrw_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucStartbit", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(63);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].fFaktor", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_float_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].iOffset", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].iPlausibel", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucBitMask", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("saWert[{0}].ucSendRate", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(254);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfWertMask", TaskDataType.type_hex_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(0x0FFF);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfSwap", TaskDataType.type_hex_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(0x0FFF);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfEnable", TaskDataType.type_hex_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(0x0FFF);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bfDirection", TaskDataType.type_hex_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(0x0FFF);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[0]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[1]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("bOrderActive", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypDerating", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypNoxSensor", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 12; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("unsignedMesswertParameter[{0}].ucPlausibilitaetscheckFlag", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(1);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("unsignedMesswertParameter[{0}].uiValueMax", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("unsignedMesswertParameter[{0}].uiValueMin", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;
            }

            for (i = 0; i < 12; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("signedMesswertParameter[{0}].ucPlausibilitaetscheckFlag", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                mItem[ItemPosition].SetPlausibilityMax(1);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("signedMesswertParameter[{0}].siValueMax", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("signedMesswertParameter[{0}].siValueMin", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulSystemStartErrorDelay", TaskDataType.type_uint_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(59999);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulFree[0]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulFree[1]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulFree[2]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulFree[3]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            return ((DataPosition + 6) == (Offset + SizeCanIn10()));
        }
    }
}
