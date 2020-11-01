using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Transform player, enemy, wall1, wall2;

    public Vector3 center;

    public Camera cam;

    public float ortho_min = 2, ortho_max = 2.2f;
    public float dist_max = 7.9f;

    private void Start() {
        cam = GetComponent<Camera> ();
        //dist_max = 2*ortho_max*cam.aspect - (0.7f * 2);
    }
    private void Update() {

        center = (player.position + enemy.position) / 2;
        float distance = Vector3.Distance(player.position, enemy.position);
        float ortho_size = (distance * (ortho_max - ortho_min)) / dist_max + ortho_min;
        transform.position = center;

        if (ortho_size > ortho_max) {
            cam.orthographicSize = ortho_max;
        }

        else {
            cam.orthographicSize = ortho_size;
        }

        if(ortho_size < ortho_max) {
            wall1.position = cam.ViewportToWorldPoint(new Vector3(0,0)) + new Vector3(-0.5f,0);
            wall2.position = cam.ViewportToWorldPoint(new Vector3(1,0)) + new Vector3(0.5f,0);
        }
        
    }



}