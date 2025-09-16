// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.DiscountedLineMapperBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Discount.Mappers;

public abstract class DiscountedLineMapperBase
{
  public PXCache Cache { get; }

  public object MappedLine { get; }

  protected DiscountedLineMapperBase(PXCache cache, object row)
  {
    this.Cache = cache;
    this.MappedLine = row;
  }

  public abstract Type GetField<T>() where T : IBqlField;

  public virtual void RaiseFieldUpdating<T>(ref object newValue) where T : IBqlField
  {
    this.Cache.RaiseFieldUpdating(this.GetField<T>().Name, this.MappedLine, ref newValue);
  }

  public virtual void RaiseFieldUpdated<T>(object oldValue) where T : IBqlField
  {
    this.Cache.RaiseFieldUpdated(this.GetField<T>().Name, this.MappedLine, oldValue);
  }

  public virtual void RaiseFieldVerifying<T>(ref object newValue) where T : IBqlField
  {
    this.Cache.RaiseFieldVerifying(this.GetField<T>().Name, this.MappedLine, ref newValue);
  }
}
