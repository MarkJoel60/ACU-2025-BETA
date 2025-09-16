// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXListDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXListDefinition : PXRtfElement
{
  private Random rand = new Random();
  private ListNumberType numberType;
  private int levelIndent = 720;
  private int id;

  public PXListDefinition(PXDocument document, ListNumberType numberType, int id)
    : base(document)
  {
    this.numberType = numberType;
    this.id = id;
  }

  /// <summary>Gets list definition ID.</summary>
  public long ID => (long) this.id;

  public ListNumberType Type => this.numberType;

  /// <summary>
  /// Gets or sets indentation of each single level for a list in twips.
  /// </summary>
  public int LevelIndent
  {
    get => this.levelIndent;
    set => this.levelIndent = value;
  }

  public override void Render(StringBuilder result)
  {
    result.Append("{\\list\\listtemplateid");
    result.Append(this.rand.Next());
    result.AppendLine("\\listhybrid");
    for (int index = 0; index < 9; ++index)
    {
      result.Append(Environment.NewLine);
      result.Append("{\\listlevel\\levelnfc");
      result.Append((int) this.numberType);
      result.Append("\\levelnfcn");
      result.Append((int) this.numberType);
      result.AppendLine("\\levelfollow0");
      result.Append("{\\leveltext\\leveltemplateid");
      result.Append(this.rand.Next());
      switch (this.numberType)
      {
        case ListNumberType.Arabic:
          result.Append("\\'02\\'0");
          result.Append(index);
          result.AppendLine(".;}");
          result.Append("{\\levelnumbers\\'01;}");
          result.Append("\\f0");
          result.Append("\\levelstartat1");
          break;
        case ListNumberType.Bullet:
          result.Append("\\'01");
          if (index % 3 == 0)
            result.AppendLine("\\u-3913 ?;}");
          else if (index % 3 == 1)
            result.AppendLine("o;}");
          else
            result.AppendLine("\\u-3929 ?;}");
          result.Append("{\\levelnumbers;}");
          result.Append("\\f1");
          break;
        case ListNumberType.NoNumber:
          result.Append("\\'01");
          result.AppendLine(" ;}");
          result.Append("{\\levelnumbers;}");
          result.Append("\\f1");
          break;
      }
      result.Append("\\fi-360\\li");
      result.AppendLine(((index + 1) * this.levelIndent).ToString());
      result.Append("}");
    }
    result.Append(Environment.NewLine);
    result.Append(Environment.NewLine);
    result.AppendLine("{\\listname ;}");
    result.Append("\\listid");
    result.AppendLine(this.ID.ToString());
    result.AppendLine("}");
  }
}
