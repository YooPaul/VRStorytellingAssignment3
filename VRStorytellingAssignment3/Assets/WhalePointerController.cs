using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhalePointerController : MonoBehaviour {

    private float timer;

    private bool isGazing;

	// Use this for initialization
	void Start () {
        timer = 0f;
        isGazing = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (isGazing)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= 2.0f)
            {
                this.GetComponent<Animator>().SetBool("isGazing", true);
            }
        }
        else
        {
            this.GetComponent<Animator>().SetBool("isGazing", false);
            timer = 0;
        }
	}


    public void OnPointerEnter()
    {

        isGazing = true;
        
    }

    public void OnPointerExit()
    {
        isGazing = false;
        
    }
}
