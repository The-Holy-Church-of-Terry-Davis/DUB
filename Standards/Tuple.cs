namespace System;

public struct Tuple<T, T1>
{
    public T _val1 { get; set; }
    public T1 _val2 { get; set; }

    public Tuple(T val1, T1 val2)
    {
        _val1 = val1;
        _val2 = val2;
    }
}