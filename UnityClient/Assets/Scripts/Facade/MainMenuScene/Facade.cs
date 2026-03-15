using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MainMenuScene
{
    public class Facade: AbstractFacade
    {
        private UiController m_UiController;
        protected override void OnInit()
        {
            base.OnInit();
            m_UiController = new UiController();
            GameMediator.Instance.RegisterController(m_UiController);

        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
            m_UiController.GameUpdate();
        }
    }
}
