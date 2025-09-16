// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCodeDirectoryCompiler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using PX.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web.Compilation;
using System.Web.Hosting;

#nullable disable
namespace PX.Data;

/// <exclude />
internal static class PXCodeDirectoryCompiler
{
  private static readonly ConcurrentDictionary<string, PXCodeDirectoryCompiler.CacheEntry> AssemblyCache = new ConcurrentDictionary<string, PXCodeDirectoryCompiler.CacheEntry>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  public const string SharedCodeVPath = "~/App_RuntimeCode";
  public const string SharedCodeConst = "App_RuntimeCode";
  private static readonly string SharedCodeFolder = HostingEnvironment.MapPath("~/App_RuntimeCode");
  private static List<System.Type> _compiledTypes = (List<System.Type>) null;
  private static CompilerResults _compiledResults = (CompilerResults) null;
  private static object compiledTypesLock = new object();
  private static FileSystemWatcher fileSystemWatcher;
  private static readonly object SyncRoot = new object();
  private static int _version;

  static PXCodeDirectoryCompiler()
  {
    if (!WebConfig.UseRuntimeCompilation)
      return;
    if (PXCodeDirectoryCompiler.fileSystemWatcher == null)
    {
      if (!Directory.Exists(PXCodeDirectoryCompiler.SharedCodeFolder))
        Directory.CreateDirectory(PXCodeDirectoryCompiler.SharedCodeFolder);
      PXCodeDirectoryCompiler.fileSystemWatcher = new FileSystemWatcher(PXCodeDirectoryCompiler.SharedCodeFolder);
      PXCodeDirectoryCompiler.fileSystemWatcher.IncludeSubdirectories = true;
      PXCodeDirectoryCompiler.fileSystemWatcher.EnableRaisingEvents = true;
      PXCodeDirectoryCompiler.NotifyOnChange(new System.Action(PXCodeDirectoryCompiler.ClearTypes));
    }
    PXBuildManager.TypeGetter += (Func<string, System.Type>) (typeName => PXCodeDirectoryCompiler.GetCompiledTypes().FirstOrDefault<System.Type>((Func<System.Type, bool>) (_ => _.FullName.Equals(typeName, StringComparison.OrdinalIgnoreCase))));
  }

  internal static void ClearTypes()
  {
    lock (PXCodeDirectoryCompiler.compiledTypesLock)
    {
      PXCodeDirectoryCompiler._compiledTypes = (List<System.Type>) null;
      PXCodeDirectoryCompiler._compiledResults = (CompilerResults) null;
    }
  }

  internal static CompilerResults GetCompilerResults()
  {
    if (!WebConfig.UseRuntimeCompilation)
      return (CompilerResults) null;
    if (!Directory.Exists(PXCodeDirectoryCompiler.SharedCodeFolder))
      return (CompilerResults) null;
    PXCodeDirectoryCompiler.CacheEntry cacheEntry;
    return PXCodeDirectoryCompiler.CompileFolder(PXCodeDirectoryCompiler.SharedCodeFolder) == (Assembly) null && PXCodeDirectoryCompiler.AssemblyCache.TryGetValue(PXCodeDirectoryCompiler.SharedCodeFolder, out cacheEntry) ? cacheEntry.Results : (CompilerResults) null;
  }

