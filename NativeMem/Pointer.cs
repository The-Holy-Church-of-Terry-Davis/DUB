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

public unsafe static class PointerExtensions
{
    public static Pointer<char> AsPtr(this char val)
    {
        Pointer<char> ret = new Pointer<char>((long)&val, val);
        return ret;
    }

    public static Pointer<int> AsPtr(this int val)
    {
        Pointer<int> ret = new Pointer<int>((long)&val, val);
        return ret;
    }

    public static Pointer<long> AsPtr(this long val)
    {
        Pointer<long> ret = new Pointer<long>((long)&val, val);
        return ret;
    }

    public static Pointer<byte> AsPtr(this byte val)
    {
        Pointer<byte> ret = new Pointer<byte>((long)&val, val);
        return ret;
    }
}