// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.EntityDescription`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class EntityDescription<RefNoteID> : BqlFormulaEvaluator<RefNoteID>, IBqlOperand where RefNoteID : IBqlField
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> pars)
  {
    Guid? par = (Guid?) pars[typeof (RefNoteID)];
    return (object) new EntityHelper(cache.Graph).GetEntityDescription(par, item.GetType());
  }
}
