using System;
using Microsoft.Owin.Hosting;

namespace ConcreteAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Executa/levanta o servidor
             using (WebApp.Start<Startup>("http://localhost:8080"))
            //using (WebApp.Start<Startup>("https://concreteapi.azurewebsites.net:443"))
            {
                Console.WriteLine("Servidor online!");
                Console.ReadLine();
                }
        }
    }
}
