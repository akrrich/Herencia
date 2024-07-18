using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private VictorController victor;
    private Collider2D victorCollider;

    private Scene currentScene;

    [SerializeField] private AudioSource laboratorioMapMusic;   
    [SerializeField] private AudioSource bosqueMapMusic;
    [SerializeField] private AudioSource palacioMapMusic;

    [SerializeField] private AudioSource journalMapMusic;
    [SerializeField] private AudioSource bossMapMusic;
    [SerializeField] private AudioSource boosterMapMusic;


    private bool laboratorioMusic = false;
    private bool bosqueMusic = false;
    private bool palacioMusic = false;  

    private bool journalMusic = false;
    private bool bossMusic = false;
    private bool boosterMusic = false;


    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        CheckIfIsPaused(laboratorioMapMusic);
        CheckIfIsPaused(bosqueMapMusic);
        CheckIfIsPaused(palacioMapMusic);
        CheckIfIsPaused(journalMapMusic);
        CheckIfIsPaused(bossMapMusic);
        CheckIfIsPaused(boosterMapMusic);
    }

    private void StartMapMusic(ref bool mapMusic, AudioSource audioMapMusic)
    {
        if (!mapMusic)
        {
            audioMapMusic.Play();
            mapMusic = true;
        }
    }

    private void StopMapMusic(ref bool mapMusic, AudioSource audioMapMusic)
    {
        audioMapMusic.Stop();
        mapMusic = false;
    }

    private void CheckIfIsPaused(AudioSource audioMapMusic)
    {
        if (GameManager.Instance.IsPaused)
        {
            audioMapMusic.Pause();
        }

        else
        {
            audioMapMusic.UnPause();
        }
    }

    private void CheckMapForHisOwnMusic()
    {
        switch (currentScene.name)
        {
            case "Laboratorio":
                StartMapMusic(ref laboratorioMusic, laboratorioMapMusic);
                break;

            case "BosqueMuerto":
                StartMapMusic(ref bosqueMusic, bosqueMapMusic);
                break;

            case "ElPalacio":
                StartMapMusic(ref palacioMusic, palacioMapMusic);
                break;
        }
    }


    public void Stay(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "JournalMap":
                StartMapMusic(ref journalMusic, journalMapMusic);
                StopMapMusic(ref laboratorioMusic, laboratorioMapMusic);
                StopMapMusic(ref bosqueMusic, bosqueMapMusic);
                StopMapMusic(ref palacioMusic, palacioMapMusic);
                break;

            case "BossMap":
                StartMapMusic(ref bossMusic, bossMapMusic);
                StopMapMusic(ref laboratorioMusic, laboratorioMapMusic);
                StopMapMusic(ref bosqueMusic, bosqueMapMusic);
                StopMapMusic(ref palacioMusic, palacioMapMusic);
                break;

            case "BoosterMap":
                StartMapMusic(ref boosterMusic, boosterMapMusic);
                StopMapMusic(ref laboratorioMusic, laboratorioMapMusic);
                StopMapMusic(ref bosqueMusic, bosqueMapMusic);
                StopMapMusic(ref palacioMusic, palacioMapMusic);
                break;

            default:
                CheckMapForHisOwnMusic();
                break;
        }
    }

    public void Exit(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "JournalMap":
                StopMapMusic(ref journalMusic, journalMapMusic);
                break;

            case "BossMap":
                StopMapMusic(ref bossMusic, bossMapMusic);
                break;

            case "BoosterMap":
                StopMapMusic(ref boosterMusic, boosterMapMusic);
                break;
        }
    }
}
