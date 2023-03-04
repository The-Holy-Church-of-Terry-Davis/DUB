using System;
using System.Runtime.InteropServices;

#region A couple very basic things
namespace System
{
    public class Object
    {
#pragma warning disable 169
        // The layout of object is a contract with the compiler.
        private IntPtr m_pMethodTable;
#pragma warning restore 169
    }
    public struct Void { }

    // The layout of primitive types is special cased because it would be recursive.
    // These really don't need any fields to work.
    public struct Boolean { }
    public struct Char { }
    public struct SByte { }
    public struct Byte { }
    public struct Int16 { }
    public struct UInt16 { }
    public struct Int32 { }
    public struct UInt32 { }
    public struct Int64 { }
    public struct UInt64 { }
    public struct IntPtr { }
    public struct UIntPtr { }
    public struct Single { }
    public struct Double { }

    public abstract class ValueType { }
    public abstract class Enum : ValueType { }

    public struct Nullable<T> where T : struct { }

    public sealed class String 
    { 
        public readonly int Length;
    }

    public abstract class Array { }
    public abstract class Delegate { }
    public abstract class MulticastDelegate : Delegate { }

    public struct RuntimeTypeHandle { }
    public struct RuntimeMethodHandle { }
    public struct RuntimeFieldHandle { }

    public class Attribute { }

    public enum AttributeTargets { }

    public sealed class AttributeUsageAttribute : Attribute
    {
        public AttributeUsageAttribute(AttributeTargets validOn) { }
        public bool AllowMultiple { get; set; }
        public bool Inherited { get; set; }
    }

    public class AppContext
    {
        public static void SetData(string s, object o) { }
    }

    namespace Runtime.CompilerServices
    {
        public class RuntimeHelpers
        {
            public static unsafe int OffsetToStringData => sizeof(IntPtr) + sizeof(int);
        }

        public static class RuntimeFeature
        {
            public const string UnmanagedSignatureCallingConvention = nameof(UnmanagedSignatureCallingConvention);
        }
    }
}

namespace System.Runtime.InteropServices
{
    public class UnmanagedType { }

    sealed class StructLayoutAttribute : Attribute
    {
        public StructLayoutAttribute(LayoutKind layoutKind)
        {
        }
    }

    internal enum LayoutKind
    {
        Sequential = 0, // 0x00000008,
        Explicit = 2, // 0x00000010,
        Auto = 3, // 0x00000000,
    }

    internal enum CharSet
    {
        None = 1,       // User didn't specify how to marshal strings.
        Ansi = 2,       // Strings should be marshalled as ANSI 1 byte chars.
        Unicode = 3,    // Strings should be marshalled as Unicode 2 byte chars.
        Auto = 4,       // Marshal Strings in the right way for the target system.
    }
}
#endregion

#region Things needed by ILC
namespace System
{
    namespace Runtime
    {
        internal sealed class RuntimeExportAttribute : Attribute
        {
            public RuntimeExportAttribute(string entry) { }
        }
    }

    class Array<T> : Array { }
}

namespace Internal.Runtime.CompilerHelpers
{
    using System.Runtime;

    // A class that the compiler looks for that has helpers to initialize the
    // process. The compiler can gracefully handle the helpers not being present,
    // but the class itself being absent is unhandled. Let's add an empty class.
    class StartupCodeHelpers
    {
        // A couple symbols the generated code will need we park them in this class
        // for no particular reason. These aid in transitioning to/from managed code.
        // Since we don't have a GC, the transition is a no-op.
        [RuntimeExport("RhpReversePInvoke")]
        static void RhpReversePInvoke(IntPtr frame) { }
        [RuntimeExport("RhpReversePInvokeReturn")]
        static void RhpReversePInvokeReturn(IntPtr frame) { }
        [RuntimeExport("RhpPInvoke")]
        static void RhpPInvoke(IntPtr frame) { }
        [RuntimeExport("RhpPInvokeReturn")]
        static void RhpPInvokeReturn(IntPtr frame) { }

        [RuntimeExport("RhpFallbackFailFast")]
        static void RhpFallbackFailFast() { while (true) ; }
    }
}
#endregion

