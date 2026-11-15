using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource _audioSource;

    [SerializeField] public AudioClip _winSound;
    [SerializeField] public AudioClip _gameOverSound;
	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioClip _backgroundMusic;

	private void Awake() 
    {
        if (Instance == null)
        {
			Instance = this;
            DontDestroyOnLoad(gameObject);
			_audioSource = GetComponent<AudioSource>();
		}
		else
        {
            Destroy(gameObject);
        }
    }

    public void PlayWinSound()
    {
		StopBackgroundMusic();
		_audioSource.PlayOneShot(_winSound);
    }

    public void PlayGameOverSound() 
    {
		StopBackgroundMusic();
		_audioSource.PlayOneShot(_gameOverSound);
    }

	public void PlayBackgroundMusic()
	{
		if(_musicSource.isPlaying) return;

		_musicSource.clip = _backgroundMusic;
		_musicSource.loop = true;
		_musicSource.Play();
	}
	public void StopBackgroundMusic()
	{
		_musicSource.Stop();
	}

	public void RestartBackgroundMusic()
	{
		_musicSource.Stop();
		_musicSource.clip = _backgroundMusic;
		_musicSource.loop = true;
		_musicSource.Play();
	}

	public void RestartGame()
	{
		RestartBackgroundMusic();
		UnityEngine.SceneManagement.SceneManager.LoadScene("Level_01");
	}
}
