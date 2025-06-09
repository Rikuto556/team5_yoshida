using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/ThunderStroke")]
public class ThunderStroke : UniqueEffect
{
    [SerializeField] int t_damage = 10; //雷撃のダメージ
    [SerializeField, Range(0, 100)] int accuracy; // 行動不可にする確率

    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        int attackValue = FlontBuff(card, flontCard);
        enemy.Base.EnemyLife -= t_damage;
        if (enemy.Base.EnemyLife < 0)
        {
            enemy.Base.EnemyLife = 0;
        }

        message.text = $"{t_damage}雷撃ダメージ与えた";

        enemy.ThunderCount++;  // 敵のカウントを増やす

        bool paralyze = false;

        if (enemy.ThunderCount >= 4)
        {
            paralyze = true; // 4回目は必中
            enemy.ThunderCount = 0; // カウントリセット
        }
        else
        {
            int rand = Random.Range(0, 100);
            paralyze = rand < accuracy;
        }

        if (paralyze)
        {
            enemy.SetParalyzed(true); // 敵に麻痺状態に
            message.text += "\n敵を麻痺させた！";
        }
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
