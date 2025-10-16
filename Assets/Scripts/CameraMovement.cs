using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - 7, player.transform.position.y + 6.5f, player.transform.position.z - 7);
    }
}
