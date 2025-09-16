// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateInvoice.CreateInvoicesFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCreateInvoice;

[PXHidden]
[Serializable]
public class CreateInvoicesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXUnboundDefault]
  [PXUIField(DisplayName = "Reference Nbr.", TabOrder = 1)]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Set Quote as Primary", Visible = false)]
  public virtual bool? MakeQuotePrimary { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Set Current Unit Prices")]
  public virtual bool? RecalculatePrices { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Manual Prices")]
  public virtual bool? OverrideManualPrices { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recalculate Discounts")]
  public virtual bool? RecalculateDiscounts { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Manual Line Discounts")]
  public virtual bool? OverrideManualDiscounts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Manual Group and Document Discounts")]
  public virtual bool? OverrideManualDocGroupDiscounts { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create an Invoice for the Specified Manual Amount")]
  public virtual bool? ConfirmManualAmount { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CreateInvoicesFilter.refNbr>
  {
  }

  public abstract class makeQuotePrimary : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoicesFilter.makeQuotePrimary>
  {
  }

  public abstract class recalculatePrices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoicesFilter.recalculatePrices>
  {
  }

  public abstract class overrideManualPrices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoicesFilter.overrideManualPrices>
  {
  }

  public abstract class recalculateDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoicesFilter.recalculateDiscounts>
  {
  }

  public abstract class overrideManualDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoicesFilter.overrideManualDiscounts>
  {
  }

  public abstract class overrideManualDocGroupDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoicesFilter.overrideManualDocGroupDiscounts>
  {
  }

  public abstract class confirmManualAmount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateInvoicesFilter.confirmManualAmount>
  {
  }
}
