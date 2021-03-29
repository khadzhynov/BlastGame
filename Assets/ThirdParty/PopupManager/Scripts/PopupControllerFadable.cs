using System;
using Zenject;

namespace GG.Infrastructure.Popups
{
    public class PopupControllerFadable : PopupControllerBase
    {
        public class Factory : PlaceholderFactory<PopupControllerFadable> { }
        public override Type ViewType => typeof(PopupViewFadable);
        protected PopupViewFadable View => ViewBase as PopupViewFadable;
    }
}