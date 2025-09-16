// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.LocationIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

[PXDBInt]
[PXInt]
[PXUIField]
[Obsolete("Only active Locations should be used. Use LocationActiveAttribute instead")]
public class LocationIDAttribute : LocationIDBaseAttribute
{
  public LocationIDAttribute()
    : base(typeof (Where<boolTrue, Equal<boolTrue>>))
  {
  }

  public LocationIDAttribute(Type WhereType)
    : base(WhereType)
  {
  }

  public LocationIDAttribute(Type WhereType, Type JoinType)
    : base(WhereType, JoinType)
  {
  }
}
