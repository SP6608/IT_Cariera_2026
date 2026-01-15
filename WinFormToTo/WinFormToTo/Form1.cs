namespace WinFormToTo
{
    public partial class FormToTo : Form
    {
        Random r = new Random();
        HashSet<int>numbers= new HashSet<int>();
        //GeneMethod...
        public int GerRandom()
        {
            return r.Next() % 49 + 1;
        }
        
        public FormToTo()
        {
            InitializeComponent();
        }

        private void FormToTo_Load(object sender, EventArgs e)
        {

        }

        private void buttonToTo_Click(object sender, EventArgs e)
        {
            while (numbers.Count!=6)
            {
                int n = GerRandom();
                numbers.Add(n);
            }
            //Печат на числата
            var elements=numbers.ToArray();
            labelNum1.Text = elements[0].ToString();
            labelNum2.Text = elements[1].ToString();
            labelNum3.Text = elements[2].ToString();
            labelNum4.Text = elements[3].ToString();
            labelNum5.Text = elements[4].ToString();
            labelNum6.Text = elements[5].ToString();
            numbers.Clear();

        }
    }
}
