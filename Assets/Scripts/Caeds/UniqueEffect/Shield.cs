using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Shield")]
public class ShieldEffect : UniqueEffect
{
    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        int difenseValue = (int)FlontBuff(card, flontCard);

        player.Defens += difenseValue;
        
        if (player.Defens > 100)
        {
            player.Defens = 100;
        }
        message.text = $"{player.Defens}ぼうぎょがあがった";
    }

    //一枚前のカードの追加効果処理
    public float FlontBuff(Card card, Card flontCard)
    {
        float difenseValue = card.Base.CardStatus.Defense_Status;

        if (flontCard == null)
        {
            return (int)difenseValue;
        }
        else
        {
            string cardName = flontCard.Base.CardName;
            FlontBuff foundBuff = card.Base.FlontBuff.Find(buff => buff.flontCard == cardName);

            if (foundBuff == null)
            {
                return (int)difenseValue;
            }
            difenseValue *= foundBuff.buff;
            return (int)difenseValue;
        }

    }
}

