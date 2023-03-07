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
public unsafe struct List<T> where T: unmanaged
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

    public List() { }

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