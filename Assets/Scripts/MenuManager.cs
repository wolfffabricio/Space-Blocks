using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static MenuManager Instance { get; private set; }
    public Image soundTarget, pauseTarget;
    public Sprite soundOn, soundOff, play, pause;
    public AudioSource audioSource;
    public AudioClip backgroundSoundMenu;
    public Text tvScore;
    public Text tvHighScore;
    public Text devText;
    public Text devEmail;
    public float timer = 0.0f;
    public float timerColors = 0.0f;
    public Image panel;
    public Button btPlay;
    public Button btHome;
    public Button btPlayAgain;
    public SpriteRenderer[] spritesRenderer;
    public Color[] colors;
    public Image[] imagesColors;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start() {

        devText.text = "2016. Developed by Fabrício Wolff";
        devEmail.text = "fabricionewnj@hotmail.com";
        Time.timeScale = 1;
        setRandomColors();
        updateHighScore();
        if (SceneManager.GetActiveScene().name == "Game")
        {
            audioSource.mute = (PlayerPrefs.GetInt("mute") == 1 ? true : false);
        }
        
    }

    // Update is called once per frame
    void Update() {

        //Testa se está na cena certa.
        if (tvScore)
        {
            timer += Time.deltaTime;

            tvScore.text = string.Format("{0:#0}.{1:0}",
            Mathf.Floor(timer) % 7200,//seconds
            Mathf.Floor((timer * 10) % 10))//miliseconds
            .ToString();
        }

        timerColors += Time.deltaTime;

        if (timerColors >= 0.4f)
        {
            setRandomColors();
            timerColors = 0;
        }
    }

    void setRandomColors()
    {
        int index = Random.Range(1, colors.Length);
        Color newColor = colors[index];
        colors[index] = colors[0];
        colors[0] = newColor;
        foreach (var ren in spritesRenderer)
        {
            ren.color = newColor;
        }

        foreach (var image in imagesColors)
        {
            image.color = newColor;
        }

    }

    public void startGame()
    {
        PlayerPrefs.SetInt("mute", (audioSource.mute) ? 1 : 0);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("Game");
    }

    public void LoadScene()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void soundControl()
    {
        if (!audioSource.mute)
        {
            soundTarget.sprite = soundOff;
            audioSource.mute = true;

        } else { 

            soundTarget.sprite = soundOn;
            audioSource.mute = false;
        }
    }

    public void pauseGame()
    {
        if (Time.timeScale == 1)
        {
            pauseTarget.sprite = play;
            panel.gameObject.SetActive(true);
            btPlay.gameObject.SetActive(true);
            btHome.gameObject.SetActive(true);
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            Time.timeScale = 0;
        }
        //else
        //{
        //    pauseTarget.sprite = pause;
        //    if (!audioSource.isPlaying)
        //    {
        //        audioSource.UnPause();
        //    } 
        //    Time.timeScale = 1;
        //}
    }

    public void playGame()
    {
        if(Time.timeScale == 0)
        {
            pauseTarget.sprite = pause;
            panel.gameObject.SetActive(false);
            btPlay.gameObject.SetActive(false);
            btHome.gameObject.SetActive(false);
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
            Time.timeScale = 1;
        }
    }

    public void homeGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void updateHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("TimerScore");
        if (tvHighScore)
        {
            tvHighScore.text = /*"BEST HIGHSCORE: " +*/ string.Format("{0:00}.{1:00}",
                    Mathf.Floor(highScore) % 7200,//seconds
                    Mathf.Floor((highScore * 100) % 100))//miliseconds
                    .ToString();
        }
    }

    public void ActivePlayAgain()
    {
        btPlayAgain.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        btHome.gameObject.SetActive(true);
    }
}
