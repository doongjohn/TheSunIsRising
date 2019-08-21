using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EventPath<T>
{
    
}

public class AUIProperty<T>
{
    T value;

    public T Value { 
        get => value; 

        set => this.value = value; 
    }

    public AUIProperty(T setValue)
    {
        value = setValue;   
    }
}
