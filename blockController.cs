using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;     //사용시 꼭 작성

// IPointer, IDrag, IDrop 인터페이스를 2D/3D 오브젝트에서 사용할 때
// EventSystem(GameObject - UI) 오브젝트를 Hierarchy View에 생성해야 하고,
// 2D 오브젝트는 Main Camera에 Physics2DRaycaster 컴포넌트를
// 3D 오브젝트는 Main Camera에 PhysicsRaycaster 컴포넌트를 추가해야 합니다.


// 추가한 block을 드래그하여 슬롯에 넣는 class
public class blockController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;       //UI가 소속되어 있는 최상단의 Canvas Transform
    private Transform previousParent;   //object가 직전에 소속되어 있었던 부모 Transform
    private RectTransform rect;     //UI 위치 제어를 위한 RectTransform 
    private CanvasGroup canvasGroup;    //UI의 알파값과 상호작용 제어를 위한 CanvasGroup
    private ChangeBlock changeblock;

    private void Awake(){
        canvas = FindObjectOfType<Canvas>().transform;  //Canvas 스크립트를 가진 ui의 transform가져오기
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        changeblock = GetComponent<ChangeBlock>();
    }

    //// object 터치하는 순간 1회 호출(IPointerDownHandler)
    // public void OnPointerDown(PointerEventData eventData){

    // }

    // //object를 떼는 순간 1회 호출(IPointerUpHandler)
    // public void OnPointerUp(PointerEventData eventData){
    // }

    // //object 터치했다 떼는 순간 1회 호출(IPointerClickHandler)
    // public void OnPointerClick(PointerEventData eventData){

    // }

    //object 드래그하기 시작할 때 1회 호출(IBeginDragHandler)
    public void OnBeginDrag(PointerEventData eventData){
        // 드래그 직전에 소속되어 있던 부모 Transform 정보 저장
        previousParent = transform.parent;

        // 현재 드래그중인 UI가 화면의 최상단에 출력되도록 하기 위해
        transform.SetParent(canvas);    //부모 오브젝트를 Canvas로 설정
        transform.SetAsLastSibling();   //가장 앞에 보이도록 마지막 자식으로 설정

        // 드래그 가능한 오브젝트가 하나가 아닌 자식들을 가지고 있을 수도 있기 때문에 CanvasGroup으로 통제
        // 알파값(불투명도)을 0.6으로 설정, 광선 충돌처리가 되지 않도록 한다
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    //object를 드래그 중일 때 매 프레임 호출(IDragHandler)
    public void OnDrag(PointerEventData eventData){
    // 현재 화면에 인식된 클릭의 x,y,z좌표로 이동한다
        rect.position = Input.mousePosition;
    }


    //object 드래그 종료할 때 1회 호출(IEndDragHandler)
    public void OnEndDrag(PointerEventData eventData){
        // 드래그를 시작하면 부모가 canvas로 설정되기 때문에
        // 드래그를 종료할 때 부모가 canvas이면 아이템 슬롯이 아닌 엉뚱한 곳에
        // 드롭을 했다는 뜻이기 때문에 드래그 직전에 소속되어 있던 아이템 슬롯으로 아이템 이동
        if(transform.parent == canvas){
            returnToPlace();    //원래 자리로 돌아가기
        }
        else{   // 아이템 슬롯에 잘 들어갔을 때
            Transform currentParent = transform.parent; // ui가 들어간 아이템 슬롯
            if(currentParent.transform.childCount > 1){  // 해당 슬롯의 자식이 둘일때
                Transform Child1 = currentParent.transform.GetChild(0);     // 자식1
                Transform Child2 = currentParent.transform.GetChild(1);     // 자식2
                if(Child1.name == Child2.name){     //자식의 preFabe이 서로 같을때(preFab 이름이 같을때)
                    Debug.Log("두 ui의 preFab이 같아요!!");
                    // 새로운 ui 생성
                    changeblock.changeBlock(currentParent, Child1, Child2);
                }
                else{
                    returnToPlace();    //원래 자리로 돌아가기
                }
            }

        }
        // 알파값(불투명도)을 1로 설정하고, 광선 충돌처리가 되도록 한다
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }

    // 마지막에 소속되어있었던 previousParent의 자식으로 설정하고, 해당 위치로 설정
    public void returnToPlace(){         
        transform.SetParent(previousParent);
        rect.position = previousParent.GetComponent<RectTransform>().position;
    }


    // //object 드래그 종료할 때 1회 호출(IDropHandler)
    // //해당 함수를 가진 object 위로 다른 object를 drop할 때 1회 호출
    // public void OnDrop(PointerEventData eventData){

    // }


}
