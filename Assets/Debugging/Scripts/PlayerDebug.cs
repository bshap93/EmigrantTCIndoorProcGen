using UnityEngine;

namespace Debug.Scripts
{
    public class PlayerDebug : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        void OnCollisionEnter(Collision other)
        {
            UnityEngine.Debug.Log("Player collided with " + other.gameObject.name);
        }
    }
}