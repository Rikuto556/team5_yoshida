using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Naught")]
public class NaughtEffect : UniqueEffect
{
    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        int naughtValue = (int)FlontBuff(card, flontCard);

        message.text = "何も起こらない";
    }

    //一枚前のカードの追加効果処理
    public float FlontBuff(Card card, Card flontCard)
    {
        string cardName = card.Base.CardName;
        float naughtValue = card.Base.CardStatus.Heal_Status;

        if (flontCard == null)
        {
            return naughtValue;
        }
        else
        {
            FlontBuff foundBuff = flontCard.Base.FlontBuff.Find(buff => buff.flontCard == cardName);

            if (foundBuff == null)
            {
                return (int)naughtValue;
            }
            naughtValue *= foundBuff.buff;
            return naughtValue;
        }

    }
}
