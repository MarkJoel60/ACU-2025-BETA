// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.LocationActiveAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.CS;

[PXDBInt]
[PXInt]
[PXUIField]
[PXRestrictor(typeof (Where<BqlOperand<Location.isActive, IBqlBool>.IsEqual<True>>), "Location '{0}' is inactive", new System.Type[] {typeof (Location.locationCD)})]
public class LocationActiveAttribute : LocationIDBaseAttribute
{
  public LocationActiveAttribute()
    : base(typeof (Where<boolTrue, Equal<boolTrue>>))
  {
  }

  public LocationActiveAttribute(System.Type WhereType)
    : base(WhereType)
  {
  }

  public LocationActiveAttribute(System.Type WhereType, System.Type JoinType)
    : base(WhereType, JoinType)
  {
  }
}
