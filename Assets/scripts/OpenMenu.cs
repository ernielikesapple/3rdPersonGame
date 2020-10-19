using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{
    public void OpenMenuAction()
    {   // step one create another scene, add this script to Canvas, drag Canvas object into 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
    // Start is called before the first frame update

