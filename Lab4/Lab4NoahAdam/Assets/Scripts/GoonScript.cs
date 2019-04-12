using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GoonScript : MonoBehaviour
{
    public EnemyWaveController bigBoss;
    Vector3 targetLocation;
    void Start()
    {
        bigBoss = transform.parent.GetComponent<EnemyWaveController>();
    }

    void Update()
    {
        targetLocation = GetComponent<NavMeshAgent>().destination;//Just give me an easy variable for that target, Boss
        if (bigBoss.currentPhase == EnemyWaveController.WavePhase.Retreat)//If the GOONS are retreating and they get close enough to target remove them
        {
            if (Vector3.Distance(targetLocation, transform.position) <= 2)
            {
                bigBoss.RemoveGoon(this.gameObject);//Boss we didn't get them but we got out alive, that's good right?
                Destroy(this.gameObject);//Goon literally disappears but surely the B I G  B O S S let them live despite their failure...right?
            }
        }
        int rnd = Random.Range(0, 1000);
        if (rnd <= 2)
        {
            //GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Hit player");
        }
        if (collision.transform.tag == "Projectile")
        {
            Debug.Log("Been hit");
            bigBoss.gcInstance.killCount++;//Boss...I've been hit. Tell the B I G G E R   B O S S they got me man...
            bigBoss.RemoveGoon(this.gameObject);//Boss systematically removes any trace this goon existed
            Destroy(this.gameObject);//Goon literally disappears like a mirage
        }
    }
}
