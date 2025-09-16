// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXSimpleTemplateDeclaration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXSimpleTemplateDeclaration(string templateName, PXWikiParserContext context) : 
  PXCustomTemplateDeclaration(templateName, context)
{
  protected override void InitParamDeclarations()
  {
    base.InitParamDeclarations();
    if (string.IsNullOrEmpty(this.Value))
      return;
    int startPos = 0;
    while (startPos < this.Value.Length)
      this.SeekParam(ref startPos);
  }

  private void SeekParam(ref int startPos)
  {
    string str = "";
    StringBuilder stringBuilder = new StringBuilder();
    while (str != "((" && startPos < this.Value.Length)
      str = this.GetNext(ref startPos);
    if (startPos >= this.Value.Length)
      return;
    int leftIndex = startPos - 2;
    for (string next = this.GetNext(ref startPos); next != "))" && startPos < this.Value.Length; next = this.GetNext(ref startPos))
      stringBuilder.Append(next);
    this.CreateParam(stringBuilder.ToString(), leftIndex, startPos);
  }

  private string GetNext(ref int index)
  {
    StringBuilder stringBuilder = new StringBuilder();
    while (index < this.Value.Length && (this.Value[index] == '(' || this.Value[index] == ')'))
    {
      stringBuilder.Append(this.Value[index]);
      ++index;
    }
    if (stringBuilder.Length == 2 && (stringBuilder.ToString() == "((" || stringBuilder.ToString() == "))"))
      return stringBuilder.ToString();
    while (index < this.Value.Length && this.Value[index] != '(' && this.Value[index] != ')')
    {
      stringBuilder.Append(this.Value[index]);
      ++index;
    }
    return stringBuilder.ToString();
  }

  private void CreateParam(string parameter, int leftIndex, int rightIndex)
  {
    parameter = parameter.Trim();
    if (string.IsNullOrEmpty(parameter))
      return;
    string[] strArray = parameter.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
    this.paramDeclarations.Add(strArray.Length != 2 ? new PXTemplateParamDeclaration(strArray[0].Trim(), leftIndex, rightIndex) : new PXTemplateParamDeclaration(strArray[0].Trim(), strArray[1].Trim(), leftIndex, rightIndex));
  }
}
