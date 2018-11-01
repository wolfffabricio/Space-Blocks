using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    Vector2 movement;
    float timerScore;
    public Button btPlayAgain;
    public Button btPause;
    public AudioSource audioSourceMenuManager;
    Vector3 position;
    public MenuManager menuManager;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        timerScore += Time.deltaTime;

#if !UNITY_EDITOR

        Touch touch = Input.GetTouch(0);
      
        if (touch.phase == TouchPhase.Moved)
        {
            
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.y = rb.position.y;
            rb.position = touchPosition;
        }
#else
        Vector2 movement = new Vector2();

        movement.x = Input.GetAxis("Horizontal") * speed;
        //movement.y = Input.GetAxis("Vertical") * speed;

        rb.velocity = movement;
#endif
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Block")
        {
            if (PlayerPrefs.GetFloat("TimerScore") < timerScore)
            {
                PlayerPrefs.SetFloat("TimerScore", timerScore);
                menuManager.updateHighScore();
            }
            menuManager.ActivePlayAgain();
            btPause.gameObject.SetActive(false);
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                if (audioSourceMenuManager.isPlaying)
                {
                    audioSourceMenuManager.Stop();
                }
            }
        }
    }
}
