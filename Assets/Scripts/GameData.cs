using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    private static int mobQuantity = 0;
    private static int score = 0;
    
    public static event Action OnAllMobsDead;

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
