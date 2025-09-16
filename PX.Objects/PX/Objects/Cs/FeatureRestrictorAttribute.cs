// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FeatureRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class FeatureRestrictorAttribute : PXAggregateAttribute, IPXRowSelectedSubscriber
{
  private readonly string typeName;
  protected BqlCommand _Select;

  public FeatureRestrictorAttribute(Type checkUsage)
  {
    BqlCommand bqlCommand;
    if (!(checkUsage != (Type) null))
      bqlCommand = (BqlCommand) null;
    else
      bqlCommand = BqlCommand.CreateInstance(new Type[1]
      {
        checkUsage
      });
    this._Select = bqlCommand;
    this.typeName = checkUsage == (Type) null ? (string) null : checkUsage.FullName;
  }

  void IPXRowSelectedSubscriber.RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this._Select == null || sender.Graph.GetType() == typeof (PXGraph) || sender.Graph.GetType() == typeof (PXGenericInqGrph) || sender.Graph.IsContractBasedAPI || !(sender.GetStateExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) is PXFieldState stateExt) || stateExt.ErrorLevel == 4)
      return;
    if (!((bool?) stateExt.Value).GetValueOrDefault())
    {
      if (!((IEnumerable<Type>) this._Select.GetReferencedFields(false)).Where<Type>((Func<Type, bool>) (_ => _.IsNested)).Select<Type, Type>((Func<Type, Type>) (_ => _.DeclaringType)).Union<Type>((IEnumerable<Type>) this._Select.GetTables()).Where<Type>((Func<Type, bool>) (_ => typeof (PXCacheExtension).IsAssignableFrom(_))).Distinct<Type>().All<Type>((Func<Type, bool>) (_ => PXCache.IsActiveExtension(_))) || sender.Graph.TypedViews.GetView(this._Select, true).SelectSingle(Array.Empty<object>()) == null)
        return;
      sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) false, (Exception) new PXSetPropertyException("This feature is in use; disabling it may cause unexpected results.", (PXErrorLevel) 2));
    }
    else
      sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) false, (Exception) null);
  }

  public virtual string ToString() => $"FeatureRestrictorAttribute<{this.typeName}>";
}
