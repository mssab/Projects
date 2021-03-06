/*
 * Object: HJS.ECU.Firmware
 * Description: Object for HJS-ECU firmware informations
 * 
 * $LastChangedDate: 2012-08-13 13:39:36 +0200 (Mo, 13 Aug 2012) $
 * $LastChangedRevision: 9 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/trunk/lib_cs_win32_hjsecu/Firmware.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU
{
    /// <summary>
    /// Class for firmware (software of HJS-ECU) relevant data
    /// </summary>
    public class Firmware
    {
        private const byte mRevisionPsoc8 = 164;
        /// <summary>
        /// Compatibility of firmware
        /// </summary>
        private byte mSoftwareRevision;

        /// <summary>
        /// Enumeration of PSoC gain values used for egr and fuel level sensor
        /// </summary>
        public enum PsocGain
        {
            /// <summary>
            /// Gain 0.06
            /// </summary>
            PGA_G0_06 = 0,
            /// <summary>
            /// Gain 16.0
            /// </summary>
            PGA_G16_0 = 8,
            /// <summary>
            /// Gain 48.0
            /// </summary>
            PGA_G48_0 = 12,
            /// <summary>
            /// Gain 0.12
            /// </summary>
            PGA_G0_12 = 16,
            /// <summary>
            /// Gain 8.00
            /// </summary>
            PGA_G8_00 = 24,
            /// <summary>
            /// Gain 24.0
            /// </summary>
            PGA_G24_0 = 28,
            /// <summary>
            /// Gain 0.18
            /// </summary>
            PGA_G0_18 = 32,
            /// <summary>
            /// Gain 5.33
            /// </summary>
            PGA_G5_33 = 40,
            /// <summary>
            /// Gain 0.25
            /// </summary>
            PGA_G0_25 = 48,
            /// <summary>
            /// Gain 4.00
            /// </summary>
            PGA_G4_00 = 56,
            /// <summary>
            /// Gain 0.31
            /// </summary>
            PGA_G0_31 = 64,
            /// <summary>
            /// Gain 3.20
            /// </summary>
            PGA_G3_20 = 72,
            /// <summary>
            /// Gain 0.37
            /// </summary>
            PGA_G0_37 = 80,
            /// <summary>
            /// Gain 2.67
            /// </summary>
            PGA_G2_67 = 88,
            /// <summary>
            /// Gain 0.43
            /// </summary>
            PGA_G0_43 = 96,
            /// <summary>
            /// Gain 2.27
            /// </summary>
            PGA_G2_27 = 104,
            /// <summary>
            /// Gain 0.50
            /// </summary>
            PGA_G0_50 = 112,
            /// <summary>
            /// Gain 2.00
            /// </summary>
            PGA_G2_00 = 120,
            /// <summary>
            /// Gain 0.56
            /// </summary>
            PGA_G0_56 = 128,
            /// <summary>
            /// Gain 1.78
            /// </summary>
            PGA_G1_78 = 136,
            /// <summary>
            /// Gain 0.62
            /// </summary>
            PGA_G0_62 = 144,
            /// <summary>
            /// Gain 1.60
            /// </summary>
            PGA_G1_60 = 152,
            /// <summary>
            /// Gain 0.68
            /// </summary>
            PGA_G0_68 = 160,
            /// <summary>
            /// Gain 1.46
            /// </summary>
            PGA_G1_46 = 168,
            /// <summary>
            /// Gain 0.75
            /// </summary>
            PGA_G0_75 = 176,
            /// <summary>
            /// Gain 1.33
            /// </summary>
            PGA_G1_33 = 184,
            /// <summary>
            /// Gain 0.81
            /// </summary>
            PGA_G0_81 = 192,
            /// <summary>
            /// Gain 1.23
            /// </summary>
            PGA_G1_23 = 200,
            /// <summary>
            /// Gain 0.87
            /// </summary>
            PGA_G0_87 = 208,
            /// <summary>
            /// Gain 1.14
            /// </summary>
            PGA_G1_14 = 216,
            /// <summary>
            /// Gain 0.93
            /// </summary>
            PGA_G0_93 = 224,
            /// <summary>
            /// Gain 1.06
            /// </summary>
            PGA_G1_06 = 232,
            /// <summary>
            /// Gain 1.00
            /// </summary>
            PGA_G1_00 = 248
        }

        /// <summary>
        /// Enumeration of fuel level sensor signal types
        /// </summary>
        public enum TankSignal
        {
            /// <summary>
            /// Input B, resistor, 120 k ohm pullup
            /// </summary>
			TK_NEU_120K = 0x02,
            /// <summary>
            /// Input B, resistor, 1.2 k ohm pullup
            /// </summary>
            TK_NEU_1K2 = 0x52,
            /// <summary>
            /// Input A, voltage
            /// </summary>
			TK_ANALOG = 0x10,
            /// <summary>
            /// Input A, pulsed, double pulse
            /// </summary>
            TK_GEPULST = 0x11,
            /// <summary>
            /// Input A, pulsed, single pulse (since SW 1.x)
            /// </summary>
            TK_GEPULST_V36 = 0x14
        }

        /// <summary>
        /// Accessors of firmware revision
        /// </summary>
        public byte SoftwareRevision
        {
            get { return mSoftwareRevision; }
            set { mSoftwareRevision = value; }
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public Firmware()
        {
            mSoftwareRevision = 8;
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="Revision"></param>
        public Firmware(byte Revision)
        {
            mSoftwareRevision = Revision;
        }

        /// <summary>
        /// Get position of value in table / structure by value identifier
        /// </summary>
        /// <param name="ValueId">Identifier of value</param>
        /// <returns>Position in table, 255 if invalid ID</returns>
        public byte GetValuePosition(byte ValueId)
        {
            byte ret = 255;

            switch (mSoftwareRevision)
            {
                case 8:
                    // Measured values ID 128 .. 207 are in position 0 .. 79
                    if ((ValueId >= 128) && (ValueId <= 207))
                    {
                        ret = (byte)(ValueId - 128);
                    }
                    // Calculated values ID 0 .. 70 are in position 80 .. 150
                    if (ValueId <= 70)
                    {
                        ret = (byte)(ValueId + (207 - 128 + 1));
                    }
                    break;
                case 9:
                    // Measured values ID 128 .. 207 are in position 0 .. 90
                    if ((ValueId >= 128) && (ValueId <= 218))
                    {
                        ret = (byte)(ValueId - 128);
                    }
                    // Calculated values ID 0 .. 75 are in position 91 .. 165
                    if (ValueId <= 70)
                    {
                        ret = (byte)(ValueId + (218 - 128 + 1));
                    }
                    break;
                case 189:
                case 186:
                case 185:
                case 184:
                case 182:
                    // Measured values ID 128 .. 207 are in position 0 .. 72
                    if ((ValueId >= 128) && (ValueId <= 200))
                    {
                        ret = (byte)(ValueId - 128);
                    }
                    // Calculated values ID 0 .. 59 are in position 73 .. 131
                    if (ValueId <= 70)
                    {
                        ret = (byte)(ValueId + (200 - 128 + 1));
                    }
                    break;
                case 174:
                case 172:
                case 167:
                    // Measured values ID 128 .. 207 are in position 0 .. 71
                    if ((ValueId >= 128) && (ValueId <= 199))
                    {
                        ret = (byte)(ValueId - 128);
                    }
                    // Calculated values ID 0 .. 58 are in position 70 .. x
                    if (ValueId <= 70)
                    {
                        ret = (byte)(ValueId + (199 - 128 + 1));
                    }
                    break;
                case 164:
                    // Measured values ID 128 .. 207 are in position 0 .. 71
                    if ((ValueId >= 128) && (ValueId <= 199))
                    {
                        ret = (byte)(ValueId - 128);
                    }
                    // Calculated values ID 0 .. 53 are in position 70 .. x
                    if (ValueId <= 70)
                    {
                        ret = (byte)(ValueId + (199 - 128 + 1));
                    }
                    break;
                default:
                    ret = 255;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Get number of actual values
        /// </summary>
        /// <returns>Number of actual values</returns>
        public byte GetValueNumber()
        {
            byte ret = 0;
            switch (mSoftwareRevision)
            {
                case 8:
                    ret = 151;
                    break;
                case 9:
                    ret = 91 + 75;
                    break;
                case 189:
                case 186:
                case 185:
                case 184:
                case 182:
                    ret = 73 + 59;
                    break;
                case 174:
                case 172:
                case 167:
                    ret = 72 + 58;
                    break;
                case 164:
                    ret = 72 + 53;
                    break;
                default:
                    ret = 0;    // hier einmal auslesen?
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Check if current software version is updateable
        /// If PSoC version 8 is on ECU, the ECU can be updated
        /// </summary>
        /// <param name="Version">Version string to be updated to</param>
        /// <returns>True if software is updatebale</returns>
        public bool IsUpdateableTo(string Version)
        {
            string[] v = System.Text.RegularExpressions.Regex.Split(Version, @"\D+");
            bool ret = true;
            if (v.Length == 3)
            {
                UInt16 MainVersion;
                byte Revision;
                try
                {
                    MainVersion = Convert.ToUInt16(v[0]);
                    Revision = Convert.ToByte(v[2]);
                }
                catch
                {
                    // keine Umwandlung in zahlen moeglich
                    Revision = 0;
                    MainVersion = 0;
                    ret = false;
                }
                if (ret)
                {
                    if (Revision == 0)
                    {
                        // Keine Version aufgespielt
                        ret = false;
                    }
                    else
                    {
                        if (MainVersion == 0)
                        {
                            // Neue Revision hoher der auf ECU
                            if ((Revision > mSoftwareRevision)
                                // Beide Revisionen fuer PSoC_V8
                                || ((mSoftwareRevision > mRevisionPsoc8) && (Revision > mRevisionPsoc8)))
                            {
                                ret = true;
                            }
                            else
                            {
                                ret = false;
                            }
                        }
                        else
                        {
                            //Mainversion kann nur 1 oder hoeher sein
                            ret = true; 
                        }
                    }
                }
            }
            else
            {
                // keine drei punkte im string, oder nicht zu parsen
                ret = false;
            }
            return ret;
        }

        //rtc: ab 1.7.8, aenderung in 1.x.9
    }
}
