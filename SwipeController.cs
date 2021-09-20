using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Monetization;

public class SwipeController : MonoBehaviour
{
    #if UNITY_IOS || UNITY_ANDROID
 
 
    Vector3 start;
    Vector3 end;
    float dragDistance;
    public float speed;
    public float zoomSpeed;

    //bool pinching = false;
    Touch t1;
    Touch t2;

    public int zoomMax;
    public int zoomMin;

    Vector3 touchPosWorld;

    //Moving Camera
    public Transform endMarker;
    public static bool moving;


    void Start(){
        dragDistance = Screen.height * 15/500;
    }

    void Update(){
        if(moving){
            transform.position = Vector3.Lerp(transform.position, new Vector3(endMarker.position.x, endMarker.position.y, transform.position.z), Time.deltaTime * 2);

            if(Vector3.Distance(transform.position, endMarker.position) - Mathf.Abs(transform.position.z/10) < 9.001f){
                moving = false;
            }
        }

        if(Input.touchCount == 1){
            Touch touch = Input.GetTouch(0);

            //if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                //return;

            if(touch.phase == TouchPhase.Began){

                start = touch.position;
                end = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved){
                //if(IsPointerOverUIObject())
                    //return;
                speed = Camera.main.orthographicSize * .0017184f;
                transform.Translate(-touch.deltaPosition.x * speed, -touch.deltaPosition.y * speed, 0);
                end = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended){
                end = touch.position;
                //if(IsPointerOverUIObject())
                    //return;
                if(Mathf.Abs(start.x - end.x) < dragDistance || Mathf.Abs(start.y - end.y) < dragDistance){
                    Debug.Log("Tap");
                    touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector3 touchPosWorld2D = new Vector3(touchPosWorld.x, touchPosWorld.y, -20);

                    RaycastHit hitInformation;
                    Physics.Raycast(touchPosWorld2D, Camera.main.transform.forward, out hitInformation);
 
                    if (hitInformation.collider != null) {
                        //We should have hit something with a 2D Physics collider!
                        //Tile tile = hitInformation.transform.GetComponent<Tile>();
                        //if(hitInformation.collider.transform.GetComponent<PlanetController>() != null)
                            //hitInformation.collider.transform.GetComponent<PlanetController>().PlanetClicked();
                        if(hitInformation.collider.transform.CompareTag("Back"))
                            FindObjectOfType<PlanetInfoScreen>().Close();
                        
                        //touchedObject should be the object someone touched.
                    }      
                }
            }
        }else if(Input.touchCount == 2){
            t1 = Input.GetTouch(0);
            t2 = Input.GetTouch(1);

            Vector2 t1PrevPos = t1.position - t1.deltaPosition;
            Vector2 t2PrevPos = t2.position - t2.deltaPosition;

            float prevMag = (t1PrevPos - t2PrevPos).magnitude;
            float curMag = (t1.position - t2.position).magnitude;

            float diff = curMag - prevMag;
            zoom(diff * 0.005f * zoomSpeed);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -45f, 45f),
                                         Mathf.Clamp(transform.position.y, -45f, 45f),
                                         transform.position.z);
    }

    void zoom(float increment){
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize-increment, zoomMin, zoomMax);
    }

    private bool IsPointerOverUIObject() {
     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
    }

    #endif
}
