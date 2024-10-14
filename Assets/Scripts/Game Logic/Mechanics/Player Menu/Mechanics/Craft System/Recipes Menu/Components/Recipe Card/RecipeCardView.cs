using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu.Craft
{
    public class RecipeCardView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _recipeTitleText;
        [SerializeField] private TMP_Text _recipeDescriptionText;
        [SerializeField] private Image _recipeResultItemIcon;
        [SerializeField] private Button _buttonComponent;

        public Button ButtonComponent => _buttonComponent;

        public void SetRecipeTitleText(string text) => _recipeTitleText.text = text;

        public void SetRecipeDescriptionText(string text) => _recipeDescriptionText.text = text;

        public void SetRecipeResultItemIcon(Sprite icon) => _recipeResultItemIcon.sprite = icon;
    }
}