using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.PlayerMenu;

namespace GameLogic.LootSystem
{
    public abstract class ItemState : ICloneable
    {
        protected ItemView itemView;
        protected ItemView hudItemView;

        public ItemData BaseParams { get; protected set; }
        public string Description { get; set; }
        public int ContainedEnergyCount => 0;
        public virtual ItemView ItemView
        {
            get => itemView;
            set
            {
                itemView = value;
                itemView.LinkedItem = this;
                itemView.SetInfo(BaseParams.Icon);
            }
        }
        public virtual ItemView HUDItemView
        {
            get => hudItemView;
            set
            {
                hudItemView = value;
                hudItemView.LinkedItem = this;
                hudItemView.SetInfo(BaseParams.Icon);
            }
        }
        public ItemSlot LinkedItemSlot { get; set; }

        public ItemState(ItemData itemData)
        {
            BaseParams = itemData;
            Description = itemData.BaseDescription;
        }

        protected ItemState() { }

        ~ItemState()
        {
            UnityEngine.Object.Destroy(ItemView);
        }

        public abstract object Clone();

        public abstract Dictionary<string, string> GetItemParams();
    }
}