  public static Assembly CompileFolder(string path)
  {
    if (!WebConfig.UseRuntimeCompilation)
      return (Assembly) null;
    string[] source = Directory.Exists(path) ? Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories) : throw new PXException($"GetAssemblyFromFile: Path not found: {path}");
    if (!((IEnumerable<string>) source).Any<string>())
      return (Assembly) null;
    long num = 757602046;
    foreach (string path1 in source)
      num = num * 31L /*0x1F*/ + File.GetLastWriteTimeUtc(path1).Ticks;
    string str1 = num.ToString("X");
    PXCodeDirectoryCompiler.CacheEntry cacheEntry1;
    if (PXCodeDirectoryCompiler.AssemblyCache.TryGetValue(path, out cacheEntry1))
    {
      if (cacheEntry1.Hash == str1 && !cacheEntry1.Results.Errors.HasErrors)
        return cacheEntry1.Results.CompiledAssembly;
      PXCodeDirectoryCompiler.AssemblyCache.TryRemove(path, out cacheEntry1);
    }
    bool flag = HostingEnvironment.ApplicationVirtualPath.Equals("/WebSiteValidationDomain");
    string path2 = string.Empty;
    try
    {
      path2 = WebsiteID.Key;
    }
    catch
    {
    }
    string str2 = Path.Combine(WebConfig.CustomizationTempFilesPath, "Temp");
    if (!flag)
      str2 = Path.Combine(str2, path2);
    if (!Directory.Exists(str2))
      Directory.CreateDirectory(str2);
    string path3 = Path.Combine(str2, $"RuntimeCode_{str1}.dll");
    if (flag)
      path3 = Path.Combine(str2, $"RuntimeCode_Precompile_{str1}.dll");
    else if (WebConfig.CompilationInDebugMode)
      path3 = Path.Combine(str2, $"RuntimeCode_Debug_{str1}.dll");
    bool keepFiles = WebConfig.CompilationInDebugMode && !flag;
    if (!flag && File.Exists(path3))
    {
      Assembly assembly = Assembly.LoadFile(path3);
      PXCodeDirectoryCompiler.CacheEntry cacheEntry2 = new PXCodeDirectoryCompiler.CacheEntry()
      {
        Hash = str1,
        Results = new CompilerResults(new TempFileCollection(str2, true))
        {
          CompiledAssembly = assembly
        }
      };
      PXCodeDirectoryCompiler.AssemblyCache[path] = cacheEntry2;
      return assembly;
    }
    using (CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider())
    {
      using (TempFileCollection tempFileCollection = new TempFileCollection(str2, keepFiles))
      {
        CompilerParameters options = new CompilerParameters();
        options.TempFiles = tempFileCollection;
        options.OutputAssembly = path3;
        if (keepFiles)
        {
          options.IncludeDebugInformation = true;
          options.GenerateInMemory = false;
        }
        else
        {
          options.IncludeDebugInformation = false;
          options.GenerateInMemory = true;
        }
        HashSet<string> stringSet = new HashSet<string>();
        foreach (Assembly referencedAssembly in (IEnumerable) BuildManager.GetReferencedAssemblies())
          stringSet.Add(referencedAssembly.Location);
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
          if (assembly.GlobalAssemblyCache)
            stringSet.Add(assembly.Location);
        }
        foreach (string str3 in stringSet)
          options.ReferencedAssemblies.Add(str3);
        WindowsImpersonationContext impersonationContext = WindowsIdentity.Impersonate(IntPtr.Zero);
        try
        {
          CompilerResults compilerResults = ((CodeDomProvider) csharpCodeProvider).CompileAssemblyFromFile(options, source);
          cacheEntry1 = new PXCodeDirectoryCompiler.CacheEntry()
          {
            Hash = str1,
            Results = compilerResults
          };
        }
        catch
        {
        }
        finally
        {
          impersonationContext.Undo();
        }
      }
    }
    ++PXCodeDirectoryCompiler._version;
    PXCodeDirectoryCompiler.AssemblyCache[path] = cacheEntry1;
    return cacheEntry1.Results.Errors.HasErrors ? (Assembly) null : cacheEntry1.Results.CompiledAssembly;
  }

  public static CompilerResults CompilerResults => PXCodeDirectoryCompiler._compiledResults;

  public static List<System.Type> GetCompiledTypes<T>()
  {
    List<System.Type> compiledTypes = new List<System.Type>();
    foreach (System.Type compiledType in PXCodeDirectoryCompiler.GetCompiledTypes())
    {
      if (typeof (T).IsAssignableFrom(compiledType))
        compiledTypes.Add(compiledType);
    }
    return compiledTypes;
  }

  public static List<System.Type> GetCompiledTypes()
  {
    if (WebConfig.DesignMode || Str.IsNullOrEmpty(PXCodeDirectoryCompiler.SharedCodeFolder))
      return new List<System.Type>();
    if (!Directory.Exists(PXCodeDirectoryCompiler.SharedCodeFolder))
      Directory.CreateDirectory(PXCodeDirectoryCompiler.SharedCodeFolder);
    lock (PXCodeDirectoryCompiler.compiledTypesLock)
    {
      if (PXCodeDirectoryCompiler._compiledTypes == null && PXExtensionManager._GraphStaticInfo != null)
      {
        PXCodeDirectoryCompiler._compiledTypes = new List<System.Type>();
        PXCodeDirectoryCompiler._compiledResults = (CompilerResults) null;
        Assembly assembly = PXCodeDirectoryCompiler.CompileFolder(PXCodeDirectoryCompiler.SharedCodeFolder);
        if (assembly != (Assembly) null)
        {
          PXCodeDirectoryCompiler._compiledTypes.AddRange(assembly.ExportedTypes);
        }
        else
        {
          PXCodeDirectoryCompiler.CacheEntry cacheEntry;
          if (PXCodeDirectoryCompiler.AssemblyCache.TryGetValue(PXCodeDirectoryCompiler.SharedCodeFolder, out cacheEntry))
            PXCodeDirectoryCompiler._compiledResults = cacheEntry.Results;
        }
      }
      return PXCodeDirectoryCompiler._compiledTypes;
    }
  }

  public static int Version => PXAccess.Version + PXCodeDirectoryCompiler._version;

  public static void NotifyOnChange(System.Action a)
  {
    if (!WebConfig.UseRuntimeCompilation)
      return;
    PXCodeDirectoryCompiler.fileSystemWatcher.Changed += (FileSystemEventHandler) ((_param1, _param2) =>
    {
      lock (PXCodeDirectoryCompiler.SyncRoot)
        a();
    });
    PXCodeDirectoryCompiler.fileSystemWatcher.Deleted += (FileSystemEventHandler) ((_param1, _param2) =>
    {
      lock (PXCodeDirectoryCompiler.SyncRoot)
        a();
    });
    PXCodeDirectoryCompiler.fileSystemWatcher.Created += (FileSystemEventHandler) ((_param1, _param2) =>
    {
      lock (PXCodeDirectoryCompiler.SyncRoot)
        a();
    });
  }

  /// <exclude />
  private class CacheEntry
  {
    public CompilerResults Results;
    public string Hash;
  }
}
