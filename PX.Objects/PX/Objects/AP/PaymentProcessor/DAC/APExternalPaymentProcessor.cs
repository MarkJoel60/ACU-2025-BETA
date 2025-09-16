// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.DAC.APExternalPaymentProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.DAC;

/// <summary>AP external payment processor</summary>
[PXCacheName("External Payment Processor")]
[PXPrimaryGraph(typeof (APExternalPaymentProcessorMaint))]
public class APExternalPaymentProcessor : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>AP external payment processor id</summary>
  [PXDefault]
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Payment Processor ID")]
  [PXSelector(typeof (Search<APExternalPaymentProcessor.externalPaymentProcessorID>))]
  public virtual string? ExternalPaymentProcessorID { get; set; }

  /// <summary>Name</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Name")]
  public virtual string? Name { get; set; }

  /// <summary>Type</summary>
  [PXDBString]
  [PXDefault("BC")]
  [APExternalPaymentPocessorType.List]
  [PXUIField(DisplayName = "Plug-In", Required = true)]
  public virtual string Type { get; set; }

  /// <summary>Is active</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <summary>Disclaimer Message</summary>
  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Disclaimer Message", Enabled = false, Visible = false)]
  public virtual string DisclaimerMessage
  {
    get
    {
      return PXMessages.LocalizeNoPrefix("Money transmission services provided by Bill.com, LLC (NMLS #: 1007645)");
    }
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string? CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created Date Time")]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string? LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  [PXUIField(DisplayName = "Noteid")]
  public virtual Guid? Noteid { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[]? Tstamp { get; set; }

  /// <summary>User session store</summary>
  public PX.PaymentProcessor.Data.UserSessionStore? UserSessionStore { get; set; }

  public class PK : 
    PrimaryKeyOf<APExternalPaymentProcessor>.By<APExternalPaymentProcessor.externalPaymentProcessorID>
  {
    public static APExternalPaymentProcessor Find(
      PXGraph graph,
      string externalPaymentProcessorID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APExternalPaymentProcessor>.By<APExternalPaymentProcessor.externalPaymentProcessorID>.FindBy(graph, (object) externalPaymentProcessorID, options);
    }
  }

  public abstract class externalPaymentProcessorID : 
    BqlType<IBqlString, string>.Field<APExternalPaymentProcessor.externalPaymentProcessorID>
  {
  }

  public abstract class name : BqlType<IBqlString, string>.Field<APExternalPaymentProcessor.name>
  {
  }

  public abstract class type : BqlType<IBqlString, string>.Field<APExternalPaymentProcessor.type>
  {
  }

  public abstract class isActive : BqlType<IBqlBool, bool>.Field<APExternalPaymentProcessor.isActive>
  {
  }

  public abstract class disclaimerMessage : 
    BqlType<IBqlString, string>.Field<APExternalPaymentProcessor.disclaimerMessage>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<APExternalPaymentProcessor.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<APExternalPaymentProcessor.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APExternalPaymentProcessor.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<APExternalPaymentProcessor.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<APExternalPaymentProcessor.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APExternalPaymentProcessor.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<IBqlGuid, Guid>.Field<APExternalPaymentProcessor.noteid>
  {
  }

  public abstract class tstamp : 
    BqlType<IBqlByteArray, byte[]>.Field<APExternalPaymentProcessor.tstamp>
  {
  }
}
