// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXCustomTemplateDeclaration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXCustomTemplateDeclaration
{
  protected List<PXTemplateParamDeclaration> paramDeclarations = new List<PXTemplateParamDeclaration>();
  protected PXWikiParserContext context;
  protected string templateName;
  private string value;
  public bool IgnoreParNames;

  public PXCustomTemplateDeclaration(string templateName, PXWikiParserContext context)
  {
    this.templateName = templateName.Trim();
    this.context = context;
    this.InitParamDeclarations();
  }

  public PXTemplateParamDeclaration[] ParamDeclarations => this.paramDeclarations.ToArray();

  protected string Value => this.value;

  public virtual string GetContent(Dictionary<string, string> pars)
  {
    if (string.IsNullOrEmpty(this.Value))
      return this.Value;
    StringBuilder result = new StringBuilder();
    if (this.IgnoreParNames)
      this.SetParametersIgnoreNames(result, pars);
    else
      this.SetParametersByNames(result, pars);
    return result.ToString();
  }

  protected virtual void InitParamDeclarations()
  {
    this.value = this.context.ReadTemplateContent(this.templateName);
    if (this.Value == null)
      return;
    this.value = this.Value.TrimStart();
  }

  private void SetParametersByNames(StringBuilder result, Dictionary<string, string> pars)
  {
    PXCustomTemplateDeclaration.SetParametersByNames(result, this.Value, (IEnumerable<PXTemplateParamDeclaration>) this.paramDeclarations, (IDictionary<string, string>) pars);
  }

  private static void SetParametersByNames(
    StringBuilder result,
    string text,
    IEnumerable<PXTemplateParamDeclaration> paramDeclarations,
    IDictionary<string, string> pars)
  {
    int startIndex = 0;
    foreach (PXTemplateParamDeclaration paramDeclaration in paramDeclarations)
    {
      if (startIndex < text.Length)
      {
        result.Append(text.Substring(startIndex, paramDeclaration.StartPos - startIndex));
        if (pars.ContainsKey(paramDeclaration.Name) && pars[paramDeclaration.Name] != null)
          result.Append(pars[paramDeclaration.Name]);
        else if (paramDeclaration.DefaultValue != null)
          result.Append(paramDeclaration.DefaultValue);
        else
          result.Append(paramDeclaration.Name);
        startIndex = paramDeclaration.EndPos;
      }
      else
        break;
    }
    if (startIndex >= text.Length)
      return;
    result.Append(text.Substring(startIndex));
  }

  private void SetParametersIgnoreNames(StringBuilder result, Dictionary<string, string> pars)
  {
    int startIndex = 0;
    int index = 0;
    foreach (string key in pars.Keys)
    {
      if (startIndex < this.Value.Length)
      {
        if (index < this.paramDeclarations.Count)
        {
          result.Append(this.Value.Substring(startIndex, this.paramDeclarations[index].StartPos - startIndex));
          result.Append(pars[key]);
          startIndex = this.paramDeclarations[index].EndPos;
          ++index;
        }
        else
          break;
      }
      else
        break;
    }
    if (startIndex >= this.Value.Length)
      return;
    result.Append(this.Value.Substring(startIndex));
  }
}
