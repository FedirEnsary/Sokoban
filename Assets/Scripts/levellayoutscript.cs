using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class levellayoutscript : MonoBehaviour
{
    //general variables
    public int[,] layout = new int[16, 10];
    public List<GameObject> boxes;
    public int xpos;
    public int ypos;
    int goaltotal = 0;
    int goalcurr = 0;

    //references to tiles that render the level, used to dfind where the player can move and where the goals are.
    public GameObject floor;
    Tilemap floortiles;
    public GameObject main;
    Tilemap maintiles;
    public GameObject blackout;
    public GameObject scenemanager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get actual tilemaps for ease.
        floortiles = floor.GetComponent<Tilemap>();
        maintiles = main.GetComponent<Tilemap>();
        //check through the floor tiles to determine where the player/boxes can move, how many and where the goals are.
        for(int i = 0; i < 16; i++){
            for(int j = 0; j < 10; j++){
                switch(floortiles.GetTile(new Vector3Int(i-8, j-5, 0)).name){
                    case "brick":
                    case "tile":
                        layout[i, j] = 1;
                        break;
                    case "goal":
                        layout[i, j] = 2;
                        goaltotal++;
                        break;
                    default:
                        break;
                }
            }
        }
        //Record each box and also if they are already holding down a button.
        foreach(Transform box in GameObject.Find("Boxes").transform){
            boxes.Add(box.gameObject);
            if(layout[(int) (box.transform.position.x + 7.5), (int) (box.transform.position.y + 4.5)] == 2){
                goalcurr++;
            }
        }

        CheckCompletion();
    }

    // Update is called once per frame
    void Update(){}

    //Checks if a given tile has a box on it.
    bool CheckForBox(int x, int y){
        foreach(GameObject box in boxes){
            if(((int) (box.transform.position.x + 7.5)) == x && ((int) (box.transform.position.y + 4.5)) == y){
                return true;
            }
        }
        return false;
    }

    //Checks if it is valid for the player to move in a given direction, if so, then affects any needed boxes and player position.
    public bool MovePlayer(int direction){
        int xoffset = 0;
        int yoffset = 0;
        switch(direction){
            case 1:
            xoffset--;
            break;
            case 2:
            yoffset++;
            break;
            case 3:
            xoffset++;
            break;
            default:
            yoffset--;
            break;
        }
        if(layout[xpos+xoffset, ypos+yoffset] != 0){
            if(CheckForBox(xpos+xoffset, ypos+yoffset)){
                if(MoveBox(xpos+xoffset, ypos+yoffset, direction)){
                    GameObject.Find("Player").GetComponent<playerscript>().pushing = true;
                    xpos += xoffset;
                    ypos += yoffset;
                    return true;
                }
                return false;
            }
            xpos += xoffset;
            ypos += yoffset;
            return true;
        }
        return false;
    }

    //Helper function for Move player that recursively moves the entire line of boxes.
    bool MoveBox(int x, int y, int direction){
        int xoffset = 0;
        int yoffset = 0;
        switch(direction){
            case 1:
            xoffset--;
            break;
            case 2:
            yoffset++;
            break;
            case 3:
            xoffset++;
            break;
            default:
            yoffset--;
            break;
        }
        GameObject box = boxes[0];
        foreach(GameObject box1 in boxes){
            if((box1.transform.position.x + 7.5f) == x*1f && (box1.transform.position.y + 4.5f) == y*1f){
                box = box1;
            }
        }
        if(layout[x+xoffset, y+yoffset] != 0){
            if(CheckForBox(x+xoffset, y+yoffset)){
                if(MoveBox(x+xoffset, y+yoffset, direction)){
                    BoxAnimation(box, direction);
                    box.transform.position = new Vector2(box.transform.position.x + xoffset, box.transform.position.y + yoffset);
                    if(layout[x, y] == 2){
                        goalcurr--;
                    }
                    if(layout[x + xoffset, y + yoffset] == 2){
                        goalcurr++;
                        CheckCompletion();
                    }
                    return true;
                }
                box.GetComponent<Animator>().Play("boxerror");
                return false;
            }
            BoxAnimation(box, direction);
            box.transform.position = new Vector2(box.transform.position.x + xoffset, box.transform.position.y + yoffset);
            if(layout[x, y] == 2){
                goalcurr--;
            }
            if(layout[x + xoffset, y + yoffset] == 2){
                goalcurr++;
                CheckCompletion();
            }
            return true;
        }
        box.GetComponent<Animator>().Play("boxerror");
        return false;
    }

    void CheckCompletion(){
        if(goalcurr == goaltotal){
            blackout.GetComponent<blackoutscript>().End();
            Invoke(nameof(NextLevel), 0.5f);
        }
    }

    void NextLevel(){
        scenemanager.GetComponent<scenemanagerscript>().NextScene();
    }

    void BoxAnimation(GameObject box, int direction){
        string state = "boxidle";
        switch(direction){
            case 1:
            state = "boxleft";
            break;
            case 2:
            state = "boxup";
            break;
            case 3:
            state = "boxright";
            break;
            default:
            state = "boxdown";
            break;
        }
        box.GetComponent<Animator>().Play(state);


    }
}
