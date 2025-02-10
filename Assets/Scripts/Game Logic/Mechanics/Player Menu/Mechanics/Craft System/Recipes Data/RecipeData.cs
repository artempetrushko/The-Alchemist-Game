using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RecipeCategory
{
    Weapon,
    Missiles,
    Clothes,
    Potions,
    Resources,
    Special,
    Others
}

[CreateAssetMenu(fileName = "New Recipe", menuName = "Game Entities/Recipe", order = 51)]
public class RecipeData : ScriptableObject
{
    [Header("����� ����������")]
    [SerializeField]
    private Sprite recipeIcon;
    [SerializeField]
    private RecipeCategory category;
    [SerializeField]
    private string title;
    [SerializeField, Multiline]
    private string description;
    [SerializeField]
    private int requiredEnergyCount;
    [Header("������ �������")]
    [SerializeField]
    private IngredientCellsTemplateView ingredientCellsTemplate;
    [SerializeField]
    private List<Sprite> templateCellsIcons = new();
    [Header("�������� �������")]
    [SerializeField]
    private List<RecipeVariant> recipeVariants = new();

    public Sprite RecipeIcon => recipeIcon;
    public RecipeCategory Category => category;
    public string LocalizedCategoryTitle => Category switch
    {
        RecipeCategory.Weapon => "������",
        RecipeCategory.Missiles => "�������",
        RecipeCategory.Clothes => "������",
        RecipeCategory.Potions => "�����",
        RecipeCategory.Resources => "�������",
        RecipeCategory.Special => "������",
        RecipeCategory.Others => "������"
    };
    public string Title => title;
    public string Description => description;
    public int RequiredEnergyCount => requiredEnergyCount;
    public IngredientCellsTemplateView IngredientCellsTemplate => ingredientCellsTemplate;
    public List<Sprite> TemplateCellsIcons => templateCellsIcons;

    public RecipeVariant TryGetMatchingRecipeVariant(List<ItemState> selectedItems)
    {
        var matchedRecipeVariants = recipeVariants
            .Where(variant => variant.CheckRecipeVariantMatching(selectedItems))
            .ToList();
        return matchedRecipeVariants.Count == 1 ? matchedRecipeVariants[0] : null;
    }
}
