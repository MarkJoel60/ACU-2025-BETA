// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.EntityFile
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.RichTextEdit;

/// <exclude />
public class EntityFile
{
  public EntityFile(string name, string comment, string size)
  {
    this.Name = name;
    this.Comment = comment;
    this.Size = string.IsNullOrEmpty(size) ? string.Empty : size;
  }

  public string Name { get; set; }

  public string ShortName => EntityFile.ShortenName(this.Name);

  public string Comment { get; set; }

  public string Size { get; set; }

  public static string ShortenName(string name)
  {
    if (!string.IsNullOrEmpty(name))
    {
      int num = System.Math.Max(name.LastIndexOf('/'), name.LastIndexOf('\\'));
      if (num >= 0)
        return name.Substring(num + 1);
    }
    return name;
  }
}
