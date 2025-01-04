using Fungus;
using UnityEngine;

namespace Betty
{
    public class key : MonoBehaviour, IInteraction
    {
        [SerializeField, Header("Flowchart_互動物件說明")]
        private Flowchart flowchartObject;
        [SerializeField, Header("撿取音效")]
        private AudioClip soundPickUp;

        private string flowchartMessage = "鑰匙_A";
        private AudioSource aud;
        private Rigidbody rig;
        private Collider col;
        private bool isPickUp;

        /// <summary>
        /// 無法撿到鑰匙?
        /// </summary>
        public bool pickUp => isPickUp;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            aud = GetComponent<AudioSource>();
        }
        public void Interaction()
        {
            print($"<color=#3f3>互動: {name}</color>");
        }

        public void PickUp()
        {
            print($"<color=#37f>撿取: {name}</color>");
            // 設為已經撿取，剛體設定為運動學(不會動)，關閉碰撞，設定座標
            isPickUp = true;
            rig.isKinematic = true;
            col.enabled = false;
            transform.position = new Vector3(0, 0, 200);
            aud.PlayOneShot(soundPickUp);
            flowchartObject.SendFungusMessage(flowchartMessage);
        }
    }
}
