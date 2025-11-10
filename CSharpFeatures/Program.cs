using CSharpFeatures.Generics;

class Program
{
    public static void Main()
    {
        BeforeGenerics();
        GenericMethodExample();
    }

    private static void BeforeGenerics()
    {
        CollectionsBeforeGenerics collectionsBeforeGenerics = new CollectionsBeforeGenerics();
        var namesArray = collectionsBeforeGenerics.GetNames();
        collectionsBeforeGenerics.PrintNames(namesArray);

        var namesArrayList = collectionsBeforeGenerics.GetNamesUsingArrayList();
        collectionsBeforeGenerics.PrintNames(namesArrayList);

        var namesStringCollection = collectionsBeforeGenerics.GetNamesUsingStringCollection();
        collectionsBeforeGenerics.PrintNames(namesStringCollection);
    }

    private static void GenericMethodExample()
    {
        List<string> names = new List<string>() { "Welcome", "To", "Da Nang, ", "VietNam" };
        GenericHelper.PrintNames(names);

        List<int> namesInt = new List<int>() { 1, 2, 3, 4 };
        GenericHelper.PrintNames(namesInt);
    }
}