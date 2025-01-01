using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class knockbackFeedback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rgbd;
    [SerializeField]
    private float strength = 0.25f, delay = 1f;

    public UnityEvent OnBegin, OnDone;
    public void PlayFeedback(GameObject sender){
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rgbd.AddForce(direction* strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    
    private IEnumerator Reset(){
        yield return new WaitForSeconds(delay);
        rgbd.linearVelocity = Vector2.zero;
        OnDone?.Invoke();

    }
    
}
