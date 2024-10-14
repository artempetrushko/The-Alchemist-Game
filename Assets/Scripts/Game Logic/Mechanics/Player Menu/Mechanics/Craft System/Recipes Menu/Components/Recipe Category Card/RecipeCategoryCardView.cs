using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu.Craft
{
    public class RecipeCategoryCardView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _categoryTitleText;
        [SerializeField] private Button _buttonComponent;       
        [SerializeField] private GameObject _recipeCardViewsContainer;

        public Button ButtonComponent => _buttonComponent;

        public void SetTitleText(string text) => _categoryTitleText.text = text;
    }
}