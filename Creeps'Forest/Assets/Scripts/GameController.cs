using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    //Author name : Tom TsilioPoulos
    public bool gameOver = false;
    public bool restart = false;
    [Header("Enemies")]
    public int numberOfZombies;
    public int numberOfSkeletones;
    public int numberOfGhosts;

    public GameObject zombie;
    public GameObject skeletone;
    public GameObject ghost;

    private float time = 0f;

    public List<GameObject> zombies;
    public List<GameObject> skeletones;
    public List<GameObject> ghosts;

    [Header("ScoreBoard")]
    [SerializeField]
    private int _lives;
    [SerializeField]
    private int _score;

    public Text livesLabel;
    public Text scoreLabel;
    public Text gameOverLabel;
    public Text restartLabel;

    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("UI Control")]
    public GameObject StartLabel;
    public GameObject StartButton;

    public int Lives
    {

        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            livesLabel.text = "Lives: " + _lives.ToString();
        }
    }
    public int Score
    {
        get
        {
            return _score;

        }
        set
        {
            _score = value;
            scoreLabel.text = "Score : " + _score.ToString();
        }
    }

    void Start()
    {
        switch (SceneManager.GetActiveScene().name) // Differs starting options by scene name.
        {
            case "Start":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                break;
            case "main":
                StartLabel.SetActive(false);
                StartButton.SetActive(false);
                break;
            case "End":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                StartLabel.SetActive(false);
                StartButton.SetActive(false);
                break;
        }
        Lives = 5;
        Score = 0;
        Spawn();
        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
            {
            AudioSource activeAudioSource = audioSources[(int)activeSoundClip];
            activeAudioSource.playOnAwake = true;
            activeAudioSource.loop = true;
            activeAudioSource.volume = 0.5f;
            activeAudioSource.Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 3f && gameOver == false) // Setting of enemy spawning delay.
            //The delay is set by seconds which can be adjust through changing float number before && in the first condition//
            //Current delay is 3 secs as the condition is time >= 3f
        {
            time = time % 1f; // Reseting time value for spawning delay
            Spawn();
            Debug.Log("Enemies spawned");
        }
        if(restart == true) // Allowing player to restart game by activing main scene when the game is overd.
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    void Spawn() //Monster spawning method which is automaticaally called by specific time dealy.
        //The loops are instantiating monster game objects by set number of monsters.
    {
        zombies = new List<GameObject>();
        skeletones = new List<GameObject>();
        ghosts = new List<GameObject>();

        for (int zombieNum = 0; zombieNum < numberOfZombies; zombieNum++)
        {
            zombies.Add(Instantiate(zombie));
        }

        for (int skeletoneNum = 0; skeletoneNum < numberOfSkeletones; skeletoneNum++)
        {
            skeletones.Add(Instantiate(skeletone));
        }

        for (int ghostNum = 0; ghostNum < numberOfGhosts; ghostNum++)
        {
            ghosts.Add(Instantiate(ghost));
        }

        ghosts.Add(Instantiate(ghost));
    }
    public void GameOver()// Game over method. It stops playing the main theme, displaying game over screen, theme,
        // and allows user to restart game.
    {
        gameOver = true;
        restart = true;
        gameOverLabel.enabled = true;
        restartLabel.enabled = true;
        audioSources[(int)SoundClip.Game_Theme].loop = false;
        audioSources[(int)SoundClip.Game_Theme].Stop();
        audioSources[(int)SoundClip.GameOver].Play();
        
        
    }
    public void OnStartButtonClick() // A method for starting the game by clicking the button on start scene.
    {
        SceneManager.LoadScene("Main");
    }
}
