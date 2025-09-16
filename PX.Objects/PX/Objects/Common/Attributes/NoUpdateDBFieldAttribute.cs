// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.NoUpdateDBFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common.Attributes;

/// <summary>
/// The attribute indicates that the changes of the field marked with it won't be persisted to the database.
/// </summary>
public class NoUpdateDBFieldAttribute : PXEventSubscriberAttribute, IPXCommandPreparingSubscriber
{
  public virtual bool NoInsert { get; set; }

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    PXDBOperation pxdbOperation = PXDBOperationExt.Command(e.Operation);
    if (pxdbOperation != 1 && (pxdbOperation != 2 || !this.NoInsert))
      return;
    e.ExcludeFromInsertUpdate();
  }
}
