using System;

[StructLayout(LayoutKind.Sequential)]
struct EFI_HANDLE
{
    private IntPtr _handle;
}