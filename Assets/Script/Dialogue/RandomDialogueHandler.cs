// using System.Collections;
// using System.Linq;
// using CleverCrow.Fluid.Dialogues.Graphs;
// using TMPro;
// using UnityEngine;

// namespace CleverCrow.Fluid.Dialogues.RandomDialogueHandler
// {
//     public class RandomDialogueHandler : MonoBehaviour
//     {
//         public static RandomDialogueHandler instance;
//         private DialogueController _ctrl;

//         // private DialogueGraph _dialogue;


//         [Header("Graphics")]
//         public GameObject _dialogPanel;
//         [SerializeField] private TextMeshProUGUI _lines;
//         [SerializeField] private TextMeshProUGUI _name;

//         public RectTransform choiceList;
//         public ChoiceButton choicePrefab;

//         private void Awake()
//         {
//             if (instance == null)
//                 instance = this;
//             else if (instance != this)
//                 Destroy(gameObject);

//             var database = new DatabaseInstanceExtended();
//             _ctrl = new DialogueController(database);
//         }

//         public void playDialog(NPCDialog npcDialog)
//         {

//             _dialogPanel.SetActive(true);

//             // @NOTE If you don't need audio just call _ctrl.Events.Speak((actor, text) => {}) instead
//             _ctrl.Events.SpeakWithAudio.AddListener((actor, text, audioClip) =>
//             {
//                 if (audioClip) Debug.Log($"Audio Clip Detected ${audioClip.name}");

//                 ClearChoices();
//                 _name.text = actor.DisplayName;
//                 _lines.text = text;

//                 StartCoroutine(NextDialogueClick());
//             });

//             _ctrl.Events.Choice.AddListener((actor, text, choices) =>
//             {
//                 ClearChoices();
//                 _name.text = actor.DisplayName;
//                 _lines.text = text;


//                 choices.ForEach(c =>
//                 {
//                     var choice = Instantiate(choicePrefab, choiceList);
//                     choice.title.text = c.Text;
//                     choice.clickEvent.AddListener(_ctrl.SelectChoice);
//                 });
//             });

//             _ctrl.Events.End.AddListener(() =>
//             {
//                 _dialogPanel.SetActive(false);
//                 npcDialog.dialogDone();

//             });

//             // _ctrl.Events.NodeEnter.AddListener((node) =>
//             // {
//             //     Debug.Log($"Node Enter: {node.GetType()} - {node.UniqueId}");
//             // });

//             _ctrl.Play(npcDialog.getDialogueGraph(), gameObjectOverrides.ToArray<IGameObjectOverride>());
//         }

//         private void ClearChoices()
//         {
//             foreach (Transform child in choiceList)
//             {
//                 Destroy(child.gameObject);
//             }
//         }

//         private IEnumerator NextDialogueClick()
//         {
//             yield return null;

//             while (!Input.GetMouseButtonDown(0) || !CharacterInput.instance.globalEnterTrigger)
//             {
//                 yield return null;
//             }

//             _ctrl.Next();
//         }

//         private void Update()
//         {
//             // Required to run actions that may span multiple frames
//             _ctrl.Tick();
//         }
//     }
// }
