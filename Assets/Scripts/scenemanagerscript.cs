using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class scenemanagerscript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene(){
        switch(SceneManager.GetActiveScene().name){
            case "Main Menu":
                SceneManager.LoadScene("1");
            break;
            case "1":
                SceneManager.LoadScene("2");
            break;
            case "2":
                SceneManager.LoadScene("3");
            break;
            case "3":
                SceneManager.LoadScene("4");
            break;
            case "4":
                SceneManager.LoadScene("5");
            break;
            case "5":
                SceneManager.LoadScene("6");
            break;
            case "6":
                SceneManager.LoadScene("End");
            break;
            default:
                SceneManager.LoadScene("Main Menu");
            break;
            
        }
    }

    public void RestartScene(){
        Scene curr = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curr.name);
    }
}
