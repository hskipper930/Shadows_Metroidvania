using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Steven Thompson & Hunter Skipper
 * SGD 285.2144
 * 12/5/2022 */
public class Obstacle : MonoBehaviour
{
    public Movement player;
    public GameObject logPickup;
    public Transform pickupSpot;
    [SerializeField] private TMPro.TMP_Text logsText;


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
            logsText.text = "Logs Collected: " + player.numLogs + "/3";
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
