/*
 * Object: HJS.ECU.Parameter.TaskConfiguration.Kommunikation
 * Description: task configuration for Kommunikation
 * 
 * $LastChangedDate: 2014-05-20 10:02:31 +0200 (Di, 20 Mai 2014) $
 * $LastChangedRevision: 53 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/TaskConfiguration.Kommunikation.cs $
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
        private UInt16 SizeKommunikation7() { return 8; }
        private bool ImportKommunikation7(UInt16 Offset, ref byte[] Data)
        {
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
            mItem[ItemPosition].SetPlausibilityMax(50);
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
            mItem[ItemPosition].SetPlausibilityMax(2);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            return ((DataPosition + 6) == (Offset + SizeKommunikation7()));
        }
        private UInt16 SizeKommunikation9() { return 28; }
        private bool ImportKommunikation9(UInt16 Offset, ref byte[] Data)
        {
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
            mItem[ItemPosition].SetPlausibilityMax(50);
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
            mItem[ItemPosition].SetPlausibilityMax(2);
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

            return ((DataPosition + 6) == (Offset + SizeKommunikation9()));
        }
	}
}
