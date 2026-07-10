using System;
using UnityEngine;

public delegate void OnWinDelegate();

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    private static int chanceToWin = 3;
    
    private static int mobQuantity = 0;
    private static int score = 0;
    
    public static event Action OnAllMobsDead;
    public static event Action OnGameOver;
    public static OnWinDelegate OnEnterToNirvana;

    public static int ChanceToWin
    {
        get { return chanceToWin; } 
        set
        {
            chanceToWin = value;
            if (chanceToWin == 0) OnGameOver?.Invoke();
        }
    }

    public static int MobQuantity
    {
        get { return mobQuantity; } 
        set
        {
            mobQuantity = value;
            if (mobQuantity == 0) OnAllMobsDead?.Invoke();
        }
    }
    
    public int Score { set { score = value; } }
    
}
