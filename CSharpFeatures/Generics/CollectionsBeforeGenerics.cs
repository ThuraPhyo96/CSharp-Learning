using System.Collections;
using System.Collections.Specialized;

namespace CSharpFeatures.Generics
{
    internal class CollectionsBeforeGenerics
    {
        // Need to know the size of the collection in advance
        public string[] GetNames()
        {
            string[] names = new string[4];
            names[0]= "Welcome";
            names[1] = "To";
            names[2] = "Da Nang, ";
            names[3] = "VietNam";
            return names;
        }

        // Get a compile-time error if you try to add anything else
        public ArrayList GetNamesUsingArrayList()
        {
            ArrayList names = new ArrayList();
            names.Add("Welcome");
            names.Add("To");
            // names.Add(1); // This will compile, but may cause runtime errors later
            names.Add("Da Nang, ");
            names.Add("VietNam");
            return names;
        }

        // Specifically designed to hold strings
        public StringCollection GetNamesUsingStringCollection()
        {
            StringCollection names = new StringCollection();
            names.Add("Welcome");
            names.Add("To");
            names.Add("Da Nang, ");
            names.Add("VietNam");
            return names;
        }

        public void PrintNames(string[] names)
        {
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }

        public void PrintNames(ArrayList names)
        {
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }

        public void PrintNames(StringCollection names)
        {
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
