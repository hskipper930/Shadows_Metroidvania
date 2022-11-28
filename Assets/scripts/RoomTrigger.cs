using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public Door door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(door.numEnemies<= 0)
        {
            
                door.roomClear = true;
            door.roomCheck();

            this.gameObject.SetActive(false);


        }
    }
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            door.roomClear = false;
            door.roomCheck();
            //this.gameObject.SetActive(false);
        }
    }
}
