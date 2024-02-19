using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//public enum Speaker
//{
//    Soo,
//    ah
//}

[System.Serializable]
public struct Dialog
{
    //public string speaker; // 화자
    [TextArea(3, 5)]
    public string dialogue; // 대사
}


public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Dialog[] dialogs; // 현재 분기의 대사 목록
    [SerializeField]
    private Image imageDialog; // 대화창 이미지UI 
    [SerializeField]
    private TMP_Text textDialogue; // 현재 대사 출력 텍스트UI
    [SerializeField]
    private GameObject objectArrow; // 대사가 완료되었을 때 출려고디는 커서 오브젝트
    [SerializeField]
    private float typingSpeed; // 텍스트 타이핑 효과의 재생 속도

    private int curIndex = -1;
    private bool isTypingEffect = false; // 텍스트 타이핑 효과를 재생중인지

    private Coroutine typingCoroutine;
    public void Setup()
    {
        //for(int i = 0; i < 2; i++)
        //{
        //    // 모든 대화 관련 게임 오브젝트 비활성화
        //    InActiveObjects(i);
        //}
        InActiveObjects();
        
        SetNextDialog();
    }

    public bool UpdateDialog()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // 텍스트 타이핑 효과를 재생중일때 마우스 왼쪽 클릭하면 타이핑 효과 종료
            if(isTypingEffect == true)
            {
                //타이핑 효과를 중지하고, 현재 대사 전체를 출력한다
                StopCoroutine(typingCoroutine);
                isTypingEffect = false;
                textDialogue.text = dialogs[curIndex].dialogue;
                //대사가 완료되었을때 출력되는 커서 활성화
                objectArrow.SetActive(true);

                return false;
            }

            //다음 대사 진행
            if(dialogs.Length > curIndex + 1)
            {
                SetNextDialog();
            }
            // 대사가 더이상 없을경우 true반환
            else
            {
                //모든 캐릭터 이미지를 어둡게 설정
                //for(int i = 0; i < 2; ++i)
                //{
                //    //모든 대화 관련 게임오브젝트 비활성화
                //    InActiveObjects(i);
                //}
                InActiveObjects();
                return true;
            }
        }

        return false;
    }

    private void SetNextDialog()
    {
        //이전 화자의 대화 관련 오브젝트 비활성화
        InActiveObjects();

        curIndex++;

        //현재 화자 설정
        //curSpeaker = dialogs[curIndex].speaker;

        //대화창 활성화
        imageDialog.gameObject.SetActive(true);

        // 화자의 대사 텍스트 활성화 및 설정 (Typing Effect)
        textDialogue.gameObject.SetActive(true);
        if(typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(nameof(TypingText));
    }

    private void InActiveObjects()
    {
        imageDialog.gameObject.SetActive(false);
        textDialogue.gameObject.SetActive(false);
        objectArrow.SetActive(false);
    }

    private IEnumerator TypingText()
    {
        int index = 0;

        isTypingEffect = true;

        //텍스트를 한글자씩 타이핑치듯 재생
        while(index < dialogs[curIndex].dialogue.Length)
        {
            textDialogue.text = dialogs[curIndex].dialogue.Substring(0, index);

            index++;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;

        //대사가 완료되었을 때 출력되는 커서 활성화
        objectArrow.SetActive(true);
    }
}
