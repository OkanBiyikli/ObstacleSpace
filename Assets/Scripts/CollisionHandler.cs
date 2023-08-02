using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    public float loadLevelDelay = 2f;

    public AudioClip crushSFX;
    public AudioClip successSFX;

    public ParticleSystem crushParticle;
    public ParticleSystem successParticle;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    AudioSource audioSource;
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        LevelCheating();
    }
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled){ return; }
        else
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This thing is friendly");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//mevcut olduğumuz sahnenin indexini getir
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//mevcut olduğumuz sahnenin indexini getir
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crushSFX);
        crushParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadLevelDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    void LevelCheating()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
}
