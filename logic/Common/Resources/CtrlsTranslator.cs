using System.Web.UI;
using System.Web.UI.WebControls;

namespace logic.Common.Resources
{
    public class CtrlsTranslator
    {
        private const string pageTitleKey = "Titulo";

        public static void Translate(Page component, KeyValuePlainTextResource ResourceManager)
        {
            string message = ResourceManager.GetMessage("Titulo");
            component.Title = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
            Control page = (Control)component.Page;
            CtrlsTranslator.FindControls(ResourceManager, page);
        }

        private static void FindControls(KeyValuePlainTextResource ResourceManager, Control control)
        {
            if (control is Label currentCtrl10 && control.ID != null)
                CtrlsTranslator.TranslateLabel(ResourceManager, control.ID, currentCtrl10);
            else if (control is ImageButton currentCtrl9 && control.ID != null)
                CtrlsTranslator.TranslateImageButton(ResourceManager, control.ID, currentCtrl9);
            else if (control is Image currentCtrl8 && control.ID != null)
                CtrlsTranslator.TranslateImage(ResourceManager, control.ID, currentCtrl8);
            else if (control is Button currentCtrl7 && control.ID != null)
                CtrlsTranslator.TranslateButton(ResourceManager, control.ID, currentCtrl7);
            else if (control is CheckBox currentCtrl6 && control.ID != null)
                CtrlsTranslator.TranslateCheckBox(ResourceManager, currentCtrl6.ID, currentCtrl6);
            else if (control is HyperLink currentCtrl5 && control.ID != null)
                CtrlsTranslator.TranslateLink(ResourceManager, control.ID, currentCtrl5);
            else if (control is GridView currentCtrl4 && control.ID != null)
                CtrlsTranslator.TranslateWebGrid(ResourceManager, control.ID, currentCtrl4);
            else if (control is CheckBoxList currentCtrl3 && control.ID != null)
                CtrlsTranslator.TranslateCheckBoxList(ResourceManager, control.ID, currentCtrl3);
            else if (control is RadioButtonList currentCtrl2 && control.ID != null)
                CtrlsTranslator.TranslateRadioList(ResourceManager, control.ID, currentCtrl2);
            else if (control is DropDownList currentCtrl1 && control.ID != null)
                CtrlsTranslator.TranslateDropDownList(ResourceManager, control.ID, currentCtrl1);
            else
                CtrlsTranslator.FindChildControls(ResourceManager, control);
        }

        private static void FindChildControls(
          KeyValuePlainTextResource ResourceManager,
          Control control)
        {
            if (control.Controls.Count <= 0)
                return;
            foreach (Control control1 in control.Controls)
                CtrlsTranslator.FindControls(ResourceManager, control1);
        }

        private static void TranslateLabel(
          KeyValuePlainTextResource ResourceManager,
          string id,
          Label currentCtrl)
        {
            string empty = string.Empty;
            string message = ResourceManager.GetMessage(id);
            if (string.IsNullOrEmpty(message))
                return;
            currentCtrl.Text = message;
        }

        private static void TranslateCheckBox(
          KeyValuePlainTextResource ResourceManager,
          string id,
          CheckBox currentCtrl)
        {
            string empty = string.Empty;
            string message = ResourceManager.GetMessage(id);
            if (string.IsNullOrEmpty(message))
                return;
            currentCtrl.Text = message;
        }

        private static void TranslateCheckBoxList(
          KeyValuePlainTextResource ResourceManager,
          string id,
          CheckBoxList currentCtrl)
        {
            string str = string.Empty;
            str = ResourceManager.GetMessage(id);
            foreach (ListItem listItem in currentCtrl.Items)
            {
                string message = ResourceManager.GetMessage(string.Format("{0}-{1}", (object)id, (object)listItem.Value));
                if (!string.IsNullOrEmpty(message))
                    listItem.Text = message;
            }
        }

        private static void TranslateRadioList(
          KeyValuePlainTextResource ResourceManager,
          string id,
          RadioButtonList currentCtrl)
        {
            string str = string.Empty;
            str = ResourceManager.GetMessage(id);
            foreach (ListItem listItem in currentCtrl.Items)
            {
                string message = ResourceManager.GetMessage(string.Format("{0}-{1}", (object)id, (object)listItem.Value));
                if (!string.IsNullOrEmpty(message))
                    listItem.Text = message;
            }
        }

        private static void TranslateDropDownList(
          KeyValuePlainTextResource ResourceManager,
          string id,
          DropDownList currentCtrl)
        {
            string str = string.Empty;
            str = ResourceManager.GetMessage(id);
            foreach (ListItem listItem in currentCtrl.Items)
            {
                string message = ResourceManager.GetMessage(string.Format("{0}-{1}", (object)id, (object)listItem.Value));
                if (!string.IsNullOrEmpty(message))
                    listItem.Text = message;
            }
        }

        private static void TranslateLink(
          KeyValuePlainTextResource ResourceManager,
          string id,
          HyperLink currentCtrl)
        {
            string empty = string.Empty;
            string message = ResourceManager.GetMessage(id);
            if (string.IsNullOrEmpty(message))
                return;
            currentCtrl.Text = message;
        }

        private static void TranslateButton(
          KeyValuePlainTextResource ResourceManager,
          string id,
          Button currentCtrl)
        {
            string empty = string.Empty;
            string message = ResourceManager.GetMessage(id);
            if (string.IsNullOrEmpty(message))
                return;
            currentCtrl.Text = message;
        }

        private static void TranslateImageButton(
          KeyValuePlainTextResource ResourceManager,
          string id,
          ImageButton currentCtrl)
        {
            string empty = string.Empty;
            string message = ResourceManager.GetMessage(id);
            if (string.IsNullOrEmpty(message))
                return;
            currentCtrl.ToolTip = message;
        }

        private static void TranslateImage(
          KeyValuePlainTextResource ResourceManager,
          string id,
          Image currentCtrl)
        {
            string empty = string.Empty;
            string message = ResourceManager.GetMessage(id);
            if (string.IsNullOrEmpty(message))
                return;
            currentCtrl.ToolTip = message;
        }

        private static void TranslateWebGrid(
          KeyValuePlainTextResource ResourceManager,
          string id,
          GridView currentCtrl)
        {
            foreach (DataControlField column in (StateManagedCollection)currentCtrl.Columns)
            {
                string message = ResourceManager.GetMessage(string.Format("{0}-{1}", (object)id, (object)column.AccessibleHeaderText));
                if (!string.IsNullOrEmpty(message))
                    column.HeaderText = message;
            }
        }
    }
}
