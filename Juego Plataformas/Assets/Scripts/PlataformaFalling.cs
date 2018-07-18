using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFalling : MonoBehaviour {

    public float fallDelay = 1f;
    public float respawnDelay = 5f;

    private Rigidbody2D rb2d;
    private PolygonCollider2D pc2d;

    private Vector3 start;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        start = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", fallDelay);
            Invoke("Respawn", fallDelay + respawnDelay);
        }
    }

    void Fall()
    {
        rb2d.isKinematic = false;
        pc2d.isTrigger = true; //activamos el triger para que no le afecten las colisiones de los demas objetos (plataformar)
    }

    void Respawn()
    {
        transform.position = start;
        rb2d.isKinematic = true;
        rb2d.velocity = Vector3.zero; //reseteo de velocidad porque cuando va cayendo va incrementando la velocidad por la gravedad
        pc2d.isTrigger = false; // reset el trigger para que el player le afecta la colision
    }
}
