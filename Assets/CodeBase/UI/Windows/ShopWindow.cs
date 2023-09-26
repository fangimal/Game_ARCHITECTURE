using TMPro;

namespace CodeBase.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI SkullText;
        
        protected override void Initialize()
        {
            //AdItem.Initialize();
            RefreshSkullText();
        }
        protected override void SubscribeUpdates()
        {
            //AdItem.Subscribe();
            Progress.WorldData.LootData.Changed += RefreshSkullText;
        }
        
        protected override void Cleanup()
        {
            base.Cleanup();
            //AdItem.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshSkullText;
        }
        private void RefreshSkullText() => 
            SkullText.text = Progress.WorldData.LootData.Collected.ToString();
    }
} 