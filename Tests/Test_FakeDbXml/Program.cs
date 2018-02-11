using KD.FakeDb.XML;
using System;

namespace Test_FakeDbXml
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Krzysztof\Desktop\TestDir1";
            IFakeDatabaseXml fakeDb = new FakeDatabaseXml(path);

            Console.WriteLine($"FakeDb Name: { fakeDb.Name }");
            fakeDb.Name = "TestDir123";
            Console.WriteLine($"FakeDb Name: { fakeDb.Name }");
        }
    }
}