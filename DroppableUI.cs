using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;     //사용시 꼭 작성
using UnityEngine.UI;



//ui 옮기기, 슬롯에 ui 넣기
public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    private RectTransform rect;
    
    private void Awake() {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    //마우스or터치 포인터가 현재 아이템 슬롯 영역 내부로 들어갈 때 1회 호출(IPointerEnterHandler)
    public void OnPointerEnter(PointerEventData eventData){
        // 아이템 슬롯의 색상을 노란색으로 변경
        image.color = Color.green;
    }

    // 마우스 포인터가 현재 아이템 슬롯 영역을 빠져나갈 때 1회 호출(IDropHandler)
    public void OnPointerExit(PointerEventData eventData){
        // 아이템 슬롯의 색상을 하얀색으로 변경
        image.color = Color.white;
    }

    // 현재 아이템 슬롯 영역 내부에서 드롭을 했을 때 1회 호출(IPointerExitHandler)
    public void OnDrop(PointerEventData eventData){
        //해당 슬롯에 넣기
        //pointerDrage는 현재 드래그하고 있는 대상(=아이템)
        if(eventData.pointerDrag != null){
            // 드래그하고 있는 대상의 부모를 현재 오브젝트로 설정
            eventData.pointerDrag.transform.SetParent(transform);
            // 위치를 현재 오브젝트 위치와 동일하게 설정
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }
}
