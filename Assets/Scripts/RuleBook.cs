﻿using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class RuleBook : MonoBehaviour
{
    [SerializeField] Text message;
    [SerializeField] float pluseffect;
    [SerializeField] public reflectorEffect reflector;



    //一枚前のカードの追加効果処理
    /*
    public void FlontEffect(Battler player, Card flontCard)
    {
        if (flontCard == null)
        {
            return;
        }
        else
        {
            player.Attack = (int)(player.Attack * flontCard.Base.CardEffect.Attack_Effect);
            player.MagicAttack = (int)(player.MagicAttack * flontCard.Base.CardEffect.Magic_Effect);
            player.Guard = (int)(player.Guard * flontCard.Base.CardEffect.Protection_Effect);
            player.Heal = (int)(player.Heal * flontCard.Base.CardEffect.Heal_Effect);
        }
    }*/

    //合成カードに掛かる倍率
    /*
    public void TypeEffect(Battler player, Card card)
    {
        if (card.Base.SynthesisType == SynthesisType.Normal)
        {
        }
        else if (card.Base.SynthesisType == SynthesisType.Plus)
        {
            player.Attack = (int)(player.Attack * pluseffect);
            player.MagicAttack = (int)(player.MagicAttack * pluseffect);
            player.Guard = (int)(player.Guard * pluseffect);
            player.Heal = (int)(player.Heal * pluseffect);
        }
    }*/

    //カードの効果処理
    public void selectedCardVS(Battler player, Card card, Card flontCard, Enemy enemy)
    {
        card.Base.UniqueEffect.Execute(card, flontCard, player, enemy, message);
        /*if (card.Base.Type == CardType.Sword)
        {
            int Hit = (int)(player.Attack * Random.Range(0.8f, 1.2f));
            float defense = 1f - enemy.Base.EnemyDefense / 100f;
            int damage = (int)(Hit * defense);
            enemy.Base.EnemyLife -= damage;
            kekka.text = $"{damage}ダメージ与えた";
            if (enemy.Base.EnemyLife < 0)
            {
                enemy.Base.EnemyLife = 0;
            }
        }
        else if (card.Base.Type == CardType.Witchcraft)
        {

            int Hit = (int)(player.MagicAttack * Random.Range(0.8f, 1.2f));
            float defense = 1f - enemy.Base.EnemyMagicDefense / 100f;
            int damage = (int)(Hit * defense);
            enemy.Base.EnemyLife -= damage;
            kekka.text = $"{damage}魔法ダメージあたえた";
            if (enemy.Base.EnemyLife < 0)
            {
                enemy.Base.EnemyLife = 0;
            }
        }
        else if (card.Base.Type == CardType.Protection)
        {

            player.Defens += player.Guard;
            if(player.Defens > 100)
            {
                player.Defens = 100;
            }
            kekka.text = $"{player.Defens}ぼうぎょがあがった";
        }
        else if (card.Base.Type == CardType.Heal)
        {

            if ((player.Life + player.Heal) > player.LifeMax)
            {
                player.Heal = player.LifeMax - player.Life;
            }
            player.Life += player.Heal;
            kekka.text = $"{player.Heal}HPかいふくした";
        }*/

    }

    //エネミーの強力攻撃までのカウントダウン
    public void EnemyCountDown(Enemy enemy)
    {
        if (enemy.Base.Count1 == 0)
        {
            enemy.Base.Count1 = enemy.Base.EnemyCount;
            enemy.CountText1.text = $"{enemy.Base.Count1}";
        }
        else
        {
            enemy.Base.Count1 = enemy.Base.Count1 - 1;
            enemy.CountText1.text = $"{enemy.Base.Count1}";
        }
    }

    //敵のターン処理
    public void EnemyAttack(Battler player, Enemy enemy)
    {
        if (enemy.IsParalyzed) //麻痺なら
        {
            message.text = $"{enemy.Base.Name1}は麻痺していて動けない！";
            enemy.IsParalyzed = false; // 1ターンで回復
            return; //スキップ
        }

        int Hit = (int)(enemy.Base.EnemyAttack * Random.Range(0.8f, 1.1f));
        float Decrease = 1f - player.Defens / 100f;

        if (enemy.Base.Count1 == 0)
        {
            Hit = 2 * Hit;
        }
//<<<<<<< HEAD

//=======
       
//>>>>>>> fixyoshida
        Hit = (int)(Hit * Decrease);
        Debug.Log(reflector.isreflector);
        if (reflector.isreflector)
        {
            reflector.reflectorAttak(player, enemy, message, Hit);
        }
        else
        {
            player.Life -= Hit;
            message.text = $"{Hit}ダメージをうけた";
        }
            
            //Debug.Log("敵");
//reflector.reflectorAttak(player, enemy, message, Hit);
        //message.text = $"{Hit}ダメージをうけた";


    }

    //敵の状態表示
    public void EnemyParLife(Enemy enemy)
    {
        float RestLife = (float)enemy.Base.EnemyLife / (float)enemy.Base.EnemyLifeMax;

        if (RestLife == 1f)
        {
            message.text = "全く傷ついていない！";
        }
        else if (RestLife > 0.7f)
        {
            message.text = $"{enemy.Base.Name1}はピンピンしている";
        }
        else if (RestLife > 0.4f)
        {
            message.text = $"{enemy.Base.Name1}は疲れ始めている";
        }
        else
        {
            message.text = $"{enemy.Base.Name1}はもうボロボロだ！";
        }
    }

    //結果のテキストリセット
    public void TextSetupNext()
    {
        message.text = "";
    }
}
