// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.RecognizedVendorMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
[PXCacheName("Vendor Specified in Recognized Documents")]
[Serializable]
public class RecognizedVendorMapping : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private const int VENDOR_PREFIX_MAX_LENGTH = 191;

  [PXDBGuid(true, IsKey = true)]
  [PXDefault]
  public virtual Guid? Id { get; set; }

  [PXDBString(191, IsUnicode = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string VendorNamePrefix { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXDefault]
  public virtual string VendorName { get; set; }

  [PXParent(typeof (RecognizedVendorMapping.FK.BusinessAccount))]
  [PXDBInt]
  [PXDefault]
  public virtual int? VendorID { get; set; }

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

  internal static string GetVendorPrefixFromName(string vendorName)
  {
    ExceptionExtensions.ThrowOnNull<string>(vendorName, nameof (vendorName), (string) null);
    return vendorName.Length > 191 ? vendorName.Substring(0, 191) : vendorName;
  }

  public static class FK
  {
    public class BusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<RecognizedVendorMapping>.By<RecognizedVendorMapping.vendorID>
    {
    }
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RecognizedVendorMapping.id>
  {
  }

  public abstract class vendorNamePrefix : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedVendorMapping.vendorNamePrefix>
  {
  }

  public abstract class vendorName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedVendorMapping.vendorName>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RecognizedVendorMapping.vendorID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RecognizedVendorMapping.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedVendorMapping.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RecognizedVendorMapping.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RecognizedVendorMapping.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecognizedVendorMapping.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RecognizedVendorMapping.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RecognizedVendorMapping.tStamp>
  {
  }
}
