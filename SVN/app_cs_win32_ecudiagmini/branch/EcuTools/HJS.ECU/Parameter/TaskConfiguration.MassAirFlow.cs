/*
 * Object: HJS.ECU.Parameter.TaskConfiguration.MassAirFlow
 * Description: task configuration for MassAirFlow
 * 
 * $LastChangedDate: 2015-02-27 15:15:54 +0100 (Fr, 27 Feb 2015) $
 * $LastChangedRevision: 97 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/TaskConfiguration.MassAirFlow.cs $
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
        private UInt16 SizeMassAirFlow8() { return 57; }
        private bool ImportMassAirFlow8(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(50);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eFreigabe", TaskDataType.type_enum_mrw_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiKriterium", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiFailSafeTurns", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulKFMafFactor", TaskDataType.type_kf_id_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypeMafFactor", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucKennGroesseX", TaskDataType.type_enum_mrw_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafPressPlausibelunten", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafPressPlausibeloben", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafPressMittelIntervall", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulKFAirPressure", TaskDataType.type_kf_id_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypeAirPressure", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiFailSafeAirPress", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafTempPlausibelunten", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafTempPlausibeloben", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafTempMittelIntervall", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulKFAirTemp", TaskDataType.type_kf_id_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypeAirTemp", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siFailSafeAirTemp", TaskDataType.type_int_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("fDisplacedVolume", TaskDataType.type_float_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiMaxMafOut", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucEnableAGRProhibition", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiAGR_MAFDeactivationLevel", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiAGR_MAFReactivationLevel", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucAGR_DeactivationDelay", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(0);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            return ((DataPosition + 6) == (Offset + SizeMassAirFlow8()));
        }
        private UInt16 SizeMassAirFlow9() { return 177; }
        private bool ImportMassAirFlow9(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(50);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eFreigabe", TaskDataType.type_enum_mrw_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiKriterium", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiFailSafeTurns", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulKFMafFactor", TaskDataType.type_kf_id_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypeMafFactor", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucKennGroesseX", TaskDataType.type_enum_mrw_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafPressPlausibelunten", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafPressPlausibeloben", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafPressMittelIntervall", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulKFAirPressure", TaskDataType.type_kf_id_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypeAirPressure", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiFailSafeAirPress", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafTempPlausibelunten", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafTempPlausibeloben", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafTempMittelIntervall", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulKFAirTemp", TaskDataType.type_kf_id_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypeAirTemp", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siFailSafeAirTemp", TaskDataType.type_int_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("fDisplacedVolume", TaskDataType.type_float_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiMaxMafOut", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucEnableAGRProhibition", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiAGR_MAFDeactivationLevel", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiAGR_MAFReactivationLevel", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucAGR_DeactivationDelay", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(0);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 7; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].eDefMeasureValue", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_enum_mrw_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].siPlausibilityMax", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                mItem[ItemPosition].SetPlausibilityMin(-31999);
                mItem[ItemPosition].SetPlausibilityMax(32999);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].siPlausibilityMin", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                mItem[ItemPosition].SetPlausibilityMin(-31999);
                mItem[ItemPosition].SetPlausibilityMax(32999);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].uiTimeDebounceActive", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].uiTimeDebounceInactive", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiFailSafeSensor", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eAirPressSource", TaskDataType.type_enum_mrw_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eAirTempSource", TaskDataType.type_enum_mrw_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 33; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("ucReserved[{0}]", i);
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

            return ((DataPosition + 6) == (Offset + SizeMassAirFlow9()));
        }
        private UInt16 SizeMassAirFlow10() { return 140; }
        private bool ImportMassAirFlow10(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(50);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("TaskCfg->ucTimeout", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(2);
            mItem[ItemPosition].SetPlausibilityMax(5);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eFreigabe", TaskDataType.type_enum_mrw_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiKriterium", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiFailSafeTurns", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulKFMafFactor", TaskDataType.type_kf_id_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypeMafFactor", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucKennGroesseX", TaskDataType.type_enum_mrw_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafPressPlausibelunten", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafPressPlausibeloben", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafPressMittelIntervall", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulKFAirPressure", TaskDataType.type_kf_id_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypeAirPressure", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiFailSafeAirPress", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafTempPlausibelunten", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafTempPlausibeloben", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADMafTempMittelIntervall", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1023);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulKFAirTemp", TaskDataType.type_kf_id_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eTypeAirTemp", TaskDataType.type_kf_type_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siFailSafeAirTemp", TaskDataType.type_int_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiMaxMafOut", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucEnableAGRProhibition", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(1);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiAGR_MAFDeactivationLevel", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiAGR_MAFReactivationLevel", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucAGR_DeactivationDelay", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(0);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (i = 0; i < 7; i++)
            {
                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].eDefMeasureValue", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_enum_mrw_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].siPlausibilityMax", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                mItem[ItemPosition].SetPlausibilityMin(-31999);
                mItem[ItemPosition].SetPlausibilityMax(32999);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].siPlausibilityMin", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_int_16, DataPosition);
                mItem[ItemPosition].SetPlausibilityMin(-31999);
                mItem[ItemPosition].SetPlausibilityMax(32999);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].uiTimeDebounceActive", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                Array.Resize(ref mItem, ItemPosition + 1);
                msg = String.Format("eMeasureObservation[{0}].uiTimeDebounceInactive", i);
                mItem[ItemPosition] = new TaskConfigurationItem(msg, TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;
            }

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiFailSafeSensor", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eAirPressSource", TaskDataType.type_enum_mrw_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("eAirTempSource", TaskDataType.type_enum_mrw_8, DataPosition);
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

            return ((DataPosition + 6) == (Offset + SizeMassAirFlow10()));
        }
    }
}
