using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Speaker
{
    Soo,
    ah
}

[System.Serializable]
public struct Dialog
{
    public Speaker speaker; // 화자
    [TextArea(3, 5)]
    public string dialogue; // 대사
}


public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Dialog[] dialogs; // 현재 분기의 대사 목록
    [SerializeField]
    private Image[] imageDialogs; // 대화창 이미지UI 
    [SerializeField]
    private TMP_Text[] textNames; // 현재 대사중인 캐릭터 이름 출력 텍스트UI
    [SerializeField]
    private TMP_Text[] textDialogues; // 현재 대사 출력 텍스트UI
    [SerializeField]
    private GameObject[] objectArrows; // 대사가 완료되었을 때 출려고디는 커서 오브젝트
    [SerializeField]
    private float typingSpeed; // 텍스트 타이핑 효과의 재생 속도
    [SerializeField]
    private KeyCode keyCodeSkip = KeyCode.Space; // 타이핑 효과를 스킵하는 키

    private int curIndex = -1;
    private bool isTypingEffect = false; // 텍스트 타이핑 효과를 재생중인지
    private Speaker curSpeaker = Speaker.Soo;

    public void Setup()
    {
        for(int i = 0; i < 2; i++)
        {

        }
    }
}
