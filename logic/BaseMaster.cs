using System.Web.UI;

namespace logic
{
    public class BaseMaster : MasterPage
    {
        public BasePage basePage = new BasePage();

        public virtual string ServicioID => this.basePage.GetAppSetting(nameof(ServicioID));
    }
}
