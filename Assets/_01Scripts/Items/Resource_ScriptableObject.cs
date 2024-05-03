using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ResourceDate", menuName = "Scriptable Object/Resource Data", order = int.MaxValue)]

public class Resource_ScriptableObject : ScriptableObject
{
    [SerializeField]
    private int Coin;
    public int coin { get { return Coin; } set { Coin = value; } }

    [SerializeField]
    private int Jem;
    public int jem { get { return Jem; } set { Jem = value; } }
}
