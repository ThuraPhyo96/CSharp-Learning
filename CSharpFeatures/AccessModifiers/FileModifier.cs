namespace CSharpFeatures.AccessModifiers
{
    file interface IWidget
    {
        int GetSize();
    }

    file class HiddenWidget
    {
        public int Work() => 42;
    }

    file class Widget : IWidget
    {
        public int GetSize()
        {
            var worker = new HiddenWidget();
            return worker.Work();
        }
    }
}
