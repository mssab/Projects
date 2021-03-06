/*
 * Object: HJS.Block
 * Description: Object of data bulk inside of linear adressable memory
 * 
 * $LastChangedDate: 2013-10-25 14:40:14 +0200 (Fr, 25 Okt 2013) $
 * $LastChangedRevision: 25 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/trunk/lib_cs_win32_hjsecu/Block.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
//using System.Runtime.InteropServices;

namespace HJS
{
    /// <summary>
    /// block object class
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Size of block header in bytes
        /// </summary>
        /// <see cref="HeaderPosition">HeaderPosition</see>
        public const byte BLOCK_HEADER_SIZE = 6;

        /// <summary>
        /// Maximum amount of bytes for complete block
        /// </summary>
	    public const UInt16 MAX_BLOCK_SIZE = 4096;

        /// <summary>
        /// Version type structure
        /// </summary>
        public struct VersionT
        {
            /// <summary>
            /// Main version number
            /// </summary>
            public UInt16 Hauptversion;
            /// <summary>
            /// Secondary version number
            /// </summary>
            public byte Nebenversion;
            /// <summary>
            /// Revision or third version number
            /// </summary>
            public byte Revision;
        }

        /// <summary>
        /// Enumeration of Identifiers for blocks
        /// </summary>
	    public enum BlockId : byte
        {
            /// <summary>
            /// Ivalid block identifier
            /// </summary>
		    IdInvalid = 0,
            /// <summary>
            /// Emergency configuration
            /// </summary>
		    IdNotKonfig = 1,
            /// <summary>
            /// Configuration
            /// </summary>
		    IdKonfig = 2,
            /// <summary>
            /// Data map
            /// </summary>
		    IdKennfld = 3,
            /// <summary>
            /// Drive pattern detection
            /// </summary>
		    IdDPD1 = 4,
            /// <summary>
            /// Drive pattern detection backup
            /// </summary>
		    IdDPD2 = 5,
            /// <summary>
            /// Empirical values
            /// </summary>
		    IdValue1 = 6,
            /// <summary>
            /// Empirical values backup
            /// </summary>
		    IdValue2 = 7,
            /// <summary>
            /// Behave ring part 1
            /// </summary>
		    IdVerhRing1 = 8,
            /// <summary>
            /// Behave ring part 2
            /// </summary>
		    IdVerhRing2 = 9,
            /// <summary>
            /// Behave ring part 3
            /// </summary>
		    IdVerhRing3 = 10,
            /// <summary>
            /// reserved identifier
            /// </summary>
		    IdReserve1 = 11,
            /// <summary>
            /// reserved identifier
            /// </summary>
            IdReserve2 = 12,
            /// <summary>
            /// reserved identifier
            /// </summary>
            IdReserve3 = 13,
            /// <summary>
            /// Production data
            /// </summary>
		    IdProduction = 14,
            /// <summary>
            /// Language german
            /// </summary>
		    IdLngDE = 30,
            /// <summary>
            /// Language english
            /// </summary>
		    IdLngEN = 31,
            /// <summary>
            /// Language french
            /// </summary>
		    IdLngFR = 32,
            /// <summary>
            /// Language italian
            /// </summary>
		    IdLngIT = 33,
            /// <summary>
            /// Language spanish
            /// </summary>
            IdLngES = 34,
            /// <summary>
            /// Language polish
            /// </summary>
            IdLngPO = 35,
            /// <summary>
            /// Language nederland
            /// </summary>
            IdLngNL = 36,
            /// <summary>
            /// Digital info block
            /// </summary>
		    IdInfoDig = 50,
            /// <summary>
            /// Info string
            /// </summary>
		    IdInfoText = 51,
            /// <summary>
            /// Actual values
            /// </summary>
		    IdIstWerte = 52,
            /// <summary>
            /// Error stack
            /// </summary>
		    IdFehlerStack = 53,
            /// <summary>
            /// Real time clock
            /// </summary>
		    IdRealTimeClock = 54,
            /// <summary>
            /// Construction kit values
            /// </summary>
		    IdParamBaukasten = 251,
            /// <summary>
            /// Deleted block
            /// </summary>
		    IdDeletion = 252,
            /// <summary>
            /// Logging block
            /// </summary>
		    IdLogging = 253,
            /// <summary>
            /// author block
            /// </summary>
		    IdAuthor = 254,
            /// <summary>
            /// Invalid block identifier
            /// </summary>
		    IdInvalid2 = 255
	    }

        /// <summary>
        /// Enumeration for byte position in block header
        /// </summary>
        public enum HeaderPosition : int
        {
            /// <summary>
            /// Position of block type
            /// </summary>
            Type = 0,
            /// <summary>
            /// Position of block version
            /// </summary>
            Version = 1,
            /// <summary>
            /// Lowbyte of block size
            /// </summary>
            SizeLowByte = 2,
            /// <summary>
            /// Highbyte of block size
            /// </summary>
            SizeHighByte = 3,
            /// <summary>
            /// Lowbyte of block checksum
            /// </summary>
            ChecksumLowByte = 4,
            /// <summary>
            /// Highbyte of block checksum
            /// </summary>
            ChecksumHighByte = 5
        }

        /// <summary>
        /// Type of block (contained in block header)
        /// </summary>
        private BlockId mType;

        /// <summary>
        /// Version of block (contained in block header)
        /// </summary>
        private byte mVersion;

        /// <summary>
        /// Size of complete block (contained in block header)
        /// </summary>
        private UInt16 mSize;

        /// <summary>
        /// Checksum of block (contained in block header)
        /// </summary>
        protected UInt16 mChecksum;

        /// <summary>
        /// Byte array of date (formally Pointer to block data)
        /// </summary>
	    protected byte[] mBlockData;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Block()
        {
            mType = BlockId.IdInvalid;
            mVersion = 0;
            mSize = 0;
            mChecksum = 0;
        }

        /// <summary>
        /// Accessors of type of block
        /// </summary>
        public BlockId Type
        {
            get { return mType; }
            set { mType = value; }
        }

        /// <summary>
        /// Accessors of block version
        /// </summary>
        public byte Version
        {
            get { return mVersion; }
            set { mVersion = value; }
        }

        /// <summary>
        /// Accessors of block data size
        /// </summary>
        public UInt16 DataSize
        {
            get { return mSize; }
            set { mSize = value; }
        }

        /// <summary>
        /// Accesssors to checksum
        /// Read only
        /// </summary>
        public UInt16 Checksum
        {
            get { return mChecksum; }
        }

        /// <summary>
        /// Get block object byte array (formally pointer to data)
        /// </summary>
        /// <returns>No return value, on errors target is set to null</returns>
        public void GetData(out byte[] TargetArray)
        {
            if ((DataSize > 0) && (DataSize == mBlockData.Length))
            {
                TargetArray = new byte[DataSize];
                for (int i = 0; i < DataSize; i++)
                {
                    TargetArray[i] = mBlockData[i];
                }
            }
            else
            {
                TargetArray = null;
            }
        }

        /// <summary>
        /// Check block for valid checksum
        /// </summary>
        /// <returns>True on success</returns>
	    public bool CheckChecksum()
        {
            UInt16 crc16 = GenerateCRC16(mBlockData, mSize, 0);
            return (crc16 == mChecksum);
        }

        /// <summary>
        /// Generate block data checksum and put it in the header
        /// </summary>
	    public void GenerateChecksum()
        {
            mChecksum = GenerateCRC16(mBlockData, mSize, 0);
        }

        /// <summary>
        /// Read complete block from byte array, including block header.
        /// The block object has to be initialized before, especially type
        /// and version!
        /// </summary>
        /// <param name="SourceData">Source byte array</param>
        /// <param name="KeepVersion">Flag if existing version of block should
        /// not be changed</param>
        /// <returns></returns>
        public ReturnValue ReadRaw(ref byte[] SourceData, bool KeepVersion)
        {
            ReturnValue ret = ReturnValue.NoError;

            if (SourceData == null)
            {
                ret = ReturnValue.BlockNotFound;
            }
            else
            {
                // Check block type
                if (SourceData[(int)HeaderPosition.Type] != (byte)mType)
                {
                    ret = ReturnValue.BlockNotFound;
                }
                else
                {
                    // Check or set block version
                    if (KeepVersion && mVersion
                        != SourceData[(int)HeaderPosition.Version])
                    {
                        ret = ReturnValue.VersionMismatch;
                    }
                    else
                    {
                        if (!KeepVersion)
                        {
                            mVersion = SourceData[(int)HeaderPosition.Version];
                        }

                        // Read block size
                        mSize = (UInt16)(SourceData[(int)HeaderPosition.SizeHighByte] * 256);
                        mSize += SourceData[(int)HeaderPosition.SizeLowByte];

                        // Check data size in header to size of byte array
                        if ((mSize <= (MAX_BLOCK_SIZE - BLOCK_HEADER_SIZE))
                            && (mSize == (SourceData.GetLength(0) - BLOCK_HEADER_SIZE)))
                        {
                            // Read block data
                            mBlockData = new byte[mSize];
                            for (int i = 0; i < mSize; i++)
                            {
                                mBlockData[i] = SourceData[i + BLOCK_HEADER_SIZE];
                            }
                            // Read Checksum
                            mChecksum = (UInt16)(SourceData[(int)HeaderPosition.ChecksumHighByte] * 256);
                            mChecksum += SourceData[(int)HeaderPosition.ChecksumLowByte];

                            // Check block checksum
                            if (CheckChecksum() == false)
                            {
                                ret = ReturnValue.ChecksumMismatch;
                            }
                        }
                        else
                        {
                            ret = ReturnValue.SizeMismatch;
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Write a complete block to a byte array, including block header
        /// </summary>
        /// <param name="Data">Target byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue WriteRaw(out byte[] Data/*, UInt16 MaxSize*/)
        {
        // <param name="MaxSize">Maximum size of target array</param>
            ReturnValue ret = ReturnValue.NoError;
            //if (Data == null)
            //{
            //    ret = ReturnValue.BlockNotFound;
            //}
            //else
            //{
                Data = new byte[mSize + BLOCK_HEADER_SIZE];
                //if (((mSize + BLOCK_HEADER_SIZE) < MaxSize)
                //    && ((mSize + BLOCK_HEADER_SIZE) < Data.Length))
                //{
                    Data[(int)HeaderPosition.Type] = (byte)mType;
                    Data[(int)HeaderPosition.Version] = mVersion;
                    Data[(int)HeaderPosition.SizeLowByte] = (byte)(mSize % 256);
                    Data[(int)HeaderPosition.SizeHighByte] = (byte)(mSize / 256);
                    Data[(int)HeaderPosition.ChecksumLowByte] = (byte)(mChecksum % 256);
                    Data[(int)HeaderPosition.ChecksumHighByte] = (byte)(mChecksum / 256);
                    for (int i = 0; i < mSize; i++)
                    {
                        Data[i + BLOCK_HEADER_SIZE] = mBlockData[i];
                    }
                //}
                //else
                //{
                //    Data = new byte[0];
                //    ret = ReturnValue.SizeMismatch;
                //}
            //}
            return ret;
        }
        
        ///// <summary>
        ///// De serializes the block data from byte array to structure using
        ///// unmanaged marshal functions
        ///// </summary>
        ///// <typeparam name="T">Target structure</typeparam>
        ///// <returns>Object of structure</returns>
        //public T DeserializeUnmanagedData<T>() where T : struct
        //{
        //    int objsize = Marshal.SizeOf(typeof(T));
        //    IntPtr buff = Marshal.AllocHGlobal(objsize);
        //    Marshal.Copy(mBlockData, 0, buff, objsize);
        //    T retStruct = (T)Marshal.PtrToStructure(buff, typeof(T));
        //    Marshal.FreeHGlobal(buff);
        //    return retStruct;
        //}

        ///// <summary>
        ///// Serializes the block data from structure to byte array using
        ///// unmanaged marshal functions
        ///// </summary>
        ///// <typeparam name="T">Type of source structure</typeparam>
        ///// <param name="block">Object of source structure</param>
        //public void SerializeUnmanagedData<T>(T block) where T : struct
        //{
        //    int objsize = Marshal.SizeOf(typeof(T));
        //    mBlockData = new byte[objsize];
        //    IntPtr buff = Marshal.AllocHGlobal(objsize);
        //    Marshal.StructureToPtr(block, buff, true);
        //    Marshal.Copy(buff, mBlockData, 0, objsize);
        //    Marshal.FreeHGlobal(buff);
        //}

        /// <summary>
        /// Table of CRC-16 Polynomial (0x8005) for byte wise usage
        /// </summary>
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

        /// <summary>
        /// Generates Cyclic redundancy check (CRC16)
        /// Byte wise Generation with use of table instead of polynomial
        /// </summary>
        /// <param name="Data">Byte array of data to be checked</param>
        /// <param name="Size">Number of bytes to be checked</param>
        /// <param name="StartValue">Initial value of this check.
        /// Normal start value 0x0000. reverse start value 0xFFFF.
        /// Segmented crc start value is crc from previous segment</param>
        /// <returns>The ShiftRegister (checksum)</returns>
	    public static UInt16 GenerateCRC16(byte[] Data,
            UInt16 Size, UInt16 StartValue) 
        {
            UInt16 Char = 0;
	        UInt16 Position;
            UInt16 ShiftRegister = StartValue;

            if (Data == null)
            {
                ShiftRegister = 0;
            }
            else
            {
                for (Position = 0; Position < Size; Position++)
                {
                    Char = Data[Position];
                    ShiftRegister = (UInt16)(mCrc16Table[(int)((byte)Char ^ (byte)ShiftRegister)] ^ (byte)(ShiftRegister >> 8));
                }
            }
	        return ShiftRegister;
        }

        /// <summary>
        /// Update header data (except type, version only if required)
        /// </summary>
        /// <param name="SourceHeader">Reference to header byte array</param>
        /// <param name="KeepVersion">Flag if version must be kept</param>
        /// <returns>True, if header was updated</returns>
        public bool UpdateHeader(ref byte[] SourceHeader, bool KeepVersion)
        {
            bool ret = true;
            if (SourceHeader == null)
            {
                ret = false;
            }
            else
            {
                if (SourceHeader[(int)HeaderPosition.Type] != (byte)mType)
                {
                    ret = false;
                }
                else
                {
                    // Check or set block version
                    if (KeepVersion && mVersion != SourceHeader[(int)HeaderPosition.Version])
                    {
                        ret = false;
                    }
                    else
                    {
                        if (!KeepVersion)
                        {
                            mVersion = SourceHeader[(int)HeaderPosition.Version];
                        }

                        // Read block size
                        mSize = (UInt16)(SourceHeader[(int)HeaderPosition.SizeHighByte] * 256);
                        mSize += SourceHeader[(int)HeaderPosition.SizeLowByte];
                        // Read Checksum
                        mChecksum = (UInt16)(SourceHeader[(int)HeaderPosition.ChecksumHighByte] * 256);
                        mChecksum += SourceHeader[(int)HeaderPosition.ChecksumLowByte];
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Get version string of block, only valid on configuration blocks
        /// </summary>
        /// <returns>String of version</returns>
        public string GetCfgVersion()
        {
            return String.Format("{0}.{1}.{2}",
                (((ushort)mBlockData[3]) * 256) + mBlockData[2],
                mBlockData[4], mBlockData[5]);
        }
    }
}
