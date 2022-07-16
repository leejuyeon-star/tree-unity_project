using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;     //사용시 꼭 작성
using UnityEngine;


//UI 사용하는 경우
public class BlockSpawner : MonoBehaviour, IPointerDownHandler
{ 
    [SerializeField]
    private GameObject boxPrefab; //복제할 원본
    
    [SerializeField]
    private int objectSpawnCount = 30;  //복제할 object 개수
    
    private int currentObjectCount = 0; //현재까지 생성한 object 개수

    private RectTransform rectTransform;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }

    //object 클릭 시작시 1회
    //boxPrefab을 복제하여 랜덤한 지점에 생성
    public void OnPointerDown(PointerEventData eventData){
        // 일정 개수만 생성하고 멈추기 위해 설정
        if(currentObjectCount+1 > objectSpawnCount){
            Debug.Log("no more spawning!! so many blocks!!");
            return;
        }
        
        //복제하여 생성하기(복제할 object'원본', 위치, 회전)
        GameObject clone = Instantiate(boxPrefab, new Vector3(Random.Range(-400,400), Random.Range(-20,20),0), Quaternion.Euler(0,0,0), GameObject.Find("Canvas").transform);

        
        //생성한 object 개수 1 증가
        currentObjectCount++;
    }

}

