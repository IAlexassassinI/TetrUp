using UnityEngine;

public class CameraRiser : MonoBehaviour
{
    [SerializeField]
    private float riseSpeed = 2.0f;

    [SerializeField]
    private float cameraHightCoef = 1.5f;

    private float targetHeight;
    private float baseHeight;

    void Start()
    {
        baseHeight = transform.position.y;
        targetHeight = transform.position.y;
    }

    void Update()
    {
        if (transform.position.y != targetHeight)
        {
            Vector3 newPosition = new Vector3(
                transform.position.x,
                Mathf.MoveTowards(transform.position.y, targetHeight, riseSpeed * Time.deltaTime),
                transform.position.z
            );

            transform.position = newPosition;
        }
    }

    
    public void UpdateTargetHeight(uint deltaHeightIter)
    {
        targetHeight = baseHeight + deltaHeightIter * cameraHightCoef;
    }
}
