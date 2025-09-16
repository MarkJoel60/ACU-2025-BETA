// Decompiled with JetBrains decompiler
// Type: PX.Api.FileEx
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.IO;

#nullable enable
namespace PX.Api;

public static class FileEx
{
  public static void RemoveReadonly(string path)
  {
    if (!File.Exists(path) || (File.GetAttributes(path) & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
      return;
    File.SetAttributes(path, FileAttributes.Normal);
  }
}
