using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public struct PrimitiveComparer<T> : IEqualityComparer<T> where T : struct, IEquatable<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(T x, T y)
    {
        return x.Equals(y); 
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetHashCode(T obj) => obj.GetHashCode();
}