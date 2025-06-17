# CodeEditor

This repository contains a minimal Windows 11 WPF code editor written in C#. It implements 15 tools that make it easier to create and manage small C# programs. The editor can compile the current file to an executable and run simple git and package commands.

## Building

This project targets .NET 6.0 and requires the Windows desktop SDK. To build on Windows:

```bash
msbuild CodeEditor/CodeEditor.csproj
```

## Tools

The editor includes the following tools:

1. New File
2. Open File
3. Save File
4. Save As
5. Build
6. Run
7. Debug
8. Format Code
9. Search
10. Replace
11. Git Commit
12. Git Push
13. Add Reference
14. Manage NuGet
15. Settings

All tools are implemented with basic functionality such as opening files, compiling code to an executable using the C# compiler, launching the resulting program, formatting the text editor contents and performing simple git and NuGet commands.
