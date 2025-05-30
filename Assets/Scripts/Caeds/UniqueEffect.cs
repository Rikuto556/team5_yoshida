using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class UniqueEffect : ScriptableObject
{
    public abstract void Execute(Card card ,Card flontCard ,Battler player, Enemy enemy, Text message);
}
