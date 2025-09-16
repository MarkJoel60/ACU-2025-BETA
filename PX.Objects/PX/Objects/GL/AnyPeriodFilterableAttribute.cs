// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AnyPeriodFilterableAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// FinPeriod selector that extends <see cref="T:PX.Objects.GL.FinPeriodSelectorAttribute" />.
/// Displays any periods (active, closed, etc), maybe filtered.
/// When Date is supplied through aSourceType parameter FinPeriod is defaulted with the FinPeriod for the given date.
/// Default columns list includes 'Active' and  'Closed in GL' columns
/// </summary>
public class AnyPeriodFilterableAttribute : FinPeriodSelectorAttribute
{
  protected int _SelAttrIndex = -1;

  public AnyPeriodFilterableAttribute(Type aSourceType)
    : this((Type) null, aSourceType)
  {
  }

  public AnyPeriodFilterableAttribute(Type sourceType, Type[] fieldList)
    : this((Type) null, sourceType, fieldList)
  {
  }

  public AnyPeriodFilterableAttribute(Type searchType, Type sourceType, Type[] fieldList)
    : this(searchType, sourceType, (Type) null, (Type) null, (Type) null, (Type) null, (Type) null, true, fieldList, (Type) null)
  {
  }

  public AnyPeriodFilterableAttribute(
    Type searchType,
    Type sourceType,
    Type branchSourceType = null,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type useMasterCalendarSourceType = null,
    Type defaultType = null,
    bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
    Type[] fieldList = null,
    Type masterFinPeriodIDType = null)
    : base(searchType, sourceType, branchSourceType, branchSourceFormulaType, organizationSourceType, useMasterCalendarSourceType, defaultType, redefaultOrRevalidateOnOrganizationSourceUpdated, fieldList: fieldList, masterFinPeriodIDType: masterFinPeriodIDType)
  {
    this.Initialize();
    this.Filterable = true;
  }

  public AnyPeriodFilterableAttribute()
    : this((Type) null)
  {
  }

  public virtual bool Filterable
  {
    get
    {
      return this._SelAttrIndex != -1 && ((PXSelectorAttribute) this._Attributes[this._SelAttrIndex]).Filterable;
    }
    set
    {
      if (this._SelAttrIndex == -1)
        return;
      ((PXSelectorAttribute) this._Attributes[this._SelAttrIndex]).Filterable = value;
    }
  }

  protected virtual void Initialize()
  {
    this._SelAttrIndex = -1;
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
    {
      if (attribute is PXSelectorAttribute && this._SelAttrIndex < 0)
        this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).IndexOf(attribute);
    }
  }
}
