using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor2 : MonoBehaviour {

    public Animator _animator;

    private bool isOpen = false;


	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            _animator.SetBool("isDoorOpen", true);
            isOpen = true;
        }

        else if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            _animator.SetBool("isDoorOpen", false);
            isOpen = false;
        }
        
    }
}
