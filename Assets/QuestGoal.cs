using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public int requiredAmount = 1 ;
    public int currentAmount;

    public bool IsReached() {
        return (currentAmount >= requiredAmount);
    }


    public void EnemyKilled() {
        currentAmount++;
    }

}
