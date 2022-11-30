using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Movement player;
    public GameObject logPickup;
    public Transform pickupSpot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.gameObject.CompareTag("Tree")&& collision.gameObject.CompareTag("PlayerAxe"))
        {

            player.numLogs++;
            Debug.Log("Logs picked up: " + player.numLogs);
            //Instantiate(logPickup,pickupSpot);
            Destroy(gameObject, .2f);

        }
        else if(this.gameObject.CompareTag("Boulder") && collision.gameObject.CompareTag("PlayerAxe") && player.isSharp == true)
        {
            Destroy(gameObject);
            Debug.Log("Boulder down");
        }
    }
   
}
