using Fungus;
using UnityEngine;

namespace Betty
{
    /// <summary>
    /// 手電筒
    /// </summary>
    public class Flashlight : MonoBehaviour, IInteraction
    {
        [SerializeField, Header("手電筒位置")]
        private Transform flashlightPoint;
        [SerializeField, Header("手電筒互動按鍵")]
        private KeyCode keyFlashlight = KeyCode.F;
        [SerializeField, Header("手電筒聚光燈")]
        private GameObject spotLight;
        [SerializeField, Header("Flowchart_互動物件說明")]
        private Flowchart flowchartObject;
        [SerializeField, Header("撿取音效")]
        private AudioClip soundPickUp;
        [SerializeField, Header("開關音效")]
        private AudioClip soundSwitch;

        private string flowchartMessage = "手電筒";

        private Rigidbody rig;
        private Collider col;
        private bool isPickUp;
        private AudioSource aud;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            aud = GetComponent<AudioSource>();
        }

        private void Update()
        {
            Interaction();
        }

        public void Interaction()
        {
            // 如果還沒有撿取就跳出
            if (!isPickUp) return;
            // 如果按下 F 鍵後
            if (Input.GetKeyDown(keyFlashlight))
            {
                print($"<color=#3f3>互動 : {name}</color>");
                // 切換聚光燈 (把聚光燈的勾勾設為相反)
                spotLight.SetActive(!spotLight.activeInHierarchy);
                // 播放一次開關的音效，音量設定為隨機的 1 ~ 1.5 之間
                aud.PlayOneShot(soundSwitch, Random.Range(1, 1.5f));
            }

        }

        public void PickUp()
        {
            print($"<color=#37f>撿取 : {name}</color>");
            // 已經撿取物件
            isPickUp = true;
            // 將此物件的父物件設定為 手電筒位置
            transform.SetParent(flashlightPoint);
            // 設定手電筒子物件的座標與角度 (歸零)
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            // 設定手電筒座標
            transform.position = flashlightPoint.position;
            // 關閉碰撞器與地心引力
            col.enabled = false;
            rig.useGravity = false;
            // 傳送訊息給蘑菇物件
            flowchartObject.SendFungusMessage(flowchartMessage);
            // 播放一次撿取音效
            aud.PlayOneShot(soundPickUp);
        }
    }
}