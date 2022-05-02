using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour{

       public float durationTime = 0.15f;
       public float magnitude = 0.5f;
	public GameObject target;
       public float camSpeed = 4.0f;
	  
	void Start(){
            target = GameObject.FindWithTag("Player");
       }
	void FixedUpdate () {
            Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)target.transform.position, camSpeed * Time.fixedDeltaTime);
            transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
       }

       //The Update Function is just for testing (hit [p]), and can be commented out:
       void Update(){
             // if (Input.GetKeyDown(KeyCode.P)){
                   //  StartCoroutine(ShakeMe(durationTime,magnitude));
            // }
       }

       //use this to call from another script, like when the player gets hit
       public void ShakeCamera(float durationTime2, float magnitude2){
              StartCoroutine(ShakeMe(durationTime2, magnitude2));
       }

       //the screenshake!
       public IEnumerator ShakeMe(float durationTime, float magnitude){
              Vector3 origPos = transform.localPosition;
              float elapsedTime = 0.0f;

              while (elapsedTime < durationTime){
                     float sX = Random.Range(-1f, 1f) * magnitude;
                     float sY = Random.Range(-1f, 1f) * magnitude;

                     transform.localPosition = new Vector3((origPos.x+sX), (origPos.y+sY), origPos.z);
                     elapsedTime += Time.deltaTime;
                     yield return null;
              }

              transform.localPosition = origPos;
       }

}
