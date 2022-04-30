
using UnityEngine;

public interface ISoldier
{
    int SpwanEnergyPoint
    {
        get;
        set;
    }
    bool IsAttacking { get; set; }
 


    void Activated();
   void InActivated();
}
