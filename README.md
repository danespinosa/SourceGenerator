# Repro

Run the ConsoleApp project, the output should be as below
```shell
Generator says: Hi from 'Generated Code'
Built at: 7/30/2024 6:41:19 PM +00:00
```
If the Library/HelloSourceGenerator.cs Execute method gets modified as below the source generator stops working and Program.g.cs is not generated and compiled anymore

```c#
        public void Execute(GeneratorExecutionContext context)
        {
            // Find the main method
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

            // Below if the code would invoke TimeProvider.System.GetUtcNow() instead of DateTimeOffset.UtcNow the generator stops compiling/generating the code.
            string source = $@"// <auto-generated/>
using System;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}
{{
    public static partial class {mainMethod.ContainingType.Name}
    {{
        static partial void HelloFrom(string name) =>
            Console.WriteLine($""Generator says: Hi from '{{name}}'"");
        static partial void BuildAt() =>
            Console.WriteLine($""Built at: {TimeProvider.System.GetUtcNow()}"");
    }}
}}
";
            var typeName = mainMethod.ContainingType.Name;

            // Add the source code to the compilation
            context.AddSource($"{typeName}.g.cs", source);
        }
```
