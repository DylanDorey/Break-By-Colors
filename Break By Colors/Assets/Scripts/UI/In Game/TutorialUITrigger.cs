using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUITrigger : MonoBehaviour
{
    private GameObject t1Screen, t2Screen, t3Screen, t4Screen, t5Screen, t6Screen, t7Screen;
    public GameObject[] tutorialScreens;
    public int index;

    private void Start()
    {
        t1Screen = UIManager.Instance.t1Screen;
        t2Screen = UIManager.Instance.t2Screen;
        t3Screen = UIManager.Instance.t3Screen;
        t4Screen = UIManager.Instance.t4Screen;
        t5Screen = UIManager.Instance.t5Screen;
        t6Screen = UIManager.Instance.t6Screen;
        t7Screen = UIManager.Instance.t7Screen;

        tutorialScreens = new GameObject[7] { t1Screen, t2Screen, t3Screen, t4Screen, t5Screen, t6Screen, t7Screen };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.EnableTrigger(tutorialScreens[index]);
        }
    }
}