// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PXCurrencyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXCurrencyAttribute : PXDecimalAttribute, ICurrencyAttribute
{
  protected internal Type ResultField;
  protected internal Type KeyField;
  protected Dictionary<long, string> _Matches;

  public virtual bool BaseCalc { get; set; } = true;

  public virtual bool ShouldShowBaseIfCuryViewState { get; set; } = true;

  int? ICurrencyAttribute.CustomPrecision => new int?();

  Type ICurrencyAttribute.ResultField => this.ResultField;

  Type ICurrencyAttribute.KeyField => this.KeyField;

  /// <summary>Constructor</summary>
  /// <param name="keyField">Field in this table used as a key for CurrencyInfo table.</param>
  /// <param name="resultField">Field in this table to store the result of currency conversion.</param>
  public PXCurrencyAttribute(Type keyField, Type resultField)
    : this(keyField)
  {
    this.ResultField = resultField;
  }

  public PXCurrencyAttribute(Type keyField) => this.KeyField = keyField;

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    this._Precision = sender.Graph.GetPrecision(sender, row, this.KeyField?.Name, this._Matches);
  }

  protected virtual void CuryFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    CuryField curyField)
  {
    if (!sender.Graph.Accessinfo.CuryViewState || !this.ShouldShowBaseIfCuryViewState || string.IsNullOrEmpty(curyField.BaseName))
      return;
    curyField.SetBaseCuryValueAsReturnState(sender, e);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PXCurrencyAttribute.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new PXCurrencyAttribute.\u003C\u003Ec__DisplayClass21_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    base.CacheAttached(cDisplayClass210.sender);
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass210.sender.Graph.IsInitializing)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      cDisplayClass210.sender.Graph.Initialized += new PXGraphInitializedDelegate((object) cDisplayClass210, __methodptr(\u003CCacheAttached\u003Eg__subscribeToEvents\u007C0));
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      cDisplayClass210.\u003CCacheAttached\u003Eg__subscribeToEvents\u007C0(cDisplayClass210.sender.Graph);
    }
    if (!(this.KeyField != (Type) null))
      return;
    // ISSUE: reference to a compiler-generated field
    this._Matches = CurrencyInfo.CuryIDStringAttribute.GetMatchesDictionary(cDisplayClass210.sender);
  }
}
