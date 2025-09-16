// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.ExcludedVendorDomain
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
[PXCacheName("Excluded Email Domains")]
[Serializable]
public class ExcludedVendorDomain : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Domain Name")]
  [PXDBString(255 /*0xFF*/, IsKey = true, IsUnicode = true, InputMask = "")]
  public 
  #nullable disable
  string Name { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExcludedVendorDomain.name>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ExcludedVendorDomain.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExcludedVendorDomain.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ExcludedVendorDomain.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ExcludedVendorDomain.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExcludedVendorDomain.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ExcludedVendorDomain.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ExcludedVendorDomain.tStamp>
  {
  }
}
