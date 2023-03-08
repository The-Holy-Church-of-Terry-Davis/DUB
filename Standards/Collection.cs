using DUM.NativeMem;

namespace System;

public unsafe struct Collection<T> where T: unmanaged
{
    private T* ptr { get; set; }
    public int Length { get; set; }

    public Collection(int len)
    {
        Length = len;
    }

    public T this[int index]
    {
        get {
            //out of bounds handling
            if(index > Length - 1)
            {
                return ptr[Length - 1];
            }

            return ptr[index];
        }

        set {
            //out of bounds handling
            if(index > Length - 1)
            {
                ptr[Length - 1] = value;
            }

            ptr[index] = value;
        }
    }
}