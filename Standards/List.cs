using DUM.NativeMem;

namespace System;

/*
    Ellie notes:

    In order to get the size of type List<T>
    I will need to do the following:

    1. Check the address of addrs[Length - 1] (last index of Length)
    2. Check if I have space to add an object of type T without
    overwriting memory.
    3. If I have space just append to the end of the array, if not
    then defragment the heap, move the object to where there is space,
    and append the object.

    List<T>.GetSize() will have to be implemented
    inside of the Kernel as an extension method.
*/
public unsafe struct List<T>
{
    private T[] addrs { get; set; }
    public int Length = 0;

    public T this[int index]
    {
        get {
            return addrs[index];
        }

        set {
            addrs[index] = value;
        }
    }

    public List(int init_len) 
    { 
        addrs = new T[init_len];
        Length = init_len;
    }

    public void Add(T obj)
    {
        addrs = new T[Length + 1];
        addrs[Length] = obj;

        Length++;
    }

/*
    Ellie notes:

    This method will need to be implemented as an extension method in the Kernel
    
    public Block<T> AsBlock(long addr)
    {
        *(Block<T> *)addr = new(addr, addr + sizeof(Block<T>)); //This cannot be done since it may overwrite memory
        Block<T> blk = *(Block<T> *)addr;
    }
*/
}

public static class ListExtensions
{
    public static List<char> AsList(this char[] vals)
    {
        List<char> ret = new List<char>(vals.Length);
        for(int i = 0; i < vals.Length; i++)
        {
            ret.Add(vals[i]);
        }

        return ret;
    }

    public static List<int> AsList(this int[] vals)
    {
        List<int> ret = new List<int>(vals.Length);
        for(int i = 0; i < vals.Length; i++)
        {
            ret.Add(vals[i]);
        }

        return ret;
    }

    public static List<long> AsList(this long[] vals)
    {
        List<long> ret = new List<long>(vals.Length);
        for(int i = 0; i < vals.Length; i++)
        {
            ret.Add(vals[i]);
        }

        return ret;
    }

    public static List<byte> AsList(this byte[] vals)
    {
        List<byte> ret = new List<byte>(vals.Length);
        for(int i = 0; i < vals.Length; i++)
        {
            ret.Add(vals[i]);
        }

        return ret;
    }
}