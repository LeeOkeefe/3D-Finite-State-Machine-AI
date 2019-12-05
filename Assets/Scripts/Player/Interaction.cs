using System.Linq;
using Objects;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal sealed class Interaction : MonoBehaviour
    {
        private void Update()
        {
            CheckInteractiveObjects();
        }

        /// <summary>
        /// Calls interact on any interactive objects we hit with an <see cref="Physics.OverlapSphere"/>
        /// </summary>
        private void CheckInteractiveObjects()
        {
            var hits = Physics.OverlapSphere(transform.position, 0.8f);

            if (!hits.Any(h => h.CompareTag("InteractiveObject")))
                return;

            var interactiveObjects = hits.Where(h => h.CompareTag("InteractiveObject"));

            foreach (var interactiveObject in interactiveObjects)
            {
                if (Vector3.Distance(transform.position, interactiveObject.transform.position) < 3 && Input.GetKeyDown(KeyCode.E))
                {
                    var interactive = interactiveObject.transform.GetComponent(typeof(IInteractive)) as IInteractive;
                    interactive?.Interact();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("InteractiveObject"))
            {
                StartCoroutine(GameManager.Instance.ShowMessage("Press 'E' to interact."));
            }
        }
    }
}
