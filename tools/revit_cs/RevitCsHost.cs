// RevitCsHost: offline compile of revit-mcp send_code_to_revit scripts with the plugin's own Roslyn (4.8).
// Built by tools/revit_cs_check.py with the .NET Framework csc.exe (C# 5 syntax only: no $"", ?., =>, nameof),
// referencing the .NET 8 shared framework, and run with `dotnet RevitCsHost.dll <refs.txt> <wrapped.cs>...`.
// refs.txt: one assembly path per line. Each wrapped.cs is compiled on its own (shared references, so a batch is fast).
// Output, tab separated, one line each:  FILE <path>  |  DIAG <severity> <id> <line> <col> <message>  |  OK <path>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

public static class RevitCsHost
{
    public static int Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("usage: RevitCsHost <refs.txt> <wrapped.cs> [...]");
            return 2;
        }
        var refs = new List<MetadataReference>();
        foreach (string line in File.ReadAllLines(args[0]))
        {
            string p = line.Trim();
            if (p.Length == 0) continue;
            try { refs.Add(MetadataReference.CreateFromFile(p)); }
            catch (Exception ex) { Console.Error.WriteLine("skip reference " + p + ": " + ex.Message); }
        }
        // Same defaults as the plugin: a DLL, default language version of Roslyn 4.8 (C# 12), Release.
        var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            .WithOptimizationLevel(OptimizationLevel.Release)
            .WithNullableContextOptions(NullableContextOptions.Disable);
        int failed = 0;
        Console.OutputEncoding = new UTF8Encoding(false);
        for (int i = 1; i < args.Length; i++)
        {
            string path = args[i];
            Console.WriteLine("FILE\t" + path);
            string text = File.ReadAllText(path);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(text, new CSharpParseOptions(LanguageVersion.Default), path);
            CSharpCompilation comp = CSharpCompilation.Create("AIGeneratedCode_" + i, new SyntaxTree[] { tree }, refs, options);
            EmitResult res;
            using (var ms = new MemoryStream()) { res = comp.Emit(ms); }
            bool err = false;
            foreach (Diagnostic d in res.Diagnostics)
            {
                if (d.Severity == DiagnosticSeverity.Hidden) continue;
                if (d.Severity == DiagnosticSeverity.Error) err = true;
                var span = d.Location.GetLineSpan();
                int ln = span.StartLinePosition.Line + 1;
                int col = span.StartLinePosition.Character + 1;
                string msg = d.GetMessage().Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ');
                Console.WriteLine("DIAG\t" + d.Severity + "\t" + d.Id + "\t" + ln + "\t" + col + "\t" + msg);
            }
            if (err) failed++; else Console.WriteLine("OK\t" + path);
        }
        return failed > 0 ? 1 : 0;
    }
}
