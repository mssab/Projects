/*
 * Object: HJS.ECU.Parameter.TaskConfiguration.Dosieren
 * Description: task configuration for Dosieren
 * 
 * $LastChangedDate: 2015-02-27 15:15:54 +0100 (Fr, 27 Feb 2015) $
 * $LastChangedRevision: 97 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/TaskConfiguration.Dosieren.cs $
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
        private UInt16 SizeDosieren7() { return 41; }
        private bool ImportDosieren7(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(250);
            mItem[ItemPosition].SetPlausibilityMax(250);
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
            mItem[ItemPosition] = new TaskConfigurationItem("fProzentualadd", TaskDataType.type_float_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(1);
            mItem[ItemPosition].SetPlausibilityMax(5.2);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulAdditivtankinhalt", TaskDataType.type_uint_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulPumpenhubvolumen", TaskDataType.type_uint_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMax(65000);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("fStromfaktor", TaskDataType.type_float_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(7.56150);
            mItem[ItemPosition].SetPlausibilityMax(7.56154);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siDeltaI", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(61);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siMaxSoll", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(1700);
            mItem[ItemPosition].SetPlausibilityMax(3000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siMinSoll", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(650);
            mItem[ItemPosition].SetPlausibilityMax(750);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siWendepunktueberhoehung", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(80);
            mItem[ItemPosition].SetPlausibilityMax(121);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siMinWendepunktStrom", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(800);
            mItem[ItemPosition].SetPlausibilityMax(1050);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siDIDTluft", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(-182);
            mItem[ItemPosition].SetPlausibilityMax(-85);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPulsdauer", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(40);
            mItem[ItemPosition].SetPlausibilityMax(40);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPumpenhubkorrektur", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(100);
            mItem[ItemPosition].SetPlausibilityMax(100);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucNotInterval", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(60);
            mItem[ItemPosition].SetPlausibilityMax(120);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            return ((DataPosition + 6) == (Offset + SizeDosieren7()));
        }
        private UInt16 SizeDosieren9() { return 61; }
        private bool ImportDosieren9(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(250);
            mItem[ItemPosition].SetPlausibilityMax(250);
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
            mItem[ItemPosition] = new TaskConfigurationItem("fProzentualadd", TaskDataType.type_float_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(1);
            mItem[ItemPosition].SetPlausibilityMax(5.2);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulAdditivtankinhalt", TaskDataType.type_uint_32, DataPosition);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulPumpenhubvolumen", TaskDataType.type_uint_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(65000);
            mItem[ItemPosition].SetPlausibilityMax(65000);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("fStromfaktor", TaskDataType.type_float_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(7.56150);
            mItem[ItemPosition].SetPlausibilityMax(7.56154);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siDeltaI", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(61);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siMaxSoll", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(1700);
            mItem[ItemPosition].SetPlausibilityMax(3000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siMinSoll", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(650);
            mItem[ItemPosition].SetPlausibilityMax(750);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siWendepunktueberhoehung", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(80);
            mItem[ItemPosition].SetPlausibilityMax(121);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siMinWendepunktStrom", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(800);
            mItem[ItemPosition].SetPlausibilityMax(1050);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siDIDTluft", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(-182);
            mItem[ItemPosition].SetPlausibilityMax(-85);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPulsdauer", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(40);
            mItem[ItemPosition].SetPlausibilityMax(40);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPumpenhubkorrektur", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(100);
            mItem[ItemPosition].SetPlausibilityMax(100);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucNotInterval", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(60);
            mItem[ItemPosition].SetPlausibilityMax(120);
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

            return ((DataPosition + 6) == (Offset + SizeDosieren9()));
        }
        private UInt16 SizeDosieren10() { return 67; }
        private bool ImportDosieren10(UInt16 Offset, ref byte[] Data)
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
            mItem[ItemPosition].SetPlausibilityMin(250);
            mItem[ItemPosition].SetPlausibilityMax(250);
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
            mItem[ItemPosition] = new TaskConfigurationItem("fProzentualadd", TaskDataType.type_float_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(1);
            mItem[ItemPosition].SetPlausibilityMax(5.2);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ulPumpenhubvolumen", TaskDataType.type_uint_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(65000);
            mItem[ItemPosition].SetPlausibilityMax(65000);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("fStromfaktor", TaskDataType.type_float_32, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(7.56150);
            mItem[ItemPosition].SetPlausibilityMax(7.56154);
            DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siDeltaI", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(50);
            mItem[ItemPosition].SetPlausibilityMax(61);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siMaxSoll", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(1700);
            mItem[ItemPosition].SetPlausibilityMax(3000);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siMinSoll", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(650);
            mItem[ItemPosition].SetPlausibilityMax(750);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siWendepunktueberhoehung", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(80);
            mItem[ItemPosition].SetPlausibilityMax(121);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siMinWendepunktStrom", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(800);
            mItem[ItemPosition].SetPlausibilityMax(1050);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("siDIDTluft", TaskDataType.type_int_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(-182);
            mItem[ItemPosition].SetPlausibilityMax(-85);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPulsdauer", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(40);
            mItem[ItemPosition].SetPlausibilityMax(40);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiPumpenhubkorrektur", TaskDataType.type_uint_16, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(100);
            mItem[ItemPosition].SetPlausibilityMax(100);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucNotInterval", TaskDataType.type_uint_8, DataPosition);
            mItem[ItemPosition].SetPlausibilityMin(60);
            mItem[ItemPosition].SetPlausibilityMax(120);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucEnableAdditiveLevelSensor", TaskDataType.type_uint_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADReserveUpperThreshold", TaskDataType.type_int_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADReserveLowerThreshold", TaskDataType.type_int_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADReserveShortCircuit", TaskDataType.type_int_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("uiADReserveOpenCircuit", TaskDataType.type_int_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            Array.Resize(ref mItem, ItemPosition + 1);
            mItem[ItemPosition] = new TaskConfigurationItem("ucDualCanMailBox", TaskDataType.type_uint_8, DataPosition);
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

            return ((DataPosition + 6) == (Offset + SizeDosieren10()));
        }
    }
}
