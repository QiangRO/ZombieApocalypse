
using UnityEngine;
using UnityEngine.AI;
public class MoveAiAgent : MonoBehaviour
{
    public Camera mapCam;
    public NavMeshAgent agent;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = mapCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)){
                //MoveAgent
                agent.SetDestination(hit.point);
            }
        }
    }
}
