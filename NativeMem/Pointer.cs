namespace DUM.NativeMem;

public unsafe struct Pointer<T> where T: unmanaged
{
    public long _addr { get; set; }

    public Pointer(long addr, T init)
    {
        _addr = addr;
        this.Set(init);
    }

    public void Set(T value)
    {
        *(T *)_addr = value;
    }
}