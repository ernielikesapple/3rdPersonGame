using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest 
{
    public bool isActive;
    public string title = "Event";
    public string description = "Journey to the Peter Curry marsh and slay the Bog Goblin. Retrieve the stolen student IDs from the creature’s corpse and return them to Lord Goldbloom to collect your reward.";

    // public int experienceReward; optional to have
    public int goldReward = 30;

    public bool isSuccess = false;


    public QuestGoal goal;

    public void Complete() {

        isActive = false;

        isSuccess = true;
    }


  

}
