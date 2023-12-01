using TMPro;
using UnityEngine;

namespace Game.Systems.UISystem.View
{
    public class HudView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI coinText;

        [SerializeField]
        private TextMeshProUGUI leveltext;

        #region Public Methods

        public void UpdateHud(int aCoin, int aLevel)
        {
            coinText.text = aCoin + "";
            leveltext.text = aLevel + "";
        }

        #endregion
    }
}