using Fungus;
using UnityEngine;

namespace Betty
{
    public class Door : MonoBehaviour, IInteraction
    {
        [SerializeField, Header("Fungus_互動物件說明")]
        private Flowchart flowchartObject;
        [SerializeField, Header("這扇門的鑰匙")]
        private key key;

        private string messageNoKey = "門_A_沒有鑰匙";
        private string messageHasKey = "門_A_有鑰匙";
        private bool hasKey;

        public void Interaction()
        {
            print($"<color=#3f3>互動:{name}</color>");

            hasKey = key.pickUp;
            print($"是否有鑰匙{hasKey}");

            //如果還沒撿到鑰匙，就出現沒有鑰匙提示
            if (!hasKey) flowchartObject.SendFungusMessage(messageNoKey);
            //否則，就出現有鑰匙提示與動畫
            else flowchartObject.SendFungusMessage(messageHasKey);
        }

        public void PickUp()
        {
            print($"<color=#37f>撿取:{name}</color>");
            Interaction();
        }

    }

}

