// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Workflow.SalesOrder.WorkflowBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.SO.Workflow.SalesOrder;

public abstract class WorkflowBase : PXGraphExtension<ScreenConfiguration, SOOrderEntry>
{
  public static void DisableWholeScreen(
    BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields states)
  {
    states.AddTable<SOOrder>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOLine>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOTaxTran>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOBillingAddress>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOBillingContact>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOShippingAddress>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOShippingContact>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOLineSplit>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<CRRelation>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
  }

  public class Conditions : BoundedTo<SOOrderEntry, SOOrder>.Condition.Pack
  {
    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsCompleted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.completed, IBqlBool>.IsEqual<True>>()), nameof (IsCompleted));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsCancelled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.cancelled, IBqlBool>.IsEqual<True>>()), nameof (IsCancelled));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsOnCreditHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.creditHold, IBqlBool>.IsEqual<True>>()), nameof (IsOnCreditHold));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsHoldEntryOrLSEntryEnabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<Where<BqlField<SOOrderType.holdEntry, IBqlBool>.FromCurrent, Equal<True>, Or<BqlField<SOOrderType.requireLocation, IBqlBool>.FromCurrent, Equal<True>, Or<BqlField<SOOrderType.requireLotSerial, IBqlBool>.FromCurrent, Equal<True>>>>>()), nameof (IsHoldEntryOrLSEntryEnabled));
      }
    }
  }
}
