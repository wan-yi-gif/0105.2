using UnityEngine;

namespace Betty
{
    public class InteractionSystem : MonoBehaviour
    {
        [Header("圖示資料")]
        [SerializeField]
        private Color rayColor = Color.red;
        [SerializeField, Range(1, 20)]
        private float rayLength = 10;
        [SerializeField]
        private LayerMask rayLayer;

        // 繪製圖示事件 ODG : 僅在 Unity 內顯示
        private void OnDrawGizmos()
        {
            // 1. 決定顏色
            Gizmos.color = rayColor;
            // 2. 繪製圖示
            // 圖示的繪製射線(中心點，方向 * 距離)
            Gizmos.DrawRay(transform.position, transform.forward * rayLength);
        }

        private void Update()
        {
            Raycast();
        }

        private void Raycast()
        {
            // 物理的射線碰撞(中心點，方向，碰到的物件，距離，圖層)
            bool isHit = Physics.Raycast(
                transform.position, transform.forward, out RaycastHit hit, rayLength, rayLayer);

            // 如果 碰到的物件 為 空的 就 跳出
            if (hit.collider == null) return;

            //判斷是否有互動介面
            if (hit.collider.TryGetComponent(out IInteraction interaction))
            {
                print($"<color=#f33>是否碰到物件 : {hit.collider.name}</color>");
                // 如果按下左鍵 就對互動物件執行撿取功能
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    interaction.PickUp();
                }
            }
        }
    }
}
