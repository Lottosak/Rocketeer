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

    [SerializeField] ParticleSystem winParticles;
    [SerializeField] ParticleSystem destroyedParticles;

    private AudioSource audioSource;

    public static State state = State.Alive;
	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKey(KeyCode.L))
	    {
            LoadNextLevel();
	    }
	}

    void OnCollisionEnter(Collision col)
    {
        if (state != State.Alive) return;

        switch (col.gameObject.tag)
        {
            case "Enemy":
                DeathSequence();
                break;
            case "Finish":
                WinSequence();
                break;
            default:

                break;
        }
    }

    private void WinSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(winAudio);
        winParticles.Play();
        state = State.Transcending;
        Invoke("LoadNextLevel", 1f);
    }

    private void DeathSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(destroyedAudio);
        destroyedParticles.Play();
        state = State.Dying;
        Invoke("ReloadLevel", 1f);
    }

    private void ReloadLevel()
    {
        state = State.Alive;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        state = State.Alive;
        int curScene = SceneManager.GetActiveScene().buildIndex;
        if (curScene == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(curScene + 1);
        }
        
    }
}
