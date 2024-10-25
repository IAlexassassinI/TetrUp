using UnityEngine;
using UnityEngine.UIElements;

public class HeightCalculator : MonoBehaviour
{
    [SerializeField]
    private static float moveIncrement = 2f;

    [SerializeField]
    private static float boxSize = 200f;

    [SerializeField]
    private static float heightSize = 2f;

    private static Vector3 colliderSides = new Vector3(boxSize, heightSize, boxSize);

    public static uint CalculateHeight(Vector3 origin)
    {
        uint madeIterations = 0;

        while (true)
        {
            Vector3 center = origin + new Vector3(0f, madeIterations * moveIncrement, 0f);
            Collider[] colliders = Physics.OverlapBox(center, colliderSides / 2, Quaternion.identity);
            bool hitStationary = false;

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("IsStationare"))
                {
                    hitStationary = true;
                    break;
                }
            }

            if (!hitStationary)
            {
                break;
            }

            madeIterations++;
        }

        return (madeIterations - 1);
    }
}
