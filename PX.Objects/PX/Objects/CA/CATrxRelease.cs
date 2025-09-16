// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATrxRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.CA;

[PXPrimaryGraph(new Type[] {typeof (CATranEntry), typeof (CashTransferEntry)}, new Type[] {typeof (Select<CAAdj, Where<CAAdj.tranID, Equal<Current<CATran.tranID>>>>), typeof (Select<CATransfer, Where<CATransfer.tranIDIn, Equal<Current<CATran.tranID>>, Or<CATransfer.tranIDOut, Equal<Current<CATran.tranID>>>>>)})]
[TableAndChartDashboardType]
[Serializable]
public class CATrxRelease : PXGraph<
#nullable disable
CATrxRelease>
{
  public PXAction<PX.Objects.CA.CARegister> viewCATrx;
  public PXAction<PX.Objects.CA.CARegister> cancel;
  [PXFilterable(new Type[] {})]
  public PXProcessing<PX.Objects.CA.CARegister> CARegisterList;
  public PXSetup<CASetup> cASetup;

  public CATrxRelease()
  {
    CASetup current = ((PXSelectBase<CASetup>) this.cASetup).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<PX.Objects.CA.CARegister>) this.CARegisterList).SetProcessDelegate(CATrxRelease.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (CATrxRelease.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXProcessingBase<PX.Objects.CA.CARegister>.ProcessListDelegate((object) CATrxRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__1_0))));
    this.CARegisterList.SetProcessCaption("Release");
    this.CARegisterList.SetProcessAllCaption("Release All");
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.CARegisterList).Cache.Clear();
    ((PXGraph) this).TimeStamp = (byte[]) null;
    PXLongOperation.ClearStatus(((PXGraph) this).UID);
    return adapter.Get();
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewCATrx(PXAdapter adapter)
  {
    PX.Objects.CA.CARegister current = ((PXSelectBase<PX.Objects.CA.CARegister>) this.CARegisterList).Current;
    if (current != null)
    {
      switch (current.TranType)
      {
        case "CAE":
          CATranEntry instance1 = PXGraph.CreateInstance<CATranEntry>();
          ((PXGraph) instance1).Clear();
          ((PXSelectBase) this.CARegisterList).Cache.IsDirty = false;
          ((PXSelectBase<CAAdj>) instance1.CAAdjRecords).Current = PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelect<CAAdj, Where<CAAdj.adjRefNbr, Equal<Required<CATran.origRefNbr>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) current.ReferenceNbr
          }));
          PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "Document");
          ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException1;
        case "CVD":
        case "CDT":
          CADepositEntry instance2 = PXGraph.CreateInstance<CADepositEntry>();
          ((PXGraph) instance2).Clear();
          ((PXSelectBase) this.CARegisterList).Cache.IsDirty = false;
          ((PXSelectBase<CADeposit>) instance2.Document).Current = PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelect<CADeposit, Where<CADeposit.refNbr, Equal<Required<CATran.origRefNbr>>, And<CADeposit.tranType, Equal<Required<CATran.origTranType>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) current.ReferenceNbr,
            (object) current.TranType
          }));
          PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException((PXGraph) instance2, true, "Document");
          ((PXBaseRedirectException) requiredException2).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException2;
        case "CT%":
          CashTransferEntry instance3 = PXGraph.CreateInstance<CashTransferEntry>();
          ((PXGraph) instance3).Clear();
          ((PXSelectBase) this.CARegisterList).Cache.IsDirty = false;
          ((PXSelectBase<CATransfer>) instance3.Transfer).Current = PXResultset<CATransfer>.op_Implicit(PXSelectBase<CATransfer, PXSelect<CATransfer, Where<CATransfer.transferNbr, Equal<Required<CATransfer.transferNbr>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) current.ReferenceNbr
          }));
          PXRedirectRequiredException requiredException3 = new PXRedirectRequiredException((PXGraph) instance3, true, "Document");
          ((PXBaseRedirectException) requiredException3).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException3;
      }
    }
    return (IEnumerable) ((PXSelectBase<PX.Objects.CA.CARegister>) this.CARegisterList).Select(Array.Empty<object>());
  }

  protected virtual IEnumerable caregisterlist()
  {
    CATrxRelease caTrxRelease1 = this;
    bool anyFound = false;
    foreach (PX.Objects.CA.CARegister caRegister in ((PXSelectBase) caTrxRelease1.CARegisterList).Cache.Inserted)
    {
      anyFound = true;
      yield return (object) caRegister;
    }
    if (!anyFound)
    {
      foreach (PXResult<CADeposit> pxResult in PXSelectBase<CADeposit, PXSelectJoin<CADeposit, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CADeposit.cashAccountID>, And<Match<CashAccount, Current<AccessInfo.userName>>>>>, Where<CADeposit.released, Equal<boolFalse>, And<CADeposit.hold, Equal<boolFalse>, And<Where<CADeposit.tranType, Equal<CATranType.cADeposit>, Or<CADeposit.tranType, Equal<CATranType.cAVoidDeposit>>>>>>>.Config>.Select((PXGraph) caTrxRelease1, Array.Empty<object>()))
      {
        CADeposit caDeposit = PXResult<CADeposit>.op_Implicit(pxResult);
        if (caDeposit.TranID.HasValue)
          yield return ((PXSelectBase) caTrxRelease1.CARegisterList).Cache.Insert((object) CATrxRelease.CARegister(caDeposit));
      }
      foreach (PXResult<CAAdj> pxResult in PXSelectBase<CAAdj, PXSelectJoin<CAAdj, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CAAdj.cashAccountID>, And<Match<CashAccount, Current<AccessInfo.userName>>>>>, Where<CAAdj.released, Equal<boolFalse>, And<CAAdj.status, Equal<CATransferStatus.balanced>, And<CAAdj.adjTranType, Equal<CATranType.cAAdjustment>>>>>.Config>.Select((PXGraph) caTrxRelease1, Array.Empty<object>()))
      {
        CAAdj caAdj = PXResult<CAAdj>.op_Implicit(pxResult);
        if (caAdj.TranID.HasValue)
          yield return ((PXSelectBase) caTrxRelease1.CARegisterList).Cache.Insert((object) CATrxRelease.CARegister(caAdj));
      }
      foreach (PXResult<CATransfer> pxResult1 in PXSelectBase<CATransfer, PXSelectJoin<CATransfer, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CATransfer.inAccountID>, And<Match<CashAccount, Current<AccessInfo.userName>>>>, InnerJoin<CATrxRelease.CashAccount1, On<CATrxRelease.CashAccount1.cashAccountID, Equal<CATransfer.outAccountID>, And<Match<CATrxRelease.CashAccount1, Current<AccessInfo.userName>>>>>>, Where<CATransfer.released, Equal<boolFalse>, And<CATransfer.hold, Equal<boolFalse>>>>.Config>.Select((PXGraph) caTrxRelease1, Array.Empty<object>()))
      {
        CATransfer trf = PXResult<CATransfer>.op_Implicit(pxResult1);
        CATrxRelease caTrxRelease2 = caTrxRelease1;
        object[] objArray = new object[2]
        {
          (object) trf.TranIDIn,
          (object) trf.TranIDOut
        };
        foreach (PXResult<CATran> pxResult2 in PXSelectBase<CATran, PXSelect<CATran, Where<CATran.released, Equal<boolFalse>, And<CATran.hold, Equal<boolFalse>, And<Where<CATran.tranID, Equal<Required<CATransfer.tranIDIn>>, Or<CATran.tranID, Equal<Required<CATransfer.tranIDOut>>>>>>>>.Config>.Select((PXGraph) caTrxRelease2, objArray))
        {
          CATran tran = PXResult<CATran>.op_Implicit(pxResult2);
          yield return ((PXSelectBase) caTrxRelease1.CARegisterList).Cache.Insert((object) CATrxRelease.CARegister(trf, tran));
        }
        trf = (CATransfer) null;
      }
      ((PXSelectBase) caTrxRelease1.CARegisterList).Cache.IsDirty = false;
    }
  }

  protected virtual void CARegister_TranID_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    PX.Objects.CA.CARegister row = (PX.Objects.CA.CARegister) e.Row;
    if (row == null)
      return;
    Dictionary<long, CAMessage> customInfo = PXLongOperation.GetCustomInfo(((PXGraph) this).UID) as Dictionary<long, CAMessage>;
    TimeSpan timeSpan;
    Exception exception;
    PXLongRunStatus status = PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception);
    if (status != 3 && status != 2 || customInfo == null)
      return;
    CAMessage caMessage = (CAMessage) null;
    Dictionary<long, CAMessage> dictionary1 = customInfo;
    long? tranId = row.TranID;
    long key1 = tranId.Value;
    if (dictionary1.ContainsKey(key1))
    {
      Dictionary<long, CAMessage> dictionary2 = customInfo;
      tranId = row.TranID;
      long key2 = tranId.Value;
      caMessage = dictionary2[key2];
    }
    if (caMessage == null)
      return;
    string str = "extTranID";
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(), new int?(), new int?(), new int?(), (object) null, str, (string) null, (string) null, caMessage.Message, caMessage.ErrorLevel, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    e.IsAltered = true;
  }

  public static void GroupReleaseTransaction(
    List<CATran> tranList,
    bool allowAP,
    bool allowAR,
    bool updateInfo)
  {
    PXSetPropertyException error = (PXSetPropertyException) null;
    Dictionary<long, CAMessage> dictionary1 = new Dictionary<long, CAMessage>();
    if (updateInfo)
      PXLongOperation.SetCustomInfo((object) dictionary1);
    List<PX.Objects.CA.CARegister> caRegisterList = new List<PX.Objects.CA.CARegister>();
    bool flag = true;
    PXGraph aGraph = (PXGraph) null;
    for (int index = 0; index < tranList.Count; ++index)
    {
      CATran tran = tranList[index];
      long? tranId;
      try
      {
        bool? nullable = tran.Released;
        if (!nullable.GetValueOrDefault())
        {
          nullable = tran.Hold;
          if (nullable.GetValueOrDefault())
            throw new PXException("Document Status is invalid for processing.");
          switch (tran.OrigModule)
          {
            case "GL":
              throw new PXException("The document of this type cannot be released in the Cash Management module. Release the document in the module from which the document has originated.");
            case "AP":
              if (!allowAP)
                throw new PXException("The document of this type cannot be released in the Cash Management module. Release the document in the module from which the document has originated.");
              CATrxRelease.ReleaseCATran(tran, ref aGraph, out error);
              if (error != null)
                throw error;
              break;
            case "AR":
              if (!allowAR)
                throw new PXException("The document of this type cannot be released in the Cash Management module. Release the document in the module from which the document has originated.");
              CATrxRelease.ReleaseCATran(tran, ref aGraph, out error);
              if (error != null)
                throw error;
              break;
            case "CA":
              CATrxRelease.ReleaseCATran(tran, ref aGraph, out error);
              if (error != null)
                throw error;
              break;
            default:
              throw new PXException("The document of this type cannot be released in the Cash Management module. Release the document in the module from which the document has originated.");
          }
          if (updateInfo)
          {
            Dictionary<long, CAMessage> dictionary2 = dictionary1;
            tranId = tran.TranID;
            long key = tranId.Value;
            tranId = tran.TranID;
            CAMessage caMessage = new CAMessage(tranId.Value, (PXErrorLevel) 1, "The record has been processed successfully.");
            dictionary2.Add(key, caMessage);
          }
        }
      }
      catch (Exception ex)
      {
        flag = false;
        if (updateInfo)
        {
          Dictionary<long, CAMessage> dictionary3 = dictionary1;
          long key = tran.TranID.Value;
          tranId = tran.TranID;
          CAMessage caMessage = new CAMessage(tranId.Value, (PXErrorLevel) 5, ex.Message);
          dictionary3.Add(key, caMessage);
        }
      }
    }
    if (!flag)
      throw new PXException("One or more items are not released");
  }

  public static void ReleaseCATran(CATran aTran, ref PXGraph aGraph)
  {
    PXSetPropertyException error;
    CATrxRelease.ReleaseCATran(aTran, ref aGraph, (List<PX.Objects.GL.Batch>) null, out error);
    if (((Exception) error)?.InnerException != null)
      throw ((Exception) error).InnerException;
  }

  public static void ReleaseCATran(
    CATran aTran,
    ref PXGraph aGraph,
    out PXSetPropertyException error)
  {
    CATrxRelease.ReleaseCATran(aTran, ref aGraph, (List<PX.Objects.GL.Batch>) null, out error);
  }

  public static void ReleaseCATran(CATran aTran, ref PXGraph aGraph, List<PX.Objects.GL.Batch> externalPostList)
  {
    CATrxRelease.ReleaseCATran(aTran, ref aGraph, externalPostList, out PXSetPropertyException _);
  }

  public static void ReleaseCATran(
    CATran aTran,
    ref PXGraph aGraph,
    List<PX.Objects.GL.Batch> externalPostList,
    out PXSetPropertyException error)
  {
    error = (PXSetPropertyException) null;
    int num = 0;
    if (aTran == null)
      return;
    if (aGraph == null)
      aGraph = (PXGraph) PXGraph.CreateInstance<CATranEntry>();
    PXGraph pxGraph = aGraph;
    switch (aTran.OrigModule)
    {
      case "AP":
        PX.Objects.AP.APRegister apRegister = PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select(pxGraph, new object[2]
        {
          (object) aTran.OrigTranType,
          (object) aTran.OrigRefNbr
        }));
        bool? nullable1 = apRegister != null ? apRegister.Hold : throw new Exception("Document Not Found");
        bool flag1 = false;
        if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
          throw new PXException("Document Status is invalid for processing.");
        nullable1 = apRegister.Approved;
        bool flag2 = false;
        if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
        {
          PXException pxException = new PXException("The document has the Pending Approval status and cannot be released. The document must be approved by a responsible person before it can be released.");
          error = new PXSetPropertyException((Exception) pxException, (PXErrorLevel) 5, ((Exception) pxException).Message, Array.Empty<object>());
          break;
        }
        nullable1 = apRegister.Released;
        bool flag3 = false;
        if (!(nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue))
          break;
        APDocumentRelease.ReleaseDoc(new List<PX.Objects.AP.APRegister>()
        {
          apRegister
        }, false, externalPostList);
        break;
      case "AR":
        List<ARRegister> arRegisterList = new List<ARRegister>();
        ARRegister arRegister = PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>>>>.Config>.Select(pxGraph, new object[2]
        {
          (object) aTran.OrigTranType,
          (object) aTran.OrigRefNbr
        }));
        bool? nullable2 = arRegister != null ? arRegister.Hold : throw new Exception("Document Not Found");
        bool flag4 = false;
        if (!(nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue))
          throw new PXException("Document Status is invalid for processing.");
        nullable2 = arRegister.Approved;
        bool flag5 = false;
        if (nullable2.GetValueOrDefault() == flag5 & nullable2.HasValue)
        {
          PXException pxException = new PXException("The document has the Pending Approval status and cannot be released. The document must be approved by a responsible person before it can be released.");
          error = new PXSetPropertyException((Exception) pxException, (PXErrorLevel) 5, ((Exception) pxException).Message, Array.Empty<object>());
          break;
        }
        nullable2 = arRegister.PendingProcessing;
        nullable2 = !nullable2.GetValueOrDefault() ? arRegister.Released : throw new PXException("A document with the Pending Processing status cannot be released. The document has to be authorized and captured before it can be released.");
        bool flag6 = false;
        if (!(nullable2.GetValueOrDefault() == flag6 & nullable2.HasValue))
          break;
        using (new PXTimeStampScope((byte[]) null))
        {
          ARDocumentRelease.ReleaseDoc(new List<ARRegister>()
          {
            arRegister
          }, false, externalPostList);
          break;
        }
      case "CA":
        switch (aTran.OrigTranType)
        {
          case "CAE":
            CAAdj _doc1 = PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelect<CAAdj, Where<CAAdj.adjRefNbr, Equal<Required<CAAdj.adjRefNbr>>, And<CAAdj.adjTranType, Equal<Required<CAAdj.adjTranType>>>>>.Config>.Select(pxGraph, new object[2]
            {
              (object) aTran.OrigRefNbr,
              (object) aTran.OrigTranType
            }));
            bool? nullable3 = _doc1 != null ? _doc1.Hold : throw new Exception("Document Not Found");
            bool flag7 = false;
            if (!(nullable3.GetValueOrDefault() == flag7 & nullable3.HasValue))
              throw new PXException("Document Status is invalid for processing.");
            nullable3 = _doc1.Approved;
            bool flag8 = false;
            if (nullable3.GetValueOrDefault() == flag8 & nullable3.HasValue)
            {
              PXException pxException = new PXException("The document has the Pending Approval status and cannot be released. The document must be approved by a responsible person before it can be released.");
              error = new PXSetPropertyException((Exception) pxException, (PXErrorLevel) 5, ((Exception) pxException).Message, Array.Empty<object>());
              return;
            }
            nullable3 = _doc1.Released;
            bool flag9 = false;
            if (!(nullable3.GetValueOrDefault() == flag9 & nullable3.HasValue))
              return;
            CATrxRelease.ReleaseDoc<CAAdj>(_doc1, num, externalPostList, (SelectedEntityEvent<CAAdj>) PXEntityEventBase<CAAdj>.Container<CAAdj.Events>.Select((Expression<Func<CAAdj.Events, PXEntityEvent<CAAdj.Events>>>) (ev => ev.ReleaseDocument)));
            return;
          case "CTI":
          case "CTO":
          case "CTE":
            CATransfer _doc2 = PXResultset<CATransfer>.op_Implicit(PXSelectBase<CATransfer, PXSelect<CATransfer, Where<CATransfer.transferNbr, Equal<Required<CATransfer.transferNbr>>>>.Config>.Select(pxGraph, new object[1]
            {
              (object) aTran.OrigRefNbr
            }));
            bool? nullable4 = _doc2 != null ? _doc2.Hold : throw new Exception("Document Not Found");
            bool flag10 = false;
            if (!(nullable4.GetValueOrDefault() == flag10 & nullable4.HasValue))
              throw new PXException("Document Status is invalid for processing.");
            nullable4 = _doc2.Released;
            bool flag11 = false;
            if (!(nullable4.GetValueOrDefault() == flag11 & nullable4.HasValue))
              return;
            CATrxRelease.ReleaseDoc<CATransfer>(_doc2, num, externalPostList, (SelectedEntityEvent<CATransfer>) PXEntityEventBase<CATransfer>.Container<CATransfer.Events>.Select((Expression<Func<CATransfer.Events, PXEntityEvent<CATransfer.Events>>>) (ev => ev.ReleaseDocument)));
            return;
          case "CDT":
          case "CVD":
            CADeposit doc = PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelect<CADeposit, Where<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>>>.Config>.Select(pxGraph, new object[1]
            {
              (object) aTran.OrigRefNbr
            }));
            bool? nullable5 = doc != null ? doc.Hold : throw new Exception("Document Not Found");
            bool flag12 = false;
            if (!(nullable5.GetValueOrDefault() == flag12 & nullable5.HasValue))
              throw new PXException("Document Status is invalid for processing.");
            nullable5 = doc.Released;
            bool flag13 = false;
            if (!(nullable5.GetValueOrDefault() == flag13 & nullable5.HasValue))
              return;
            CADepositEntry.ReleaseDoc(doc, externalPostList);
            return;
          default:
            throw new Exception("Document Not Found");
        }
      default:
        throw new Exception("The document of this type cannot be released in the Cash Management module. Release the document in the module from which the document has originated.");
    }
  }

  public static void GroupRelease(List<PX.Objects.CA.CARegister> list, bool updateInfo)
  {
    Dictionary<long, CAMessage> dictionary1 = new Dictionary<long, CAMessage>();
    if (updateInfo)
      PXLongOperation.SetCustomInfo((object) dictionary1);
    CAReleaseProcess instance = PXGraph.CreateInstance<CAReleaseProcess>();
    JournalEntry journalEntry = CATrxRelease.CreateJournalEntry();
    HashSet<int> persistedBatchIndices = new HashSet<int>();
    Exception exception1 = (Exception) null;
    long? nullable1;
    for (int index = 0; index < list.Count; ++index)
    {
      PX.Objects.CA.CARegister caRegister = list[index];
      nullable1 = caRegister.TranID;
      if (!nullable1.HasValue)
        throw new PXException("Cannot release this document because it has no lines.");
      if (caRegister != null)
      {
        try
        {
          bool? nullable2 = caRegister.Released;
          bool flag = false;
          if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue))
            throw new Exception("Original document has already been released");
          nullable2 = caRegister.Hold;
          if (nullable2.Value)
            throw new Exception("Document on hold cannot be released");
          switch (caRegister.Module)
          {
            case "AP":
              List<PX.Objects.AP.APRegister> list1 = new List<PX.Objects.AP.APRegister>();
              list1.Add(PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) journalEntry, new object[2]
              {
                (object) caRegister.TranType,
                (object) caRegister.ReferenceNbr
              })) ?? throw new Exception("Transaction Not Complete"));
              APDocumentRelease.ReleaseDoc(list1, false);
              break;
            case "AR":
              List<ARRegister> list2 = new List<ARRegister>();
              list2.Add(PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>>>>.Config>.Select((PXGraph) journalEntry, new object[2]
              {
                (object) caRegister.TranType,
                (object) caRegister.ReferenceNbr
              })) ?? throw new Exception("Transaction Not Complete"));
              ARDocumentRelease.ReleaseDoc(list2, false);
              break;
            case "CA":
              switch (caRegister.TranType)
              {
                case "CAE":
                  CATrxRelease.ReleaseAndRecordCADoc<CAAdj>(journalEntry, instance, PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelect<CAAdj, Where<CAAdj.adjRefNbr, Equal<Required<CAAdj.adjRefNbr>>, And<CAAdj.adjTranType, Equal<Required<CAAdj.adjTranType>>>>>.Config>.Select((PXGraph) journalEntry, new object[2]
                  {
                    (object) caRegister.ReferenceNbr,
                    (object) caRegister.TranType
                  })) ?? throw new Exception("Document Not Found"), (SelectedEntityEvent<CAAdj>) PXEntityEventBase<CAAdj>.Container<CAAdj.Events>.Select((Expression<Func<CAAdj.Events, PXEntityEvent<CAAdj.Events>>>) (e => e.ReleaseDocument)));
                  break;
                case "CT%":
                  CATrxRelease.ReleaseAndRecordCADoc<CATransfer>(journalEntry, instance, PXResultset<CATransfer>.op_Implicit(PXSelectBase<CATransfer, PXSelect<CATransfer, Where<CATransfer.transferNbr, Equal<Required<CATransfer.transferNbr>>>>.Config>.Select((PXGraph) journalEntry, new object[1]
                  {
                    (object) caRegister.ReferenceNbr
                  })) ?? throw new Exception("Document Not Found"), (SelectedEntityEvent<CATransfer>) PXEntityEventBase<CATransfer>.Container<CATransfer.Events>.Select((Expression<Func<CATransfer.Events, PXEntityEvent<CATransfer.Events>>>) (e => e.ReleaseDocument)));
                  break;
                case "CDT":
                case "CVD":
                  CADepositEntry.ReleaseDoc(PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelect<CADeposit, Where<CADeposit.tranType, Equal<Required<CADeposit.tranType>>, And<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>>>>.Config>.Select((PXGraph) journalEntry, new object[2]
                  {
                    (object) caRegister.TranType,
                    (object) caRegister.ReferenceNbr
                  })) ?? throw new Exception("Document Not Found"));
                  break;
                default:
                  throw new Exception("Document Not Found");
              }
              break;
            default:
              throw new Exception("Document Not Found");
          }
          int num;
          if ((num = journalEntry.created.IndexOf(((PXSelectBase<PX.Objects.GL.Batch>) journalEntry.BatchModule).Current)) >= 0)
            persistedBatchIndices.Add(num);
          if (updateInfo)
          {
            if (!(caRegister.Module != "CA"))
            {
              if (instance.AutoPost)
                continue;
            }
            Dictionary<long, CAMessage> dictionary2 = dictionary1;
            nullable1 = caRegister.TranID;
            long key = nullable1.Value;
            nullable1 = caRegister.TranID;
            CAMessage caMessage = new CAMessage(nullable1.Value, (PXErrorLevel) 1, "The record has been processed successfully.");
            dictionary2.Add(key, caMessage);
          }
        }
        catch (Exception ex)
        {
          if (updateInfo)
          {
            string aMessage = ex is PXOuterException ? $"{ex.Message} {string.Join(" ", ((PXOuterException) ex).InnerMessages)}" : ex.Message;
            Dictionary<long, CAMessage> dictionary3 = dictionary1;
            nullable1 = caRegister.TranID;
            long key = nullable1.Value;
            nullable1 = caRegister.TranID;
            CAMessage caMessage = new CAMessage(nullable1.Value, (PXErrorLevel) 5, aMessage);
            dictionary3.Add(key, caMessage);
          }
          ((PXGraph) journalEntry).Clear();
          journalEntry.CleanupCreated((ICollection<int>) persistedBatchIndices);
          exception1 = ex;
        }
      }
    }
    Exception exception2 = (Exception) null;
    if (instance.AutoPost)
    {
      Dictionary<PX.Objects.GL.Batch, Exception> dictionary4 = PostGraph.Post(journalEntry.created.Where<PX.Objects.GL.Batch>((Func<PX.Objects.GL.Batch, bool>) (b => b.Released.GetValueOrDefault())).ToList<PX.Objects.GL.Batch>());
      if (dictionary4.Count > 0)
      {
        exception2 = dictionary4.Values.FirstOrDefault<Exception>();
        if (updateInfo)
        {
          foreach (KeyValuePair<PX.Objects.GL.Batch, Exception> keyValuePair in dictionary4)
          {
            foreach (PXResult<PX.Objects.GL.GLTran> pxResult in PXSelectBase<PX.Objects.GL.GLTran, PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.module, Equal<Required<PX.Objects.GL.Batch.module>>, And<PX.Objects.GL.GLTran.batchNbr, Equal<Required<PX.Objects.GL.Batch.batchNbr>>, And<PX.Objects.GL.GLTran.cATranID, IsNotNull>>>>.Config>.Select((PXGraph) instance, new object[2]
            {
              (object) keyValuePair.Key.Module,
              (object) keyValuePair.Key.BatchNbr
            }))
            {
              nullable1 = PXResult<PX.Objects.GL.GLTran>.op_Implicit(pxResult).CATranID;
              long valueOrDefault = nullable1.GetValueOrDefault();
              if (!dictionary1.ContainsKey(valueOrDefault))
                dictionary1.Add(valueOrDefault, new CAMessage(valueOrDefault, (PXErrorLevel) 5, keyValuePair.Value.Message));
            }
          }
        }
      }
      if (updateInfo)
      {
        foreach (PX.Objects.CA.CARegister caRegister in list)
        {
          nullable1 = caRegister.TranID;
          long num = nullable1.Value;
          if (!dictionary1.ContainsKey(num))
            dictionary1.Add(num, new CAMessage(num, (PXErrorLevel) 1, "The record has been processed successfully."));
        }
      }
    }
    if (exception1 != null)
    {
      if (list.Count == 1)
        throw exception1;
      throw new Exception("One or more items are not released");
    }
    if (exception2 == null)
      return;
    if (list.Count == 1)
      throw exception2;
    throw new Exception("One or more items are not posted");
  }

  public static JournalEntry CreateJournalEntry()
  {
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    instance.PrepareForDocumentRelease();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).RowInserting.AddHandler<PX.Objects.GL.GLTran>(CATrxRelease.\u003C\u003Ec.\u003C\u003E9__16_0 ?? (CATrxRelease.\u003C\u003Ec.\u003C\u003E9__16_0 = new PXRowInserting((object) CATrxRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateJournalEntry\u003Eb__16_0))));
    return instance;
  }

  private static void ReleaseAndRecordCADoc<TCADoc>(
    JournalEntry je,
    CAReleaseProcess rg,
    TCADoc doc,
    SelectedEntityEvent<TCADoc> releaseEvent)
    where TCADoc : class, ICADocument, IBqlTable, new()
  {
    List<PX.Objects.GL.Batch> batchlist = new List<PX.Objects.GL.Batch>();
    ((PXGraph) rg).Clear();
    rg.ReleaseDocProc<TCADoc>(je, ref batchlist, doc, releaseEvent);
  }

  public static PX.Objects.CA.CARegister CARegister(CATran item)
  {
    CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
    switch (item.OrigModule)
    {
      case "AP":
        return CATrxRelease.CARegister(PXResultset<PX.Objects.AP.APPayment>.op_Implicit(PXSelectBase<PX.Objects.AP.APPayment, PXSelect<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.cATranID, Equal<Required<PX.Objects.AP.APPayment.cATranID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) item.TranID
        })) ?? throw new Exception("Orig. Document Can Not Be Found"));
      case "AR":
        return CATrxRelease.CARegister(PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(PXSelectBase<PX.Objects.AR.ARPayment, PXSelect<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.cATranID, Equal<Required<PX.Objects.AR.ARPayment.cATranID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) item.TranID
        })) ?? throw new Exception("Orig. Document Can Not Be Found"));
      case "GL":
        PX.Objects.GL.GLTran tran = PXResultset<PX.Objects.GL.GLTran>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTran, PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.module, Equal<Required<PX.Objects.GL.GLTran.module>>, And<PX.Objects.GL.GLTran.cATranID, Equal<Required<PX.Objects.GL.GLTran.cATranID>>>>>.Config>.Select((PXGraph) instance, new object[2]
        {
          (object) item.OrigModule,
          (object) item.TranID
        }));
        PX.Objects.CA.CARegister caRegister = tran != null ? CATrxRelease.CARegister(tran) : throw new Exception("Orig. Document Can Not Be Found");
        int? cashAccountID;
        if (GLCashTranIDAttribute.CheckGLTranCashAcc((PXGraph) instance, tran, out cashAccountID).GetValueOrDefault())
        {
          caRegister.CashAccountID = cashAccountID;
          return caRegister;
        }
        throw new PXException("Cash account doesn't exist for this branch, account and sub account ({0}, {1}, {2})", new object[3]
        {
          (object) ((PX.Objects.GL.Branch) PXSelectorAttribute.Select<PX.Objects.GL.GLTran.branchID>(((PXGraph) instance).Caches[typeof (PX.Objects.GL.GLTran)], (object) tran)).BranchCD,
          (object) ((PX.Objects.GL.Account) PXSelectorAttribute.Select<PX.Objects.GL.GLTran.accountID>(((PXGraph) instance).Caches[typeof (PX.Objects.GL.GLTran)], (object) tran)).AccountCD,
          (object) ((PX.Objects.GL.Sub) PXSelectorAttribute.Select<PX.Objects.GL.GLTran.subID>(((PXGraph) instance).Caches[typeof (PX.Objects.GL.GLTran)], (object) tran)).SubCD
        });
      case "CA":
        switch (item.OrigTranType)
        {
          case "CAE":
            return CATrxRelease.CARegister(PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelect<CAAdj, Where<CAAdj.tranID, Equal<Required<CAAdj.tranID>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) item.TranID
            })) ?? throw new Exception("Orig. Document Can Not Be Found"));
          case "CTI":
            return CATrxRelease.CARegister(PXResultset<CATransfer>.op_Implicit(PXSelectBase<CATransfer, PXSelect<CATransfer, Where<CATransfer.tranIDIn, Equal<Required<CATransfer.tranIDIn>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) item.TranID
            })) ?? throw new Exception("Orig. Document Can Not Be Found"), item);
          case "CTO":
            return CATrxRelease.CARegister(PXResultset<CATransfer>.op_Implicit(PXSelectBase<CATransfer, PXSelect<CATransfer, Where<CATransfer.tranIDOut, Equal<Required<CATransfer.tranIDOut>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) item.TranID
            })) ?? throw new Exception("Orig. Document Can Not Be Found"), item);
          default:
            throw new Exception("This CATran Orig. Document Type Not Defined");
        }
      default:
        throw new Exception("This CATran Orig. Document Type Not Defined");
    }
  }

  public static PX.Objects.CA.CARegister CARegister(CADeposit item)
  {
    return new PX.Objects.CA.CARegister()
    {
      TranID = item.TranID,
      Hold = item.Hold,
      Released = item.Released,
      Module = "CA",
      TranType = item.TranType,
      Description = item.TranDesc,
      FinPeriodID = item.FinPeriodID,
      DocDate = item.TranDate,
      ReferenceNbr = item.RefNbr,
      NoteID = item.NoteID,
      CashAccountID = item.CashAccountID,
      CuryID = item.CuryID,
      TranAmt = item.TranAmt,
      CuryTranAmt = item.CuryTranAmt
    };
  }

  public static PX.Objects.CA.CARegister CARegister(CAAdj item)
  {
    return new PX.Objects.CA.CARegister()
    {
      TranID = item.TranID,
      Hold = item.Hold,
      Released = item.Released,
      Module = "CA",
      TranType = item.AdjTranType,
      Description = item.TranDesc,
      FinPeriodID = item.FinPeriodID,
      DocDate = item.TranDate,
      ReferenceNbr = item.AdjRefNbr,
      NoteID = item.NoteID,
      CashAccountID = item.CashAccountID,
      CuryID = item.CuryID,
      TranAmt = item.TranAmt,
      CuryTranAmt = item.CuryTranAmt,
      BranchID = item.BranchID
    };
  }

  public static PX.Objects.CA.CARegister CARegister(PX.Objects.GL.GLTran item)
  {
    PX.Objects.CA.CARegister caRegister = new PX.Objects.CA.CARegister();
    caRegister.TranID = item.CATranID;
    caRegister.Hold = new bool?(!item.Released.GetValueOrDefault());
    caRegister.Released = item.Released;
    caRegister.Module = "GL";
    caRegister.TranType = item.TranType;
    caRegister.Description = item.TranDesc;
    caRegister.FinPeriodID = item.FinPeriodID;
    caRegister.DocDate = item.TranDate;
    caRegister.ReferenceNbr = item.RefNbr;
    caRegister.NoteID = item.NoteID;
    caRegister.CashAccountID = item.AccountID;
    Decimal? nullable = item.DebitAmt;
    Decimal? creditAmt = item.CreditAmt;
    caRegister.TranAmt = nullable.HasValue & creditAmt.HasValue ? new Decimal?(nullable.GetValueOrDefault() - creditAmt.GetValueOrDefault()) : new Decimal?();
    Decimal? curyDebitAmt = item.CuryDebitAmt;
    nullable = item.CuryCreditAmt;
    caRegister.CuryTranAmt = curyDebitAmt.HasValue & nullable.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?();
    return caRegister;
  }

  public static PX.Objects.CA.CARegister CARegister(PX.Objects.AR.ARPayment item)
  {
    return new PX.Objects.CA.CARegister()
    {
      TranID = item.CATranID,
      Hold = item.Hold,
      Released = item.Released,
      Module = "AR",
      TranType = item.DocType,
      Description = item.DocDesc,
      FinPeriodID = item.FinPeriodID,
      DocDate = item.DocDate,
      ReferenceNbr = item.RefNbr,
      NoteID = item.NoteID,
      CashAccountID = item.CashAccountID,
      CuryID = item.CuryID,
      TranAmt = item.DocBal,
      CuryTranAmt = item.DocBal
    };
  }

  public static PX.Objects.CA.CARegister CARegister(PX.Objects.AP.APPayment item)
  {
    return new PX.Objects.CA.CARegister()
    {
      TranID = item.CATranID,
      Hold = item.Hold,
      Released = item.Released,
      Module = "AP",
      TranType = item.DocType,
      Description = item.DocDesc,
      FinPeriodID = item.FinPeriodID,
      DocDate = item.DocDate,
      ReferenceNbr = item.RefNbr,
      NoteID = item.NoteID,
      CashAccountID = item.CashAccountID,
      CuryID = item.CuryID,
      TranAmt = item.DocBal,
      CuryTranAmt = item.DocBal
    };
  }

  public static PX.Objects.CA.CARegister CARegister(CATransfer item, CATran tran)
  {
    PX.Objects.CA.CARegister caRegister = new PX.Objects.CA.CARegister()
    {
      TranID = tran.TranID,
      Hold = item.Hold,
      Released = item.Released,
      Module = "CA",
      TranType = "CT%",
      Description = item.Descr,
      FinPeriodID = tran.FinPeriodID,
      DocDate = item.OutDate,
      ReferenceNbr = item.TransferNbr
    };
    caRegister.TranType = "CT%";
    caRegister.NoteID = item.NoteID;
    caRegister.CashAccountID = tran.CashAccountID;
    caRegister.CuryID = tran.CuryID;
    caRegister.CuryTranAmt = tran.CuryTranAmt;
    caRegister.TranAmt = tran.TranAmt;
    return caRegister;
  }

  public static void ReleaseDoc<TCADocument>(
    TCADocument _doc,
    int _item,
    List<PX.Objects.GL.Batch> externalPostList,
    SelectedEntityEvent<TCADocument> releaseEvent)
    where TCADocument : class, ICADocument, IBqlTable, new()
  {
    CAReleaseProcess instance1 = PXGraph.CreateInstance<CAReleaseProcess>();
    JournalEntry journalEntry = CATrxRelease.CreateJournalEntry();
    bool flag1 = externalPostList != null;
    List<PX.Objects.GL.Batch> batchlist = new List<PX.Objects.GL.Batch>();
    List<int> intList = new List<int>();
    bool flag2 = false;
    ((PXGraph) instance1).Clear();
    instance1.ReleaseDocProc<TCADocument>(journalEntry, ref batchlist, _doc, releaseEvent);
    for (int count = intList.Count; count < batchlist.Count; ++count)
      intList.Add(count);
    if (flag1)
    {
      if (instance1.AutoPost)
        externalPostList.AddRange((IEnumerable<PX.Objects.GL.Batch>) batchlist);
    }
    else
    {
      PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
      for (int index = 0; index < batchlist.Count; ++index)
      {
        PX.Objects.GL.Batch b = batchlist[index];
        try
        {
          if (instance1.AutoPost)
          {
            ((PXGraph) instance2).Clear();
            ((PXGraph) instance2).TimeStamp = b.tstamp;
            instance2.PostBatchProc(b);
          }
        }
        catch (Exception ex)
        {
          throw new PX.Objects.Common.PXMassProcessException(intList[index], ex);
        }
      }
    }
    if (flag2)
      throw new PXException("One or more documents could not be released.");
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CA.CARegister.branchID> e)
  {
  }

  /// <summary>CashAccount override - SQL Alias</summary>
  [PXHidden]
  [Serializable]
  public class CashAccount1 : CashAccount
  {
    public new abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CATrxRelease.CashAccount1.cashAccountID>
    {
    }
  }
}
