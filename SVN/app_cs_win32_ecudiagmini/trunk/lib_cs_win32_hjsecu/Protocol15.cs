/*
 * Object: HJS.ECU.Protocol.Protocol15
 * Description: Protocol version 15 (compatible to HJS-ECU 1.36)
 * 
 * $LastChangedDate: 2013-10-24 16:31:16 +0200 (Do, 24 Okt 2013) $
 * $LastChangedRevision: 24 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/trunk/lib_cs_win32_hjsecu/Protocol15.cs $
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
    /// Protocol Version 15
    /// </summary>
    public class Protocol15 : ProtocolBase
    {
        private VolatileData mRtcData;

        private EmpiricalDataBlock mEmpData;

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="strPortName">Name of new serial port</param>
        /// <param name="ucaKey">Byte array of new ECU key</param>
        /// <param name="ConnectionType">Type of connection</param>
        public Protocol15(string strPortName, byte[] ucaKey, Comm.PortType ConnectionType)
            : base(strPortName, ucaKey, ConnectionType)
        {
            ProtocolVersion = 15;
            mRtcData = new VolatileData();
            mEmpData = new EmpiricalDataBlock();
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
                if (ret == ReturnValue.Retry)
                {
                    ret = mEcu.Read(Comm.OrderByte.ReadInfoBlock, out ReadBuffer);
                }
                if (ret == ReturnValue.NoError)
                {
                    ReturnValue RetB;

                    RetB = mInfo.Parse(ref ReadBuffer);
                    mFirmware.SoftwareRevision = mInfo.SoftwareVersion.Revision;
                    mRtcData.SoftwareRevision = mInfo.SoftwareVersion.Revision;
                    mEmpData.Version = mInfo.EmpiricalVersion;
                    // Read Language data
                    Block.BlockId LanguageBlockId = GetLanguageBlockId(language);
                    ReadBuffer = new byte[4096];
                    ret = mEcu.Read(Comm.OrderByte.ReadLanguage, (UInt16)LanguageBlockId, out ReadBuffer);
                    if (ret == ReturnValue.Retry)
                    {
                        // One retry
                        ret = mEcu.Read(Comm.OrderByte.ReadLanguage, (UInt16)LanguageBlockId, out ReadBuffer);
                    }
                    if (ret == ReturnValue.NoError)
                    {
                        mLanguage = new LanguageBlock(LanguageBlockId);
                        ReturnValue BlockRet = ReturnValue.NoError;
                        BlockRet = mLanguage.ReadRaw(ref ReadBuffer, false);
                        if (BlockRet == ReturnValue.NoError)
                        {
                            BlockRet = mLanguage.Parse();
                        }
                        if (BlockRet != ReturnValue.NoError)
                        {
                            ret = ReturnValue.LanguageNotValid;
                        }
                    }
                    else
                    {
                        // Language not available, so load english
                        ret = mEcu.Read(Comm.OrderByte.ReadLanguage, (UInt16)Block.BlockId.IdLngEN, out ReadBuffer);
                        if (ret == ReturnValue.Retry)
                        {
                            // One retry
                            ret = mEcu.Read(Comm.OrderByte.ReadLanguage, (UInt16)Block.BlockId.IdLngEN, out ReadBuffer);
                        }
                        if (ret == ReturnValue.NoError)
                        {
                            mLanguage = new LanguageBlock(Block.BlockId.IdLngEN);
                            ReturnValue BlockRet = ReturnValue.NoError;
                            BlockRet = mLanguage.ReadRaw(ref ReadBuffer, false);
                            if (BlockRet == ReturnValue.NoError)
                            {
                                BlockRet = mLanguage.Parse();
                            }
                            if (BlockRet != ReturnValue.NoError)
                            {
                                ret = ReturnValue.LanguageNotValid;
                            }
                        }
                        else
                        {
                            if (mInfo.ConfigurationVersion.Hauptversion == 0)
                            {
                                // Emergency configuration: language not available
                                mLanguage = new LanguageBlock(Block.BlockId.IdLngEN);
                                ret = ReturnValue.NoError;
                            }
                        }
                    }
                }
            }
            else
            {
                mEcu.Disconnect();
            }
            return ret;
        }

        /// <summary>
        /// Connect to ECU without reading language!
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
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
                    ReturnValue RetB;
                    RetB = mInfo.Parse(ref ReadBuffer);
                    mFirmware.SoftwareRevision = mInfo.SoftwareVersion.Revision;
                    mRtcData.SoftwareRevision = mInfo.SoftwareVersion.Revision;
                }
            }
            else
            {
                if ((ret != ReturnValue.PortInUse)
                    && (ret != ReturnValue.PortNotOpened))
                {
                    mEcu.Disconnect();
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    mEcu.Disconnect();
                }
            }
            return ret;
        }

        /// <summary>
        /// Reboot ECU
        /// </summary>
        /// <param name="Mode">Mode of reboot</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Reboot(RebootMode Mode)
        {
            ReturnValue ret = mEcu.Order(Comm.OrderByte.Reboot, (UInt16)Mode);
            if (ret == ReturnValue.NoError)
            {
                System.Threading.Thread.Sleep(3000);
            }
            return ret;
        }

        /// <summary>
        /// Reset all empirical values and erase all flash ring memory
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue MasterReset()
        {
            // Execution time of deleting flash: ~ 4MB / 32K * 350ms = 44,8s
            // Meassured duration of deleting ~1,2s + 28s
            // So first try we give 30s execution time, on error second try we wait 6 more seconds
            ReturnValue ret = mEcu.Order(Comm.OrderByte.ResetMaster, 0, 30000);
            if (ret == ReturnValue.Retry)
            {
                ret = mEcu.Order(Comm.OrderByte.ResetMaster, 0, 36000);
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
            ReturnValue ret = mEcu.Write(Comm.OrderByte.WriteSaveBlock, (UInt16)Data.Length, ref Data, 10);
            if (ret != ReturnValue.NoError)
            {
                ret = mEcu.Order(Comm.OrderByte.EraseSaveBlock, 0, 2000);
                if (ret == ReturnValue.Retry)
                {
                    ret = mEcu.Order(Comm.OrderByte.EraseSaveBlock, 0, 2000);
                }
                if (ret == ReturnValue.NoError)
                {
                    ret = mEcu.Write(Comm.OrderByte.WriteSaveBlock, (UInt16)Data.Length, ref Data, 10);
                }
            }
            return ret;
        }

        /// <summary>
        /// Erase blocks in ECU flash for parameters
        /// </summary>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue EraseParamBlocks()
        {
            ReturnValue ret = mEcu.Order(Comm.OrderByte.SetInitBlockWriting, 0, 3000);
            return ret;
        }

        /// <summary>
        /// Write parameter block to ECU
        /// </summary>
        /// <param name="ParameterBlock">Reference of parameter block</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue WriteParamBlock(ref Block ParameterBlock)
        {
            ReturnValue ret = ReturnValue.NoError;
            byte[] Buffer;
            ParameterBlock.WriteRaw(out Buffer);
            ret = mEcu.Write(Comm.OrderByte.WriteBlock, (UInt16)ParameterBlock.Type, ref Buffer, 500);
            return ret;
        }

        /// <summary>Read parameter block from ecu</summary>
        /// <param name="Identifier">Block identifier</param>
        /// <param name="ParameterBlock">Referece of block to read to</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadParamBlock(Block.BlockId Identifier, ref Block ParameterBlock)
        {
            ReturnValue ret = ReturnValue.NoError;
            byte[] Buffer;
            switch (Identifier)
            {
                case Block.BlockId.IdKonfig:
                case Block.BlockId.IdKennfld:
                    ret = mEcu.Read(Comm.OrderByte.ReadBlock, (UInt16)Identifier, out Buffer);
                    if (ret == ReturnValue.NoError)
                    {
                        ret = ParameterBlock.ReadRaw(ref Buffer, false);
                    }
                    break;
                case Block.BlockId.IdLngDE:
                case Block.BlockId.IdLngEN:
                case Block.BlockId.IdLngFR:
                case Block.BlockId.IdLngIT:
                case Block.BlockId.IdLngES:
                case Block.BlockId.IdLngNL:
                case Block.BlockId.IdLngPO:
                    ret = mEcu.Read(Comm.OrderByte.ReadLanguage, (UInt16)Identifier, out Buffer);
                    if (ret == ReturnValue.NoError)
                    {
                        ret = ParameterBlock.ReadRaw(ref Buffer, false);
                    }
                    break;
                default:
                    ret = ReturnValue.BlockNotFound;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Read language and check checksum
        /// </summary>
        /// <param name="Language">Language identifier</param>
        /// <param name="Checksum">Reference checksum</param>
        /// <returns>True is language is loaded and checksums matching</returns>
        public override bool CheckLanguageChecksum(Block.BlockId Language, UInt16 Checksum)
        {
            ReturnValue ret = ReturnValue.NoError;
            byte[] ReadBuffer = new byte[4096];

            ret = mEcu.Read(Comm.OrderByte.ReadLanguage, (UInt16)Language, out ReadBuffer);
            if (ret == ReturnValue.Retry)
            {
                ret = mEcu.Read(Comm.OrderByte.ReadLanguage, (UInt16)Language, out ReadBuffer);
            }
            if (ret == ReturnValue.NoError)
            {
                return Checksum != ((ReadBuffer[5] * 256) + ReadBuffer[4]) ? false : true;
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
            bool bEmpty = true;
            ReturnValue ret = ReturnValue.NoError;
            if (mBusy)
            {
                Data = new byte[0];
                ret = ReturnValue.ThreadingBusy;
            }
            else
            {
                Data = new byte[MAX_READ_BUFFER_SIZE];
                // clear target with flash clear value
                for (int i = 0; i < MAX_READ_BUFFER_SIZE; i++) { Data[i] = 0xFF; }
                int PositionInData = 0;
                if (Position <= mInfo.AqSizeNumBlocks)
                {
                    byte[] ReadBuffer = new byte[256];

                    for (int Sector = 0; Sector < (32768 / 256); Sector++)
                    {
                        UInt16 _address = (UInt16)((PositionInData / 256) + (128 * Position));
                        //ret = mEcu.Read(Comm.OrderByte.ReadAcquiDev1Sector, (UInt16)(PositionInData / 256), out ReadBuffer);
                        ret = mEcu.Read(Comm.OrderByte.ReadAcquiDev1Sector, _address, out ReadBuffer);
                        if (ret == ReturnValue.NoError)
                        {
                            Array.Copy(ReadBuffer, 0, Data, PositionInData, 256);
                            PositionInData += 256;
                        }
                        else
                        {
                            Sector = (32768 / 256); // break for-loop on error
                        }
                        // Break sector on empty first 255 flash
                        if (Sector == 0)
                        {
                            bEmpty = true;
                            for (int i = 0; i < ReadBuffer.Length; i++)
                            {
                                if (ReadBuffer[i] != 255)
                                {
                                    bEmpty = false;
                                }
                            }
                            if (bEmpty)
                            {
                                Sector = (32768 / 256); // break for-loop on empty
                            }
                        }
                    }
                }
                else
                {
                    ret = ReturnValue.ComOrderFailed;
                }
            }
            return ret;
        }

        /// <summary>
        /// Read error history ring data unit
        /// </summary>
        /// <param name="Position">Position of data unit in ring</param>
        /// <param name="Data">Target byte array of error ring data</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadErrorRing(byte Position, out byte[] Data)
        {
            bool bEmpty = true;
            ReturnValue ret = ReturnValue.NoError;
            if (mBusy)
            {
                Data = new byte[0];
                ret = ReturnValue.ThreadingBusy;
            }
            else
            {
                // ret = mEcu.Read(OrderByte.ReadErrorStack, out ReadBuffer);
                Data = new byte[MAX_READ_BUFFER_SIZE];
                //foreach (byte b in Data) { Data.SetValue(255, b); }
                //foreach (byte b in Data) { b = 255; }
                for (int i = 0; i < Data.Length; i++) { Data[i] = 255; }
                int PositionInData = 0;
                if (Position <= mInfo.ErrorRingNumSectors)
                {
                    byte[] Buffer = new byte[256];

                    for (int Sector = 0; Sector < (32768 / 256); Sector++)
                    {
                        //Console.WriteLine(String.Format("Start read error ring sector {0} in block {1}", Sector, Position));
                        UInt16 _address = (UInt16)((PositionInData / 256) + (128 * Position));
                        //Console.WriteLine(String.Format("Start read error ring address {0} in block {1}", _address, Position));
                        //ret = mEcu.Read(Comm.OrderByte.ReadErrorRingSector, (UInt16)(PositionInData / 256), out Buffer);
                        ret = mEcu.Read(Comm.OrderByte.ReadErrorRingSector, _address, out Buffer);
                        if (ret == ReturnValue.NoError)
                        {
                            Array.Copy(Buffer, 0, Data, PositionInData, 256);
                            PositionInData += 256;
                        }
                        else
                        {
                            Sector = (32768 / 256); // break for-loop on error
                        }
                        // Break sector on empty first 255 flash
                        if (Sector == 0)
                        {
                            bEmpty = true;
                            for (int i = 0; i < Buffer.Length; i++)
                            {
                                if (Buffer[i] != 255)
                                {
                                    bEmpty = false;
                                }
                            }
                            if (bEmpty)
                            {
                                Sector = (32768 / 256); // break for-loop on empty
                            }
                        }
                    }
                }
                else
                {
                    ret = ReturnValue.ComOrderFailed;
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
            byte[] buffer;
            ReturnValue ret = mEcu.Read(Comm.OrderByte.ReadRealTimeClockData, 64, out buffer);
            if (ret == ReturnValue.NoError)
            {
                ret = mRtcData.Read(ref buffer);
            }
            return ret;
        }

        /// <summary>
        /// Get number of volatiles values
        /// </summary>
        /// <returns>Number of volatiles values</returns>
        public override UInt16 GetNumberOfVolatiles()
        {
            return mRtcData.NumberOfValues;
        }

        /// <summary>
        /// Get volatile value as string
        /// </summary>
        /// <param name="Position">Position of volatile value</param>
        /// <returns>Volatile value as string</returns>
        public override string GetVolatileValue(UInt16 Position)
        {
            return mRtcData.GetVolatileValue(Position);
        }

        /// <summary>
        /// Read empirical data
        /// </summary>
        /// <returns>0 on sucess, else see ReturnValue</returns>
        public override ReturnValue ReadEmpiricals()
        {
            byte[] buffer;
            ReturnValue ret = mEcu.Read(Comm.OrderByte.ReadEmpiricalValues, out buffer);
            if (ret == ReturnValue.NoError)
            {
                ret = mEmpData.Parse(ref buffer);
            }
            return ret;
        }

        /// <summary>
        /// Get number of empirical values
        /// </summary>
        /// <param name="Group">Enumeration of group</param>
        /// <returns>Number of values</returns>
        public override UInt16 GetNumberOfEmpiricalValues(EmpiricalDataBlock.GroupName Group)
        {
            return mEmpData.GetNumberOfValues(Group);
        }

        /// <summary>
        /// Get empirical group names
        /// </summary>
        /// <param name="GroupNames">Position of group</param>
        /// <returns>True on success</returns>
        public override bool GetEmpiricalGroupNames(out string[] GroupNames)
        {
            UInt16 _amount = 0;
            // zaehlen wenn ein oder mehr eintraege
            foreach (EmpiricalDataBlock.GroupName grp in Enum.GetValues(typeof(EmpiricalDataBlock.GroupName)))
            {
                if (mEmpData.GetNumberOfValues(grp) > 0) _amount++;
            }
            if (_amount > 0)
            {
                GroupNames = new string[_amount];
                // Namen kopieren
                for (int i = 0; i < _amount; i++)
                {
                    if (mEmpData.GetNumberOfValues((EmpiricalDataBlock.GroupName)i) > 0)
                    {
                        GroupNames[i] = ((EmpiricalDataBlock.GroupName)i).ToString();
                    }
                }
                return true;
            }
            else
            {
                GroupNames = new string[0];
                return false;
            }
        }

        /// <summary>
        /// Get empirical value string
        /// </summary>
        /// <param name="GroupPosition">Position of group</param>
        /// <param name="ValuePosition">Position of value</param>
        /// <returns>String with name and value</returns>
        public override string GetEmpiricalValue(UInt16 GroupPosition, UInt16 ValuePosition)
        {
            return mEmpData.GetEmpiricalValue((EmpiricalDataBlock.GroupName)GroupPosition, ValuePosition);
        }

        /// <summary>
        /// Read DTC data
        /// </summary>
        /// <param name="target">Output byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadDtc(out byte[] target)
        {
            ReturnValue ret = mEcu.Read(Comm.OrderByte.ReadDTC, out target);
            if (ret != ReturnValue.NoError)
            {
                target = new byte[0];
            }
            return ret;
        }

        /// <summary>
        /// Read DTC assignement table
        /// </summary>
        /// <param name="target">Output byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue ReadDtcTable(out byte[] target)
        {
            ReturnValue ret = mEcu.Read(Comm.OrderByte.ReadDTCIds, out target);
            if (ret != ReturnValue.NoError)
            {
                target = new byte[0];
            }
            return ret;
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
