using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip hit, buy;
    [SerializeField] private AudioSource music, sound;
    public List<AudioClip> musicTracks;
    public AudioSource audioSource;
    private int currentTrackIndex = 0;
    private List<int> playedTracksIndices;

    private void Start()
    {
        DamagePerClick.OnClickHit += PlayHitSound;
        UpgradeManager.upgrade += SoundBuy;
        playedTracksIndices = new List<int>();
        if (musicTracks.Count > 0)
        {
            audioSource.clip = musicTracks[0];
            audioSource.Play();
        }
    }
    void Update()
    {
        if (!audioSource.isPlaying && musicTracks.Count > 0)
        {
            PlayRandomTrack();
        }
    }

    void PlayRandomTrack()
    {
        if (playedTracksIndices.Count == musicTracks.Count)
        {
            playedTracksIndices.Clear(); // �� ����� ��������, �������� ����� ����
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, musicTracks.Count);
        } while (playedTracksIndices.Contains(randomIndex));

        playedTracksIndices.Add(randomIndex);
        audioSource.clip = musicTracks[randomIndex];
        audioSource.Play();

        Debug.Log($"Now playing track: {musicTracks[randomIndex].name}");
    }
    public void SetMusic(float value)
    {
        music.volume = value;
    }
    public void SetSound(float value)
    {
        sound.volume = value;
    }
    private void PlayHitSound(Vector3 vector3)
    {
        sound.PlayOneShot(hit);
    }
    private void SoundBuy()
    {
        sound.PlayOneShot(buy);
    }
}
