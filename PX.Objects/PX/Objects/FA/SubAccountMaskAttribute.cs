// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.SubAccountMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

/// <summary>
/// A subaccount mask that is used to generate the subaccounts of the fixed asset.
/// </summary>
/// <value>The string mask for the SUBACCOUNT segmented key with the appropriate structure.
///  The mask can contain the following mask symbols:
///  <list type="bullet">
///  <item> <term><c>A</c></term> <description>The subaccount source is a fixed asset, <see cref="P:PX.Objects.FA.FixedAsset.DisposalSubID" /></description> </item>
///  <item> <term><c>C</c></term> <description>The subaccount source is a fixed asset class, <see cref="!:FAClass.DisposalSubID" /></description> </item>
///  <item> <term><c>L</c></term> <description>The subaccount source is a location, <see cref="!:Location.CMPExpenseSubID" /></description> </item>
///  <item> <term><c>D</c></term> <description>The subaccount source is a department, <see cref="P:PX.Objects.EP.EPDepartment.ExpenseSubID" /></description> </item>
/// </list>
///  By default, the mask contains only "C" symbols (which means that the only subaccount source is a fixed asset class).
///  </value>
[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField]
public sealed class SubAccountMaskAttribute : PXEntityAttribute
{
  private const string _DimensionName = "SUBACCOUNT";
  private const string _MaskName = "FASETUP";

  public SubAccountMaskAttribute()
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = new PXDimensionMaskAttribute("SUBACCOUNT", "FASETUP", "C", new FAAcctSubDefault.ClassListAttribute().AllowedValues, new FAAcctSubDefault.ClassListAttribute().AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public static string MakeSub<Field>(PXGraph graph, string mask, object[] sources, Type[] fields) where Field : IBqlField
  {
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, new FAAcctSubDefault.ClassListAttribute().AllowedValues, 3, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      return (string) null;
    }
  }
}
