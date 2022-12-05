using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Steven Thompson
 * SGD 285.2144
 * 12/5/2022 */
public class PlayerProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;
    public Rigidbody2D rb;
   
    public GameObject Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        //rb.velocity = transform.right * speed;
        if (Player.GetComponent<Movement>().isFacingRight == true)
        {
            rb.velocity = transform.right * speed;
        }
        else if (Player.GetComponent<Movement>().isFacingRight == false)
        {
            rb.velocity = -transform.right * speed;
        }

    }
   
    // Update is called once per frame
    void Update()
    {
       /* if (Player.GetComponent<Movement>().isFacingRight == true)
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
        else if(Player.GetComponent<Movement>().isFacingRight == false)
        {
            transform.position += -transform.right * Time.deltaTime * speed;

        }*/


        Object.Destroy(gameObject, .5f);
    }
   
}
