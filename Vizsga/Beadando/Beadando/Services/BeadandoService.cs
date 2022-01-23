using Beadando;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Grpc.Core;
using MySqlConnector;

namespace Beadando.Services
{
    public class BeadandoService : Beadandopackage.BeadandopackageBase
    {
        static List<string> sessions = new List<string>();
        static List<Product> Products = new List<Product>();

        public override async Task List(Empty vmi, Grpc.Core.IServerStreamWriter<Product> responseStream, Grpc.Core.ServerCallContext context)
        {
            //DBConnect db = new DBConnect();
            ////Products = db.Select();
            //foreach (var Product in db.Select())
            //    Products.Add(Product);

            //foreach (var Product in Products)
            //    await responseStream.WriteAsync(Product);

            ////foreach (var Product in Products)
            ////    await responseStream.WriteAsync(Product);
            if (OpenConnection())
            {
                string query = "SELECT * FROM cars";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Brand = reader.GetString("Brand");
                    product.Model = reader.GetString("Model");
                    product.Type = reader.GetString("Type");
                    product.Price = reader.GetInt32("Price");
                    //product.Id = reader.GetInt32("id");
                    Products.Add(product);
                }
            }
            foreach (var Product in Products)
            {
                await responseStream.WriteAsync(Product);
            }
        }

