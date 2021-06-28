using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBuilderOld : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject BuildingCubePreFab;
    // Start is called before the first frame update

    void OnCollisionStay(Collision collisionInfo){
        Vector3 direction = (this.gameObject.transform.position - collisionInfo.gameObject.transform.position).normalized;    
        Debug.DrawRay(collisionInfo.gameObject.transform.position,direction);

        if(Input.GetMouseButtonDown(0)){
            
            if(collisionInfo.gameObject.tag == "PLANE"){
                GameObject buildingCube = Instantiate(BuildingCubePreFab);
                buildingCube.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    collisionInfo.gameObject.transform.position.y + buildingCube.transform.localScale.y/2,
                    this.gameObject.transform.position.z
                );
            }
            else{
                buildCube(this.gameObject,collisionInfo.gameObject);
                // GameObject buildingCube = Instantiate(BuildingCubePreFab);
                // buildingCube.transform.position = new Vector3(
                //     collisionInfo.gameObject.transform.position.x,
                //     collisionInfo.gameObject.transform.position.y + buildingCube.transform.localScale.y/2,
                //     collisionInfo.gameObject.transform.position.z
                // );
            }
        }
    }

    private void buildCube(GameObject tool,GameObject hitCube){

        Vector3 direction = (tool.transform.position - hitCube.transform.position).normalized;
        Ray CalculatedRay = new Ray(hitCube.transform.position, direction);

        RaycastHit hitCubeRay;
        if (Physics.Raycast(CalculatedRay,out hitCubeRay)){
            if (hitCubeRay.collider != null){
                Debug.Log("Just created a cube");
                Vector3 normalHitCubeRay = hitCubeRay.normal;
                normalHitCubeRay = hitCubeRay.transform.TransformDirection(normalHitCubeRay);
                Debug.DrawRay(normalHitCubeRay,hitCubeRay.transform.position);
                // GameObject buildingCube = Instantiate(BuildingCubePreFab);
                // buildingCube.transform.position = hitCube.transform.position;

                //Top
                if(normalHitCubeRay == hitCubeRay.transform.up){
                    Debug.Log("TOP");
                }
                // Bottom
                else if(normalHitCubeRay == -hitCubeRay.transform.up){
                    Debug.Log("Bottom");
                }
                //Forward
                else if(normalHitCubeRay == hitCubeRay.transform.forward){
                    Debug.Log("Forward");
                }
                //Backward
                else if(normalHitCubeRay == -hitCubeRay.transform.forward){
                    Debug.Log("Backward");
                }
                //Right
                else if(normalHitCubeRay == hitCubeRay.transform.right){
                    Debug.Log("Right");
                }
                //Left
                else{
                    Debug.Log(normalHitCubeRay.ToString());
                }
            }    
        }
     }

}
