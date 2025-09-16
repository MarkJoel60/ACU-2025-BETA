// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.AssignmentMapType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.AP.Standalone;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CR;
using PX.Objects.PM;
using PX.Objects.RQ;
using PX.Objects.WZ;

#nullable enable
namespace PX.Objects.EP;

/// <exclude />
public static class AssignmentMapType
{
  public class AssignmentMapTypeLead : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeLead>
  {
    public AssignmentMapTypeLead()
      : base(typeof (CRLead).FullName)
    {
    }
  }

  public class AssignmentMapTypeContact : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeContact>
  {
    public AssignmentMapTypeContact()
      : base(typeof (PX.Objects.CR.Contact).FullName)
    {
    }
  }

  public class AssignmentMapTypeCase : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeCase>
  {
    public AssignmentMapTypeCase()
      : base(typeof (CRCase).FullName)
    {
    }
  }

  public class AssignmentMapTypeOpportunity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeOpportunity>
  {
    public AssignmentMapTypeOpportunity()
      : base(typeof (CROpportunity).FullName)
    {
    }
  }

  public class AssignmentMapTypeExpenceClaim : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeExpenceClaim>
  {
    public AssignmentMapTypeExpenceClaim()
      : base(typeof (EPExpenseClaim).FullName)
    {
    }
  }

  public class AssignmentMapTypeTimeCard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeTimeCard>
  {
    public AssignmentMapTypeTimeCard()
      : base(typeof (EPTimeCard).FullName)
    {
    }
  }

  public class AssignmentMapTypeEquipmentTimeCard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeEquipmentTimeCard>
  {
    public AssignmentMapTypeEquipmentTimeCard()
      : base(typeof (EPEquipmentTimeCard).FullName)
    {
    }
  }

  public class AssignmentMapTypeSalesOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeSalesOrder>
  {
    public AssignmentMapTypeSalesOrder()
      : base(typeof (PX.Objects.SO.SOOrder).FullName)
    {
    }
  }

  public class AssignmentMapTypeSalesOrderShipment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeSalesOrderShipment>
  {
    public AssignmentMapTypeSalesOrderShipment()
      : base(typeof (PX.Objects.SO.SOShipment).FullName)
    {
    }
  }

  public class AssignmentMapTypePurchaseOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypePurchaseOrder>
  {
    public AssignmentMapTypePurchaseOrder()
      : base(typeof (PX.Objects.PO.POOrder).FullName)
    {
    }
  }

  public class AssignmentMapTypePurchaseOrderReceipt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypePurchaseOrderReceipt>
  {
    public AssignmentMapTypePurchaseOrderReceipt()
      : base(typeof (PX.Objects.PO.POReceipt).FullName)
    {
    }
  }

  public class AssignmentMapTypePurchaseRequestItem : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypePurchaseRequestItem>
  {
    public AssignmentMapTypePurchaseRequestItem()
      : base(typeof (RQRequest).FullName)
    {
    }
  }

  public class AssignmentMapTypePurchaseRequisition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypePurchaseRequisition>
  {
    public AssignmentMapTypePurchaseRequisition()
      : base(typeof (RQRequisition).FullName)
    {
    }
  }

  public class AssignmentMapTypeCashTransaction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeCashTransaction>
  {
    public AssignmentMapTypeCashTransaction()
      : base(typeof (CAAdj).FullName)
    {
    }
  }

  public class AssignmentMapTypeProspect : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeProspect>
  {
    public AssignmentMapTypeProspect()
      : base(typeof (PX.Objects.CR.BAccount).FullName)
    {
    }
  }

  public class AssignmentMapTypeProject : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeProject>
  {
    public AssignmentMapTypeProject()
      : base(typeof (PMProject).FullName)
    {
    }
  }

  public class AssignmentMapTypeActivity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeActivity>
  {
    public AssignmentMapTypeActivity()
      : base(typeof (CRActivity).FullName)
    {
    }
  }

  public class AssignmentMapTypeImplementationScenario : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeImplementationScenario>
  {
    public AssignmentMapTypeImplementationScenario()
      : base(typeof (WZScenario).FullName)
    {
    }
  }

