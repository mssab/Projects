/*
 * Object: HJS.ECU.Parameter.TaskConfiguration.PreDiagnose
 * Description: task configuration for PreDiagnose
 * 
 * $LastChangedDate: 2014-05-20 10:02:31 +0200 (Di, 20 Mai 2014) $
 * $LastChangedRevision: 53 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/TaskConfiguration.PreDiagnose.cs $
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
        private UInt16 SizePreDiagnose7() { return 8; }
        private bool ImportPreDiagnose7(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(6);
            mItem[ItemPosition].SetPlausibilityMax(6);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiStacksize", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(512);
            mItem[ItemPosition].SetPlausibilityMax(512);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucPrio", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(71);
            mItem[ItemPosition].SetPlausibilityMax(71);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(5);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            return ((DataPosition + 6) == (Offset + SizePreDiagnose7()));
        }
        private UInt16 SizePreDiagnose9() { return 28; }
        private bool ImportPreDiagnose9(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(6);
            mItem[ItemPosition].SetPlausibilityMax(6);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->uiStacksize", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(512);
            mItem[ItemPosition].SetPlausibilityMax(512);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucPrio", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(71);
            mItem[ItemPosition].SetPlausibilityMax(71);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(5);
            mItem[ItemPosition].SetPlausibilityMax(5);
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

            return ((DataPosition + 6) == (Offset + SizePreDiagnose9()));
        }
	}
}
