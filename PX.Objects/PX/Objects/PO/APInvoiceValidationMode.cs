// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.APInvoiceValidationMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PO;

public class APInvoiceValidationMode
{
  public const string None = "N";
  public const string Warning = "W";

  public class List : PXStringListAttribute
  {
    public List()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("N", "No Validation"),
        PXStringListAttribute.Pair("W", "Validate with Warning")
      })
    {
    }
  }
}
