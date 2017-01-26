/*
 * Object: HJS.ECU.EmpiricalData
 * Description: Object of empirical data
 * 
 * $LastChangedDate: 2012-09-03 13:45:27 +0200 (Mo, 03 Sep 2012) $
 * $LastChangedRevision: 12 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/tags/Version_1_3_1/lib_cs_win32_hjsecu/EmpiricalDataBlock.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU
{
    /// <summary>
    /// Block object of empirical data
    /// </summary>
    public class EmpiricalDataBlock : Block
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmpiricalDataBlock(){
            Type = BlockId.IdValue1;
            Version = 5;
            DataSize = 0;
        }

        /// <summary>
        /// Read empirical data (block) from byte array
        /// </summary>
        /// <param name="SourceData">Reference to source byte array</param>
        /// <returns>0 on success, see ReturnValue</returns>
        public ReturnValue Parse(ref byte[] SourceData)
        {
            byte[] _buffer = new byte[SourceData.Length + BLOCK_HEADER_SIZE];
            ReturnValue ret = ReturnValue.BlockNotFound;
            //create new header to data without crc
            _buffer[(int)HeaderPosition.Type] = (byte)BlockId.IdValue1;
            _buffer[(int)HeaderPosition.Version] = Version;
            _buffer[(int)HeaderPosition.SizeHighByte] = (byte)(SourceData.Length / 256);
            _buffer[(int)HeaderPosition.SizeLowByte] = (byte)(SourceData.Length % 256);
            _buffer[(int)HeaderPosition.ChecksumHighByte] = 0;
            _buffer[(int)HeaderPosition.ChecksumLowByte] = 0;
            //append data after header
            for (int i = 0; i < SourceData.Length; i++)
            {
                _buffer[BLOCK_HEADER_SIZE + i] = SourceData[i];
            }
            //import from new array
            ret = ReadRaw(ref _buffer, true);
            if (ret == ReturnValue.ChecksumMismatch)
            {
                //create new checksum
                GenerateChecksum();
                ret = ReturnValue.NoError;
            }
            
            return ret;
        }


        /// <summary>
        /// Group names
        /// </summary>
        public enum GroupName
        {
            /// <summary>
            /// Regeneration
            /// </summary>
            Regenerieren = 0,
            /// <summary>
            /// Heater
            /// </summary>
            Heizen = 1,
            /// <summary>
            /// Continous regeneration trap
            /// </summary>
            BeladungCRT = 2,
            /// <summary>
            /// Flash memory
            /// </summary>
            Flash = 3,
            /// <summary>
            /// Additivating
            /// </summary>
            Additivieren = 4,
            /// <summary>
            /// Intelligent continous dosing
            /// </summary>
            IKD = 5,
            /// <summary>
            /// VERT
            /// </summary>
            VERT = 6,
            /// <summary>
            /// Turns and Speeds
            /// </summary>
            Turns = 7,
            /// <summary>
            /// Dosing
            /// </summary>
            Dosieren = 8,
            /// <summary>
            /// In- and outputs
            /// </summary>
            IO = 9,
            /// <summary>
            /// Dirve pattern detection
            /// </summary>
            DPD = 10,
            /// <summary>
            /// Fuel sensor
            /// </summary>
            Tankgeber = 11
        };

        /// <summary>
        /// Get number of values
        /// </summary>
        /// <param name="Group">Enumeration of group</param>
        /// <returns>Number of values</returns>
        public UInt16 GetNumberOfValues(GroupName Group)
        {
            UInt16 ret = 0;
            if (Version > 7)
            {
                // Version not supported!
                ret = 0;
            }
            else
            {
                switch (Group)
                {
                    case GroupName.Regenerieren:
                        ret = 5;
                        break;
                    case GroupName.Heizen:
                        ret = 2;
                        break;
                    case GroupName.BeladungCRT:
                        ret = 39;
                        break;
                    case GroupName.Flash:
                        ret = 8;
                        break;
                    case GroupName.Additivieren:
                        ret = 4;
                        break;
                    case GroupName.IKD:
                        if (Version < 4)
                        {
                            ret = 30;
                        }
                        else
                        {
                            // ab 4 : + D_Work_ICDosing dwork
                            ret = 30;
                        }
                        break;
                    case GroupName.VERT:
                        if (Version < 6)
                        {
                            ret = 23;
                        }
                        else
                        {
                            // ab 6
                            if (Version < 7)
                            {
                                ret = 31;
                            }
                            else
                            {
                                // ab 7
                                ret = 33;
                            }
                        }
                        break;
                    case GroupName.Turns:
                        ret = 2;
                        break;
                    case GroupName.Dosieren:
                        ret = 1;
                        break;
                    case GroupName.IO:
                        if (Version < 6)
                        {
                            ret = 2;
                        }
                        else
                        {
                            // ab 6
                            ret = 3;
                        }
                        break;
                    case GroupName.DPD:
                        ret = 46 + 15;
                        break;
                    case GroupName.Tankgeber:
                        if (Version < 4)
                        {
                            ret = 5;
                        }
                        else
                        {
                            // ab 4
                            ret = 6;
                        }
                        break;
                }
            }
            return ret;
        }
        /// <summary>
        /// Get empirical value as string
        /// </summary>
        /// <param name="Group">Enumeration of group</param>
        /// <param name="Position">Position of empirical value</param>
        /// <returns>Empirical value as string</returns>
        public string GetEmpiricalValue(GroupName Group, UInt16 Position)
        {
            UInt32 ui32 = 0;
            UInt16 ui16 = 0;
            Int16 i16 = 0;
            int offset;
            string ret = "";
            if (DataSize == 0) { return "Keine Daten!"; }
            switch (Group)
            {
                case GroupName.Regenerieren:
                    switch (Position)
                    {
                        case 0:
                            ui32 = (UInt32)(mBlockData[3] * 256);
                            ui32 += mBlockData[2];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[1];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[0];
                            if (ui32 != 0xFFFFFFFF)
                            {
                                ret = String.Format("sReg.ulLastRegRequestTS = {0}", ui32);
                            }
                            else
                            {
                                ret = "sReg.ulLastRegRequestTS = -1";
                            }
                            break;
                        case 1:
                            ui32 = (UInt32)(mBlockData[7] * 256);
                            ui32 += mBlockData[6];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[5];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[4];
                            if (ui32 != 0xFFFFFFFF)
                            {
                                ret = String.Format("sReg.ulLastNotregTS = {0}", ui32);
                            }
                            else
                            {
                                ret = "sReg.ulLastNotregTS = -1";
                            }
                            break;
                        case 2:
                            ret = String.Format("sReg.ucSperranzahl = {0}", mBlockData[8]);
                            break;
                        case 3:
                            ui16 = (UInt16)(mBlockData[10] * 256);
                            ui16 += mBlockData[9];
                            ret = String.Format("sReg.uiUnterdrueckZeit = {0}", ui16);
                            break;
                        case 4:
                            ui16 = (UInt16)(mBlockData[12] * 256);
                            ui16 += mBlockData[11];
                            ret = String.Format("sReg.uiLastEngineRun = {0}", ui16);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.Heizen:
                    switch (Position)
                    {
                        case 0:
                            ui16 = (UInt16)(mBlockData[14] * 256);
                            ui16 += mBlockData[13];
                            ret = String.Format("sHeizen.uiStartsCounter = {0}", ui16);
                            break;
                        case 1:
                            ui32 = (UInt32)(mBlockData[18] * 256);
                            ui32 += mBlockData[17];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[16];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[15];
                            ret = String.Format("sHeizen.ulCompleteJoule = {0}", ui32);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.BeladungCRT:
                    switch (Position)
                    {
                        case 0:
                            ui16 = (UInt16)(mBlockData[20] * 256);
                            ui16 += mBlockData[19];
                            ret = String.Format("sBeladCRT.uiFilterBetriebsstundenCRT = {0}", ui16);
                            break;
                        case 1:
                            ui16 = (UInt16)(mBlockData[22] * 256);
                            ui16 += mBlockData[21];
                            ret = String.Format("sBeladCRT.uiSekCounter = {0}", ui16);
                            break;
                        case 2:
                            ui32 = (UInt32)(mBlockData[26] * 256);
                            ui32 += mBlockData[25];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[24];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[23];
                            ret = String.Format("sBeladCRT.ulDruckSummeCRT = {0}", ui32);
                            break;
                        case 3:
                            ui16 = (UInt16)(mBlockData[28] * 256);
                            ui16 += mBlockData[27];
                            ret = String.Format("sBeladCRT.uiDruckMittelCRT = {0}", ui16);
                            break;
                        case 4:
                            ui16 = (UInt16)(mBlockData[30] * 256);
                            ui16 += mBlockData[29];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCounter = {0}", ui16);
                            break;
                        case 5:
                            ui16 = (UInt16)(mBlockData[32] * 256);
                            ui16 += mBlockData[31];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[0] = {0}", ui16);
                            break;
                        case 6:
                            ui16 = (UInt16)(mBlockData[34] * 256);
                            ui16 += mBlockData[33];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[1] = {0}", ui16);
                            break;
                        case 7:
                            ui16 = (UInt16)(mBlockData[36] * 256);
                            ui16 += mBlockData[35];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[2] = {0}", ui16);
                            break;
                        case 8:
                            ui16 = (UInt16)(mBlockData[38] * 256);
                            ui16 += mBlockData[37];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[3] = {0}", ui16);
                            break;
                        case 9:
                            ui16 = (UInt16)(mBlockData[40] * 256);
                            ui16 += mBlockData[39];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[4] = {0}", ui16);
                            break;
                        case 10:
                            ui16 = (UInt16)(mBlockData[42] * 256);
                            ui16 += mBlockData[41];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[5] = {0}", ui16);
                            break;
                        case 11:
                            ui16 = (UInt16)(mBlockData[44] * 256);
                            ui16 += mBlockData[43];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[6] = {0}", ui16);
                            break;
                        case 12:
                            ui16 = (UInt16)(mBlockData[46] * 256);
                            ui16 += mBlockData[45];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[7] = {0}", ui16);
                            break;
                        case 13:
                            ui16 = (UInt16)(mBlockData[48] * 256);
                            ui16 += mBlockData[47];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[8] = {0}", ui16);
                            break;
                        case 14:
                            ui16 = (UInt16)(mBlockData[50] * 256);
                            ui16 += mBlockData[49];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[9] = {0}", ui16);
                            break;
                        case 15:
                            ui16 = (UInt16)(mBlockData[52] * 256);
                            ui16 += mBlockData[51];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[10] = {0}", ui16);
                            break;
                        case 16:
                            ui16 = (UInt16)(mBlockData[54] * 256);
                            ui16 += mBlockData[53];
                            ret = String.Format("sBeladCRT.uiDruckMittelFeldCRT[11] = {0}", ui16);
                            break;
                        case 17:
                            ui16 = (UInt16)(mBlockData[56] * 256);
                            ui16 += mBlockData[55];
                            ret = String.Format("sBeladCRT.uiDruckMittelCRTVor = {0}", ui16);
                            break;
                        case 18:
                            ui16 = (UInt16)(mBlockData[58] * 256);
                            ui16 += mBlockData[57];
                            ret = String.Format("sBeladCRT.uiDruckMessungCounter = {0}", ui16);
                            break;
                        case 19:
                            ui16 = (UInt16)(mBlockData[60] * 256);
                            ui16 += mBlockData[59];
                            ret = String.Format("sBeladCRT.uiDruckMessungCounter = {0}", ui16);
                            break;
                        case 20:
                            ui32 = (UInt32)(mBlockData[64] * 256);
                            ui32 += mBlockData[63];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[62];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[61];
                            ret = String.Format("sBeladCRT.ulTempKoeffSummeCRT = {0}", ui32);
                            break;
                        case 21:
                            ui16 = (UInt16)(mBlockData[66] * 256);
                            ui16 += mBlockData[65];
                            ret = String.Format("sBeladCRT.uiTempKoeffCRT = {0}", ui16);
                            break;
                        case 22:
                            ui16 = (UInt16)(mBlockData[68] * 256);
                            ui16 += mBlockData[67];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCounter = {0}", ui16);
                            break;
                        case 23:
                            ui16 = (UInt16)(mBlockData[70] * 256);
                            ui16 += mBlockData[69];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[0] = {0}", ui16);
                            break;
                        case 24:
                            ui16 = (UInt16)(mBlockData[72] * 256);
                            ui16 += mBlockData[71];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[1] = {0}", ui16);
                            break;
                        case 25:
                            ui16 = (UInt16)(mBlockData[74] * 256);
                            ui16 += mBlockData[73];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[2] = {0}", ui16);
                            break;
                        case 26:
                            ui16 = (UInt16)(mBlockData[76] * 256);
                            ui16 += mBlockData[75];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[3] = {0}", ui16);
                            break;
                        case 27:
                            ui16 = (UInt16)(mBlockData[78] * 256);
                            ui16 += mBlockData[77];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[4] = {0}", ui16);
                            break;
                        case 28:
                            ui16 = (UInt16)(mBlockData[80] * 256);
                            ui16 += mBlockData[79];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[5] = {0}", ui16);
                            break;
                        case 29:
                            ui16 = (UInt16)(mBlockData[82] * 256);
                            ui16 += mBlockData[81];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[6] = {0}", ui16);
                            break;
                        case 30:
                            ui16 = (UInt16)(mBlockData[84] * 256);
                            ui16 += mBlockData[83];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[7] = {0}", ui16);
                            break;
                        case 31:
                            ui16 = (UInt16)(mBlockData[86] * 256);
                            ui16 += mBlockData[85];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[8] = {0}", ui16);
                            break;
                        case 32:
                            ui16 = (UInt16)(mBlockData[88] * 256);
                            ui16 += mBlockData[87];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[9] = {0}", ui16);
                            break;
                        case 33:
                            ui16 = (UInt16)(mBlockData[90] * 256);
                            ui16 += mBlockData[89];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[10] = {0}", ui16);
                            break;
                        case 34:
                            ui16 = (UInt16)(mBlockData[92] * 256);
                            ui16 += mBlockData[91];
                            ret = String.Format("sBeladCRT.uiTempKoeffFeldCRT[11] = {0}", ui16);
                            break;
                        case 35:
                            ui16 = (UInt16)(mBlockData[94] * 256);
                            ui16 += mBlockData[93];
                            ret = String.Format("sBeladCRT.uiTempMessungCounter = {0}", ui16);
                            break;
                        case 36:
                            ui16 = (UInt16)(mBlockData[96] * 256);
                            ui16 += mBlockData[95];
                            ret = String.Format("sBeladCRT.uiTempTimeCounter = {0}", ui16);
                            break;
                        case 37:
                            ui16 = (UInt16)(mBlockData[98] * 256);
                            ui16 += mBlockData[97];
                            ret = String.Format("sBeladCRT.uiFilterbeladung = {0}", ui16);
                            break;
                        case 38:
                            ui16 = (UInt16)(mBlockData[100] * 256);
                            ui16 += mBlockData[99];
                            ret = String.Format("sBeladCRT.uiTempMinGrenze = {0}", ui16);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.Flash:
                    switch (Position)
                    {
                        case 0:
                            ui16 = (UInt16)(mBlockData[102] * 256);
                            ui16 += mBlockData[101];
                            ret = String.Format("sFlash.uiPsocFlashCounter = {0}", ui16);
                            break;
                        case 1:
                            ui16 = (UInt16)(mBlockData[104] * 256);
                            ui16 += mBlockData[103];
                            ret = String.Format("sFlash.uiSaveValueCt = {0}", ui16);
                            break;
                        case 2:
                            ui16 = (UInt16)(mBlockData[106] * 256);
                            ui16 += mBlockData[105];
                            ret = String.Format("sFlash.uiRingError = {0}", ui16);
                            break;
                        case 3:
                            ui16 = (UInt16)(mBlockData[108] * 256);
                            ui16 += mBlockData[107];
                            ret = String.Format("sFlash.uiAcquiError = {0}", ui16);
                            break;
                        case 4:
                            ui16 = (UInt16)(mBlockData[110] * 256);
                            ui16 += mBlockData[109];
                            ret = String.Format("sFlash.uiMasterreset = {0}", ui16);
                            break;
                        case 5:
                            ui16 = (UInt16)(mBlockData[112] * 256);
                            ui16 += mBlockData[111];
                            ret = String.Format("sFlash.uiMasterresetFailCount = {0}", ui16);
                            break;
                        case 6:
                            ui16 = (UInt16)(mBlockData[114] * 256);
                            ui16 += mBlockData[113];
                            ret = String.Format("sFlash.uiValueSafeError = {0}", ui16);
                            break;
                        case 7:
                            ui32 = (UInt32)(mBlockData[118] * 256);
                            ui32 += mBlockData[117];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[116];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[115];
                            ret = String.Format("sFlash.ulSumMILonMinutes = {0}", ui32);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.Additivieren:
                    switch (Position)
                    {
                        case 0:
                            ret = String.Format("sAddi.ucAnzahlKraftstoffklau = {0}", mBlockData[119]);
                            break;
                        case 1:
                            ui32 = (UInt32)(mBlockData[123] * 256);
                            ui32 += mBlockData[122];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[121];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[120];
                            ret = String.Format("sAddi.ulKraftstoffGesamt = {0}", ui32);
                            break;
                        case 2:
                            ret = String.Format("sAddi.ucLowVoltageErrorCounter = {0}", mBlockData[124]);
                            break;
                        case 3:
                            ui32 = (UInt32)(mBlockData[128] * 256);
                            ui32 += mBlockData[127];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[126];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[125];
                            ret = String.Format("sAddi.ulLowVoltageLastTimeChecked = {0}", ui32);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.IKD:
                    switch (Position)
                    {
                        case 0:
                            ret = String.Format("sICD.ucRingPointer = {0}", mBlockData[129]);
                            break;
                        case 1:
                            ui16 = (UInt16)(mBlockData[131] * 256);
                            ui16 += mBlockData[130];
                            ret = String.Format("sICD.uiaRingValue[0] = {0}", ui16);
                            break;
                        case 2:
                            ui16 = (UInt16)(mBlockData[133] * 256);
                            ui16 += mBlockData[132];
                            ret = String.Format("sICD.uiaRingValue[1] = {0}", ui16);
                            break;
                        case 3:
                            ui16 = (UInt16)(mBlockData[135] * 256);
                            ui16 += mBlockData[134];
                            ret = String.Format("sICD.uiaRingValue[2] = {0}", ui16);
                            break;
                        case 4:
                            ui16 = (UInt16)(mBlockData[137] * 256);
                            ui16 += mBlockData[136];
                            ret = String.Format("sICD.uiaRingValue[3] = {0}", ui16);
                            break;
                        case 5:
                            ui16 = (UInt16)(mBlockData[139] * 256);
                            ui16 += mBlockData[138];
                            ret = String.Format("sICD.uiaRingValue[4] = {0}", ui16);
                            break;
                        case 6:
                            ui16 = (UInt16)(mBlockData[141] * 256);
                            ui16 += mBlockData[140];
                            ret = String.Format("sICD.uiaRingValue[5] = {0}", ui16);
                            break;
                        case 7:
                            ui16 = (UInt16)(mBlockData[143] * 256);
                            ui16 += mBlockData[142];
                            ret = String.Format("sICD.uiaRingValue[6] = {0}", ui16);
                            break;
                        case 8:
                            ui16 = (UInt16)(mBlockData[145] * 256);
                            ui16 += mBlockData[144];
                            ret = String.Format("sICD.uiaRingValue[7] = {0}", ui16);
                            break;
                        case 9:
                            ui16 = (UInt16)(mBlockData[147] * 256);
                            ui16 += mBlockData[146];
                            ret = String.Format("sICD.uiaRingValue[8] = {0}", ui16);
                            break;
                        case 10:
                            ui16 = (UInt16)(mBlockData[149] * 256);
                            ui16 += mBlockData[148];
                            ret = String.Format("sICD.uiaRingValue[9] = {0}", ui16);
                            break;
                        case 11:
                            ui16 = (UInt16)(mBlockData[151] * 256);
                            ui16 += mBlockData[150];
                            ret = String.Format("sICD.uiaRingValue[10] = {0}", ui16);
                            break;
                        case 12:
                            ui16 = (UInt16)(mBlockData[153] * 256);
                            ui16 += mBlockData[152];
                            ret = String.Format("sICD.uiaRingValue[11] = {0}", ui16);
                            break;
                        case 13:
                            ui16 = (UInt16)(mBlockData[155] * 256);
                            ui16 += mBlockData[154];
                            ret = String.Format("sICD.uiaRingValue[12] = {0}", ui16);
                            break;
                        case 14:
                            ret = String.Format("sICD.ucaRingReg[0] = {0}", mBlockData[155]);
                            break;
                        case 15:
                            ret = String.Format("sICD.ucaRingReg[1] = {0}", mBlockData[156]);
                            break;
                        case 16:
                            ret = String.Format("sICD.ucaRingReg[2] = {0}", mBlockData[157]);
                            break;
                        case 17:
                            ret = String.Format("sICD.ucaRingReg[3] = {0}", mBlockData[158]);
                            break;
                        case 18:
                            ret = String.Format("sICD.ucaRingReg[4] = {0}", mBlockData[159]);
                            break;
                        case 19:
                            ret = String.Format("sICD.ucaRingReg[5] = {0}", mBlockData[160]);
                            break;
                        case 20:
                            ret = String.Format("sICD.ucaRingReg[6] = {0}", mBlockData[161]);
                            break;
                        case 21:
                            ret = String.Format("sICD.ucaRingReg[7] = {0}", mBlockData[162]);
                            break;
                        case 22:
                            ret = String.Format("sICD.ucaRingReg[8] = {0}", mBlockData[163]);
                            break;
                        case 23:
                            ret = String.Format("sICD.ucaRingReg[9] = {0}", mBlockData[164]);
                            break;
                        case 24:
                            ret = String.Format("sICD.ucaRingReg[10] = {0}", mBlockData[165]);
                            break;
                        case 25:
                            ret = String.Format("sICD.ucaRingReg[11] = {0}", mBlockData[166]);
                            break;
                        case 26:
                            ret = String.Format("sICD.ucaRingReg[13] = {0}", mBlockData[167]);
                            break;
                        case 27:
                            ret = String.Format("sICD.ucaRingReg[12] = {0}", mBlockData[168]);
                            break;
                        case 28:
                            ui16 = (UInt16)(mBlockData[170] * 256);
                            ui16 += mBlockData[169];
                            ret = String.Format("sICD.uiAvgFilterLoad = {0}", ui16);
                            break;
                        case 29:
                            ui16 = (UInt16)(mBlockData[172] * 256);
                            ui16 += mBlockData[171];
                            ret = String.Format("sICD.uiFilterRunTime = {0}", ui16);
                            break;
                        /* ab Version 4:
                            sICD.dwork.*
                        */
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.VERT:
                    if (Version <= 4)
                    {
                        offset = 173;
                    }
                    else
                    {
                        offset = 173 + 50;
                    }
                    switch (Position)
                    {
                        case 0:
                            ret = String.Format("sVert_ucResetBehaveWatch = {0}", mBlockData[offset]);
                            break;
                        case 1:
                            ui16 = (UInt16)(mBlockData[offset + 2] * 256);
                            ui16 += mBlockData[offset + 1];
                            ret = String.Format("sVert.uiPalarm = {0}", ui16);
                            break;
                        case 2:
                            ui16 = (UInt16)(mBlockData[offset + 4] * 256);
                            ui16 += mBlockData[offset + 3];
                            ret = String.Format("sVert.uiTalarm = {0}", ui16);
                            break;
                        case 3:
                            ui16 = (UInt16)(mBlockData[offset + 6] * 256);
                            ui16 += mBlockData[offset + 5];
                            ret = String.Format("sVert.uiPalarmResetDelay = {0}", ui16);
                            break;
                        case 4:
                            ui16 = (UInt16)(mBlockData[offset + 8] * 256);
                            ui16 += mBlockData[offset + 7];
                            ret = String.Format("sVert.uiPwarn = {0}", ui16);
                            break;
                        case 5:
                            ui16 = (UInt16)(mBlockData[offset + 10] * 256);
                            ui16 += mBlockData[offset + 9];
                            ret = String.Format("sVert.uiTwarn = {0}", ui16);
                            break;
                        case 6:
                            ui16 = (UInt16)(mBlockData[offset + 12] * 256);
                            ui16 += mBlockData[offset + 11];
                            ret = String.Format("sVert.uiPwarnResetDelay = {0}", ui16);
                            break;
                        case 7:
                            ui16 = (UInt16)(mBlockData[offset + 14] * 256);
                            ui16 += mBlockData[offset + 13];
                            ret = String.Format("sVer.uiPbroken = {0}", ui16);
                            break;
                        case 8:
                            ui16 = (UInt16)(mBlockData[offset + 14] * 256);
                            ui16 += mBlockData[offset + 15];
                            ret = String.Format("sVert.uiPbrokenTime = {0}", ui16);
                            break;
                        case 9:
                            ui16 = (UInt16)(mBlockData[offset + 18] * 256);
                            ui16 += mBlockData[offset + 17];
                            ret = String.Format("sVert.uiPbrokenTimeDelay = {0}", ui16);
                            break;
                        case 10:
                            ui16 = (UInt16)(mBlockData[offset + 20] * 256);
                            ui16 += mBlockData[offset + 19];
                            ret = String.Format("sVert.uiTurnSchwelleMin = {0}", ui16);
                            break;
                        case 11:
                            ui16 = (UInt16)(mBlockData[offset + 22] * 256);
                            ui16 += mBlockData[offset + 21];
                            if (ui16 > 32767)
                            {
                                i16 = (Int16)(65535 - ((Int32)ui16));
                            }
                            else
                            {
                                i16 = (Int16)ui16;
                            }
                            ret = String.Format("sVert.siTvorAlarmValue = {0}", i16);
                            break;
                        case 12:
                            ui16 = (UInt16)(mBlockData[offset + 24] * 256);
                            ui16 += mBlockData[offset + 23];
                            ret = String.Format("sVert.uiTvorAlarmTime = {0}", ui16);
                            break;
                        case 13:
                            ui16 = (UInt16)(mBlockData[offset + 26] * 256);
                            ui16 += mBlockData[offset + 25];
                            ret = String.Format("sVert.uiTvorAlarmResetDelay = {0}", ui16);
                            break;
                        case 14:
                            ui16 = (UInt16)(mBlockData[offset + 28] * 256);
                            ui16 += mBlockData[offset + 27];
                            if (ui16 > 32767)
                            {
                                i16 = (Int16)(65535 - ((Int32)ui16));
                            }
                            else
                            {
                                i16 = (Int16)ui16;
                            }
                            ret = String.Format("sVert.siTvorWarnValue = {0}", i16);
                            break;
                        case 15:
                            ui16 = (UInt16)(mBlockData[offset + 30] * 256);
                            ui16 += mBlockData[offset + 29];
                            ret = String.Format("sVert.uiTvorWarnTime = {0}", ui16);
                            break;
                        case 16:
                            ui16 = (UInt16)(mBlockData[offset + 32] * 256);
                            ui16 += mBlockData[offset + 31];
                            ret = String.Format("sVert.uiTvorWarnResetDelay = {0}", ui16);
                            break;
                        case 17:
                            ui16 = (UInt16)(mBlockData[offset + 34] * 256);
                            ui16 += mBlockData[offset + 33];
                            if (ui16 > 32767)
                            {
                                i16 = (Int16)(65535 - ((Int32)ui16));
                            }
                            else
                            {
                                i16 = (Int16)ui16;
                            }
                            if (Version < 6)
                            {
                                ret = String.Format("sVert.uiDruckzeit = {0}", ui16);
                            }
                            else
                            {
                                ret = String.Format("sVert.siTnachAlarmValue = {0}", i16);
                            }
                            break;
                        case 18:
                            ui16 = (UInt16)(mBlockData[offset + 36] * 256);
                            ui16 += mBlockData[offset + 35];
                            if (Version < 6)
                            {
                                ret = String.Format("sVert.uiDruckzeitWarn = {0}", ui16);
                            }
                            else
                            {
                                ret = String.Format("sVert.uiTnachAlarmTime = {0}", i16);
                            }
                            break;
                        case 19:
                            ui16 = (UInt16)(mBlockData[offset + 38] * 256);
                            ui16 += mBlockData[offset + 37];
                            if (Version < 6)
                            {
                                ret = String.Format("sVert.uiPbrokenTimeCt = {0}", ui16);
                            }
                            else
                            {
                                ret = String.Format("sVert.uiTnachAlarmResetDelay = {0}", i16);
                            }
                            break;
                        case 20:
                            ui16 = (UInt16)(mBlockData[offset + 40] * 256);
                            ui16 += mBlockData[offset + 39];
                            if (ui16 > 32767)
                            {
                                i16 = (Int16)(65535 - ((Int32)ui16));
                            }
                            else
                            {
                                i16 = (Int16)ui16;
                            }
                            if (Version < 6)
                            {
                                ret = String.Format("sVert.uiTvorAlarmZeit = {0}", ui16);
                            }
                            else
                            {
                                ret = String.Format("sVert.siTnachWarnValue = {0}", i16);
                            }
                            break;
                        case 21:
                            ui16 = (UInt16)(mBlockData[offset + 42] * 256);
                            ui16 += mBlockData[offset + 41];
                            if (Version < 6)
                            {
                                ret = String.Format("sVert.uiTvorWarnZeit = {0}", ui16);
                            }
                            else
                            {
                                ret = String.Format("sVert.uiTnachWarnTime = {0}", i16);
                            }
                            break;
                        case 22:
                            ui16 = (UInt16)(mBlockData[offset + 44] * 256);
                            ui16 += mBlockData[offset + 43];
                            if (Version < 6)
                            {
                                ret = String.Format("sVert.uiAdditivImFilter = {0}", ui16);
                            }
                            else
                            {
                                ret = String.Format("sVert.uiTnachWarnResetDelay = {0}", i16);
                            }
                            break;
                        case 23:
                            // v6 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 46] * 256);
                            ui16 += mBlockData[offset + 45];
                            ret = String.Format("sVert.uiDruckzeit = {0}", ui16);
                            break;
                        case 24:
                            // v6 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 48] * 256);
                            ui16 += mBlockData[offset + 47];
                            ret = String.Format("sVert.uiDruckzeitWarn = {0}", ui16);
                            break;
                        case 25:
                            // v6 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 50] * 256);
                            ui16 += mBlockData[offset + 49];
                            ret = String.Format("sVert.uiPbrokenTimeCt = {0}", ui16);
                            break;
                        case 26:
                            // v6 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 52] * 256);
                            ui16 += mBlockData[offset + 51];
                            ret = String.Format("sVert.uiTvorAlarmZeit = {0}", ui16);
                            break;
                        case 27:
                            // v6 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 54] * 256);
                            ui16 += mBlockData[offset + 53];
                            ret = String.Format("sVert.uiTvorWarnZeit = {0}", ui16);
                            break;
                        case 28:
                            // v6 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 56] * 256);
                            ui16 += mBlockData[offset + 55];
                            ret = String.Format("sVert.uiTnachAlarmZeit = {0}", ui16);
                            break;
                        case 29:
                            // v6 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 58] * 256);
                            ui16 += mBlockData[offset + 57];
                            ret = String.Format("sVert.uiTnachWarnZeit = {0}", ui16);
                            break;
                        case 30:
                            // v6 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 60] * 256);
                            ui16 += mBlockData[offset + 59];
                            ret = String.Format("sVert.uiAdditivImFilter = {0}", ui16);
                            break;
                        case 31:
                            // v7 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 62] * 256);
                            ui16 += mBlockData[offset + 61];
                            ret = String.Format("sVert.uiF37acceptCounter = {0}", ui16);
                            break;
                        case 32:
                            // v7 oder hoeher
                            ui16 = (UInt16)(mBlockData[offset + 64] * 256);
                            ui16 += mBlockData[offset + 63];
                            ret = String.Format("sVert.uiF37acceptTimeStamp = {0}", ui16);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.Turns:
                    if (Version <= 4)
                    {
                        offset = 173 + 45;
                    }
                    else
                    {
                        if (Version <= 6)
                        {
                            offset = 173 + 50 + 45;
                        }
                        else
                        {
                            if (Version <= 7)
                            {
                                offset = 173 + 50 + 65;
                            }
                            else
                            {
                                offset = 173 + 50 + 61;
                            }
                        }
                    }
                    switch (Position)
                    {
                        case 0:
                            ret = String.Format("sTurns.fFaktorTurns = {0}", BitConverter.ToSingle(mBlockData, offset));
                            break;
                        case 1:
                            ui16 = (UInt16)(mBlockData[offset + 5] * 256);
                            ui16 += mBlockData[offset + 4];
                            ret = String.Format("sTurns.uiKilometerzaehler = {0}", ui16);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.Dosieren:
                    if (Version <= 4)
                    {
                        offset = 173 + 6 + 45;
                    }
                    else
                    {
                        if (Version <= 6)
                        {
                            offset = 173 + 6 + 45 + 50;
                        }
                        else
                        {
                            if (Version <= 7)
                            {
                                offset = 173 + 6 + 50 + 65;
                            }
                            else
                            {
                                offset = 173 + 6 + 50 + 61;
                            }
                        }
                    }
                    switch (Position)
                    {
                        case 0:
                            ui32 = (UInt32)(mBlockData[offset + 3] * 256);
                            ui32 += mBlockData[offset + 2];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[offset + 1];
                            ui32 = (UInt32)(ui32 * 256);
                            ui32 += mBlockData[offset];
                            ret = String.Format("sDosi.ulSollKonzentration = {0}", ui32);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.IO:
                    if (Version <= 4)
                    {
                        offset = 173 + 10 + 45;
                    }
                    else
                    {
                        if (Version <= 6)
                        {
                            offset = 173 + 10 + 45 + 50;
                        }
                        else
                        {
                            if (Version <= 7)
                            {
                                offset = 173 + 10 + 50 + 65;
                            }
                            else
                            {
                                offset = 173 + 10 + 50 + 61;
                            }
                        }
                    }
                    switch (Position)
                    {
                        case 0:
                            ui16 = (UInt16)(mBlockData[offset + 1] * 256);
                            ui16 += mBlockData[offset];
                            ret = String.Format("sIO.uiDPlus = {0}", ui16);
                            break;
                        case 1:
                            ret = String.Format("sIO.ucTempEnable = {0}", mBlockData[offset + 2]);
                            break;
                        case 2:
                            // v6 oder hoeher
                            ret = String.Format("sIO.ucAbsPressOffset = {0}", mBlockData[offset + 3]);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                case GroupName.DPD:
                    if (Version <= 4)
                    {
                        offset = 173 + 13 + 45;
                    }
                    else
                    {
                        if (Version <= 6)
                        {
                            offset = 173 + 14 + 45 + 50;
                        }
                        else
                        {
                            if (Version <= 7)
                            {
                                offset = 173 + 14 + 50 + 65;
                            }
                            else
                            {
                                offset = 173 + 14 + 50 + 61;
                            }
                        }
                    }
                    ret = String.Format("sDPD.KategorieMinute[{0}].ucJNRestlaufzeit = {1}", Position, mBlockData[offset + Position]);
                    break;
                case GroupName.Tankgeber:
                    if (Version <= 4)
                    {
                        offset = 173 + 46 + 15 + 13 + 45;
                    }
                    else
                    {
                        if (Version <= 6)
                        {
                            offset = 173 + 46 + 15 + 14 + 45 + 50;
                        }
                        else
                        {
                            if (Version <= 7)
                            {
                                offset = 173 + 46 + 15 + 14 + 50 + 65;
                            }
                            else
                            {
                                offset = 173 + 46 + 15 + 14 + 50 + 61;
                            }
                        }
                    }
                    switch (Position)
                    {
                        case 0:
                            ret = String.Format("sTank.ucTankBlockiertMinutes = {0}", mBlockData[offset]);
                            break;
                        case 1:
                            ret = String.Format("sTank.ucTankBlockiertLowCounter = {0}", mBlockData[offset + 1]);
                            break;
                        case 2:
                            ret = String.Format("sTank.ucTankBlockiertHighCounter = {0}", mBlockData[offset + 2]);
                            break;
                        case 3:
                            ret = String.Format("sTank.ucTankBlockiertFehlerCounter = {0}", mBlockData[offset + 3]);
                            break;
                        case 4:
                            ui16 = (UInt16)(mBlockData[offset + 5] * 256);
                            ui16 += mBlockData[offset + 4];
                            ret = String.Format("sTank.uiTankBlockiertInhaltAlt = {0}", ui16);
                            break;
                        case 5:
                            // ab v 5
                            ui16 = (UInt16)(mBlockData[offset + 7] * 256);
                            ui16 += mBlockData[offset + 6];
                            ret = String.Format("sTank.uiTankBlockiertInhaltSum = {0}", ui16);
                            break;
                        default:
                            ret = "N/A";
                            break;
                    }
                    break;
                default:
                    ret = "N/A";
                    break;
            }
            return ret;
        }
    }
}
