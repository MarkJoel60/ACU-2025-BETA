// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXChangeBAccountID`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

public class PXChangeBAccountID<Table, Field> : PXChangeID<Table, Field>
  where Table : class, IBqlTable, new()
  where Field : class, IBqlField
{
  protected string DuplicatedKeyMessage = "The {0} identifier is already used for the following records: {1}.";

  public PXChangeBAccountID(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXChangeBAccountID(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    string str1 = "";
    string acctCD = e.NewValue.ToString().Trim();
    BAccount baccount = BAccount.UK.Find(sender.Graph, acctCD);
    if (baccount != null)
    {
      string str2;
      BAccountType.BAccountTypes.TryGetValue(baccount.Type, out str2);
      string str3 = $"{str1}{str2}, ";
      if (!Str.IsNullOrEmpty(str3))
      {
        bool? isBranch = baccount.IsBranch;
        if (isBranch.HasValue && isBranch.GetValueOrDefault() && !(baccount.Type == "CP"))
          str3 += "Branch";
        string str4 = str3.Replace(" &", ",").Trim(',', ' ');
        throw new PXSetPropertyException(this.DuplicatedKeyMessage, new object[2]
        {
          (object) acctCD,
          (object) str4
        });
      }
    }
    object newValue = e.NewValue;
    sender.Graph.Caches[typeof (Table)].RaiseFieldVerifying<Field>((object) null, ref newValue);
    e.NewValue = newValue;
    PXUIFieldAttribute.SetError<ChangeIDParam.cD>(sender, e.Row, (string) null);
  }
}
