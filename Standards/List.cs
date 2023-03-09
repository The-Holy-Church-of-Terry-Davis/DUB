using DUM.NativeMem;
using System.Collections;

namespace System;

/*
    [DYSFUNCTIONAL] In process of rewriting

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
    private Collection<T> _coll { get; set; }
    public int Length => _coll.Length;

    public List(long addr, int length)
    {
        Collection<T> coll = new Collection<T>(addr, length);
    }

    public void Add(T item, long new_addr)
    {
        Collection<T> newColl = new Collection<T>(new_addr, Length + 1);
        newColl[Length] = item;
        _coll = newColl;
    }

/*
    Ellie notes:

    These methods will need to be implemented as an extension methods in the Kernel
    
    public void Add(T item) { }
    public void AddRange(Collection<T> items) { }

    public Block<T> AsBlock(long addr)
    {
        *(Block<T> *)addr = new(addr, addr + sizeof(Block<T>)); //This cannot be done since it may overwrite memory
        Block<T> blk = *(Block<T> *)addr;
    }
*/
}