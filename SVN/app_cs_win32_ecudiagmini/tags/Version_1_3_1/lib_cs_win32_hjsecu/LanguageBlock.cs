/*
 * Object: HJS.ECU.LanguageBlock
 * Description: Block of language texts
 * 
 * $LastChangedDate: 2013-05-16 10:28:12 +0200 (Do, 16 Mai 2013) $
 * $LastChangedRevision: 21 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/tags/Version_1_3_1/lib_cs_win32_hjsecu/LanguageBlock.cs $
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
    /// <summary>
    /// Class for language blocks
    /// </summary>
    public class LanguageBlock : Block
    {
        #region Private Members

        /// <summary>
        /// Name of actual value
        /// </summary>
        private string[] mValueName;

        /// <summary>
        /// Number of Decimals of actual value
        /// </summary>
        private byte[] mValueDecimals;

        /// <summary>
        /// Actual value is hexadecimal
        /// </summary>
        private bool[] mValueHexValue;

        /// <summary>
        /// Actual value is signed
        /// </summary>
        private bool[] mValueSigned;

        /// <summary>
        /// Actual value is in group of calculated values
        /// </summary>
        private bool[] mValueGroup;

        /// <summary>
        /// Actual value is hidden
        /// </summary>
        private bool[] mValueHidden;

        /// <summary>
        /// Actual value is hidden in displays
        /// </summary>
        private bool[] mValueHiddenInDisplay;

        /// <summary>
        /// Unit of actual value
        /// </summary>
        private string[] mValueUnit;

        /// <summary>
        /// Factor for alternative Unit (formally known as A)
        /// </summary>
        private byte[] mValueFaktor;

        /// <summary>
        /// Divisor for alternative Unit (formally known as B)
        /// </summary>
        private byte[] mValueDivisor;

        /// <summary>
        /// Offset for alternative Unit (formally known as C)
        /// </summary>
        private string[] mValueOffset;

        /// <summary>
        /// Alternative unit of actual value
        /// </summary>
        private string[] mValueAltUnit;

        /// <summary>
        /// Name of error event
        /// </summary>
        private string[] mErrorName;

        /// <summary>
        /// Flag if error is event
        /// </summary>
        private bool[] mErrorEvent;

        /// <summary>
        /// Flag if error is hidden
        /// </summary>
        private bool[] mErrorHidden;

        /// <summary>
        /// Flag if error is shown on displays
        /// </summary>
        private bool[] mErrorLcdShow;

        /// <summary>
        /// Flag if error is blue led
        /// </summary>
        private bool[] mErrorBlueLed;

        /// <summary>
        /// Name of behave
        /// </summary>
        private string[] mBehaveName;

        /// <summary>
        /// Flag if calculation into alternative unit is denied
        /// </summary>
        private bool mNoAltUnit;

        /// <summary>
        /// Array size of Values
        /// </summary>
        private const int MAX_NUMBER_OF_VALUES = 256;

        /// <summary>
        /// Array size of errors
        /// </summary>
        private const int MAX_NUMBER_OF_ERRORS = 64;

        /// <summary>
        /// Array size of behaves
        /// </summary>
        private const int MAX_NUMBER_OF_BEHAVES = 16;

        /// <summary>
        /// Enumeration of language groups
        /// </summary>
        private enum Group : int
        {
            Values = 0,
            Errors = 1,
            Behaves = 2,
            Max = 3
        }

        /// <summary>
        /// Delimiter for columns (null termination)
        /// </summary>
        private const byte DELIMITER_COLUMN = 0;

        /// <summary>
        /// Delimiter for rows (\N)
        /// </summary>
        private const byte DELIMITER_ROW = 10;

        /// <summary>
        /// Delimiter for groups (\R)
        /// </summary>
        private const byte DELIMITER_GROUP = 13;

        /// <summary>
        /// Offset in byte array for value factor and value divisor
        /// </summary>
        private const byte FACTOR_OFFSET = 13;
        
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public LanguageBlock()
        {
            Type = BlockId.IdLngEN;
            Version = 1;
            DataSize = 4090;// 4096 - 6;

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

        /// <summary>
        /// Flag if calculation into alternative unit is denied
        /// </summary>
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

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="language"></param>
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
                case BlockId.IdLngPO:
                    Type = BlockId.IdLngPO;
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

        /// <summary>
        /// Read language block from plain data (raw array from old protocol i.e. 14)
        /// </summary>
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

        /// <summary>
        /// Read language information (block) from byte array
        /// </summary>
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
                            if (GroupInBlock == Group.Errors) { GroupInBlock = Group.Behaves; }
                            if (GroupInBlock == Group.Values) { GroupInBlock = Group.Errors; }
                            RowInGroup = 0;
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

        private void adjustCodepage()
        {
            // codepage anpassen
            if (Type == BlockId.IdLngPO)
            {
                Encoding cpEN = Encoding.GetEncoding(28591); // Westeuropäisch (iso-8859-1)
                Encoding cpPL = Encoding.GetEncoding(28592); // Mitteleuropäisch (iso-8859-2)
                byte[] cur_strg;
                for (int i = 0; i < mValueName.Length; i++)
                {
                    cur_strg = cpEN.GetBytes(mValueName[i]);
                    mValueName[i] = cpPL.GetString(cur_strg);
                }
                for (int i = 0; i < mValueUnit.Length; i++)
                {
                    cur_strg = cpEN.GetBytes(mValueUnit[i]);
                    mValueUnit[i] = cpPL.GetString(cur_strg);
                }
                for (int i = 0; i < mValueAltUnit.Length; i++)
                {
                    cur_strg = cpEN.GetBytes(mValueAltUnit[i]);
                    mValueAltUnit[i] = cpPL.GetString(cur_strg);
                }
                for (int i = 0; i < mBehaveName.Length; i++)
                {
                    cur_strg = cpEN.GetBytes(mBehaveName[i]);
                    mBehaveName[i] = cpPL.GetString(cur_strg);
                }
                for (int i = 0; i < mErrorName.Length; i++)
                {
                    cur_strg = cpEN.GetBytes(mErrorName[i]);
                    mErrorName[i] = cpPL.GetString(cur_strg);
                }
            }
            //else if(Type == BlockId.IdLngTK)
            //{
            //    Encoding cpEN = Encoding.GetEncoding(28591); // Westeuropäisch (iso-8859-1)
            //    Encoding cpTK = Encoding.GetEncoding(28599); // Türkisch (iso-8859-9)
            //    byte[] cur_strg;
            //    for (int i = 0; i < mValueName.Length; i++)
            //    {
            //        cur_strg = cpEN.GetBytes(mValueName[i]);
            //        mValueName[i] = cpTK.GetString(cur_strg);
            //    }
            //    for (int i = 0; i < mValueUnit.Length; i++)
            //    {
            //        cur_strg = cpEN.GetBytes(mValueUnit[i]);
            //        mValueUnit[i] = cpTK.GetString(cur_strg);
            //    }
            //    for (int i = 0; i < mValueAltUnit.Length; i++)
            //    {
            //        cur_strg = cpEN.GetBytes(mValueAltUnit[i]);
            //        mValueAltUnit[i] = cpTK.GetString(cur_strg);
            //    }
            //    for (int i = 0; i < mBehaveName.Length; i++)
            //    {
            //        cur_strg = cpEN.GetBytes(mBehaveName[i]);
            //        mBehaveName[i] = cpTK.GetString(cur_strg);
            //    }
            //    for (int i = 0; i < mErrorName.Length; i++)
            //    {
            //        cur_strg = cpEN.GetBytes(mErrorName[i]);
            //        mErrorName[i] = cpTK.GetString(cur_strg);
            //    }
            //}

            //  (28597 = Griechisch(iso-8859-7) )
        }


        /// <summary>
        /// Get hidden flag for value
        /// </summary>
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

        /// <summary>
        /// Get display flag for value
        /// </summary>
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

        /// <summary>
        /// Convert Value into a string, using format parameter depending on position in value table
        /// </summary>
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
                    if (ret == "")
                    {
                        // Vorzeichen
                        if (mValueSigned[Position] != false)
                        {
                            dValue = (Int16)Value;
                        }
                        else
                        {
                            dValue = Value;
                        }
                        // Umrechnung in alternative Einheit
                        if (mNoAltUnit == false)
                        {
                            if (mValueFaktor[Position] > 0 && mValueDivisor[Position] > 0)
                            {
                                // Faktor und Divisor
                                dValue *= (double)mValueFaktor[Position] / (double)mValueDivisor[Position];
                                UInt16 uiOffset = 0;
                                if (UInt16.TryParse(mValueOffset[Position], out uiOffset))
                                {
                                    dValue += uiOffset;
                                }
                            }
                            else if (mValueFaktor[Position] > 0)
                            {
                                // nur Faktor
                                dValue *= mValueFaktor[Position];
                            }
                        }
                        // Nachkommastellen
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

        /// <summary>
        /// Get Value name depending on position in value table
        /// </summary>
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

        /// <summary>
        /// Get Value unit depending on position in value table
        /// </summary>
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

        //

        /// <summary>
        /// Get error name depending on position in error table
        /// </summary>
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

        /// <summary>
        /// Get flag if error is only an event
        /// </summary>
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

        /// <summary>
        /// Get behave name depending on position
        /// </summary>
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
    }
}

/*
bool CLanguageBlock::IsErrorHidden(uint8_t ucPosition)
{
	if(Events[ucPosition].Hidden != 0){
		return true;
	}else{
		return false;
	}
}

bool CLanguageBlock::IsErrorEvent(uint8_t ucPosition)
{
	if(Events[ucPosition].Event != 0){
		return true;
	}else{
		return false;
	}
}

CString CLanguageBlock::GetErrorName(uint8_t ucPosition)
{
	return Events[ucPosition].strName;
}

uint8_t CLanguageBlock::GetErrorEvent(uint8_t ucPosition)
{
	return Events[ucPosition].Event;
}

uint8_t CLanguageBlock::GetErrorHidden(uint8_t ucPosition)
{
	return Events[ucPosition].Hidden;
}

uint16_t CLanguageBlock::GetByteUsage(void)
{
	return LastByte;
}

CString CLanguageBlock::GetBehaveName(uint8_t ucPosition)
{
	return Behaves[ucPosition].strName;
}

////

uint8_t CLanguageBlock::SetValueName(uint8_t ucPosition, CString strValueName)
{
	if(ucPosition < 256){
		Values[ucPosition].strName = strValueName;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueDecimals(uint8_t ucPosition, uint8_t ucDecimals)
{
	if(ucPosition < 256){
		Values[ucPosition].Decimals = ucDecimals;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueHexValue(uint8_t ucPosition, uint8_t ucHexValue)
{
	if(ucPosition < 256){
		Values[ucPosition].HexValue = ucHexValue;
		return 0U;
	}else{
		return 1U;
	};
}

uint8_t CLanguageBlock::SetValueSigned(uint8_t ucPosition, uint8_t ucSigned)
{
	if(ucPosition < 256){
		Values[ucPosition].Signed =ucSigned ;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueGroup(uint8_t ucPosition, uint8_t ucGroup)
{
	if(ucPosition < 256){
		Values[ucPosition].Group = ucGroup;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueHidden(uint8_t ucPosition, uint8_t ucHidden)
{
	if(ucPosition < 256){
		Values[ucPosition].Hidden = ucHidden;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueHiddenInDisplay(uint8_t ucPosition, uint8_t ucHiddenInDisplay)
{
	if(ucPosition < 256){
		Values[ucPosition].HiddenInDisplay = ucHiddenInDisplay;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueUnit(uint8_t ucPosition, CString strValueUnit)
{
	if(ucPosition < 256){
		Values[ucPosition].strUnit = strValueUnit;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueFactor(uint8_t ucPosition, uint8_t ucA_Factor)
{
	if(ucPosition < 256){
		Values[ucPosition].ucFaktor = ucA_Factor;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueDivisor(uint8_t ucPosition, uint8_t ucB_Divisor)
{
	if(ucPosition < 256){
		Values[ucPosition].ucDivisor = ucB_Divisor;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueOffset(uint8_t ucPosition, uint16_t uiC_Offset)
{
	if(ucPosition < 256){
		Values[ucPosition].uiOffset = uiC_Offset;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetValueAltUnit(uint8_t ucPosition, CString strValueAltunit)
{
	if(ucPosition < 256){
		Values[ucPosition].strAltUnit = strValueAltunit;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetErrorName(uint8_t ucPosition, CString strErrorName)
{
	if(ucPosition < 64){
		Events[ucPosition].strName = strErrorName;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetErrorEvent(uint8_t ucPosition, uint8_t ucEvent)
{
	if(ucPosition < 64){
		Events[ucPosition].Event = ucEvent;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetErrorHidden(uint8_t ucPosition, uint8_t ucHidden)
{
	if(ucPosition < 64){
		Events[ucPosition].Hidden = ucHidden;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetErrorLcdShow(uint8_t ucPosition, uint8_t ucLcdShow)
{
	if(ucPosition < 64){
		Events[ucPosition].LcdShow = ucLcdShow;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetErrorBlue(uint8_t ucPosition, uint8_t ucBlueLed)
{
	if(ucPosition < 64){
		Events[ucPosition].BlueLed = ucBlueLed;
		return 0U;
	}else{
		return 1U;
	}
}

uint8_t CLanguageBlock::SetBehaveName(uint8_t ucPosition, CString strBehaveName)
{
	if(ucPosition < 16){
		Behaves[ucPosition].strName = strBehaveName;
		return 0U;
	}else{
		return 1U;
	}
}

uint16_t CLanguageBlock::PreCheckSize(void)
{
	uint16_t i = 0;
	uint16_t uiSize = static_cast<uint16_t>(sizeof(CBlock::BLOCK_Header_t));
uint8_t* pucDebug;

	for(i = 0; i < NumberOfValues; i++){
		uiSize += Values[i].strName.GetLength();
		uiSize++;	// Nullterminierung
		uiSize++;	// Formatbyte
		uiSize++;	// Nullterminierung
		uiSize += Values[i].strUnit.GetLength();
		uiSize++;	// Nullterminierung
		if(Values[i].ucFaktor != 0){
pucDebug = &ucaBuffer[uiSize];
			uiSize++;
			if(Values[i].ucDivisor != 0){
pucDebug = &ucaBuffer[uiSize];
				uiSize++;
				if(Values[i].uiOffset != 0){
pucDebug = &ucaBuffer[uiSize];
					CString x;
					x.Format("%x", Values[i].uiOffset);
					uiSize += x.GetLength();
				}
			}
			uiSize++;	// Nullterminierung
pucDebug = &ucaBuffer[uiSize];
			uiSize += Values[i].strAltUnit.GetLength();
			uiSize++;	// Nullterminierung
		}
		uiSize++;	// Zeilenendezeichen
	}
	uiSize++;	// Blockendezeichen
	for(i = 0; i < 64; i++){
		uiSize += Events[i].strName.GetLength();
		uiSize++;	// Nullterminierung
		uiSize++;	// Formatbyte
		uiSize++;	// Nullterminierung
		uiSize++;	// Zeilenendezeichen
	}
	uiSize++;	// Blockendezeichen
	for(i = 0; i < 16; i++){
		uiSize += Behaves[i].strName.GetLength();
		uiSize++;	// Nullterminierung
		uiSize++;	// Zeilenendezeichen
	}
	uiSize++;	// Blockendezeichen

	return uiSize;
}

void CLanguageBlock::ConvertBuffer(void)
{
	uint16_t i = 0;
	uint16_t pos = 0;
	uint16_t delta = 0;
	uint8_t ucFormat = 0;

	memset(pBlockData, 0xFF, Header.uiSize);

	// Values
	for(i = 0; i < NumberOfValues; i++){
		// Value Name
		strcpy_s((char *)(&ucaBuffer[pos]), (Header.uiSize - pos), Values[i].strName);
		delta = (uint16_t)strlen((char *)(&ucaBuffer[pos]));
		pos = pos + delta + 1;	// 1 wg nullterminierung

		// Value format
		ucFormat = 0x40;
		if(Values[i].HexValue != 0){
			ucFormat |= 0x07;
		}else{
			ucFormat |= (Values[i].Decimals & 0x07);
		}
		if(Values[i].Signed != 0) {ucFormat |= 0x08;};
		if(Values[i].Group != 0) {ucFormat |= 0x10;};
		if(Values[i].Hidden != 0) {ucFormat |= 0x20;};
		if(Values[i].HiddenInDisplay != 0) {ucFormat |= 0x80;};
		ucaBuffer[pos] = ucFormat;
		ucaBuffer[pos + 1] = 0x00;
		pos = pos + 2;

		// Value unit
		strcpy_s((char *)(&ucaBuffer[pos]), (Header.uiSize - pos), Values[i].strUnit);
		delta = (uint16_t)strlen((char *)(&ucaBuffer[pos]));
		pos = pos + delta + 1;	// 1 wg nullterminierung

		if(Values[i].ucFaktor != 0){
			//a schreiben
			ucaBuffer[pos] = Values[i].ucFaktor + 13;
			pos = pos + 1;
			if(Values[i].ucDivisor != 0){
				//b schreiben
				ucaBuffer[pos] = Values[i].ucDivisor + 13;
				pos = pos + 1;
				if(Values[i].uiOffset != 0){
				//c schreiben
					CString x;
					x.Format("%x", Values[i].uiOffset);
					memcpy(&ucaBuffer[pos], x.GetBuffer(), x.GetLength());
					pos += x.GetLength();
				}
			}
			ucaBuffer[pos] = 0x00;
			pos = pos + 1;
			//Alt unit
			strcpy_s((char *)(&ucaBuffer[pos]), (Header.uiSize - pos), Values[i].strAltUnit);
			delta = (uint16_t)strlen((char *)(&ucaBuffer[pos]));
			pos = pos + delta + 1;	// 1 wg nullterminierung
		}

		// Zeilenende
		ucaBuffer[pos] = '\n';
		pos = pos + 1;
	}

	// Blockende
	ucaBuffer[pos] = '\r';
	pos = pos + 1;

	// Events, Errors
	for(i = 0; i < 64; i++){
		// Event Name
		strcpy_s((char *)(&ucaBuffer[pos]), (Header.uiSize - pos), Events[i].strName);
		delta = (uint16_t)strlen((char *)(&ucaBuffer[pos]));
		pos = pos + delta + 1;	// 1 wg nullterminierung

		// Event format
		ucFormat = 0x40;
		if(Events[i].Hidden != 0) {ucFormat |= 0x01;};
		if(Events[i].Event != 0) {ucFormat |= 0x02;};
		if(Events[i].LcdShow != 0) {ucFormat |= 0x04;};
		if(Events[i].BlueLed != 0) {ucFormat |= 0x10;};
		ucaBuffer[pos] = ucFormat;
		ucaBuffer[pos + 1] = 0x00;
		pos = pos + 2;

		// Zeilenende
		ucaBuffer[pos] = '\n';
		pos = pos + 1;
	}

	// Blockende
	ucaBuffer[pos] = '\r';
	pos = pos + 1;

	// Behaves
	for(i = 0; i < 16; i++){
		// Behave Name
		strcpy_s((char *)(&ucaBuffer[pos]), (Header.uiSize - pos), Behaves[i].strName);
		delta = (uint16_t)strlen((char *)(&ucaBuffer[pos]));
		pos = pos + delta + 1;	// 1 wg nullterminierung

		// Zeilenende
		ucaBuffer[pos] = '\n';
		pos = pos + 1;
	}

	// Blockende
	ucaBuffer[pos] = '\r';
	pos = pos + 1;

	memset(&ucaBuffer[pos], 0xFF, Header.uiSize - pos);

	// Crc
	Header.uiCrc16 = CRC_GenerateW(ucaBuffer, Header.uiSize, 0x0000);
}
 */