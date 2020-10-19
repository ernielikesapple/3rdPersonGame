using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    
    public Player player;

    public GameObject questWindow;

    public Text titleText;
    public Text descriptionText;
    // public Text experienceText; optional
    public Text goldText;

    public Text currentQuestNumber;

    public void OpenQuestWindow()
    {
        
        questWindow.SetActive(true);

        Debug.Log(quest.title);
        titleText.text = quest.title;

        descriptionText.text = quest.description;
        // experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();

    }


    public void AcceptTask() {
        questWindow.SetActive(false);
        quest.isActive = true;

        player.quest = quest;

        currentQuestNumber.text = "current quest number:  1";
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }


}
