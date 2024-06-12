using UnityEngine;
using CleverCrow.Fluid.Dialogues.DialogueHandler;
using CleverCrow.Fluid.Dialogues.Graphs;

[RequireComponent(typeof(BoxCollider))]
public class NPCDialog : MonoBehaviour
{
    [SerializeField] private VirtualCameraSwitcher _cameraSwitcher;
    [SerializeField] private DialogueGraph _dialogueGraph;
    private bool onDialog = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            HUDHandler.instance.roamingUIHandler.showInteractUI();
            onDialog = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player") && CharacterInput.instance.actionTrigger && !onDialog)
        {
            CharacterInput.instance.changePlayerState(PlayerState.Dialogue);
            HUDHandler.instance.roamingUIHandler.hideInteractUI();
            _cameraSwitcher.virtualMode();
            DialogueHandler.instance.playDialog(this);
            onDialog = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            HUDHandler.instance.roamingUIHandler.hideInteractUI();
            onDialog = false;
        }
    }

    public void dialogDone()
    {
        CharacterInput.instance.changePlayerState(PlayerState.FreeLook);
        _cameraSwitcher.roamingMode();
    }

    #region getter setter
    public DialogueGraph getDialogueGraph()
    {
        return _dialogueGraph;
    }
    #endregion
}
