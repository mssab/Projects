/*
 * Object: HJS.ECU.Port.SerialDirect
 * Description: Communication object for direct connection to HJS-ECU
 * 
 * $LastChangedDate: 2013-10-25 14:40:14 +0200 (Fr, 25 Okt 2013) $
 * $LastChangedRevision: 25 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/tags/Version_1_3_1/lib_cs_win32_hjsecu/SerialDirect.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.IO.Ports;

namespace HJS.ECU.Port
{
    /// <summary>
    /// Serial communication object class
    /// </summary>
    public class SerialDirect : Comm, IDisposable
    {
        /// <summary>
        /// Accessors for Keyword
        /// Write only
        /// </summary>
        public void SetKey (byte[] ucaKey)
        {
            if (ucaKey.Length != 8)
                throw new ArgumentException("EcuProtocol: Size of key mismatch!");
            else
            {
                mKey[0] = ucaKey[0];
                mKey[1] = ucaKey[1];
                mKey[2] = ucaKey[2];
                mKey[3] = ucaKey[3];
                mKey[4] = ucaKey[4];
                mKey[5] = ucaKey[5];
                mKey[6] = ucaKey[6];
                mKey[7] = ucaKey[7];
            }
        }

        /// <summary>
        /// Accessors for Size of data (or value of order)
        /// </summary>
        public UInt16 Size
        {
            get { return (UInt16)(((int)(mSizeHighByte) * 256) + mSizeLowByte); }
            set
            {
                mSizeLowByte = (byte)(value % 256);
                mSizeHighByte = (byte)(value / 256);
            }
        }

        /// <summary>
        /// Accessors for checksum of last received communication
        /// Read only
        /// </summary>
        public UInt16 Checksum
        {
            get { return (UInt16)((int)(mChecksumHighByte * 256) + mChecksumLowByte); }
        }

        /// <summary>
        /// Accessors for time stamp of last received communication
        /// Read only
        /// </summary>
        public override UInt32 TimeStamp
        {
            get
            {
                UInt32 ret = mTimeStamp3;
                ret *= 256;
                ret += mTimeStamp2;
                ret *= 256;
                ret += mTimeStamp1;
                ret *= 256;
                ret += mTimeStamp0;
                return ret;
            }
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="strPortName">Name of new serial port</param>
        /// <param name="ucVersion">Version of protocol</param>
        /// <param name="ucaKey">Byte array of new ECU key</param>
        /// <param name="ucaClient">Byte array of client identifier</param>
        public SerialDirect(string strPortName, byte ucVersion, byte[] ucaKey, byte[] ucaClient)
        {
            // Create class local arrays
            mClient = new byte[7];
            mKey = new byte[8];
            // Set standard values
            mClient[0] = 72;
            mClient[1] = 74;
            mClient[2] = 83;
            mClient[3] = 45;
            mClient[4] = 69;
            mClient[5] = 67;
            mClient[6] = 85;
            mServer = 100;// 112; // 100;
            mOrder = OrderByte.Alive;
            mSizeHighByte = 0;
            mSizeLowByte = 0;
            // Set selected port
            PortName = strPortName;
            // Set selected protocol version
            pVersion = ucVersion;
            // Set selected protocol keyword
            if (ucaKey.Length != 8)
            {
                throw new ArgumentException("EcuProtocol: Size of key mismatch!");
            }
            else
            {

                mKey[0] = ucaKey[0];
                mKey[1] = ucaKey[1];
                mKey[2] = ucaKey[2];
                mKey[3] = ucaKey[3];
                mKey[4] = ucaKey[4];
                mKey[5] = ucaKey[5];
                mKey[6] = ucaKey[6];
                mKey[7] = ucaKey[7];
            }
            // Set selected client identifier
            if (ucaKey.Length != 8)
            {
                throw new ArgumentException("EcuProtocol: Size of client mismatch!");
            }
            else
            {

                mClient[0] = ucaClient[0];
                mClient[1] = ucaClient[1];
                mClient[2] = ucaClient[2];
                mClient[3] = ucaClient[3];
                mClient[4] = ucaClient[4];
                mClient[5] = ucaClient[5];
                mClient[6] = ucaClient[6];
            }
        }

        /// <summary>
        /// Dispose of managed and native resouces
        /// </summary>
        /// <param name="disposing">True if managed members should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mComPort != null) mComPort.Close();
            }
            else
            {
            }
        }

        /// <summary>
        /// Dispose of instantiable members
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Open connection to ECU
        /// </summary>
        public override ReturnValue Connect()
        {
            // Create port
            if (mComPort != null)
            {
                if (mComPort.IsOpen)
                {
                    mComPort.Close();
                }
                mComPort.PortName = PortName;
            }
            else
            {
                mComPort = new SerialPort(PortName, 38400, Parity.None, 8, StopBits.One);
            }
            // Set Timeouts
            mComPort.WriteTimeout = 1000; // down from 6s
            mComPort.ReadTimeout = 1000; // down from 6s
            // Set Handshake
            mComPort.Handshake = Handshake.None;
            mComPort.DiscardNull = false;
            mComPort.RtsEnable = false;
            // Open the port
            ReturnValue ret = ReturnValue.NoError;
            try
            {
                mComPort.Open();
            }
            catch (UnauthorizedAccessException)
            {
                ret = ReturnValue.PortInUse;
            }
            catch (System.IO.IOException)
            {
                ret = ReturnValue.InvalidPort;
            }
            catch (TimeoutException)
            {
                ret = ReturnValue.TimeOut;
            }
            // Is an online ECU connected?
            if(ret == ReturnValue.NoError){
                ret = Order(OrderByte.Alive);
            }
            return ret;
        }

        /// <summary>
        /// Close Connection to ECU
        /// </summary>
        public override void Disconnect()
        {

            if (mComPort != null)
            {
                mComPort.Dispose();
                mComPort.Close();
            }
        }

        /// <summary>
        /// Write data to ECU
        /// </summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Value">Size of data or parameter</param>
        /// <param name="Data">Source data byte array</param>
        /// <param name="ExecutionTime">Execution time between sending request and response</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Write(OrderByte STB, UInt16 Value, ref byte[] Data, UInt16 ExecutionTime)
        {
            ReturnValue ret = ReturnValue.NoError;

            // Do not send anything without opened port!
            if (!mComPort.IsOpen)
            {
                ret = ReturnValue.PortNotOpened;
            }
            else
            {
                // Set protocol  data length
                Size = Value;   // Size = (UInt16)Data.Length;
                // Write order
                WriteHeader(STB);
                // Write data
                for (int i = 0; i < Data.Length; i++)
                {
                    mComPort.Write(Data, i, 1);
                }
                // Wait during execution
                if (ExecutionTime != 0) System.Threading.Thread.Sleep(ExecutionTime);
                // Receive answer
                ret = WaitForSync();
                if (ret == ReturnValue.Retry || ret == ReturnValue.TimeOut)
                {
                    WriteHeader(STB);
                    // Write data
                    for (int i = 0; i < Data.Length; i++)
                    {
                        mComPort.Write(Data, i, 1);
                    }
                    // Wait during execution
                    if (ExecutionTime != 0) System.Threading.Thread.Sleep(ExecutionTime);
                    // Receive answer
                    ret = WaitForSync();
                }
                if (ret == ReturnValue.NoError)
                {
                    ret = ReadHeader();
                }
            }
            return ret;
        }

        /// <summary>Write data to ECU after erasing target area</summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Data">BYte array of data</param>
        /// <param name="ExecutionTime">Execution time between sending request and response</param>
        /// <param name="EraseTime">Time required for erasing between sending header and sending data</param>
        /// <returns>0 on success, else error (see ReturnValue)</returns>
        public override ReturnValue EraseAndWrite(OrderByte STB, ref byte[] Data, UInt16 ExecutionTime, UInt16 EraseTime)
        {
            ReturnValue ret = ReturnValue.NoError;

            // Do not send anything without opened port!
            if (!mComPort.IsOpen)
            {
                ret = ReturnValue.PortNotOpened;
            }
            else
            {
                // Set protocol  data length
                Size = (UInt16)Data.Length;
                // Write order
                WriteHeader(STB);
                // Wait for target to erase
                if (ExecutionTime != 0) System.Threading.Thread.Sleep(EraseTime);
                // Receive answer
                ret = WaitForSync();
                //Console.WriteLine("< sync_a=" + ret);
                if (ret == ReturnValue.NoError)
                {
                    ret = ReadHeader();
                    //Console.WriteLine("< header_a=" + ret);
                    // Write data
                    //Console.WriteLine("> data");
                    for (int i = 0; i < Data.Length; i++)
                    {
                        mComPort.Write(Data, i, 1);
                    }
                    // Wait during execution
                    if (ExecutionTime != 0) System.Threading.Thread.Sleep(ExecutionTime);
                    // Receive answer
                    ret = WaitForSync();
                    //Console.WriteLine("< sync_b=" + ret);
                    if (ret == ReturnValue.NoError)
                    {
                        ret = ReadHeader();
                        //Console.WriteLine("< header_b=" + ret);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Read data from ECU
        /// </summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Data">Target data byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Read(OrderByte STB, out byte[] Data)
        {
            ReturnValue ret = ReturnValue.NoError;

            // Do not send anything without opened port!
            if (!mComPort.IsOpen)
            {
                Data = new byte[0];
                ret = ReturnValue.PortNotOpened;
            }
            else
            {
                // Write order (read request)
                WriteHeader(STB);
                // Receive Answer
                ret = WaitForSync();
                if (ret == ReturnValue.Retry || ret == ReturnValue.TimeOut)
                {
                    WriteHeader(STB);
                    // Receive answer
                    ret = WaitForSync();
                }
                if (ret == ReturnValue.NoError)
                {
                    ret = ReadHeader();
                    if (ret != ReturnValue.NoError)
                    {
                        Data = new byte[0];
                    }
                    else
                    {
                        // Set new site of data
                        Data = new byte[Size];
                        try
                        {
                            // Receive data bytes
                            for (int i = 0; i < Size; i++)
                            {
                                Data[i] = (byte)mComPort.ReadByte();
                            }
                        }
                        catch (TimeoutException)
                        {
                            return ReturnValue.TimeOut;
                        }
                    }
                }
                else
                {
                    Data = new byte[0];
                }
            }
            return ret;
        }

        /// <summary>
        /// Override Read data from ECU with value for Size parameter
        /// </summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Value">Special value to be send in Size parameter</param>
        /// <param name="Data">Target data byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Read(OrderByte STB, UInt16 Value, out byte[] Data)
        {
            ReturnValue ret = ReturnValue.NoError;

            // Do not send anything without opened port!
            if (!mComPort.IsOpen)
            {
                Data = new byte[0];
                return ReturnValue.PortNotOpened;
            }
            else
            {
                // Prepare value
                Size = Value;
                // Write order (read request)
                WriteHeader(STB);
                // Receive Answer
                ret = WaitForSync();
                if (ret == ReturnValue.Retry || ret == ReturnValue.TimeOut)
                {
                    WriteHeader(STB);
                    // Receive answer
                    ret = WaitForSync();
                }
                if (ret == ReturnValue.NoError)
                {
                    ret = ReadHeader();
                    if (ret != ReturnValue.NoError)
                    {
                        Data = new byte[0];
                    }
                    else
                    {
                        // Set new site of data
                        Data = new byte[Size];
                        try
                        {
                            // Receive data bytes
                            for (int i = 0; i < Size; i++)
                            {
                                Data[i] = (byte)mComPort.ReadByte();
                            }
                        }
                        catch (TimeoutException)
                        {
                            return ReturnValue.TimeOut;
                        }
                    }
                }
                else
                {
                    Data = new byte[0];
                }
            }
            return ret;
        }

        /// <summary>
        /// Send an order to ECU
        /// </summary>
        /// <param name="STB">Communication order identifier</param>
        /// <returns>0 on success, else error (see ErrorByte)</returns>
        public override ReturnValue Order(OrderByte STB)
        {
            ReturnValue ret = ReturnValue.NoError;

            // Do not send anything without opened port!
            if (!mComPort.IsOpen)
            {
                return ReturnValue.PortNotOpened;
            }
            else
            {
                // Do not use data
                Size = 0;
                // Write order
                WriteHeader(STB);
                // Receive answer
                ret = WaitForSync();
                if (ret == ReturnValue.Retry || ret == ReturnValue.TimeOut)
                {
                    WriteHeader(STB);
                    // Receive answer
                    ret = WaitForSync();
                }
                if (ret == ReturnValue.NoError)
                {
                    ret = ReadHeader();
                }
            }
            return ret;
        }

        /// <summary>
        /// Override of sending order to ECU with an 16-bit-value
        /// </summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Value">Value of order</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Order(OrderByte STB, UInt16 Value)
        {
            ReturnValue ret = ReturnValue.NoError;

            // Do not send anything without opened port!
            if (!mComPort.IsOpen)
            {
                return ReturnValue.PortNotOpened;
            }
            else
            {
                // Prepare value
                Size = Value;
                // Write order
                WriteHeader(STB);
                // Receive answer
                ret = WaitForSync();
                if (ret == ReturnValue.Retry || ret == ReturnValue.TimeOut)
                {
                    WriteHeader(STB);
                    // Receive answer
                    ret = WaitForSync();
                }
                if (ret == ReturnValue.NoError)
                {
                    ret = ReadHeader();
                }
            }
            return ret;
        }

        /// <summary>
        /// Override of sending order to ECU with an 16-bit-value and wait for ECU to execute command
        /// </summary>
        /// <param name="STB">Communication order identifier</param>
        /// <param name="Value">Value of order</param>
        /// <param name="ExecutionTime">Wait execution time of ECU in ms</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public override ReturnValue Order(OrderByte STB, UInt16 Value, UInt16 ExecutionTime)
        {
            ReturnValue ret = ReturnValue.NoError;

            // Do not send anything without opened port!
            if (!mComPort.IsOpen)
            {
                return ReturnValue.PortNotOpened;
            }
            else
            {
                // Prepare value
                Size = Value;
                // Write order
                WriteHeader(STB);
                // Wait during execution
                if (ExecutionTime != 0) System.Threading.Thread.Sleep(ExecutionTime);
                // Receive answer
                ret = WaitForSync();
                if (ret == ReturnValue.Retry || ret == ReturnValue.TimeOut)
                {
                    WriteHeader(STB);
                    // Wait during execution
                    if (ExecutionTime != 0) System.Threading.Thread.Sleep(ExecutionTime);
                    // Receive answer
                    ret = WaitForSync();
                }
                if (ret == ReturnValue.NoError)
                {
                    ret = ReadHeader();
                }
            }
            return ret;
        }

        #region private members

        /// <summary>
        /// Serial port object
        /// </summary>
        private SerialPort mComPort;

        /// <summary>
        /// First synchronization byte (0x55 = 01010101b)
        /// </summary>
        private const byte SYNC_0 = 85;

        /// <summary>
        /// Second synchronization byte (0xAA = 10101010b)
        /// </summary>
        private const byte SYNC_1 = 170;

        /// <summary>
        /// Third synchronization byte (0x55 = 01010101b)
        /// </summary>
        private const byte SYNC_2 = 85;

        /// <summary>
        /// Identifier of client ("HJS-ECU", or "TESTER ")
        /// </summary>
        private byte[] mClient;

        /// <summary>
        /// ECU keyword
        /// </summary>
        private byte[] mKey;

        /// <summary>
        /// Size (low byte of 16-Bit-Value)
        /// </summary>
        private byte mSizeLowByte;

        /// <summary>
        /// Size (high byte of 16-Bit-Value)
        /// </summary>
        private byte mSizeHighByte;

        /// <summary>
        /// Current order byte
        /// </summary>
        private OrderByte mOrder;

        /// <summary>
        /// Checksum of last received communication (low byte of 16-Bit-Value)
        /// </summary>
        private byte mChecksumLowByte;

        /// <summary>
        /// Checksum of last received communication (high byte of 16-Bit-Value)
        /// </summary>
        private byte mChecksumHighByte;

        /// <summary>
        /// First byte of time stamp of last communication
        /// </summary>
        private byte mTimeStamp0;

        /// <summary>
        /// Second byte of time stamp of last communication
        /// </summary>
        private byte mTimeStamp1;

        /// <summary>
        /// Third byte of time stamp of last communication
        /// </summary>
        private byte mTimeStamp2;

        /// <summary>
        /// Fourth byte of time stamp of last communication
        /// </summary>
        private byte mTimeStamp3;

        /// <summary>
        /// Write header to ECU
        /// </summary>
        /// <param name="STB"></param>
        private void WriteHeader(OrderByte STB)
        {
            byte[] header = new byte[23];
            // Save current order byte
            mOrder = STB;
            Console.WriteLine(">STB." + STB);
            // Prepare header
            header[0] = SYNC_0;
            header[1] = SYNC_1;
            header[2] = SYNC_2;
            header[3] = mClient[0];
            header[4] = mClient[1];
            header[5] = mClient[2];
            header[6] = mClient[3];
            header[7] = mClient[4];
            header[8] = mClient[5];
            header[9] = mClient[6];
            header[10] = mServer;
            header[11] = pVersion;
            header[12] = (byte)mOrder;
            header[13] = mSizeLowByte;
            header[14] = mSizeHighByte;
            header[15] = mKey[0];
            header[16] = mKey[1];
            header[17] = mKey[2];
            header[18] = mKey[3];
            header[19] = mKey[4];
            header[20] = mKey[5];
            header[21] = mKey[6];
            header[22] = mKey[7];
            // Send header bytes to ECU
            try
            {
                mComPort.Write(header, 0, header.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// Synchronize receiving header from ECU
        /// </summary>
        /// <returns></returns>
        private ReturnValue WaitForSync()
        {
            int ReadBuffer = 0;
            bool Sync = false;

            try
            {
                while (!Sync)
                {
                    // Wait for first byte
                    try
                    {
                        ReadBuffer = mComPort.ReadByte();
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                        return ReturnValue.Retry;
                    }
                    if ((byte)ReadBuffer != SYNC_0)
                        continue;

                    // Try reading second sync. byte
                    ReadBuffer = mComPort.ReadByte();
                    if ((byte)ReadBuffer == SYNC_1)
                    {
                        // Try reading third sync. byte
                        ReadBuffer = mComPort.ReadByte();
                        if ((byte)ReadBuffer == SYNC_2)
                        {
                            Sync = true;
                        }
                        else
                        {
                            // Third sync. byte did not match
                            return ReturnValue.Retry;     // Retry because communication was aborted by unsynchronous data
                        }
                    }
                    else
                    {
                        // Second byte did not match sync. bytes
                        return ReturnValue.Retry;     // Retry because communication was aborted by unsynchronous data
                    }
                }
            }
            catch (TimeoutException)
            {
                return ReturnValue.TimeOut;
            }
            return ReturnValue.NoError;
        }

        /// <summary>
        /// Receive rest of header (without sync. bytes)
        /// </summary>
        /// <returns></returns>
        private ReturnValue ReadHeader()
        {
            int ReadBuffer = 0;

            try
            {
                // Read client id
                for (int i = 0; i < 7; i++)
                {
                    ReadBuffer = mComPort.ReadByte();
                    if ((byte)ReadBuffer != mClient[i])
                    {
                        return ReturnValue.Retry;     // Retry because communication was aborted by unsynchronous data
                    }
                }
                // Read server id
                ReadBuffer = mComPort.ReadByte();
                if ((byte)ReadBuffer != 3) // ECU task id of communication task
                {
                    //ignore this error return ErrorByte.Unsychronous;
                }
                // Read version 
                ReadBuffer = mComPort.ReadByte();
                if ((byte)ReadBuffer != pVersion)
                {
                    return ReturnValue.VersionMismatch;
                }
                // Read order byte
                ReadBuffer = mComPort.ReadByte();
                if ((byte)ReadBuffer != (byte)mOrder)
                {
                    return ReturnValue.ComOrderFailed;
                }
                // Read status 
                ReadBuffer = mComPort.ReadByte();
                if ((byte)ReadBuffer != (byte)StatusByte.NoError)
                {
                    switch ((StatusByte)ReadBuffer)
                    {
                        case StatusByte.NoError:
                            break;
                        case StatusByte.NoDecive:
                        case StatusByte.ModeFailed:
                        case StatusByte.OrderFailed:
                        case StatusByte.UnknownOrder:
                            return ReturnValue.ComOrderFailed;
                        case StatusByte.ChecksumError:
                            return ReturnValue.ChecksumMismatch;
                        case StatusByte.PasswordError:
                            return ReturnValue.PasswordFailed;
                        case StatusByte.VersionError:
                            return ReturnValue.VersionMismatch;
                        case StatusByte.SyncError:
                            return ReturnValue.Retry;
                    }
                }
                // Read size
                ReadBuffer = mComPort.ReadByte();
                mSizeLowByte = (byte)ReadBuffer;
                ReadBuffer = mComPort.ReadByte();
                mSizeHighByte = (byte)ReadBuffer;
                // Read checksum 
                ReadBuffer = mComPort.ReadByte();
                mChecksumLowByte = (byte)ReadBuffer;
                ReadBuffer = mComPort.ReadByte();
                mChecksumHighByte = (byte)ReadBuffer;
                ReadBuffer = mComPort.ReadByte();
                // Read time stamp
                mTimeStamp0 = (byte)ReadBuffer;
                ReadBuffer = mComPort.ReadByte();
                mTimeStamp1 = (byte)ReadBuffer;
                ReadBuffer = mComPort.ReadByte();
                mTimeStamp2 = (byte)ReadBuffer;
                ReadBuffer = mComPort.ReadByte();
                mTimeStamp3 = (byte)ReadBuffer;
            }
            catch (TimeoutException)
            {
                return ReturnValue.TimeOut;
            }
            return ReturnValue.NoError;
        }

        #endregion
    }
}
