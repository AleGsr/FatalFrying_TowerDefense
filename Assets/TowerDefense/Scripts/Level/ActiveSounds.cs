using UnityEngine;

public class ActiveSounds : MonoBehaviour
{
    public AudioSource enemyDamage;
    public AudioSource enemyDie;
    public AudioSource makeDamage;
    public AudioSource shootSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void EnemyDamage()
    {
        enemyDamage.Play();
    }

    public void EnemyDie()
    {
        enemyDie.Play();
    }

    public void MakeDamage()
    {
        makeDamage.Play();
    }

    public void Shoot()
    {
        shootSound.Play();
    }
        


}
