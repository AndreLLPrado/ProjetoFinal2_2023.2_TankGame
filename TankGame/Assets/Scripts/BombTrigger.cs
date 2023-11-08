using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    [SerializeField]
    private float radius;

    private List<GameObject> inArea;

    private SphereCollider collider;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
        collider.radius = radius;
        inArea = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            inArea.Add(other.gameObject);
            Debug.Log(other.gameObject.name);
        }
    }

    public List<GameObject> getInAreaList()
    {
        return inArea;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
