/*
 * Object: HJS.ECU.Parameter.Datamap2_Block
 * Description: Datamap parameter block version 2
 * 
 * $LastChangedDate: 2016-06-07 09:55:50 +0200 (Di, 07 Jun 2016) $
 * $LastChangedRevision: 102 $
 * $LastChangedBy: jdr $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/Datamap2_Block.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU.Parameter
{
    /// <summary>Datamap parameter block version 2</summary>
    public class Datamap2_Block : Block
    {
        private VersionT mVersion;
        private DataMap[] mMaps;

        /// <summary>Accessors for number of stored data maps
        /// Read only</summary>
        public byte NumberOfStoredDatamaps
        {
            get { return (mMaps != null) ? (byte)mMaps.Length : (byte)0; }
        }

        /// <summary>Accessors for config. version struct
        /// Read only</summary>
        public VersionT ConfigVersion
        {
            get { return mVersion; }
            set
            {
                mVersion = value;
                mBlockData[0] = (byte)(mVersion.Hauptversion % 256);
                mBlockData[1] = (byte)(mVersion.Hauptversion / 256);
                mBlockData[2] = mVersion.Nebenversion;
                mBlockData[3] = mVersion.Revision;
            }
        }

        /// <summary>Default constructor</summary>
        public Datamap2_Block()
        {
            Type = BlockId.IdKennfld;
            Version = 2;
            DataSize = 0;
        }

        /// <summary>Overloaded constructor</summary>
        public Datamap2_Block(Block b)
        {
            Type = BlockId.IdKennfld;
            Version = 2;
            DataSize = 4090;

            if (b.Type == BlockId.IdKennfld)
            {
                b.GetData(out mBlockData);
                mChecksum = b.Checksum;
                Parse();
            }
        }

        private void Parse()
        {
            int pos = 4;
            int map = 0;
            int used_maps = 0;
            mVersion.Hauptversion = BitConverter.ToUInt16(mBlockData, 0);
            mVersion.Nebenversion = mBlockData[2];
            mVersion.Revision = mBlockData[3];

            // count used maps
            for (map = 0; map < 16; map++)
            {
                if ((mBlockData[pos] != 0xFF) || (mBlockData[pos + 1] != 0xFF)
                    || (mBlockData[pos + 2] != 0xFF) || (mBlockData[pos + 3] != 0xFF))
                {
                    used_maps++;
                }
                else { break; }
                pos = pos + 29;
            }
            mMaps = new DataMap[map];
            pos = 4;

            for (map = 0; map < used_maps; map++)
            {
                mMaps[map] = new DataMap();
                mMaps[map].MapIdentifier = BitConverter.ToUInt32(mBlockData, pos);
                pos = pos + 4;
                mMaps[map].MapType = mBlockData[pos];
                pos++;
                mMaps[map].MapOffset = BitConverter.ToUInt32(mBlockData, pos);
                pos = pos + 4;
                mMaps[map].MapDimension = mBlockData[pos];
                pos++;
                mMaps[map].MapDataType = mBlockData[pos];
                pos++;
                mMaps[map].MapXStart = BitConverter.ToInt16(mBlockData, pos);
                pos = pos + 2;
                mMaps[map].MapYStart = BitConverter.ToInt16(mBlockData, pos);
                pos = pos + 2;
                mMaps[map].MapZStart = BitConverter.ToInt16(mBlockData, pos);
                pos = pos + 2;
                mMaps[map].MapXStepsize = BitConverter.ToInt16(mBlockData, pos);
                pos = pos + 2;
                mMaps[map].MapYStepsize = BitConverter.ToInt16(mBlockData, pos);
                pos = pos + 2;
                mMaps[map].MapZStepsize = BitConverter.ToInt16(mBlockData, pos);
                pos = pos + 2;
                mMaps[map].MapXSteps = BitConverter.ToUInt16(mBlockData, pos);
                pos = pos + 2;
                mMaps[map].MapYSteps = BitConverter.ToUInt16(mBlockData, pos);
                pos = pos + 2;
                mMaps[map].MapZSteps = BitConverter.ToUInt16(mBlockData, pos);
                pos = pos + 2;
                mMaps[map].ReadData(mBlockData);
            }
        }

        /// <summary>Re-parse data to byte array</summary>
        public void GenerateByteArray()
        {
            int map = 0;
            byte[] buf;

            // Clear complete data
            for (int i = 0; i < (Block.MAX_BLOCK_SIZE - Block.BLOCK_HEADER_SIZE); i++) mBlockData[i] = 0xFF;
            // Copy structure
            mBlockData[0] = (byte)(mVersion.Hauptversion % 256);
            mBlockData[1] = (byte)(mVersion.Hauptversion / 256);
            mBlockData[2] = mVersion.Nebenversion;
            mBlockData[3] = mVersion.Revision;
            int pos = 4;
            for (map = 0; map < mMaps.Length; map++)
            {
                buf = BitConverter.GetBytes(mMaps[map].MapIdentifier);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                mBlockData[pos + 2] = buf[2];
                mBlockData[pos + 3] = buf[3];
                pos = pos + 4;

                mBlockData[pos] = mMaps[map].MapType;
                pos++;
                buf = BitConverter.GetBytes(mMaps[map].MapOffset);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                mBlockData[pos + 2] = buf[2];
                mBlockData[pos + 3] = buf[3];
                pos = pos + 4;
                mBlockData[pos] = mMaps[map].MapDimension;
                pos++;
                mBlockData[pos] = mMaps[map].MapDataType;
                pos++;
                buf = BitConverter.GetBytes(mMaps[map].MapXStart);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                pos = pos + 2;
                buf = BitConverter.GetBytes(mMaps[map].MapYStart);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                pos = pos + 2;
                buf = BitConverter.GetBytes(mMaps[map].MapZStart);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                pos = pos + 2;
                buf = BitConverter.GetBytes(mMaps[map].MapXStepsize);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                pos = pos + 2;
                buf = BitConverter.GetBytes(mMaps[map].MapYStepsize);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                pos = pos + 2;
                buf = BitConverter.GetBytes(mMaps[map].MapZStepsize);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                pos = pos + 2;
                buf = BitConverter.GetBytes(mMaps[map].MapXSteps);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                pos = pos + 2;
                buf = BitConverter.GetBytes(mMaps[map].MapYSteps);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                pos = pos + 2;
                buf = BitConverter.GetBytes(mMaps[map].MapZSteps);
                mBlockData[pos + 0] = buf[0];
                mBlockData[pos + 1] = buf[1];
                pos = pos + 2;
            }
            // Copy data
            for (map = 0; map < mMaps.Length; map++)
            {
                for (int i = 0; i < mMaps[map].Data.Length; i++)
                {
                    mBlockData[mMaps[map].MapOffset + i - 6] = mMaps[map].Data[i];
                }
            }
            GenerateChecksum();
        }

        /// <summary>Get identifier of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Identifier of map, or -1 on error</returns>
        public UInt32 GetDatamapIdentifier(int position)
        {
            UInt32 ret = 0xFFFFFFFF;
            if( mMaps != null){
                if (position < mMaps.Length)
                {
                    ret = mMaps[position].MapIdentifier;
                }
            }
            return ret;
        }

        /// <summary>Get type of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Type of data map</returns>
        public byte GetDatamapType(int position)
        {
            byte ret = 255;
            if (mMaps != null)
            {
                if (position < mMaps.Length)
                {
                    ret = (byte)mMaps[position].MapType;
                }
            }
            return ret;
        }

        /// <summary>Get dimension of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Dimansion of data map</returns>
        public byte GetDimension(int position)
        {
            byte ret = 0;
            if (mMaps != null)
            {
                if (position < mMaps.Length)
                {
                    ret = mMaps[position].MapDimension;
                }
            }
            return ret;
        }

        /// <summary>Get offset of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Offset of data map</returns>
        public UInt32 GetOffset(int position)
        {
            UInt32 ret = 0;
            if (mMaps != null)
            {
                if (position < mMaps.Length)
                {
                    ret = mMaps[position].MapOffset;
                }
            }
            return ret;
        }

        /// <summary>Get start values as string</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Start values as string</returns>
        public string GetStartString(int position)
        {
            string ret = "";
            if (mMaps != null)
            {
                if (position < mMaps.Length)
                {
                    ret = mMaps[position].MapXStart.ToString();
                    if (mMaps[position].MapDimension > 0)
                    {
                        ret += "/";
                        ret += mMaps[position].MapYStart.ToString();
                    }
                    if (mMaps[position].MapDimension > 1)
                    {
                        ret += "/";
                        ret += mMaps[position].MapZStart.ToString();
                    }
                }
            }
            return ret;
        }

        /// <summary>Get step sizes as string</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Step sizes as string</returns>
        public string GetStepsizeString(int position)
        {
            string ret = "";
            if (mMaps != null)
            {
                if (position < mMaps.Length)
                {
                    ret = mMaps[position].MapXStepsize.ToString();
                    if (mMaps[position].MapDimension > 0)
                    {
                        ret += "/";
                        ret += mMaps[position].MapYStepsize.ToString();
                    }
                    if (mMaps[position].MapDimension > 1)
                    {
                        ret += "/";
                        ret += mMaps[position].MapZStepsize.ToString();
                    }
                }
            }
            return ret;
        }

        /// <summary>Get number of steps as string</summary>
        /// <param name="position">Position of data map in block</param>
        /// <returns>Number of steps as string</returns>
        public string GetStepsString(int position)
        {
            string ret = "";
            if (mMaps != null)
            {
                if (position < mMaps.Length)
                {
                    ret = mMaps[position].MapXSteps.ToString();
                    if (mMaps[position].MapDimension > 0)
                    {
                        ret += "/";
                        ret += mMaps[position].MapYSteps.ToString();
                    }
                    if (mMaps[position].MapDimension > 1)
                    {
                        ret += "/";
                        ret += mMaps[position].MapZSteps.ToString();
                    }
                }
            }
            return ret;
        }

        /// <summary>Get axis parameters</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="XStart">Start value of x-axis</param>
        /// <param name="XSize">Size of stes on x-axis</param>
        /// <param name="XSteps">Number of steps on x-axis</param>
        /// <param name="YStart">Start value of y-axis</param>
        /// <param name="YSize">Size of stes on y-axis</param>
        /// <param name="YSteps">Number of steps on y-axis</param>
        /// <param name="ZStart">Start value of z-axis</param>
        /// <param name="ZSize">Size of stes on z-axis</param>
        /// <param name="ZSteps">Number of steps on z-axis</param>
        public void GetAxis(int position,
            out Int16 XStart, out Int16 XSize, out UInt16 XSteps,
            out Int16 YStart, out Int16 YSize, out UInt16 YSteps,
            out Int16 ZStart, out Int16 ZSize, out UInt16 ZSteps)
        {
            XStart = 0; XSize = 0; XSteps = 0;
            YStart = 0; YSize = 0; YSteps = 0;
            ZStart = 0; ZSize = 0; ZSteps = 0;
            if (mMaps != null)
            {
                if (position < mMaps.Length)
                {
                    XStart = mMaps[position].MapXStart;
                    XSize = mMaps[position].MapXStepsize;
                    XSteps = mMaps[position].MapXSteps;
                    YStart = mMaps[position].MapYStart;
                    YSize = mMaps[position].MapYStepsize;
                    YSteps = mMaps[position].MapYSteps;
                    ZStart = mMaps[position].MapZStart;
                    ZSize = mMaps[position].MapZStepsize;
                    ZSteps = mMaps[position].MapZSteps;
                }
            }
        }

        /// <summary>Get value of data map cell</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="x">Position on x-axis</param>
        /// <param name="y">Position on y-axis</param>
        /// <returns>Value of cell, or missing on error (65533)</returns>
        public UInt16 GetDatamapValue(int position, UInt16 x, UInt16 y)
        {
            int bytepos = 0;
            UInt16 ret = 65533;
            if (mMaps != null)
            {
                if (position < mMaps.Length)
                {
                    bytepos = (2 * (y * mMaps[position].MapXSteps));
                    bytepos += (2 * y);
                    bytepos += (2 * x);
                    ret = BitConverter.ToUInt16(mMaps[position].Data, bytepos);
                }
            }
            return ret;
        }

        /// <summary>Set value of data map cell</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="x">Position on x-axis</param>
        /// <param name="y">Position on y-axis</param>
        /// <param name="Content">New content of data map cell</param>
        /// <returns>True on success</returns>
        public bool SetDatamapValue(int position, UInt16 x, UInt16 y, UInt16 Content)
        {
            bool ret = false;
            int bytepos = 0;
            if (mMaps != null)
            {
                if (position < mMaps.Length)
                {
                    bytepos = (2 * (y * mMaps[position].MapXSteps));
                    bytepos += (2 * y);
                    bytepos += (2 * x);
                    mMaps[position].Data[bytepos] = (byte)(Content % 256);
                    mMaps[position].Data[bytepos + 1] = (byte)(Content / 256);
                    GenerateByteArray();
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>Get number of used bytes in block</summary>
        /// <param name="gaps">Number of gapping bytes</param>
        /// <param name="overlapp">Number of overlapping bytes</param>
        /// <returns>Number of totaly used bytes</returns>
        public UInt16 GetUsedSize(out UInt32 gaps, out UInt32 overlapp)
        {
            UInt32 ret = 474; // todo: ?
            UInt32 gap = 0;
            UInt32 ovr = 0;
            if (mMaps != null)
            {
                for (int i = 0; i < mMaps.Length; i++)
                {
                    if (mMaps[i].MapOffset != ret)
                    {
                        if (mMaps[i].MapOffset > ret)
                        {
                            gap += (UInt32)(mMaps[i].MapOffset - ret);
                            ret = mMaps[i].MapOffset;
                        }
                        else
                        {
                            ovr += (UInt32)(ret - mMaps[i].MapOffset);
                        }
                    }
                    if (mMaps[i].MapDimension != 0)
                    {
                        ret += (UInt32)(2 * ((mMaps[i].MapXSteps + 1) * (mMaps[i].MapYSteps + 1)));
                    }
                    else
                    {
                        ret += (UInt32)(2 * (mMaps[i].MapXSteps + 1));
                    }
                }
                gaps = gap;
                overlapp = ovr;
                if (ret > 65535)
                {
                    return 65535;
                }
                else
                {
                    return (UInt16)ret;
                }
            }
            else
            {
                gaps = 0;
                overlapp = 0;
                return 0;
            }
        }

        /// <summary>Set identifier of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="Id">New Identifier</param>
        public void SetDatamapIdentifier(int position, UInt32 Id)
        {
            mMaps[position].MapIdentifier = Id;
        }

        /// <summary>Set type of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="Type">New type</param>
        public void SetDatamapType(int position, byte Type)
        {
            mMaps[position].MapType = Type;
        }

        /// <summary>Set dimension of data map</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="Dimension">New dimension</param>
        public void SetDimension(int position, byte Dimension)
        {
            mMaps[position].MapDimension = Dimension;
        }

        /// <summary>Set axis parameters</summary>
        /// <param name="position">Position of data map in block</param>
        /// <param name="XStart">Start value of x-axis</param>
        /// <param name="XSize">Size of stes on x-axis</param>
        /// <param name="XSteps">Number of steps on x-axis</param>
        /// <param name="YStart">Start value of y-axis</param>
        /// <param name="YSize">Size of stes on y-axis</param>
        /// <param name="YSteps">Number of steps on y-axis</param>
        /// <param name="ZStart">Start value of z-axis</param>
        /// <param name="ZSize">Size of stes on z-axis</param>
        /// <param name="ZSteps">Number of steps on z-axis</param>
        public void SetAxis(int position,
            Int16 XStart, Int16 XSize, UInt16 XSteps,
            Int16 YStart, Int16 YSize, UInt16 YSteps,
            Int16 ZStart, Int16 ZSize, UInt16 ZSteps)
        {
            int new_size = 0;
            mMaps[position].MapXStart = XStart;
            mMaps[position].MapYStart = YStart;
            mMaps[position].MapZStart = ZStart;
            mMaps[position].MapXStepsize = XSize;
            mMaps[position].MapYStepsize = YSize;
            mMaps[position].MapZStepsize = ZSize;
            mMaps[position].MapXSteps = XSteps;
            mMaps[position].MapYSteps = YSteps;
            mMaps[position].MapZSteps = ZSteps;


            // calculate new byte usage
            if (mMaps[position].MapDimension != 0)
            {
                new_size = (2 * ((mMaps[position].MapXSteps + 1) * (mMaps[position].MapYSteps + 1)));
            }
            else
            {
                new_size = (2 * (mMaps[position].MapXSteps + 1));
            }
            int diff = new_size - mMaps[position].Data.Length;

            if (diff != 0)
            {
                // resize data
                byte[] buffer = new byte[mMaps[position].Data.Length];
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = mMaps[position].Data[i];
                }
                mMaps[position].Data = new byte[new_size];
                for (int i = 0; (i < buffer.Length) && (i < mMaps[position].Data.Length); i++)
                {
                    mMaps[position].Data[i] = buffer[i];
                }

                // offsets der folgenden Tasks!
                for (int map = position + 1; map < mMaps.Length; map++)   //wtr/jdr 2016-01-25 (Position +1, alt: nur Position)
                {
                    mMaps[map].MapOffset = (UInt32)(mMaps[map].MapOffset + diff);
                }
            }
        }

        /// <summary>Add an empty data map to block</summary>
        public void AddMap()
        {
            if (mMaps.Length < 16)
            {
                Array.Resize(ref mMaps, mMaps.Length + 1);
            }
            mMaps[mMaps.Length - 1] = new DataMap();
            mMaps[mMaps.Length - 1].MapIdentifier = 0;
            mMaps[mMaps.Length - 1].MapType = 0;
            mMaps[mMaps.Length - 1].MapDimension = 0;
            mMaps[mMaps.Length - 1].MapXStart = 0;
            mMaps[mMaps.Length - 1].MapYStart = -1;
            mMaps[mMaps.Length - 1].MapZStart = -1;
            mMaps[mMaps.Length - 1].MapXStepsize = 1;
            mMaps[mMaps.Length - 1].MapYStepsize = -1;
            mMaps[mMaps.Length - 1].MapZStepsize = -1;
            mMaps[mMaps.Length - 1].MapXSteps = 1;
            mMaps[mMaps.Length - 1].MapYSteps = 65535;
            mMaps[mMaps.Length - 1].MapZSteps = 65535;
            mMaps[mMaps.Length - 1].MapDataType = 2;
            uint new_offset = mMaps[mMaps.Length - 2].MapOffset;
            if (mMaps[mMaps.Length - 2].MapDimension != 0)
            {
                new_offset += (uint)(2 * ((mMaps[mMaps.Length - 2].MapXSteps + 1) * (mMaps[mMaps.Length - 2].MapYSteps + 1)));
            }
            else
            {
                new_offset += (uint)(2 * (mMaps[mMaps.Length - 2].MapXSteps + 1));
            }
            mMaps[mMaps.Length - 1].MapOffset = new_offset;
            mMaps[mMaps.Length - 1].Data = new byte[4];
        }
    }
}
