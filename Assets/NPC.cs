using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    //定义NPC对话数据
    private string[] dialogue = { "Hi,I am NPC", "Here is a quest for you", "Kill the Bog Goblin" };

    private int index = 0;
    public Text Text;
    public Text rewardText;
    public Player refToPlayer;

    private bool isTalk;


    

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (isTalk)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (index < dialogue.Length)
                {
                    Text.text = "NPC:" + dialogue[index];
                    index = index + 1;
                }
                else
                {
                    Destroy(Text);
                    isTalk = false;
                }
            }
        }


        if (Input.GetMouseButtonDown(1))
        {

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 15))
            {
               
                
                if (hit.transform.tag == "NPC")
                {
                    if (refToPlayer.quest.isSuccess)
                    {
                        StartCoroutine(ActivationRoutine());
                    }
                    else
                    {
                        this.GetComponent<QuestGiver>().OpenQuestWindow();
                    }
                }


            }
        }



     }


     



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isTalk = true;
        }
    }

    private IEnumerator ActivationRoutine()
    {
        //Wait for 14 secs.
        //yield return new WaitForSeconds(14);

        //Turn My game object that is set to false(off) to True(on).
        rewardText.gameObject.SetActive(true);



        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(5);
        Debug.Log("should be off");
        //Game object will turn off
        rewardText.gameObject.SetActive(false);
    }
}


