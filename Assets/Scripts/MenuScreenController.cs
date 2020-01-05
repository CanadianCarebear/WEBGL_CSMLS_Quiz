using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{   public int scene;


    public void StartGame()
    {   LoadScene(scene);   }

    public void LoadScene(int scene)
    {   switch (scene)
        {   case 1:
                SceneManager.LoadScene("MenuScreen");
                break;
            case 2:
                SceneManager.LoadScene("Game00");
                break;
            case 3:
                SceneManager.LoadScene("AboutMe");
                break;
            case 4:
                SceneManager.LoadScene("Credits");
                break;
            default:
                break;
        }
    }
}
