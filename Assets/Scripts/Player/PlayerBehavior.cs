﻿using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	float velFactor = 10f;
	public bool grounded = true;

	public int health = 100;
	public int ammo = 10;
    Animator anim;

	public AudioClip shoot;
	AudioSource audio;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
         
		float direction = Input.GetAxis ("Horizontal");
		float accx = direction * velFactor;
        anim.SetFloat("speed", Mathf.Abs(accx));
		Vector2 acc = new Vector2 (accx, 0);
		if (accx > 0) {
			GetComponent<SpriteRenderer> ().flipX = false;
		}
		if (accx < 0) {
			GetComponent<SpriteRenderer> ().flipX = true;
		}
		//GetComponent<Rigidbody2D> ().MovePosition (transform.position + vel);
		GetComponent<Rigidbody2D> ().AddForce (acc);

		GetComponent<Rigidbody2D> ().MoveRotation (0); // no rotation

		if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) && grounded) {
			// jump
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 400));
			grounded = false;
		}

		if (Input.GetKeyDown (KeyCode.Space) && ammo > 0) {
			audio.PlayOneShot(shoot, 0.7F);
			if (!GetComponent<SpriteRenderer> ().flipX) {
				Instantiate (Resources.Load ("bullet"), transform.position + (new Vector3(2, 0)), transform.rotation);
			} else {
				GameObject bullet = (GameObject)Instantiate (Resources.Load ("bullet"), transform.position + (new Vector3(-2, 0)), transform.rotation);
				bullet.GetComponent<SpriteRenderer> ().flipX = true;
			}
			ammo--;
		}

		if (health <= 0) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("TitleScreen");
		}
	}
}
