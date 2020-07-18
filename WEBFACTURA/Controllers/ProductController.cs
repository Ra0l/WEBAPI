using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEBFACTURA.Models;

namespace WEBFACTURA.Controllers
{
    public class ProductController : Controller
    {
        string BaseUrl = "https://localhost:44330/api/Product/";
        
        public async Task<ActionResult> Index()
        {
            List<Product> EmpInfo = new List<Product>();
            using (var client = new HttpClient())
            {

                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("GetAll");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    EmpInfo = JsonConvert.DeserializeObject<List<Product>>(EmpResponse);
                }
                return View(EmpInfo);
            }
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create (Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/api/Product/Post");
                var postTask = client.PostAsJsonAsync<Product>("Post", product);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador");
            return View(product);
        }

        //Modificar producto
        public ActionResult Edit(int id)
        {
            //Models
            Product products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/");
                //Http get
                var responseTask = client.GetAsync("api/Product/Get/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();
                    products = readTask.Result;
                }
            }

            return View(products);
        }

        [HttpPost]
        public ActionResult Edit(Product products)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/api/Product/PutAllProduct");

                //HttpPost
                var putTask = client.PutAsJsonAsync<Product>("PutAllProducts", products);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(products);
        }

        public ActionResult Delete(int id)
        {
            //Models
            Product products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/");
                //Http get
                var responseTask = client.GetAsync("api/Product/Get/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();
                    products = readTask.Result;
                }
            }

            return View(products);
        }

        [HttpPost]
        public ActionResult Delete(Product products, int id)
        {
            using(var client= new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/");

                var deleteTask = client.DeleteAsync($"api/Product/EliminarProducto/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(products);
        }

        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}