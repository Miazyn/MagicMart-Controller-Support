using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RhythmGameManager : MonoBehaviour
{
    [Header("Rhythm Game")]
    public AudioSource rhythmMusic;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] int maxNotesToBeSpawned = 20;
    [SerializeField] int minNotesToBeSpawned = 15;
    bool StartPlaying = false;

    public static RhythmGameManager instance;
    GameManager manager;

    [SerializeField] int currentScore;
    float scoreDifference;

    [SerializeField] int scorePerNote = 100;
    [SerializeField] int scorePerGoodNote = 125;
    [SerializeField] int scorePerPerfectNote = 150;



    [Header("Effects")]
    [SerializeField] GameObject sparkleEffect1;
    [SerializeField] GameObject sparkleEffect2;
    [SerializeField] GameObject sparkleEffect3;
    [SerializeField] GameObject glow;
    [SerializeField] float timeUntilSFXDisable = 2.0f;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip arrowClickSound;
    [SerializeField] AudioSource soundAudioSource;


    int NotesToBeMade;
    int NotesLeft;
    float perfectScore, onePercent;
    [SerializeField] NoteSpawner noteSpawn;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] SceneMana sceneMana;

    //bool IsGameFinished = false;

    [SerializeField] GameObject Panel;

    Coroutine disableSFX;
    AudioSource managerMusic;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        soundAudioSource.clip = hitSound;
        rhythmMusic.clip = audioClips[Random.Range(0, audioClips.Length)];

        manager = GameManager.Instance;
        NotesToBeMade = Random.Range(minNotesToBeSpawned,maxNotesToBeSpawned);
        NotesLeft = NotesToBeMade;
        currentScore = 0;

        perfectScore = NotesToBeMade * scorePerPerfectNote;
        onePercent = perfectScore / 100;
        scoreText.text = 100.00 + "%";

        manager.ChangeGameState(GameManager.GameState.MiniRhythmGameState);
        managerMusic = manager.gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {

        if (!StartPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                managerMusic.volume = 0;
                Panel.SetActive(false);
                StartPlaying = true;
                rhythmMusic.Play();
                StartCoroutine(noteSpawn.CreateNotes(NotesToBeMade));
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)||
            Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            soundAudioSource.clip = arrowClickSound;
            soundAudioSource.Play();
        }

    }

    public void PercentageCalc()
    {
        NotesLeft--;
        float playerScore = NotesLeft * scorePerPerfectNote + currentScore;
        playerScore /= onePercent;
        scoreText.text = playerScore.ToString("F2") + "%";

        if(NotesLeft == 0)
        {
            //IsGameFinished = true;

            for(int i = 0; i < noteSpawn.spawnedNotes.Count; i++)
            {
                Destroy(noteSpawn.spawnedNotes[i]);
            }

            manager.RhythmGameScore = playerScore;
            StartCoroutine(BreathTime());
        }
    }

    IEnumerator BreathTime()
    {

        sparkleEffect1.SetActive(true);
        sparkleEffect2.SetActive(true);
        sparkleEffect3.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        sceneMana.LoadNextScene("TransitionScene");
    }

    public void NormalHit()
    {
        soundAudioSource.clip = hitSound;
        soundAudioSource.Play();

        glow.SetActive(true);
        DisableAllEffects();

        currentScore += scorePerNote; 
        PercentageCalc(); 
    }
    public void GoodHit() 
    {
        soundAudioSource.clip = hitSound;
        soundAudioSource.Play();

        sparkleEffect1.SetActive(false);
        sparkleEffect2.SetActive(false);
        sparkleEffect3.SetActive(false);
        glow.SetActive(true);
        DisableAllEffects();

        currentScore += scorePerGoodNote;
        PercentageCalc(); 
    }
    public void PerfectHit()
    {
        soundAudioSource.clip = hitSound;
        soundAudioSource.Play();

        sparkleEffect1.SetActive(true);
        sparkleEffect2.SetActive(true);
        sparkleEffect3.SetActive(true);
        glow.SetActive(true);
        DisableAllEffects();

        currentScore += scorePerPerfectNote;
        PercentageCalc();
    }
    private void DisableAllEffects()
    {
        if (disableSFX != null)
        {
            StopCoroutine(disableSFX);
        }
        disableSFX = StartCoroutine(DisableEffects());
    }
    public void NoteMiss()
    {
        PercentageCalc();
        sparkleEffect1.SetActive(false);
        sparkleEffect2.SetActive(false);
        sparkleEffect3.SetActive(false);
        glow.SetActive(false);
    }
    IEnumerator DisableEffects()
    {
        yield return new WaitForSeconds(timeUntilSFXDisable);
        sparkleEffect1.SetActive(false);
        sparkleEffect2.SetActive(false);
        sparkleEffect3.SetActive(false);
        glow.SetActive(false);
    }
}
