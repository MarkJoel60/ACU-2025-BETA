// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXListItem
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXListItem : PXRtfElement
{
  private ListNumberType numberType;
  private List<PXRtfElement> children = new List<PXRtfElement>();
  private int level;
  private int levelIndent = 360;
  private int id;
  public int TableLevel;

  public PXListItem(PXDocument document, ListNumberType numberType, int level, int id)
    : base(document)
  {
    this.numberType = numberType;
    this.level = level;
    this.id = id;
  }

  public int Level => this.level;

  public int LevelIndent
  {
    get => this.levelIndent;
    set => this.levelIndent = value;
  }

  public List<PXRtfElement> Children => this.children;

  public override void Render(StringBuilder result)
  {
    result.Append(Environment.NewLine);
    if (this.TableLevel > 0)
    {
      result.Append("\\intbl\\itap");
      result.Append(this.TableLevel);
    }
    result.Append("\\fi-360\\adjustright");
    result.Append("\\ls");
    result.Append(this.document.GetListCode(this.id, this.numberType));
    if (this.Level != 0)
    {
      result.Append("\\ilvl");
      result.Append(this.Level);
      result.Append("\\li");
      result.AppendLine(((this.Level + 1) * this.LevelIndent).ToString());
    }
    else
    {
      result.Append("\\li");
      result.AppendLine(this.LevelIndent.ToString());
    }
    result.Append("{");
    foreach (PXRtfElement child in this.children)
      child.Render(result);
    result.Append(Environment.NewLine);
    result.AppendLine("}");
    result.AppendLine("\\par\\pard");
  }
}
