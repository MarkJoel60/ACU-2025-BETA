// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.DR;

[PXPrimaryGraph(typeof (DRSetupMaint))]
[PXCacheName("Deferred Revenue Preferences")]
[Serializable]
public class DRSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> used for the Deferred Revenue.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("DRSCHEDULE")]
  [PXUIField(DisplayName = "Deferral Schedule Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual 
  #nullable disable
  string ScheduleNumberingID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingRevenueValidate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingExpenseValidate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? UseFairValuePricesInBaseCurrency { get; set; }

  [Account(DisplayName = "Suspense Аccount")]
  public virtual int? SuspenseAccountID { get; set; }

  [SubAccount(DisplayName = "Suspense Sub.")]
  public virtual int? SuspenseSubID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? RecognizeAdjustmentsInPreviousPeriods { get; set; }

  public static class FK
  {
    public class DeferralScheduleNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<DRSetup>.By<DRSetup.scheduleNumberingID>
    {
    }
  }

  public abstract class scheduleNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRSetup.scheduleNumberingID>
  {
  }

  public abstract class pendingRevenueValidate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DRSetup.pendingRevenueValidate>
  {
  }

  public abstract class pendingExpenseValidate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DRSetup.pendingExpenseValidate>
  {
  }

  public abstract class useFairValuePricesInBaseCurrency : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DRSetup.useFairValuePricesInBaseCurrency>
  {
  }

  public abstract class suspenseAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRSetup.suspenseAccountID>
  {
  }

  public abstract class suspenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRSetup.suspenseSubID>
  {
  }

  public abstract class recognizeAdjustmentsInPreviousPeriods : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DRSetup.recognizeAdjustmentsInPreviousPeriods>
  {
  }
}
