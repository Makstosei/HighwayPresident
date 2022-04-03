using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private float explosionPower = 1;
    private float explosionRadius = 2;
    private float randomz;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Guard")
        {
            other.gameObject.transform.SetParent(null);
            other.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.transform.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, other.transform.localPosition, explosionRadius);
            other.gameObject.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, Random.Range(3, 7), 0), ForceMode.Impulse);
            other.gameObject.transform.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(10, 50), Random.Range(10, 50), Random.Range(10, 50)));
            other.tag = "Crashed";
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.AddComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            while (randomz == 0)
            {
                randomz = Random.Range(-3, 3);
            }
            gameObject.transform.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, other.transform.localPosition, explosionRadius);
            gameObject.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, Random.Range(4, 6), randomz), ForceMode.Impulse);
            gameObject.transform.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(10, 50), Random.Range(10, 50), Random.Range(10, 50)));
        }
        else if (other.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.AddComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            while (randomz == 0)
            {
                randomz = Random.Range(-3, 3);
            }
            gameObject.transform.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, other.transform.localPosition, explosionRadius);
            gameObject.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, Random.Range(4, 6), randomz), ForceMode.Impulse);
            gameObject.transform.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(10, 50), Random.Range(10, 50), Random.Range(10, 50)));
            other.GetComponent<PlayerHealthSystem>().TakeDamage(1);
        }

    }
}
