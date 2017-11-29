﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairsAnimation : MonoBehaviour {
    
    private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire1G") || Input.GetAxisRaw("FireRT") > 0)
        {
            anim.Play();
        }
	}
}