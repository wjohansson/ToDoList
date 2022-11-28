using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ToDoListApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Fixa så att allt i json fil har id, problem som måste lösas då är att alla ID måste uppdateras när ett objekt deletas, gäller både tasks och lists
            //Strukturera om funktioner så att ViewList inte innehåller allt, flytta ut delas som inte tillhör där
            
            
            Store.CreateSaveFile();

            Overview.AllLists();


            //ToDoList.CreateList();



            //list.Add(new Task()
            //{
            //    TaskId = 1,
            //    TaskTitle = "New test from program",
            //    Completed = false
            //});

            //list.Add(new Task()
            //{
            //    TaskId = 2,
            //    TaskTitle = "Another test from program",
            //    Completed = false
            //});

            //jsonData = JsonSerializer.Serialize(list);
            //File.WriteAllText(path, jsonData);






            //JsonSerializer.SerializeAsync(path);

            //Overview.AllLists();


            //var task = new Task("Title");
            //task.CreateTask();
        }
    }
}