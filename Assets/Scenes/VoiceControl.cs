using FrostweepGames.SpeechRecognition.Utilites;
using UnityEngine;
using UnityEngine.UI;

namespace FrostweepGames.SpeechRecognition.Google.Cloud.Examples
{
    public class VoiceControl : MonoBehaviour
    {
        private ILowLevelSpeechRecognition _speechRecognition;

        private Button _startRecordButton;

        private Image _speechRecognitionState;

        private Text _speechRecognitionResult;

        private Dropdown _languageDropdown;

        private bool recordButtonPressed;
        private bool recording;


        private void Start()
        {
            _speechRecognition = SpeechRecognitionModule.Instance;
            _speechRecognition.SpeechRecognizedSuccessEvent += SpeechRecognizedSuccessEventHandler;
            _speechRecognition.SpeechRecognizedFailedEvent += SpeechRecognizedFailedEventHandler;

            _startRecordButton = transform.Find("Canvas/Button_StartRecord").GetComponent<Button>();

            _speechRecognitionResult = transform.Find("Canvas/Text_Result").GetComponent<Text>();

            _languageDropdown = transform.Find("Canvas/Dropdown_Language").GetComponent<Dropdown>();

            _startRecordButton.interactable = true;

            _languageDropdown.ClearOptions();

            recordButtonPressed = false;
            recording = false;

            for (int i = 0; i < 43; i++)
            {
                _languageDropdown.options.Add(new Dropdown.OptionData(((Enumerators.Language)i).ToString()));
            }

            _languageDropdown.onValueChanged.AddListener(LanguageDropdownOnValueChanged);

            _languageDropdown.value = 5;//set default to English/US

        }

        private void Update()
        {
            if (!recording && recordButtonPressed)
            {
                //_startRecordButton.interactable = false;
                //_speechRecognitionState.color = Color.red;
                _speechRecognitionResult.text = "Korgi is listening";
                _speechRecognition.StartRecord();
                print("Start recording speech");
                recording = true;
            }else if (recording && !recordButtonPressed)
            {
                ApplySpeechContextPhrases();
                print("Stop recording");
                //_stopRecordButton.interactable = false;
                //_speechRecognitionState.color = Color.yellow;
                _speechRecognition.StopRecord();
                recording = false;
            }
        }

        public void onPointerDownRecordButton()
        {
            recordButtonPressed = true;
            print("button pushed down");
        }
        public void onPointerUpRecordButton()
        {
            recordButtonPressed = false;
            print("button raised up");
        }

        private void OnDestroy()
        {
            _speechRecognition.SpeechRecognizedSuccessEvent -= SpeechRecognizedSuccessEventHandler;
            _speechRecognition.SpeechRecognizedFailedEvent -= SpeechRecognizedFailedEventHandler;
        }



        private void LanguageDropdownOnValueChanged(int value)
        {
            _speechRecognition.SetLanguage((Enumerators.Language)value);
        }

        private void ApplySpeechContextPhrases()
        {
            //add context phrases
            string[] phrases = { "sit down", "stand up", "lay down", "bark","korgi"};
            _speechRecognition.SetSpeechContext(phrases);
        }

        private void SpeechRecognizedFailedEventHandler(string obj)
        {
            _speechRecognitionResult.text = "Speech Recognition failed with error: " + obj;

            _startRecordButton.interactable = true;
        }

        private void SpeechRecognizedSuccessEventHandler(RecognitionResponse obj)
        {
            _startRecordButton.interactable = true;

            if (obj != null && obj.results.Length > 0)
            {
                _speechRecognitionResult.text = "You said: " + obj.results[0].alternatives[0].transcript;

                string other = "\nOr: ";

                foreach (var result in obj.results)
                {
                    foreach (var alternative in result.alternatives)
                    {
                        if (obj.results[0].alternatives[0] != alternative)
                            other += alternative.transcript + ", ";
                    }
                }

                _speechRecognitionResult.text += other;
            }
            else
            {
                _speechRecognitionResult.text = "Speech Recognition succeeded! Words are no detected.";

            }
        }
    }
}