// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Landed Costs Receipt")]
[Serializable]
public class POLandedCostReceipt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [POLandedCostDocType.List]
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (POLandedCostDoc.docType))]
  [PXUIField(DisplayName = "Landed Cost Type", Visible = false)]
  public virtual 
  #nullable disable
  string LCDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (POLandedCostDoc.refNbr))]
  [PXUIField(DisplayName = "Landed Cost Nbr.")]
  [PXParent(typeof (POLandedCostReceipt.FK.LandedCostDocument))]
  public virtual string LCRefNbr { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "PO Receipt Type", Visible = false)]
  public virtual string POReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.")]
  [PXSelector(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Current<POLandedCostReceipt.pOReceiptType>>>>))]
  public virtual string POReceiptNbr { get; set; }

  [PXInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<POLandedCostReceipt>.By<POLandedCostReceipt.lCDocType, POLandedCostReceipt.lCRefNbr, POLandedCostReceipt.pOReceiptType, POLandedCostReceipt.pOReceiptNbr>
  {
    public static POLandedCostReceipt Find(
      PXGraph graph,
      string lCDocType,
      string lCRefNbr,
      string pOReceiptType,
      string pOReceiptNbr,
      PKFindOptions options = 0)
    {
      return (POLandedCostReceipt) PrimaryKeyOf<POLandedCostReceipt>.By<POLandedCostReceipt.lCDocType, POLandedCostReceipt.lCRefNbr, POLandedCostReceipt.pOReceiptType, POLandedCostReceipt.pOReceiptNbr>.FindBy(graph, (object) lCDocType, (object) lCRefNbr, (object) pOReceiptType, (object) pOReceiptNbr, options);
    }
  }

  public static class FK
  {
    public class LandedCostDocument : 
      PrimaryKeyOf<POLandedCostDoc>.By<POLandedCostDoc.docType, POLandedCostDoc.refNbr>.ForeignKeyOf<POLandedCostReceipt>.By<POLandedCostReceipt.lCDocType, POLandedCostReceipt.lCRefNbr>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POLandedCostReceipt>.By<POLandedCostReceipt.pOReceiptType, POLandedCostReceipt.pOReceiptNbr>
    {
    }
  }

  public abstract class lCDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostReceipt.lCDocType>
  {
  }

  public abstract class lCRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostReceipt.lCRefNbr>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostReceipt.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostReceipt.pOReceiptNbr>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostReceipt.lineCntr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLandedCostReceipt.Tstamp>
  {
  }
}
