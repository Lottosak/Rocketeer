using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour {
    public enum State
    {
        Alive, Dying, Transcending
    }

    [SerializeField] AudioClip winAudio;
    [SerializeField] AudioClip destroyedAudio;
    private AudioSource audioSource;

    public static State state = State.Alive;
	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (state != State.Alive) return;

        switch (col.gameObject.tag)
        {
            case "Enemy":
                WinSequence();
                break;
            case "Finish":
                DeathSequence();
                break;
            default:

                break;
        }
    }

    private void DeathSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(winAudio);
        state = State.Transcending;
        Invoke("LoadNextLevel", 1f);
    }

    private void WinSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(destroyedAudio);
        state = State.Dying;
        Invoke("LoadFirstLevel", 1f);
    }

    private void LoadFirstLevel()
    {
        state = State.Alive;
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        state = State.Alive;
        SceneManager.LoadScene(1);
    }
}
