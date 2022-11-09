using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Collectible", menuName = "Collectibles" , order = 1)]
public class CollectibleSO : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
}
