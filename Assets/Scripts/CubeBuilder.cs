using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.VRModuleManagement;

public class CubeBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject BuildingCubePreFab;


    // void Update()
    // {
    //     //------ Trigger
    //     // gettin button click (Trigger / Menu  / Grip / PadTouch)
    //     if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Trigger))
    //     {
    //         Debug.Log("trigger is pressed Down");
    //     }
    //     // Getting the Trigger anlog float value [0:1]
    //     float triggerFloatValue = ViveInput.GetAxis(HandRole.RightHand, ControllerAxis.Trigger, false);
    //     Debug.Log("Trigger = " + triggerFloatValue);
 
 
    //     //------ TouchPad
    //     //Get the Touch Pad click 
    //     if(ViveInput.GetPressDown(HandRole.RightHand , ControllerButton.PadTouch)){
    //         Debug.Log("TouchPad is pressed Down");
    //     }
    //     // Getting the touch pad touch position [Vector 2]
    //     //         1
    //     //    -1 <-|-> 1
    //     //         -1
    //     Debug.Log("Touch pad = "+ViveInput.GetPadAxis(HandRole.RightHand ));
 
    //     //------ Menu
    //     if(ViveInput.GetPressDown(HandRole.RightHand , ControllerButton.Menu)){
    //         Debug.Log("Menuu Button is clicked Down");
    //     }
    //     //------ Grip
    //     if(ViveInput.GetPressDown(HandRole.RightHand , ControllerButton.Grip)){
    //         Debug.Log("Grip Button is clicked Down");
    //     }
 
    //     //Getting the controller velocity
    //     uint deviceIndex = ViveRoleProperty.New(HandRole.RightHand).GetDeviceIndex();
    //     Vector3 ControllerVelocity = VRModule.GetDeviceState(deviceIndex, false).velocity;
    //     Vector3 ControllerAngularVelocity = VRModule.GetDeviceState(deviceIndex, false).angularVelocity;            
    // }


    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag != "PLANE"){
            Component meshRenderer = collisionInfo.gameObject.GetComponent<MeshRenderer>(); 
            if(meshRenderer){
                collisionInfo.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    void OnCollisionStay(Collision collisionInfo){
        // Vector3 direction = (this.gameObject.transform.position - collisionInfo.gameObject.transform.position).normalized;    
        // Debug.DrawRay(collisionInfo.gameObject.transform.position,direction);

        if(ViveInput.GetPressDown(HandRole.RightHand , ControllerButton.PadTouch)){
            // Debug.Log("TouchPad is pressed Down");
            GameObject buildingCube = Instantiate(BuildingCubePreFab);
            if(collisionInfo.gameObject.tag == "PLANE"){
                buildingCube.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    collisionInfo.gameObject.transform.position.y + buildingCube.transform.localScale.y/2,
                    this.gameObject.transform.position.z
                );
                buildingCube.transform.GetChild(1).gameObject.SetActive(false);
            }
            else{
                try{
                    buildingCube.transform.position = collisionInfo.transform.position;
                    // Component meshRenderer = collisionInfo.gameObject.GetComponent<MeshRenderer>(); 
                    // if(meshRenderer){
                    //     collisionInfo.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    //     collisionInfo.gameObject.GetComponent<BoxCollider>().enabled = false;
                    // }
                    if(collisionInfo.gameObject.tag == "TOP"){
                        buildingCube.transform.GetChild(1).gameObject.SetActive(false);
                        // buildingCube.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = false;
                        // buildingCube.transform.GetChild(1).gameObject.GetComponent<BoxCollider>().enabled = false;
                    }else if(collisionInfo.gameObject.tag == "BOTTOM"){
                        buildingCube.transform.GetChild(0).gameObject.SetActive(false);
                        // buildingCube.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        // buildingCube.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
                    }else if(collisionInfo.gameObject.tag == "FORWARD"){
                        buildingCube.transform.GetChild(3).gameObject.SetActive(false);
                        // buildingCube.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        // buildingCube.transform.GetChild(3).gameObject.GetComponent<BoxCollider>().enabled = false;
                    }else if (collisionInfo.gameObject.tag == "BACKWARD"){
                        buildingCube.transform.GetChild(2).gameObject.SetActive(false);
                        // buildingCube.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        // buildingCube.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled = false;                    
                    }else if(collisionInfo.gameObject.tag == "LEFT"){
                        buildingCube.transform.GetChild(5).gameObject.SetActive(false);
                        // buildingCube.transform.GetChild(5).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        // buildingCube.transform.GetChild(5).gameObject.GetComponent<BoxCollider>().enabled = false;
                    }else{
                        buildingCube.transform.GetChild(4).gameObject.SetActive(false);
                        // buildingCube.transform.GetChild(4).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        // buildingCube.transform.GetChild(4).gameObject.GetComponent<BoxCollider>().enabled = false;                    
                    }

                    collisionInfo.gameObject.SetActive(false);

                }catch (System.NullReferenceException exce){
                    Debug.Log("Null pointer Exception at "+exce.Data);
                }
            }
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag != "PLANE"){
            Component meshRenderer = collisionInfo.gameObject.GetComponent<MeshRenderer>(); 
            if(meshRenderer){
                collisionInfo.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

}
