# Dotnet UEFI Manager (DUM)

DUM is a essentially UEFI mappings for C#. This is
for my project <a>Ellie</a>; Ellie is an OS written
entirely in C# and HVCPU Assemblies.

# Running DUM based applications

In order to run DUM applications you will need to get
your code from code to a VHDX file. Easiest method here
is to copy the markup in``DUM.csproj`` and paste it into
your projects ``.csproj`` file.

After you take care of the CSPROJ file you can proceed with
building your project:

```
dotnet publish -p:BuildVHDX=true
```

# How do I get around C#'s GC?

C#'s GC shouldn't run or mess with unmanaged code. You should
be able to just directly set addresses to values. However, if
you are looking for something to help you manage your memory,
you can reference <a>Ellie's</a> kernel or <a>HVCPU's</a> GC.
Note that neither of these are recommended for use within an
operating system due to the fact that you will likely end with
a segfault.