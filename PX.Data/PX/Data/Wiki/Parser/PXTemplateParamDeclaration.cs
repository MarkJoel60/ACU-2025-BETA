// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXTemplateParamDeclaration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXTemplateParamDeclaration
{
  public readonly string Name;
  public readonly string DefaultValue;
  public readonly int StartPos;
  public readonly int EndPos;

  public PXTemplateParamDeclaration(string name, string defaultValue, int startPos, int endPos)
  {
    this.Name = name;
    this.DefaultValue = defaultValue;
    this.StartPos = startPos;
    this.EndPos = endPos;
  }

  public PXTemplateParamDeclaration(string name, int startPos, int endPos)
    : this(name, (string) null, startPos, endPos)
  {
  }
}
