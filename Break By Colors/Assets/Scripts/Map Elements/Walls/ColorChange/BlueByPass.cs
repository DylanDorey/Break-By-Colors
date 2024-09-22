using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueByPass : MonoBehaviour
{

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (Player.gameObject.tag == "Blue")
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
