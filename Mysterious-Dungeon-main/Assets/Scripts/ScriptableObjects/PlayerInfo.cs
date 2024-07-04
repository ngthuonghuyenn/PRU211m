using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "PlayerInfo", menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    public int sceneBuildIndex = 0;
    public int Coin;
    public int Health;
    public List<string> Skills;
    public List<string> Items;
}
