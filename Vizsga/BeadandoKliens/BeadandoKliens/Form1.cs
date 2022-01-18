using Grpc.Net.Client;
using Beadando;
using BeadandoKliens;
using Grpc.Core;

namespace BeadandoKliens
{
    public partial class Form1 : Form
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5001");
        Beadandopackage.BeadandopackageClient client;
        string uid = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new Beadandopackage.BeadandopackageClient(channel);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                label1.Text = "Error";
            }
            else if (textBox1.Text != "u" && textBox2.Text != "p")
            {
                label1.Text = "Error";
            }
            else
            {
                User user = new User(); user.Name = textBox1.Text; user.Passwd = textBox2.Text;
                Session_Id tempuid = client.Login(user);
                label1.Text = tempuid.ToString();
                string temp = tempuid.ToString();
                uid = temp.Substring(9, 36);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (var call = client.List(new Empty()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    Product products = call.ResponseStream.Current;
                    string temp = products.Brand = " " + products.Model + " " + products.Type + " " + products.Price;
                    listBox1.Items.Add(temp);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Data data = new Data();
            data.Brand = textBox3.Text; data.Model = textBox4.Text; data.Type = textBox5.Text; data.Price = int.Parse(textBox8.Text);
            data.Price = int.Parse(textBox5.Text);
            data.Uid = uid;
            Result res = client.Add(data);
            label1.Text = res.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Product2 data = new Product2();
            data.Code = textBox6.Text; data.Uid = uid;
            data.Price = int.Parse(textBox7.Text);
            Result ered = client.Bid(data);
            label1.Text = ered.ToString();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateBtn_Click(object sender, EventArgs e)
        {

        }
    }
}