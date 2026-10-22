using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerscript : MonoBehaviour
{
    public GameObject layout;
    public GameObject blackout;
    public GameObject scenemanager;
    int xpos;
    int ypos;
    public bool pushing = false;

    public Sprite left;
    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite idle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xpos = layout.GetComponent<levellayoutscript>().xpos;
        ypos = layout.GetComponent<levellayoutscript>().ypos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(xpos-7.5f, ypos-4.5f);
    }

    public void OnLeft(InputAction.CallbackContext action){
        if(action.performed){
            if(layout.GetComponent<levellayoutscript>().MovePlayer(1)){
                xpos--;
                PlayerAnimation(1);
            }
            else{
                FutilePush(1);
            }
        }
    }

    public void OnRight(InputAction.CallbackContext action){
        if(action.performed){
            if(layout.GetComponent<levellayoutscript>().MovePlayer(3)){
                xpos++;
                PlayerAnimation(3);
            }
            else{
                FutilePush(3);
            }
        }
    }

    public void OnDown(InputAction.CallbackContext action){
        if(action.performed){
            if(layout.GetComponent<levellayoutscript>().MovePlayer(4)){
                ypos--;
                PlayerAnimation(4);
            }
            else{
                FutilePush(4);
            }
        }
    }

    public void OnUp(InputAction.CallbackContext action){
        if(action.performed){ 
            if(layout.GetComponent<levellayoutscript>().MovePlayer(2)){
                ypos++;
                PlayerAnimation(2);
            }
            else{
                FutilePush(2);
            }
        }
    }

    public void OnRestart(InputAction.CallbackContext action){
        if(action.performed){ 
            blackout.GetComponent<blackoutscript>().End();
            Invoke(nameof(RestartLevel), 0.5f);
        }
    }

    void PlayerAnimation(int direction){
        string state = "playeridle";
        switch(direction){
            case 1:
            state = "playerleft";
            if(pushing){
                Invoke(nameof(NotPushing), 0.3f);
                gameObject.GetComponent<SpriteRenderer>().sprite = left;
            }
            break;
            case 2:
            state = "playerup";
            if(pushing){
                Invoke(nameof(NotPushing), 0.3f);
                gameObject.GetComponent<SpriteRenderer>().sprite = up;
            }
            break;
            case 3:
            state = "playerright";
            if(pushing){
                Invoke(nameof(NotPushing), 0.3f);
                gameObject.GetComponent<SpriteRenderer>().sprite = right;
            }
            break;
            default:
            state = "playerdown";
            if(pushing){
                Invoke(nameof(NotPushing), 0.3f);
                gameObject.GetComponent<SpriteRenderer>().sprite = down;

            }
            break;
        }
        gameObject.GetComponent<Animator>().Play(state);
    }

    void NotPushing(){
        gameObject.GetComponent<SpriteRenderer>().sprite = idle;
        pushing = false;
    }

    void FutilePush(int direction){
        switch(direction){
            case 1:
                Invoke(nameof(NotPushing), 0.3f);
                gameObject.GetComponent<SpriteRenderer>().sprite = left;
            break;
            case 2:
                Invoke(nameof(NotPushing), 0.3f);
                gameObject.GetComponent<SpriteRenderer>().sprite = up;
            break;
            case 3:
                Invoke(nameof(NotPushing), 0.3f);
                gameObject.GetComponent<SpriteRenderer>().sprite = right;
            break;
            default:
                Invoke(nameof(NotPushing), 0.3f);
                gameObject.GetComponent<SpriteRenderer>().sprite = down;
            break;
        }
    }

    void RestartLevel(){
        scenemanager.GetComponent<scenemanagerscript>().RestartScene();
    }
}
