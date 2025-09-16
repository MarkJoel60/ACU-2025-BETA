// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseMaint_Extensions.CRCaseMaint_CRCreateReturnOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CR.CRCaseMaint_Extensions;

public class CRCaseMaint_CRCreateReturnOrder : PX.Objects.CR.Extensions.CRCreateReturnOrder.CRCreateReturnOrder<CRCaseMaint, CRCase>
{
  public static bool IsActive() => PX.Objects.CR.Extensions.CRCreateReturnOrder.CRCreateReturnOrder<CRCaseMaint, CRCase>.IsExtensionActive();

  public virtual void _(PX.Data.Events.RowSelected<CRCase> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.CreateReturnOrder).SetEnabled(!(e.Row.Status == "C") && !(e.Row.Status == "R") && e.Row.IsActive.GetValueOrDefault());
    if (PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXViewOf<PX.Objects.AR.Customer>.BasedOn<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOOrder.FK.Customer>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.behavior, Equal<SOBehavior.rM>>>>, And<BqlOperand<PX.Objects.SO.SOOrder.status, IBqlString>.IsNotEqual<SOOrderStatus.completed>>>, And<BqlOperand<PX.Objects.SO.SOOrder.status, IBqlString>.IsNotEqual<SOOrderStatus.cancelled>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.status, IBqlString>.IsNotEqual<SOOrderStatus.voided>>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) e.Row.CustomerID
    }))?.AcctCD == null)
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache.RaiseExceptionHandling<CRCase.customerID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("This customer already has at least one open return order.", (PXErrorLevel) 2));
  }

  public override PX.Objects.SO.SOOrder FillSalesOrder(
    SOOrderEntry soGraph,
    CRCase document,
    PX.Objects.SO.SOOrder salesOrder)
  {
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, document.CustomerID);
    salesOrder.CuryID = customer.CuryID;
    salesOrder.OrderDate = ((PXGraph) this.Base).Accessinfo.BusinessDate;
    salesOrder.OrderDesc = document.Subject;
    salesOrder.TermsID = customer.TermsID;
    salesOrder.CustomerID = document.CustomerID;
    salesOrder.CustomerLocationID = document.LocationID ?? customer.DefLocationID;
    int? nullable = document.ContactID;
    if (!nullable.HasValue)
    {
      if (customer.PrimaryContactID.HasValue)
      {
        nullable = customer.PrimaryContactID;
      }
      else
      {
        List<object> list = ((IQueryable<PXResult<PX.Objects.CR.Contact>>) PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<CRCase.customerID>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Select<PXResult<PX.Objects.CR.Contact>, object>((Expression<Func<PXResult<PX.Objects.CR.Contact>, object>>) (res => res.get_Item(0))).ToList<object>();
        if (list.Count == 1)
          nullable = ((PX.Objects.CR.Contact) list[0]).ContactID;
      }
    }
    salesOrder.ContactID = nullable;
    salesOrder = ((PXSelectBase<PX.Objects.SO.SOOrder>) soGraph.Document).Update(salesOrder);
    ((PXSelectBase<PX.Objects.AR.Customer>) soGraph.customer).Current.CreditRule = customer.CreditRule;
    return salesOrder;
  }

  public override CRRelation FillRelations(
    SOOrderEntry soGraph,
    CRCase document,
    PX.Objects.SO.SOOrder salesOrder)
  {
    SOOrderEntry_CRRelationDetailsExt extension = ((PXGraph) soGraph).GetExtension<SOOrderEntry_CRRelationDetailsExt>();
    CRRelation crRelation = ((PXSelectBase<CRRelation>) extension.Relations).Insert();
    crRelation.RefNoteID = salesOrder.NoteID;
    crRelation.RefEntityType = ((object) salesOrder).GetType().FullName;
    crRelation.Role = "SR";
    crRelation.TargetType = "PX.Objects.CR.CRCase";
    crRelation.TargetNoteID = document.NoteID;
    crRelation.DocNoteID = document.NoteID;
    crRelation.EntityID = document.CustomerID;
    crRelation.ContactID = document.ContactID;
    return ((PXSelectBase<CRRelation>) extension.Relations).Update(crRelation);
  }

  public override bool CheckBAccountStateBeforeConvert()
  {
    PX.Objects.CR.BAccount baccount = PX.Objects.CR.BAccount.PK.Find((PXGraph) this.Base, ((PXSelectBase<CRCase>) this.Base.CaseCurrent).Current.CustomerID);
    return baccount != null && !(baccount.Type == "PR");
  }
}
