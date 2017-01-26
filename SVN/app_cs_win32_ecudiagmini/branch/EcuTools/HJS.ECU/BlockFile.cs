/*
 * Object: HJS.BlockFile
 * Description: Object for files containig block objects
 * 
 * $LastChangedDate: 2014-06-25 10:18:41 +0200 (Mi, 25 Jun 2014) $
 * $LastChangedRevision: 61 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/BlockFile.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.IO;

namespace HJS
{
    /// <summary>Block file object class</summary>
    public class BlockFile
    {
        /// <summary>Enumeration for identifiers different block file types
        /// (file extensions)</summary>
        public enum FileIdentifier : byte
        {
            /// <summary>Null enumeration</summary>
            None = 0,
            /// <summary>Parameter set (K)</summary>
            ParameterSet = 75,
            /// <summary>Draft of parameter set (V)</summary>
            Draft = 86,
            /// <summary>Data read from System (D)</summary>
            ReadData = 68
        }

        /// <summary>Open block file (for reading)</summary>
        /// <param name="Id">Identifier of file</param>
        /// <param name="FileName">Path to file
        /// including name with extension</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue Open(FileIdentifier Id, string FileName)
        {
            ReturnValue RetVal = ReturnValue.NoError;
            byte[] FileHeaderByteArray = new byte[FILE_HEADER_SIZE];
            UInt16 uiRevision;

            // Check Filename
            if(File.Exists(FileName))
            {
                try
                {
                    // Read file header
                    hFile = File.Open(FileName, FileMode.Open, FileAccess.Read);
                    hFile.Read(FileHeaderByteArray, 0, FILE_HEADER_SIZE);
                    // Check first three letters of header
                    // as file extension identifier
                    if (   (FileHeaderByteArray[0] != (byte)Id)
                        || (FileHeaderByteArray[1] != FILE_ID_2)
                        || (FileHeaderByteArray[2] != FILE_ID_3))
                    {
                        RetVal = ReturnValue.InvalidFile;
                    }
                    else
                    {
                        // Check file version
                        uiRevision = (UInt16)(
                            ((UInt16)(FileHeaderByteArray[4]) * 256)
                            + FileHeaderByteArray[3]);
                        if (uiRevision != VERSION_OF_BLOCK_FILE)
                        {
                            RetVal = ReturnValue.VersionMismatch;
                        }
                        else
                        {
                            // Read number of contained blocks
                            mNumberOfBlocks = FileHeaderByteArray[5];
                            // Check Offset to first block
                            if (FileHeaderByteArray[6] != FILE_HEADER_SIZE)
                            {
                                RetVal = ReturnValue.InvalidFile;
                            }
                        }
                    }
                }
                catch
                {
                    // Currently no differs between any exceptions !
                    RetVal = ReturnValue.FileReadError;
                }
            }
            else
            {
                RetVal =  ReturnValue.FileNotFound;
            }
            return RetVal;
        }

        /// <summary>Close block file</summary>
	    public void Close()
		{
            mNumberOfBlocks = 0;
            if (hFile != null)
            {
                hFile.Close();
            }
		}

        /// <summary>Create new block file
        /// Existing files will be truncated</summary>
        /// <param name="Id">File extension identifier</param>
        /// <param name="FileName">Name of new file
        /// (including file extension)</param>
        /// <param name="CreateAuthorBlock">Flag if author block is
        /// automatically generated and attached to file</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue Create(FileIdentifier Id,
            string FileName, bool CreateAuthorBlock)
        {
            ReturnValue RetVal = ReturnValue.NoError;

            try
            {
                // todo: wird eine existierende datei ueberschrieben?
                hFile = File.Open(FileName,
                    FileMode.Create,
                    FileAccess.ReadWrite);
                // Write file header
                hFile.WriteByte((byte)Id);
                hFile.WriteByte(FILE_ID_2);
                hFile.WriteByte(FILE_ID_3);
                hFile.WriteByte((byte)(VERSION_OF_BLOCK_FILE % 256));
                hFile.WriteByte((byte)(VERSION_OF_BLOCK_FILE / 256));
                hFile.WriteByte(mNumberOfBlocks);
                hFile.WriteByte(FILE_HEADER_SIZE);
                for (byte b = 7; b < FILE_HEADER_SIZE; b++)
                {
                    hFile.WriteByte(0);
                }
                // If create author flag is on, one block is generated
                if (CreateAuthorBlock)
                {
                    AuthorBlock ab = new AuthorBlock();
                    ab.Generate();
                    PutBlock(ab);
                }
            }
            catch
            {
                // Currently no differs between any exceptions !
                RetVal = ReturnValue.FileWriteError;
            }
            return RetVal;
        }

        /// <summary>Get certain block from file</summary>
        /// <param name="pBlock">Reference to target block</param>
        /// <param name="BlockType">Type of block</param>
        /// <param name="KeepVersion">Flag if current version of target
        /// block is not allowed to be changed</param>
        /// <returns>0 on success, else see ReturnValue</returns>
	    public ReturnValue GetBlock(out Block pBlock,
            Block.BlockId BlockType, bool KeepVersion)
        {
            byte[] BlockHeaderByteArray = new byte[Block.BLOCK_HEADER_SIZE];
            int lOff = FILE_HEADER_SIZE;
            ReturnValue RetVal = ReturnValue.NoError;
            pBlock = new Block();
            pBlock.Type = BlockType;
            if (mNumberOfBlocks > 0)
            {
                for (byte i = 1; i <= mNumberOfBlocks; i++)
                {
                    try
                    {
                        hFile.Position = lOff;
                        hFile.Read(BlockHeaderByteArray, 0, Block.BLOCK_HEADER_SIZE);
                        lOff += Block.BLOCK_HEADER_SIZE;
                        if (BlockHeaderByteArray[(int)Block.HeaderPosition.Type] == (byte)BlockType)
                        {
                            break;
                        }
                        lOff += BlockHeaderByteArray[(int)Block.HeaderPosition.SizeHighByte] * 256;
                        lOff += BlockHeaderByteArray[(int)Block.HeaderPosition.SizeLowByte];
                    }
                    catch
                    {
                        RetVal = ReturnValue.FileReadError;
                        break;
                    }
                }
                if (RetVal == ReturnValue.NoError)
                {
                    if (BlockHeaderByteArray[(int)Block.HeaderPosition.Type] != (byte)BlockType)
                    {
                        RetVal = ReturnValue.BlockNotFound;
                    }
                    else
                    {
                        // read block header
                        if (pBlock.UpdateHeader(ref BlockHeaderByteArray, KeepVersion) == false)
                        {
                            RetVal = ReturnValue.VersionMismatch;
                        }
                        // adjust file pointer for reading block header again!
                        //hFile.Position = lOff;
                        hFile.Position = hFile.Position - Block.BLOCK_HEADER_SIZE;
                        byte[] BlockByteArray = new byte[pBlock.DataSize + Block.BLOCK_HEADER_SIZE];
                        try
                        {
                            hFile.Read(BlockByteArray, 0, (pBlock.DataSize + Block.BLOCK_HEADER_SIZE));
                        }
                        catch
                        {
                            RetVal = ReturnValue.FileReadError;
                        }
                        if (RetVal == ReturnValue.NoError)
                        {
                            RetVal = pBlock.ReadRaw(ref BlockByteArray, KeepVersion);
                        }
                    }
                }
                else
                {
                    RetVal = ReturnValue.FileEmpty;
                }
            }
            else
            {
                pBlock = null;
            }
            return RetVal; 
        }

        /// <summary>Append a block to a file
        /// This function can not replace existing blocks!</summary>
        /// <param name="pBlock">Block to append</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue PutBlock(Block pBlock)
        {
            ReturnValue RetVal = ReturnValue.NoError;
            byte[] WriteBuffer;

            if (pBlock == null) return ReturnValue.BlockNotFound;

            mNumberOfBlocks++;

            if (pBlock.WriteRaw(out WriteBuffer) == ReturnValue.NoError)
            {
                // Adjust block number in header
                hFile.Seek(5, SeekOrigin.Begin);
                hFile.WriteByte(mNumberOfBlocks);
                // Write block
                hFile.Seek(0, SeekOrigin.End);
                hFile.Write(WriteBuffer, 0, Block.BLOCK_HEADER_SIZE + pBlock.DataSize);
            }
            else
            {
                RetVal = ReturnValue.BlockNotFound;
            }
            return RetVal;
        }

        /// <summary>Block file format version number</summary>
	    private const UInt16 VERSION_OF_BLOCK_FILE = 1;

        /// <summary>constant for second identifier byte : B</summary>
        private const byte FILE_ID_2 = 66;

        /// <summary>constant for third identifier byte : F</summary>
        private const byte FILE_ID_3 = 70;

        /// <summary>Number of containing blocks</summary>
        private byte mNumberOfBlocks;

        /// <summary>Size of block header</summary>
        private const int FILE_HEADER_SIZE = 16;

        /// <summary>Handle to binary file</summary>
	    FileStream hFile;
    }
}
