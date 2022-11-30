using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ToDoListApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Ändringar som kanske måste göras vid pålägg av ui:
            //Fixa så att allt i json fil har id, problem som måste lösas då är att alla ID måste uppdateras när ett objekt tas bort,
            //  gäller både tasks och lists
            //Fixa ID Till när vi ska koppla på ett UI, måste kanske ha det för att koppla tryck till toggle
            //Fixa så att man inte kan få duplicate av lists eller tasks. Kan behöva finnas dubletter senare,
            //  då måste man jobba med ID istället


            //Fixa så att task title blir grön då alla tasks i den är completed
            //Både kunna arkivera och hämta tillbaka viss task
            //Gör filen bara läsbar för användaren, endast programmet som ska kunna redigera den.
            //Hamnar i konstiga loops, fixa flödet på appen, måste hoppa ut ur rätt vid alla tillfällen
            //Lägga till möjlighet att gå till förra sidan från alla funktioner, eller åtminstone de som skapar och ändrar
            //Try catch block går för djupt, försök ändra struktur som gör så att funktioner inte blir så nestade
            //Lägg till fler try catch blocks för att undvika att få icke specifika errors
            //Sätt att kolla på alla arkiverade tasks och att kunna återställa dom via en annan meny
            //Rensa hela json fil, och hela arkiv json fil med kommandon på AllListsOverview, samt 
            //Gör så att AllArchiveListsOverview och AllListsOverview tar från samma interface. Några ska inte finnas, exempel create task eller create list
            //Option att ta bort en arkiverad task som försvinner för alltid. 
            //Fixa så att en arkiverad lista försvinner om alla tasks tas bort
            //Fixa lista så till [] istället för ''. Måste göras på både archive och vanliga
            //Kunna arkivera en hel lista
            //Möjlighet att expandera och collapsa tasks precis som listor


            //Frågor: Ska klar markering kunna göras från listan eller när man väl är inne i tasken? Menyn
            //Hur mycket av alla olika delar i kurserna förväntas vi använda? Känns som att jag inte använt så mycket av det
            //  Mosh gick igenom på kurserna, dels för att jag inte riktigt har förstått användningsområdena för allt ännu, men också
            //  för att vissa saker inte känns relevanta att använda i en sån här app.
            //Är det mening att detta ska finnas under meny för task: Redigera namn på lista (bekräftelse y/n)
            //  och inte: Redigera task

            ProgramManager.CreateFiles();

            AllListsOverview.AllLists();
        }
    }
}