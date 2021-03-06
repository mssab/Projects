/*
 * Object: HJS.ECU.Parameter.TaskConfiguration.CanCom
 * Description: task configuration for CanCom
 * 
 * $LastChangedDate: 2014-11-24 11:34:22 +0100 (Mo, 24 Nov 2014) $
 * $LastChangedRevision: 75 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/TaskConfiguration.CanCom.cs $
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
        private UInt16 SizeCanCom7() { return 155; }
        private bool ImportCanCom7(UInt16 Offset, ref byte[] Data)
        {
            String msg = "";
            UInt16 ItemPosition = 0;
            UInt16 DataPosition = 0;
            int i = 0;
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
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(2);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiStacksize", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(512);
            mItem[ItemPosition].SetPlausibilityMax(512);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucPrio", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(50);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 4; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("aulMesswerte[{0}]", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_hex_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;
            }

            for (i = 0; i < 4; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("aulRechenwerte[{0}]", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_hex_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiSeedOffset", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiKeyOffset", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[0]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[1]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucBaudrate", TaskDataType.type_can_baudrate_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucNetworkSourceAdress", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("DiagConf", TaskDataType.type_uint_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 100; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("aucTextInfo[{0}]", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_hex_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;
            }

            return ((DataPosition + 6) == (Offset + SizeCanCom7()));
        }
        private UInt16 SizeCanCom9() { return 175; }
        private bool ImportCanCom9(UInt16 Offset, ref byte[] Data)
        {
            String msg = "";
            UInt16 ItemPosition = 0;
            UInt16 DataPosition = 0;
            int i = 0;
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
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(2);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiStacksize", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(512);
            mItem[ItemPosition].SetPlausibilityMax(512);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucPrio", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(50);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 4; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("aulMesswerte[{0}]", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_hex_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;
            }

            for (i = 0; i < 4; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("aulRechenwerte[{0}]", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_hex_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiSeedOffset", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiKeyOffset", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[0]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("aulShowErr[1]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucBaudrate", TaskDataType.type_can_baudrate_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucNetworkSourceAdress", TaskDataType.type_hex_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("DiagConf", TaskDataType.type_uint_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 100; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("aucTextInfo[{0}]", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_hex_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;
            }

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

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulFree[4]", TaskDataType.type_hex_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            return ((DataPosition + 6) == (Offset + SizeCanCom9()));
        }
	}
}
