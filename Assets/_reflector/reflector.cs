using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UniqueEffects/reflector")]
public class reflectorEffect : UniqueEffect
{
    [SerializeField] float reflectorValue = 3f;
    public bool isreflector = false;
    public override void Execute(Card card, Card flontCard, Battler player, Enemy enemy, Text message)
    {
        message.text = "反射状態になった";
        isreflector = true;

        //player.Defens += difenseValue;

        //if (player.Defens > 100)
        //{
        //    player.Defens = 100;
        //}
        //message.text = $"{player.Defens}ぼうぎょがあがった";
        //Debug.Log("リフレクター処理");
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
    public void reflectorAttak(Battler player, Enemy enemy, Text message, int HIt)
    {
        int Enemydamege = (int)(HIt * 0.5f);//受けたダメージの半減
        Debug.Log("敵の攻撃カット");
        player.Life -= (int)Enemydamege;
        int damege =(int)(Enemydamege*reflectorValue);
        //Debug.Log(Enemydamege);
        enemy.Base.EnemyLife -= damege;
        enemy.EnemyLifeContlloer.lifeReflection(enemy);
        message.text = $"{Enemydamege}ダメージをうけた" + $"{damege}ダメージを与えた";

        //enemy.Base.EnemyLife -= (int)Enemydamege * 3;
        Debug.Log(Enemydamege * 3);
        if (enemy.Base.EnemyLife < 0)
        {
            enemy.Base.EnemyLife = 0;
        }
        isreflector = false;




    }












}

