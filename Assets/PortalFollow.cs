using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFollow : MonoBehaviour
{
    public Transform portalOBJ;
    public float fullOpaqueDist;

    private Transform player;
    private MeshRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = portalOBJ.gameObject.GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position - player.position;
        //transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        RaycastHit hit;
        Ray playerRay = new Ray(player.position, Camera.main.transform.forward);
        if (Physics.Raycast(playerRay,out hit))
        {
            portalOBJ.position = new Vector3(hit.point.x, portalOBJ.position.y, hit.point.z);
            //portalOBJ.rotation = Quaternion.LookRotation(Vector3.right, hit.normal);
            portalOBJ.rotation = Quaternion.Euler(hit.normal.x * 90,hit.normal.y+90,hit.normal.z*90);
            Debug.DrawRay(hit.point, hit.normal);
        }

        float dist = Vector3.Distance(player.transform.position, portalOBJ.position);
        Color oldCol = rend.material.color;

        rend.material.SetFloat("_lerpVal", Mathf.Clamp(fullOpaqueDist/dist,0,1));
    }
}
