using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment.Interactables.SceneTransitions.Scripts
{
    public class SceneChangeTrigger : MonoBehaviour
    {
        public string sceneName;

        public void ChangeScene()
        {
            SceneManager.LoadScene(sceneName);
            gameObject.SetActive(false);
        }
    }
}
