using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    private Vector3 bossPosition;

    private float damageValue = 3f;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        bossPosition = FindObjectOfType<Boss>().transform.position;

        float AngleRad = Mathf.Atan2(bossPosition.y - transform.position.y, bossPosition.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg + 90);

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(bossPosition.x, bossPosition.y, transform.position.z), 7f*Time.deltaTime);

        if(transform.position.x == bossPosition.x) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Boss"){
            other.GetComponent<Boss>().Damage(damageValue);
            audioManager.Play("MageFire");
            Destroy(this.gameObject);
        }
    }
}
