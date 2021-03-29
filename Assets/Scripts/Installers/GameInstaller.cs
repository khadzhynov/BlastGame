using BlastGame.Services;
using BlastGame.UI;
using UnityEngine;
using Zenject;

namespace BlastGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private IconsModel _iconsModel;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CoroutineService>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<IconsModel>().FromInstance(_iconsModel).AsSingle().NonLazy();
        }
    }
}
