using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private IAssets _assets;
        private Transform _uiRoot;
        
        private readonly IStaticDataService _staticdata;
        private readonly IPersistentProgressService _progressService;
        public UIFactory(IAssets assets, IStaticDataService staticdata, IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticdata = staticdata;
            _progressService = progressService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticdata.ForWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_progressService);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
    }
}