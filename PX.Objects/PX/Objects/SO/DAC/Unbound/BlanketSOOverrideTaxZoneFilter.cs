// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Unbound.BlanketSOOverrideTaxZoneFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO.DAC.Unbound;

[PXCacheName("Override Customer Tax Zone Filter in Blanket Orders")]
public class BlanketSOOverrideTaxZoneFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaxZoneID;

  [PXString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  public abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOOverrideTaxZoneFilter.taxZoneID>
  {
  }
}
