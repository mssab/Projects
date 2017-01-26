/*
 * Object: HJS.ECU.Protocol.Protocol14
 * Description: Protocol version 14 (compatible to HJS-ECU 1.34)
 * 
 * $LastChangedDate: 2013-10-25 14:40:14 +0200 (Fr, 25 Okt 2013) $
 * $LastChangedRevision: 25 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/tags/Version_1_3_1/lib_cs_win32_hjsecu/Protocol14.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using HJS.ECU.Port;

namespace HJS.ECU.Protocol
{
    /// <summary>
    /// Protocol version 14
    /// </summary>
    public class Protocol14 : ProtocolBase 
    {
        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="strPortName">Name of new serial port</param>
        /// <param name="ucaKey">Byte array of new ECU key</param>
        /// <param name="ConnectionType">Type of connection</param>
        public Protocol14(string strPortName, byte[] ucaKey, Comm.PortType ConnectionType)
            : base(strPortName, ucaKey, ConnectionType)
        {
            ProtocolVersion = 14;
        }

        /// <summary>
        /// Connect to ECU
        /// </summary>
        /// <param name="language">Language: 0=german, 1=english, 2=french, 3=italian</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Connect(LanguageId language)
        {
            mEcu.pVersion = ProtocolVersion;
            ReturnValue ret = mEcu.Connect();
            if (ret == ReturnValue.NoError)
            {
                // Read digital info block
                mInfo = new DigitalInfoBlock();
                byte[] ReadBuffer = new byte[MAX_READ_BUFFER_SIZE];
                ret = mEcu.Read(Comm.OrderByte.ReadInfoBlock, out ReadBuffer);
                if (ret == ReturnValue.NoError)
                {
                    mInfo.Parse(ref ReadBuffer);        // todo: so ok?
                    mFirmware.SoftwareRevision = mInfo.SoftwareVersion.Revision;
                    // Read Language data
                    UInt16 LanguageValue = 0;
                    switch (language)
                    {
                        case LanguageId.German:
                            LanguageValue = 0; break;
                        case LanguageId.English:
                            LanguageValue = 1; break;
                        case LanguageId.French:
                            LanguageValue = 2; break;
                        case LanguageId.Italian:
                            LanguageValue = 3; break;
                        default:
                            LanguageValue = 1; break;
                    }
                    ReadBuffer = new byte[4096];
                    ret = mEcu.Read(Comm.OrderByte.ReadLanguage, LanguageValue, out ReadBuffer);
                    if (ret == ReturnValue.NoError)
                    {
                        //mLanguage = new LanguageBlock(language);
                        switch (language)
                        {
                            case ProtocolBase.LanguageId.German:
                                mLanguage = new LanguageBlock(Block.BlockId.IdLngDE);
                                break;
                            case ProtocolBase.LanguageId.English:
                                mLanguage = new LanguageBlock(Block.BlockId.IdLngEN);
                                break;
                            case ProtocolBase.LanguageId.French:
                                mLanguage = new LanguageBlock(Block.BlockId.IdLngFR);
                                break;
                            case ProtocolBase.LanguageId.Italian:
                                mLanguage = new LanguageBlock(Block.BlockId.IdLngIT);
                                break;
                            default:
                                mLanguage = new LanguageBlock(Block.BlockId.IdLngEN);
                                break;
                        }
                        ReturnValue BlockRet = ReturnValue.NoError;
                        // achtung kompatibilitaet 14 15 !
                        BlockRet = mLanguage.ReadPlain(ref ReadBuffer);
                        if (BlockRet == ReturnValue.NoError)
                        {
                            BlockRet = mLanguage.Parse();
                        }
                        if (BlockRet != ReturnValue.NoError)
                        {
                            ret = ReturnValue.LanguageNotValid;
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Connect to ECU without reading language!
        /// </summary>
        /// <returns>0 on success, else see Error</returns>
        public override ReturnValue FastConnect()
        {
            ReturnValue ret = mEcu.Connect();
            if (ret == ReturnValue.NoError)
            {
                // Read digital info block
                mInfo = new DigitalInfoBlock();
                byte[] ReadBuffer = new byte[MAX_READ_BUFFER_SIZE];
                ret = mEcu.Read(Comm.OrderByte.ReadInfoBlock, out ReadBuffer);
                if (ret == ReturnValue.Retry)
                {
                    ret = mEcu.Read(Comm.OrderByte.ReadInfoBlock, out ReadBuffer);
                }
                if (ret == ReturnValue.NoError)
                {
                    mInfo.Parse(ref ReadBuffer);        // todo: so ok?
                    mFirmware.SoftwareRevision = mInfo.SoftwareVersion.Revision;
                }
            }
            return ret;
        }

        /// <summary>
        /// Execute master reset
        /// </summary>
        /// <returns>0 on success, else see Error</returns>
        public override ReturnValue MasterReset()
        {
            ReturnValue ret = mEcu.Order(Comm.OrderByte.ResetMaster, 0);
            if (ret == ReturnValue.Retry)
            {
                ret = mEcu.Order(Comm.OrderByte.ResetMaster, 0);
            }
            return ret;
        }

        /// <summary>
        /// Write production data (SN, date, temperature)
        /// </summary>
        /// <param name="Data">Byte array of production data</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue WriteProductionData(ref byte[] Data)
        {
            // Workaround:
            // Das alte protokoll kannte keine bloecke, daher muss das
            // Bytearray zurueckgewandelt werden und ueber einzelne Befehle
            // zur ECU gesendet werden
            HJS.ECU.ProductionDataBlock pb = new ProductionDataBlock();
            ReturnValue ret = pb.ReadRaw(ref Data, false);
            if (ret != ReturnValue.NoError)
            {
                return ret;
            }
            byte[] buff = BitConverter.GetBytes(pb.SerialNumber);
            Array.Resize(ref buff, 8);
            buff[4] = 0x01; buff[5] = 0x00; // 1
            buff[6] = HardwareVersion.Nebenversion;
            buff[7] = HardwareVersion.Revision;
            ret = mEcu.Order(Comm.OrderByte.SetTemperatureCalibration, (UInt16)pb.TempertureOffset);
            if (ret == ReturnValue.Retry)
            {
                ret = mEcu.Order(Comm.OrderByte.SetTemperatureCalibration, (UInt16)pb.TempertureOffset);
            }
            ret = mEcu.Write(Comm.OrderByte.SetSerialNumber, 8, ref buff, 200);
            if (ret == ReturnValue.Retry)
            {
                ret = mEcu.Write(Comm.OrderByte.SetSerialNumber, 8, ref buff, 500);
            }
            if (ret == ReturnValue.Retry)
            {
                ret = mEcu.Write(Comm.OrderByte.SetSerialNumber, 8, ref buff, 500);
            }
            return ret;
        }

        /// <summary>
        /// Reboot ecu
        /// </summary>
        /// <param name="Mode">Mode of reboot</param>
        /// <returns>0 on success, else see Error</returns>
        public override ReturnValue Reboot(RebootMode Mode)
        {
            // erst ab Protokoll 15
            // throw new Exception("The method or operation is not implemented.");
            return ReturnValue.NoError;
        }

        /// <summary>
        /// Delete parameter blocks
        /// </summary>
        /// <returns>0 on success, else see Error</returns>
        public override ReturnValue EraseParamBlocks()
        {
            return ReturnValue.VersionMismatch;
        }

        /// <summary>
        /// Write parameter block
        /// </summary>
        /// <param name="ParameterBlock">Reference to source block</param>
        /// <returns>0 on success, else see Error</returns>
        public override ReturnValue WriteParamBlock(ref Block ParameterBlock)
        {
            return ReturnValue.VersionMismatch;
        }

        /// <summary>Read parameter block from ecu</summary>
        /// <param name="Identifier">Block identifier</param>
        /// <param name="ParameterBlock">Referece of block to read to</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadParamBlock(Block.BlockId Identifier, ref Block ParameterBlock)
        {
            return ReturnValue.VersionMismatch;
        }

        /// <summary>
        /// Check checksum of language
        /// </summary>
        /// <param name="Language">Language identifier</param>
        /// <param name="Checksum">Checksum</param>
        /// <returns>True if checksum matches</returns>
        public override bool CheckLanguageChecksum(Block.BlockId Language, UInt16 Checksum)
        {
            ReturnValue ret = ReturnValue.NoError;
            byte[] ReadBuffer = new byte[4096];
            UInt16 l = 1;
            switch (Language)
            {
                case Block.BlockId.IdLngDE:
                    l = 0; break;
                case Block.BlockId.IdLngEN:
                    l = 1; break;
                case Block.BlockId.IdLngFR:
                    l = 2; break;
                case Block.BlockId.IdLngIT:
                    l = 3; break;
            }
            ret = mEcu.Read(Comm.OrderByte.ReadLanguage, l, out ReadBuffer);
            //ret = mEcu.Read(Comm.OrderByte.ReadLanguage, (UInt16)Language, out ReadBuffer);
            if (ret == ReturnValue.Retry)
            {
                ret = mEcu.Read(Comm.OrderByte.ReadLanguage, l, out ReadBuffer);
            }
            if (ret == ReturnValue.NoError)
            {
                UInt16 c = Block.GenerateCRC16(ReadBuffer, 4096, 0);
                return Checksum != c ? false : true;
            }
            else
            {
                // auslesefehler
                return false;
            }
        }

        /// <summary>
        /// Read one flash block (32kByte) of acquisition raw data
        /// </summary>
        /// <param name="Position">Position of acquisition data in ring</param>
        /// <param name="Data">Target byte array for flash data</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadAcquisitionData(UInt16 Position, out byte[] Data)
        {
            ReturnValue ret = ReturnValue.NoError;
            if (mBusy)
            {
                Data = new byte[0];
                ret = ReturnValue.ThreadingBusy;
            }
            else
            {
                Data = new byte[MAX_READ_BUFFER_SIZE];
                for (int i = 0; i < MAX_READ_BUFFER_SIZE; i++) { Data[i] = 0xFF; }
                if (Position <= mInfo.AqSizeNumBlocks)
                {
                    Comm.OrderByte stb = Comm.OrderByte.Alive;
                    switch (Position)
                    {
                        case 0:
                            stb = Comm.OrderByte.ReadAcquisitionSector_01; break;
                        case 1:
                            stb = Comm.OrderByte.ReadAcquisitionSector_02; break;
                        case 2:
                            stb = Comm.OrderByte.ReadAcquisitionSector_03; break;
                        case 3:
                            stb = Comm.OrderByte.ReadAcquisitionSector_04; break;
                        case 4:
                            stb = Comm.OrderByte.ReadAcquisitionSector_05; break;
                        case 5:
                            stb = Comm.OrderByte.ReadAcquisitionSector_06; break;
                        case 6:
                            stb = Comm.OrderByte.ReadAcquisitionSector_07; break;
                        case 7:
                            stb = Comm.OrderByte.ReadAcquisitionSector_08; break;
                        case 8:
                            stb = Comm.OrderByte.ReadAcquisitionSector_09; break;
                        case 9:
                            stb = Comm.OrderByte.ReadAcquisitionSector_10; break;
                        case 10:
                            stb = Comm.OrderByte.ReadAcquisitionSector_11; break;
                        case 11:
                            stb = Comm.OrderByte.ReadAcquisitionSector_12; break;
                        case 12:
                            stb = Comm.OrderByte.ReadAcquisitionSector_13; break;
                        case 13:
                            stb = Comm.OrderByte.ReadAcquisitionSector_14; break;
                        case 14:
                            stb = Comm.OrderByte.ReadAcquisitionSector_14; break;
                        case 15:
                            stb = Comm.OrderByte.ReadAcquisitionSector_16; break;
                        case 16:
                            stb = Comm.OrderByte.ReadAcquisitionSector_17; break;
                        case 17:
                            stb = Comm.OrderByte.ReadAcquisitionSector_18; break;
                        case 18:
                            stb = Comm.OrderByte.ReadAcquisitionSector_19; break;
                        case 19:
                            stb = Comm.OrderByte.ReadAcquisitionSector_20; break;
                        case 20:
                            stb = Comm.OrderByte.ReadAcquisitionSector_21; break;
                        case 21:
                            stb = Comm.OrderByte.ReadAcquisitionSector_22; break;
                        case 22:
                            stb = Comm.OrderByte.ReadAcquisitionSector_23; break;
                        case 23:
                            stb = Comm.OrderByte.ReadAcquisitionSector_24; break;
                        case 24:
                            stb = Comm.OrderByte.ReadAcquisitionSector_25; break;
                        case 25:
                            stb = Comm.OrderByte.ReadAcquisitionSector_26; break;
                        case 26:
                            stb = Comm.OrderByte.ReadAcquisitionSector_27; break;
                        case 27:
                            stb = Comm.OrderByte.ReadAcquisitionSector_28; break;
                        case 28:
                            stb = Comm.OrderByte.ReadAcquisitionSector_29; break;
                        default:
                            stb = Comm.OrderByte.Alive; break;
                    }
                    if (stb != Comm.OrderByte.Alive)
                    {
                        ret = mEcu.Read(stb, out Data);
                    }
                    else
                    {
                        ret = ReturnValue.ComOrderFailed;
                    }
                }
                else
                {
                    ret = ReturnValue.ComOrderFailed;
                }
            }
            return ret;
        }

        // fehlt hier die pos des ring sektors?

        /// <summary>
        /// Read error history ring data unit
        /// </summary>
        /// <param name="Position">Position of data unit in ring</param>
        /// <param name="Data">Target byte array of error ring data</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadErrorRing(byte Position, out byte[] Data)
        {
            ReturnValue ret = ReturnValue.NoError;
            if (mBusy)
            {
                Data = new byte[0];
                ret = ReturnValue.ThreadingBusy;
            }
            else
            {
                ret = mEcu.Read(Comm.OrderByte.ReadErrorRing, out Data);
                if (ret == ReturnValue.Retry)
                {
                    ret = mEcu.Read(Comm.OrderByte.ReadErrorStack, out Data);
                }
            }
            return ret;
        }

        /// <summary>
        /// Read rtc data
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadRtc()
        {
            return ReturnValue.VersionMismatch;
        }

        /// <summary>
        /// Get number of volatiles values
        /// </summary>
        /// <returns>Number of volatiles values</returns>
        public override UInt16 GetNumberOfVolatiles()
        {
            return 0;
        }

        /// <summary>
        /// Get volatile value as string
        /// </summary>
        /// <param name="Position">Position of volatile value</param>
        /// <returns>Volatile value as string</returns>
        public override string GetVolatileValue(UInt16 Position)
        {
            return "N/A";
        }

        /// <summary>
        /// Read empirical data
        /// </summary>
        /// <returns>0 on sucess, else see ReturnValue</returns>
        public override ReturnValue ReadEmpiricals()
        {
            return ReturnValue.VersionMismatch;
        }

        /// <summary>
        /// Get empirical group names
        /// </summary>
        /// <param name="GroupNames">Position of group</param>
        /// <returns>True on success</returns>
        public override bool GetEmpiricalGroupNames(out string[] GroupNames)
        {
            // Function not available
            GroupNames = new string[0];
            return false;
        }

        /// <summary>
        /// Get number of empirical values
        /// </summary>
        /// <param name="Group">Enumeration of group</param>
        /// <returns>Number of values</returns>
        public override UInt16 GetNumberOfEmpiricalValues(EmpiricalDataBlock.GroupName Group)
        {
            return 0;
        }

        /// <summary>
        /// Get empirical value string
        /// </summary>
        /// <param name="GroupPosition">Position of group</param>
        /// <param name="ValuePosition">Position of value</param>
        /// <returns>String with name and value</returns>
        public override string GetEmpiricalValue(UInt16 GroupPosition, UInt16 ValuePosition)
        {
            return "N/A";
        }

        /// <summary>
        /// Read DTC data
        /// </summary>
        /// <param name="target">Output byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadDtc(out byte[] target)
        {
            target = new byte[0];
            return ReturnValue.VersionMismatch;
        }

        /// <summary>
        /// Read DTC assignement table
        /// </summary>
        /// <param name="target">Output byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadDtcTable(out byte[] target)
        {
            // Function not available
            target = new byte[0];
            return ReturnValue.VersionMismatch;
        }

        /// <summary>
        /// Send order to ecu
        /// </summary>
        /// <param name="Order">Enumeration of order</param>
        /// <param name="Value">Value of order</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue SendOrder(Comm.OrderByte Order, UInt16 Value)
        {
            return mEcu.Order(Order, Value);
        }
    }
}
