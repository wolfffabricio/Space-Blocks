using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public float speed = 10;
    float currentSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (currentSpeed <= 17)
        {
            currentSpeed = speed + MenuManager.Instance.timer / 10;
        }

        transform.Translate(Vector2.down * currentSpeed * Time.deltaTime);
        
    }
}
