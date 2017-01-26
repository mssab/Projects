/*
 * Object: HJS.ECU.Parameter.Configuration10_Block
 * Description: Configuration parameter block compatibility 10 second can
 * 
 * $LastChangedDate: 2015-03-12 16:55:48 +0100 (Do, 12 Mrz 2015) $
 * $LastChangedRevision: 101 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/Configuration10_Block.SecondCan.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU.Parameter
{
    public partial class Configuration10_Block
    {
        private TaskConfigurationItem[] SecondCanItem;
        private const int SecondCanByteOffset = 495;

        /// <summary>Import second can from base block</summary>
        public override void SecondCanImport()
        {
            UInt16 ItemPosition = 0;
            UInt16 DataPosition = SecondCanByteOffset;

            Array.Resize(ref SecondCanItem, 2+(8*14) );
            SecondCanItem[ItemPosition] = new TaskConfigurationItem("Can2.uiTime", TaskDataType.type_uint_16, DataPosition);
            DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

            SecondCanItem[ItemPosition] = new TaskConfigurationItem("Can2.ucDefBaudrate", TaskDataType.type_can_baudrate_8, DataPosition);
            DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

            for (int i = 0; i < 8; i++)
            {

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].uiSendrate", i), TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].ucDefDirection", i), TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].ulIdentifier", i), TaskDataType.type_can_id_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].ucDLC", i), TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].ucDefExtStd", i), TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].uiP", i), TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].ucDefMeasureValue", i), TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].ucDefIntelMotorola", i), TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].ucStartbit", i), TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].uiBitlength", i), TaskDataType.type_uint_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].fFaktor", i), TaskDataType.type_float_32, DataPosition);
                DataPosition = (UInt16)(DataPosition + 4); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].siOffset", i), TaskDataType.type_int_16, DataPosition);
                DataPosition = (UInt16)(DataPosition + 2); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].ucValueSigned", i), TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;

                SecondCanItem[ItemPosition] = new TaskConfigurationItem(String.Format("Can2.Msg[{0}].ucDefTrigger", i), TaskDataType.type_uint_8, DataPosition);
                DataPosition = (UInt16)(DataPosition + 1); ItemPosition++;
            }
        }

        /// <summary>Number of second can items</summary>
        /// <returns>Number of second can items</returns>
        public override int SecondCanGetNumber()
        {
            return SecondCanItem.Length;
        }

        /// <summary>Get name of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Name of second can item</returns>
        public override string SecondCanGetItemName(int position)
        {
            if (SecondCanItem != null)
            {
                if (position < SecondCanItem.Length)
                {
                    return SecondCanItem[position].Name;

                }
                else
                {
                    return "N/A";
                }
            }
            else
            {
                return "N/A";
            }
        }

        /// <summary>Get value of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of second can item as double</returns>
        public override double SecondCanGetItemValue(int position)
        {
            if (InitValueItem != null)
            {
                if (position < SecondCanItem.Length)
                {
                    return SecondCanItem[position].GetValue(ref mBlockData);

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>Set value of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <param name="NewValue">New Value to be set</param>
        /// <returns>True on success</returns>
        public override bool SecondCanSetItemValue(int position, double NewValue)
        {
            if (SecondCanItem != null)
            {
                if (position < SecondCanItem.Length)
                {
                    return SecondCanItem[position].SetValue(ref mBlockData, NewValue);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>Get value string of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of second can item as string</returns>
        public override string SecondCanGetItemValueString(int position)
        {
            if (SecondCanItem != null)
            {
                if (position < SecondCanItem.Length)
                {
                    return SecondCanItem[position].GetValueString(ref mBlockData, Version);

                }
                else
                {
                    return "N/A";
                }
            }
            else
            {
                return "N/A";
            }
        }

        /// <summary>Get data type of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Data type of second can item</returns>
        public override TaskDataType SecondCanGetItemType(int position)
        {
            if (SecondCanItem != null)
            {
                if (position < SecondCanItem.Length)
                {
                    return SecondCanItem[position].DataType;

                }
                else
                {
                    return TaskDataType.type_uint_8;
                }
            }
            else
            {
                return TaskDataType.type_uint_8;
            }
        }
    }
}