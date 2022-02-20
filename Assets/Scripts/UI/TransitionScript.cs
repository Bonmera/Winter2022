using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
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
        transform.gameObject.SetActive(true);
        GameObject.Find("Player").GetComponent<CharacterController2D>().enabled = false;
        
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(0, 0, 0, i);
            yield return null;
        }
        // fade from transparent to opaque

        transform.gameObject.SetActive(false);
        GameObject.Find("Player").GetComponent<CharacterController2D>().enabled = true;
    }



}
