/*
 * Object: HJS.ECU.Firmware
 * Description: Object for HJS-ECU firmware informations
 * 
 * $LastChangedDate: 2015-03-05 13:27:15 +0100 (Do, 05 Mrz 2015) $
 * $LastChangedRevision: 100 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/Firmware.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;

namespace HJS.ECU
{
    /// <summary>Enumeration for task identifier</summary>
    public enum TaskIdentifier
    {
        /// <summary>Pre diagnose task</summary>
        taskPreDiagnose = 0,
        /// <summary>Post diagnose task</summary>
        taskPostDiagnose = 1,
        /// <summary>Tankgeber task</summary>
        taskTankgeber = 2,
        /// <summary>Kommunication task</summary>
        taskKommunikation = 3,
        /// <summary>I/O task</summary>
        taskIo = 4,
        /// <summary>Dosieren task</summary>
        taskDosieren = 5,
        /// <summary>Additivieren task</summary>
        taskAdditivierung = 6,
        /// <summary>VERT watvch task</summary>
        taskVertWatch = 7,
        /// <summary>Beladung prozentual task</summary>
        taskBeladPro = 8,
        /// <summary>CAN I/O task</summary>
        taskCanIn = 9,
        /// <summary>Beladung mittelung task</summary>
        taskBeladMittel = 10,
        /// <summary>Acquisition task</summary>
        taskAcquisition = 11,
        /// <summary>Turnspeed task</summary>
        taskTurnspeed = 12,
        /// <summary>Beladung Kennfeld task</summary>
        taskBeladKennfeld = 13,
        /// <summary>Regenerieren task</summary>
        taskRegenerieren = 14,
        /// <summary>Heizen task</summary>
        taskHeizen = 15,
        /// <summary>AGR task</summary>
        taskAgr = 16,
        /// <summary>Drive pattern detection task</summary>
        taskDrivePattern = 17,
        /// <summary>Beladung Luftmasse task</summary>
        taskBeladLuftmasse = 18,
        /// <summary>Beladung CRT task</summary>
        taskBeladCRT = 19,
        /// <summary>Can communication task</summary>
        taskCanCom = 20,
        /// <summary>ICDosing task</summary>
        taskIcDosing = 21,
        /// <summary>J1939 session layer task</summary>
        taskSaeComm = 22,
        /// <summary>J1939 application layer task</summary>
        taskAplSae = 23,
        /// <summary>Mass air flow task</summary>
        taskMassAirFlow = 24,
        /// <summary>Grundfos urea pump task</summary>
        taskGrundfos = 25,
        /// <summary>Back pressure task</summary>
        taskStaudruck = 26,
        /// <summary>SCR heater task</summary>
        taskScrHeiz = 27,
        /// <summary>Can 2 com task</summary>
        taskCAN2Com = 28,
        /// <summary>Self regeneration detection task</summary>
        taskSelfRegeneration = 29,
        /// <summary>XCP task</summary>
        taskXCP = 30,
        /// <summary>Invalid task identifier</summary>
        taskInvalid = 255
    }

    /// <summary>Class for firmware (software of HJS-ECU) relevant data</summary>
    public class Firmware
    {
        /// <summary>Compatibility since PSoC version 8 (max. downgradable)</summary>
        private const byte mRevisionPsoc8 = 164;
        /// <summary>Compatibility of firmware</summary>
        private byte mSoftwareRevision;

        /// <summary>Enumeration of PSoC gain values used for egr and fuel level sensor</summary>
        public enum PsocGain
        {
            /// <summary>Gain 0.06</summary>
            PGA_G0_06 = 0,
            /// <summary>Gain 16.0</summary>
            PGA_G16_0 = 8,
            /// <summary>Gain 48.0</summary>
            PGA_G48_0 = 12,
            /// <summary>Gain 0.12</summary>
            PGA_G0_12 = 16,
            /// <summary>Gain 8.00</summary>
            PGA_G8_00 = 24,
            /// <summary>Gain 24.0</summary>
            PGA_G24_0 = 28,
            /// <summary>Gain 0.18</summary>
            PGA_G0_18 = 32,
            /// <summary>Gain 5.33</summary>
            PGA_G5_33 = 40,
            /// <summary>Gain 0.25</summary>
            PGA_G0_25 = 48,
            /// <summary>Gain 4.00</summary>
            PGA_G4_00 = 56,
            /// <summary>Gain 0.31</summary>
            PGA_G0_31 = 64,
            /// <summary>Gain 3.20</summary>
            PGA_G3_20 = 72,
            /// <summary>Gain 0.37</summary>
            PGA_G0_37 = 80,
            /// <summary>Gain 2.67</summary>
            PGA_G2_67 = 88,
            /// <summary>Gain 0.43</summary>
            PGA_G0_43 = 96,
            /// <summary>Gain 2.27</summary>
            PGA_G2_27 = 104,
            /// <summary>Gain 0.50</summary>
            PGA_G0_50 = 112,
            /// <summary>Gain 2.00</summary>
            PGA_G2_00 = 120,
            /// <summary>Gain 0.56</summary>
            PGA_G0_56 = 128,
            /// <summary>Gain 1.78</summary>
            PGA_G1_78 = 136,
            /// <summary>Gain 0.62</summary>
            PGA_G0_62 = 144,
            /// <summary>Gain 1.60</summary>
            PGA_G1_60 = 152,
            /// <summary>Gain 0.68</summary>
            PGA_G0_68 = 160,
            /// <summary>Gain 1.46</summary>
            PGA_G1_46 = 168,
            /// <summary>Gain 0.75</summary>
            PGA_G0_75 = 176,
            /// <summary>Gain 1.33</summary>
            PGA_G1_33 = 184,
            /// <summary>Gain 0.81</summary>
            PGA_G0_81 = 192,
            /// <summary>Gain 1.23</summary>
            PGA_G1_23 = 200,
            /// <summary>Gain 0.87</summary>
            PGA_G0_87 = 208,
            /// <summary>Gain 1.14</summary>
            PGA_G1_14 = 216,
            /// <summary>Gain 0.93</summary>
            PGA_G0_93 = 224,
            /// <summary>Gain 1.06</summary>
            PGA_G1_06 = 232,
            /// <summary>Gain 1.00</summary>
            PGA_G1_00 = 248
        }

        /// <summary>Enumeration of fuel level sensor signal types</summary>
        public enum TankSignal
        {
            /// <summary>Null enumeration</summary>
            TK_NULL = 0,
            /// <summary>Input B, resistor, 120 ohm ohm pullup</summary>
			TK_NEU_120R = 0x02,
            /// <summary>Input B, resistor, 1.2 k ohm pullup</summary>
            TK_NEU_1K2 = 0x52,
            /// <summary>Input A, voltage</summary>
			TK_ANALOG = 0x10,
            /// <summary>Input A, pulsed, double pulse</summary>
            TK_GEPULST = 0x11,
            /// <summary>Input A, pulsed, single pulse (since SW 1.x)</summary>
            TK_GEPULST_V36 = 0x14,
            /// <summary>Input EGR, pulse width modulation</summary>
            TK_PWM = 0x80,
            /// <summary>Input EGR, frequency</summary>
            TK_FREQUENZ = 0x82
        }

        /// <summary>Enumeration for Baudrates</summary>
        public enum CanBaudrate
        {
            /// <summary>Baudrate not set</summary>
            BR_NOT_SET = 0,
            /// <summary>Can baudrate 125000 Hz</summary>
            BR_125_KHZ = 1,
            /// <summary>Can baudrate 250000 Hz</summary>
            BR_250_KHZ = 2,
            /// <summary>Can baudrate 500000 Hz</summary>
            BR_500_KHZ = 3
        }

        /// <summary>Enumeration for measured and calcuated values compatibility 8</summary>
        public enum MessWert8
        {
            /// <summary>dosing pulses from tank low int</summary>
            MRW_ULL_DOSIERIMPULSE_TANK = 0,
            /// <summary>dosing pulses from tank high int</summary>
            MRW_ULH_DOSIERIMPULSE_TANK = 1,
            /// <summary>dosing pulses in filter low int</summary>
            MRW_ULL_DOSIERIMPULSE_FILTER = 2,
            /// <summary>dosing pulses in filter high int</summary>
            MRW_ULH_DOSIERIMPULSE_FILTER = 3,
            /// <summary>dosing pulses absolute low int</summary>
            MRW_ULL_DOSIERIMPULSE = 4,
            /// <summary>dosing pulses absolute high int</summary>
            MRW_ULH_DOSIERIMPULSE = 5,
            /// <summary>current additive concentration</summary>
            MRW_UI_ADDITIV_IST = 6,
            /// <summary>additive concentration should</summary>
            MRW_UI_ADDITIV_SOLL = 7,
            /// <summary>fuel level</summary>
            MRW_UI_TANKINHALT = 8,
            /// <summary>averaged fuel level</summary>
            MRW_UI_TANKINHALT_MITTEL = 9,
            /// <summary>averaged pressure</summary>
            MRW_UI_GEGENDRUCKMITTEL = 10,
            /// <summary>counted kilometers</summary>
            MRW_UI_KILOMETER = 11,
            /// <summary>soot rate [mg/min]</summary>
            MRW_UI_RUSSMENGE = 12,
            /// <summary>soot load sum low int</summary>
            MRW_ULL_BELADUNGSSUMME = 13,
            /// <summary>soot load sum high int</summary>
            MRW_ULH_BELADUNGSSUMME = 14,
            /// <summary>regerating time</summary>
            MRW_UI_REGTIME = 15,
            /// <summary>desired heating power</summary>
            MRW_UI_HEIZLEISTUNG_SOLL = 16,
            /// <summary>heating power a</summary>
            MRW_UI_HEIZLEISTUNG_IST_A = 17,
            /// <summary>heating power b</summary>
            MRW_UI_HEIZLEISTUNG_IST_B = 18,
            /// <summary>possible heating power a at 100% pwm</summary>
            MRW_UI_HEIZLEISTUNG_IST_100A = 19,
            /// <summary>possible heating power b at 100% pwm</summary>
            MRW_UI_HEIZLEISTUNG_IST_100B = 20,
            /// <summary>status register from both single highside switches</summary>
            MRW_UI_SHS_STATUS = 21,
            /// <summary>heating current a</summary>
            MRW_UI_STROM_A = 22,
            /// <summary>heating current b</summary>
            MRW_UI_STROM_B = 23,
            /// <summary>resistance of heating element a</summary>
            MRW_UI_RI_A = 24,
            /// <summary>resistance of heating element b</summary>
            MRW_UI_RI_B = 25,
            /// <summary>pwm of heater a</summary>
            MRW_UI_HEIZ_PWM_A = 26,
            /// <summary>pwm of heater b</summary>
            MRW_UI_HEIZ_PWM_B = 27,
            /// <summary>duration of dosing pulse [ms]</summary>
            MRW_UI_IMPULSTIME = 28,
            /// <summary>moment of lowest current after saddle piont</summary>
            MRW_UI_MIN_I_TIME = 29,
            /// <summary>state of dosing pulse</summary>
            MRW_UI_STATUS = 30,
            /// <summary>latest measures dosing current for curve sketching</summary>
            MRW_SI_IST_STROM = 31,
            /// <summary>dosing current at saddle point</summary>
            MRW_SI_MAX_STROM = 32,
            /// <summary>lowest Di/Dt</summary>
            MRW_SI_DIDT_MIN = 33,
            /// <summary>debug 5</summary>
            MRW_DEBUG5 = 34,
            /// <summary>debug 6</summary>
            MRW_DEBUG6 = 35,
            /// <summary>debug 7</summary>
            MRW_DEBUG7 = 36,
            /// <summary>current filterload in percent</summary>
            MRW_REGWUNSCH = 37,
            /// <summary>regeratable filterload</summary>
            MRW_BELADUNG_REGBAR = 38,
            /// <summary>regeneration lock time</summary>
            MRW_REG_SPERRZEIT = 39,
            /// <summary>password</summary>
            MRW_PASSWD0 = 40,
            /// <summary>password</summary>
            MRW_PASSWD1 = 41,
            /// <summary>password</summary>
            MRW_PASSWD2 = 42,
            /// <summary>password</summary>
            MRW_PASSWD3 = 43,
            /// <summary>operation time</summary>
            MRW_OPERATIONTIME = 44,
            /// <summary>air volume flow</summary>
            MRW_LUFTMENGE = 45,
            /// <summary>cleaning cycle</summary>
            MRW_REINIGUNGS_INTERVALL = 46,
            /// <summary>total fuel</summary>
            MRW_UI_TANKMENGE_GESAMT = 47,
            /// <summary>averaged pressure for CRT</summary>
            MRW_UI_CRTDRUCKMITTEL = 48,
            /// <summary>CRT temperature coefficient</summary>
            MRW_UI_CRTWIRKUNGMITTEL = 49,
            /// <summary>deviation of launced heat energy a</summary>
            MRW_SI_DELTA_JOULE_A = 50,
            /// <summary>deviation of launced heat energy b</summary>
            MRW_SI_DELTA_JOULE_B = 51,
            /// <summary>number of saving empirical values</summary>
            MRW_UI_SAVE_VALUE = 52,
            /// <summary>number of regenerations</summary>
            MRW_UI_REG_STARTS = 53,
            /// <summary>absolute heat ernergy consumption low int</summary>
            MRW_ULL_COMPLETE_JOULE = 54,
            /// <summary>absolute heat ernergy consumption high int</summary>
            MRW_ULH_COMPLETE_JOULE = 55,
            /// <summary>derating level</summary>
            MRW_UI_DERATING_LEVEL = 56,
            /// <summary>fuel consumption</summary>
            MRW_UI_KRAFTSTOFFVERBRAUCH = 57,
            /// <summary>time between continous dosing pulses</summary>
            MRW_UI_DOSIERINTERVALL = 58,
            /// <summary>filter runtime for ICDosing</summary>
            MRW_UI_FILTER_RUNTIME = 59,
            /// <summary>ICDosing status bitmask</summary>
            MRW_UI_ICD_STATUS = 60,
            /// <summary>Intake air pressure</summary>
            MRW_UI_MAF_AIR_PESS = 61,
            /// <summary>Intake air temperature</summary>
            MRW_SI_MAF_AIR_TEMP = 62,
            /// <summary>Intake mass air flow rate (calculated)</summary>
            MRW_UI_MAF_LMS_OUT = 63,
            /// <summary>A/D Value for intake mass air flow rate (calculated)</summary>
            MRW_UI_AD_MAF_LMS_OUT = 64,
            /// <summary>Grundfos SCR Error</summary>
            MRW_UI_SCR_ERROR = 65,
            /// <summary>ADBlue tank level</summary>
            MRW_UI_GRUNDFOS_TANKLEVEL = 66,
            /// <summary>Visualisation of pressure alarm</summary>
            MRW_UI_VIEW_P_ALARM = 67,
            /// <summary>Visualisation of pressure alarm time</summary>
            MRW_UI_VIEW_T_ALARM = 68,
            /// <summary>Visualisation of pressure warning</summary>
            MRW_UI_VIEW_P_WARN = 69,
            /// <summary>Visualisation of pressure warning time</summary>
            MRW_UI_VIEW_T_WARN = 70,
            /// <summary>operating voltage</summary>
            MRW_UI_UB = 128,
            /// <summary>minutes since programmed (last 2 bytes)</summary>
            MRW_UI_TIME = 129,
            /// <summary>filter back pressure</summary>
            MRW_UI_P_VOR = 130,
            /// <summary>temperature inside ECU</summary>
            MRW_SI_T_ECU = 131,
            /// <summary>temperature in front of filter</summary>
            MRW_SI_T_VOR = 132,
            /// <summary>temperature after filter</summary>
            MRW_SI_T_NACH = 133,
            /// <summary>load or air mass flow</summary>
            MRW_UI_LAST = 134,
            /// <summary>Clamp15</summary>
            MRW_UI_KL15 = 135,
            /// <summary>D+</summary>
            MRW_UI_D_PLUS = 136,
            /// <summary>ADC value for fuel level</summary>
            MRW_UI_AD_TANK = 137,
            /// <summary>ADC value for former fuel level b</summary>
            MRW_UI_AD_FREE3 = 138,
            /// <summary>ADC value for former unternal supply voltage</summary>
            MRW_UI_AD_FREE2 = 139,
            /// <summary>ADC value for differential filter pressure</summary>
            MRW_UI_AD_P_VOR = 140,
            /// <summary>ADC value for former mosfet temperature</summary>
            MRW_UI_AD_FREE1 = 141,
            /// <summary>ADC value for heat current A</summary>
            MRW_UI_AD_I_HEIZ_A = 142,
            /// <summary>ADC value for operating voltage</summary>
            MRW_UI_AD_UB = 143,
            /// <summary>ADC value for temperature in front of filter</summary>
            MRW_UI_AD_T_VOR = 144,
            /// <summary>ADC value for temperature after filter</summary>
            MRW_UI_AD_T_NACH = 145,
            /// <summary>ADC value for heat current b</summary>
            MRW_UI_AD_I_HEIZ_B = 146,
            /// <summary>ADC value for dosing pump current</summary>
            MRW_UI_AD_I_PUMP = 147,
            /// <summary>ADC value for load signal or air mass sensor</summary>
            MRW_UI_AD_LAST = 148,
            /// <summary>normed heat current a</summary>
            MRW_UI_I_HEIZ_A = 149,
            /// <summary>normed heat current b</summary>
            MRW_UI_I_HEIZ_B = 150,
            /// <summary>normed fuel level 0,00% - 100,00%</summary>
            MRW_UI_TANKINHALT_PROZENT = 151,
            /// <summary>normed fuel level [l]</summary>
            MRW_UI_TANKINHALT_LITER = 152,
            /// <summary>speed [100m/h]</summary>
            MRW_UI_GESCHWINDIGKEIT = 153,
            /// <summary>motor speed [U/min]</summary>
            MRW_UI_DREHZAHL = 154,
            /// <summary>pwm of air mass sensor</summary>
            MRW_UI_LMM_PWM = 155,
            /// <summary>pwm of EGR</summary>
            MRW_UI_AGR_PWM = 156,
            /// <summary>frequency of EGR</summary>
            MRW_UI_AGR_FREQUENZ = 157,
            /// <summary>position for EGR</summary>
            MRW_UI_AGR_POS = 158,
            /// <summary>offset fir EGR</summary>
            MRW_UI_AGR_OFFSET = 159,
            /// <summary>resistance of first temperature sensor</summary>
            MRW_UI_R_T_VOR = 160,
            /// <summary>resistance of second temperature sensor</summary>
            MRW_UI_R_T_NACH = 161,
            /// <summary>startup counter</summary>
            MRW_UI_STARTUP = 162,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_0 = 163,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_1 = 164,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_2 = 165,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_3 = 166,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_4 = 167,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_5 = 168,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_6 = 169,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_7 = 170,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_8 = 171,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_9 = 172,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_10 = 173,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_11 = 174,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_12 = 175,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_13 = 176,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_14 = 177,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_15 = 178,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_16 = 179,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_17 = 180,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_18 = 181,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_19 = 182,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_20 = 183,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_21 = 184,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_22 = 185,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_23 = 186,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_24 = 187,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_25 = 188,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_26 = 189,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_27 = 190,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_28 = 191,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_29 = 192,
            /// <summary>value 1 from can i.e. turns</summary>
            MRW_UI_CAN_VALUE_1 = 193,
            /// <summary>value 2 from can i.e. speed</summary>
            MRW_UI_CAN_VALUE_2 = 194,
            /// <summary>value 3 from can</summary>
            MRW_UI_CAN_VALUE_3 = 195,
            /// <summary>value 4 from can</summary>
            MRW_UI_CAN_VALUE_4 = 196,
            /// <summary>value 1 from can i.e. turns</summary>
            MRW_SI_CAN_VALUE_5 = 197,
            /// <summary>value 2 from can i.e. speed</summary>
            MRW_SI_CAN_VALUE_6 = 198,
            /// <summary>can order register</summary>
            MRW_UI_CAN_ORDER = 199,
            /// <summary>can error register</summary>
            MRW_UI_CAN_ERROR = 200,
            /// <summary>wake up counter</summary>
            MRW_UI_WAKEUP_CTR = 201,
            /// <summary>temperature normed pressure</summary>
            MRW_UI_PNORM = 202,
            /// <summary>PSoC-Version</summary>
            MRW_UI_PSOC_VER = 203,
            /// <summary>Impact pressure</summary>
            MRW_UI_ADSTAUDRUCK = 204,
            /// <summary>Coolant temperature</summary>
            MRW_SI_CoolantTemp = 205,
            /// <summary>Flow rate of Grundfos pump</summary>
            MRW_UI_MAF_FLOWRATE = 206,
            /// <summary>Status of Grundfos pump</summary>
            MRW_UI_MAF_PUMP_STATUS = 207,
            /// <summary>Value for N/A or unused</summary>
            NotAvailable = 255
        }

        /// <summary>Enumeration for measured and calcuated values compatibility 9</summary>
        public enum MessWert9
        {
            /// <summary>dosing pulses from tank low int</summary>
            MRW_ULL_DOSIERIMPULSE_TANK = 0,
            /// <summary>dosing pulses from tank high int</summary>
            MRW_ULH_DOSIERIMPULSE_TANK = 1,
            /// <summary>dosing pulses in filter low int</summary>
            MRW_ULL_DOSIERIMPULSE_FILTER = 2,
            /// <summary>dosing pulses in filter high int</summary>
            MRW_ULH_DOSIERIMPULSE_FILTER = 3,
            /// <summary>dosing pulses absolute low int</summary>
            MRW_ULL_DOSIERIMPULSE = 4,
            /// <summary>dosing pulses absolute high int</summary>
            MRW_ULH_DOSIERIMPULSE = 5,
            /// <summary>current additive concentration</summary>
            MRW_UI_ADDITIV_IST = 6,
            /// <summary>additive concentration should</summary>
            MRW_UI_ADDITIV_SOLL = 7,
            /// <summary>fuel level</summary>
            MRW_ULL_TANKINHALT = 8,
            /// <summary>fuel level</summary>
            MRW_ULH_TANKINHALT = 9,
            /// <summary>averaged fuel level</summary>
            MRW_ULL_TANKINHALT_MITTEL = 10,
            /// <summary>averaged fuel level</summary>
            MRW_ULH_TANKINHALT_MITTEL = 11,
            /// <summary>averaged pressure</summary>
            MRW_UI_GEGENDRUCKMITTEL = 12,
            /// <summary>counted kilometers</summary>
            MRW_UI_KILOMETER = 13,
            /// <summary>soot rate [mg/min]</summary>
            MRW_UI_RUSSMENGE = 14,
            /// <summary>soot load sum low int</summary>
            MRW_ULL_BELADUNGSSUMME = 15,
            /// <summary>soot load sum high int</summary>
            MRW_ULH_BELADUNGSSUMME = 16,
            /// <summary>regerating time</summary>
            MRW_UI_REGTIME = 17,
            /// <summary>desired heating power</summary>
            MRW_UI_HEIZLEISTUNG_SOLL = 18,
            /// <summary>heating power a</summary>
            MRW_UI_HEIZLEISTUNG_IST_A = 19,
            /// <summary>heating power b</summary>
            MRW_UI_HEIZLEISTUNG_IST_B = 20,
            /// <summary>possible heating power a at 100% pwm</summary>
            MRW_UI_HEIZLEISTUNG_IST_100A = 21,
            /// <summary>possible heating power b at 100% pwm</summary>
            MRW_UI_HEIZLEISTUNG_IST_100B = 22,
            /// <summary>status register from both single highside switches</summary>
            MRW_UI_SHS_STATUS = 23,
            /// <summary>heating current a</summary>
            MRW_UI_STROM_A = 24,
            /// <summary>heating current b</summary>
            MRW_UI_STROM_B = 25,
            /// <summary>resistance of heating element a</summary>
            MRW_UI_RI_A = 26,
            /// <summary>resistance of heating element b</summary>
            MRW_UI_RI_B = 27,
            /// <summary>pwm of heater a</summary>
            MRW_UI_HEIZ_PWM_A = 28,
            /// <summary>pwm of heater b</summary>
            MRW_UI_HEIZ_PWM_B = 29,
            /// <summary>duration of dosing pulse [ms]</summary>
            MRW_UI_IMPULSTIME = 30,
            /// <summary>moment of lowest current after saddle piont</summary>
            MRW_UI_MIN_I_TIME = 31,
            /// <summary>state of dosing pulse</summary>
            MRW_UI_STATUS = 32,
            /// <summary>latest measures dosing current for curve sketching</summary>
            MRW_SI_IST_STROM = 33,
            /// <summary>dosing current at saddle point</summary>
            MRW_SI_MAX_STROM = 34,
            /// <summary>lowest Di/Dt</summary>
            MRW_SI_DIDT_MIN = 35,
            /// <summary>debug 5</summary>
            MRW_DEBUG5 = 36,
            /// <summary>debug 6</summary>
            MRW_DEBUG6 = 37,
            /// <summary>debug 7</summary>
            MRW_DEBUG7 = 38,
            /// <summary>current filterload in percent</summary>
            MRW_REGWUNSCH = 39,
            /// <summary>regeratable filterload</summary>
            MRW_BELADUNG_REGBAR = 40,
            /// <summary>regeneration lock time</summary>
            MRW_REG_SPERRZEIT = 41,
            /// <summary>password</summary>
            MRW_PASSWD0 = 42,
            /// <summary>password</summary>
            MRW_PASSWD1 = 43,
            /// <summary>password</summary>
            MRW_PASSWD2 = 44,
            /// <summary>password</summary>
            MRW_PASSWD3 = 45,
            /// <summary>operation time</summary>
            MRW_OPERATIONTIME = 46,
            /// <summary>air volume flow</summary>
            MRW_LUFTMENGE = 47,
            /// <summary>cleaning cycle</summary>
            MRW_REINIGUNGS_INTERVALL = 48,
            /// <summary>total fuel</summary>
            MRW_ULL_TANKMENGE_GESAMT = 49,
            /// <summary>total fuel</summary>
            MRW_ULH_TANKMENGE_GESAMT = 50,
            /// <summary>averaged pressure for CRT</summary>
            MRW_UI_CRTDRUCKMITTEL = 51,
            /// <summary>CRT temperature coefficient</summary>
            MRW_UI_CRTWIRKUNGMITTEL = 52,
            /// <summary>deviation of launced heat energy a</summary>
            MRW_SI_DELTA_JOULE_A = 53,
            /// <summary>deviation of launced heat energy b</summary>
            MRW_SI_DELTA_JOULE_B = 54,
            /// <summary>number of saving empirical values</summary>
            MRW_UI_SAVE_VALUE = 55,
            /// <summary>number of regenerations</summary>
            MRW_UI_REG_STARTS = 56,
            /// <summary>absolute heat ernergy consumption low int</summary>
            MRW_ULL_COMPLETE_JOULE = 57,
            /// <summary>absolute heat ernergy consumption high int</summary>
            MRW_ULH_COMPLETE_JOULE = 58,
            /// <summary>derating level</summary>
            MRW_UI_DERATING_LEVEL = 59,
            /// <summary>fuel consumption</summary>
            MRW_UI_KRAFTSTOFFVERBRAUCH = 60,
            /// <summary>time between continous dosing pulses</summary>
            MRW_UI_DOSIERINTERVALL = 61,
            /// <summary>filter runtime for ICDosing</summary>
            MRW_UI_FILTER_RUNTIME = 62,
            /// <summary>ICDosing status bitmask</summary>
            MRW_UI_ICD_STATUS = 63,
            /// <summary>Intake air pressure</summary>
            MRW_UI_MAF_AIR_PESS = 64,
            /// <summary>Intake air temperature</summary>
            MRW_SI_MAF_AIR_TEMP = 65,
            /// <summary>Intake mass air flow rate (calculated)</summary>
            MRW_UI_MAF_LMS_OUT = 66,
            /// <summary>A/D Value for intake mass air flow rate (calculated)</summary>
            MRW_UI_AD_MAF_LMS_OUT = 67,
            /// <summary>Grundfos SCR Error</summary>
            MRW_UI_SCR_ERROR = 68,
            /// <summary>ADBlue tank level</summary>
            MRW_UI_GRUNDFOS_TANKLEVEL = 69,
            /// <summary>Visualisation of pressure alarm</summary>
            MRW_UI_VIEW_P_ALARM = 70,
            /// <summary>Visualisation of pressure alarm time</summary>
            MRW_UI_VIEW_T_ALARM = 71,
            /// <summary>Visualisation of pressure warning</summary>
            MRW_UI_VIEW_P_WARN = 72,
            /// <summary>Visualisation of pressure warning time</summary>
            MRW_UI_VIEW_T_WARN = 73,
            /// <summary>Remaining Additiv in tank</summary>
            MRW_UI_ADDITIV_REST = 74,
            /// <summary>operating voltage</summary>
            MRW_UI_UB = 128,
            /// <summary>minutes since programmed (last 2 bytes)</summary>
            MRW_UI_TIME = 129,
            /// <summary>filter back pressure</summary>
            MRW_UI_P_VOR = 130,
            /// <summary>temperature inside ECU</summary>
            MRW_SI_T_ECU = 131,
            /// <summary>temperature in front of filter</summary>
            MRW_SI_T_VOR = 132,
            /// <summary>temperature after filter</summary>
            MRW_SI_T_NACH = 133,
            /// <summary>load or air mass flow</summary>
            MRW_UI_LAST = 134,
            /// <summary>Clamp15</summary>
            MRW_UI_KL15 = 135,
            /// <summary>D+</summary>
            MRW_UI_D_PLUS = 136,
            /// <summary>ADC value for fuel level</summary>
            MRW_UI_AD_TANK = 137,
            /// <summary>ADC value for former fuel level b</summary>
            MRW_UI_AD_FREE3 = 138,
            /// <summary>ADC value for former unternal supply voltage</summary>
            MRW_UI_AD_FREE2 = 139,
            /// <summary>ADC value for differential filter pressure</summary>
            MRW_UI_AD_P_VOR = 140,
            /// <summary>ADC value for former mosfet temperature</summary>
            MRW_UI_AD_FREE1 = 141,
            /// <summary>ADC value for heat current A</summary>
            MRW_UI_AD_I_HEIZ_A = 142,
            /// <summary>ADC value for operating voltage</summary>
            MRW_UI_AD_UB = 143,
            /// <summary>ADC value for temperature in front of filter</summary>
            MRW_UI_AD_T_VOR = 144,
            /// <summary>ADC value for temperature after filter</summary>
            MRW_UI_AD_T_NACH = 145,
            /// <summary>ADC value for heat current b</summary>
            MRW_UI_AD_I_HEIZ_B = 146,
            /// <summary>ADC value for dosing pump current</summary>
            MRW_UI_AD_I_PUMP = 147,
            /// <summary>ADC value for load signal or air mass sensor</summary>
            MRW_UI_AD_LAST = 148,
            /// <summary>normed heat current a</summary>
            MRW_UI_I_HEIZ_A = 149,
            /// <summary>normed heat current b</summary>
            MRW_UI_I_HEIZ_B = 150,
            /// <summary>normed fuel level 0,00% - 100,00%</summary>
            MRW_UI_TANKINHALT_PROZENT = 151,
            /// <summary>normed fuel level [l]</summary>
            MRW_UI_TANKINHALT_LITER = 152,
            /// <summary>speed [100m/h]</summary>
            MRW_UI_GESCHWINDIGKEIT = 153,
            /// <summary>motor speed [U/min]</summary>
            MRW_UI_DREHZAHL = 154,
            /// <summary>pwm of air mass sensor</summary>
            MRW_UI_LMM_PWM = 155,
            /// <summary>pwm of EGR</summary>
            MRW_UI_AGR_PWM = 156,
            /// <summary>frequency of EGR</summary>
            MRW_UI_AGR_FREQUENZ = 157,
            /// <summary>position for EGR</summary>
            MRW_UI_AGR_POS = 158,
            /// <summary>offset fir EGR</summary>
            MRW_UI_AGR_OFFSET = 159,
            /// <summary>resistance of first temperature sensor</summary>
            MRW_UI_R_T_VOR = 160,
            /// <summary>resistance of second temperature sensor</summary>
            MRW_UI_R_T_NACH = 161,
            /// <summary>startup counter</summary>
            MRW_UI_STARTUP = 162,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_0 = 163,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_1 = 164,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_2 = 165,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_3 = 166,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_4 = 167,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_5 = 168,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_6 = 169,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_7 = 170,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_8 = 171,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_9 = 172,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_10 = 173,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_11 = 174,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_12 = 175,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_13 = 176,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_14 = 177,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_15 = 178,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_16 = 179,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_17 = 180,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_18 = 181,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_19 = 182,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_20 = 183,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_21 = 184,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_22 = 185,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_23 = 186,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_24 = 187,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_25 = 188,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_26 = 189,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_27 = 190,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_28 = 191,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_29 = 192,
            /// <summary>value 1 from can i.e. turns</summary>
            MRW_UI_CAN_VALUE_1 = 193,
            /// <summary>value 2 from can i.e. speed</summary>
            MRW_UI_CAN_VALUE_2 = 194,
            /// <summary>value 3 from can</summary>
            MRW_UI_CAN_VALUE_3 = 195,
            /// <summary>value 4 from can</summary>
            MRW_UI_CAN_VALUE_4 = 196,
            /// <summary>value 1 from can i.e. turns</summary>
            MRW_UI_CAN_VALUE_5 = 197,
            /// <summary>value 2 from can i.e. speed</summary>
            MRW_UI_CAN_VALUE_6 = 198,
            /// <summary>value 3 from can</summary>
            MRW_UI_CAN_VALUE_7 = 199,
            /// <summary>value 4 from can</summary>
            MRW_UI_CAN_VALUE_8 = 200,
            /// <summary>value 5 from can (signed)</summary>
            MRW_SI_CAN_VALUE_9 = 201,
            /// <summary>value 6 from can (signed)</summary>
            MRW_SI_CAN_VALUE_10 = 202,
            /// <summary>value 5 from can (signed)</summary>
            MRW_SI_CAN_VALUE_11 = 203,
            /// <summary>value 6 from can (signed)</summary>
            MRW_SI_CAN_VALUE_12 = 204,
            /// <summary>value 5 from can (signed)</summary>
            MRW_SI_CAN_VALUE_13 = 205,
            /// <summary>value 6 from can (signed)</summary>
            MRW_SI_CAN_VALUE_14 = 206,
            /// <summary>value 5 from can (signed)</summary>
            MRW_SI_CAN_VALUE_15 = 207,
            /// <summary>value 6 from can (signed)</summary>
            MRW_SI_CAN_VALUE_16 = 208,
            /// <summary>can order register</summary>
            MRW_UI_CAN_ORDER = 209,
            /// <summary>can error register</summary>
            MRW_UI_CAN_ERROR = 210,
            /// <summary>wake up counter</summary>
            MRW_UI_WAKEUP_CTR = 211,
            /// <summary>temperature normed pressure</summary>
            MRW_UI_PNORM = 212,
            /// <summary>PSoC-Version</summary>
            MRW_UI_PSOC_VER = 213,
            /// <summary>Impact pressure</summary>
            MRW_UI_ADSTAUDRUCK = 214,
            /// <summary>Coolant temperature</summary>
            MRW_SI_CoolantTemp = 215,
            /// <summary>Flow rate of Grundfos pump</summary>
            MRW_UI_MAF_FLOWRATE = 216,
            /// <summary>Status of Grundfos pump</summary>
            MRW_UI_MAF_PUMP_STATUS = 217,
            /// <summary>Absolute ambient pressure</summary>
            MRW_UI_ABSOLUTDRUCK = 218,
            /// <summary>Value for N/A or unused</summary>
            NotAvailable = 255
        }

        /// <summary>Enumeration for measured and calcuated values compatibility 10</summary>
        public enum MessWert10
        {
            /// <summary>dosing pulses from tank low int</summary>
            MRW_ULL_DOSIERIMPULSE_TANK = 0,
            /// <summary>dosing pulses from tank high int</summary>
            MRW_ULH_DOSIERIMPULSE_TANK = 1,
            /// <summary>dosing pulses in filter low int</summary>
            MRW_ULL_DOSIERIMPULSE_FILTER = 2,
            /// <summary>dosing pulses in filter high int</summary>
            MRW_ULH_DOSIERIMPULSE_FILTER = 3,
            /// <summary>dosing pulses absolute low int</summary>
            MRW_ULL_DOSIERIMPULSE = 4,
            /// <summary>dosing pulses absolute high int</summary>
            MRW_ULH_DOSIERIMPULSE = 5,
            /// <summary>current additive concentration</summary>
            MRW_UI_ADDITIV_IST = 6,
            /// <summary>additive concentration should</summary>
            MRW_UI_ADDITIV_SOLL = 7,
            /// <summary>fuel level</summary>
            MRW_ULL_TANKINHALT = 8,
            /// <summary>fuel level</summary>
            MRW_ULH_TANKINHALT = 9,
            /// <summary>averaged fuel level</summary>
            MRW_ULL_TANKINHALT_MITTEL = 10,
            /// <summary>averaged fuel level</summary>
            MRW_ULH_TANKINHALT_MITTEL = 11,
            /// <summary>averaged pressure</summary>
            MRW_UI_GEGENDRUCKMITTEL = 12,
            /// <summary>counted kilometers</summary>
            MRW_UI_KILOMETER = 13,
            /// <summary>soot rate [mg/min]</summary>
            MRW_UI_RUSSMENGE = 14,
            /// <summary>soot load sum low int</summary>
            MRW_ULL_BELADUNGSSUMME = 15,
            /// <summary>soot load sum high int</summary>
            MRW_ULH_BELADUNGSSUMME = 16,
            /// <summary>regerating time</summary>
            MRW_UI_REGTIME = 17,
            /// <summary>desired heating power</summary>
            MRW_UI_HEIZLEISTUNG_SOLL = 18,
            /// <summary>heating power a</summary>
            MRW_UI_HEIZLEISTUNG_IST_A = 19,
            /// <summary>heating power b</summary>
            MRW_UI_HEIZLEISTUNG_IST_B = 20,
            /// <summary>possible heating power a at 100% pwm</summary>
            MRW_UI_HEIZLEISTUNG_IST_100A = 21,
            /// <summary>possible heating power b at 100% pwm</summary>
            MRW_UI_HEIZLEISTUNG_IST_100B = 22,
            /// <summary>status register from both single highside switches</summary>
            MRW_UI_SHS_STATUS = 23,
            /// <summary>heating current a</summary>
            MRW_UI_STROM_A = 24,
            /// <summary>heating current b</summary>
            MRW_UI_STROM_B = 25,
            /// <summary>resistance of heating element a</summary>
            MRW_UI_RI_A = 26,
            /// <summary>resistance of heating element b</summary>
            MRW_UI_RI_B = 27,
            /// <summary>pwm of heater a</summary>
            MRW_UI_HEIZ_PWM_A = 28,
            /// <summary>pwm of heater b</summary>
            MRW_UI_HEIZ_PWM_B = 29,
            /// <summary>duration of dosing pulse [ms]</summary>
            MRW_UI_IMPULSTIME = 30,
            /// <summary>moment of lowest current after saddle piont</summary>
            MRW_UI_MIN_I_TIME = 31,
            /// <summary>state of dosing pulse</summary>
            MRW_UI_STATUS = 32,
            /// <summary>latest measures dosing current for curve sketching</summary>
            MRW_SI_IST_STROM = 33,
            /// <summary>dosing current at saddle point</summary>
            MRW_SI_MAX_STROM = 34,
            /// <summary>lowest Di/Dt</summary>
            MRW_SI_DIDT_MIN = 35,
            /// <summary>ECU Status</summary>
            MRW_UI_ECU_STATUS = 36,
            /// <summary>debug 6</summary>
            MRW_DEBUG6 = 37,
            /// <summary>debug 7</summary>
            MRW_DEBUG7 = 38,
            /// <summary>current filterload in percent</summary>
            MRW_REGWUNSCH = 39,
            /// <summary>regeratable filterload</summary>
            MRW_BELADUNG_REGBAR = 40,
            /// <summary>regeneration lock time</summary>
            MRW_REG_SPERRZEIT = 41,
            /// <summary>password</summary>
            MRW_PASSWD0 = 42,
            /// <summary>password</summary>
            MRW_PASSWD1 = 43,
            /// <summary>password</summary>
            MRW_PASSWD2 = 44,
            /// <summary>password</summary>
            MRW_PASSWD3 = 45,
            /// <summary>operation time</summary>
            MRW_OPERATIONTIME = 46,
            /// <summary>air volume flow</summary>
            MRW_LUFTMENGE = 47,
            /// <summary>cleaning cycle</summary>
            MRW_REINIGUNGS_INTERVALL = 48,
            /// <summary>total fuel</summary>
            MRW_ULL_TANKMENGE_GESAMT = 49,
            /// <summary>total fuel</summary>
            MRW_ULH_TANKMENGE_GESAMT = 50,
            /// <summary>averaged pressure for CRT</summary>
            MRW_UI_CRTDRUCKMITTEL = 51,
            /// <summary>CRT temperature coefficient</summary>
            MRW_UI_CRTWIRKUNGMITTEL = 52,
            /// <summary>deviation of launced heat energy a</summary>
            MRW_SI_DELTA_JOULE_A = 53,
            /// <summary>deviation of launced heat energy b</summary>
            MRW_SI_DELTA_JOULE_B = 54,
            /// <summary>number of saving empirical values</summary>
            MRW_UI_SAVE_VALUE = 55,
            /// <summary>number of regenerations</summary>
            MRW_UI_REG_STARTS = 56,
            /// <summary>absolute heat ernergy consumption low int</summary>
            MRW_ULL_COMPLETE_JOULE = 57,
            /// <summary>absolute heat ernergy consumption high int</summary>
            MRW_ULH_COMPLETE_JOULE = 58,
            /// <summary>derating level</summary>
            MRW_UI_DERATING_LEVEL = 59,
            /// <summary>fuel consumption</summary>
            MRW_UI_KRAFTSTOFFVERBRAUCH = 60,
            /// <summary>time between continous dosing pulses</summary>
            MRW_UI_DOSIERINTERVALL = 61,
            /// <summary>filter runtime for ICDosing</summary>
            MRW_UI_FILTER_RUNTIME = 62,
            /// <summary>ICDosing status bitmask</summary>
            MRW_UI_ICD_STATUS = 63,
            /// <summary>Intake air pressure</summary>
            MRW_UI_MAF_AIR_PESS = 64,
            /// <summary>Intake air temperature</summary>
            MRW_SI_MAF_AIR_TEMP = 65,
            /// <summary>Intake mass air flow rate (calculated)</summary>
            MRW_UI_MAF_LMS_OUT = 66,
            /// <summary>A/D Value for intake mass air flow rate (calculated)</summary>
            MRW_UI_AD_MAF_LMS_OUT = 67,
            /// <summary>Grundfos SCR Error</summary>
            MRW_UI_SCR_ERROR = 68,
            /// <summary>ADBlue tank level</summary>
            MRW_UI_GRUNDFOS_TANKLEVEL = 69,
            /// <summary>Visualisation of pressure alarm</summary>
            MRW_UI_VIEW_P_ALARM = 70,
            /// <summary>Visualisation of pressure alarm time</summary>
            MRW_UI_VIEW_T_ALARM = 71,
            /// <summary>Visualisation of pressure warning</summary>
            MRW_UI_VIEW_P_WARN = 72,
            /// <summary>Visualisation of pressure warning time</summary>
            MRW_UI_VIEW_T_WARN = 73,
            /// <summary>Remaining Additiv in tank</summary>
            MRW_UI_ADDITIV_REST = 74,
            /// <summary>operating voltage</summary>
            MRW_UI_UB = 128,
            /// <summary>minutes since programmed (last 2 bytes)</summary>
            MRW_UI_TIME = 129,
            /// <summary>filter back pressure</summary>
            MRW_UI_P_VOR = 130,
            /// <summary>temperature inside ECU</summary>
            MRW_SI_T_ECU = 131,
            /// <summary>temperature in front of filter</summary>
            MRW_SI_T_VOR = 132,
            /// <summary>temperature after filter</summary>
            MRW_SI_T_NACH = 133,
            /// <summary>load or air mass flow</summary>
            MRW_UI_LAST = 134,
            /// <summary>Clamp15</summary>
            MRW_UI_KL15 = 135,
            /// <summary>D+</summary>
            MRW_UI_D_PLUS = 136,
            /// <summary>ADC value for fuel level</summary>
            MRW_UI_AD_TANK = 137,
            /// <summary>ADC value for former fuel level b</summary>
            MRW_UI_AD_FREE3 = 138,
            /// <summary>ADC value for former unternal supply voltage</summary>
            MRW_UI_AD_FREE2 = 139,
            /// <summary>ADC value for differential filter pressure</summary>
            MRW_UI_AD_P_VOR = 140,
            /// <summary>ADC value for former mosfet temperature</summary>
            MRW_UI_AD_FREE1 = 141,
            /// <summary>ADC value for heat current A</summary>
            MRW_UI_AD_I_HEIZ_A = 142,
            /// <summary>ADC value for operating voltage</summary>
            MRW_UI_AD_UB = 143,
            /// <summary>ADC value for temperature in front of filter</summary>
            MRW_UI_AD_T_VOR = 144,
            /// <summary>ADC value for temperature after filter</summary>
            MRW_UI_AD_T_NACH = 145,
            /// <summary>ADC value for heat current b</summary>
            MRW_UI_AD_I_HEIZ_B = 146,
            /// <summary>ADC value for dosing pump current</summary>
            MRW_UI_AD_I_PUMP = 147,
            /// <summary>ADC value for load signal or air mass sensor</summary>
            MRW_UI_AD_LAST = 148,
            /// <summary>normed heat current a</summary>
            MRW_UI_I_HEIZ_A = 149,
            /// <summary>normed heat current b</summary>
            MRW_UI_I_HEIZ_B = 150,
            /// <summary>normed fuel level 0,00% - 100,00%</summary>
            MRW_UI_TANKINHALT_PROZENT = 151,
            /// <summary>normed fuel level [l]</summary>
            MRW_UI_TANKINHALT_LITER = 152,
            /// <summary>speed [100m/h]</summary>
            MRW_UI_GESCHWINDIGKEIT = 153,
            /// <summary>motor speed [U/min]</summary>
            MRW_UI_DREHZAHL = 154,
            /// <summary>pwm of air mass sensor</summary>
            MRW_UI_LMM_PWM = 155,
            /// <summary>pwm of EGR</summary>
            MRW_UI_AGR_PWM = 156,
            /// <summary>frequency of EGR</summary>
            MRW_UI_AGR_FREQUENZ = 157,
            /// <summary>position for EGR</summary>
            MRW_UI_AGR_POS = 158,
            /// <summary>offset fir EGR</summary>
            MRW_UI_AGR_OFFSET = 159,
            /// <summary>resistance of first temperature sensor</summary>
            MRW_UI_R_T_VOR = 160,
            /// <summary>resistance of second temperature sensor</summary>
            MRW_UI_R_T_NACH = 161,
            /// <summary>startup counter</summary>
            MRW_UI_STARTUP = 162,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_0 = 163,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_1 = 164,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_2 = 165,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_3 = 166,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_4 = 167,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_5 = 168,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_6 = 169,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_7 = 170,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_8 = 171,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_9 = 172,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_10 = 173,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_11 = 174,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_12 = 175,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_13 = 176,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_14 = 177,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_15 = 178,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_16 = 179,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_17 = 180,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_18 = 181,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_19 = 182,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_20 = 183,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_21 = 184,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_22 = 185,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_23 = 186,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_24 = 187,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_25 = 188,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_26 = 189,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_27 = 190,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_28 = 191,
            /// <summary>array for dosing current samples</summary>
            MRW_UI_DOSIERSTROM_29 = 192,
            /// <summary>value 1 from can i.e. turns</summary>
            MRW_UI_CAN_VALUE_1 = 193,
            /// <summary>value 2 from can i.e. speed</summary>
            MRW_UI_CAN_VALUE_2 = 194,
            /// <summary>value 3 from can</summary>
            MRW_UI_CAN_VALUE_3 = 195,
            /// <summary>value 4 from can</summary>
            MRW_UI_CAN_VALUE_4 = 196,
            /// <summary>value 1 from can i.e. turns</summary>
            MRW_UI_CAN_VALUE_5 = 197,
            /// <summary>value 2 from can i.e. speed</summary>
            MRW_UI_CAN_VALUE_6 = 198,
            /// <summary>value 3 from can</summary>
            MRW_UI_CAN_VALUE_7 = 199,
            /// <summary>value 4 from can</summary>
            MRW_UI_CAN_VALUE_8 = 200,
            /// <summary>value 5 from can (signed)</summary>
            MRW_SI_CAN_VALUE_9 = 201,
            /// <summary>value 6 from can (signed)</summary>
            MRW_SI_CAN_VALUE_10 = 202,
            /// <summary>value 5 from can (signed)</summary>
            MRW_SI_CAN_VALUE_11 = 203,
            /// <summary>value 6 from can (signed)</summary>
            MRW_SI_CAN_VALUE_12 = 204,
            /// <summary>value 5 from can (signed)</summary>
            MRW_SI_CAN_VALUE_13 = 205,
            /// <summary>value 6 from can (signed)</summary>
            MRW_SI_CAN_VALUE_14 = 206,
            /// <summary>value 5 from can (signed)</summary>
            MRW_SI_CAN_VALUE_15 = 207,
            /// <summary>value 6 from can (signed)</summary>
            MRW_SI_CAN_VALUE_16 = 208,
            /// <summary>can order register</summary>
            MRW_UI_CAN_ORDER = 209,
            /// <summary>can error register</summary>
            MRW_UI_CAN_ERROR = 210,
            /// <summary>Unused value</summary>
            MRW_UI_FREE_01 = 211,
            /// <summary>temperature normed pressure</summary>
            MRW_UI_PNORM = 212,
            /// <summary>PSoC-Version</summary>
            MRW_UI_PSOC_VER = 213,
            /// <summary>Impact pressure</summary>
            MRW_UI_ADSTAUDRUCK = 214,
            /// <summary>Coolant temperature</summary>
            MRW_SI_CoolantTemp = 215,
            /// <summary>Flow rate of Grundfos pump</summary>
            MRW_UI_MAF_FLOWRATE = 216,
            /// <summary>Status of Grundfos pump</summary>
            MRW_UI_MAF_PUMP_STATUS = 217,
            /// <summary>Absolute ambient pressure</summary>
            MRW_UI_ABSOLUTDRUCK = 218,
            /// <summary>Value for N/A or unused</summary>
            NotAvailable = 255
        }

        /// <summary>Enumeration for data map types</summary>
        public enum KennfeldTyp
        {
            /// <summary>Dummy</summary>
            dummy = 0,
            /// <summary>Maximales Druckmittel pro Betriebsstunde</summary>
            CrtDruckMittelMax = 1,
            /// <summary>Minimales Druckmittel pro Betriebsstunde</summary>
            CrtDruckMittelMin = 2,
            /// <summary>Wirkungsgradkennlinie CRT Temperatur</summary>
            CrtTemperaturWirkungsgrad = 3,
            /// <summary>max. Druckabweichung pro CRT Wirkungsgrad</summary>
            CrtDruckAbweichungMax = 4,
            /// <summary>min. Druckabweichung pro CRT Wirkungsgrad</summary>
            CrtDruckAbweichungMin = 5,
            /// <summary>ulBeladKennfeldID</summary>
            ulBeladKennfeldID = 6,
            /// <summary>ulGegendruckKennfeldI</summary>
            ulGegendruckKennfeldI = 7,
            /// <summary>ulRegentimeKennfeldID</summary>
            ulRegentimeKennfeldID = 8,
            /// <summary>ulBeladungLKFID</summary>
            ulBeladungLKFID = 9,
            /// <summary>ulFreigabeLKFID</summary>
            ulFreigabeLKFID = 10,
            /// <summary>ulKennfeldID</summary>
            ulKennfeldID = 11,
            /// <summary>ulFreigabeKennfeldID</summary>
            ulFreigabeKennfeldID = 12,
            /// <summary>ulKennfeldObenID</summary>
            ulKennfeldObenID = 13,
            /// <summary>ulKennfeldUntenID</summary>
            ulKennfeldUntenID = 14,
            /// <summary>Sensorkennlinie Luftmassenmesser</summary>
            KennlinieLuftmassenmesser = 15,
            /// <summary>Sensorkennlinie Ansaugluftdruck</summary>
            KennlinieAnsaugluftdruck = 16,
            /// <summary>Sensorkennlinie Ansauglufttemperatur</summary>
            KennlinieAnsauglufttemperatur = 17,
            /// <summary>ulKFMafFactor</summary>
            ulKFMafFactor = 18,
            /// <summary>Sensorkennlinie Tankgeber [cl]</summary>
            KennlinieTankgeberCl = 19,
            /// <summary>CAN Sendewerte Dreating</summary>
            SendewerteDreating = 20,
            /// <summary>CAN Sendewerte NOx Sensor</summary>
            SendewerteNoxSensor = 21,
            /// <summary>Sensorkennlinie Tankgeber [liter]</summary>
            KennlinieTankgeberL = 22,
            /// <summary>Kennlinie Staudruck K-Faktor</summary>
            KennlinieKfactor = 23,
            /// <summary>Value for N/A or unused</summary>
            NotAvailable = 255
        }

        /// <summary>Accessors of firmware revision</summary>
        public byte SoftwareRevision
        {
            get { return mSoftwareRevision; }
            set { mSoftwareRevision = value; }
        }

        /// <summary>Default constructor</summary>
        public Firmware()
        {
            mSoftwareRevision = 8;
        }

        /// <summary>Overloaded constructor</summary>
        /// <param name="Revision">Revision = Compatibility of firmware</param>
        public Firmware(byte Revision)
        {
            mSoftwareRevision = Revision;
        }

        /// <summary>Get position of value in table / structure by value identifier</summary>
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
                case 10:
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

        /// <summary>Get number of measured actual values</summary>
        /// <returns>Number of measured actual values</returns>
        public byte GetMeasuredValueNumber()
        {
            byte ret = 0;
            switch (mSoftwareRevision)
            {
                case 8:
                    ret = 80;
                    break;
                case 9:
                case 10:
                    ret = 91;
                    break;
                case 189:
                case 186:
                case 185:
                case 184:
                case 182:
                    ret = 73;
                    break;
                case 174:
                case 172:
                case 167:
                    ret = 72;
                    break;
                case 164:
                    ret = 72;
                    break;
                default:
                    ret = 255;    // hier einmal auslesen?
                    break;
            }
            return ret;
        }

        /// <summary>Get number of actual values</summary>
        /// <returns>Number of actual values</returns>
        public byte GetValueNumber()
        {
            byte ret = 0;
            switch (mSoftwareRevision)
            {
                case 8:
                    ret = 80 + 71;
                    break;
                case 9:
                case 10:
                    ret = 91 + 75;
                    break;
                case 189:
                case 186:
                case 185:
                case 184:
                case 182:
                    ret = 73 + 59;
                    break;
                case 175:
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

        /// <summary>Get identifier of actual value by position as string</summary>
        /// <param name="Position">Position of actual value</param>
        /// <returns>Identifier of actual value as string</returns>
        public string GetMessWertString(int Position)
        {
            string ret = "N/A";
            int id = Position;
            if (Position < GetMeasuredValueNumber())
            {
                id = Position + 128;
            }
            else
            {
                id = Position - GetMeasuredValueNumber();
            }
            switch (mSoftwareRevision)
            {
                case 8:
                    ret = ((HJS.ECU.Firmware.MessWert8)(id)).ToString();
                    break;
                case 9:
                    ret = ((HJS.ECU.Firmware.MessWert9)(id)).ToString();
                    break;
                case 10:
                    ret = ((HJS.ECU.Firmware.MessWert10)(id)).ToString();
                    break;
            }
            return ret;
        }

        /// <summary>Get identifier of actual value by position of compatibility 8 as string</summary>
        /// <param name="Position">Position of actual value of compatibility 8</param>
        /// <returns>Identifier of actual value as string</returns>
        public MessWert8 GetValueIdentifier8(int Position)
        {
            int id = Position;
            if (Position < GetMeasuredValueNumber())
            {
                id = Position + 128;
            }
            else
            {
                id = Position - GetMeasuredValueNumber();
            }
            return (HJS.ECU.Firmware.MessWert8)(id);
        }

        /// <summary>Get identifier of actual value by position of compatibility 9 as string</summary>
        /// <param name="Position">Position of actual value of compatibility 9</param>
        /// <returns>Identifier of actual value as string</returns>
        public MessWert9 GetValueIdentifier9(int Position)
        {
            int id = Position;
            if (Position < GetMeasuredValueNumber())
            {
                id = Position + 128;
            }
            else
            {
                id = Position - GetMeasuredValueNumber();
            }
            return (HJS.ECU.Firmware.MessWert9)(id);
        }

        /// <summary>Get identifier of actual value by position of compatibility 10 as string</summary>
        /// <param name="Position">Position of actual value of compatibility 10</param>
        /// <returns>Identifier of actual value as string</returns>
        public MessWert10 GetValueIdentifier10(int Position)
        {
            int id = Position;
            if (Position < GetMeasuredValueNumber())
            {
                id = Position + 128;
            }
            else
            {
                id = Position - GetMeasuredValueNumber();
            }
            return (HJS.ECU.Firmware.MessWert10)(id);
        }

        /// <summary>Check if current software version is updateable
        /// If PSoC version 8 is on ECU, the ECU can be updated</summary>
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

        /// <summary>Upgrade value of MessWert enumeration from compatibility 8 to 9</summary>
        /// <param name="mrw8">Value of enumeration in compatibility 8 as byte</param>
        /// <returns>Value of enumeration in compatibility 9 as byte</returns>
        public static byte UpgradeMessWert8to9(byte mrw8)
        {
            byte mrw9 = 255;

            if (mrw8 > 127)
            { // Messwert
                if (mrw8 < 208)
                {
                    mrw9 = mrw8;
                    if (mrw8 > 196) mrw9 = (byte)(mrw9 + 4); // nach mrw_ui_can_value_4 noch 4 weitere ui_can_value
                    if (mrw8 > 198) mrw9 = (byte)(mrw9 + 6); // nach mrw_si_can_value_5 & 6 noch 6 weitere si_can_value
                }
            }
            else
            { // Rechenwert
                mrw9 = mrw8;
                if (mrw8 > 8) mrw9++; // mrw_ui_tankinhalt 16-bit in 32-bit erweitert 
                if (mrw8 > 9) mrw9++; // mrw_ui_tankinhalt_mittel 16-bit in 32-bit erweitert 
                if (mrw8 > 47) mrw9++; // mrw_ui_tankmenge_gesamt 16-bit in 32-bit erweitert 
            }

            return mrw9;
        }

        /// <summary>Check plausibility of value flags
        /// Check for signed, group and decimals (incl. hex)</summary>
        /// <param name="mrw">Value Identifier</param>
        /// <param name="flags">Flag byte</param>
        /// <returns>True if flags are plausible</returns>
        public static bool CheckMesswertFlag(MessWert8 mrw, byte flags)
        {
            bool ret = false;

            byte DisplayMask = 0x1F; // Hidden und LCD Flag ausmaskieren
            byte SignedMask = 0xF7;
            byte GroupMask = 0xEF;
            byte DecimalsMask = 0xF8;

            switch (mrw)
            {
                case MessWert8.MRW_ULL_DOSIERIMPULSE_TANK:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_ULH_DOSIERIMPULSE_TANK:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_ULL_DOSIERIMPULSE_FILTER:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_ULH_DOSIERIMPULSE_FILTER:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_ULL_DOSIERIMPULSE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_ULH_DOSIERIMPULSE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_ADDITIV_IST:
                    if ((flags & (DisplayMask & GroupMask)) == 3) ret = true;
                    break;
                case MessWert8.MRW_UI_ADDITIV_SOLL:
                    if ((flags & (DisplayMask & GroupMask)) == 3) ret = true;
                    break;
                case MessWert8.MRW_UI_TANKINHALT:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert8.MRW_UI_TANKINHALT_MITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert8.MRW_UI_GEGENDRUCKMITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_KILOMETER:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_RUSSMENGE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_ULL_BELADUNGSSUMME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_ULH_BELADUNGSSUMME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_REGTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_HEIZLEISTUNG_SOLL:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_HEIZLEISTUNG_IST_A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_HEIZLEISTUNG_IST_B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_HEIZLEISTUNG_IST_100A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_HEIZLEISTUNG_IST_100B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_SHS_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert8.MRW_UI_STROM_A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_STROM_B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_RI_A:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert8.MRW_UI_RI_B:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert8.MRW_UI_HEIZ_PWM_A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_HEIZ_PWM_B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_IMPULSTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_MIN_I_TIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert8.MRW_SI_IST_STROM:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_SI_MAX_STROM:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_SI_DIDT_MIN:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_DEBUG5:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_DEBUG6:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_DEBUG7:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_REGWUNSCH:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_BELADUNG_REGBAR:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_REG_SPERRZEIT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_PASSWD0:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_PASSWD1:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_PASSWD2:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_PASSWD3:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_OPERATIONTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_LUFTMENGE:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_REINIGUNGS_INTERVALL:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_TANKMENGE_GESAMT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_CRTDRUCKMITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_CRTWIRKUNGMITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_SI_DELTA_JOULE_A:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_SI_DELTA_JOULE_B:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_SAVE_VALUE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_REG_STARTS:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_ULL_COMPLETE_JOULE:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_ULH_COMPLETE_JOULE:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DERATING_LEVEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_KRAFTSTOFFVERBRAUCH:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERINTERVALL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_FILTER_RUNTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_ICD_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert8.MRW_UI_MAF_AIR_PESS:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_SI_MAF_AIR_TEMP:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_MAF_LMS_OUT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_MAF_LMS_OUT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_SCR_ERROR:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_GRUNDFOS_TANKLEVEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_VIEW_P_ALARM:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_VIEW_T_ALARM:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_VIEW_P_WARN:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_VIEW_T_WARN:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_UB:
                    if ((flags & (DisplayMask)) == 3) ret = true;
                    break;
                case MessWert8.MRW_UI_TIME:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_P_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_SI_T_ECU:
                    if ((flags & (DisplayMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_SI_T_VOR:
                    if ((flags & (DisplayMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_SI_T_NACH:
                    if ((flags & (DisplayMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_LAST:
                    if ((flags & (DisplayMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_KL15:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_D_PLUS:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_TANK:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_FREE3:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_FREE2:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_P_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_FREE1:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_I_HEIZ_A:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_UB:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_T_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_T_NACH:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_I_HEIZ_B:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_I_PUMP:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AD_LAST:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_I_HEIZ_A:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_I_HEIZ_B:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_TANKINHALT_PROZENT:
                    if ((flags & (DisplayMask)) == 2) ret = true;
                    break;
                case MessWert8.MRW_UI_TANKINHALT_LITER:
                    if ((flags & (DisplayMask)) == 2) ret = true;
                    break;
                case MessWert8.MRW_UI_GESCHWINDIGKEIT:
                    if ((flags & (DisplayMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_DREHZAHL:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_LMM_PWM:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AGR_PWM:
                    if ((flags & (DisplayMask)) == 1) ret = true;
                    break;
                case MessWert8.MRW_UI_AGR_FREQUENZ:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AGR_POS:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_AGR_OFFSET:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_R_T_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_R_T_NACH:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_STARTUP:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_0:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_1:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_2:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_3:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_4:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_5:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_6:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_7:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_8:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_9:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_10:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_11:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_12:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_13:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_14:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_15:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_16:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_17:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_18:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_19:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_20:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_21:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_22:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_23:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_24:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_25:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_26:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_27:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_28:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_DOSIERSTROM_29:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_CAN_VALUE_1:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_CAN_VALUE_2:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_CAN_VALUE_3:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_CAN_VALUE_4:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_SI_CAN_VALUE_5:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_SI_CAN_VALUE_6:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_CAN_ORDER:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_CAN_ERROR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_WAKEUP_CTR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_PNORM:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_PSOC_VER:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_ADSTAUDRUCK:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_SI_CoolantTemp:
                    if ((flags & (DisplayMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_MAF_FLOWRATE:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert8.MRW_UI_MAF_PUMP_STATUS:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
            }
            return ret;
        }

        /// <summary>Check plausibility of value flags
        /// Check for signed, group and decimals (incl. hex)</summary>
        /// <param name="mrw">Value Identifier</param>
        /// <param name="flags">Flag byte</param>
        /// <returns>True if flags are plausible</returns>
        public static bool CheckMesswertFlag(MessWert9 mrw, byte flags)
        {
            bool ret = false;

            byte DisplayMask = 0x1F; // Hidden und LCD Flag ausmaskieren
            byte SignedMask = 0xF7;
            byte GroupMask = 0xEF;
            byte DecimalsMask = 0xF8;

            switch (mrw)
            {
                case MessWert9.MRW_ULL_DOSIERIMPULSE_TANK:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULH_DOSIERIMPULSE_TANK:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULL_DOSIERIMPULSE_FILTER:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULH_DOSIERIMPULSE_FILTER:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULL_DOSIERIMPULSE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULH_DOSIERIMPULSE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_ADDITIV_IST:
                    if ((flags & (DisplayMask & GroupMask)) == 3) ret = true;
                    break;
                case MessWert9.MRW_UI_ADDITIV_SOLL:
                    if ((flags & (DisplayMask & GroupMask)) == 3) ret = true;
                    break;
                case MessWert9.MRW_ULL_TANKINHALT:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert9.MRW_ULH_TANKINHALT:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULL_TANKINHALT_MITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert9.MRW_ULH_TANKINHALT_MITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_GEGENDRUCKMITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_KILOMETER:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_RUSSMENGE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULL_BELADUNGSSUMME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULH_BELADUNGSSUMME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_REGTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_HEIZLEISTUNG_SOLL:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_HEIZLEISTUNG_IST_A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_HEIZLEISTUNG_IST_B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_HEIZLEISTUNG_IST_100A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_HEIZLEISTUNG_IST_100B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_SHS_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert9.MRW_UI_STROM_A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_STROM_B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_RI_A:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert9.MRW_UI_RI_B:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert9.MRW_UI_HEIZ_PWM_A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_HEIZ_PWM_B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_IMPULSTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_MIN_I_TIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert9.MRW_SI_IST_STROM:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_MAX_STROM:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_DIDT_MIN:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_DEBUG5:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_DEBUG6:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert9.MRW_DEBUG7:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_REGWUNSCH:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_BELADUNG_REGBAR:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_REG_SPERRZEIT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_PASSWD0:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_PASSWD1:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_PASSWD2:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_PASSWD3:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_OPERATIONTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_LUFTMENGE:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_REINIGUNGS_INTERVALL:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULL_TANKMENGE_GESAMT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULH_TANKMENGE_GESAMT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CRTDRUCKMITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CRTWIRKUNGMITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_DELTA_JOULE_A:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_SI_DELTA_JOULE_B:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_SAVE_VALUE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_REG_STARTS:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_ULL_COMPLETE_JOULE:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_ULH_COMPLETE_JOULE:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DERATING_LEVEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_KRAFTSTOFFVERBRAUCH:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERINTERVALL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_FILTER_RUNTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_ICD_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert9.MRW_UI_MAF_AIR_PESS:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_MAF_AIR_TEMP:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_MAF_LMS_OUT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_MAF_LMS_OUT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_SCR_ERROR:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_GRUNDFOS_TANKLEVEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_VIEW_P_ALARM:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_VIEW_T_ALARM:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_VIEW_P_WARN:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_VIEW_T_WARN:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_ADDITIV_REST:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_UB:
                    if ((flags & (DisplayMask)) == 3) ret = true;
                    break;
                case MessWert9.MRW_UI_TIME:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_P_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_T_ECU:
                    if ((flags & (DisplayMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_T_VOR:
                    if ((flags & (DisplayMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_SI_T_NACH:
                    if ((flags & (DisplayMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_LAST:
                    if ((flags & (DisplayMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_KL15:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_D_PLUS:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_TANK:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_FREE3:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_FREE2:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_P_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_FREE1:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_I_HEIZ_A:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_UB:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_T_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_T_NACH:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_I_HEIZ_B:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_I_PUMP:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AD_LAST:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_I_HEIZ_A:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_I_HEIZ_B:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_TANKINHALT_PROZENT:
                    if ((flags & (DisplayMask)) == 2) ret = true;
                    break;
                case MessWert9.MRW_UI_TANKINHALT_LITER:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_GESCHWINDIGKEIT:
                    if ((flags & (DisplayMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_DREHZAHL:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_LMM_PWM:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AGR_PWM:
                    if ((flags & (DisplayMask)) == 1) ret = true;
                    break;
                case MessWert9.MRW_UI_AGR_FREQUENZ:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AGR_POS:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_AGR_OFFSET:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_R_T_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_R_T_NACH:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_STARTUP:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_0:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_1:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_2:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_3:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_4:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_5:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_6:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_7:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_8:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_9:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_10:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_11:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_12:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_13:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_14:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_15:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_16:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_17:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_18:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_19:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_20:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_21:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_22:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_23:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_24:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_25:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_26:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_27:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_28:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_DOSIERSTROM_29:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_VALUE_1:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_VALUE_2:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_VALUE_3:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_VALUE_4:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_VALUE_5:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_VALUE_6:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_VALUE_7:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_VALUE_8:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_CAN_VALUE_9:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_CAN_VALUE_10:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_CAN_VALUE_11:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_CAN_VALUE_12:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_CAN_VALUE_13:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_CAN_VALUE_14:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_CAN_VALUE_15:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_CAN_VALUE_16:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_ORDER:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_CAN_ERROR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_WAKEUP_CTR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_PNORM:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_PSOC_VER:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_ADSTAUDRUCK:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_SI_CoolantTemp:
                    if ((flags & (DisplayMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_MAF_FLOWRATE:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_MAF_PUMP_STATUS:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert9.MRW_UI_ABSOLUTDRUCK:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
            }
            return ret;
        }

        /// <summary>Check plausibility of value flags
        /// Check for signed, group and decimals (incl. hex)</summary>
        /// <param name="mrw">Value Identifier</param>
        /// <param name="flags">Flag byte</param>
        /// <returns>True if flags are plausible</returns>
        public static bool CheckMesswertFlag(MessWert10 mrw, byte flags)
        {
            bool ret = false;

            byte DisplayMask = 0x1F; // Hidden und LCD Flag ausmaskieren
            byte SignedMask = 0xF7;
            byte GroupMask = 0xEF;
            byte DecimalsMask = 0xF8;

            switch (mrw)
            {
                case MessWert10.MRW_ULL_DOSIERIMPULSE_TANK:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULH_DOSIERIMPULSE_TANK:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULL_DOSIERIMPULSE_FILTER:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULH_DOSIERIMPULSE_FILTER:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULL_DOSIERIMPULSE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULH_DOSIERIMPULSE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_ADDITIV_IST:
                    if ((flags & (DisplayMask & GroupMask)) == 3) ret = true;
                    break;
                case MessWert10.MRW_UI_ADDITIV_SOLL:
                    if ((flags & (DisplayMask & GroupMask)) == 3) ret = true;
                    break;
                case MessWert10.MRW_ULL_TANKINHALT:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert10.MRW_ULH_TANKINHALT:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULL_TANKINHALT_MITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert10.MRW_ULH_TANKINHALT_MITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_GEGENDRUCKMITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_KILOMETER:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_RUSSMENGE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULL_BELADUNGSSUMME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULH_BELADUNGSSUMME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_REGTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_HEIZLEISTUNG_SOLL:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_HEIZLEISTUNG_IST_A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_HEIZLEISTUNG_IST_B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_HEIZLEISTUNG_IST_100A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_HEIZLEISTUNG_IST_100B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_SHS_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert10.MRW_UI_STROM_A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_STROM_B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_RI_A:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert10.MRW_UI_RI_B:
                    if ((flags & (DisplayMask & GroupMask)) == 2) ret = true;
                    break;
                case MessWert10.MRW_UI_HEIZ_PWM_A:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_HEIZ_PWM_B:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_IMPULSTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_MIN_I_TIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert10.MRW_SI_IST_STROM:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_MAX_STROM:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_DIDT_MIN:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_ECU_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_DEBUG6:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert10.MRW_DEBUG7:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_REGWUNSCH:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_BELADUNG_REGBAR:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_REG_SPERRZEIT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_PASSWD0:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_PASSWD1:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_PASSWD2:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_PASSWD3:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_OPERATIONTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_LUFTMENGE:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_REINIGUNGS_INTERVALL:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULL_TANKMENGE_GESAMT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULH_TANKMENGE_GESAMT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CRTDRUCKMITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CRTWIRKUNGMITTEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_DELTA_JOULE_A:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_SI_DELTA_JOULE_B:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_SAVE_VALUE:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_REG_STARTS:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_ULL_COMPLETE_JOULE:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_ULH_COMPLETE_JOULE:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DERATING_LEVEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_KRAFTSTOFFVERBRAUCH:
                    if ((flags & (DisplayMask & GroupMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERINTERVALL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_FILTER_RUNTIME:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_ICD_STATUS:
                    if ((flags & (DisplayMask & GroupMask)) == 7) ret = true;
                    break;
                case MessWert10.MRW_UI_MAF_AIR_PESS:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_MAF_AIR_TEMP:
                    if ((flags & (DisplayMask & GroupMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_MAF_LMS_OUT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_MAF_LMS_OUT:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_SCR_ERROR:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_GRUNDFOS_TANKLEVEL:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_VIEW_P_ALARM:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_VIEW_T_ALARM:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_VIEW_P_WARN:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_VIEW_T_WARN:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_ADDITIV_REST:
                    if ((flags & (DisplayMask & GroupMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_UB:
                    if ((flags & (DisplayMask)) == 3) ret = true;
                    break;
                case MessWert10.MRW_UI_TIME:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_P_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_T_ECU:
                    if ((flags & (DisplayMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_T_VOR:
                    if ((flags & (DisplayMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_SI_T_NACH:
                    if ((flags & (DisplayMask & SignedMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_LAST:
                    if ((flags & (DisplayMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_KL15:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_D_PLUS:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_TANK:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_FREE3:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_FREE2:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_P_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_FREE1:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_I_HEIZ_A:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_UB:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_T_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_T_NACH:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_I_HEIZ_B:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_I_PUMP:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AD_LAST:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_I_HEIZ_A:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_I_HEIZ_B:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_TANKINHALT_PROZENT:
                    if ((flags & (DisplayMask)) == 2) ret = true;
                    break;
                case MessWert10.MRW_UI_TANKINHALT_LITER:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_GESCHWINDIGKEIT:
                    if ((flags & (DisplayMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_DREHZAHL:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_LMM_PWM:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AGR_PWM:
                    if ((flags & (DisplayMask)) == 1) ret = true;
                    break;
                case MessWert10.MRW_UI_AGR_FREQUENZ:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AGR_POS:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_AGR_OFFSET:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_R_T_VOR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_R_T_NACH:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_STARTUP:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_0:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_1:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_2:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_3:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_4:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_5:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_6:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_7:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_8:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_9:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_10:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_11:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_12:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_13:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_14:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_15:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_16:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_17:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_18:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_19:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_20:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_21:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_22:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_23:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_24:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_25:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_26:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_27:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_28:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_DOSIERSTROM_29:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_VALUE_1:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_VALUE_2:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_VALUE_3:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_VALUE_4:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_VALUE_5:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_VALUE_6:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_VALUE_7:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_VALUE_8:
                    if ((flags & (DisplayMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_CAN_VALUE_9:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_CAN_VALUE_10:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_CAN_VALUE_11:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_CAN_VALUE_12:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_CAN_VALUE_13:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_CAN_VALUE_14:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_CAN_VALUE_15:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_CAN_VALUE_16:
                    if ((flags & (DisplayMask & SignedMask & DecimalsMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_ORDER:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_CAN_ERROR:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_FREE_01:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_PNORM:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_PSOC_VER:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_ADSTAUDRUCK:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_SI_CoolantTemp:
                    if ((flags & (DisplayMask & SignedMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_MAF_FLOWRATE:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_MAF_PUMP_STATUS:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
                case MessWert10.MRW_UI_ABSOLUTDRUCK:
                    if ((flags & (DisplayMask)) == 0) ret = true;
                    break;
            }
            return ret;
        }

        /// <summary>Get number of bytes for all task stacks</summary>
        /// <param name="Revision">Compatiblity revision</param>
        /// <returns>Number of bytes for all task stacks</returns>
        public static UInt32 GetTotalStackSize(byte Revision)
        {
            UInt32 ret = 0;
            switch (Revision)
            {
                case 8:
                case 9:
                case 10:
                    ret = 11264;
                    break;
            }
            return ret;
        }

        /// <summary>Get default error number</summary>
        /// <param name="task">Task identifier</param>
        /// <param name="position">Task error position (0..6)</param>
        /// <returns>Default Error number (1..63), or 0 for not used, or 255 for obsolete = any number allowed</returns>
        public byte GetTaskError(TaskIdentifier task, int position)
        {
            byte ret = 0;
            switch (task)
            {
                case TaskIdentifier.taskPostDiagnose:
                    if (position == 0) ret = 255;
                    if (position == 1) ret = 53;
                    if (position == 2) ret = 54;
                    if (position == 3) ret = 55;
                    if (position == 4) ret = 62;
                    if (position == 5) ret = 63;
                    break;
                case TaskIdentifier.taskTankgeber:
                    if (position == 0) ret = 1;
                    if (position == 1) ret = 2;
                    if (position == 2) ret = 3;
                    if (position == 3) ret = 11;
                    if (position == 4) ret = 19;
                    if (position == 5) ret = 255;
                    break;
                case TaskIdentifier.taskKommunikation:
                    if (position == 0) ret = 47;
                    if (position == 1) ret = 62;
                    if (position == 2) ret = 62;
                    break;
                case TaskIdentifier.taskIo:
                    if (position == 0) ret = 30;
                    if (position == 1) ret = 31;
                    if (position == 2) ret = 34;
                    if (position == 3) ret = 33;
                    if (position == 4) ret = 32;
                    if (position == 5) ret = 32;
                    if (position == 6) ret = 58;
                    break;
                case TaskIdentifier.taskDosieren:
                    if (position == 0) ret = 4;
                    if (position == 1) ret = 5;
                    if (position == 2) ret = 6;
                    if (position == 3) ret = 7;
                    if (position == 4) ret = 8;
                    if (position == 5) ret = 9;
                    if (position == 6) ret = 10;
                    break;
                case TaskIdentifier.taskAdditivierung:
                    if (position == 0) ret = 50;
                    if (position == 1) ret = 51;
                    if (position == 2) ret = 52;
                    if (position == 3) ret = 42;
                    break;
                case TaskIdentifier.taskVertWatch:
                    if (position == 0) ret = 56;
                    if (position == 1) ret = 57;
                    if (position == 2) ret = 37;
                    if (position == 3) ret = 38;
                    if (position == 4) ret = 36;
                    if (position == 5) ret = 62;
                    if ((position == 6) && (mSoftwareRevision > 8)) ret = 59;
                    break;
                case TaskIdentifier.taskBeladPro:
                    if (position == 0) ret = 19;
                    if (position == 1) ret = 59;
                    if (position == 2) ret = 255;
                    if (position == 3) ret = 43;
                    if (position == 4) ret = 15;
                    if (position == 5) ret = 13;
                    break;
                case TaskIdentifier.taskCanIn:
                    if (position == 0) ret = 12;
                    if (position == 1) ret = 255;
                    if (position == 2) ret = 62;
                    if ((position == 3) && (mSoftwareRevision > 9)) ret = 61;
                    if ((position == 4) && (mSoftwareRevision > 9)) ret = 61;
                    if ((position == 5) && (mSoftwareRevision > 9)) ret = 61;
                    if ((position == 6) && (mSoftwareRevision > 9)) ret = 61;
                    break;
                case TaskIdentifier.taskBeladMittel:
                    if (position == 0) ret = 20;
                    if (position == 1) ret = 21;
                    if (position == 2) ret = 23;
                    if (position == 3) ret = 24;
                    if (position == 4) ret = 44;
                    break;
                case TaskIdentifier.taskTurnspeed:
                    if (position == 1) ret = 42;
                    if (position == 2) ret = 42;
                    break;
                case TaskIdentifier.taskBeladKennfeld:
                    if (position == 4) ret = 14;
                    if (position == 5) ret = 14;
                    if (position == 6) ret = 36;
                    break;
                case TaskIdentifier.taskRegenerieren:
                    if (position == 0) ret = 255;
                    if (position == 1) ret = 48;
                    if (position == 2) ret = 49;
                    if ((position == 3) && (mSoftwareRevision > 8)) ret = 60;
                    if (position == 4) ret = 41;
                    if (position == 5) ret = 60;
                    if (position == 6) ret = 22;
                    break;
                case TaskIdentifier.taskHeizen:
                    if (position == 0) ret = 25;
                    if (position == 1) ret = 27;
                    if (position == 2) ret = 27;
                    if (position == 3) ret = 26;
                    if (position == 4) ret = 40;
                    if (position == 5) ret = 18;
                    break;
                case TaskIdentifier.taskAgr:
                    if (position == 0) ret = 18;
                    break;
                case TaskIdentifier.taskDrivePattern:
                    if (position == 3) ret = 17;
                    break;
                case TaskIdentifier.taskBeladLuftmasse:
                    if (position == 0) ret = 14;
                    if (position == 1) ret = 43;
                    if (position == 2) ret = 13;
                    if (position == 3) ret = 19;
                    if (position == 4) ret = 15;
                    if (position == 5) ret = 36;
                    break;
                case TaskIdentifier.taskBeladCRT:
                    if (position == 0) ret = 36;
                    if (position == 1) ret = 44;
                    if (position == 2) ret = 35;
                    if (position == 3) ret = 45;
                    if (position == 4) ret = 46;
                    if (position == 5) ret = 21;
                    if (position == 6) ret = 19;
                    break;
                case TaskIdentifier.taskIcDosing:
                    if (position == 0) ret = 29;
                    break;
                case TaskIdentifier.taskMassAirFlow:
                    if (position == 0) ret = 16;
                    if (position == 1) ret = 61;
                    if (position == 2) ret = 19;
                    if (position == 3) ret = 18;
                    break;
                case TaskIdentifier.taskGrundfos:
                    if (position == 0) ret = 41;
                    if (position == 1) ret = 48;
                    if (position == 2) ret = 49;
                    if (position == 3) ret = 19;
                    break;
                case TaskIdentifier.taskStaudruck:
                    if (position == 0) ret = 30;
                    if (position == 1) ret = 12;
                    if (position == 2) ret = 58;
                    break;
                case TaskIdentifier.taskPreDiagnose:
                case TaskIdentifier.taskAcquisition:
                case TaskIdentifier.taskCanCom:
                case TaskIdentifier.taskSaeComm:
                case TaskIdentifier.taskAplSae:
                case TaskIdentifier.taskScrHeiz:
                case TaskIdentifier.taskCAN2Com:
                case TaskIdentifier.taskSelfRegeneration:
                case TaskIdentifier.taskXCP:
                case TaskIdentifier.taskInvalid:
                    // No errors or ignore
                    break;
            }
            return ret;
        }

        /// <summary>Get value signed flag from Firmware</summary>
        /// <param name="Position">Position in value array from ECU</param>
        /// <returns>True if value is signed</returns>
        public bool IsValueSigned(int Position)
        {
            bool bRet = false;

            switch (mSoftwareRevision)
            {
                case 8:
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_IST_STROM)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_MAX_STROM)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_DIDT_MIN)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_DELTA_JOULE_A)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_DELTA_JOULE_B)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_MAF_AIR_TEMP)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_T_ECU)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_T_VOR)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_T_NACH)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_CAN_VALUE_5)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_CAN_VALUE_6)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert8.MRW_SI_CoolantTemp)) bRet = true;
                    break;
                case 9:
                case 10:
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_IST_STROM)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_MAX_STROM)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_DIDT_MIN)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_DELTA_JOULE_A)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_DELTA_JOULE_B)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_MAF_AIR_TEMP)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_T_ECU)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_T_VOR)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_T_NACH)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_CAN_VALUE_9)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_CAN_VALUE_10)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_CAN_VALUE_11)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_CAN_VALUE_12)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_CAN_VALUE_13)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_CAN_VALUE_14)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_CAN_VALUE_15)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_CAN_VALUE_16)) bRet = true;
                    if (Position == GetValuePosition((byte)MessWert9.MRW_SI_CoolantTemp)) bRet = true;
                    break;
            }

            return bRet;
        }

        //rtc: ab 1.7.8, aenderung in 1.x.9
    }
}
