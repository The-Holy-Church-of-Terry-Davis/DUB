using DUM.NativeMem;

namespace System;

public unsafe struct Collection<T> where T: unmanaged
{
    private T* ptr { get; set; }
    public int Length { get; set; }

    public T this[int index]
    {
        get {
            return ptr[index];
        }

        set {
            ptr[index] = value;
        }
    }
}