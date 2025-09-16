// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSAttributeValueValidationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.CS;

public class CSAttributeValueValidationAttribute : ValueValidationAttribute
{
  private readonly System.Type sourceAttribute;

  public CSAttributeValueValidationAttribute(System.Type sourceAttribute)
  {
    this.sourceAttribute = sourceAttribute;
  }

  protected override string FindValidationRegexp(PXCache sender, object row)
  {
    string str = (string) sender.GetValue(row, this.sourceAttribute.Name);
    return str != null ? ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[str].With<CRAttribute.Attribute, string>((Func<CRAttribute.Attribute, string>) (attr => attr.RegExp)) : (string) null;
  }
}
