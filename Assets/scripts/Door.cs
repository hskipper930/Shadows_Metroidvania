using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Steven Thompson
 * SGD 285.2144
 * 12/5/2022 */
public class Door : MonoBehaviour
{
    public int numEnemies;
    public GameObject roomTrigger;
    public bool roomClear = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         
       
    }
    public void roomCheck()
    {

        if (roomClear == false)
        {
            transform.position += new Vector3(0, 10);
        }
        if (roomClear == true)
        {
            transform.position -= new Vector3(0, 10);
        }
    }

   /* private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.gameObject.CompareTag("Player"))
        {
            roomClear = false;
            Destroy(roomTrigger);
        }
    }*/
}
