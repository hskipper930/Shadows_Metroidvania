using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public Door door;
    public Movement player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(player.hasKey == true)
            {

                Destroy(door.gameObject);
                player.hasKey = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
