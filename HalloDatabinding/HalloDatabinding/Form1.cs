namespace HalloDatabinding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox2.DataBindings.Add("Text", textBox1, "Text", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox2.DataBindings.Add(nameof(TextBox.BackColor), textBox1, "Text", true);
        }
    }
}
