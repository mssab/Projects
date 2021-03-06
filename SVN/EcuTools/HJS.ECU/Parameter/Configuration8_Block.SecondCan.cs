/*
 * Object: HJS.ECU.Parameter.Configuration8_Block
 * Description: Configuration parameter block compatibility 8 second can
 * 
 * $LastChangedDate: 2013-12-02 10:31:16 +0100 (Mo, 02 Dez 2013) $
 * $LastChangedRevision: 26 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Parameter/Configuration8_Block.SecondCan.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU.Parameter
{
    public partial class Configuration8_Block
    {
        /// <summary>Import second can from base block</summary>
        public override void SecondCanImport()
        {
        }

        /// <summary>Number of second can items</summary>
        /// <returns>Number of second can items</returns>
        public override int SecondCanGetNumber()
        {
            return 0;
        }

        /// <summary>Get name of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Name of second can item</returns>
        public override string SecondCanGetItemName(int position)
        {
            return "N/A";
        }

        /// <summary>Get value of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of second can item as double</returns>
        public override double SecondCanGetItemValue(int position)
        {
            return 0;
        }

        /// <summary>Set value of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <param name="NewValue">New Value to be set</param>
        /// <returns>True on success</returns>
        public override bool SecondCanSetItemValue(int position, double NewValue)
        {
            return false;
        }

        /// <summary>Get value string of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Value of second can item as string</returns>
        public override string SecondCanGetItemValueString(int position)
        {
            return "N/A";
        }

        /// <summary>Get data type of second can item</summary>
        /// <param name="position">Position of item</param>
        /// <returns>Data type of second can item</returns>
        public override TaskDataType SecondCanGetItemType(int position)
        {
            return TaskDataType.type_uint_8;
        }
    }
}