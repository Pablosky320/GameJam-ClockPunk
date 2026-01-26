using UnityEngine;
public class Follow : MonoBehaviour {
    public Transform player;
    public Vector3 offset = new Vector3(-10, 15, -10);
    void LateUpdate() {
        if(player) transform.position = player.position + offset;
    }
}