        public override Task<Result> Add(Data data, ServerCallContext context)
        {
            //lock (Products)
            //{
            //    int i = 0;
            //    for (i = 0; i < Products.Count; i++) ;
            //    if (i < Products.Count)
            //    {
            //        return Task.FromResult(new Result { Success = "Exists" });
            //    }
            //    else
            //    {
            //        Product temp = new Product();
            //        temp.Brand = data.Brand; temp.Price = data.Price; temp.Type = data.Type; temp.Model = data.Model;
            //        Products.Add(temp);
            //        return Task.FromResult(new Result { Success = "OK!" });
            //    }
            //}
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE session_string='" + data.Uid + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count == 1)
                {
                    if (data.Price > 0)
                    {
                        query = "SELECT COUNT(*) FROM cars WHERE code= '" + data.Code + "'";
                        cmd = new MySqlCommand(query, connection);
                        count = int.Parse(cmd.ExecuteScalar() + "");
                        if (count == 1)
                        {
                            return Task.FromResult(new Result { Success = "Az autó már létezik!" });
                        }
                        else
                        {
                            string user = "";
                            query = "SELECT username FROM users WHERE session_string='" + data.Uid + "'";
                            cmd = new MySqlCommand(query, connection);
                            MySqlDataReader dataReader = cmd.ExecuteReader();
                            while (dataReader.Read())
                            {
                                user = dataReader.GetString(0);
                            }
                            dataReader.Close();
                            query = "INSERT INTO cars (brand,model,type,price) VALUES('" + data.Brand + "','" + data.Model + "','" + data.Type + "','" + data.Price + "','" + "')";
                            cmd = new MySqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "Sikeresen hozzáadás!" });
                        }
                    }
                    else
                    {
                        return Task.FromResult(new Result { Success = "Hibás ár! Minimum 0 Ft" });
                    }
                }
                else
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Be kell jelentkezni!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem  elérhetõ az adatbázist!" });
            }

        }

        public override Task<Result> Bid(Product3 data, ServerCallContext context)
        {
            //if (!sessions.Contains(data.Uid))
            //{
            //    return Task.FromResult(new Result { Success = "Log in" });
            //}

            //else
            //{
            //    //Products t = null;
            //    lock (Products)
            //    {
            //        int j = 0;
            //        for (j = 0; j < Products.Count; j++) ;
            //        if (j >= Products.Count)
            //        {
            //            return Task.FromResult(new Result { Success = "No such product" });
            //        }
            //        else
            //        {
            //            Products[j].Id = data.Uid;
            //            Products[j].Brand = data.Brand;
            //            Products[j].Model = data.Model;
            //            Products[j].Type = data.Type;
            //            Products[j].Price = data.Price;
            //            return Task.FromResult(new Result { Success = "OK" });
            //        }
            //        //else
            //        //    return Task.FromResult(new Result { Success = "Low price" });

            //    }
            //}
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE session_string='" + data.Uid + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count == 1)
                {
                    query = "SELECT COUNT(*) FROM products WHERE code='" + data.Code + "'";
                    cmd = new MySqlCommand(query, connection);
                    count = int.Parse(cmd.ExecuteScalar() + "");
                    if (count == 1)
                    {
                        string user = "";
                        query = "SELECT username FROM users WHERE session_string ='" + data.Uid + "'";
                        cmd = new MySqlCommand(query, connection);
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            user = dataReader.GetString(0);
                        }
                        dataReader.Close();

                        query = "SELECT COUNT(*) FROM products WHERE username='" + user + "' AND code='" + data.Code + "'";
                        cmd = new MySqlCommand(query, connection);
                        count = int.Parse(cmd.ExecuteScalar() + "");

                        if (count == 1)
                        {
                            query = "UPDATE cars SET Price='" + data.Price + "'WHERE code='" + data.Code + "';";
                            cmd = new MySqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "OK!" });
                        }
                        else
                        {
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "Más áruját nem lehet változtatni!" });
                        }
                    }
                    else
                    {
                        CloseConnection();
                        return Task.FromResult(new Result { Success = "Nem létezik az áru!" });
                    }
                }
                else
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Be kell jelentkezni!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett elérni az adatbázist!" });
            }
        }

        public override Task<Result> Logout(Session_Id id, ServerCallContext context)
        {
            //lock (sessions)
            //{
            //    sessions.Remove(id.Id);
            //}
            //return Task.FromResult(new Result { Success = "Loged out" });
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE session_string='" + id.Id + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count == 1)
                {
                    query = "UPDATE users SET session_string='' WHERE session_string='" + id.Id + "'";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return Task.FromResult(new Result { Success = "Sikeres Kijelentkezés!" });
                }
                else
                {
                    return Task.FromResult(new Result { Success = "Nincs bejelentkezve!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett elérni az adatbázist!" });
            }
        }

        public override Task<Session_Id> Login(User user, ServerCallContext context/*,Empty vmi, Grpc.Core.IServerStreamWriter<User> responseStream, Grpc.Core.ServerCallContext context2*/)
        {
            //string id = "";
            //if (user.Name == "u" && user.Passwd == "p")
            //{
            //    id = Guid.NewGuid().ToString();
            //    lock (sessions)
            //    {
            //        sessions.Add(id);
            //    }
            //    return Task.FromResult(new Session_Id { Id = id });
            //}
            //else
            //{
            //    return Task.FromResult(new Session_Id { Id = null });
            //}

            //DBConnect db = new DBConnect();
            //foreach (var User in db.Select())
            //    Users.Add(User);

            //foreach (var User in Users)
            //    await responseStream.WriteAsync(User);

            string id = "";
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE username='" + user.Name + "' AND password='" + user.Passwd + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count == 1)
                {
                    id = Guid.NewGuid().ToString();
                    query = "UPDATE users SET session_string='" + id + "'WHERE username='" + user.Name + "'";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    this.CloseConnection();
                    return Task.FromResult(new Session_Id { Id = id });
                }
                else
                {
                    this.CloseConnection();
                    return Task.FromResult(new Session_Id { Id = "N/A" });
                }
            }
            else
            {
                return Task.FromResult(new Session_Id { Id = "Nem lehetett elérni az adatbázist!" });
            }

        }

        public override Task<Result> Delete(Product2 data, ServerCallContext context)
        {
            //if (!sessions.Contains(data.Uid))
            //{
            //    return Task.FromResult(new Result { Success = "login" });
            //}
            //else
            //{
            //    lock (Products)
            //    {
            //        int i = 0;
            //        for (i = 0; i < Products.Count; i++) ;
            //        if (i < Products.Count)
            //        {
            //            return Task.FromResult(new Result { Success = "Deleted" });
            //            Products.RemoveAt(i);
            //        }
            //        else
            //        {
            //            return Task.FromResult(new Result { Success = "Not deleted" });
            //        }
            //    }
            //}
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE session_string='" + data.Uid + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count == 1)
                {
                    query = "SELECT COUNT(*) FROM products WHERE code='" + data.Code + "'";
                    cmd = new MySqlCommand(query, connection);
                    count = int.Parse(cmd.ExecuteScalar() + "");
                    if (count == 1)
                    {
                        string user = "";
                        query = "SELECT username FROM users WHERE session_string ='" + data.Uid + "'";
                        cmd = new MySqlCommand(query, connection);
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            user = dataReader.GetString(0);
                        }
                        dataReader.Close();

                        query = "SELECT COUNT(*) FROM cars WHERE username='" + user + "' AND code='" + data.Code + "'";
                        cmd = new MySqlCommand(query, connection);
                        count = int.Parse(cmd.ExecuteScalar() + "");
                        if (count == 1)
                        {
                            query = "DELETE FROM cars WHERE code='" + data.Code + "'";
                            cmd = new MySqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "OK!" });
                        }
                        else
                        {
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "Más autóját nem lehet törölni!" });
                        }
                    }
                    else
                    {
                        CloseConnection();
                        return Task.FromResult(new Result { Success = "Nem létezik az autó!" });
                    }
                }
                else
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Be kell jelentkezni!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett elérni az adatbázist!" });
            }
        }

        MySqlConnection connection = new MySqlConnection("SERVER=localhost;DATABASE=szop_vizsga;UID=root;PASSWORD= ;");
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password");
                        break;
                }
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}