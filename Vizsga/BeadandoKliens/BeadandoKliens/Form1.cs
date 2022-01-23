using Grpc.Net.Client;
using Beadando;
using BeadandoKliens;
using Grpc.Core;
using System.Threading.Channels;

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
            //List<User> list = new List<User>();
            //using (var call = client.List(new Empty()))
            //{
            //    while (await call.ResponseStream.MoveNext())
            //    {
            //        User user = call.ResponseStream.Current;
            //        list.Add(user);
            //    }
            //}

            //foreach (User item in list)
            //{
            //    if (textBox1.Text == "" && textBox2.Text == "")
            //    {
            //        label1.Text = "Error";
            //    }
            //    else if (textBox1.Text != item.Name && textBox2.Text != item.Passwd)
            //    {
            //        label1.Text = "Error";
            //    }
            //    else
            //    {
            //        User user = new User(); user.Name = textBox1.Text; user.Passwd = textBox2.Text;
            //        Session_Id tempuid = client.Login(user);
            //        label1.Text = tempuid.ToString();
            //        string temp = tempuid.ToString();
            //        uid = temp.Substring(9, 36);
            //    }
            //}


            //if (textBox1.Text == "" && textBox2.Text == "")
            //{
            //    label1.Text = "Error";
            //}
            //else if (textBox1.Text != "u" && textBox2.Text != "p")
            //{
            //    label1.Text = "Error";
            //}
            //else
            //{
            //    User user = new User(); user.Name = textBox1.Text; user.Passwd = textBox2.Text;
            //    Session_Id tempuid = client.Login(user);
            //    label1.Text = tempuid.ToString();
            //    string temp = tempuid.ToString();
            //    uid = temp.Substring(9, 36);
            //    textBox1.Text = String.Empty;
            //    textBox2.Text = String.Empty;
            //}
            //logOut.Visible = true;
            //button2.Visible = false;



            if (textBox1.Text != "" || textBox2.Text != "")
            {
                try
                {
                    User user = new User();
                    user.Name = textBox1.Text;
                    user.Passwd = textBox2.Text;
                    Session_Id tempuid = client.Login(user);
                    if (tempuid.ToString().Contains("N/A"))
                    {
                        label1.Text = "Bejelentkezés sikertelen";
                    }
                    else
                    {
                        string temp = tempuid.ToString();
                        uid = temp.Substring(9, 36);
                    }
                }
                catch (RpcException x)
                {
                    label1.Text = "Szerver hiba";
                }
            }

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            logOut.Visible = true;
            button2.Visible = false;
        }
        private void logOut_Click(object sender, EventArgs e)
        {
            //Session_Id temp = new Session_Id(); temp.Id = uid;
            //Session_Id tempuid = client.Logout(temp);
            //label1.Text = tempuid.ToString();
            //string temp2 = tempuid.ToString();
            //uid = temp2.Substring(9, 36);
            uid = null;
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            logOut.Visible = false;
            button2.Visible = true;

            try
            {
                Session_Id s = new Session_Id();
                s.Id = uid;
                Result res = client.Logout(s);
                label1.Text = res.ToString();
                uid = null;
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                logOut.Visible = false;
                button2.Visible = true;
            }
            catch (RpcException x)
            {
                label1.Text = "Szerver hiba";
            }
        }

        private async void button1_Click(object sender, EventArgs e) //list gomb
        {
            if (uid != null)
            {
                listBox1.Items.Clear();
                using (var call = client.List(new Empty()))
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        Product products = call.ResponseStream.Current;
                        string temp;
                        if (products.Type == null)
                        {
                            temp = products.Brand + " " + products.Model + " " + products.Price;
                        }
                        else
                        {
                            temp = products.Brand + " " + products.Model + " " + products.Type + " " + products.Price;
                        }

                        listBox1.Items.Add(temp);
                    }
                }
            }

        }

        private async void button3_Click(object sender, EventArgs e) //add gomb
        {
            if (uid != null)
            {
                try
                {
                    using (var call = client.List(new Empty()))
                    {
                        while (await call.ResponseStream.MoveNext())
                        {
                            Data data = new Data();
                            data.Brand = textBox3.Text; data.Model = textBox4.Text; data.Type = textBox5.Text; data.Price = int.Parse(textBox8.Text);
                            data.Uid = uid;
                            Result res = client.Add(data);
                            label1.Text = res.ToString();
                        }
                    }
                }
                catch (Exception x)
                {
                    label1.Text = "Szerver hiba";
                }
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (uid != null)
            {
                try
                {
                    if (!(int.Parse(textBox7.Text) >= 0))
                    {
                        Product3 data = new Product3();
                        data.Code = updateID.Text;
                        data.Uid = uid;
                        data.Brand = updateModel.Text;
                        data.Model = updateModel.Text;
                        data.Type = updateModel.Text;
                        data.Price = int.Parse(updatePrice.Text) - 1;
                        Result ered = client.Bid(data);
                        label1.Text = ered.ToString();
                    }
                    else
                    {
                        label1.Text = "Nem lehet negatív az ár";
                    }

                }
                catch (Exception ex)
                {
                    label1.Text = "Szerver hiba";
                }
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (uid != null)
            {
                try
                {
                    Product2 n = new Product2();
                    n.Code = deleteId.Text;
                    n.Uid = uid;
                    Result res = client.Delete(n);
                    label1.Text = res.ToString();
                }
                catch (Exception)
                {
                    label1.Text = "Szerver hiba";
                }
            }
        }
        //trash
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
        }
        private void button4_Click(object sender, EventArgs e) //bid gomb
        {
            //Product2 data = new Product2();
            //data.Code = textBox6.Text; data.Uid = uid;
            //data.Price = int.Parse(textBox7.Text);
            //Result ered = client.Bid(data);
            //label1.Text = ered.ToString();
        }

    }
}