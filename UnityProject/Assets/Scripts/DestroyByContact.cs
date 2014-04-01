using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            //Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}	
