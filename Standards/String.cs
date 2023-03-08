namespace System;

public unsafe class String
{ 
    private char *ptr { get; set; }
    public int Length;

    public char this[int index]
    {
        get {
            return ptr[index];
        }

        set {
            ptr[index] = value;
        }
    }

    public static String operator +(String str1, String str2) 
    {
        String ret = new();
        ret.Length = str1.Length + str2.Length;

        for(int i = 0; i < str1.Length; i++)
        {
            ret[i] = str1[i];
        }

        for(int i = 0; i < str2.Length; i++)
        {
            ret[i + str1.Length] = str2[i];
        }

        return ret;
    }

    public static bool operator ==(String str1, String str2)
    {
        if(str1.Length != str2.Length)
        {
            return false;
        }

        for(int i = 0; i < str1.Length; i++)
        {
            if(str1[i] != str2[i])
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator !=(String str1, String str2)
    {
        if(str1.Length != str2.Length)
        {
            return true;
        }

        for(int i = 0; i < str1.Length; i++)
        {
            if(str1[i] != str2[i])
            {
                return true;
            }
        }

        return false;
    }

    public static implicit operator char[](String val)
    {
        char[] ret = new char[val.Length];

        for(int i = 0; i < val.Length; i++)
        {
            ret[i] = val[i];
        }

        return ret;
    }

    public static explicit operator String(char[] val)
    {
        String ret = "";
        ret.Length = val.Length;
        
        for(int i = 0; i < val.Length; i++)
        {
            ret[i] = val[i];
        }

        return ret;
    }
}

public static class StringExtensions
{
    public static String AsStr(char[] val)
    {
        return (String)val;
    }
}