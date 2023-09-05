using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public class ReviveLoseUI : BaseRewardLoseUI
    {
        [SerializeField] private ReviveTimerUI reviveTimerUI;

        public override void Show()
        {
            base.Show();
            reviveTimerUI.StartTimer();
        }

        protected override void OnRestartButtonClicked()
        {
            Hide();
            // Put revive logic here

            Taptic.Selection();
        }

        protected override void OnNoThanksButtonClicked()
        {
            base.OnRestartButtonClicked();
            reviveTimerUI.StopTimer();
            Taptic.Selection();
        }

        protected override void OnRewardedAdShown()
        {
            base.OnRewardedAdShown();
            reviveTimerUI.StopTimer();
        }

        protected override void OnRewardButtonClicked()
        {
            Taptic.Selection();
            // Put show reward logic here
        }
    }
}
