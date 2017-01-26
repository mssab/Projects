/*
 * Object: HJS.ECU.Parameter.TaskConfiguration
 * Description: Base class of task configuration
 * 
 * $LastChangedDate: 2016-06-07 09:55:50 +0200 (Di, 07 Jun 2016) $
 * $LastChangedRevision: 102 $
 * $LastChangedBy: jdr $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/TaskConfiguration.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU.Parameter
{
    /// <summary>Configuration data type</summary>
    public enum TaskDataType
    {
        /// <summary>Data type for 8-Bit unsigned</summary>
        type_uint_8 = 0,
        /// <summary>Data type for 8-Bit signed</summary>
        type_int_8 = 1,
        /// <summary>Data type for 16-Bit unsigned</summary>
        type_uint_16 = 2,
        /// <summary>Data type for 16-Bit signed</summary>
        type_int_16 = 3,
        /// <summary>Data type for 32-Bit unsigned</summary>
        type_uint_32 = 4,
        /// <summary>Data type for 32-Bit signed</summary>
        type_int_32 = 5,
        /// <summary>Data type for float 32-Bit</summary>
        type_float_32 = 6,
        /// <summary>Data type for can identifier</summary>
        type_can_id_32 = 7,
        /// <summary>Data type for measurement value</summary>
        type_enum_mrw_8 = 8,
        /// <summary>Data type for datamap identifier</summary>
        type_kf_id_32 = 9,
        /// <summary>Data type for datamap identifier</summary>
        type_kf_type_8 = 10,
        /// <summary>Data type for 8-Bit hexadecimal value</summary>
        type_hex_8 = 11,
        /// <summary>Data type for 16-Bit hexadecimal value</summary>
        type_hex_16 = 12,
        /// <summary>Data type for 32-Bit hexadecimal value</summary>
        type_hex_32 = 13,
        /// <summary>Data type for gain amplification identifier for PSoC</summary>
        type_psoc_gain_8 = 14,
        /// <summary>Data type for fuel sensor signal type</summary>
        type_tank_signal_8 = 15,
        /// <summary>Data type for can baudrate identifier</summary>
        type_can_baudrate_8 = 16
    }

    /// <summary>Base class of task configuration</summary>
    public partial class TaskConfiguration
    {
        /// <summary>Data byte array</summary>
        protected byte[] mData;
        /// <summary>Task identifier</summary>
        protected TaskIdentifier mTaskIdentifierNumber;
        /// <summary>Configuration items</summary>
        protected TaskConfigurationItem[] mItem;

        /// <summary>Table of CRC-16 Polynomial (0x8005) for byte wise usage</summary>
        #region Crc16PolynomialTable
        private static UInt16[] mCrc16Table = new UInt16[256]{
            0x0000, 0xC0C1, 0xC181, 0x0140, 0xC301, 0x03C0, 0x0280, 0xC241,
            0xC601, 0x06C0, 0x0780, 0xC741, 0x0500, 0xC5C1, 0xC481, 0x0440,
            0xCC01, 0x0CC0, 0x0D80, 0xCD41, 0x0F00, 0xCFC1, 0xCE81, 0x0E40,
            0x0A00, 0xCAC1, 0xCB81, 0x0B40, 0xC901, 0x09C0, 0x0880, 0xC841,
            0xD801, 0x18C0, 0x1980, 0xD941, 0x1B00, 0xDBC1, 0xDA81, 0x1A40,
            0x1E00, 0xDEC1, 0xDF81, 0x1F40, 0xDD01, 0x1DC0, 0x1C80, 0xDC41,
            0x1400, 0xD4C1, 0xD581, 0x1540, 0xD701, 0x17C0, 0x1680, 0xD641,
            0xD201, 0x12C0, 0x1380, 0xD341, 0x1100, 0xD1C1, 0xD081, 0x1040,
            0xF001, 0x30C0, 0x3180, 0xF141, 0x3300, 0xF3C1, 0xF281, 0x3240,
            0x3600, 0xF6C1, 0xF781, 0x3740, 0xF501, 0x35C0, 0x3480, 0xF441,
            0x3C00, 0xFCC1, 0xFD81, 0x3D40, 0xFF01, 0x3FC0, 0x3E80, 0xFE41,
            0xFA01, 0x3AC0, 0x3B80, 0xFB41, 0x3900, 0xF9C1, 0xF881, 0x3840,
            0x2800, 0xE8C1, 0xE981, 0x2940, 0xEB01, 0x2BC0, 0x2A80, 0xEA41,
            0xEE01, 0x2EC0, 0x2F80, 0xEF41, 0x2D00, 0xEDC1, 0xEC81, 0x2C40,
            0xE401, 0x24C0, 0x2580, 0xE541, 0x2700, 0xE7C1, 0xE681, 0x2640,
            0x2200, 0xE2C1, 0xE381, 0x2340, 0xE101, 0x21C0, 0x2080, 0xE041,
            0xA001, 0x60C0, 0x6180, 0xA141, 0x6300, 0xA3C1, 0xA281, 0x6240,
            0x6600, 0xA6C1, 0xA781, 0x6740, 0xA501, 0x65C0, 0x6480, 0xA441,
            0x6C00, 0xACC1, 0xAD81, 0x6D40, 0xAF01, 0x6FC0, 0x6E80, 0xAE41,
            0xAA01, 0x6AC0, 0x6B80, 0xAB41, 0x6900, 0xA9C1, 0xA881, 0x6840,
            0x7800, 0xB8C1, 0xB981, 0x7940, 0xBB01, 0x7BC0, 0x7A80, 0xBA41,
            0xBE01, 0x7EC0, 0x7F80, 0xBF41, 0x7D00, 0xBDC1, 0xBC81, 0x7C40,
            0xB401, 0x74C0, 0x7580, 0xB541, 0x7700, 0xB7C1, 0xB681, 0x7640,
            0x7200, 0xB2C1, 0xB381, 0x7340, 0xB101, 0x71C0, 0x7080, 0xB041,
            0x5000, 0x90C1, 0x9181, 0x5140, 0x9301, 0x53C0, 0x5280, 0x9241,
            0x9601, 0x56C0, 0x5780, 0x9741, 0x5500, 0x95C1, 0x9481, 0x5440,
            0x9C01, 0x5CC0, 0x5D80, 0x9D41, 0x5F00, 0x9FC1, 0x9E81, 0x5E40,
            0x5A00, 0x9AC1, 0x9B81, 0x5B40, 0x9901, 0x59C0, 0x5880, 0x9841,
            0x8801, 0x48C0, 0x4980, 0x8941, 0x4B00, 0x8BC1, 0x8A81, 0x4A40,
            0x4E00, 0x8EC1, 0x8F81, 0x4F40, 0x8D01, 0x4DC0, 0x4C80, 0x8C41,
            0x4400, 0x84C1, 0x8581, 0x4540, 0x8701, 0x47C0, 0x4680, 0x8641,
            0x8201, 0x42C0, 0x4380, 0x8341, 0x4100, 0x81C1, 0x8081, 0x4040
        };
        #endregion

        /// <summary>default constuctor</summary>
        public TaskConfiguration(TaskIdentifier id)
        {
            mTaskIdentifierNumber = id;
        }

        /// <summary>Generate CRC16 checksum</summary>
        /// <param name="compatibility">Compatibility required for single task sizes</param>
        /// <returns>Checksum, or 0xFFFF on error</returns>
        public UInt16 GenerateChecksum(byte compatibility)
        {
            UInt16 Char = 0;
            UInt16 Position;
            UInt16 ShiftRegister = 0;
            UInt16 taskStart = 0;
            UInt16 taskEnde = 0;

            if (mData == null) return 0xFFFF;
            if (mData.Length == 0) return 0xFFFF;
            if (mData == null)
            {
                ShiftRegister = 0;
            }
            else
            {
                taskStart = (UInt16)(mItem[1].Offset); // nach der checksumme anfangen!
                taskEnde = (UInt16)(taskStart + GetByteUsage(compatibility) - 2); // 2 bytes checksumme abziehen!
                for (Position = taskStart; Position < taskEnde; Position++)
                {
                    Char = mData[Position];
                    ShiftRegister = (UInt16)(mCrc16Table[(int)((byte)Char ^ (byte)ShiftRegister)] ^ (byte)(ShiftRegister >> 8));
                }
            }
            taskEnde = (UInt16)mTaskIdentifierNumber;
            return (UInt16)(ShiftRegister ^ (UInt16)mTaskIdentifierNumber);
        }

        /// <summary>Get checksum stored in data / header</summary>
        /// <returns>16-bit-checksum from memory</returns>
        public UInt16 GetChecksum()
        {
            if (mItem == null) return 0;
            return (UInt16)mItem[0].GetValue(ref mData);
        }

        /// <summary>Generate and set task checksum</summary>
        /// <param name="compatibility">Compatibility required for single task sizes</param>
        /// <returns>True on success</returns>
        public bool SetChecksum(byte compatibility)
        {
            return mItem[0].SetValue(ref mData, (double)GenerateChecksum(compatibility));
        }

        /// <summary>Get identifier of task</summary>
        /// <returns>Task name</returns>
        public TaskIdentifier GetTaskId()
        {
            return mTaskIdentifierNumber;
        }

        /// <summary>Import task items from byte array for compatibility 8</summary>
        /// <param name="Offset">Offset of task data in byte array</param>
        /// <param name="Data">Reference to configuration block byte array</param>
        public bool Import8(UInt16 Offset, ref byte[] Data)
        {
            bool bRet = true;
            switch (mTaskIdentifierNumber)
            {
                case TaskIdentifier.taskPreDiagnose:
                    if (ImportPreDiagnose7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskPostDiagnose:
                    if (ImportPostDiagnose7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskTankgeber:
                    if (ImportTankgeber7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskKommunikation:
                    if (ImportKommunikation7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskIo:
                    if (ImportIoTask7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskDosieren:
                    if (ImportDosieren7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAdditivierung:
                    if (ImportAdditivierung7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskVertWatch:
                    if (ImportVertWatch7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladPro:
                    if (ImportBeladungPro7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskCanIn:
                    if (ImportCanIn8(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladMittel:
                    if (ImportBeladungMittel7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAcquisition:
                    if (ImportAcquisition7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskTurnspeed:
                    if (ImportTurnSpeed7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladKennfeld:
                    if (ImportBeladKennfeld7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskRegenerieren:
                    if (ImportRegeneration7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskHeizen:
                    if (ImportHeizen7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAgr:
                    if (ImportAgr7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskDrivePattern:
                    if (ImportDrivePattern7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladLuftmasse:
                    if (ImportBeladungLuftmasse7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladCRT:
                    if (ImportBeladCrt7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskCanCom:
                    if (ImportCanCom7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskIcDosing:
                    if (ImportIcDosing7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskSaeComm:
                    if (ImportSaeComm7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAplSae:
                    if (ImportAplSae7(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskMassAirFlow:
                    if (ImportMassAirFlow8(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskGrundfos:
                    if (ImportGrundfos9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskStaudruck:
                case TaskIdentifier.taskScrHeiz:
                case TaskIdentifier.taskCAN2Com:
                case TaskIdentifier.taskSelfRegeneration:
                case TaskIdentifier.taskXCP:
                case TaskIdentifier.taskInvalid:
                    //
                    break;
            }
            return bRet;
        }

        /// <summary>Import task items from byte array for compatibility 9</summary>
        /// <param name="Offset">Offset of task data in byte array</param>
        /// <param name="Data">Reference to configuration block byte array</param>
        public bool Import9(UInt16 Offset, ref byte[] Data)
        {
            bool bRet = true;
            switch (mTaskIdentifierNumber)
            {
                case TaskIdentifier.taskPreDiagnose:
                    if (ImportPreDiagnose9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskPostDiagnose:
                    if (ImportPostDiagnose9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskTankgeber:
                    if (ImportTankgeber9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskKommunikation:
                    if (ImportKommunikation9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskIo:
                    if (ImportIoTask9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskDosieren:
                    if (ImportDosieren9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAdditivierung:
                    if (ImportAdditivierung9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskVertWatch:
                    if (ImportVertWatch9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladPro:
                    if (ImportBeladungPro9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskCanIn:
                    if (ImportCanIn9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladMittel:
                    if (ImportBeladungMittel9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAcquisition:
                    if (ImportAcquisition9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskTurnspeed:
                    if (ImportTurnSpeed9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladKennfeld:
                    if (ImportBeladKennfeld9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskRegenerieren:
                    if (ImportRegeneration9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskHeizen:
                    if (ImportHeizen9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAgr:
                    if (ImportAgr9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskDrivePattern:
                    if (ImportDrivePattern9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladLuftmasse:
                    if (ImportBeladungLuftmasse9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladCRT:
                    if (ImportBeladCrt9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskCanCom:
                    if (ImportCanCom9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskIcDosing:
                    if (ImportIcDosing9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskSaeComm:
                    if (ImportSaeComm9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAplSae:
                    if (ImportAplSae9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskMassAirFlow:
                    if (ImportMassAirFlow9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskGrundfos:
                    if (ImportGrundfos9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskStaudruck:
                    if (ImportStaudruck9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskScrHeiz:
                    if (ImportScrHeiz9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskCAN2Com:
                    if (ImportCan2Com9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskInvalid:
                    //
                    break;
            }
            return bRet;
        }

        /// <summary>Import task items from byte array for compatibility 10</summary>
        /// <param name="Offset">Offset of task data in byte array</param>
        /// <param name="Data">Reference to configuration block byte array</param>
        public bool Import10(UInt16 Offset, ref byte[] Data)
        {
            bool bRet = true;
            switch (mTaskIdentifierNumber)
            {
                case TaskIdentifier.taskPreDiagnose:
                    if (ImportPreDiagnose9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskPostDiagnose:
                    if (ImportPostDiagnose10(Offset, ref Data) == false)   //wtr/jdr 2016-01, alt: ImportPreDiagnose9()
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskTankgeber:
                    if (ImportTankgeber10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskKommunikation:
                    if (ImportKommunikation9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskIo:
                    if (ImportIoTask9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskDosieren:
                    if (ImportDosieren10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAdditivierung:
                    if (ImportAdditivierung10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskVertWatch:
                    if (ImportVertWatch9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladPro:
                    if (ImportBeladungPro9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskCanIn:
                    if (ImportCanIn10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladMittel:
                    if (ImportBeladungMittel9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAcquisition:
                    if (ImportAcquisition9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskTurnspeed:
                    if (ImportTurnSpeed9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladKennfeld:
                    if (ImportBeladKennfeld9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskRegenerieren:
                    if (ImportRegeneration10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskHeizen:
                    if (ImportHeizen9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAgr:
                    if (ImportAgr9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskDrivePattern:
                    if (ImportDrivePattern9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladLuftmasse:
                    if (ImportBeladungLuftmasse9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskBeladCRT:
                    if (ImportBeladCrt9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskCanCom:
                    if (ImportCanCom9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskIcDosing:
                    if (ImportIcDosing9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskSaeComm:
                    if (ImportSaeComm9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskAplSae:
                    if (ImportAplSae9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskMassAirFlow:
                    if (ImportMassAirFlow10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskGrundfos:
                    if (ImportGrundfos10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskStaudruck:
                    if (ImportStaudruck10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskScrHeiz:
                    if (ImportScrHeiz9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskCAN2Com:
                    if (ImportCan2Com9(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskSelfRegeneration:
                    if (ImportSelfRegeneration10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskXCP:
                    if (ImportXcpTask10(Offset, ref Data) == false)
                    {
                        bRet = false;
                    }
                    break;
                case TaskIdentifier.taskInvalid:
                    //
                    break;
            }
            return bRet;
        }

        /// <summary>Number of task items</summary>
        /// <returns>Number of task items</returns>
        public int GetNumber()
        {
            if (mItem == null)
            {
                return 0;
            }
            else
            {
                return mItem.Length;
            }
        }

        /// <summary>Get name of task item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Name of task item</returns>
        public string GetItemName(int position)
        {
            if (mItem != null)
            {
                if (position < mItem.Length)
                {
                    return mItem[position].Name;

                }
                else
                {
                    return "N/A";
                }
            }
            else
            {
                return "N/A";
            }
        }

        /// <summary>Get value of task item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of task item as double</returns>
        public double GetItemValue(int position)
        {
            if (mItem != null)
            {
                if (position < mItem.Length)
                {
                    return mItem[position].GetValue(ref mData);

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        
        /// <summary>Set value of task item</summary>
        /// <param name="position">Position of item</param>
        /// <param name="NewValue">New Value to be set</param>
        /// <returns>True on success</returns>
        public bool SetItemValue(int position, double NewValue)
        {
            if (mItem != null)
            {
                if (position < mItem.Length)
                {
                    return mItem[position].SetValue(ref mData, NewValue);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>Get value string of task item</summary>
        /// <param name="position">Position of item</param>
        /// <param name="Compatibility">Compatibility of firmware</param>
        /// <returns>Value of task item as string</returns>
        public string GetItemValueString(int position, byte Compatibility)
        {
            if (mItem != null)
            {
                if (position < mItem.Length)
                {
                    return mItem[position].GetValueString(ref mData, Compatibility);

                }
                else
                {
                    return "N/A";
                }
            }
            else
            {
                return "N/A";
            }
        }

        /// <summary>Get data type of task item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Data type of task item</returns>
        public TaskDataType GetItemType(int position)
        {
            if (mItem != null)
            {
                if (position < mItem.Length)
                {
                    return mItem[position].DataType;

                }
                else
                {
                    return TaskDataType.type_uint_8;
                }
            }
            else
            {
                return TaskDataType.type_uint_8;
            }
        }

        /// <summary>Get bytes used by task compatible to configuration version</summary>
        /// <param name="ConfigVersion">Compatibility of task</param>
        /// <returns>Number of used task bytes, or 0 on any error</returns>
        public UInt16 GetByteUsage(byte ConfigVersion)
        {
            UInt16 retVal = 0;
            switch (ConfigVersion)
            {
                case 8:
                    switch (mTaskIdentifierNumber)
                    {
                        case TaskIdentifier.taskPreDiagnose:
                            retVal = SizePreDiagnose7();
                            break;
                        case TaskIdentifier.taskPostDiagnose:
                            retVal = SizePostDiagnose7();
                            break;
                        case TaskIdentifier.taskTankgeber:
                            retVal = SizeTankgeber7();
                            break;
                        case TaskIdentifier.taskKommunikation:
                            retVal = SizeKommunikation7();
                            break;
                        case TaskIdentifier.taskIo:
                            retVal = SizeIoTask7();
                            break;
                        case TaskIdentifier.taskDosieren:
                            retVal = SizeDosieren7();
                            break;
                        case TaskIdentifier.taskAdditivierung:
                            retVal = SizeAdditivierung7();
                            break;
                        case TaskIdentifier.taskVertWatch:
                            retVal = SizeVertWatch7();
                            break;
                        case TaskIdentifier.taskBeladPro:
                            retVal = SizeBeladungPro7();
                            break;
                        case TaskIdentifier.taskCanIn:
                            retVal = SizeCanIn8();
                            break;
                        case TaskIdentifier.taskBeladMittel:
                            retVal = SizeBeladungMittel7();
                            break;
                        case TaskIdentifier.taskAcquisition:
                            retVal = SizeAcquisition7();
                            break;
                        case TaskIdentifier.taskTurnspeed:
                            retVal = SizeTurnspeed7();
                            break;
                        case TaskIdentifier.taskBeladKennfeld:
                            retVal = SizeBeladKennfeld7();
                            break;
                        case TaskIdentifier.taskRegenerieren:
                            retVal = SizeRegeneration7();
                            break;
                        case TaskIdentifier.taskHeizen:
                            retVal = SizeHeizen7();
                            break;
                        case TaskIdentifier.taskAgr:
                            retVal = SizeAgr7();
                            break;
                        case TaskIdentifier.taskDrivePattern:
                            retVal = SizeDrivePattern7();
                            break;
                        case TaskIdentifier.taskBeladLuftmasse:
                            retVal = SizeBeladungLuftmasse7();
                            break;
                        case TaskIdentifier.taskBeladCRT:
                            retVal = SizeBeladCrt7();
                            break;
                        case TaskIdentifier.taskCanCom:
                            retVal = SizeCanCom7();
                            break;
                        case TaskIdentifier.taskIcDosing:
                            retVal = SizeIcDosing7();
                            break;
                        case TaskIdentifier.taskSaeComm:
                            retVal = SizeSaeComm7();
                            break;
                        case TaskIdentifier.taskAplSae:
                            retVal = SizeAplSae7();
                            break;
                        case TaskIdentifier.taskMassAirFlow:
                            retVal = SizeMassAirFlow8();
                            break;
                        case TaskIdentifier.taskGrundfos:
                            retVal = SizeGrundfos7();
                            break;
                        case TaskIdentifier.taskStaudruck:
                        case TaskIdentifier.taskScrHeiz:
                        case TaskIdentifier.taskCAN2Com:
                        case TaskIdentifier.taskInvalid:
                            retVal = 0;
                            break;
                    }
                    break;
                case 9:
                    switch (mTaskIdentifierNumber)
                    {
                        case TaskIdentifier.taskPreDiagnose:
                            retVal = SizePreDiagnose9();
                            break;
                        case TaskIdentifier.taskPostDiagnose:
                            retVal = SizePostDiagnose9();
                            break;
                        case TaskIdentifier.taskTankgeber:
                            retVal = SizeTankgeber9();
                            break;
                        case TaskIdentifier.taskKommunikation:
                            retVal = SizeKommunikation9();
                            break;
                        case TaskIdentifier.taskIo:
                            retVal = SizeIoTask9();
                            break;
                        case TaskIdentifier.taskDosieren:
                            retVal = SizeDosieren9();
                            break;
                        case TaskIdentifier.taskAdditivierung:
                            retVal = SizeAdditivierung9();
                            break;
                        case TaskIdentifier.taskVertWatch:
                            retVal = SizeVertWatch9();
                            break;
                        case TaskIdentifier.taskBeladPro:
                            retVal = SizeBeladungPro9();
                            break;
                        case TaskIdentifier.taskCanIn:
                            retVal = SizeCanIn9();
                            break;
                        case TaskIdentifier.taskBeladMittel:
                            retVal = SizeBeladungMittel9();
                            break;
                        case TaskIdentifier.taskAcquisition:
                            retVal = SizeAcquisition9();
                            break;
                        case TaskIdentifier.taskTurnspeed:
                            retVal = SizeTurnspeed9();
                            break;
                        case TaskIdentifier.taskBeladKennfeld:
                            retVal = SizeBeladKennfeld9();
                            break;
                        case TaskIdentifier.taskRegenerieren:
                            retVal = SizeRegeneration9();
                            break;
                        case TaskIdentifier.taskHeizen:
                            retVal = SizeHeizen9();
                            break;
                        case TaskIdentifier.taskAgr:
                            retVal = SizeAgr9();
                            break;
                        case TaskIdentifier.taskDrivePattern:
                            retVal = SizeDrivePattern9();
                            break;
                        case TaskIdentifier.taskBeladLuftmasse:
                            retVal = SizeBeladungLuftmasse9();
                            break;
                        case TaskIdentifier.taskBeladCRT:
                            retVal = SizeBeladCrt9();
                            break;
                        case TaskIdentifier.taskCanCom:
                            retVal = SizeCanCom9();
                            break;
                        case TaskIdentifier.taskIcDosing:
                            retVal = SizeIcDosing9();
                            break;
                        case TaskIdentifier.taskSaeComm:
                            retVal = SizeSaeComm9();
                            break;
                        case TaskIdentifier.taskAplSae:
                            retVal = SizeAplSae9();
                            break;
                        case TaskIdentifier.taskMassAirFlow:
                            retVal = SizeMassAirFlow9();
                            break;
                        case TaskIdentifier.taskGrundfos:
                            retVal = SizeGrundfos9();
                            break;
                        case TaskIdentifier.taskStaudruck:
                            retVal = SizeStaudruck9();
                            break;
                        case TaskIdentifier.taskScrHeiz:
                            retVal = SizeScrHeiz9();
                            break;
                        case TaskIdentifier.taskCAN2Com:
                            retVal = SizeCan2Com9();
                            break;
                        case TaskIdentifier.taskInvalid:
                            retVal = 0;
                            break;
                     }
                    break;
                case 10:
                    switch (mTaskIdentifierNumber)
                    {
                        case TaskIdentifier.taskPreDiagnose:
                            retVal = SizePreDiagnose9();
                            break;
                        case TaskIdentifier.taskPostDiagnose:
                            retVal = SizePostDiagnose9();
                            break;
                        case TaskIdentifier.taskTankgeber:
                            retVal = SizeTankgeber9();
                            break;
                        case TaskIdentifier.taskKommunikation:
                            retVal = SizeKommunikation9();
                            break;
                        case TaskIdentifier.taskIo:
                            retVal = SizeIoTask9();
                            break;
                        case TaskIdentifier.taskDosieren:
                            retVal = SizeDosieren10();
                            break;
                        case TaskIdentifier.taskAdditivierung:
                            retVal = SizeAdditivierung10();
                            break;
                        case TaskIdentifier.taskVertWatch:
                            retVal = SizeVertWatch9();
                            break;
                        case TaskIdentifier.taskBeladPro:
                            retVal = SizeBeladungPro9();
                            break;
                        case TaskIdentifier.taskCanIn:
                            retVal = SizeCanIn9();
                            break;
                        case TaskIdentifier.taskBeladMittel:
                            retVal = SizeBeladungMittel9();
                            break;
                        case TaskIdentifier.taskAcquisition:
                            retVal = SizeAcquisition9();
                            break;
                        case TaskIdentifier.taskTurnspeed:
                            retVal = SizeTurnspeed9();
                            break;
                        case TaskIdentifier.taskBeladKennfeld:
                            retVal = SizeBeladKennfeld9();
                            break;
                        case TaskIdentifier.taskRegenerieren:
                            retVal = SizeRegeneration10();
                            break;
                        case TaskIdentifier.taskHeizen:
                            retVal = SizeHeizen9();
                            break;
                        case TaskIdentifier.taskAgr:
                            retVal = SizeAgr9();
                            break;
                        case TaskIdentifier.taskDrivePattern:
                            retVal = SizeDrivePattern9();
                            break;
                        case TaskIdentifier.taskBeladLuftmasse:
                            retVal = SizeBeladungLuftmasse9();
                            break;
                        case TaskIdentifier.taskBeladCRT:
                            retVal = SizeBeladCrt9();
                            break;
                        case TaskIdentifier.taskCanCom:
                            retVal = SizeCanCom9();
                            break;
                        case TaskIdentifier.taskIcDosing:
                            retVal = SizeIcDosing9();
                            break;
                        case TaskIdentifier.taskSaeComm:
                            retVal = SizeSaeComm9();
                            break;
                        case TaskIdentifier.taskAplSae:
                            retVal = SizeAplSae9();
                            break;
                        case TaskIdentifier.taskMassAirFlow:
                            retVal = SizeMassAirFlow10();
                            break;
                        case TaskIdentifier.taskGrundfos:
                            retVal = SizeGrundfos10();
                            break;
                        case TaskIdentifier.taskStaudruck:
                            retVal = SizeStaudruck10();
                            break;
                        case TaskIdentifier.taskScrHeiz:
                            retVal = SizeScrHeiz9();
                            break;
                        case TaskIdentifier.taskCAN2Com:
                            retVal = SizeCan2Com9();
                            break;
                        case TaskIdentifier.taskSelfRegeneration:
                            retVal = SizeSelfRegeneration10();
                            break;
                        case TaskIdentifier.taskXCP:
                            retVal = SizeXcpTask10();
                            break;
                        case TaskIdentifier.taskInvalid:
                            retVal = 0;
                            break;
                    }
                    break;
                default:
                    retVal = 0;
                    break;
            }
            return retVal;
        }

        /// <summary>Check task items versus palausbility</summary>
        /// <param name="ConfigVersion">Compatibility of task</param>
        /// <returns>Empty string on success, else error text</returns>
        public string CheckItems(byte ConfigVersion)
        {
            string ret = "";
            if (mItem != null)
            {
                for(int position = 0; position < mItem.Length; position++)
                {
                    ret = mItem[position].CheckValue(ref mData, ConfigVersion);
                    if(!String.IsNullOrEmpty(ret))
                    {
                        return String.Format("{0}.{1}", mTaskIdentifierNumber, ret);
                    }
                }
            }
            else
            {
                return String.Format("Task {0} hat keine Items", mTaskIdentifierNumber);
            }
            return ret;
        }
    }
}
