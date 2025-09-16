// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RegisterRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

[TableDashboardType]
public class RegisterRelease : PXGraph<RegisterRelease>
{
  public PXCancel<PMRegister> Cancel;
  [PXFilterable(new Type[] {})]
  public PXProcessing<PMRegister, Where<PMRegister.released, Equal<False>>> Items;

  public RegisterRelease()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<PMRegister>) this.Items).SetProcessDelegate(RegisterRelease.\u003C\u003Ec.\u003C\u003E9__2_0 ?? (RegisterRelease.\u003C\u003Ec.\u003C\u003E9__2_0 = new PXProcessingBase<PMRegister>.ProcessListDelegate((object) RegisterRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__2_0))));
    ((PXProcessing<PMRegister>) this.Items).SetProcessCaption("Release");
    ((PXProcessing<PMRegister>) this.Items).SetProcessAllCaption("Release All");
  }

  public static void Release(PMRegister doc)
  {
    RegisterRelease.Release(new List<PMRegister>() { doc }, false);
  }

  public static void Release(List<PMRegister> list, bool isMassProcess)
  {
    List<ProcessInfo<Batch>> infoList;
    try
    {
      if (!RegisterRelease.ReleaseWithoutPost(list, isMassProcess, out infoList))
        throw new PXException("One or more documents could not be released.");
    }
    catch (PMAllocationException ex)
    {
      throw new PXException("The document ({0}) has been released but the auto-allocation process performed for the related project transactions has failed for a range of the transactions. See Trace for details.", new object[1]
      {
        (object) ex.RefNbr
      });
    }
    if (!RegisterRelease.Post(infoList, isMassProcess))
      throw new PXException("One or more documents was released but could not be posted.");
  }

  public static bool ReleaseWithoutPost(
    List<PMRegister> list,
    bool isMassProcess,
    out List<ProcessInfo<Batch>> infoList)
  {
    bool flag1 = false;
    bool flag2 = false;
    string str = "";
    infoList = new List<ProcessInfo<Batch>>();
    if (!list.Any<PMRegister>())
      return !flag1;
    RegisterReleaseProcess instance1 = PXGraph.CreateInstance<RegisterReleaseProcess>();
    JournalEntry instance2 = PXGraph.CreateInstance<JournalEntry>();
    PMAllocator instance3 = PXGraph.CreateInstance<PMAllocator>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance2).FieldVerifying.AddHandler<GLTran.projectID>(RegisterRelease.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (RegisterRelease.\u003C\u003Ec.\u003C\u003E9__5_0 = new PXFieldVerifying((object) RegisterRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseWithoutPost\u003Eb__5_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance2).FieldVerifying.AddHandler<GLTran.taskID>(RegisterRelease.\u003C\u003Ec.\u003C\u003E9__5_1 ?? (RegisterRelease.\u003C\u003Ec.\u003C\u003E9__5_1 = new PXFieldVerifying((object) RegisterRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseWithoutPost\u003Eb__5_1))));
    for (int index = 0; index < list.Count; ++index)
    {
      ProcessInfo<Batch> processInfo = new ProcessInfo<Batch>(index);
      infoList.Add(processInfo);
      PMRegister doc = list[index];
      try
      {
        List<PMTask> allocTasks;
        processInfo.Batches.AddRange((IEnumerable<Batch>) instance1.Release(instance2, doc, out allocTasks));
        ((PXGraph) instance3).Clear();
        ((PXGraph) instance3).TimeStamp = ((PXGraph) instance2).TimeStamp;
        if (allocTasks.Count > 0)
        {
          if (!instance3.ExecuteContinueOnError(allocTasks))
          {
            flag2 = true;
            str += $"{doc.RefNbr},";
          }
          ((PXGraph) instance3).Actions.PressSave();
        }
        if (((PXSelectBase<PMRegister>) instance3.Document).Current != null && instance1.AutoReleaseAllocation)
          processInfo.Batches.AddRange((IEnumerable<Batch>) instance1.Release(instance2, ((PXSelectBase<PMRegister>) instance3.Document).Current, out List<PMTask> _));
        if (isMassProcess)
        {
          if (flag2)
            PXProcessing<PMRegister>.SetWarning(index, "Auto-allocation of Project Transactions failed.");
          else
            PXProcessing<PMRegister>.SetInfo(index, "The record has been processed successfully.");
        }
      }
      catch (Exception ex)
      {
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<PMRegister>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
        flag1 = true;
      }
    }
    if (flag2)
      throw new PMAllocationException(str.TrimEnd(','), "Auto-allocation of Project Transactions failed.");
    return !flag1;
  }

  public static bool Post(List<ProcessInfo<Batch>> infoList, bool isMassProcess)
  {
    PostGraph instance = PXGraph.CreateInstance<PostGraph>();
    PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSetup<PMSetup>.Select((PXGraph) instance, Array.Empty<object>()));
    bool flag = false;
    if (pmSetup != null && pmSetup.AutoPost.GetValueOrDefault())
    {
      foreach (ProcessInfo<Batch> info in infoList)
      {
        foreach (Batch batch in info.Batches)
        {
          try
          {
            ((PXGraph) instance).Clear();
            instance.PostBatchProc(batch);
          }
          catch (Exception ex)
          {
            if (isMassProcess)
            {
              flag = true;
              PXProcessing<PMRegister>.SetError(info.RecordIndex, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
            }
            else
            {
              string str = PXMessages.LocalizeNoPrefix("Failed to Automatically Post GLBatch created during release of PM document.") + Environment.NewLine + ex.Message;
              throw new PXMassProcessException(info.RecordIndex, (Exception) new PXException(str, ex));
            }
          }
        }
      }
    }
    return !flag;
  }
}
