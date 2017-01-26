/*
 * Object: HJS.ReturnValue
 * Description: Enumeration for return values inside entire hjs name space
 * 
 * $LastChangedDate: 2013-12-17 09:02:02 +0100 (Di, 17 Dez 2013) $
 * $LastChangedRevision: 28 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/HJS.ECU/ReturnValue.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
namespace HJS
{
    /// <summary>Enumeration for return values</summary>
    public enum ReturnValue : byte
    {
        /// <summary>No Error occurred, communication successfully</summary>
        NoError = 0,
        /// <summary>Communication failed, please retry</summary>
        Retry,
        /// <summary>Could not communicate, no port opened</summary>
        PortNotOpened,
        /// <summary>Port already in use</summary>
        PortInUse,
        /// <summary>Timeout / No answer</summary>
        TimeOut,
        /// <summary>Protocol version mismatch</summary>
        VersionMismatch,
        /// <summary>Password error</summary>
        PasswordFailed,
        /// <summary>Data checksum error</summary>
        ChecksumMismatch,
        /// <summary>Could not read or parse language block</summary>
        LanguageNotValid,
        /// <summary>Source and target size mismatch</summary>
        SizeMismatch,
        /// <summary>Block not found in memory or file</summary>
        BlockNotFound,
        /// <summary>Block header is invalid</summary>
        BlockHeaderInvalid,
        /// <summary>Block already exists in file</summary>
        BlockAlreadyExists,
        /// <summary>File not found</summary>
        FileNotFound,
        /// <summary>Invalid file or file type</summary>
        InvalidFile,
        /// <summary>File is empty</summary>
        FileEmpty,
        /// <summary>Interface busy, another thread is using the interface</summary>
        ThreadingBusy,
        /// <summary>Could not execute ECU command</summary>
        ComOrderFailed,
        /// <summary>File read error</summary>
        FileReadError,
        /// <summary>File write error</summary>
        FileWriteError,
        /// <summary>Port does not exist, or has IO errors</summary>
        InvalidPort,
        /// <summary>Port busy for an other command</summary>
        PortBusy,
        /// <summary>Mode of ECU not matching STB</summary>
        ComOrderModeFailed
    }
}
