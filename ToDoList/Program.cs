using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ToDoListApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Ändringar som kanske måste göras vid pålägg av ui: (ALLT KLART)
                // Fixa så att allt i json fil har id, problem som måste lösas då är att alla ID måste uppdateras när ett objekt tas bort,
                //      gäller både tasks och lists
                // Fixa ID Till när vi ska koppla på ett UI, måste kanske ha det för att koppla tryck till toggle
                // Fixa så att man inte kan få duplicate av lists eller tasks. Kan behöva finnas dubletter senare,
                //      då måste man jobba med ID istället


            // Många av funktionerna är i princip dubletter av varandra fast man bara ändrar några parametrar, fixa dom så att man istället lägger in dessa parametrar i en overload
            // Open recent list fixa, senaste som man varit inne i. Måste uppdatera history.json fil vid alla ändringar i
            // Tänk på om någon klass ska inherita eller använda komposition
            // Kolla igenom uppgifter för att fräscha upp minnet
            // Kolla igenom advanced för att se om något koncept går att använda i appen
            // Fixa undo på alla saker, hela vägen tillbaka till ursprung
            // Kolla upp hur man skapar overloads enkelt
            // Notera alla funktioner som en kommentar här i program för att komma ihåg allt som går att göra


            // Överkurs:
            // Kunna fixa historik på saker man tar bort för att kunna hämta tillbaka
            // GÅ igenom hela programmet, och försöka nå alla endpoints med debugger för att kolla så att allt fungerar
            // Lägg till en timer på returning för att se hur länge det är kvar i konsol. Problem att den måste köra loopen flera gånger. Alternativ att köra ny cw och få uppdatering varje sekund där
            // Gör filen bara läsbar för användaren, endast programmet som ska kunna redigera den.
            // Lägga till möjlighet att gå till förra sidan från alla funktioner, eller åtminstone de som skapar och ändrar, alltså en quit/return från överallt
            // Lägg till fler try catch blocks för att undvika att få icke specifika errors
            // När man editar en lista eller någonting i den måste man också uppdatera den i history. Måste finnas något mer effektivt sätt att lösa det på
            //      använda sig av ID blir ett eget problem eftersom om man tar bort en lista så får alla nya ID. Fixas genom att ge dem specifika ID när man skapar dom.
            //      gör isåfall samma på både tasks och subtasks då man skapar och ger ID. 
            // Gör så att AllArchiveListsOverview och AllListsOverview tar från samma interface. Några ska inte finnas, exempel create task eller create list


            // Frågor: Ska klar markering kunna göras från listan eller när man väl är inne i tasken? Menyn
            // Hur mycket av alla olika delar i kurserna förväntas vi använda? Känns som att jag inte använt så mycket av det
            // Mosh gick igenom på kurserna, dels för att jag inte riktigt har förstått användningsområdena för allt ännu, men också
            // för att vissa saker inte känns relevanta att använda i en sån här app.
            // Är det mening att detta ska finnas under meny för task: Redigera namn på lista (bekräftelse y/n)
            // och inte: Redigera task
            // Möjlighet att expandera och collapsa tasks precis som listor


            ProgramManager.CreateFiles();

            ProgramManager.Lists = ProgramManager.GetAllLists();
            ProgramManager.ArchiveLists = ProgramManager.GetArchive();
            ProgramManager.RecentList = ProgramManager.GetRecent();

            AllListsOverview.AllLists();
        }
    }
}