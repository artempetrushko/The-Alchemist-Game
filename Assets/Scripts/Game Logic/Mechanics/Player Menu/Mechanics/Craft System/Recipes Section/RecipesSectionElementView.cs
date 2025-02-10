using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public abstract class RecipesSectionElementView : MonoBehaviour
    {
        [SerializeField]
        protected TMP_Text title;
        [SerializeField]
        protected Button buttonComponent;

        public void Select() => buttonComponent.Select();

        public void InvokeLinkedAction() => buttonComponent.onClick.Invoke();
    }
}