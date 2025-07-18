﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPosition submitPosition;

    [Header("プレイヤーベースステータス")]
    [SerializeField] int lifeMax;
    [Space(10)]
    [Header("実数値")]
    [SerializeField] int life;
    [SerializeField] int defens;


    public UnityAction OnSubmitAction;
    public UnityAction OnSynthesisAction;

    public bool IsSubmitted { get; private set; }
    public int LifeMax { get => lifeMax; set => lifeMax = value; }

    public int Defens { get => defens; set => defens = value; }
    public int Life { get => life; set => life = value; }

    public BattlerHand Hand { get => hand; }
    public SubmitPosition SubmitPosition { get => submitPosition; }
    //public Card SubmitCard { get => submitPosition.SubmitCard;} 
    public List<Card> SubmitList { get => submitPosition.Submitlist; }


    public void SetPlayer()
    {
        life = lifeMax;
    }


    //生成されたカードをリストに追加・カードクリック時の効果追加
    public void SerCardToHand(Card card)
    {
        hand.Add(card);
        card.OnClickCard = SelectedCard;
    }

    //カードクリック時のリアクション
    public void SelectedCard(Card card)
    {
        if (IsSubmitted)
            return;

        if (card.transform.parent == submitPosition.transform)
        {
            // 戻す処理（制限なし）
            submitPosition.ReRemove(card);
            submitPosition.SubmitCard = null;
            hand.Add(card);
            hand.RePosition(card);
            submitPosition.SubmitPositionIn();
            card.PosReset();
            submitPosition.effectReSet(card);
        }
        else if (submitPosition.SubmitCard != null)　
        {
            return;
        }
        else if (card.transform.parent == hand.transform)　　//クリックされたカードが手札にあるときの処理
        {
            // ここから追加
            int totalCon = 0; // 代償の合計値(1ターン内の2枚目以降を計算)
            foreach (Card submittedCard in submitPosition.Submitlist) //場に出したカード
            {
                if (submittedCard.Base.UniqueEffect is Compensation comp)
                {
                    totalCon += comp.Damageteisuu;
                }
            }

            if (card.Base.UniqueEffect is Compensation currentComp)
            {
                totalCon += currentComp.Damageteisuu;
            }

            if (totalCon > Life)
            {
                Debug.Log("HPが足りないためカードを出せません。");
                return;
            }

            // 出す処理
            submitPosition.Set(card);
            hand.RemoveList(card);
            hand.ResetPosition();
            card.PosReset();
        }
    }
    //決定ボタン入力時に行うアクション
    public void OnSubmitButton()
    {
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
    }

    public void OnSynthesisButton()
    {
        IsSubmitted = true;
        OnSynthesisAction?.Invoke();
    }


    //次のターンでの関数のリセット
    public void SetupNext()
    {
        IsSubmitted = false;
        submitPosition.DeleteCard();
        Defens = 0;
    }
}
