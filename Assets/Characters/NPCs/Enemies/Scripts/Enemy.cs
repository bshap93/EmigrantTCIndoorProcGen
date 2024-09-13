using UnityEngine;

namespace Characters.NPCs.Enemies.Scripts
{
    public class Enemy : MonoBehaviour

    {
        public void TakeAction()
        {
            Debug.Log($"{gameObject.name} is taking its turn.");
        }
    }
}
