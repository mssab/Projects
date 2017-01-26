/*
 * Object: HJS.ECU.InternalDataItem
 * Description: Object for HJS-ECU internal data list irtem
 * 
 * $LastChangedDate: 2013-12-02 10:31:16 +0100 (Mo, 02 Dez 2013) $
 * $LastChangedRevision: 26 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/InternalDataItem.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU
{
    /// <summary>Interal data list item</summary>
    public class InternalDataItem
    {
        /// <summary>Name of internal data item</summary>
        public string Name { get; set; }

        /// <summary>String of value of internal data item</summary>
        public string ValueString { get; set; }

        /// <summary>String of group name of internal data item</summary>
        public string Group { get; set; }
    }
}
