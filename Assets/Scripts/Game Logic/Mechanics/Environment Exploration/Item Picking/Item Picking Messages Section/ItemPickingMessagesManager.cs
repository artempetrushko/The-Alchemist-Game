using System.Collections;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.HUD
{
    public class ItemPickingMessagesManager : MonoBehaviour
    {
        [SerializeField]
        private int maxMessagesCount;
        [SerializeField]
        private int messagesShowingTimeInSeconds;
        [Space, SerializeField]
        private ItemPickingMessageView itemPickingMessagePrefab;
        [SerializeField]
        private GameObject itemPickingMessagesContainer;

        private bool isShowingStarted = false;
        private int currentShowingTime = 0;

        private bool IsShowingStarted
        {
            set
            {
                if (value != isShowingStarted)
                {
                    isShowingStarted = value;
                    if (isShowingStarted)
                    {
                        StartCoroutine(CountMessagesShowcaseTime_COR());
                    }
                    else
                    {
                        for (var i = itemPickingMessagesContainer.transform.childCount - 1; i >= 0; i--)
                        {
                            Destroy(itemPickingMessagesContainer.transform.GetChild(i).gameObject);
                        }
                        itemPickingMessagesContainer.transform.DetachChildren();
                        currentShowingTime = 0;
                    }
                }
            }
        }

        public void ShowNewMessage(ItemState pickedItem)
        {
            var newMessage = Instantiate(itemPickingMessagePrefab, itemPickingMessagesContainer.transform);
            newMessage.SetInfo(pickedItem.BaseParams.Icon, pickedItem switch
            {
                StackableItemState stackableItem => stackableItem.ItemsCount,
                _ => 1
            });
            if (itemPickingMessagesContainer.transform.childCount > maxMessagesCount)
            {
                Destroy(itemPickingMessagesContainer.transform.GetChild(0).gameObject);
            }
            IsShowingStarted = true;
            currentShowingTime = 0;
        }

        private IEnumerator CountMessagesShowcaseTime_COR()
        {
            while (currentShowingTime < messagesShowingTimeInSeconds)
            {
                yield return new WaitForSeconds(1f);
                currentShowingTime++;
            }
            IsShowingStarted = false;
        }
    }
}