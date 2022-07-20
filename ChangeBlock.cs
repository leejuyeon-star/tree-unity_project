using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//새로운 ui 생성
public class ChangeBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject lv1;     //lv1의 prefab

    [SerializeField]
    private GameObject lv2;     //lv2의 prefab

    [SerializeField]
    private GameObject lv3;     //lv3의 prefab

    private blockController blockcontroller;

    private void Awake() {
        blockcontroller = GetComponent<blockController>();
    }

    // 현 ui 종류에 따라 새로운 ui 생성, 기존 ui 삭제
    public void changeBlock(Transform currentParent, Transform child1, Transform child2){
        string currentName = child1.name;

        // 겹쳐진 prefab의 종류에 따라
        if(currentName == lv1.name){
            // lv2 object 생성, 부모를 해당 슬롯으로 지정
            Instantiate(lv2,child1.position, Quaternion.Euler(0,0,0)).transform.SetParent(currentParent);
            // 두 ui 삭제
            Destroy(child1.gameObject);
            Destroy(child2.gameObject);  
        }
        else if(currentName == lv2.name){
            // lv3 object 생성, 부모를 해당 슬롯으로 지정
            Instantiate(lv3,child1.position, Quaternion.Euler(0,0,0)).transform.SetParent(currentParent);
            // 두 ui 삭제
            Destroy(child1.gameObject);
            Destroy(child2.gameObject);  
        }
        else if(currentName == lv3.name){   // 마지막 lv인 경우 원래 자리로 돌아가기
            Debug.Log("해당 블록은 마지막 lv 입니다.");
            blockcontroller.returnToPlace();
        }
   
    }
}
