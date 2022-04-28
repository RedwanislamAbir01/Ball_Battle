
using UnityEngine;

public interface ISoldier
{
    int SpwanEnergyPoint
    {
        get;
        set;
    }
    
   void Activated();
   void InActivated();
}
