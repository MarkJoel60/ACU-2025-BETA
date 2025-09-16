// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PXDBVendorCuryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Microsoft.CSharp.RuntimeBinder;
using PX.Data;
using PX.Objects.CM;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Objects.AP;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBVendorCuryAttribute : PXDBDecimalAttribute
{
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2021 R2.")]
  public PXDBVendorCuryAttribute(System.Type vendorID)
    : base(BqlCommand.Compose(typeof (Search2<,,>), typeof (Vendor.taxReportPrecision), typeof (LeftJoin<PX.Objects.CM.Currency, On<PX.Objects.CM.Currency.curyID, Equal<Vendor.curyID>>, LeftJoin<Currency2, On<Currency2.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>), typeof (Where<,>), typeof (Vendor.bAccountID), typeof (Equal<>), typeof (Current<>), vendorID))
  {
  }

  public PXDBVendorCuryAttribute(System.Type vendorID, System.Type branchID)
    : base(BqlCommand.Compose(typeof (Search2<,,>), typeof (Vendor.taxReportPrecision), typeof (LeftJoin<,,>), typeof (PX.Objects.GL.Branch), typeof (On<,>), typeof (PX.Objects.GL.Branch.branchID), typeof (Equal<>), typeof (Current<>), branchID, typeof (LeftJoin<PX.Objects.CM.Currency, On<PX.Objects.CM.Currency.curyID, Equal<Vendor.curyID>>, LeftJoin<Currency2, On<Currency2.curyID, Equal<PX.Objects.GL.Branch.baseCuryID>>>>), typeof (Where<,>), typeof (Vendor.bAccountID), typeof (Equal<>), typeof (Current<>), vendorID))
  {
  }

  protected override int? GetItemPrecision(PXView view, object item)
  {
    object obj1 = (object) (item as PXResult<Vendor, PX.Objects.CM.Currency, Currency2>);
    // ISSUE: reference to a compiler-generated field
    if (PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (PXDBVendorCuryAttribute), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (PXDBVendorCuryAttribute), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__0, obj1, (object) null);
    if (target1((CallSite) p1, obj2))
      obj1 = (object) (item as PXResult<Vendor, PX.Objects.GL.Branch, PX.Objects.CM.Currency, Currency2>);
    // ISSUE: reference to a compiler-generated field
    if (PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (PXDBVendorCuryAttribute), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target2 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p3 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__3;
    // ISSUE: reference to a compiler-generated field
    if (PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (PXDBVendorCuryAttribute), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__2, obj1, (object) null);
    if (target2((CallSite) p3, obj3))
      return new int?();
    PXCache cach1 = view.Graph.Caches[typeof (Vendor)];
    PXCache pxCache1 = cach1;
    // ISSUE: reference to a compiler-generated field
    if (PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, Vendor>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (Vendor), typeof (PXDBVendorCuryAttribute)));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    Vendor data1 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__4.Target((CallSite) PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__4, obj1);
    if (((bool?) pxCache1.GetValue<Vendor.taxUseVendorCurPrecision>((object) data1)).GetValueOrDefault())
    {
      PXCache cach2 = view.Graph.Caches[typeof (PX.Objects.CM.Currency)];
      // ISSUE: reference to a compiler-generated field
      if (PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, PX.Objects.CM.Currency>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (PX.Objects.CM.Currency), typeof (PXDBVendorCuryAttribute)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      PX.Objects.CM.Currency data2 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__5.Target((CallSite) PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__5, obj1);
      short? nullable1 = (short?) cach2.GetValue<PX.Objects.CM.Currency.decimalPlaces>((object) data2);
      short? nullable2;
      if (!nullable1.HasValue)
      {
        PXCache cach3 = view.Graph.Caches[typeof (Currency2)];
        // ISSUE: reference to a compiler-generated field
        if (PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, Currency2>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (Currency2), typeof (PXDBVendorCuryAttribute)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        Currency2 data3 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__6.Target((CallSite) PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__6, obj1);
        nullable2 = (short?) cach3.GetValue<Currency2.decimalPlaces>((object) data3);
      }
      else
        nullable2 = nullable1;
      short? nullable3 = nullable2;
      return !nullable3.HasValue ? new int?() : new int?((int) nullable3.GetValueOrDefault());
    }
    PXCache pxCache2 = cach1;
    // ISSUE: reference to a compiler-generated field
    if (PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, Vendor>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (Vendor), typeof (PXDBVendorCuryAttribute)));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    Vendor data4 = PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__7.Target((CallSite) PXDBVendorCuryAttribute.\u003C\u003Eo__2.\u003C\u003Ep__7, obj1);
    short? nullable = (short?) pxCache2.GetValue<Vendor.taxReportPrecision>((object) data4);
    return !nullable.HasValue ? new int?() : new int?((int) nullable.GetValueOrDefault());
  }

  public override void CacheAttached(PXCache sender)
  {
    sender.SetAltered(this._FieldName, true);
    base.CacheAttached(sender);
  }
}
