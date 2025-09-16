// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Unbound.SOIncreaseAuthorizedAmountDialog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Unbound;

/// <summary>
/// All data needed to perform increase authorized amount operation for document
/// </summary>
[PXCacheName("Increase Authorized Amount")]
[PXVirtual]
public class SOIncreaseAuthorizedAmountDialog : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// Warning message shows in case of multiple application of the payment
  /// </summary>
  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Warning")]
  public virtual 
  #nullable disable
  string WarningMessage { get; set; }

  /// <summary>
  /// Type of document to be increased by increase authorized amount operation
  /// </summary>
  [PXString]
  [ARPaymentType.ListEx]
  [PXUIField(DisplayName = "Type", Enabled = false, TabOrder = 2)]
  public virtual string InstanceType { get; set; }

  /// <summary>
  /// Number of document to be increased by increase authorized amount operation
  /// </summary>
  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Reference Nbr", Enabled = false, TabOrder = 3)]
  public virtual string ReferenceNumber { get; set; }

  /// <summary>
  /// Currency ID of document to be increased (display to user)
  /// </summary>
  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  [PXUIField(DisplayName = "Currency", Enabled = false, TabOrder = 4)]
  public virtual string CuryID { get; set; }

  /// <summary>Currency info ID of document to be increased</summary>
  [PXLong]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// Authorized amount of document (in a currency of document)
  /// </summary>
  [PXCurrency(typeof (SOIncreaseAuthorizedAmountDialog.curyInfoID), typeof (SOIncreaseAuthorizedAmountDialog.authorizedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Authorized", Enabled = false, TabOrder = 5)]
  public virtual Decimal? CuryAuthorizedAmt { get; set; }

  /// <summary>Authorized amount of document (in a base currency)</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AuthorizedAmt { get; set; }

  /// <summary>
  /// Applied amount of document (in a currency of document)
  /// </summary>
  [PXCurrency(typeof (SOIncreaseAuthorizedAmountDialog.curyInfoID), typeof (SOIncreaseAuthorizedAmountDialog.origAdjAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Applied to Document", Enabled = false, TabOrder = 6)]
  public virtual Decimal? CuryOrigAdjAmt { get; set; }

  /// <summary>Applied amount of document (in a base currency)</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigAdjAmt { get; set; }

  /// <summary>
  /// New increased amount of document (in a currency of document)
  /// </summary>
  [PXCurrency(typeof (SOIncreaseAuthorizedAmountDialog.curyInfoID), typeof (SOIncreaseAuthorizedAmountDialog.authorizedAmtNew))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Authorized", TabOrder = 0)]
  public virtual Decimal? CuryAuthorizedAmtNew { get; set; }

  /// <summary>
  /// Maximum possible amount of document (in a currency of document)
  /// </summary>
  [PXDecimal]
  public virtual Decimal? CuryAuthorizedAmtMax { get; set; }

  /// <summary>New increased amount of document (in a base currency)</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AuthorizedAmtNew { get; set; }

  /// <summary>
  /// New increased applied amount (in a currency of document)
  /// </summary>
  [PXCurrency(typeof (SOIncreaseAuthorizedAmountDialog.curyInfoID), typeof (SOIncreaseAuthorizedAmountDialog.origAdjAmtNew))]
  [PXFormula(typeof (Add<SOIncreaseAuthorizedAmountDialog.curyOrigAdjAmt, Sub<SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmtNew, SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmt>>))]
  [PXUIField(DisplayName = "Applied to Document", Enabled = false, TabOrder = 1)]
  public virtual Decimal? CuryOrigAdjAmtNew { get; set; }

  /// <summary>New increased applied amount (in a base currency)</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigAdjAmtNew { get; set; }

  public abstract class warningMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.warningMessage>
  {
  }

  public abstract class instanceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.instanceType>
  {
  }

  public abstract class referenceNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.referenceNumber>
  {
  }

  public abstract class curyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.curyID>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.curyInfoID>
  {
  }

  public abstract class curyAuthorizedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmt>
  {
  }

  public abstract class authorizedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.authorizedAmt>
  {
  }

  public abstract class curyOrigAdjAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.curyOrigAdjAmt>
  {
  }

  public abstract class origAdjAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.origAdjAmt>
  {
  }

  public abstract class curyAuthorizedAmtNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmtNew>
  {
  }

  public abstract class curyAuthorizedAmtMax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.curyAuthorizedAmtMax>
  {
  }

  public abstract class authorizedAmtNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.authorizedAmtNew>
  {
  }

  public abstract class curyOrigAdjAmtNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.curyOrigAdjAmtNew>
  {
  }

  public abstract class origAdjAmtNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOIncreaseAuthorizedAmountDialog.origAdjAmtNew>
  {
  }
}
