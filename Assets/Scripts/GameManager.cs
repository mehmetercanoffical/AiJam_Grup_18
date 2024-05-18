using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public AudioSource gameSource;
    void Start()
    {
        
    }

    public void OnPlayerActive(bool isActive)
    {
        Player.Instance.OnPlayerActive(isActive);
    }


    public void PlaySound(AudioClip clip)
    {
        gameSource.clip = clip;
        gameSource.Play();
    }

}
