using UnityEngine;

namespace UI.Scripts
{
    public class VignetteController : MonoBehaviour
    {
        static readonly int Hit = Animator.StringToHash("Hit");
        static readonly int Heal = Animator.StringToHash("Heal");
        [SerializeField] Animator damageVignetteAnimator;
        [SerializeField] Animator healOxygenVignetteAnimator;

        void Start()
        {
            damageVignetteAnimator.gameObject.SetActive(false);
            healOxygenVignetteAnimator.gameObject.SetActive(false);
        }

        public void ShowDamageVignette()
        {
            damageVignetteAnimator.gameObject.SetActive(true);
            damageVignetteAnimator.SetTrigger(Hit);
        }

        public void ShowHealVignette()
        {
            healOxygenVignetteAnimator.gameObject.SetActive(true);
            healOxygenVignetteAnimator.SetTrigger(Heal);
        }
    }
}
