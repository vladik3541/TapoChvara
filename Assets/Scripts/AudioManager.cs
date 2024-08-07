using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip hit, buy;
    [SerializeField] private AudioSource music, sound;

    private void Start()
    {
        DamagePerClick.OnClickHit += PlayHitSound;
        UpgradeManager.upgrade += SoundBuy;
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
