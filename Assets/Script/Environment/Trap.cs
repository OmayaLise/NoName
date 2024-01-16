using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] int damage;
    private ContactPoint2D[] contacts = new ContactPoint2D[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            collision.gameObject.GetComponent<PlayerHealth>().PlayerLooseHealth(damage);

            //collision.GetContacts(contacts);
            //Vector2 pushDirection = contacts[0].point;

            //collision.gameObject.GetComponent<PlayerHealth>().PushBack(pushDirection);   
        }
        
    }
   
}
