/*
 * Object: HJS.ECU.LanguageBlock
 * Description: Block of language texts
 * 
 * $LastChangedDate: 2015-03-05 13:27:15 +0100 (Do, 05 Mrz 2015) $
 * $LastChangedRevision: 100 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/LanguageBlock.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Text;

// todo: GetValue() sollte andere dezimaltrennzeichen (locale) unterstuetzen
namespace HJS.ECU
{
    /// <summary>Class for language blocks</summary>
    public class LanguageBlock : Block
    {
        #region Private Members
        /// <summary>Name of actual value</summary>
        private string[] mValueName;

        /// <summary>Number of Decimals of actual value</summary>
        private byte[] mValueDecimals;

        /// <summary>Actual value is hexadecimal</summary>
        private bool[] mValueHexValue;

        /// <summary>Actual value is signed</summary>
        private bool[] mValueSigned;

        /// <summary>Actual value is in group of calculated values</summary>
        private bool[] mValueGroup;

        /// <summary>Actual value is hidden</summary>
        private bool[] mValueHidden;

        /// <summary>Actual value is hidden in displays</summary>
        private bool[] mValueHiddenInDisplay;

        /// <summary>Unit of actual value</summary>
        private string[] mValueUnit;

        /// <summary>Factor for alternative Unit (formally known as A)</summary>
        private byte[] mValueFaktor;

        /// <summary>Divisor for alternative Unit (formally known as B)</summary>
        private byte[] mValueDivisor;

        /// <summary>Offset for alternative Unit (formally known as C)</summary>
        private string[] mValueOffset;

        /// <summary>Alternative unit of actual value</summary>
        private string[] mValueAltUnit;

        /// <summary>Name of error event</summary>
        private string[] mErrorName;

        /// <summary>Flag if error is event</summary>
        private bool[] mErrorEvent;

        /// <summary>Flag if error is hidden</summary>
        private bool[] mErrorHidden;

        /// <summary>Flag if error is shown on displays</summary>
        private bool[] mErrorLcdShow;

        /// <summary>Flag if error is blue led</summary>
        private bool[] mErrorBlueLed;

        /// <summary>Name of behave</summary>
        private string[] mBehaveName;

        /// <summary>Flag if calculation into alternative unit is denied</summary>
        private bool mNoAltUnit;

        /// <summary>Array size of Values</summary>
        private const int MAX_NUMBER_OF_VALUES = 256;

        /// <summary>Array size of errors</summary>
        private const int MAX_NUMBER_OF_ERRORS = 64;

        /// <summary>Array size of behaves</summary>
        private const int MAX_NUMBER_OF_BEHAVES = 16;

        /// <summary>Enumeration of language groups</summary>
        private enum Group : int
        {
            /// <summary>Measured and calculated values</summary>
            Values = 0,
            /// <summary>Errors and events</summary>
            Errors = 1,
            /// <summary>Behaves</summary>
            Behaves = 2,
            /// <summary>Maximum number of groups</summary>
            Max = 3
        }

        /// <summary>Delimiter for columns (null termination)</summary>
        private const byte DELIMITER_COLUMN = 0;

        /// <summary>Delimiter for rows (\N)</summary>
        private const byte DELIMITER_ROW = 10;

        /// <summary>Delimiter for groups (\R)</summary>
        private const byte DELIMITER_GROUP = 13;

        /// <summary>Offset in byte array for value factor and value divisor</summary>
        private const byte FACTOR_OFFSET = 13;

        /// <summary>Number of parsed bytes</summary>
        private int mParsedBytes;
        #endregion

        /// <summary>Flag if calculation into alternative unit is denied</summary>
        public bool NoAltUnit
        {
            get
            {
                return mNoAltUnit;
            }
            set
            {
                mNoAltUnit = value;
            }
        }

        /// <summary>Default constructor</summary>
        public LanguageBlock()
        {
            Type = BlockId.IdLngEN;
            Version = 1;
            DataSize = 4090;// 4096 - 6;
            mParsedBytes = 0;

            mNoAltUnit = false;

            mValueName = new string[MAX_NUMBER_OF_VALUES];
            mValueDecimals = new byte[MAX_NUMBER_OF_VALUES];
            mValueHexValue = new bool[MAX_NUMBER_OF_VALUES];
            mValueSigned = new bool[MAX_NUMBER_OF_VALUES];
            mValueGroup = new bool[MAX_NUMBER_OF_VALUES];
            mValueHidden = new bool[MAX_NUMBER_OF_VALUES];
            mValueHiddenInDisplay = new bool[MAX_NUMBER_OF_VALUES];
            mValueUnit = new string[MAX_NUMBER_OF_VALUES];
            mValueFaktor = new byte[MAX_NUMBER_OF_VALUES];
            mValueDivisor = new byte[MAX_NUMBER_OF_VALUES];
            mValueOffset = new string[MAX_NUMBER_OF_VALUES];
            mValueAltUnit = new string[MAX_NUMBER_OF_VALUES];

            mErrorName = new string[MAX_NUMBER_OF_ERRORS];
            mErrorEvent = new bool[MAX_NUMBER_OF_ERRORS];
            mErrorHidden = new bool[MAX_NUMBER_OF_ERRORS];
            mErrorLcdShow = new bool[MAX_NUMBER_OF_ERRORS];
            mErrorBlueLed = new bool[MAX_NUMBER_OF_ERRORS];

            mBehaveName = new string[MAX_NUMBER_OF_BEHAVES];
        }

        /// <summary>Overloaded constructor</summary>
        /// <param name="language">Block identifier of new language</param>
        public LanguageBlock(BlockId language)
        {
            switch (language)
            {
                case BlockId.IdLngDE:
                    Type = BlockId.IdLngDE;
                    break;
                case BlockId.IdLngEN:
                    Type = BlockId.IdLngEN;
                    break;
                case BlockId.IdLngFR:
                    Type = BlockId.IdLngFR;
                    break;
                case BlockId.IdLngIT:
                    Type = BlockId.IdLngIT;
                    break;
                case BlockId.IdLngES:
                    Type = BlockId.IdLngES;
                    break;
                case BlockId.IdLngPL:
                    Type = BlockId.IdLngPL;
                    break;
                case BlockId.IdLngNL:
                    Type = BlockId.IdLngNL;
                    break;
                default:
                    Type = BlockId.IdLngEN;
                    break;
            }
            Version = 1;
            DataSize = 4090;// 4096 - 6;
            mParsedBytes = 0;

            mValueName = new string[MAX_NUMBER_OF_VALUES];
            mValueDecimals = new byte[MAX_NUMBER_OF_VALUES];
            mValueHexValue = new bool[MAX_NUMBER_OF_VALUES];
            mValueSigned = new bool[MAX_NUMBER_OF_VALUES];
            mValueGroup = new bool[MAX_NUMBER_OF_VALUES];
            mValueHidden = new bool[MAX_NUMBER_OF_VALUES];
            mValueHiddenInDisplay = new bool[MAX_NUMBER_OF_VALUES];
            mValueUnit = new string[MAX_NUMBER_OF_VALUES];
            mValueFaktor = new byte[MAX_NUMBER_OF_VALUES];
            mValueDivisor = new byte[MAX_NUMBER_OF_VALUES];
            mValueOffset = new string[MAX_NUMBER_OF_VALUES];
            mValueAltUnit = new string[MAX_NUMBER_OF_VALUES];

            mErrorName = new string[MAX_NUMBER_OF_ERRORS];
            mErrorEvent = new bool[MAX_NUMBER_OF_ERRORS];
            mErrorHidden = new bool[MAX_NUMBER_OF_ERRORS];
            mErrorLcdShow = new bool[MAX_NUMBER_OF_ERRORS];
            mErrorBlueLed = new bool[MAX_NUMBER_OF_ERRORS];

            mBehaveName = new string[MAX_NUMBER_OF_BEHAVES];
        }

        /// <summary>Overloaded constructor with copying from base class</summary>
        /// <param name="b">Base class object</param>
        public LanguageBlock(Block b)
        {
            Version = 1;
            DataSize = 4090;// 4096 - 6;
            mParsedBytes = 0;

            mValueName = new string[MAX_NUMBER_OF_VALUES];
            mValueDecimals = new byte[MAX_NUMBER_OF_VALUES];
            mValueHexValue = new bool[MAX_NUMBER_OF_VALUES];
            mValueSigned = new bool[MAX_NUMBER_OF_VALUES];
            mValueGroup = new bool[MAX_NUMBER_OF_VALUES];
            mValueHidden = new bool[MAX_NUMBER_OF_VALUES];
            mValueHiddenInDisplay = new bool[MAX_NUMBER_OF_VALUES];
            mValueUnit = new string[MAX_NUMBER_OF_VALUES];
            mValueFaktor = new byte[MAX_NUMBER_OF_VALUES];
            mValueDivisor = new byte[MAX_NUMBER_OF_VALUES];
            mValueOffset = new string[MAX_NUMBER_OF_VALUES];
            mValueAltUnit = new string[MAX_NUMBER_OF_VALUES];

            mErrorName = new string[MAX_NUMBER_OF_ERRORS];
            mErrorEvent = new bool[MAX_NUMBER_OF_ERRORS];
            mErrorHidden = new bool[MAX_NUMBER_OF_ERRORS];
            mErrorLcdShow = new bool[MAX_NUMBER_OF_ERRORS];
            mErrorBlueLed = new bool[MAX_NUMBER_OF_ERRORS];

            mBehaveName = new string[MAX_NUMBER_OF_BEHAVES];

            switch (b.Type)
            {
                case BlockId.IdLngDE:
                    Type = BlockId.IdLngDE;
                    break;
                case BlockId.IdLngEN:
                    Type = BlockId.IdLngEN;
                    break;
                case BlockId.IdLngFR:
                    Type = BlockId.IdLngFR;
                    break;
                case BlockId.IdLngIT:
                    Type = BlockId.IdLngIT;
                    break;
                case BlockId.IdLngES:
                    Type = BlockId.IdLngES;
                    break;
                case BlockId.IdLngPL:
                    Type = BlockId.IdLngPL;
                    break;
                case BlockId.IdLngNL:
                    Type = BlockId.IdLngNL;
                    break;
                default:
                    Type = BlockId.IdInvalid;
                    break;
            }
            if (Type != BlockId.IdInvalid)
            {
                // import rest
                byte[] _data;
                b.GetData(out _data);
                if (ReadPlain(ref _data) == ReturnValue.NoError)
                {
                    Parse();
                }
            }
        }

        /// <summary>Read language block from plain data (raw array from old protocol i.e. 14)</summary>
        /// <param name="SourceData">Reference to plain byte array</param>
        /// <returns>0 on success, else see ReturnValue</returns>
        public ReturnValue ReadPlain(ref byte[] SourceData)
        {
            ReturnValue ret = ReturnValue.NoError;
            if (SourceData == null)
            {
                ret = ReturnValue.BlockNotFound;
            }
            else
            {
                // Read block size
                DataSize = (UInt16)(SourceData.Length);
                // Check data size in header to size of byte array
                if (DataSize <= MAX_BLOCK_SIZE)
                {
                    // Read block data
                    mBlockData = new byte[DataSize];
                    for (int i = 0; i < DataSize; i++)
                    {
                        mBlockData[i] = SourceData[i];
                    }
                    // old block contains no checksum
                    GenerateChecksum();
                }
                else
                {
                    ret = ReturnValue.SizeMismatch;
                }
            }
            return ret;
        }

        /// <summary>Read language information (block) from byte array</summary>
        /// <returns>0 on success, see ReturnValue</returns>
        public ReturnValue Parse()
        {
            ReturnValue ret = ReturnValue.NoError;

            Group GroupInBlock = Group.Values;
            UInt16 RowInGroup = 0;
            UInt16 ColInRow = 0;
            UInt16 ByteInCol = 0;

            for (int BytePosition = 0; BytePosition < DataSize; BytePosition++)
            {
                if ((mBlockData[BytePosition] == DELIMITER_COLUMN) ||
                     (mBlockData[BytePosition] == DELIMITER_ROW) ||
                     (mBlockData[BytePosition] == DELIMITER_GROUP))
                {
                    // Delimiter detected
                    switch (mBlockData[BytePosition])
                    {
                        case DELIMITER_COLUMN:
                            // Next column
                            ColInRow++;
                            ByteInCol = 0;
                            break;
                        case DELIMITER_ROW:
                            // Next row
                            if (((GroupInBlock == Group.Values) && (ColInRow > (UInt16)MAX_NUMBER_OF_VALUES)) ||
                                ((GroupInBlock == Group.Errors) && (ColInRow > (UInt16)MAX_NUMBER_OF_ERRORS)) ||
                                ((GroupInBlock == Group.Behaves) && (ColInRow > (UInt16)MAX_NUMBER_OF_BEHAVES)))
                            {
                                ret = ReturnValue.BlockHeaderInvalid;   // Data damaged!
                            }
                            else
                            {
                                RowInGroup++;
                            }
                            ColInRow = 0;
                            break;
                        case DELIMITER_GROUP:
                            // Next group
                            if (GroupInBlock == Group.Behaves) { GroupInBlock = Group.Max; } // ende
                            if (GroupInBlock == Group.Errors) { GroupInBlock = Group.Behaves; }
                            if (GroupInBlock == Group.Values) { GroupInBlock = Group.Errors; }
                            RowInGroup = 0;
                            mParsedBytes = BytePosition;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    // Data byte
                    if (GroupInBlock == Group.Values)
                    {
                        switch (ColInRow)
                        {
                            case 0: // Name of value
                                mValueName[RowInGroup] += (char)mBlockData[BytePosition];
                                break;
                            case 1: // Flags for displaying value
                                // Decimals and or hexadecimal
                                mValueDecimals[RowInGroup] = (byte)(mBlockData[BytePosition] & 7);
                                if (mValueDecimals[RowInGroup] == 7)
                                {
                                    mValueHexValue[RowInGroup] = true;
                                    mValueDecimals[RowInGroup] = 0;
                                }
                                else
                                {
                                    mValueHexValue[RowInGroup] = false;
                                }
                                // other flags
                                mValueSigned[RowInGroup] = ((mBlockData[BytePosition] & 8) != 0);
                                mValueGroup[RowInGroup] = ((mBlockData[BytePosition] & 16) != 0);
                                mValueHidden[RowInGroup] = ((mBlockData[BytePosition] & 32) != 0);
                                mValueHiddenInDisplay[RowInGroup] = ((mBlockData[BytePosition] & 128) != 0);
                                // Preset optional row elements
                                mValueUnit[RowInGroup] = "";
                                mValueFaktor[RowInGroup] = 0;
                                mValueDivisor[RowInGroup] = 1;
                                mValueOffset[RowInGroup] = "";
                                break;
                            case 2: // Unit of value
                                mValueUnit[RowInGroup] += (char)mBlockData[BytePosition];
                                break;
                            case 3: // Calculation parameters of alternative unit (optional)
                                switch (ByteInCol)
                                {
                                    case 0:
                                        mValueFaktor[RowInGroup] = (byte)(mBlockData[BytePosition] - FACTOR_OFFSET);
                                        ByteInCol++;
                                        break;
                                    case 1:
                                        mValueDivisor[RowInGroup] = (byte)(mBlockData[BytePosition] - FACTOR_OFFSET);
                                        ByteInCol++;
                                        break;
                                    case 2:
                                        if (mBlockData[BytePosition] == DELIMITER_COLUMN)
                                        {
                                            ByteInCol++;
                                        }
                                        else
                                        {
                                            mValueOffset[RowInGroup] += (char)mBlockData[BytePosition];
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 4: // Alternative unit of value (optional)
                                ByteInCol = 0;
                                mValueAltUnit[RowInGroup] += (char)mBlockData[BytePosition];
                                break;
                            default:
                                break;
                        }
                    }
                    if (GroupInBlock == Group.Errors)
                    {
                        switch (ColInRow)
                        {
                            case 0: // Name of error
                                mErrorName[RowInGroup] += (char)mBlockData[BytePosition];
                                break;
                            case 1: // Flags for displaying error
                                mErrorHidden[RowInGroup] = ((mBlockData[BytePosition] & 1) != 0);
                                mErrorEvent[RowInGroup] = ((mBlockData[BytePosition] & 2) != 0);
                                mErrorLcdShow[RowInGroup] = ((mBlockData[BytePosition] & 4) != 0);
                                mErrorBlueLed[RowInGroup] = ((mBlockData[BytePosition] & 0x10) != 0);
                                break;
                            default:
                                break;
                        }
                    }
                    if (GroupInBlock == Group.Behaves)
                    {
                        if (ColInRow == 0) // Name of behave
                        {
                            mBehaveName[RowInGroup] += (char)mBlockData[BytePosition];
                        }
                    }
                }
            }
            adjustCodepage();

            return ret;
        }

        /// <summary>Adjust encoding codepage according to current language via blockidentifier</summary>
        private void adjustCodepage()
        {
            // codepage anpassen  (28597 = Griechisch(iso-8859-7) )
            if (Type == BlockId.IdLngPL)
            {
                Encoding cpEN = Encoding.GetEncoding(28591); // Westeuropäisch (iso-8859-1)
                Encoding cpPL = Encoding.GetEncoding(28592); // Mitteleuropäisch (iso-8859-2)
                byte[] cur_strg;
                for (int i = 0; i < mValueName.Length; i++)
                {
                    if (mValueName[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mValueName[i]);
                        mValueName[i] = cpPL.GetString(cur_strg);
                    }
                }
                for (int i = 0; i < mValueUnit.Length; i++)
                {
                    if (mValueUnit[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mValueUnit[i]);
                        mValueUnit[i] = cpPL.GetString(cur_strg);
                    }
                }
                for (int i = 0; i < mValueAltUnit.Length; i++)
                {
                    if (mValueAltUnit[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mValueAltUnit[i]);
                        mValueAltUnit[i] = cpPL.GetString(cur_strg);
                    }
                }
                for (int i = 0; i < mBehaveName.Length; i++)
                {
                    if (mBehaveName[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mBehaveName[i]);
                        mBehaveName[i] = cpPL.GetString(cur_strg);
                    }
                }
                for (int i = 0; i < mErrorName.Length; i++)
                {
                    if (mErrorName[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mErrorName[i]);
                        mErrorName[i] = cpPL.GetString(cur_strg);
                    }
                }
            }
            else if(Type == BlockId.IdLngTK)
            {
                Encoding cpEN = Encoding.GetEncoding(28591); // Westeuropäisch (iso-8859-1)
                Encoding cpTK = Encoding.GetEncoding(28599); // Türkisch (iso-8859-9)
                byte[] cur_strg;
                for (int i = 0; i < mValueName.Length; i++)
                {
                    if (mValueName[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mValueName[i]);
                        mValueName[i] = cpTK.GetString(cur_strg);
                    }
                }
                for (int i = 0; i < mValueUnit.Length; i++)
                {
                    if (mValueUnit[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mValueUnit[i]);
                        mValueUnit[i] = cpTK.GetString(cur_strg);
                    }
                }
                for (int i = 0; i < mValueAltUnit.Length; i++)
                {
                    if (mValueAltUnit[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mValueAltUnit[i]);
                        mValueAltUnit[i] = cpTK.GetString(cur_strg);
                    }
                }
                for (int i = 0; i < mBehaveName.Length; i++)
                {
                    if (mBehaveName[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mBehaveName[i]);
                        mBehaveName[i] = cpTK.GetString(cur_strg);
                    }
                }
                for (int i = 0; i < mErrorName.Length; i++)
                {
                    if (mErrorName[i] != null)
                    {
                        cur_strg = cpEN.GetBytes(mErrorName[i]);
                        mErrorName[i] = cpTK.GetString(cur_strg);
                    }
                }
            }
        }

        /// <summary>Get number of used bytes (detected while / after parsing byte array)</summary>
        /// <returns>Number of used bytes</returns>
        public UInt16 getParsedBytes()
        {
            return (UInt16)(mParsedBytes + 1); // + 1 weil 0 indiziert
        }

        /// <summary>Get number of used bytes in language data array</summary>
        /// <returns>Number of used bytes in language data array</returns>
        public UInt16 PreCheckSize()
        {
	        UInt16 i = 0, j = 0;
	        int uiSize = 6; // blockheader
            for (i = 0; i < mValueName.Length; i++)
            {
                if (mValueName[i] != null) j++;
            }

            for (i = 0; i < j; i++)
            {
                uiSize += mValueName[i].Length;
		        uiSize++;	// Nullterminierung
		        uiSize++;	// Formatbyte
		        uiSize++;	// Nullterminierung
                uiSize += mValueUnit[i].Length;
		        uiSize++;	// Nullterminierung
                if (mValueFaktor[i] != 0)
                {
			        uiSize++;
                    if (mValueDivisor[i] != 0)
                    {
				        uiSize++;
                        //if (mValueOffset[i].Length != 0)
                        //{
                        //    CString x;
                        //    x.Format("%x", mValueName[i].uiOffset);
                            uiSize += mValueOffset[i].Length;
				        //}
			        }
			        uiSize++;	// Nullterminierung
			        uiSize += mValueAltUnit[i].Length;
			        uiSize++;	// Nullterminierung
		        }
		        uiSize++;	// Zeilenendezeichen
	        }
	        uiSize++;	// Blockendezeichen
	        for(i = 0; i < 64; i++){
		        uiSize += mErrorName[i].Length;
		        uiSize++;	// Nullterminierung
		        uiSize++;	// Formatbyte
		        uiSize++;	// Nullterminierung
		        uiSize++;	// Zeilenendezeichen
	        }
	        uiSize++;	// Blockendezeichen
	        for(i = 0; i < 16; i++){
		        uiSize += mBehaveName[i].Length;
		        uiSize++;	// Nullterminierung
		        uiSize++;	// Zeilenendezeichen
	        }
	        uiSize++;	// Blockendezeichen

	        return (UInt16)uiSize;
        }

        /// <summary>Write language information (block) to byte array</summary>
        public void ConvertBuffer()
        {
	        UInt16 i = 0;
            UInt16 pos = 0;
            byte ucFormat = 0;
            byte[] stringBuffer;
            Encoding cp;

            mBlockData = new byte[DataSize];

            // Select appropriate code page
            switch (Type)
            {
                case BlockId.IdLngPL:
                    cp = Encoding.GetEncoding(28592); // Mitteleuropäisch (iso-8859-2)
                    break;
                case BlockId.IdLngTK:
                    cp = Encoding.GetEncoding(28599); // Türkisch (iso-8859-9)
                    break;
                case BlockId.IdLngDE:
                case BlockId.IdLngEN:
                case BlockId.IdLngFR:
                case BlockId.IdLngIT:
                case BlockId.IdLngES:
                case BlockId.IdLngNL:
                default:
                    cp = Encoding.GetEncoding(28591); // Westeuropäisch (iso-8859-1)
                    break;
            }

	        // Values
            for (i = 0; i < getNumberOfUsedValues(); i++)
            {
		        // Value Name
                stringBuffer = cp.GetBytes(mValueName[i]);
                for (UInt16 j = 0; j < stringBuffer.Length; j++)
                {
                    mBlockData[pos] = stringBuffer[j];
                    pos++;
                }
                // Terminierung value name
                mBlockData[pos] = 0x00;
                pos++;

		        // Value format
		        ucFormat = 0x40;
		        if(mValueHexValue[i]){
			        ucFormat |= 0x07;
		        }else{
			        ucFormat |= (byte)(mValueDecimals[i] & 0x07);
		        }
		        if(mValueSigned[i]) {ucFormat |= 0x08;};
		        if(mValueGroup[i]) {ucFormat |= 0x10;};
		        if(mValueHidden[i]) {ucFormat |= 0x20;};
		        if(mValueHiddenInDisplay[i]) {ucFormat |= 0x80;};
                mBlockData[pos] = ucFormat;
                pos++;
                // Terminierung value format
                mBlockData[pos] = 0x00;
		        pos++;

		        // Value unit
                stringBuffer = cp.GetBytes(mValueUnit[i]);
                for (UInt16 j = 0; j < stringBuffer.Length; j++)
                {
                    mBlockData[pos] = stringBuffer[j];
                    pos++;
                }
                // Terminierung value name
                mBlockData[pos] = 0x00;
                pos++;

                // Value umrechnung fuer alt. unit
		        if(mValueFaktor[i] != 0){
			        //a schreiben
                    mBlockData[pos] = (byte)(mValueFaktor[i] + 13);
			        pos++;
			        if(mValueDivisor[i] != 0){
				        //b schreiben
                        mBlockData[pos] = (byte)(mValueDivisor[i] + 13);
				        pos++;
				        if(!String.IsNullOrEmpty(mValueOffset[i])){
				        //c schreiben
                            stringBuffer = cp.GetBytes(mValueOffset[i]);
                            for (UInt16 j = 0; j < stringBuffer.Length; j++)
                            {
                                mBlockData[pos] = stringBuffer[j];
                                pos++;
                            }
                        }
			        }
                    // Terminierung umrechnung
                    mBlockData[pos] = 0x00;
			        pos++;
			        //Alt unit
                    stringBuffer = cp.GetBytes(mValueAltUnit[i]);
                    for (UInt16 j = 0; j < stringBuffer.Length; j++)
                    {
                        mBlockData[pos] = stringBuffer[j];
                        pos++;
                    }
                    // Terminierung value name
                    mBlockData[pos] = 0x00;
                    pos++;
                }

		        // Zeilenende
                mBlockData[pos] = (byte)'\n';
		        pos++;
	        }

	        // Blockende
	        mBlockData[pos] = (byte)'\r';
	        pos++;

	        // Events, Errors
	        for(i = 0; i < 64; i++){
		        // Event Name
                stringBuffer = cp.GetBytes(mErrorName[i]);
                for (UInt16 j = 0; j < stringBuffer.Length; j++)
                {
                    mBlockData[pos] = stringBuffer[j];
                    pos++;
                }
                // Terminierung event name
                mBlockData[pos] = 0x00;
                pos++;

		        // Event format
		        ucFormat = 0x40;
		        if(mErrorHidden[i]) {ucFormat |= 0x01;};
		        if(mErrorEvent[i]) {ucFormat |= 0x02;};
		        if(mErrorLcdShow[i]) {ucFormat |= 0x04;};
		        if(mErrorBlueLed[i]) {ucFormat |= 0x10;};
                mBlockData[pos] = ucFormat;
                pos++;
                mBlockData[pos] = 0x00;
		        pos++;

		        // Zeilenende
                mBlockData[pos] = (byte)'\n';
		        pos++;
	        }

	        // Blockende
            mBlockData[pos] = (byte)'\r';
	        pos++;

	        // Behaves
	        for(i = 0; i < 16; i++){
		        // Behave Name
                stringBuffer = cp.GetBytes(mBehaveName[i]);
                for (UInt16 j = 0; j < stringBuffer.Length; j++)
                {
                    mBlockData[pos] = stringBuffer[j];
                    pos++;
                }
                // Terminierung behave name
                mBlockData[pos] = 0x00;
                pos++;

		        // Zeilenende
                mBlockData[pos] = (byte)'\n';
		        pos++;
	        }

	        // Blockende
            mBlockData[pos] = (byte)'\r';
	        pos++;

            // Header aktualisieren
            GenerateChecksum();
        }

        /// <summary>Upgrade content according to HJS.ECU.Firmware.Messwert(9 from 8)</summary>
        /// <returns>True on success</returns>
        public ReturnValue Upgrade8to9()
        {
            Firmware fm = new Firmware(8);
            if (getNumberOfUsedValues() != fm.GetValueNumber())
                return ReturnValue.VersionMismatch; // Source not V8
            // save old language
            string[] _ValueName = mValueName;
            byte[] _ValueDecimals = mValueDecimals;
            bool[] _ValueHexValue = mValueHexValue;
            bool[] _ValueSigned = mValueSigned;
            bool[] _ValueGroup = mValueGroup;
            bool[] _ValueHidden = mValueHidden;
            bool[] _ValueHiddenInDisplay = mValueHiddenInDisplay;
            string[] _ValueUnit = mValueUnit;
            byte[] _ValueFaktor = mValueFaktor;
            byte[] _ValueDivisor = mValueDivisor;
            string[] _ValueOffset = mValueOffset;
            string[] _ValueAltUnit = mValueAltUnit;
            // create new arrays
            mValueName = new string[255];
            mValueDecimals = new byte[255];
            mValueHexValue = new bool[255];
            mValueSigned = new bool[255];
            mValueGroup = new bool[255];
            mValueHidden = new bool[255];
            mValueHiddenInDisplay = new bool[255];
            mValueUnit = new string[255];
            mValueFaktor = new byte[255];
            mValueDivisor = new byte[255];
            mValueOffset = new string[255];
            mValueAltUnit = new string[255];
            //Fill

            // MRW_UI_UB to MRW_UI_TANKINHALT_LITER
            for (int i = 0; i < 24; i++ )
            {
                mValueName[i] = _ValueName[i];
                mValueDecimals[i] = _ValueDecimals[i];
                mValueHexValue[i] = _ValueHexValue[i];
                mValueSigned[i] = _ValueSigned[i];
                mValueGroup[i] = _ValueGroup[i];
                mValueHidden[i] = _ValueHidden[i];
                mValueHiddenInDisplay[i] = _ValueHiddenInDisplay[i];
                mValueUnit[i] = _ValueUnit[i];
                mValueFaktor[i] = _ValueFaktor[i];
                mValueDivisor[i] = _ValueDivisor[i];
                mValueOffset[i] = _ValueOffset[i];
                mValueAltUnit[i] = _ValueAltUnit[i];
            }
            // MRW_UI_TANKINHALT_LITER
            mValueName[24] = _ValueName[24];
            mValueDecimals[24] = 0;
            mValueHexValue[24] = _ValueHexValue[24];
            mValueSigned[24] = _ValueSigned[24];
            mValueGroup[24] = _ValueGroup[24];
            mValueHidden[24] = _ValueHidden[24];
            mValueHiddenInDisplay[24] = _ValueHiddenInDisplay[24];
            mValueUnit[24] = _ValueUnit[24];
            mValueFaktor[24] = _ValueFaktor[24];
            mValueDivisor[24] = _ValueDivisor[24];
            mValueOffset[24] = _ValueOffset[24];
            mValueAltUnit[24] = _ValueAltUnit[24];
            // MRW_UI_GESCHWINDIGKEIT to MRW_UI_CAN_VALUE_4
            for (int i = 25; i < 69; i++)
            {
                mValueName[i] = _ValueName[i];
                mValueDecimals[i] = _ValueDecimals[i];
                mValueHexValue[i] = _ValueHexValue[i];
                mValueSigned[i] = _ValueSigned[i];
                mValueGroup[i] = _ValueGroup[i];
                mValueHidden[i] = _ValueHidden[i];
                mValueHiddenInDisplay[i] = _ValueHiddenInDisplay[i];
                mValueUnit[i] = _ValueUnit[i];
                mValueFaktor[i] = _ValueFaktor[i];
                mValueDivisor[i] = _ValueDivisor[i];
                mValueOffset[i] = _ValueOffset[i];
                mValueAltUnit[i] = _ValueAltUnit[i];
            }
            // MRW_UI_CAN_VALUE_5 to MRW_UI_CAN_VALUE_8
            for (int i = 69; i < 73; i++)
            {
                mValueName[i] = "-";
                mValueDecimals[i] = 0;
                mValueHexValue[i] = false;
                mValueSigned[i] = false;
                mValueGroup[i] = false;
                mValueHidden[i] = true;
                mValueHiddenInDisplay[i] = true;
                mValueUnit[i] = "-";
                mValueFaktor[i] = _ValueFaktor[1]; // No alternative unit (taken from MRW_UI_TIME)
                mValueDivisor[i] = _ValueDivisor[1];
                mValueOffset[i] = _ValueOffset[1];
                mValueAltUnit[i] = _ValueAltUnit[1];
            }
            // MRW_SI_CAN_VALUE_9 and _10 (from MRW_SI_CAN_VALUE_5 and _6)
            for (int i = 73; i < 75; i++)
            {
                mValueName[i] = _ValueName[i - 4];
                mValueDecimals[i] = _ValueDecimals[i - 4];
                mValueHexValue[i] = _ValueHexValue[i - 4];
                mValueSigned[i] = _ValueSigned[i - 4];
                mValueGroup[i] = _ValueGroup[i - 4];
                mValueHidden[i] = _ValueHidden[i - 4];
                mValueHiddenInDisplay[i] = _ValueHiddenInDisplay[i - 4];
                mValueUnit[i] = _ValueUnit[i - 4];
                mValueFaktor[i] = _ValueFaktor[i - 4];
                mValueDivisor[i] = _ValueDivisor[i - 4];
                mValueOffset[i] = _ValueOffset[i - 4];
                mValueAltUnit[i] = _ValueAltUnit[i - 4];
            }
            // MRW_SI_CAN_VALUE_11 to MRW_SI_CAN_VALUE_16
            for (int i = 75; i < 81; i++)
            {
                mValueName[i] = "-";
                mValueDecimals[i] = 0;
                mValueHexValue[i] = false;
                mValueSigned[i] = false;
                mValueGroup[i] = false;
                mValueHidden[i] = true;
                mValueHiddenInDisplay[i] = true;
                mValueUnit[i] = "-";
                mValueFaktor[i] = _ValueFaktor[1]; // No alternative unit (taken from MRW_UI_TIME)
                mValueDivisor[i] = _ValueDivisor[1];
                mValueOffset[i] = _ValueOffset[1];
                mValueAltUnit[i] = _ValueAltUnit[1];
            }
            // MRW_UI_CAN_ORDER to MRW_UI_MAF_PUMP_STATUS
            for (int i = 81; i < 90; i++)
            {
                mValueName[i] = _ValueName[i - 10];
                mValueDecimals[i] = _ValueDecimals[i - 10];
                mValueHexValue[i] = _ValueHexValue[i - 10];
                mValueSigned[i] = _ValueSigned[i - 10];
                mValueGroup[i] = _ValueGroup[i - 10];
                mValueHidden[i] = _ValueHidden[i - 10];
                mValueHiddenInDisplay[i] = _ValueHiddenInDisplay[i - 10];
                mValueUnit[i] = _ValueUnit[i - 10];
                mValueFaktor[i] = _ValueFaktor[i - 10];
                mValueDivisor[i] = _ValueDivisor[i - 10];
                mValueOffset[i] = _ValueOffset[i - 10];
                mValueAltUnit[i] = _ValueAltUnit[i - 10];
            }
            //MRW_UI_ABSOLUTDRUCK
            mValueName[90] = "-";
            mValueDecimals[90] = 0;
            mValueHexValue[90] = false;
            mValueSigned[90] = false;
            mValueGroup[90] = false;
            mValueHidden[90] = true;
            mValueHiddenInDisplay[90] = true;
            mValueUnit[90] = "-";
            mValueFaktor[90] = _ValueFaktor[1]; // No alternative unit (taken from MRW_UI_TIME)
            mValueDivisor[90] = _ValueDivisor[1];
            mValueOffset[90] = _ValueOffset[1];
            mValueAltUnit[90] = _ValueAltUnit[1];
            // MRW_ULL_DOSIERIMPULSE_TANK to MRW_ULH_DOSIERIMPULSE
            for (int i = 91; i < 97; i+=2)
            {
                // ULL
                mValueName[i] = _ValueName[i - 11];
                mValueDecimals[i] = _ValueDecimals[i - 11];
                mValueHexValue[i] = _ValueHexValue[i - 11];
                mValueSigned[i] = _ValueSigned[i - 11];
                mValueGroup[i] = _ValueGroup[i - 11];
                mValueHidden[i] = _ValueHidden[i - 11];
                mValueHiddenInDisplay[i] = _ValueHiddenInDisplay[i - 11];
                mValueUnit[i] = "ml";
                mValueFaktor[i] = _ValueFaktor[1]; // No alternative unit (taken from MRW_UI_TIME
                mValueDivisor[i] = _ValueDivisor[1];
                mValueOffset[i] = _ValueOffset[1];
                mValueAltUnit[i] = _ValueAltUnit[1];
                // ULH
                mValueName[i+ 1] = _ValueName[i - 10];
                mValueDecimals[i + 1] = _ValueDecimals[i - 10];
                mValueHexValue[i + 1] = _ValueHexValue[i - 10];
                mValueSigned[i + 1] = _ValueSigned[i - 10];
                mValueGroup[i + 1] = _ValueGroup[i - 10];
                mValueHidden[i + 1] = _ValueHidden[i - 10];
                mValueHiddenInDisplay[i + 1] = _ValueHiddenInDisplay[i - 10];
                mValueUnit[i + 1] = _ValueUnit[i - 10];
                mValueFaktor[i + 1] = _ValueFaktor[i - 10];
                mValueDivisor[i + 1] = _ValueDivisor[i - 10];
                mValueOffset[i + 1] = _ValueOffset[i - 10];
                mValueAltUnit[i + 1] = _ValueAltUnit[i - 10];
            }
            // MRW_UI_ADDITIV_IST to MRW_ULL_TANKINHALT (=MRW_UI_TANKINHALT)
            for (int i = 97; i < 100; i++)
            {
                mValueName[i] = _ValueName[i - 11];
                mValueDecimals[i] = _ValueDecimals[i - 11];
                mValueHexValue[i] = _ValueHexValue[i - 11];
                mValueSigned[i] = _ValueSigned[i - 11];
                mValueGroup[i] = _ValueGroup[i - 11];
                mValueHidden[i] = _ValueHidden[i - 11];
                mValueHiddenInDisplay[i] = _ValueHiddenInDisplay[i - 11];
                mValueUnit[i] = _ValueUnit[i - 11];
                mValueFaktor[i] = _ValueFaktor[i - 11];
                mValueDivisor[i] = _ValueDivisor[i - 11];
                mValueOffset[i] = _ValueOffset[i - 11];
                mValueAltUnit[i] = _ValueAltUnit[i - 11];
            }
            // MRW_ULH_TANKINHALT
            mValueName[100] = "do";
            mValueDecimals[100] = mValueDecimals[99];
            mValueHexValue[100] = mValueHexValue[99];
            mValueSigned[100] = mValueSigned[99];
            mValueGroup[100] = mValueGroup[99];
            mValueHidden[100] = mValueHidden[99];
            mValueHiddenInDisplay[100] = mValueHiddenInDisplay[99];
            mValueUnit[100] = "x65536";
            mValueFaktor[100] = _ValueFaktor[1]; // No alternative unit (taken from MRW_UI_TIME)
            mValueDivisor[100] = _ValueDivisor[1];
            mValueOffset[100] = _ValueOffset[1];
            mValueAltUnit[100] = _ValueAltUnit[1];
            // MRW_ULL_TANKINHALT_MITTEL
            mValueName[101] = _ValueName[89];
            mValueDecimals[101] = _ValueDecimals[89];
            mValueHexValue[101] = _ValueHexValue[89];
            mValueSigned[101] = _ValueSigned[89];
            mValueGroup[101] = _ValueGroup[89];
            mValueHidden[101] = _ValueHidden[89];
            mValueHiddenInDisplay[101] = _ValueHiddenInDisplay[89];
            mValueUnit[101] = _ValueUnit[89];
            mValueFaktor[101] = _ValueFaktor[89];
            mValueDivisor[101] = _ValueDivisor[89];
            mValueOffset[101] = _ValueOffset[89];
            mValueAltUnit[101] = _ValueAltUnit[89];
            // MRW_ULH_TANKINHALT_MITTEL
            mValueName[102] = "do";
            mValueDecimals[102] = mValueDecimals[101];
            mValueHexValue[102] = mValueHexValue[101];
            mValueSigned[102] = mValueSigned[101];
            mValueGroup[102] = mValueGroup[101];
            mValueHidden[102] = mValueHidden[101];
            mValueHiddenInDisplay[102] = mValueHiddenInDisplay[101];
            mValueUnit[102] = "x65536";
            mValueFaktor[102] = _ValueFaktor[1]; // No alternative unit (taken from MRW_UI_TIME)
            mValueDivisor[102] = _ValueDivisor[1];
            mValueOffset[102] = _ValueOffset[1];
            mValueAltUnit[102] = _ValueAltUnit[1];
            // MRW_UI_GEGENDRUCKMITTEL to MRW_UI_TANKMENGE_GESAMT
            for (int i = 103; i < 141; i++)
            {
                mValueName[i] = _ValueName[i - 13];
                mValueDecimals[i] = _ValueDecimals[i - 13];
                mValueHexValue[i] = _ValueHexValue[i - 13];
                mValueSigned[i] = _ValueSigned[i - 13];
                mValueGroup[i] = _ValueGroup[i - 13];
                mValueHidden[i] = _ValueHidden[i - 13];
                mValueHiddenInDisplay[i] = _ValueHiddenInDisplay[i - 13];
                mValueUnit[i] = _ValueUnit[i - 13];
                mValueFaktor[i] = _ValueFaktor[i - 13];
                mValueDivisor[i] = _ValueDivisor[i - 13];
                mValueOffset[i] = _ValueOffset[i - 13];
                mValueAltUnit[i] = _ValueAltUnit[i - 13];
            }
            // MRW_ULH_TANKMENGE_GESAMT
            mValueName[141] = "do";
            mValueDecimals[141] = mValueDecimals[140];
            mValueHexValue[141] = mValueHexValue[140];
            mValueSigned[141] = mValueSigned[140];
            mValueGroup[141] = mValueGroup[140];
            mValueHidden[141] = mValueHidden[140];
            mValueHiddenInDisplay[141] = mValueHiddenInDisplay[140];
            mValueUnit[141] = "x65536";
            mValueFaktor[141] = _ValueFaktor[1]; // No alternative unit (taken from MRW_UI_TIME)
            mValueDivisor[141] = _ValueDivisor[1];
            mValueOffset[141] = _ValueOffset[1];
            mValueAltUnit[141] = _ValueAltUnit[1];
            // MRW_UI_CRTDRUCKMITTEL to  MRW_UI_VIEW_T_WARN
            for (int i = 142; i < 165; i++)
            {
                mValueName[i] = _ValueName[i - 14];
                mValueDecimals[i] = _ValueDecimals[i - 14];
                mValueHexValue[i] = _ValueHexValue[i - 14];
                mValueSigned[i] = _ValueSigned[i - 14];
                mValueGroup[i] = _ValueGroup[i - 14];
                mValueHidden[i] = _ValueHidden[i - 14];
                mValueHiddenInDisplay[i] = _ValueHiddenInDisplay[i - 14];
                mValueUnit[i] = _ValueUnit[i - 14];
                mValueFaktor[i] = _ValueFaktor[i - 14];
                mValueDivisor[i] = _ValueDivisor[i - 14];
                mValueOffset[i] = _ValueOffset[i - 14];
                mValueAltUnit[i] = _ValueAltUnit[i - 14];
            }
            // MRW_UI_ADDITIV_REST
            mValueName[165] = "-";
            mValueDecimals[165] = 0;
            mValueHexValue[165] = false;
            mValueSigned[165] = false;
            mValueGroup[165] = false;
            mValueHidden[165] = true;
            mValueHiddenInDisplay[165] = true;
            mValueUnit[165] = "-";
            mValueFaktor[165] = _ValueFaktor[1]; // No alternative unit (taken from MRW_UI_TIME)
            mValueDivisor[165] = _ValueDivisor[1];
            mValueOffset[165] = _ValueOffset[1];
            mValueAltUnit[165] = _ValueAltUnit[1];

            if (PreCheckSize() > DataSize)
            {
                return ReturnValue.SizeMismatch;
            }
            else
            {
                ConvertBuffer();
                return ReturnValue.NoError;
            }
        }

        /// <summary>Set sign flags. Used if no language available</summary>
        /// <param name="fm">Current firmware object</param>
        public void PresetValueSigns(HJS.ECU.Firmware fm)
        {
            for (int i = 0; i < fm.GetValueNumber(); i++)
            {
                mValueSigned[i] = fm.IsValueSigned(i);
            }
        }

        #region Values
        /// <summary>Get hidden flag for value</summary>
        /// <param name="Position">Position of value in total table</param>
        /// <returns>true if value is hidden, if position is greater than value array, the flag is set to false</returns>
        public bool IsValueHidden(UInt16 Position)
        {
            if (Position < mValueHidden.Length)
            {
                return mValueHidden[Position];
            }
            else
            {
                return false;
            }
        }

        /// <summary>Get display flag for value</summary>
        /// <param name="Position">Position of value in total table</param>
        /// <returns>true if value is displayed, if position is greater than value array, the flag is set to false</returns>
        public bool IsValueDisplayed(UInt16 Position)
        {
            if (Position < mValueHiddenInDisplay.Length)
            {
                return mValueHiddenInDisplay[Position];
            }
            else
            {
                return false;
            }
        }

        /// <summary>Get group flag for value
        /// (0 = measured value, 1=calculated value)</summary>
        /// <param name="Position">Position of value in total table</param>
        /// <returns>False = measured value, true = calculated value</returns>
        public bool IsValueGroup(UInt16 Position)
        {
            if (Position < mValueGroup.Length)
            {
                return mValueGroup[Position];
            }
            else
            {
                return false;
            }
        }
        /// <summary>Convert Value into a string, using format parameter
        /// depending on position in value table</summary>
        /// <param name="Position">Position in value table</param>
        /// <param name="Value">16 bit value to be displayed</param>
        /// <returns>String of value, empty string, if unable to be formated</returns>
        public string GetValue(UInt16 Position, UInt16 Value)
        {
            string ret = "";
            double dValue = 0;
            if (Position < mValueName.Length)
            {
                if (mValueHexValue[Position] == false)
                {
                    // display decimal value 
                    if (mValueSigned[Position] == false)
                    {
                        switch (Value)
                        {
                            case 0xFFFF: ret = "short"; break;
                            case 0xFFFE: ret = "min"; break;
                            case 0xFFFD: ret = "missing"; break;
                            case 0xFFFC: ret = "max"; break;
                            case 0xFFFB: ret = "open"; break;
                            default: ret = ""; break;
                        }
                    }
                    else
                    {
                        switch (Value)
                        {
                            case 0x8004: ret = "short"; break;
                            case 0x8003: ret = "min"; break;
                            case 0x8002: ret = "missing"; break;
                            case 0x8001: ret = "max"; break;
                            case 0x8000: ret = "open"; break;
                            default: ret = ""; break;
                        }
                    }
                    if (String.IsNullOrEmpty(ret))
                    {
                        if (mValueSigned[Position])
                        {
                            dValue = (Int16)Value;
                        }
                        else
                        {
                            dValue = Value;
                        }
                        if (mNoAltUnit == false)
                        {
                            // Umrechnung erlaubt
                            if (mValueFaktor[Position] > 0 && mValueDivisor[Position] > 0)
                            {
                                dValue *= (double)mValueFaktor[Position] / (double)mValueDivisor[Position];
                                Int32 iOffset = 0;
                                if (Int32.TryParse(mValueOffset[Position], out iOffset))
                                {
                                    dValue += iOffset;
                                }
                            }
                            else if (mValueFaktor[Position] > 0)
                            {
                                dValue *= mValueFaktor[Position];
                            }
                        }

                        switch (mValueDecimals[Position])
                        {
                            case 0: ret = String.Format("{0:f0}", dValue); break;
                            case 1: ret = String.Format("{0:f1}", dValue / 10); break;
                            case 2: ret = String.Format("{0:f2}", dValue / 100); break;
                            case 3: ret = String.Format("{0:f3}", dValue / 1000); break;
                            case 4: ret = String.Format("{0:f4}", dValue / 10000); break;
                            case 5: ret = String.Format("{0:f5}", dValue / 100000); break;
                            case 6: ret = String.Format("{0:f6}", dValue / 1000000); break;
                            case 7: ret = String.Format("0x{0:X4}", Value); break;
                            default: ret = String.Format("*{0:f0}", Value); break;
                        }
                    }
                }
                else
                {
                    // display hexadecimal value
                    switch (Value)
                    {
                        case 0xFFFF: ret = "short"; break;
                        case 0xFFFE: ret = "min"; break;
                        case 0xFFFD: ret = "missing"; break;
                        case 0xFFFC: ret = "max"; break;
                        case 0xFFFB: ret = "open"; break;
                        default: ret = String.Format("0x{0:X}", Value); break;
                    }
                }
            }
            return ret;
        }

        /// <summary>Get Value name depending on position in value table</summary>
        /// <param name="Position">Position in value table</param>
        /// <returns>String of value name, empty string on error</returns>
        public string GetValueName(UInt16 Position)
        {
            string ret;

            if (Position < mValueName.Length)
            {
                if (mValueName[Position] == null)
                {
                    ret = String.Format("Value_{0}", Position);
                }
                else
                {
                    ret = mValueName[Position];
                }
            }
            else
            {
                ret = String.Format("Value_{0}", Position);
            }
            return ret;
        }

        /// <summary>Get Value unit depending on position in value table</summary>
        /// <param name="Position">Position in value table</param>
        /// <returns>String of value unit, empty string on error</returns>
        public string GetValueUnit(UInt16 Position)
        {
            string ret;

            if (Position < mValueUnit.Length)
            {
                if (mNoAltUnit == false)
                {
                    // Umrechnung erlaubt
                    if (mValueFaktor[Position] == 0)
                    {
                        ret = mValueUnit[Position];
                    }
                    else
                    {
                        ret = mValueAltUnit[Position];
                    }
                }
                else
                {
                    // Keine Umrechnung
                    ret = mValueUnit[Position];
                }
            }
            else
            {
                ret = "";
            }
            return ret;
        }

        /// <summary>Get Value unit depending on position in value table</summary>
        /// <param name="Position">Position in value table</param>
        /// <param name="altUnit">Flag if alternative unit must be used</param>
        /// <returns>String of value unit, empty string on error</returns>
        public string GetValueUnit(UInt16 Position, bool altUnit)
        {
            string ret;

            if (Position < mValueUnit.Length)
            {
                if (altUnit == false)
                {
                    ret = mValueUnit[Position];
                }
                else
                {
                    ret = mValueAltUnit[Position];
                }
            }
            else
            {
                ret = "";
            }
            return ret;
        }

        /// <summary>Get number of decimal</summary>
        /// <param name="Position">Position of value</param>
        /// <returns>Number of decimal</returns>
        public byte GetValueDecimals(UInt16 Position)
        {
            if (mValueDecimals != null)
            {
                if (Position < mValueDecimals.Length)
                {
                    return mValueDecimals[Position];
                }
                else return 0;
            }
            else return 0;
        }

        /// <summary>Get signed flag for value</summary>
        /// <param name="Position">Position of value in total table</param>
        /// <returns>true if value is signed, if position is greater than value array, the flag is set to false</returns>
        public bool IsValueSigned(UInt16 Position)
        {
            if (Position < mValueSigned.Length)
            {
                return mValueSigned[Position];
            }
            else
            {
                return false;
            }
        }

        /// <summary>Get hex flag for value</summary>
        /// <param name="Position">Position of value in total table</param>
        /// <returns>true if value is hexadecimal, if position is greater than value array, the flag is set to false</returns>
        public bool IsValueHexadecimal(UInt16 Position)
        {
            if (Position < mValueSigned.Length)
            {
                return mValueHexValue[Position];
            }
            else
            {
                return false;
            }
        }

        /// <summary>Get Factor of value</summary>
        /// <param name="Position">Position of value</param>
        /// <returns>Factor of value</returns>
        public byte GetValueFaktor(UInt16 Position)
        {
            if (mValueFaktor != null)
            {
                if (Position < mValueFaktor.Length)
                {
                    return mValueFaktor[Position];
                }
                else return 0;
            }
            else return 0;
        }

        /// <summary>Get Divisor of value</summary>
        /// <param name="Position">Position of value</param>
        /// <returns>Divisor of value</returns>
        public byte GetValueDivisor(UInt16 Position)
        {
            if (mValueDivisor != null)
            {
                if (Position < mValueDivisor.Length)
                {
                    return mValueDivisor[Position];
                }
                else return 0;
            }
            else return 0;
        }

        /// <summary>Get Offset of value</summary>
        /// <param name="Position">Position of value</param>
        /// <returns>Offset of value</returns>
        public string GetValueOffset(UInt16 Position)
        {
            if (mValueOffset != null)
            {
                if (Position < mValueOffset.Length)
                {
                    return mValueOffset[Position];
                }
                else return "";
            }
            else return "";
        }

        /// <summary>Set value name</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="ValueName">Name of value</param>
        /// <returns>True on success</returns>
        public bool SetValueName(UInt16 Position, string ValueName)
        {
            if (mValueName != null)
            {
                if (Position < 256)
                {
                    mValueName[Position] = ValueName;
                    return true;
                }
                else return false;
            }
            else return false;
        }
        
        /// <summary>Set value decimals</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="Decimals">Value Decimals</param>
        /// <returns>True on success</returns>
        public bool SetValueDecimals(UInt16 Position, byte Decimals)
        {
            if (mValueDecimals != null)
            {
                if (Position < 256)
                {
                    mValueDecimals[Position] = Decimals;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value hex flag</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="hexFlag">Hex flag</param>
        /// <returns>True on success</returns>
        public bool SetValueHex(UInt16 Position, bool hexFlag)
        {
            if (mValueHexValue != null)
            {
                if (Position < 256)
                {
                    mValueHexValue[Position] = hexFlag;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value signed flag</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="Signed">signed flag</param>
        /// <returns>True on success</returns>
        public bool SetValueSigned(UInt16 Position, bool Signed)
        {
            if (mValueSigned != null)
            {
                if (Position < 256)
                {
                    mValueSigned[Position] = Signed;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value Group flag</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="Group">Group flag</param>
        /// <returns>True on success</returns>
        public bool SetValueGroup(UInt16 Position, bool Group)
        {
            if (mValueGroup != null)
            {
                if (Position < 256)
                {
                    mValueGroup[Position] = Group;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value Hedden flag</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="Hidden">Hidden flag</param>
        /// <returns>True on success</returns>
        public bool SetValueHidden(UInt16 Position, bool Hidden)
        {
            if (mValueHidden != null)
            {
                if (Position < 256)
                {
                    mValueHidden[Position] = Hidden;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value Display flag</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="HiddenInDisplay">Display flag</param>
        /// <returns>True on success</returns>
        public bool SetValueHiddenInDisplay(UInt16 Position, bool HiddenInDisplay)
        {
            if (mValueHiddenInDisplay != null)
            {
                if (Position < 256)
                {
                    mValueHiddenInDisplay[Position] = HiddenInDisplay;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value unit</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="ValueUnit">Value unit</param>
        /// <returns>True on success</returns>
        public bool SetValueUnit(UInt16 Position, string ValueUnit)
        {
            if (mValueUnit != null)
            {
                if (Position < 256)
                {
                    mValueUnit[Position] = ValueUnit;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value factor</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="A_Factor">Value factor</param>
        /// <returns>True on success</returns>
        public bool SetValueFactor(UInt16 Position, byte A_Factor)
        {
            if (mValueFaktor != null)
            {
                if (Position < 256)
                {
                    mValueFaktor[Position] = A_Factor;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value divisor</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="B_Divisor">Value divisor</param>
        /// <returns>True on success</returns>
        public bool SetValueDivisor(UInt16 Position, byte B_Divisor)
        {
            if (mValueDivisor != null)
            {
                if (Position < 256)
                {
                    mValueDivisor[Position] = B_Divisor;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value offset</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="C_Offset">Value offset</param>
        /// <returns>True on success</returns>
        public bool SetValueOffset(UInt16 Position, string C_Offset)
        {
            if (mValueOffset != null)
            {
                if (Position < 256)
                {
                    mValueOffset[Position] = C_Offset;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set value alternative unit</summary>
        /// <param name="Position">Position of value</param>
        /// <param name="ValueAltunit">Alternative unit</param>
        /// <returns>True on success</returns>
        public bool SetValueAltUnit(UInt16 Position, string ValueAltunit)
        {
            if (mValueAltUnit != null)
            {
                if (Position < 256)
                {
                    mValueAltUnit[Position] = ValueAltunit;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Get number of used values
        /// </summary>
        /// <returns>Number of used values</returns>
        public UInt16 getNumberOfUsedValues()
        {
            UInt16 i = 0, j = 0;
            for (i = 0; i < mValueName.Length; i++)
            {
                if (mValueName[i] != null) j++;
            }
            return j;
        }
        #endregion

        #region Errors
        /// <summary>Get error name depending on position in error table</summary>
        /// <param name="Position">Position in error table</param>
        /// <returns>String of error name, empty string on error</returns>
        public string GetErrorName(UInt16 Position)
        {
            string ret;

            if (Position < mErrorName.Length)
            {
                if (mErrorName[Position] == null)
                {
                    ret = String.Format("Error_{0}", Position);
                }
                else
                {
                    ret = mErrorName[Position];
                }
            }
            else
            {
                ret = String.Format("Error_{0}", Position);
            }
            return ret;
        }

        /// <summary>Get flag if error is only an event</summary>
        /// <param name="Position">Position in error table</param>
        /// <returns>True if event only</returns>
        public bool IsEventOrError(UInt16 Position)
        {
            bool ret;

            if (Position < mErrorEvent.Length)
            {
                ret = mErrorEvent[Position];
            }
            else
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>Get flag if event is shown on display</summary>
        /// <param name="Position">Position in error table</param>
        /// <returns>True if event is shown on display</returns>
        public bool IsEventDisplayed(UInt16 Position)
        {
            bool ret;

            if (Position < mErrorLcdShow.Length)
            {
                ret = mErrorLcdShow[Position];
            }
            else
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>Get flag if event is hidden</summary>
        /// <param name="Position">Position in error table</param>
        /// <returns>True if event is hidden</returns>
        public bool IsEventHidden(UInt16 Position)
        {
            bool ret;

            if (Position < mErrorHidden.Length)
            {
                ret = mErrorHidden[Position];
            }
            else
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>Get flag if event ligts blue led on displays</summary>
        /// <param name="Position">Position in error table</param>
        /// <returns>True if event ligts blue led on displays</returns>
        public bool IsEventBlueLed(UInt16 Position)
        {
            bool ret;

            if (Position < mErrorBlueLed.Length)
            {
                ret = mErrorBlueLed[Position];
            }
            else
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>Set error name</summary>
        /// <param name="Position">Position of error</param>
        /// <param name="ErrorName">Error name</param>
        /// <returns>True on success</returns>
        public bool SetErrorName(UInt16 Position, string ErrorName)
        {
            if (mErrorName != null)
            {
                if (Position < 64)
                {
                    mErrorName[Position] = ErrorName;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set error event flag</summary>
        /// <param name="Position">Position of error</param>
        /// <param name="Event">Event flag</param>
        /// <returns>True on success</returns>
        public bool SetErrorEvent(UInt16 Position, bool Event)
        {
            if (mErrorEvent != null)
            {
                if (Position < 64)
                {
                    mErrorEvent[Position] = Event;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set error </summary>
        /// <param name="Position">Position of error</param>
        /// <param name="Hidden">Error hidden flag</param>
        /// <returns>True on success</returns>
        public bool SetErrorHidden(UInt16 Position, bool Hidden)
        {
            if (mErrorHidden != null)
            {
                if (Position < 64)
                {
                    mErrorHidden[Position] = Hidden;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set error display flag</summary>
        /// <param name="Position">Position of error</param>
        /// <param name="LcdShow">Display flag</param>
        /// <returns>True on success</returns>
        public bool SetErrorLcdShow(UInt16 Position, bool LcdShow)
        {
            if (mErrorLcdShow != null)
            {
                if (Position < 64)
                {
                    mErrorLcdShow[Position] = LcdShow;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>Set error blue led flag</summary>
        /// <param name="Position">Position of error</param>
        /// <param name="BlueLed">Blue led flag</param>
        /// <returns>True on success</returns>
        public bool SetErrorBlue(UInt16 Position, bool BlueLed)
        {
            if (mErrorBlueLed != null)
            {
                if (Position < 64)
                {
                    mErrorBlueLed[Position] = BlueLed;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Get number of used errors
        /// </summary>
        /// <returns>Number of used errord</returns>
        public UInt16 getNumberOfUsedErrors()
        {
            UInt16 i = 0, j = 0;
            for (i = 0; i < mErrorName.Length; i++)
            {
                if (mErrorName[i] != null) j++;
            }
            return j;
        }
        #endregion

        #region Behaves
        /// <summary>Get behave name depending on position</summary>
        /// <param name="Position">Position of behave</param>
        /// <returns>String of error behave, empty string on error</returns>
        public string GetBehaveName(UInt16 Position)
        {
            string ret;

            if (Position < mBehaveName.Length)
            {
                if (mBehaveName[Position] == null)
                {
                    ret = String.Format("Behave_{0}", Position);
                }
                else
                {
                    ret = mBehaveName[Position];
                }
            }
            else
            {
                ret = String.Format("Behave_{0}", Position);
            }
            return ret;
        }

        /// <summary>Set behave name</summary>
        /// <param name="Position">Position of behave</param>
        /// <param name="BehaveName">Behave name</param>
        /// <returns>True on success</returns>
        public bool SetBehaveName(UInt16 Position, string BehaveName)
        {
            if (mBehaveName != null)
            {
                if (Position < 16)
                {
                    mBehaveName[Position] = BehaveName;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Get number of used behaves
        /// </summary>
        /// <returns>Number of used behaves</returns>
        public UInt16 getNumberOfUsedBehaves()
        {
            UInt16 i = 0, j = 0;
            for (i = 0; i < mBehaveName.Length; i++)
            {
                if (mBehaveName[i] != null) j++;
            }
            return j;
        }
        #endregion
    }
}
