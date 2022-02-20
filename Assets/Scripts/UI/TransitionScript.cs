using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    private Image image;
    private GameObject transitionPanel;

    // Start is called before the first frame update
    void Start()
    {
        transitionPanel = transform.gameObject;
        image = transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        image.enabled = true;
        CharacterController2D playerController = GameObject.Find("Player").GetComponent<CharacterController2D>();
        playerController.StopMovement();
        playerController.enabled = false;
        for (float i = 1.5f; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(0, 0, 0, i);
            yield return null;
        }
        // fade from transparent to opaque
        playerController.enabled = true;
        image.enabled = false;
    }
}
