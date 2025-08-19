using UnityEngine;

public class Results : MonoBehaviour
{
    [SerializeField] AudioSource hitSoundEffect;
    public void PlayHitSound()
    {
        hitSoundEffect.PlayOneShot(hitSoundEffect.clip);
    }
}
