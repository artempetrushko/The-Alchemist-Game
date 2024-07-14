using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private LocationTitleView _locationTitleView;
        [SerializeField] private QuestProgressView _questProgressView;
        [SerializeField] private Image startBlackScreen;

        public LocationTitleView LocationTitleView => _locationTitleView;
        public QuestProgressView QuestProgressView => _questProgressView;

        public IEnumerator ShowLocationName_COR(string locationName)
        {
            yield return StartCoroutine(_locationTitleView.ShowLocationName_COR(locationName));
        }

        public IEnumerator HideStartBlackScreen_COR()
        {
            startBlackScreen.gameObject.SetActive(true);
            var hideBlackScreenTween = startBlackScreen.DOFade(0f, 2f);
            yield return hideBlackScreenTween.WaitForCompletion();
        }
    }
}