// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PlugInFilter
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
public class PlugInFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (PaymentMethod.paymentMethodID))]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  [PXDBString(255 /*0xFF*/, IsKey = true)]
  [PXDefault(typeof (PaymentMethod.aPBatchExportPlugInTypeName))]
  public virtual string PlugInTypeName { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show All Settings")]
  public virtual bool? ShowAllSettings { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Offset Settings", Visible = false)]
  public virtual bool? ShowOffsetSettings { get; set; }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PlugInFilter.paymentMethodID>
  {
  }

  public abstract class plugInTypeName : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PlugInFilter.plugInTypeName>
  {
  }

  public abstract class showAllSettings : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PlugInFilter.showAllSettings>
  {
  }

  public abstract class showOffsetSettings : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PlugInFilter.showOffsetSettings>
  {
  }
}
