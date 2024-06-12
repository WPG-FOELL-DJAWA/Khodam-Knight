using System.Collections;
using CleverCrow.Fluid.Databases;
using TMPro;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.DialogueHandler
{
    [RequireComponent(typeof(TypewriterUI))]
    public class DialogueHandler : MonoBehaviour
    {
        public static DialogueHandler instance;
        [SerializeField] private TypewriterUI _typewriterUI;
        private DialogueController _ctrl;

        [Header("Graphics")]
        public GameObject _dialogPanel;
        [SerializeField] private TextMeshProUGUI _lines;
        [SerializeField] private TextMeshProUGUI _name;

        [SerializeField] private RectTransform _choiceList;
        [SerializeField] private ChoiceButton _choicePrefab;

        private bool isChooseMode;
        [SerializeField] private float dialogueSkipDelay = 2;
        private float delayTemp;


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        private void Start()
        {
            var database = new DatabaseInstance();
            _ctrl = new DialogueController(database);
        }

        public void playDialog(NPCDialog npcDialog)
        {

            var database = new DatabaseInstance();
            _ctrl = new DialogueController(database);

            _dialogPanel.SetActive(true);

            // @NOTE If you don't need audio just call _ctrl.Events.Speak((actor, text) => {}) instead
            _ctrl.Events.SpeakWithAudio.AddListener((actor, text, audioClip) =>
            {
                if (audioClip) Debug.Log($"Audio Clip Detected ${audioClip.name}");

                ClearChoices();
                isChooseMode = false;
                _name.text = actor.DisplayName;
                StartCoroutine(_typewriterUI.TypeWriterTMP(_lines, text));
                // _lines.text = text;

                StartCoroutine(NextDialogueClick());
            });

            _ctrl.Events.Choice.AddListener((actor, text, choices) =>
            {
                ClearChoices();
                isChooseMode = true;
                _name.text = actor.DisplayName;
                StartCoroutine(_typewriterUI.TypeWriterTMP(_lines, text));
                // _lines.text = text;

                choices.ForEach(c =>
                {
                    var choice = Instantiate(_choicePrefab, _choiceList);
                    choice.title.text = c.Text;
                    choice.clickEvent.AddListener(_ctrl.SelectChoice);
                });
            });

            _ctrl.Events.End.AddListener(() =>
            {
                isChooseMode = false;
                _dialogPanel.SetActive(false);
                npcDialog.dialogDone();

            });

            _ctrl.Play(npcDialog.getDialogueGraph());
        }

        private void ClearChoices()
        {
            foreach (Transform child in _choiceList)
            {
                Destroy(child.gameObject);
            }
        }

        private IEnumerator NextDialogueClick()
        {
            yield return null;
            delayTemp = dialogueSkipDelay;

            while (!Input.GetMouseButtonDown(0) || _typewriterUI.getWritingStatus() != TypeMode.Done) // add space
            {
                if (Input.GetMouseButtonDown(0) && _typewriterUI.getWritingStatus() != TypeMode.Done && delayTemp <= 0) StartCoroutine(_typewriterUI.SpeedUp());
                yield return null;
            }

            _ctrl.Next();
            delayTemp = dialogueSkipDelay;
        }

        private void Update()
        {
            _ctrl.Tick();

            if (isChooseMode)
                if (_typewriterUI.getWritingStatus() == TypeMode.Done)
                    _choiceList.gameObject.SetActive(true);
                else
                    _choiceList.gameObject.SetActive(false);
        }

    }
}
