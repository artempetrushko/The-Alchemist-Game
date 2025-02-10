using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour
{
    [SerializeField] private LocationTitleView locationTitleView;
    [SerializeField] private Image startBlackScreen;

    public async UniTask ShowLocationNameAsync(string locationName)
    {
        await locationTitleView.ShowLocationNameAsync(locationName);
    }

    public async UniTask HideStartBlackScreenAsync()
    {
        startBlackScreen.gameObject.SetActive(true);
        await startBlackScreen.DOFade(0f, 2f).AsyncWaitForCompletion();
    }
}
