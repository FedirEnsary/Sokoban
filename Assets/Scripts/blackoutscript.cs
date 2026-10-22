using UnityEngine;

public class blackoutscript : MonoBehaviour
{
    bool ending = false;
    bool starting = true;
    int counter = 25;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(starting){
            counter--;
            transform.localScale = new Vector3(1f*counter*counter, 1f*counter*counter, 1);
        }
        else if(ending){
            transform.localScale = new Vector3(8000f/(counter*counter/2), 8000f/(counter*counter/2), 1);
            counter--;
        }
        if(counter == 0){
            starting = false;
            ending = false;
            transform.eulerAngles = new Vector3(0, 0, 90f);
        } 
    }

    public void End(){
        ending = true;
        counter = 50;
    }
}
