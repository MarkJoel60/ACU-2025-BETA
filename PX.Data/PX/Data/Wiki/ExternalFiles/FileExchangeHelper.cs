// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ExternalFiles.FileExchangeHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Data.Wiki.ExternalFiles;

public static class FileExchangeHelper
{
  private static Dictionary<string, System.Type> _exchangers = new Dictionary<string, System.Type>();

  public static IEnumerable<string> Exchangers
  {
    get => (IEnumerable<string>) FileExchangeHelper._exchangers.Keys;
  }

  static FileExchangeHelper()
  {
    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
    {
      if (PXSubstManager.IsSuitableTypeExportAssembly(assembly, true))
      {
        System.Type[] typeArray = (System.Type[]) null;
        try
        {
          if (!assembly.IsDynamic)
            typeArray = assembly.GetExportedTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
          typeArray = ex.Types;
        }
        catch
        {
          continue;
        }
        if (typeArray != null)
        {
          foreach (System.Type type in typeArray)
          {
            if (!(type != (System.Type) null) || typeof (IFileExchange).IsAssignableFrom(type) && !type.IsAbstract && !(type == typeof (IFileExchange)) && type.IsClass)
              FileExchangeHelper._exchangers.Add(((IFileExchange) Activator.CreateInstance(type, new object[4])).Code, type);
          }
        }
      }
    }
  }

  public static IFileExchange GetExchanger(string code)
  {
    return FileExchangeHelper.GetExchanger(code, (string) null, (string) null);
  }

  public static IFileExchange GetExchanger(
    string code,
    string login,
    string password,
    FileInfo sshCertificate = null,
    string sshPassword = null)
  {
    if (string.IsNullOrEmpty(code) || !FileExchangeHelper._exchangers.ContainsKey(code))
      return (IFileExchange) null;
    return (IFileExchange) Activator.CreateInstance(FileExchangeHelper._exchangers[code], (object) login, (object) password, (object) sshCertificate, (object) sshPassword);
  }
}
