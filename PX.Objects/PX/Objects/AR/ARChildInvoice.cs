// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARChildInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
[Serializable]
public class ARChildInvoice : ARInvoice
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARChildInvoice>.By<ARChildInvoice.docType, ARChildInvoice.refNbr>
  {
    public static ARChildInvoice Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARChildInvoice) PrimaryKeyOf<ARChildInvoice>.By<ARChildInvoice.docType, ARChildInvoice.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARChildInvoice.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARChildInvoice.refNbr>
  {
  }
}
