using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Heal")]
public class HealEffect : UniqueEffect
{
    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        int healValue = (int)FlontBuff(card, flontCard);

        if ((player.Life + healValue) > player.LifeMax)
        {
            healValue = player.LifeMax - player.Life;
        }
        player.Life += healValue;
        message.text = $"{healValue}HPかいふくした";
    }

    //一枚前のカードの追加効果処理
    public float FlontBuff(Card card, Card flontCard)
    {
        float healValue = card.Base.CardStatus.Heal_Status;

        if (flontCard == null)
        {
            return healValue;
        }
        else
        {
            string cardName = flontCard.Base.CardName;
            FlontBuff foundBuff = card.Base.FlontBuff.Find(buff => buff.flontCard == cardName);

            if (foundBuff == null)
            {
                return (int)healValue;
            }
            healValue *= foundBuff.buff;
            return healValue;
        }

    }
}
