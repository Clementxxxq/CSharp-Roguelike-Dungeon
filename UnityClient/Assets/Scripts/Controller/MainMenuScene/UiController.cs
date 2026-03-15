namespace MainMenuScene
{
    public class UiController : AbstractController
    {
        private PanelRoot panelRoot;
        public UiController() { }
        protected override void OnInit()
        {
            base.OnInit();
            panelRoot = new PanelRoot();
        }
        protected override void AlwaysUpdate()
        {
            base.AlwaysUpdate();
            panelRoot.GameUpdate();
        }
    }
}
