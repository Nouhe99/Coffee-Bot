using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    //ControlsVariables
    [Header("Control Variables")]
    public Transform origin;
    public SugarCube sucrePref;
    public Vector2 MinMaxShootForce;
    public float timeToMaxForce;
    private float shootTime;

   


    //Trajectoire Variables
    [Header("Line renderer veriables")]
    public LineRenderer line;
    [Range(2, 30)]
    public int resolution;

    [Header("Formula variables")]
    public Vector2 velocity;
    public float yLimit;
    private float g;

    [Header("Linecast variables")]
    [Range(2, 30)]
    public int linecastResolution;
    public LayerMask canHit;
    public SpriteRenderer dotPF;
    private List<SpriteRenderer> dots = new List<SpriteRenderer>();

    public UnityEvent OnThrow;

    private void Start()
    {
        g = Mathf.Abs(Physics2D.gravity.y);

        for(int i = 0; i < linecastResolution; i++)
        {
            dots.Add(Instantiate(dotPF, transform.position-Vector3.one*100,Quaternion.identity));
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            shootTime = Time.time;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            RenderArc();
        }
        else
        {
            for (int i = 0; i < linecastResolution; i++)
            {
                dots[i].transform.position = - Vector3.one * 100;
            }
        }

        Controlers();
    }

    public void Controlers()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ShootSugar(sucrePref, SugarType.white);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ShootSugar(sucrePref, SugarType.brown);
        }
    }

    private void ShootSugar(SugarCube Sugar, SugarType type)
    {
        float shootForce = Mathf.Lerp(MinMaxShootForce.x, MinMaxShootForce.y, Mathf.Clamp01((Time.time - shootTime) / timeToMaxForce));

        SugarCube s = Instantiate(Sugar, transform.position, Quaternion.identity);
        s.type = type;
        s.velocity = new Vector2(shootForce, shootForce);
        OnThrow.Invoke();
    }

    private void RenderArc()
    {
        float shootForce = Mathf.Lerp(MinMaxShootForce.x, MinMaxShootForce.y, Mathf.Clamp01((Time.time - shootTime) / timeToMaxForce));
        velocity = new Vector2(shootForce, shootForce);

        line.positionCount = resolution + 1;

        Vector3[] lineArr = CalculateLineArray();

        line.SetPositions(lineArr);

        for(int i = 0; i < dots.Count; i++)
        {
            dots[i].transform.position = lineArr[Mathf.Min(i, dots.Count - 1)];
        }
    }

    
    private Vector3[] CalculateLineArray()
    {
        Vector3[] lineArray = new Vector3[resolution + 1];

        var lowestTimeValue = MaxTimeX() / resolution;

        for (int i = 0; i < lineArray.Length; i++)
        {
            var t = lowestTimeValue * i;
            lineArray[i] = CalculateLinePoint(t);
        }

        return lineArray;
    }

    private Vector2 HitPosition()
    {
        var lowestTimeValue = MaxTimeY() / linecastResolution;

        for (int i = 0; i < linecastResolution + 1; i++)
        {
            var t = lowestTimeValue * i;
            var tt = lowestTimeValue * (i + 1);

            var hit = Physics2D.Linecast(CalculateLinePoint(t), CalculateLinePoint(tt), canHit);

            if (hit) return hit.point;
        }

        return CalculateLinePoint(1.5f);
    }

    private Vector2 CalculateLinePoint(float t)
    {
        float x = velocity.x * t;

        float y = (velocity.y * t)-(g * Mathf.Pow(t, 2) / 2);

        return new Vector2(x + transform.position.x, y + transform.position.y);

        //pos=new Vector2(
    }

    private float MaxTimeY()
    {
        var v = velocity.y;
        var vv = v * v;

        var t = (v + Mathf.Sqrt(vv + 2 * g * (transform.position.y - yLimit))) / g;
        return t;
    }

    private float MaxTimeX()
    {
        var x = velocity.x;
        if (x == 0)
        {
            velocity.x = 000.1f;
            x = velocity.x;
        }

        var t = (HitPosition().x - transform.position.x) / x;
        return t;
    }
}
