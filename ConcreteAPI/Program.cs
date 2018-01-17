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
            {
                Console.WriteLine("Servidor online!");
                Console.ReadLine();
                }
        }
    }
}
