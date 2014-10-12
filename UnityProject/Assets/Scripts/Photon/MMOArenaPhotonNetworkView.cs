using UnityEngine;
using System.Collections;

public class MMOArenaPhotonNetworkView : MonoBehaviour {

    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

    private int healthAmount;

    public PhotonView photonView;
    CharacterStats characterStats;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        characterStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
            characterStats.health = healthAmount;
        }
        else
        {
            healthAmount = characterStats.health;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(healthAmount);

        }
        else
        {
            // Network player, receive data
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
            this.healthAmount = (int)stream.ReceiveNext();
        }
    }
}
