// Decompiled with JetBrains decompiler
// Type: PX.Data.Interceptor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

internal class Interceptor : PXDBInterceptorAttribute
{
  public Func<bool, IList> Source;
  public System.Type ItemType;
  public System.Action Commit;

  public override BqlCommand GetRowCommand() => (BqlCommand) null;

  public override BqlCommand GetTableCommand() => (BqlCommand) null;

  internal int GetIndexOf(object item, PXCache cache)
  {
    IList list = this.Source(false);
    if (list == null)
      return -1;
    for (int index = 0; index < list.Count; ++index)
    {
      object a = list[index];
      if (cache.ObjectsEqual(a, item))
        return index;
    }
    return -1;
  }

  public override bool PersistInserted(PXCache sender, object row)
  {
    if (this.GetIndexOf(row, sender) >= 0)
      throw new PXException("Items exists");
    int num = this.Verify(sender, row, PXDBOperation.Insert) ? 1 : 0;
    if (num != 0)
    {
      object clone = sender.CreateCopy(row);
      IList lst = this.Source(true);
      this.Commit += (System.Action) (() => lst.Add(clone));
      return num != 0;
    }
    this.GenerateError(sender, row, PXDBOperation.Insert);
    return num != 0;
  }

  public override bool PersistDeleted(PXCache sender, object row)
  {
    int indexOf = this.GetIndexOf(row, sender);
    if (indexOf < 0)
      throw new PXException("Items not found");
    int num = this.Verify(sender, row, PXDBOperation.Delete) ? 1 : 0;
    if (num != 0)
    {
      IList lst = this.Source(true);
      object deletedCopy = lst[indexOf];
      this.Commit += (System.Action) (() => lst.Remove(deletedCopy));
      return num != 0;
    }
    this.GenerateError(sender, row, PXDBOperation.Delete);
    return num != 0;
  }

  public override bool PersistUpdated(PXCache sender, object row)
  {
    if (this.GetIndexOf(row, sender) < 0)
      return this.PersistInserted(sender, row);
    int num = this.Verify(sender, row, PXDBOperation.Update) ? 1 : 0;
    if (num != 0)
      return num != 0;
    this.GenerateError(sender, row, PXDBOperation.Update);
    return num != 0;
  }

  private void GenerateError(PXCache cache, object row, PXDBOperation operation)
  {
    string local;
    switch (operation)
    {
      case PXDBOperation.Insert:
        local = ErrorMessages.GetLocal("Inserting ");
        break;
      case PXDBOperation.Delete:
        local = ErrorMessages.GetLocal("Deleting ");
        break;
      default:
        local = ErrorMessages.GetLocal("Updating ");
        break;
    }
    Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(cache, row);
    string itemName = PXUIFieldAttribute.GetItemName(cache);
    throw new PXOuterException(cache.GetNonEmptyDacDescriptor(row), errors, cache.Graph.GetType(), row, "{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
    {
      (object) local,
      (object) itemName
    });
  }

  private bool Verify(PXCache cache, object row, PXDBOperation operation)
  {
    bool flag = true;
    PXRowPersistingEventArgs e = new PXRowPersistingEventArgs(operation, row);
    foreach (string field in (List<string>) cache.Fields)
    {
      if (this.HasError(cache, row, field))
        return false;
      foreach (PXDefaultAttribute defaultAttribute in cache.GetAttributesReadonly(row, field).OfType<PXDefaultAttribute>().Where<PXDefaultAttribute>((Func<PXDefaultAttribute, bool>) (defaultAttribute => defaultAttribute.PersistingCheck != PXPersistingCheck.Nothing)))
      {
        defaultAttribute.RowPersisting(cache, e);
        int num = this.HasError(cache, row, field) ? 1 : 0;
        if (num != 0)
          flag = false;
        if (num != 0)
        {
          cache.RaiseExceptionHandling(field, row, (object) null, (Exception) null);
          return false;
        }
      }
    }
    return flag;
  }

  private bool HasError(PXCache cache, object row, string fieldName)
  {
    return !string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly(cache, row, fieldName));
  }
}
