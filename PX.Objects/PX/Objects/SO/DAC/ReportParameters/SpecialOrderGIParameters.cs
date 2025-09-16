// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.ReportParameters.SpecialOrderGIParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.SO.DAC.ReportParameters;

/// <exclude />
[PXVirtual]
[PXCacheName("Special Order GI Parameters")]
public class SpecialOrderGIParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, IsDBField = false)]
  [SpecialOrderGIParameters.branchID.Branch]
  public virtual int? BranchID { get; set; }

  [PXString(2, IsKey = true, IsFixed = true)]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<SOOrderType.behavior, In3<SOBehavior.sO, SOBehavior.rM, SOBehavior.qT>>, OrderBy<Desc<SOOrderType.orderType>>>), Filterable = true)]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXString(15, IsKey = true, IsUnicode = true)]
  [PXSelector(typeof (Search2<PX.Objects.SO.SOOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<KeysRelation<Field<PX.Objects.SO.SOOrder.customerID>.IsRelatedTo<PX.Objects.AR.Customer.bAccountID>.AsSimpleKey.WithTablesOf<PX.Objects.AR.Customer, PX.Objects.SO.SOOrder>, PX.Objects.AR.Customer, PX.Objects.SO.SOOrder>.And<Where<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<SpecialOrderGIParameters.orderType>>, And<Exists<Select<SOLine, Where<SOLine.isSpecialOrder, Equal<True>, And<SOLine.FK.Order>>>>>>, OrderBy<Desc<PX.Objects.SO.SOOrder.orderNbr>>>), Filterable = true)]
  public virtual string OrderNbr { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SpecialOrderGIParameters.branchID>
  {
    [PXInternalUseOnly]
    public class BranchAttribute : PXEventSubscriberAttribute
    {
      public virtual void CacheAttached(PXCache sender)
      {
        base.CacheAttached(sender);
        this.HideTheFieldIfFeatureDisabled(sender.Graph);
      }

      protected virtual void HideTheFieldIfFeatureDisabled(PXGraph graph)
      {
        if (!typeof (PXGenericInqGrph).IsAssignableFrom(graph.GetType()) || !((Dictionary<string, PXView>) graph.Views).ContainsKey("Filter"))
          return;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        graph.FieldSelecting.AddHandler("Filter", "Branch", SpecialOrderGIParameters.branchID.BranchAttribute.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (SpecialOrderGIParameters.branchID.BranchAttribute.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXFieldSelecting((object) SpecialOrderGIParameters.branchID.BranchAttribute.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CHideTheFieldIfFeatureDisabled\u003Eb__1_0))));
      }
    }
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SpecialOrderGIParameters.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SpecialOrderGIParameters.orderNbr>
  {
  }
}
