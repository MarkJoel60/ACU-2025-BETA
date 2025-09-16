// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APIntegrityCheckFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class APIntegrityCheckFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _VendorClassID;
  protected string _FinPeriodID;

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (VendorClass.vendorClassID), DescriptionField = typeof (VendorClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Vendor Class")]
  public virtual string VendorClassID
  {
    get => this._VendorClassID;
    set => this._VendorClassID = value;
  }

  [FinPeriodNonLockedSelector]
  [PXDefault]
  [PXUIField(DisplayName = "Fin. Period")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public abstract class vendorClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APIntegrityCheckFilter.vendorClassID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APIntegrityCheckFilter.finPeriodID>
  {
  }
}