namespace EFI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EFI_HANDLE
    {
        private IntPtr _handle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe readonly struct EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL
    {
        private readonly IntPtr _pad;

        public readonly delegate* unmanaged<void*, char*, void*> OutputString;
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct EFI_TABLE_HEADER
    {
        public readonly ulong Signature;
        public readonly uint Revision;
        public readonly uint HeaderSize;
        public readonly uint Crc32;
        public readonly uint Reserved;
    }


    [StructLayout(LayoutKind.Sequential)]
    public unsafe readonly struct EFI_SYSTEM_TABLE
    {
        public readonly EFI_TABLE_HEADER Hdr;
        public readonly char* FirmwareVendor;
        public readonly uint FirmwareRevision;
        public readonly EFI_HANDLE ConsoleInHandle;
        public readonly EFI_SIMPLE_TEXT_INPUT_PROTOCOL* ConIn;
        public readonly EFI_HANDLE ConsoleOutHandle;
        public readonly EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* ConOut;
    }
}


namespace EFI
{
    public enum KeyShiftState : uint
    {
        EFI_SHIFT_STATE_VALID = 0x80000000,

        EFI_RIGHT_SHIFT_PRESSED = 0x00000001,
        EFI_LEFT_SHIFT_PRESSED = 0x00000002,

        EFI_RIGHT_CONTROL_PRESSED = 0x00000004,
        EFI_LEFT_CONTROL_PRESSED = 0x00000008,

        EFI_RIGHT_ALT_PRESSED = 0x00000010,
        EFI_LEFT_ALT_PRESSED = 0x00000020,

        EFI_RIGHT_LOGO_PRESSED = 0x00000040,
        EFI_LEFT_LOGO_PRESSED = 0x00000080,

        EFI_MENU_KEY_PRESSED = 0x00000100,
        EFI_SYS_REQ_PRESSED = 0x00000200
    }

    public enum EFI_KEY_TOGGLE_STATE : byte
    {
        EFI_TOGGLE_STATE_VALID = 0x80,

        /// <summary>
        /// If enabled the instance of EFI_SIMPLE_INPUT_EX_PROTOCOL that returned this supports the ability to return partial keystrokes.
        /// <para>Therefore the ReadKeyStrokeEx function will allow the return of incomplete keystrokes such as the holding down of certain</para>
        /// <para>keys which are expressed as a part of KeyState when there is no Key data.</para>
        /// </summary>
        EFI_KEY_STATE_EXPOSED = 0x40, 
        EFI_SCROLL_LOCK_ACTIVE = 0x01, 
        EFI_NUM_LOCK_ACTIVE = 0x02, 
        EFI_CAPS_LOCK_ACTIVE = 0x04
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct EFI_KEY_STATE
    {
        //Reflects the currently pressed shift modifiers for the input device. The returned value is valid only if the high order bit has been set.
        public readonly KeyShiftState KeyShiftState;
        //Reflects the current internal state of various toggled attributes. The returned value is valid only if the high order bit has been set.
        public readonly EFI_KEY_TOGGLE_STATE KeyToggleState;

        public EFI_KEY_STATE(KeyShiftState shiftState, EFI_KEY_TOGGLE_STATE toggleState)
        {
            KeyShiftState = shiftState;
            KeyToggleState = toggleState;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct EFI_INPUT_KEY
    {
        private readonly ushort ScanCode;
        public readonly char UnicodeChar;

        public EFI_INPUT_KEY(char unicodeChar)
        {
            ScanCode = 0;
            UnicodeChar = unicodeChar;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct EFI_KEY_DATA
    {
        /// <summary>The EFI scan code and Unicode value returned from the input device.</summary>
        public readonly EFI_INPUT_KEY Key;
        /// <summary>The current state of various toggled attributes as well as input modifier values.</summary>
        public readonly EFI_KEY_STATE KeyState;

        public EFI_KEY_DATA(EFI_INPUT_KEY key, EFI_KEY_STATE keyState)
        {
            Key = key;
            KeyState = keyState;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct EFIKeyNotifyHandle
    {
        private readonly void* Handle;
    }

    public enum EFI_STATUS : ulong
    {
        //Success Codes
        EFI_SUCCESS = 0,

        //Warning Codes
        EFI_WARN_UNKNOWN_GLYPH = 1,
        //EFI_WARN_DELETE_FAILURE = 2,
        //EFI_WARN_WRITE_FAILURE = 3,
        //EFI_WARN_BUFFER_TOO_SMALL = 4,
        //EFI_WARN_STALE_DATA = 5,
        //EFI_WARN_FILE_SYSTEM = 6,
        //EFI_WARN_RESET_REQUIRED = 7,

        //Error Codes
        //Unused but convenient to represent high bit being set for error status codes
        EFI_ERROR = 0x8000000000000000,
        
        //EFI_LOAD_ERROR = 0x8000000000000001,
        EFI_INVALID_PARAMETER = 2 | EFI_ERROR,
        EFI_UNSUPPORTED = 3 | EFI_ERROR,
        //EFI_BAD_BUFFER_SIZE = 0x8000000000000004,
        EFI_BUFFER_TOO_SMALL = 5 | EFI_ERROR,
        EFI_NOT_READY = 6 | EFI_ERROR,
        EFI_DEVICE_ERROR = 7 | EFI_ERROR,
        //EFI_WRITE_PROTECTED = 0x8000000000000008,
        EFI_OUT_OF_RESOURCES = 9 | EFI_ERROR,
        //EFI_VOLUME_CORRUPTED = 0x800000000000000A,
        //EFI_VOLUME_FULL = 0x800000000000000B,
        //EFI_NO_MEDIA = 0x800000000000000C,
        //EFI_MEDIA_CHANGED = 0x800000000000000D,
        EFI_NOT_FOUND = 14 | EFI_ERROR,
        EFI_ACCESS_DENIED = 15 | EFI_ERROR,
        //EFI_NO_RESPONSE = 0x8000000000000010,
        //EFI_NO_MAPPING = 0x8000000000000011,
        //EFI_TIMEOUT = 0x8000000000000012,
        //EFI_NOT_STARTED = 0x8000000000000013,
        EFI_ALREADY_STARTED = 20 | EFI_ERROR,
        //EFI_ABORTED = 0x8000000000000015,
        //EFI_ICMP_ERROR = 0x8000000000000016,
        //EFI_TFTP_ERROR = 0x8000000000000017,
        //EFI_PROTOCOL_ERROR = 0x8000000000000018,
        //EFI_INCOMPATIBLE_VERSION = 0x8000000000000019,
        //EFI_SECURITY_VIOLATION = 0x800000000000001A,
        //EFI_CRC_ERROR = 0x800000000000001B,
        //EFI_END_OF_MEDIA = 0x800000000000001C,
        //EFI_END_OF_FILE = 0x800000000000001F,
        //EFI_INVALID_LANGUAGE = 0x8000000000000020,
        //EFI_COMPROMISED_DATA = 0x8000000000000021,
        //EFI_IP_ADDRESS_CONFLICT = 0x8000000000000022,
        //EFI_HTTP_ERROR = 0x8000000000000023
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct EFI_EVENT
    {
        private readonly void* Event;
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct EFI_GUID
    {
        private readonly uint Data1;
        private readonly ushort Data2;
        private readonly ushort Data3;
        private readonly byte Data41;
        private readonly byte Data42;
        private readonly byte Data43;
        private readonly byte Data44;
        private readonly byte Data45;
        private readonly byte Data46;
        private readonly byte Data47;
        private readonly byte Data48;

        public EFI_GUID(uint data1, ushort data2, ushort data3, byte data41, byte data42, byte data43, byte data44, byte data45, byte data46, byte data47, byte data48)
        {
            Data1 = data1;
            Data2 = data2;
            Data3 = data3;
            Data41 = data41;
            Data42 = data42;
            Data43 = data43;
            Data44 = data44;
            Data45 = data45;
            Data46 = data46;
            Data47 = data47;
            Data48 = data48;
        }

        public bool Equals(EFI_GUID other)
        {
            return Data1 == other.Data1 && Data2 == other.Data2 && Data3 == other.Data3 &&
                   Data41 == other.Data41 && Data42 == other.Data42 && Data43 == other.Data43 && Data44 == other.Data44 &&
                   Data45 == other.Data45 && Data46 == other.Data46 && Data47 == other.Data48 && Data44 == other.Data44;
        }
    }

    /// <summary>This protocol is used to obtain input from the ConsoleIn device.</summary>
    /// <remarks>The EFI specification requires that the EFI_SIMPLE_TEXT_INPUT_EX_PROTOCOL supports the same languages as the corresponding EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct EFI_SIMPLE_TEXT_INPUT_PROTOCOL
    {
        public static readonly EFI_GUID EFI_SIMPLE_TEXT_INPUT_EX_PROTOCOL_GUID =
            new(0xdd9e7534, 0x7762, 0x4698, 0x8c, 0x14, 0xf5, 0x85, 0x17, 0xa6, 0x25, 0xaa);

        private readonly IntPtr _pad1;
        private readonly delegate*<EFI_SIMPLE_TEXT_INPUT_PROTOCOL*, EFI_KEY_DATA*, EFI_STATUS> _readKeyStrokeEx;
        /// <summary>Event to use with WaitForEvent() to wait for a key to be available. An Event will only be triggered if KeyData.Key has information contained within it.</summary>
        public readonly EFI_EVENT WaitForKeyEx;
        private readonly IntPtr _pad2;
        private readonly delegate*<EFI_SIMPLE_TEXT_INPUT_PROTOCOL*, EFI_KEY_DATA*, delegate*<EFI_KEY_DATA*,
            EFI_STATUS>, EFIKeyNotifyHandle*, EFI_STATUS> _registerKeyNotify;

        //TODO Support Scan codes and SetState which should be called on startup to ensure EFI_KEY_STATE_EXPOSED is set
        /// <summary>Reads the next keystroke from the input device.</summary>
        /// <remarks>The <paramref name="keyData"/>.Key.UnicodeChar is the actual printable character or is zero if the key does not represent a printable character (control key, function key, etc.).
        /// <para>The <paramref name="keyData"/>.KeyState is the modifier shift state for the character reflected in <paramref name="keyData"/>.Key.UnicodeChar <!-- or <paramref name="keyData"/>.Key.ScanCode-->.</para>
        /// <para>When interpreting the data from this function, it should be noted that if a class of printable characters that are normally adjusted by shift modifiers (e.g. Shift Key + "f" key) would be presented solely as a <paramref name="keyData"/>.Key.UnicodeChar without the associated shift state.</para>
        /// <para>This of course would not typically be the case for non-printable characters such as the pressing of the Right Shift Key + F10 key since the corresponding returned data would be reflected <!--both--> in the <paramref name="keyData"/>.KeyState.KeyShiftState <!--and <paramref name="keyData"/>.Key.ScanCode--> value<!--s-->.</para>
        /// <para> It should also be noted that certain input devices may not be able to produce shift or toggle state information, and in those cases the high order bit in the respective Toggle and Shift state fields should not be active.</para>
        /// <para>With <see cref="EFI_KEY_TOGGLE_STATE.EFI_KEY_STATE_EXPOSED"/> bit enabled, this function will allow the return of incomplete keystrokes such as the holding down of certain keys which are expressed as a part of KeyState when there is no Key data.</para>
        /// </remarks>
        /// <param name="keyData">A buffer that is filled in with the keystroke state data for the key that was pressed.</param>
        /// <returns>
        /// <list type="table">
        ///     <item>
        ///         <term><see cref="EFI_STATUS.EFI_SUCCESS"/></term>
        ///         <description>The keystroke information was returned.</description>
        ///     </item>
        ///      <item>
        ///         <term><see cref="EFI_STATUS.EFI_NOT_READY"/></term>
        ///         <description>There was no keystroke data available. Current <paramref name="keyData"/>.KeyState values are exposed if <see cref="EFI_KEY_TOGGLE_STATE.EFI_KEY_STATE_EXPOSED"/> is set.</description>
        ///     </item>
        ///      <item>
        ///         <term><see cref="EFI_STATUS.EFI_DEVICE_ERROR"/></term>
        ///         <description>The keystroke information was not returned due to hardware errors.</description>
        ///     </item>
        /// </list>
        /// </returns>
        public EFI_STATUS ReadKeyStrokeEx(out EFI_KEY_DATA keyData)
        {
            fixed (EFI_SIMPLE_TEXT_INPUT_PROTOCOL* pThis = &this)
            {
                fixed (EFI_KEY_DATA* pKey = &keyData)
                {
                    return _readKeyStrokeEx(pThis, pKey);
                }
            }
        }

        /// <summary>Register a notification function for a particular keystroke for the input device.</summary>
        /// <param name="keyData">A buffer that is filled in with the keystroke information for the key that was pressed.
        /// <para>If KeyData.Key, KeyData.KeyState.KeyToggleState and KeyData.KeyState.KeyShiftState are 0, then any incomplete keystroke will trigger a notification of the <paramref name="keyNotificationFunction"/>.</para></param>
        /// <param name="keyNotificationFunction">Points to the function to be called when the key sequence specified by <paramref name="keyData"/> is typed.</param>
        /// <param name="notifyHandle">Points to the unique handle assigned to the registered notification</param>
        /// <returns>
        /// <list type="table">
        ///     <item>
        ///         <term><see cref="EFI_STATUS.EFI_SUCCESS"/></term>
        ///         <description>Key notify was registered successfully</description>
        ///     </item>
        ///      <item>
        ///         <term><see cref="EFI_STATUS.EFI_OUT_OF_RESOURCES"/></term>
        ///         <description>Unable to allocate necessary data structures.</description>
        ///     </item>
        /// </list>
        /// </returns>
        public EFI_STATUS RegisterKeyNotify(EFI_KEY_DATA keyData,
            delegate*<EFI_KEY_DATA*, EFI_STATUS> keyNotificationFunction, out EFIKeyNotifyHandle notifyHandle)
        {
            fixed (EFI_SIMPLE_TEXT_INPUT_PROTOCOL* pThis = &this)
            {
                fixed (EFIKeyNotifyHandle* pNotifyHandle = &notifyHandle)
                {
                    return _registerKeyNotify(pThis, &keyData, keyNotificationFunction, pNotifyHandle);
                }
            }
        }
    }
}