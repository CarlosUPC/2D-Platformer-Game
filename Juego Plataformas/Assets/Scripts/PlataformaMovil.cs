using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour {

    public Transform target;//variable necesaría para poder utilizar en esta script sus propiedades transform (position)
    public float speed;//variable para regular la velocidad

    private Vector3 start, end; //variables de tipo vector para representar las posiciones x,y,z de los objetos
	// Use this for initialization
	void Start () {

		if(target != null)
        {
            target.parent = null; // anulamos las posiciones absolutas del padre para no afectar a las relativas de los hijos
            start = transform.position;
            end = target.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        // vector que moverá la plataforma siempre hacía la posicion del target en cada frame
        if(target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
        }


        //Condicion ternaria que hará cambiar la posición del target
        if(transform.position == target.position)
        {
            target.position = (target.position == start) ? end : start;
        }
    }
}
