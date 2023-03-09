namespace System.Collections;

public unsafe struct Collection<T> where T: unmanaged
{
    public long _addr { get; set; }
    public int Length { get; set; }

    public T this[int index]
    {
        get {
            return *(T *)(index + sizeof(char));
        }

        set {
            *(T *)(index + sizeof(char)) = value;
        }
    }

    public Collection(long addr, int length)
    {
        _addr = addr;
        Length = length;
    }

    public unsafe T * GetPtr()
    {
        return (T *)_addr;
    }
}