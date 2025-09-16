// Decompiled with JetBrains decompiler
// Type: PX.Data.EntityDescription`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <summary>Gets description of the entity whose <tt>NoteID</tt> value is stored in a specified <tt>RefNoteID</tt> field
/// and type is stored in a <tt>RefEntityType</tt> field (if type is empty then it will be taken from a Note record).</summary>
/// <value>The description is retrieved by the <see cref="M:PX.Data.EntityHelper.GetEntityDescription(System.Nullable{System.Guid},System.Type)" /> method.</value>
internal class EntityDescription<RefNoteID, RefEntityType> : 
  BqlFormulaEvaluator<RefNoteID, RefEntityType>,
  IBqlOperand
  where RefNoteID : IBqlField
  where RefEntityType : IBqlField
{
  public override object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> pars)
  {
    Guid? par1 = (Guid?) pars[typeof (RefNoteID)];
    string par2 = (string) pars[typeof (RefEntityType)];
    EntityHelper entityHelper = new EntityHelper(cache.Graph);
    object row;
    string str;
    if (string.IsNullOrEmpty(par2))
    {
      str = entityHelper.GetEntityDescription(par1, item.GetType(), out row);
    }
    else
    {
      row = entityHelper.GetEntityRow(PXBuildManager.GetType(par2, false), par1);
      str = EntityHelper.GetEntityDescription(cache.Graph, row);
    }
    if (string.IsNullOrEmpty(str))
      str = entityHelper.GetEntityKeysDescription((IBqlTable) row);
    return (object) str;
  }
}
