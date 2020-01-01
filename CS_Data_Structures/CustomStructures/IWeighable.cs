using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;

namespace CustomStructures
{
    public interface IWeighable<T>
    {
        T InfinityValue();
        T ZeroValue();
       event Func<T,bool> Comparer;
    }
}