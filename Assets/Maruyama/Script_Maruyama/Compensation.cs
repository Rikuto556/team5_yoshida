﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/Compensation")]
public class Compensation : UniqueEffect
{
    [SerializeField] float magnification = 1.5f; //ダメージの倍率を設定
    [SerializeField] int sacrifice = 10; //代償

    public int Damageteisuu => sacrifice; //　Battlers.csで呼び出せるようにプロパティを設定

    public bool CanUse(Battler player)
    {
        return player.Life >= sacrifice;
    }

    //カードの効果処理
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {

        int attackValue = FlontBuff(card, flontCard);

        int Hit = (int)(attackValue * magnification * Random.Range(0.8f, 1.2f)); //攻撃力に倍率をかける
        float defense = 1f - enemy.Base.EnemyDefense / 100f;
        int damage = (int)(Hit * defense);
        enemy.Base.EnemyLife -= damage;
        if (enemy.Base.EnemyLife < 0)
        {
            enemy.Base.EnemyLife = 0;
        }
        player.Life -= sacrifice;
        if (player.Life < 0)
        {
            player.Life = 0;
        }
        message.text = $"{sacrifice }ダメージを受けた。\n{damage}ダメージを与えた！"; 
        //  SubmitPositionでCanUseを呼び出さないとどうやってもカードを止められない
    }
    //一枚前のカードの追加効果処理
    public int FlontBuff(Card card, Card flontCard)
    {

        float attackValue = (int)card.Base.CardStatus.Attack_Status;


        if (flontCard == null)
        {
            return (int)attackValue;
        }
        else
        {
            string cardName = flontCard.Base.CardName;
            FlontBuff foundBuff = card.Base.FlontBuff.Find(buff => buff.flontCard == cardName);

            if (foundBuff == null)
            {
                return (int)attackValue;
            }
            attackValue *= foundBuff.buff;
            return (int)attackValue;
        }


    }
}
