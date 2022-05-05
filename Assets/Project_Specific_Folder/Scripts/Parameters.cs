using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public bool isAtk;
}


public static class Parameters 
{
    public static int MatchPerGame = 5;
    public static float TimeLeft = 14f;
    public static int EnergyBarCount = 6;

    public static float EnergyRegenAttacker = .5f;
    public static float EnergyRegenDefender = .5f;
    public static float EnergyCostAttacker = 2;
    public static float EnergyCostDefender = 3;

    public static float SpawnTimeAtk = .5f;
    public static float SpawnTimeDef = .5f;

    public static float ReactivateTimeAtk = 2.5f;
    public static float ReactivateTimeDef = 4f;

    public static float SpeedNormalAtk = 1.5f;
    public static float SpeedNormalDef = 1.0f;

    public static float CarryingSpeed = .75f;
    public static float SpeedBall = 1.5f;

    public static float ReturnSpeed = 2;
    public static float DetectionRangeMultiplier = .35f; 
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
                GameManager.Instance.FieldScript.SwapGates();
                return AttackerSet();
            }
        }
    }
}
