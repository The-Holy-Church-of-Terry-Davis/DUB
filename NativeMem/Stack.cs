using System;

namespace DUM.NativeMem;

public unsafe class Stack
{
    private long *_addr { get; set; }
    private int pos { get; set; }
    public int Length { get; set; }

    private int sizeof_last { get; set; }
    private StackType typeof_last { get; set; }

    public void Push() { }

    public void Pop()
    {
        switch(typeof_last)
        {
            case StackType.Char:
            {
                pos = pos - sizeof(char);
                break;
            }

            case StackType.Int:
            {
                pos = pos - sizeof(int);
                break;
            }

            case StackType.Long:
            {
                pos = pos - sizeof(long);
                break;
            }

            case StackType.Byte:
            {
                pos = pos - sizeof(byte);
                break;
            }
        } 
    }
}

public enum StackType
{
    Char = 1,
    Int = 2,
    Long = 3,
    Byte = 4
}