// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRSelectNotificatonTemplateExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <exclude />
public abstract class CRSelectNotificatonTemplateExt<TGraph, TMaster> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TMaster : class, IBqlTable, new()
{
  [PXViewName("Email Template")]
  public CRValidationFilter<NotificationFilter> NotificationInfo;
  public PXAction<TMaster> LoadEmailSource;

  [PXUIField]
  [PXButton(Tooltip = "Select Template")]
  protected virtual IEnumerable loadEmailSource(PXAdapter adapter)
  {
    if (this.NotificationInfo.AskExtFullyValid((DialogAnswerType) 1, true))
    {
      Notification notification = PXResultset<Notification>.op_Implicit(PXSelectBase<Notification, PXSelect<Notification, Where<Notification.name, Equal<Required<Notification.name>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((PXSelectBase<NotificationFilter>) this.NotificationInfo).Current.NotificationName
      }));
      List<Guid> guidList = new List<Guid>();
      foreach (PXResult<NoteDoc> pxResult in PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) notification.NoteID
      }))
      {
        NoteDoc noteDoc = PXResult<NoteDoc>.op_Implicit(pxResult);
        guidList.Add(noteDoc.FileID.Value);
      }
      PXNoteAttribute.SetFileNotes((PXCache) GraphHelper.Caches<TMaster>((PXGraph) this.Base), ((PXCache) GraphHelper.Caches<TMaster>((PXGraph) this.Base)).Current, guidList.ToArray());
      this.MapData(notification);
    }
    if (this.Base.IsContractBasedAPI)
      this.Base.Actions.PressSave();
    return adapter.Get();
  }

  public abstract void MapData(Notification notification);
}
