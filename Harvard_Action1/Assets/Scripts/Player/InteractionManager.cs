using System.Collections;
using System.Collections.Generic;
using TMPro; // Only if we want to show text.
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
  private IInteractable curInteractable;
  private GameObject curInteractGameObject;
  private float lastCheckTime;
  public LayerMask layerMask;
  public TextMeshProUGUI promptText;
  public float raycastRate = 0.05f; // If we want to show text, would need to use a raycast. Probably too complicated?

  /*void Update() {
    if (Time.time - lastCheckTime > checkRate) {
      lastCheckTime = Time.time;
    }
  }*/

  /*public void OnInteractInput(InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Started && curInteractable != null) {
      curInteractable.OnInteract();
      curInteractable = null;
      curInteractGameObject = null;
      // promptText.gameObject.SetActive(false);
    }
  }*/

  void SetPromptText() {
    promptText.gameObject.SetActive(true);
    promptText.text = string.Format("[E] {0}", curInteractable.GetInteractPrompt());
  }
}

public interface IInteractable {
  string GetInteractPrompt(); // Idea would be that we could show text over items / doors, etc. that we might want to give players a hint.
  void OnInteract();
}
