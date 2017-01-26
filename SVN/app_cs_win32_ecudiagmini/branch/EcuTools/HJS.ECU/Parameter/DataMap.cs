using System;
using System.Text;

namespace HJS.ECU.Parameter
{
    /// <summary>Single data map</summary>
    public class DataMap
    {
        private UInt32 mMapIdentifier;
        private byte mMapType;
        private UInt32 mMapOffset;
        private byte mMapDimension;
        private byte mMapDataType;
        private Int16 mMapXStart;
        private Int16 mMapXStepsize;
        private UInt16 mMapXSteps;
        private Int16 mMapYStart;
        private Int16 mMapYStepsize;
        private UInt16 mMapYSteps;
        private Int16 mMapZStart;
        private Int16 mMapZStepsize;
        private UInt16 mMapZSteps;
        private byte[] mData;

        /// <summary>Accessor to data map identifier</summary>
        public UInt32 MapIdentifier
        {
            get { return mMapIdentifier; }
            set { mMapIdentifier = value; }
        }

        /// <summary>Accessor to data map type</summary>
        public byte MapType
        {
            get { return mMapType; }
            set { mMapType = value; }
        }

        /// <summary>Accessor to data map offset</summary>
        public UInt32 MapOffset
        {
            get { return mMapOffset; }
            set { mMapOffset = value; }
        }

        /// <summary>Accessor to data map dimension</summary>
        public byte MapDimension
        {
            get { return mMapDimension; }
            set { mMapDimension = value; }
        }

        /// <summary>Accessor to data map data type</summary>
        public byte MapDataType
        {
            get { return mMapDataType; }
            set { mMapDataType = value; }
        }

        /// <summary>Accessor to data map x-axis start value</summary>
        public Int16 MapXStart
        {
            get { return mMapXStart; }
            set { mMapXStart = value; }
        }

        /// <summary>Accessor to data map x-axis step size value</summary>
        public Int16 MapXStepsize
        {
            get { return mMapXStepsize; }
            set { mMapXStepsize = value; }
        }

        /// <summary>Accessor to data map x-axis number of steps</summary>
        public UInt16 MapXSteps
        {
            get { return mMapXSteps; }
            set { mMapXSteps = value; }
        }

        /// <summary>Accessor to data map y-axis start value</summary>
        public Int16 MapYStart
        {
            get { return mMapYStart; }
            set { mMapYStart = value; }
        }

        /// <summary>Accessor to data map y-axis step size value</summary>
        public Int16 MapYStepsize
        {
            get { return mMapYStepsize; }
            set { mMapYStepsize = value; }
        }

        /// <summary>Accessor to data map y-axis number of steps</summary>
        public UInt16 MapYSteps
        {
            get { return mMapYSteps; }
            set { mMapYSteps = value; }
        }

        /// <summary>Accessor to data map z-axis start value</summary>
        public Int16 MapZStart
        {
            get { return mMapZStart; }
            set { mMapZStart = value; }
        }

        /// <summary>Accessor to data map z-axis step size value</summary>
        public Int16 MapZStepsize
        {
            get { return mMapZStepsize; }
            set { mMapZStepsize = value; }
        }

        /// <summary>Accessor to data map z-axis number of steps</summary>
        public UInt16 MapZSteps
        {
            get { return mMapZSteps; }
            set { mMapZSteps = value; }
        }

        /// <summary>Byte array of data</summary>
        public byte[] Data
        {
            get { return mData; }
            set { mData = value; }
        }

        /// <summary>Default constructor</summary>
        public DataMap()
        {
        }

        /// <summary>Copy data from block array</summary>
        /// <param name="Source">Block byte array</param>
        public void ReadData(byte[] Source)
        {
            int size = 0;
            if (mMapDataType != 2) return;

            if (mMapDimension != 0)
            {
                size = (2 * ((mMapXSteps + 1) * (mMapYSteps + 1)));
            }
            else
            {
                size = (2 * (mMapXSteps + 1));
            }

            mData = new byte[size];
            for (int i = 0; i < size; i++)
            {
                mData[i] = Source[mMapOffset + i - 6];
            }
        }
    }
}
