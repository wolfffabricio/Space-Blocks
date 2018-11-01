using UnityEngine;
using System.Collections;

public class Blocks : MonoBehaviour {

    public Rigidbody2D rb;
    //public float speed = 8;
    float instantiationTimer = 0.5f;
    Vector2 startPosition;
    public Vector3[] positions;
    //public Blocks blockPrefab;
    //Blocks block;

    // Use this for initialization
    void Start () {

        //rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        randomBlocksPositons();
    }

    // Update is called once per frame
    void Update()
    {
        //if (rb.velocity.magnitude > speed)
        //    rb.velocity = rb.velocity.normalized * speed;

        if(transform.position.y <= -5.17)
        {
            randomBlocksPositons();
        }
    }

    void randomBlocksPositons()
    {
        instantiationTimer -= Time.deltaTime;
        if(instantiationTimer <= 0)
        {

            Vector2 pos = new Vector2(Random.Range(-3f, 3f), 5.20f);
            Vector2 scale = new Vector2(Random.Range(1.5f, 3.4f), Random.Range(0.4f, 0.7f));
            transform.position = pos;
            transform.localScale = scale; 

            //block = Instantiate(blockPrefab);

            //int randomNumbers = Random.Range(1, positions.Length);
            //transform.position = positions[randomNumbers];
            //positions[randomNumbers] = positions[0];
            //positions[0] = transform.position;
            //transform.position = positions[randomNumbers];

            instantiationTimer = 0.5f;
        }
    }
}
