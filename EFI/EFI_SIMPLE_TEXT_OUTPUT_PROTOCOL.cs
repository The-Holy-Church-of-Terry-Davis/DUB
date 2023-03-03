using System;
using System.Runtime.InteropServices;

namespace DUM.EFI;

[StructLayout(LayoutKind.Sequential)]
unsafe readonly struct EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL
{
    private readonly IntPtr _pad;

    public readonly delegate* unmanaged<void*, char*, void*> OutputString;
}