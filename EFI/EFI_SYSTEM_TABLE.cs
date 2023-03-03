using System;
using System.Runtime.InteropServices;

namespace DUM.EFI;

[StructLayout(LayoutKind.Sequential)]
unsafe readonly struct EFI_SYSTEM_TABLE
{
    public readonly EFI_TABLE_HEADER Hdr;
    public readonly char* FirmwareVendor;
    public readonly uint FirmwareRevision;
    public readonly EFI_HANDLE ConsoleInHandle;
    public readonly EFI_SIMPLE_TEXT_INPUT_PROTOCOL* ConIn;
    public readonly EFI_HANDLE ConsoleOutHandle;
    public readonly EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* ConOut;
}