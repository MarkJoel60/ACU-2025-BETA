// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.AddPaymentsFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXHidden]
[PXCacheName("AddPaymentsFilter")]
public class AddPaymentsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Next Payment Ref. Number", Visible = true)]
  public virtual 
  #nullable disable
  string NextPaymentRefNumber { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField]
  public virtual int? SelectedCount { get; set; }

  public abstract class nextPaymentRefNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddPaymentsFilter.nextPaymentRefNumber>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AddPaymentsFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AddPaymentsFilter.endDate>
  {
  }

  public abstract class selectedCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddPaymentsFilter.selectedCount>
  {
  }
}
