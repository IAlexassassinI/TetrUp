using UnityEngine;
using System.Collections;

public class WindGenerator : MonoBehaviour
{
    public float currentHeight;
    public Vector3 boxSize = new Vector3(20f, 5f, 20f);
    public float windAcceleration = 1f;

    [SerializeField]
    private float minInterval = 1f;

    [SerializeField]
    private float maxInterval = 5f;

    [SerializeField]
    private float heightCoef = 2f;

    private void Start()
    {
        
        StartCoroutine(ApplyWindEffectAtIntervals());
    }

    private IEnumerator ApplyWindEffectAtIntervals()
    {
        while (true) 
        {
            
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            
            ApplyWindEffect();
        }
    }

    private void ApplyWindEffect()
    {
        
        float randomHeight = Random.Range(0f, TowerManager.Instance.GetCurrentTowerHeight() * heightCoef);

        Vector3 boxCenter = new Vector3(transform.position.x, randomHeight, transform.position.z);


        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2f, Quaternion.identity);


        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                Vector3 windDirection = Vector3.right;
                rb.AddForce(windDirection * windAcceleration, ForceMode.Acceleration);
            }
        }

        Debug.Log($"Wind applied at Height: {randomHeight} with {colliders.Length} affected objects.");
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Vector3 boxCenter = new Vector3(transform.position.x, currentHeight / 2, transform.position.z);
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
