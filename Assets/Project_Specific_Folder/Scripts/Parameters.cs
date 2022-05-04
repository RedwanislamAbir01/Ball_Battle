using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public bool isAtk;
}


public static class Parameters 
{
 
    static Player AttackerSet()
    {
        Player p = new Player();
        p.isAtk = true;
        return p;
    }
    static Player DefenderSet()
    {
        Player p = new Player();
        p.isAtk = false;
        return p;
    }

    public static Player PlayerState(bool isPlayer)
    {
        if (isPlayer)
        {
            if(GameManager.Instance.MatchNo % 2 == 1)
            {
                return AttackerSet();
            }
            else
            {
                GameManager.Instance.FieldScript.SwapGates();
                return DefenderSet();

            }
        }
        else
        {
            if (GameManager.Instance.MatchNo % 2 == 1)
            {
                return DefenderSet();
            }
            else
            {
                return AttackerSet();
            }
        }
    }
}
