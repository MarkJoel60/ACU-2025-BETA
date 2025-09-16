// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFileHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.SM;

public static class UploadFileHelper
{
  public static int BytesToKilobytes(int byteSize)
  {
    return (int) Math.Ceiling((double) byteSize / 1024.0);
  }

  internal static string GetOriginalName(string fileName, string versionName)
  {
    return !(FileInfo.GetShortName(fileName) == versionName) ? versionName : (string) null;
  }
}
