using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarCube : MonoBehaviour
{
    //public float speed;
    int i = 0;


    public SugarType type; //Set this when instantiated
    [HideInInspector]
    public Vector2 velocity;
    private Vector3 p0;

    public Sprite SucreBlanc;
    public Sprite SucreRoux;

    private float timeStamp;
    //public LineRenderer line;

    private void Start()
    {

        timeStamp = Time.time;
        p0 = transform.position;
    }

    void ChangeType()
    {
        if(type == SugarType.white)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = SucreBlanc;
        }
         if (type == SugarType.brown)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = SucreRoux;
        }
    }
  

    private void Update()
    {
        Debug.Log(Mathf.Abs(Physics2D.gravity.y));
        transform.position = p0+ContinuousParabolicMotion(velocity);
        ChangeType();

        if(Time.time-timeStamp > 10)
        {
            Destroy(this.gameObject);
        }
    }
    private Vector3 ContinuousParabolicMotion(Vector2 velocity)
    {
        float dt = Time.time -timeStamp;
        float dtSqr = Mathf.Pow(dt,2);

        float x = velocity.x * dt;

        float y = (velocity.y * dt) - (Mathf.Abs(Physics2D.gravity.y) * dtSqr / 2);

        return new Vector3(x, y, 0);

    }
}
