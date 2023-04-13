using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private SpeakerUI[] speakers; //대화에 참여하는 캐릭터들의 UI 배엵
    [SerializeField]
    private DialogData[] dialogs; //현재 분기의 대사 목록 배열
    [SerializeField]
    private bool Dialoglnit = true; //자동 시작 여부
    [SerializeField]
    private bool dialogsDB = false; //DB를 통해 읽는것 설정

    public int currentDialogIndex = -1; //현재 대사 순번
    public int currentSpeakerIndex = 0; //현재 말을 하는 화자의 Speakers 배열 순번
    public float typingSpeed = 0.1f; //텍스트 타이핑 효과의 재생속도
    public bool isTypingEffect = false; //텍스트 타이핑 효과가 재생중인지 판단

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetActiveObjects(SpeakerUI speaker, bool visible)
    {
        speaker.imageDialog.gameObject.SetActive(visible);
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);
        //화살표 대사가 종료되었을 때만 활성화 되기 때문에
        speaker.objectArrow.SetActive(visible);

        Color color = speaker.imgCharacter.color;
        if(visible)
        {
            color.a = 1;
        }
        else
        {
            color.a = 0.2f;
        }
        speaker.imgCharacter.color = color;
    }

    private void SetAllClose()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            SetActiveObjects(speakers[i], false);
        }
    }

    private void SetNextDialog(int currentIndex)
    {
        SetAllClose();
        currentDialogIndex = currentIndex;
        currentSpeakerIndex = dialogs[currentDialogIndex].speakerUIindex;
        SetActiveObjects(speakers[currentSpeakerIndex], true);
        speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name;
        StartCoroutine("OnTypingText");
    }

    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;

        if(dialogs[currentDialogIndex].characterPath != "None")//None이 아닐겨우 DB에 넣어놓은 경로의 캐릭터의 이미지를 가져온다.
        {
            speakers[currentSpeakerIndex].imgCharacter.sprite =
                Resources.Load<Sprite>(dialogs[currentDialogIndex].characterPath);
        }

        while(index < dialogs[currentDialogIndex].dialogue.Length + 1)
        {
            speakers[currentSpeakerIndex].textDialogue.text =
                dialogs[currentDialogIndex].dialogue.Substring(0, index); //텍스트를 한글자씩 타이핑 재생
            index++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;

        speakers[currentSpeakerIndex].objectArrow.SetActive(true);
    }

    public bool UpdateDialog(int currentIndex, bool InitType)
    {
        if (Dialoglnit == true && InitType == true)
        {
            SetAllClose();
            SetNextDialog(currentIndex);
            Dialoglnit = false;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(isTypingEffect == true)
            {
                isTypingEffect = false;
                StopCoroutine("OnTypingText"); //타이핑 효과를 중지하고, 현재 대사 전체를 출력한다.
                speakers[currentIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
                //대사가 완료되었을때 커서
                speakers[currentSpeakerIndex].objectArrow.SetActive(true);

                return false;
            }

            if(dialogs[currentDialogIndex].nextindex !=-100)
            {
                SetNextDialog(dialogs[currentDialogIndex].nextindex);
            }
            else
            {
                SetAllClose();
                Dialoglnit = true;
                return true;
            }
        }

        return false;
    }

    private void Awake()
    {
        SetAllClose();
    }

    [System.Serializable]
    public struct SpeakerUI
    {
        public Image imgCharacter; //캐릭터 이미지
        public Image imageDialog; //대화창 ImageUI
        public Text textName; //현재 대사중인 캐릭터 이름 출력 TextUI
        public Text textDialogue; //현재 대사 출력 Text UI
        public GameObject objectArrow; //대사가 완료되었을때 출력하는 커서 오브젝트
    }

    [System.Serializable]
    public struct DialogData
    {
        public int index; //대사 번호
        public int speakerUIindex; //스피커 배열 번호
        public string name; //이름
        public string dialogue; //대사
        public string characterPath; //캐릭터 이미지 경로
        public int tweenType; //트윈번호
        public int nextindex; //다음 대사
    }
}
