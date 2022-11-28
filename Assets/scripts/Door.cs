using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int numEnemies;
    public GameObject roomTrigger;
    public bool roomClear = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(roomClear == false)
        {
            transform.position += new Vector3(0, 10);
        }
        if(numEnemies<=0)
        {
            roomClear = true;
        }    
        if(roomClear == true)
        {
            transform.position -= new Vector3(0, 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.gameObject.CompareTag("Player"))
        {
            roomClear = false;
            Destroy(roomTrigger);
        }
    }
}
