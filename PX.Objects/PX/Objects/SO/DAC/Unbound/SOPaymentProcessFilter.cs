// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Unbound.SOPaymentProcessFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Unbound;

[PXCacheName("Credit Card Processing for Sales Filter")]
public class SOPaymentProcessFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [SOPaymentProcessFilter.action.List]
  [PXUIField(DisplayName = "Action")]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "End Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? EndDate { get; set; }

  [Customer]
  public virtual int? CustomerID { get; set; }

  /// <summary>Increase before capture</summary>
  [PXUIField(DisplayName = "Increase Authorized Amount Before Capture")]
  [PXBool]
  [PXDefault(false)]
  public virtual bool? IncreaseBeforeCapture { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPaymentProcessFilter.action>
  {
    public const string CaptureCCPayment = "CaptureCCPayment";
    public const string ValidateCCPayment = "ValidateCCPayment";
    public const string VoidExpiredCCPayment = "VoidExpiredCCPayment";
    public const string ReAuthorizeCCPayment = "ReAuthorizeCCPayment";
    public const string IncreaseCCPayment = "IncreaseCCPayment";
    public const string IncreaseAndCaptureCCPayment = "IncreaseAndCaptureCCPayment";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[5]
        {
          PXStringListAttribute.Pair("CaptureCCPayment", "Capture"),
          PXStringListAttribute.Pair("ValidateCCPayment", "Validate Card Payment"),
          PXStringListAttribute.Pair("VoidExpiredCCPayment", "Void Expired Card Payment"),
          PXStringListAttribute.Pair("ReAuthorizeCCPayment", "Reauthorize"),
          PXStringListAttribute.Pair("IncreaseCCPayment", "Increase Authorized Amount")
        })
      {
      }
    }

    public class captureCCPayment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPaymentProcessFilter.action.captureCCPayment>
    {
      public captureCCPayment()
        : base("CaptureCCPayment")
      {
      }
    }

    public class validateCCPayment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPaymentProcessFilter.action.validateCCPayment>
    {
      public validateCCPayment()
        : base("ValidateCCPayment")
      {
      }
    }

    public class voidCCPayment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPaymentProcessFilter.action.voidCCPayment>
    {
      public voidCCPayment()
        : base("VoidExpiredCCPayment")
      {
      }
    }

    public class reAuthorizeCCPayment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPaymentProcessFilter.action.reAuthorizeCCPayment>
    {
      public reAuthorizeCCPayment()
        : base("ReAuthorizeCCPayment")
      {
      }
    }

    public class increaseCCPayment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPaymentProcessFilter.action.increaseCCPayment>
    {
      public increaseCCPayment()
        : base("IncreaseCCPayment")
      {
      }
    }
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPaymentProcessFilter.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPaymentProcessFilter.endDate>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPaymentProcessFilter.customerID>
  {
  }

  public abstract class increaseBeforeCapture : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPaymentProcessFilter.increaseBeforeCapture>
  {
  }
}
