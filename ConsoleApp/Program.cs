namespace SourceGenerator
{
    partial class Program
    {
        static void Main(string[] args)
        {
            HelloFrom("Generated Code");
            BuildAt();
            BuildAtTimeProvider();
        }

        static partial void HelloFrom(string name);

        static partial void BuildAt();
        static partial void BuildAtTimeProvider();
    }
}
