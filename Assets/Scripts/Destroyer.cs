using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the "Tetromino" tag or any specific condition
        if (other.gameObject.CompareTag("Tetranomino"))
        {
            Debug.Log("Tetromino fell below ground and will be destroyed: " + other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
