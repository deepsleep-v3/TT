namespace Setup
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
            FontHelper.ApplyEmbeddedTTFToControl(WelcomeCaption, "Setup.caption.ttf", 20f);
            FontHelper.ApplyEmbeddedTTFToControl(WelcomeNext, "Setup.alert.ttf", 15f);
        }

        private void Welcome_Load(object sender, EventArgs e)
        {

        }

        private void WelcomeNext_Click(object sender, EventArgs e)
        {

        }
    }
}
