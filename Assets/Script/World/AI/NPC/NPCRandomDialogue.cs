using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CleverCrow.Fluid.Dialogues.DialogueHandler;
using CleverCrow.Fluid.Dialogues.Graphs;

[RequireComponent(typeof(BoxCollider))]
public class NPCRandomDialog : MonoBehaviour
{
    [SerializeField] private List<string> _dialog = new List<string>();
    [SerializeField] private GameObject _dialogUI;
    [SerializeField] private float _speedFactorTemp = .001f;
    [SerializeField] private DialogueGraph _randomDialogue;
    private bool _encounterTriggered = false;
    private int _dialogIndex = 0;
    private bool _breakRandomEncounter = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (_encounterTriggered) return;

            _encounterTriggered = true;
            // StartCoroutine(randomDialog(_dialog[_dialogIndex]));
            _dialogUI.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _dialogUI.SetActive(false);
            _breakRandomEncounter = true;
        }
    }


    // private IEnumerator randomDialog(string dialog)
    // {
    //     _text.text = "";
    //     _state = State.Playing;
    //     int wordIndex = 0;

    //     while (_state != State.Completed)
    //     {
    //         _text.text += dialog[wordIndex];
    //         yield return new WaitForSeconds(_speedFactorTemp * 0.05f);
    //         if (++wordIndex == dialog.Length)
    //         {
    //             _state = State.Completed;
    //         }
    //     }

    //     yield return new WaitForSeconds(1);
    //     if (++_dialogIndex < _dialog.Count && !_breakRandomEncounter)
    //     {
    //         StartCoroutine(randomDialog(_dialog[_dialogIndex]));
    //         yield return 0;
    //     }
    // }
}
