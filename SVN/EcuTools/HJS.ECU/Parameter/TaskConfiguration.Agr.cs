/*
 * Object: HJS.ECU.Parameter.TaskConfiguration.Agr
 * Description: task configuration for agr
 * 
 * $LastChangedDate: 2014-11-24 11:34:22 +0100 (Mo, 24 Nov 2014) $
 * $LastChangedRevision: 75 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/TaskConfiguration.Agr.cs $
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
        private UInt16 SizeAgr7() { return 32; }
        private bool ImportAgr7(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(100);
            mItem[ItemPosition].SetPlausibilityMax(100);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiStacksize", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(512);
            mItem[ItemPosition].SetPlausibilityMax(512);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucPrio", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(80);
            mItem[ItemPosition].SetPlausibilityMax(80);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucAgrType", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(3);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucLMMdaempf", TaskDataType.type_uint_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucLMMstart", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(15);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucLMMlimit", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(15);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPWMmin", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPWMmax", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            for (i = 1; i < 6; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("uiLimit{0}", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("ucGain{0}", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucGainMax", TaskDataType.type_uint_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            return ((DataPosition + 6) == (Offset + SizeAgr7()));
        }
        private UInt16 SizeAgr9() { return 52; }
        private bool ImportAgr9(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(100);
            mItem[ItemPosition].SetPlausibilityMax(100);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiStacksize", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(512);
            mItem[ItemPosition].SetPlausibilityMax(512);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucPrio", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(80);
            mItem[ItemPosition].SetPlausibilityMax(80);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucAgrType", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(3);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucLMMdaempf", TaskDataType.type_uint_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucLMMstart", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(15);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucLMMlimit", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(15);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPWMmin", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPWMmax", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            for (i = 1; i < 6; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("uiLimit{0}", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("ucGain{0}", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucGainMax", TaskDataType.type_uint_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

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

            return ((DataPosition + 6) == (Offset + SizeAgr9()));
        }
	}
}
