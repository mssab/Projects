/*
 * Object: HJS.ReportBlock
 * Description: Block of report of construction kit application
 * 
 * $LastChangedDate: 2014-03-07 10:44:08 +0100 (Fr, 07 Mrz 2014) $
 * $LastChangedRevision: 42 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/ReportBlock.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Xml;

namespace HJS
{
    /// <summary>Block of report of construction kit application</summary>
    public class ReportBlock : Block
    {
        private string[] mItemNames;
        private string[] mItemValues;

        /// <summary>Default constructor</summary>
        public ReportBlock()
        {
            Type = BlockId.IdReport;
            Version = 1;
            DataSize = 4090;

            mItemNames = new string[0];
            mItemValues = new string[0];
        }

        /// <summary>Overloaded constructor</summary>
        /// <param name="b">Reference to author block in base class type</param>
        public ReportBlock(Block b)
        {
            Type = BlockId.IdReport;
            Version = 1;
            DataSize = 40;

            mItemNames = new string[0];
            mItemValues = new string[0];

            if (b.Type == BlockId.IdReport)
            {
                b.GetData(out mBlockData);
                Parse();
            }
        }

        private void Parse()
        {
            String x = System.Text.Encoding.Default.GetString(mBlockData);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(x);
            XmlNodeList xNodes = xDoc.GetElementsByTagName("report");
            XmlNode xRoot = xNodes[0];
            foreach (XmlNode xChild in xRoot.ChildNodes)
            {
                Array.Resize<string>(ref mItemNames, mItemNames.Length + 1);
                mItemNames[mItemNames.Length - 1] = xChild.Name;
                Array.Resize<string>(ref mItemValues, mItemValues.Length + 1);
                mItemValues[mItemValues.Length - 1] = xChild.InnerText;
            }
        }

        /// <summary>Get number of report items</summary>
        /// <returns>Number of report items</returns>
        public int GetItemNumber()
        {
            return mItemValues.Length;
        }

        /// <summary>Get name of report item</summary>
        /// <param name="Position">Position of report item</param>
        /// <returns>Name of report item</returns>
        public string GetItemName(int Position)
        {
            return mItemNames[Position];
        }

        /// <summary>Get value of report item</summary>
        /// <param name="Position">Position of report item</param>
        /// <returns>Value of report item</returns>
        public string GetItemValue(int Position)
        {
            return mItemValues[Position];
        }
    }
}
