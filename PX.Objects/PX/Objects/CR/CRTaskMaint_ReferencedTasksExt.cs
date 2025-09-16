// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRTaskMaint_ReferencedTasksExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR.CRTaskMaint_Extensions;
using System;
using System.Collections;
using System.Linq;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
public class CRTaskMaint_ReferencedTasksExt : PXGraphExtension<
#nullable disable
CRTaskMaint>
{
  [PXFilterable(new System.Type[] {})]
  public PXSelect<CRTaskMaint_ReferencedTasksExt.CRActivityReferencedTasksBackwardCompatibility> ReferencedTasks;

  public IEnumerable referencedTasks()
  {
    foreach (CRChildActivity crChildActivity in GraphHelper.RowCast<CRChildActivity>((IEnumerable) ((PXSelectBase<CRChildActivity>) ((PXGraph) this.Base).GetExtension<CRTaskMaint_ActivityDetailsExt>().Activities).Select(Array.Empty<object>())).ToList<CRChildActivity>().Where<CRChildActivity>((Func<CRChildActivity, bool>) (_ =>
    {
      int? classId = _.ClassID;
      int num = 0;
      return classId.GetValueOrDefault() == num & classId.HasValue;
    })))
    {
      CRTaskMaint_ReferencedTasksExt.CRActivityReferencedTasksBackwardCompatibility backwardCompatibility = new CRTaskMaint_ReferencedTasksExt.CRActivityReferencedTasksBackwardCompatibility();
      backwardCompatibility.CompletedDateTime = crChildActivity.CompletedDate;
      backwardCompatibility.EndDate = crChildActivity.EndDate;
      backwardCompatibility.StartDate = crChildActivity.StartDate;
      backwardCompatibility.Status = crChildActivity.UIStatus;
      backwardCompatibility.Subject = crChildActivity.Subject;
      backwardCompatibility.RefNoteID = crChildActivity.RefNoteID;
      backwardCompatibility.NoteID = crChildActivity.NoteID;
      yield return (object) backwardCompatibility;
    }
  }

  [PXHidden]
  public sealed class CRActivityReferencedTasksBackwardCompatibility : CRChildActivity
  {
    [PXInt]
    [PXUIField(DisplayName = "Record ID", Visible = false)]
    public int? RecordID { get; set; }

    [PXDate(InputMask = "g", DisplayMask = "g")]
    [PXUIField(DisplayName = "Completed At", Enabled = false)]
    public DateTime? CompletedDateTime { get; set; }

    [PXString]
    [ActivityStatusList]
    [PXUIField(DisplayName = "Status", Enabled = false)]
    public string Status { get; set; }

    public abstract class recordID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CRTaskMaint_ReferencedTasksExt.CRActivityReferencedTasksBackwardCompatibility.recordID>
    {
    }

    public abstract class completedDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CRTaskMaint_ReferencedTasksExt.CRActivityReferencedTasksBackwardCompatibility.completedDateTime>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CRTaskMaint_ReferencedTasksExt.CRActivityReferencedTasksBackwardCompatibility.status>
    {
    }
  }
}
