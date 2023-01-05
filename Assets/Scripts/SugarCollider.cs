using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SugarCollider : MonoBehaviour
{
    [HideInInspector]
    public int id; //Setup when instantited only, never ever touch otherwise or u die

    [SerializeField]
    public List<Sprite> spritesPerso = new List<Sprite>();

    public int maxSugarWhite;

    public int maxSugarBrown;

    [HideInInspector]
    public UnityEvent<int> OnSugarError;
    [HideInInspector]
    public UnityEvent<int> OnSugarSuccess;
    [HideInInspector]
    public UnityEvent<int> OnSugarCompleted;

    public Dictionary<SugarType, int> sugarNeeded = new Dictionary<SugarType, int>();

    private void ReceiveSugar(SugarType sgt)
    {
        if(sugarNeeded[sgt] <= 0)
        {
            OnSugarError.Invoke(id);
            return;
        }
        else
        {
            sugarNeeded[sgt] = sugarNeeded[sgt] - 1;
            OnSugarSuccess.Invoke(id);
        }

        if ((sugarNeeded[SugarType.brown] == 0) && (sugarNeeded[SugarType.white] == 0))
        {
            OnSugarCompleted.Invoke(id);
        }
    }

    public void KillCoffee()
    {
        Destroy(gameObject);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.otherCollider.tag == "Sugar")
        {
            SugarType sgt = collision.otherCollider.gameObject.GetComponent<SugarCube>().type;

            ReceiveSugar(sgt);

            Destroy(collision.otherCollider.gameObject);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.tag == "Sugar")
        {
            SugarType sgt = collision.attachedRigidbody.gameObject.GetComponent<SugarCube>().type;

            ReceiveSugar(sgt);

            Destroy(collision.attachedRigidbody.gameObject);
        }
    }
}