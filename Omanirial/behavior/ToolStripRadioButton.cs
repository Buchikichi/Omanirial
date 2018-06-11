using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Omanirial.behavior
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip
        | ToolStripItemDesignerAvailability.StatusStrip)]
    [ToolboxBitmap(typeof(ToolStripRadioButton), "RadioButton_16x")]
    public class ToolStripRadioButton : ToolStripControlHost
    {
        public ToolStripRadioButton() : base(new RadioButton())
        {
            Radio.Appearance = Appearance.Button;
            Radio.FlatStyle = FlatStyle.Standard;
            Radio.AutoSize = false;
        }

        #region Attributes
        private RadioButton Radio => (RadioButton)Control;
        public bool Checked
        {
            get => Radio.Checked;
            set => Radio.Checked = value;
        }
        #endregion
    }
}
