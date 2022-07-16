using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;     //사용시 꼭 작성
using UnityEngine.UI;       //사용시 꼭 작성


public class blockController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }

    //object 터치 시작시 1회
    public void OnPointerDown(PointerEventData eventData){

    }

    //object 클릭 or 터치중일때 매 프레임   
    public void OnDrag(PointerEventData eventData){

        // 현재 화면에 인식된 클릭의 x,y,z좌표로 이동한다
        rectTransform.position = Input.mousePosition;

    }

    //object 터치 종료시 1회
    public void OnPointerUp(PointerEventData eventDate){
    }
}