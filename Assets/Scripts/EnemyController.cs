using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject back;
    [SerializeField] GameObject forth;
    float phase = 0;
    float speed = 1;
    float phaseDirection = 1;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(back.GetComponent<Rigidbody2D>().transform.position, forth.GetComponent<Rigidbody2D>().transform.position, phase);
        phase += Time.deltaTime * speed * phaseDirection;
        if(phase >= 1 || phase <= 0) phaseDirection *= -1;
    }
}
