// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxEPExpenseClaimDetails
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.FS;

public class FSxEPExpenseClaimDetails : PXCacheExtension<
#nullable disable
EPExpenseClaimDetails>
{
  public string _FSEntityType;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXString]
  [PXUIField(DisplayName = "Related Svc. Doc. Type")]
  [PXDefault]
  [ListField_FSEntity_Type.List]
  public virtual string FSEntityTypeUI { get; set; }

  [PXString]
  [PXDefault("PX.Objects.FS.FSServiceOrder")]
  [PXFormula(typeof (Switch<Case<Where<FSxEPExpenseClaimDetails.fsEntityTypeUI, IsNotNull>, FSxEPExpenseClaimDetails.fsEntityTypeUI>, ListField_FSEntity_Type.serviceOrder>))]
  public virtual string FSEntityType { get; set; }

  [PXUIField(DisplayName = "Related Svc. Doc. Nbr.")]
  [FSEntityIDExpenseSelector(typeof (FSxEPExpenseClaimDetails.fsEntityTypeUI), typeof (EPExpenseClaimDetails.customerID), typeof (EPExpenseClaimDetails.contractID), typeof (EPExpenseClaimDetails.customerLocationID))]
  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? FSEntityNoteID { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsDocBilledOrClosed { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsDocRelatedToProject { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (Default<FSxEPExpenseClaimDetails.fsEntityNoteID, FSxEPExpenseClaimDetails.fsEntityTypeUI>))]
  [PXFormula(typeof (Switch<Case<Where<EPExpenseClaimDetails.paidWith, Equal<EPExpenseClaimDetails.paidWith.cardCompanyExpense>>, boolFalse>, FSxEPExpenseClaimDetails.fsBillable>))]
  [PXUIEnabled(typeof (Where<EPExpenseClaimDetails.paidWith, NotEqual<EPExpenseClaimDetails.paidWith.cardCompanyExpense>, Or<EPExpenseClaimDetails.curyExtCost, GreaterEqual<decimal0>>>))]
  [PXUIField(DisplayName = "Billable in Svc. Doc.")]
  public virtual bool? FSBillable { get; set; }

  public abstract class fsEntityTypeUI : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxEPExpenseClaimDetails.fsEntityTypeUI>
  {
    public abstract class Values : ListField_FSEntity_Type
    {
    }
  }

  public abstract class fsEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxEPExpenseClaimDetails.fsEntityType>
  {
  }

  public abstract class fsEntityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSxEPExpenseClaimDetails.fsEntityNoteID>
  {
  }

  public abstract class isDocBilledOrClosed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEPExpenseClaimDetails.isDocBilledOrClosed>
  {
  }

  public abstract class isDocRelatedToProject : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEPExpenseClaimDetails.isDocRelatedToProject>
  {
  }

  public abstract class fsBillable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEPExpenseClaimDetails.fsBillable>
  {
  }
}
