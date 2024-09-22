using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorChange : MonoBehaviour
{
    public GameObject Obstacle;
    public GameObject Player;
    private Renderer Ren;
    public Material red;
    public Material blue;
    public Material yellow;
    public Material green;
    public List<Material> ListMat;
    public string Red;
    public string Blue;
    public string Yellow;
    public string Green;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerExit(Collider other)
    {
        List<Material> ListMat = new List<Material>() {red, blue, yellow, green};
        if (Obstacle.gameObject.tag == "Wall")
        {
            int hold = Random.Range(0, 4);
            Ren = GetComponent<Renderer>();
            Ren.material = ListMat[hold];

            if (hold == 0)
            {
                Player.tag = Red;

            }
            if (hold == 1)
            {
                Player.tag = Blue;

            }
            if (hold == 2)
            {
                Player.tag = Yellow;

            }
            if (hold == 3)
            {
                Player.tag = Green;

            }

        }
        
    }

}
