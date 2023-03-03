using System;
using System.Runtime.InteropServices;

namespace DUM.EFI;

[StructLayout(LayoutKind.Sequential)]
struct EFI_HANDLE
{
    private IntPtr _handle;
}