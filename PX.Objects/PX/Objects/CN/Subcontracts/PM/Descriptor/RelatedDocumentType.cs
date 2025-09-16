// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PM.Descriptor.RelatedDocumentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CN.Subcontracts.PM.Descriptor;

public class RelatedDocumentType
{
  public const 
  #nullable disable
  string AllCommitmentsType = "ALL";
  public const string PurchaseOrderType = "RO + PD";
  public const string SubcontractType = "RS";
  public const string AllCommitmentsLabel = "All Commitments";
  public const string PurchaseOrderLabel = "Purchase Order";
  public const string SubcontractLabel = "Subcontract";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("ALL", "All Commitments"),
        PXStringListAttribute.Pair("RO + PD", "Purchase Order"),
        PXStringListAttribute.Pair("RS", "Subcontract")
      })
    {
    }
  }

  public class DetailListAttribute : PXStringListAttribute
  {
    public DetailListAttribute()
      : base(new (string, string)[3]
      {
        ("RO", "Purchase Order, Normal"),
        ("RS", "Subcontract"),
        ("PD", "Purchase Order, Project Drop-Ship")
      })
    {
    }
  }

  public class all : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RelatedDocumentType.all>
  {
    public all()
      : base("ALL")
    {
    }
  }

  public class purchaseOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    RelatedDocumentType.purchaseOrder>
  {
    public purchaseOrder()
      : base("RO + PD")
    {
    }
  }

  public class subcontract : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RelatedDocumentType.subcontract>
  {
    public subcontract()
      : base("RS")
    {
    }
  }
}
