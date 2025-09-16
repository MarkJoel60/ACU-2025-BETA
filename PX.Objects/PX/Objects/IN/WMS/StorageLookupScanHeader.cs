// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.StorageLookupScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.WMS;

public sealed class StorageLookupScanHeader : PXCacheExtension<
#nullable disable
ScanHeader>
{
  [Site(Enabled = false)]
  public int? SiteID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Storage ID", Enabled = false)]
  [PXSelector(typeof (Search<StoragePlace.storageID, Where<StoragePlace.active, Equal<True>, And<StoragePlace.siteID, Equal<Current<StorageLookupScanHeader.siteID>>>>>), new Type[] {typeof (StoragePlace.siteID), typeof (StoragePlace.storageCD), typeof (StoragePlace.isCart), typeof (StoragePlace.active)}, SubstituteKey = typeof (StoragePlace.storageCD), DescriptionField = typeof (StoragePlace.descr), ValidateValue = false)]
  public int? StorageID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Cart", IsReadOnly = true, FieldClass = "Carts")]
  public bool? IsCart { get; set; }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StorageLookupScanHeader.siteID>
  {
  }

  public abstract class storageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StorageLookupScanHeader.storageID>
  {
  }

  public abstract class isCart : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  StorageLookupScanHeader.isCart>
  {
  }
}
