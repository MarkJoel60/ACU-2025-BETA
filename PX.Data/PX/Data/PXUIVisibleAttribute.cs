// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUIVisibleAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>This attribute manages the field visibility at runtime.</summary>
/// <remarks>This attribute subscribes to the <tt>RowSelected</tt> event handler
/// of the attribute level. It is applicable when the field visibility
/// depends on the state of the object only.
/// <para>Make sure that you do not use this attribute and the
/// <tt>PXUIFieldAttribute.SetVisible</tt> method at the same time.</para></remarks>
/// <seealso cref="T:PX.Data.PXUIEnabledAttribute" />
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXUIVisibleAttribute(System.Type conditionType) : 
  PXBaseConditionAttribute(conditionType),
  IPXFieldSelectingSubscriber
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.SetAltered(this._FieldName, true);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._Formula == null || e.Row == null && BqlFormula.IsContextualFormula(sender, this.Formula))
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, visible: new bool?(PXBaseConditionAttribute.GetConditionResult(sender, e.Row, this.Formula)));
  }
}
