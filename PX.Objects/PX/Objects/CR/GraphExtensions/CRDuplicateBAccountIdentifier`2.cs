// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.GraphExtensions.CRDuplicateBAccountIdentifier`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.GraphExtensions;

public abstract class CRDuplicateBAccountIdentifier<TGraph, TMain> : PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable cancel(PXAdapter a)
  {
    CRDuplicateBAccountIdentifier<TGraph, TMain> baccountIdentifier = this;
    foreach (object obj in ((PXAction) new PXCancel<TMain>((PXGraph) baccountIdentifier.Base, "Cancel")).Press(a))
    {
      BAccount baccount = PXResult.Unwrap<BAccount>(obj);
      if (baccount != null && baccountIdentifier.Base.Caches[typeof (BAccount)].GetStatus((object) baccount) == 2)
      {
        string str1 = "";
        foreach (PXResult<BAccountItself> pxResult in PXSelectBase<BAccountItself, PXSelectReadonly<BAccountItself, Where<BAccount.acctCD, Equal<Required<BAccount.acctCD>>, And<BAccount.bAccountID, NotEqual<Required<BAccount.bAccountID>>>>>.Config>.Select((PXGraph) baccountIdentifier.Base, new object[2]
        {
          (object) baccount.AcctCD,
          (object) baccount.BAccountID
        }))
        {
          BAccountItself baccountItself = PXResult<BAccountItself>.op_Implicit(pxResult);
          str1 = $"{str1}{baccountItself.Type}, ";
        }
        if (!Str.IsNullOrEmpty(str1))
        {
          string str2 = str1.Trim(',', ' ');
          baccountIdentifier.Base.Caches[typeof (BAccount)].RaiseExceptionHandling<BAccount.acctCD>((object) baccount, (object) null, (Exception) new PXSetPropertyException("The {0} identifier is already used for the following records: {1}.", new object[2]
          {
            (object) baccount.AcctCD,
            (object) str2
          }));
        }
      }
      yield return obj;
    }
  }
}
