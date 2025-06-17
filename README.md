# CodeEditor

This repository contains a minimal Windows 11 WPF code editor written in C#. It implements a collection of tools that make it easier to create and manage small C# programs. The editor can compile files to executables, run basic git and package commands and now provides a project explorer and output window for working with multiple files.

## Building

This project targets .NET 6.0 and requires the Windows desktop SDK. To build on Windows:

```bash
msbuild CodeEditor/CodeEditor.csproj
```

## Tools

The editor includes the following tools:

1. New File
2. Open File
3. Open Project
4. Save File
5. Save As
6. Build
7. Build All
8. Run
9. Debug
10. Format Code
11. Search
12. Replace
13. Git Commit
14. Git Push
15. Add Reference
16. Manage NuGet
17. Deploy
18. Snippet Manager
19. Terminal
20. Settings
21. Check Updates

## Updating

Updates are stored in an `Updates` folder with version directories such as `v1.0` or `v1.2.4`. Use **Check Updates** to copy files from the newest version into the application directory and restart the editor.

All tools are implemented with basic functionality such as opening files, compiling code to an executable using the C# compiler, launching the resulting program, formatting text, manipulating git and NuGet and even deploying the output. A project explorer panel lets you switch between files and an output window captures build results.
