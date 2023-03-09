namespace System;


/*
    Str is meant to sit on the stack but
    really is just a pointer to characters
    sitting in the heap.
*/

public unsafe struct Str
{
    private long _addr { get; set; }
    private char _firstchar { get; set; }
    public int Length;

    public char this[int index]
    {
        get {
            return GetPtr()[index];
        }

        set {
            GetPtr()[index] = value;
        }
    }

    public unsafe Str(long addr, int len)
    {
        _addr = addr;
        
        char *ptr = (char *)addr;
        _firstchar = ptr[0];

        Length = len;
    }

    public void Set(char *ptr, int length)
    {
        Length = length;

        for(int i = 0; i < length; i++)
        {
            this[i] = ptr[i];
        }
    }

    public unsafe char* GetPtr()
    {
        return (char *)_addr;
    }

    public bool Equals(Str val)
    {
        if(val.Length != this.Length)
        {
            return false;
        }

        for(int i = 0; i < val.Length; i++)
        {
            if(val[i] != this[i])
            {
                return false;
            }
        }

        return true;
    }
}