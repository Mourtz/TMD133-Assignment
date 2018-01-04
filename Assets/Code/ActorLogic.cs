using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActorLogic : MonoBehaviour {

    public static Rigidbody2D rigidBody2D;

    public bool startStatic = true;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        if(startStatic) rigidBody2D.bodyType = RigidbodyType2D.Static;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.volume = Mathf.Min(rigidBody2D.velocity.magnitude/10.0f, 1f);
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
            SG.Finish();
    }
}
