using Beadando;
using Grpc.Core;
using Oracle.ManagedDataAccess.Client;

namespace Beadando.Services
{
    public class BeadandoService : Beadandopackage.BeadandopackageBase
    {
        static readonly List<string> sessions = new List<string>();
        static List<Product> Products = new List<Product>();
        static List<User> Users = new List<User>();

        public override async Task List(Empty vmi, Grpc.Core.IServerStreamWriter<Product> responseStream, Grpc.Core.ServerCallContext context)
        {
            DBConnect db = new DBConnect();
            //Products = db.Select();
            foreach (var Product in db.Select())
                Products.Add(Product);

            foreach (var Product in Products)
                await responseStream.WriteAsync(Product);

            //foreach (var Product in Products)
            //    await responseStream.WriteAsync(Product);
        }

        public override Task<Result> Add(Data data, ServerCallContext context)
        {
            if (!sessions.Contains(data.Uid))
            {
                return Task.FromResult(new Result { Success = "login" });
            }
            else
            {
                lock (Products)
                {
                    int i = 0;
                    for (i = 0; i < Products.Count; i++) ;
                    if (i < Products.Count)
                    {
                        return Task.FromResult(new Result { Success = "Exists" });
                    }
                    else
                    {
                        Product temp = new Product();
                        temp.Brand = data.Brand; temp.Price = data.Price; temp.Type = data.Type; temp.Model = data.Model;
                        Products.Add(temp);
                        return Task.FromResult(new Result { Success = "OK!" });
                    }
                }
            }
        }


        public override Task<Result> Bid(Product3 data, ServerCallContext context)
        {
            if (!sessions.Contains(data.Uid))
            {
                return Task.FromResult(new Result { Success = "Log in" });
            }

            else
            {
                //Products t = null;
                lock (Products)
                {
                    int j = 0;
                    for (j = 0; j < Products.Count; j++) ;
                    if (j >= Products.Count)
                    {
                        return Task.FromResult(new Result { Success = "No such product" });
                    }
                    else
                    {
                        Products[j].Id = data.Uid;
                        Products[j].Brand = data.Brand;
                        Products[j].Model = data.Model;
                        Products[j].Type = data.Type;
                        Products[j].Price = data.Price;
                        return Task.FromResult(new Result { Success = "OK" });
                    }
                    //else
                    //    return Task.FromResult(new Result { Success = "Low price" });
                }
            }
        }

        public override Task<Result> Logout(Session_Id id, ServerCallContext context)
        {
            lock (sessions)
            {
                sessions.Remove(id.Id);
            }
            return Task.FromResult(new Result { Success = "Loged out" }); ;

        }


        public override Task<Session_Id> Login(User user, ServerCallContext context/*,Empty vmi, Grpc.Core.IServerStreamWriter<User> responseStream, Grpc.Core.ServerCallContext context2*/)
        {
            string id = "";
            if (user.Name == "u" && user.Passwd == "p")
            {
                id = Guid.NewGuid().ToString();
                lock (sessions)
                {
                    sessions.Add(id);
                }
                return Task.FromResult(new Session_Id { Id = id });
            }
            else
            {
                return Task.FromResult(new Session_Id { Id = null });
            }

            //DBConnect db = new DBConnect();
            //foreach (var User in db.Select())
            //    Users.Add(User);

            //foreach (var User in Users)
            //    await responseStream.WriteAsync(User);

        }

        public override Task<Result> Delete(Product2 data, ServerCallContext context)
        {
            if (!sessions.Contains(data.Uid))
            {
                return Task.FromResult(new Result { Success = "login" });
            }
            else
            {
                lock (Products)
                {
                    int i = 0;
                    for (i = 0; i < Products.Count; i++) ;
                    if (i < Products.Count)
                    {
                        return Task.FromResult(new Result { Success = "Deleted" });
                        Products.RemoveAt(i);
                    }
                    else
                    {
                        return Task.FromResult(new Result { Success = "Not deleted" });
                    }
                }
            }
        }
    }
}