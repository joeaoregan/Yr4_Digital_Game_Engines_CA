﻿// Joe O'Regan
// All levels
// Manage zombie health, and dying animation, and scores
// update the healthbar, and play sound effects
// Blood splatter particle system for zombies body and head activated on being wounded

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour {

	AudioSource zombieAudio;

	public AudioClip shotFX;

    public int currentHealth = 25;                			// Default enemy health value set to 15

	public Slider healthBar;

	public Canvas hideCanvas;

	//public GameObject healthBarHide;

	//public GameObject sliderShowHide;
	//public GameObject imageShowHide;

	//public ManageZombies zombieCount;
	//public GameObject zombieCounter;
	public GameObject bloodSplatter;

	bool alive;
	bool wounded;

	public bool isWounded() { return wounded; }
	public bool isAlive() { return alive; }
	public void setDead() {	alive = false; }

	private GameObject gc;									// Game Controller

	//private Animation anim;									// Animate the health bar canvas
	private Animator animator;								// Animate the health bar canvas

	public void HeadShot () {
		setHealth (0);
		animator.SetTrigger("Headshot");
	}

	public void setHealth(int health) { 
		currentHealth = health; 
		healthBar.value = currentHealth;					// Set the healthbar value
		//hideCanvas.gameObject.SetActive(false);
		//if (currentHealth <= 0)
		//	anim.Play("BloodStain");						// Show score and bloodstain instead of hiding canvas
		//	animator.SetTrigger("Headshot");
	}

	void Start(){
		//anim = hideCanvas.gameObject.GetComponent<Animation>();
		animator = hideCanvas.gameObject.GetComponent<Animator>();
		//hideCanvas = GameObject.GetComponent<Canvas> ();

		zombieAudio = GetComponent<AudioSource> ();			// Get the audio source component
		gc = GameObject.FindWithTag ("GameController");		// Get the game controller
		alive = true;
		wounded = false;

		//healthBar.gameObject.SetActive (false);
	}

	public void InjureZombie (int amount){
		DeductPoints (amount);
	}

    void DeductPoints(int DamageAmount)
    {
		if (alive) {
			currentHealth -= DamageAmount;            		// Decrement enemy health
			healthBar.value = currentHealth;				// Set the healthbar value
			zombieAudio.clip = shotFX;						// Load the effect into the audio source
			zombieAudio.Play ();							// Play the effect
			StartCoroutine (BloodSplatter ());

			//if (currentHealth <= 0) imageShowHide.gameObject.SetActive(true);
		}
    }

	IEnumerator BloodSplatter(){
		if (Random.Range(1,3) == 1)
			GetComponent<Animator> ().SetTrigger ("Hit1");
		else 
			GetComponent<Animator> ().SetTrigger ("Hit2");
		if (currentHealth > 0) GetComponent<Animator> ().SetTrigger ("Alive");

		wounded = true;										// Don't move the zombie if hit animation playing
		
		bloodSplatter.SetActive (true);						// Turn on the blood particles
		yield return new WaitForSeconds (0.7f);				// Show them for half a second
		bloodSplatter.SetActive (false);					// Turn them off again
	
		wounded = false;									// Move the zombie again

		if (currentHealth <= 0 && alive)           	   		// If the enemies health has run out
		{
			//healthBar.gameObject.SetActive (false);
			StartCoroutine (Death ());						// Kill the zombie
			//anim.Play("BloodStain");						// Show score and bloodstain instead of hiding canvas
		}
	}

	IEnumerator Death(){
		gc.GetComponent<ManageScore> ().BonusScore (25);

		alive = false;
		//Destroy (healthBar);
		//Debug.Log ("health bar");
		//healthBar.enabled = false;						// Disable the health bar slider when zombie is killed

		//sliderShowHide.SetActive(false);
		//imageShowHide.gameObject.SetActive(true);

		//healthBar.gameObject.SetActive (false);

		//hideCanvas.enabled = false;
		//hideCanvas.gameObject.SetActive(false);
		//anim.Play("BloodStain");							// Show score and bloodstain instead of hiding canvas
		animator.SetTrigger("Dead");
		//healthBarHide.SetActive(false);
		//healthBarHide.gameObject.SetActive (false);
		//healthBarHide.gameObject.enabled = false;
		//healthBarHide.transform.position.y = -0.1f;


		GetComponent<Animator> ().SetTrigger ("IsDying");
		gc.GetComponent<ManageZombies>().incrementZombies();// Increment the number of zombies killed
		yield return new WaitForSeconds (4); 
		Debug.Log ("Zombie Dying Animation");

		//Destroy(gameObject);              				// Destroy the game object the script is attached to
		//healthBar.enabled = false;						// Disable the health bar slider when zombie is killed
		GetComponent<CapsuleCollider> ().enabled = false;
		this.enabled = false;

		//Debug.Log("Zombie Killed");

		//healthBar.enabled = false;						// Disable the health bar slider when zombie is killed
		//Destroy(healthBar);

		//zombieCount.incrementZombies ();					// Increment the number of zombies killed
		//Debug.Log("Increment Zombies Kill Count");
	}
}


//public GameObject destroyZombie;							// Needed for separate head shots
/* 
    void Update()    {
		if (zombieHealth <= 0)                   			// If the enemies health has run out
		{
			Destroy(gameObject);              				// Destroy the game object the script is attached to
			//Destroy(destroyZombie);              			// Destroy the game object specified by destroyZombie - separate headshot script
			//if (gameObject.tag == "Zombie")				// If the object to be destroyed is a target
			zombieCount.incrementZombies ();				// Increment the number of zombies killed
        }
    }
    */

