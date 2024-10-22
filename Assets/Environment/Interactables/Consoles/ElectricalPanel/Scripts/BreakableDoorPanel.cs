using System.Collections.Generic;
using Environment.ObjectAttributes.Interfaces;
using UnityEngine;

namespace Environment.Interactables.Consoles.ElectricalPanel.Scripts
{
    public class BreakableDoorPanel : MonoBehaviour, IBreakable
    {
        [SerializeField] GameObject beacon;
        [SerializeField] GameObject brokenPanel;
        [SerializeField] GameObject fixedPanel;
        [SerializeField] ParticleSystem sparks;
        bool isBroken;
        void Start()
        {
            isBroken = false;
        }
        public void Break()
        {
            isBroken = true;
            beacon.SetActive(false);
            StartCoroutine(PlaySparks(0.5f));
            brokenPanel.SetActive(true);
            fixedPanel.SetActive(false);
        }
        public bool IsBroken()
        {
            return isBroken;
        }
        // play sparks for a few seconds 
        IEnumerator<WaitForSeconds> PlaySparks(float seconds)
        {
            sparks.Play();
            yield return new WaitForSeconds(seconds);
            sparks.Stop();
        }
    }
}
