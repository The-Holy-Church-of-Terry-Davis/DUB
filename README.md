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

I had this question/problem for a very long time while
developing <a>Ellie</a>. I finally figured out a solution
and implemented it in <a>HVCPU's</a> Memory library. Keep
in mind that HVCPU's No-Runtime library is not recommended
for use on systems with a Kernel; You will likely end with
a segfault. However, without an OS, HVCPU will set up a
stack, heap, and GC (WITH MANAGED POINTERS!!!!) for you!