using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedByPass : MonoBehaviour
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
        if (Player.gameObject.tag == "Red")
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
