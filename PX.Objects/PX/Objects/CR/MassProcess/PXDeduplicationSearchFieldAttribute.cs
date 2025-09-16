// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MassProcess.PXDeduplicationSearchFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using System;

#nullable disable
namespace PX.Objects.CR.MassProcess;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class PXDeduplicationSearchFieldAttribute : PXEventSubscriberAttribute
{
  public PXDeduplicationSearchFieldAttribute(bool sameLevelOnly = false, bool accountToAccount = true)
  {
    if (sameLevelOnly)
      this.ValidationTypes = new string[3]
      {
        "LL",
        "CC",
        "AA"
      };
    else if (!accountToAccount)
      this.ValidationTypes = new string[6]
      {
        "LL",
        "LC",
        "LA",
        "CC",
        "CL",
        "CA"
      };
    else
      this.ValidationTypes = new string[7]
      {
        "LL",
        "LC",
        "LA",
        "CC",
        "CL",
        "CA",
        "AA"
      };
  }

  public string[] ValidationTypes { get; set; }

  public virtual object[] ConvertValue(object input, string transformationRule)
  {
    return input.SingleToArray<object>();
  }
}
