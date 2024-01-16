using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<UIBase> uiList = new List<UIBase>(); 
    private Stack<UIBase> uiStack = new Stack<UIBase>();

    public void OpenWindow(GameObject uiPrefab)   //UI창을 열기 위한 메서드 /
    {
        UIBase uiInstance = Instantiate(uiPrefab, transform).GetComponent<UIBase>();
        if(uiStack.Count>0)    //처음 열리는 창이 아닐 때에는
        {
            uiStack.Peek().gameObject.SetActive(false);   // 기존에 열려있던 창을 비활성화. Peek()이란 스택 맨 위를 확인하는 메서.
        }
        uiStack.Push(uiInstance);  //ui프리팹을 열어줌
    }


    public void GoBack()     //뒤로가기 버튼용
    { 
    if(uiStack.Count>1)
        {
            UIBase curUIWindow = uiStack.Pop();    //uiStack에서 
            Destroy(curUIWindow.gameObject);       //현재 열려있는 창 파괴
            uiStack.Peek().gameObject.SetActive(true); //이전에 Stack에 저장된 ui창을 true로 돌림
        }
    }
}
