﻿using System;

namespace TFPlay.SceneFaders
{
    public interface ISceneFaderAnimation
    {
        public void Show(Action callback);
        public void Hide(Action callback);
        public void ShowImmediately();
        public void HideImmediately();
    }
}