  public class AssignmentMapTypeAPInvoice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeAPInvoice>
  {
    public AssignmentMapTypeAPInvoice()
      : base(typeof (PX.Objects.AP.APInvoice).FullName)
    {
    }
  }

  public class AssignmentMapTypeAPPayment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeAPPayment>
  {
    public AssignmentMapTypeAPPayment()
      : base(typeof (PX.Objects.AP.APPayment).FullName)
    {
    }
  }

  public class AssignmentMapTypeAPQuickCheck : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeAPQuickCheck>
  {
    public AssignmentMapTypeAPQuickCheck()
      : base(typeof (APQuickCheck).FullName)
    {
    }
  }

  public class AssignmentMapTypeExpenceClaimDetails : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeExpenceClaimDetails>
  {
    public AssignmentMapTypeExpenceClaimDetails()
      : base(typeof (EPExpenseClaimDetails).FullName)
    {
    }
  }

  public class AssignmentMapTypeARInvoice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeARInvoice>
  {
    public AssignmentMapTypeARInvoice()
      : base(typeof (PX.Objects.AR.ARInvoice).FullName)
    {
    }
  }

  public class AssignmentMapTypeARPayment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeARPayment>
  {
    public AssignmentMapTypeARPayment()
      : base(typeof (PX.Objects.AR.ARPayment).FullName)
    {
    }
  }

  public class AssignmentMapTypeARCashSale : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeARCashSale>
  {
    public AssignmentMapTypeARCashSale()
      : base(typeof (ARCashSale).FullName)
    {
    }
  }

  public class AssignmentMapTypeProforma : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeProforma>
  {
    public AssignmentMapTypeProforma()
      : base(typeof (PMProforma).FullName)
    {
    }
  }

  public class AssignmentMapTypeEmail : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeEmail>
  {
    public AssignmentMapTypeEmail()
      : base(typeof (CRSMEmail).FullName)
    {
    }
  }

  public class AssignmentMapTypeChangeOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeChangeOrder>
  {
    public AssignmentMapTypeChangeOrder()
      : base(typeof (PMChangeOrder).FullName)
    {
    }
  }

  public class AssignmentMapTypeChangeRequest : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeChangeRequest>
  {
    public AssignmentMapTypeChangeRequest()
      : base(typeof (PMChangeRequest).FullName)
    {
    }
  }

  public class AssignmentMapTypeQuotes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeQuotes>
  {
    public AssignmentMapTypeQuotes()
      : base(typeof (CRQuote).FullName)
    {
    }
  }

  public class AssignmentMapTypeProjectQuotes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeProjectQuotes>
  {
    public AssignmentMapTypeProjectQuotes()
      : base(typeof (PMQuote).FullName)
    {
    }
  }

  public class AssignmentMapTypeGLBatch : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeGLBatch>
  {
    public AssignmentMapTypeGLBatch()
      : base(typeof (PX.Objects.GL.Batch).FullName)
    {
    }
  }

  public class AssignmentMapTypeProgressWorksheet : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeProgressWorksheet>
  {
    public AssignmentMapTypeProgressWorksheet()
      : base(typeof (PMProgressWorksheet).FullName)
    {
    }
  }

  public class AssignmentMapTypeReconciliationStatements : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeReconciliationStatements>
  {
    public AssignmentMapTypeReconciliationStatements()
      : base(typeof (CARecon).FullName)
    {
    }
  }

  public class AssignmentMapTypeCostProjection : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeCostProjection>
  {
    public AssignmentMapTypeCostProjection()
      : base(typeof (PMCostProjection).FullName)
    {
    }
  }

  public class AssignmentMapTypeCostProjectionByDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeCostProjectionByDate>
  {
    public AssignmentMapTypeCostProjectionByDate()
      : base(typeof (PMCostProjectionByDate).FullName)
    {
    }
  }

  public class AssignmentMapTypeWipAdjustment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AssignmentMapType.AssignmentMapTypeWipAdjustment>
  {
    public AssignmentMapTypeWipAdjustment()
      : base(typeof (PMWipAdjustment).FullName)
    {
    }
  }
}
