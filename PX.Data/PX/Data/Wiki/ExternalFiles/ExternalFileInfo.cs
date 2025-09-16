// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ExternalFiles.ExternalFileInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.ExternalFiles;

public class ExternalFileInfo
{
  public string Name { get; set; }

  public string FullName { get; set; }

  public System.DateTime Date { get; set; }

  public long Size { get; set; }

  public ExternalFileInfo()
  {
  }

  public ExternalFileInfo(string name) => this.Name = name;
}
