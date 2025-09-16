// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAttributeGroupList`4
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FS;

public class FSAttributeGroupList<TClass, TEntity1, TEntity2, TEntity3> : 
  CSAttributeGroupList<TClass, TEntity1>,
  IPXRowInsertedSubscriber,
  IPXRowDeletedSubscriber,
  IPXRowUpdatedSubscriber
  where TClass : class
{
  public FSAttributeGroupList(PXGraph graph)
    : base(graph)
  {
    PXGraph.RowInsertedEvents rowInserted = graph.RowInserted;
    FSAttributeGroupList<TClass, TEntity1, TEntity2, TEntity3> attributeGroupList1 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) attributeGroupList1, __vmethodptr(attributeGroupList1, RowInserted));
    rowInserted.AddHandler<CSAttributeGroup>(pxRowInserted);
    PXGraph.RowUpdatedEvents rowUpdated = graph.RowUpdated;
    FSAttributeGroupList<TClass, TEntity1, TEntity2, TEntity3> attributeGroupList2 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) attributeGroupList2, __vmethodptr(attributeGroupList2, RowUpdated));
    rowUpdated.AddHandler<CSAttributeGroup>(pxRowUpdated);
    PXGraph.RowDeletedEvents rowDeleted = graph.RowDeleted;
    FSAttributeGroupList<TClass, TEntity1, TEntity2, TEntity3> attributeGroupList3 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) attributeGroupList3, __vmethodptr(attributeGroupList3, RowDeleted));
    rowDeleted.AddHandler<CSAttributeGroup>(pxRowDeleted);
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.CacheEventHandler(sender, e.Row, (PXDBOperation) 1, e.ExternalCall);
  }

  public void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    this.CacheEventHandler(sender, e.Row, (PXDBOperation) 3, e.ExternalCall);
  }

  public void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.CacheEventHandler(sender, e.Row, (PXDBOperation) 2, e.ExternalCall);
  }

  public void CacheEventHandler(
    PXCache sender,
    object row,
    PXDBOperation operation,
    bool externalCall)
  {
    if (row == null)
      return;
    CSAttributeGroup csAttributeGroup = (CSAttributeGroup) row;
    if (csAttributeGroup.EntityType != typeof (TEntity1).FullName)
      return;
    CSAttributeGroup copy1 = (CSAttributeGroup) sender.CreateCopy((object) csAttributeGroup);
    copy1.EntityType = typeof (TEntity2).FullName;
    this.UpdateCacheRecord(sender, copy1, operation);
    CSAttributeGroup copy2 = (CSAttributeGroup) sender.CreateCopy((object) csAttributeGroup);
    copy2.EntityType = typeof (TEntity3).FullName;
    this.UpdateCacheRecord(sender, copy2, operation);
  }

  public void UpdateCacheRecord(
    PXCache sender,
    CSAttributeGroup cSAttributeGroup,
    PXDBOperation operation)
  {
    if (operation == 2 || operation == 1)
    {
      sender.Update((object) cSAttributeGroup);
    }
    else
    {
      if (operation != 3)
        return;
      sender.Delete((object) cSAttributeGroup);
    }
  }
}
