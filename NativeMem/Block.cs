namespace DUM.NativeMem;

public unsafe struct Block
{
    public long _start { get; set; }
    public long _end  { get; set; }

    public Block(long start, long end)
    {
        _start = start;
        _end = end;
    }
}