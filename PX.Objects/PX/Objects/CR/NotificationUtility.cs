// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.NotificationUtility
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PM;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.CR;

public sealed class NotificationUtility
{
  private readonly PXGraph _graph;

  public NotificationUtility(PXGraph graph) => this._graph = graph;

  public (NotificationSetup SetupWithBranch, NotificationSetup SetupWithoutBranch) SearchSetup(
    string source,
    string notificationCD,
    int? branchID)
  {
    if (source == null || notificationCD == null)
      return ((NotificationSetup) null, (NotificationSetup) null);
    NotificationSetup notificationSetup1 = (NotificationSetup) null;
    NotificationSetup notificationSetup2 = (NotificationSetup) null;
    foreach (PXResult<NotificationSetup> pxResult in PXSelectBase<NotificationSetup, PXSelect<NotificationSetup, Where<NotificationSetup.sourceCD, Equal<Required<NotificationSetup.sourceCD>>, And<NotificationSetup.notificationCD, Equal<Required<NotificationSetup.notificationCD>>, And2<Where<NotificationSetup.nBranchID, IsNull, Or<NotificationSetup.nBranchID, Equal<Required<NotificationSetup.nBranchID>>>>, And<NotificationSetup.active, Equal<True>>>>>>.Config>.SelectWindowed(this._graph, 0, 2, new object[3]
    {
      (object) source,
      (object) notificationCD,
      (object) branchID
    }))
    {
      NotificationSetup notificationSetup3 = PXResult<NotificationSetup>.op_Implicit(pxResult);
      if (notificationSetup3.NBranchID.HasValue)
        notificationSetup1 = notificationSetup3;
      else
        notificationSetup2 = notificationSetup3;
    }
    return (notificationSetup1, notificationSetup2);
  }

  /// <summary>
  /// Search for report ID in Notification settings for particular entity. To use if entity is not suported by <see cref="M:PX.Objects.CR.NotificationUtility.SearchReport``1(System.String,System.Nullable{System.Int32},System.Nullable{System.Int32})" />
  /// </summary>
  /// <typeparam name="TSourceEntity">Entity to search notifaction settings for</typeparam>
  /// <typeparam name="TNoteField">Note field of the entity</typeparam>
  /// <typeparam name="TIDField">ID field of the entity to search it by</typeparam>
  /// <param name="source">entity name to search it in NotificationSetup table (usualy equals to cluss name) </param>
  /// <param name="reportID">Default report ID</param>
  /// <param name="entityID">Value in column TIDField to search by</param>
  /// <param name="branchID">Branch to search by</param>
  /// <returns>Replacement ReportID specified in Notification settings</returns>
  public string SearchReport<TSourceEntity, TNoteField, TIDField>(
    string source,
    string reportID,
    int? entityID,
    int? branchID)
    where TSourceEntity : class, IBqlTable, new()
    where TNoteField : class, IBqlField
    where TIDField : class, IBqlField
  {
    NotificationSetup setup = this.GetSetup(source, reportID, branchID);
    if (setup == null)
      return reportID;
    return this.ChooseReportIDFromNotificationSource(GraphHelper.RowCast<NotificationSource>((IEnumerable) PXSelectBase<TSourceEntity, PXSelectJoin<TSourceEntity, LeftJoin<NotificationSource, On<TNoteField, Equal<NotificationSource.refNoteID>>>, Where<TIDField, Equal<Required<TIDField>>, And<NotificationSource.setupID, Equal<Required<NotificationSource.setupID>>>>>.Config>.Select(this._graph, new object[2]
    {
      (object) entityID,
      (object) setup.SetupID
    })), branchID, reportID);
  }

  /// <summary>
  /// Search for report ID in Notification settings for particular entity. To use if entity is not suported by <see cref="M:PX.Objects.CR.NotificationUtility.SearchReport``1(System.String,System.Nullable{System.Int32},System.Nullable{System.Int32})" />
  /// </summary>
  /// <typeparam name="TSourceEntity">Entity to search notifaction settings for</typeparam>
  /// <typeparam name="TNoteField">Note field of the entity</typeparam>
  /// <typeparam name="TIDField">ID field of the entity to search it by</typeparam>
  /// <typeparam name="TClassField">Class ID field of the entity (class is alternative sdource for the setting)</typeparam>
  /// <param name="source">entity name to search it in NotificationSetup table (usualy equals to cluss name) </param>
  /// <param name="reportID">Default report ID</param>
  /// <param name="entityID">Value in column TIDField to search by</param>
  /// <param name="branchID">Branch to search by</param>
  /// <returns>Replacement ReportID specified in Notification settings</returns>
  public string SearchReport<TSourceEntity, TNoteField, TIDField, TClassField>(
    string source,
    string reportID,
    int? entityID,
    int? branchID)
    where TSourceEntity : class, IBqlTable, new()
    where TNoteField : class, IBqlField
    where TIDField : class, IBqlField
    where TClassField : class, IBqlField
  {
    NotificationSetup setup = this.GetSetup(source, reportID, branchID);
    if (setup == null)
      return reportID;
    return this.ChooseReportIDFromNotificationSource(GraphHelper.RowCast<NotificationSource>((IEnumerable) PXSelectBase<TSourceEntity, PXSelectJoin<TSourceEntity, LeftJoin<NotificationSource, On<TNoteField, Equal<NotificationSource.refNoteID>, Or<TClassField, Equal<NotificationSource.classID>>>>, Where<TIDField, Equal<Required<TIDField>>, And<NotificationSource.setupID, Equal<Required<NotificationSource.setupID>>>>>.Config>.Select(this._graph, new object[2]
    {
      (object) entityID,
      (object) setup.SetupID
    })), branchID, reportID);
  }

  /// <summary>
  /// Search for report ID in Notification settings for particular entity
  /// </summary>
  /// <typeparam name="TSourceEntity">Entity to search notifaction settings for. Project, Baccount, Vendor and Customer are currently supported. Use other overloads for other classes</typeparam>
  /// <param name="entityID">Value in column TIDField to search by</param>
  /// <param name="branchID">Branch to search by</param>
  /// <returns>Replacement ReportID specified in Notification settings</returns>
  internal string SearchReport<TSourceEntity>(string reportID, int? entityID, int? branchID) where TSourceEntity : class, IBqlTable, new()
  {
    System.Type type = typeof (TSourceEntity);
    if ((object) type != null)
    {
      if (type == typeof (PMProject))
        return this.SearchProjectReport(reportID, entityID, branchID);
      if (type == typeof (PX.Objects.AR.Customer))
        return this.SearchCustomerReport(reportID, entityID, branchID);
      if (type == typeof (PX.Objects.AP.Vendor))
        return this.SearchVendorReport(reportID, entityID, branchID);
    }
    return this.SearchReport<TSourceEntity, BAccount.noteID, BAccount.bAccountID>(nameof (TSourceEntity), reportID, entityID, branchID);
  }

  public string SearchVendorReport(string reportID, int? vendorID, int? branchID)
  {
    return this.SearchReport<PX.Objects.AP.Vendor, PX.Objects.AP.Vendor.noteID, PX.Objects.AP.Vendor.bAccountID, PX.Objects.AP.Vendor.vendorClassID>("Vendor", reportID, vendorID, branchID);
  }

  public string SearchCustomerReport(string reportID, int? customerID, int? branchID)
  {
    return this.SearchReport<PX.Objects.AR.Light.Customer, PX.Objects.AR.Light.BAccount.noteID, PX.Objects.AR.Light.Customer.bAccountID, PX.Objects.AR.Light.Customer.customerClassID>("Customer", reportID, customerID, branchID);
  }

  public string SearchProjectReport(string reportID, int? projectID, int? branchID)
  {
    return this.SearchReport<PMProject, PMProject.noteID, PMProject.contractID>("Project", reportID, projectID, branchID);
  }

  internal string ChooseReportIDFromNotificationSource(
    IEnumerable<NotificationSource> notificationSources,
    int? branchID,
    string originalReportID)
  {
    return this.ChooseNotificationSource(notificationSources, branchID)?.ReportID ?? originalReportID;
  }

  private NotificationSource ChooseNotificationSource(
    IEnumerable<NotificationSource> notificationSources,
    int? branchID)
  {
    return notificationSources.FirstOrDefault<NotificationSource>((Func<NotificationSource, bool>) (_ =>
    {
      if (_.ClassID != null)
        return false;
      int? nbranchId = _.NBranchID;
      int? nullable = branchID;
      return nbranchId.GetValueOrDefault() == nullable.GetValueOrDefault() & nbranchId.HasValue == nullable.HasValue;
    })) ?? notificationSources.FirstOrDefault<NotificationSource>((Func<NotificationSource, bool>) (_ => _.ClassID == null && !_.NBranchID.HasValue)) ?? notificationSources.FirstOrDefault<NotificationSource>((Func<NotificationSource, bool>) (_ =>
    {
      if (_.ClassID == null)
        return false;
      int? nbranchId = _.NBranchID;
      int? nullable = branchID;
      return nbranchId.GetValueOrDefault() == nullable.GetValueOrDefault() & nbranchId.HasValue == nullable.HasValue;
    })) ?? notificationSources.FirstOrDefault<NotificationSource>((Func<NotificationSource, bool>) (_ => _.ClassID != null && !_.NBranchID.HasValue));
  }

  public Guid? SearchPrinter(string source, string reportID, int? branchID)
  {
    NotificationSetupUserOverride setupUserOverride = PXResultset<NotificationSetupUserOverride>.op_Implicit(PXSelectBase<NotificationSetupUserOverride, PXViewOf<NotificationSetupUserOverride>.BasedOn<SelectFromBase<NotificationSetupUserOverride, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<NotificationSetup>.On<NotificationSetupUserOverride.FK.DefaultSetup>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetupUserOverride.userID, Equal<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>, And<BqlOperand<NotificationSetupUserOverride.active, IBqlBool>.IsEqual<True>>>, And<BqlOperand<NotificationSetupUserOverride.shipVia, IBqlString>.IsNull>>, And<BqlOperand<NotificationSetup.active, IBqlBool>.IsEqual<True>>>, And<BqlOperand<NotificationSetup.sourceCD, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<NotificationSetup.reportID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.nBranchID, Equal<P.AsInt>>>>>.Or<BqlOperand<NotificationSetup.nBranchID, IBqlInt>.IsNull>>>.Order<By<BqlField<NotificationSetup.nBranchID, IBqlInt>.Desc>>>.Config>.Select(this._graph, new object[3]
    {
      (object) source,
      (object) reportID,
      (object) branchID
    }));
    Guid? nullable;
    if (setupUserOverride != null)
    {
      nullable = setupUserOverride.DefaultPrinterID;
      if (nullable.HasValue)
        return setupUserOverride.DefaultPrinterID;
    }
    UserPreferences userPreferences = PXResultset<UserPreferences>.op_Implicit(PXSelectBase<UserPreferences, PXViewOf<UserPreferences>.BasedOn<SelectFromBase<UserPreferences, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<UserPreferences.userID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select(this._graph, new object[1]
    {
      (object) this._graph.Accessinfo.UserID
    }));
    if (userPreferences != null)
    {
      nullable = userPreferences.DefaultPrinterID;
      if (nullable.HasValue)
        return userPreferences.DefaultPrinterID;
    }
    if (source != null && reportID != null)
    {
      NotificationSetup setup = this.GetSetup(source, reportID, branchID);
      if (setup != null)
      {
        nullable = setup.DefaultPrinterID;
        if (nullable.HasValue)
          return setup.DefaultPrinterID;
      }
    }
    PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(this._graph, new object[1]
    {
      (object) (branchID ?? this._graph.Accessinfo.BranchID)
    }));
    if (branch != null)
    {
      nullable = branch.DefaultPrinterID;
      if (nullable.HasValue)
        return branch.DefaultPrinterID;
      PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXViewOf<PX.Objects.GL.DAC.Organization>.BasedOn<SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(this._graph, new object[1]
      {
        (object) branch.OrganizationID
      }));
      if (organization != null)
        return organization.DefaultPrinterID;
    }
    nullable = new Guid?();
    return nullable;
  }

  public NotificationSetup GetSetup(string source, string reportID, int? branchID)
  {
    return PXResultset<NotificationSetup>.op_Implicit(PXSelectBase<NotificationSetup, PXViewOf<NotificationSetup>.BasedOn<SelectFromBase<NotificationSetup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.active, Equal<True>>>>, And<BqlOperand<NotificationSetup.sourceCD, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<NotificationSetup.reportID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<NotificationSetup.shipVia, IBqlString>.IsNull>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.nBranchID, Equal<P.AsInt>>>>>.Or<BqlOperand<NotificationSetup.nBranchID, IBqlInt>.IsNull>>>.Order<By<BqlField<NotificationSetup.nBranchID, IBqlInt>.Desc>>>.Config>.SelectWindowed(this._graph, 0, 1, new object[3]
    {
      (object) source,
      (object) reportID,
      (object) branchID
    }));
  }

  public NotificationSource GetSource(NotificationSetup setup)
  {
    return new NotificationSource()
    {
      Active = setup.Active,
      EMailAccountID = setup.EMailAccountID,
      ReportID = setup.ReportID,
      NotificationID = setup.NotificationID,
      Format = setup.Format,
      SetupID = setup.SetupID,
      NBranchID = setup.NBranchID
    };
  }

  public NotificationSource GetSource(
    string sourceType,
    object row,
    IList<Guid?> setupIDs,
    int? branchID)
  {
    if (row == null)
      return (NotificationSource) null;
    PXGraph graph = this.CreatePrimaryGraph(sourceType, row);
    NotificationUtility.NavigateRow(graph, row);
    PXView pxView = (PXView) null;
    ((Dictionary<string, PXView>) graph.Views).TryGetValue("NotificationSources", out pxView);
    if (pxView == null)
    {
      using (IEnumerator<PXView> enumerator = graph.GetViewNames().Select<string, PXView>((Func<string, PXView>) (name => graph.Views[name])).Where<PXView>((Func<PXView, bool>) (view => typeof (NotificationSource).IsAssignableFrom(view.GetItemType()))).GetEnumerator())
      {
        if (enumerator.MoveNext())
          pxView = enumerator.Current;
      }
    }
    if (pxView == null)
      return (NotificationSource) null;
    NotificationSource source1 = (NotificationSource) null;
    foreach (NotificationSource source2 in GraphHelper.RowCast<NotificationSource>((IEnumerable) pxView.SelectMulti(Array.Empty<object>())))
    {
      bool? active = source2.Active;
      bool flag = false;
      if (!(active.GetValueOrDefault() == flag & active.HasValue))
      {
        Guid? setupId = source2.SetupID;
        if (setupId.HasValue)
        {
          IList<Guid?> nullableList = setupIDs;
          setupId = source2.SetupID;
          Guid? nullable = new Guid?(setupId.Value);
          if (!nullableList.Contains(nullable))
            continue;
        }
        int? nbranchId = source2.NBranchID;
        int? nullable1 = branchID;
        if (nbranchId.GetValueOrDefault() == nullable1.GetValueOrDefault() & nbranchId.HasValue == nullable1.HasValue)
          return source2;
        nullable1 = source2.NBranchID;
        if (!nullable1.HasValue)
          source1 = source2;
      }
    }
    return source1;
  }

  protected List<object> GetDefaultRecipients(PXCache cache, NotificationSource source)
  {
    List<object> defaultRecipients = new List<object>();
    foreach (PXResult<NotificationSetupRecipient> pxResult in PXSelectBase<NotificationSetupRecipient, PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Required<NotificationSetupRecipient.setupID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) source.SetupID
    }))
    {
      NotificationSetupRecipient notificationSetupRecipient = PXResult<NotificationSetupRecipient>.op_Implicit(pxResult);
      try
      {
        NotificationRecipient instance = (NotificationRecipient) cache.CreateInstance();
        instance.SetupID = source.SetupID;
        instance.ContactType = notificationSetupRecipient.ContactType;
        instance.ContactID = notificationSetupRecipient.ContactID;
        instance.Active = notificationSetupRecipient.Active;
        instance.AddTo = notificationSetupRecipient.AddTo;
        instance.Format = notificationSetupRecipient.Format;
        defaultRecipients.Add((object) instance);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
      }
    }
    return defaultRecipients;
  }

  public RecipientList GetRecipients(string type, object row, NotificationSource source)
  {
    if (row == null)
      return (RecipientList) null;
    PXGraph graph = this.CreatePrimaryGraph(type, row);
    NotificationUtility.NavigateRow(graph, row);
    NotificationUtility.NavigateRow(graph, (object) source, false);
    PXView current;
    ((Dictionary<string, PXView>) graph.Views).TryGetValue("NotificationRecipients", out current);
    if (current == null)
    {
      using (IEnumerator<PXView> enumerator = graph.GetViewNames().Select<string, PXView>((Func<string, PXView>) (name => graph.Views[name])).Where<PXView>((Func<PXView, bool>) (view => typeof (NotificationRecipient).IsAssignableFrom(view.GetItemType()))).GetEnumerator())
      {
        if (enumerator.MoveNext())
          current = enumerator.Current;
      }
    }
    if (current == null)
      return (RecipientList) null;
    RecipientList recipients = (RecipientList) null;
    Dictionary<string, string> source1 = new Dictionary<string, string>();
    int num = 0;
    List<object> objectList = current.SelectMulti(Array.Empty<object>());
    if (!NonGenericIEnumerableExtensions.Any_((IEnumerable) objectList))
      objectList = this.GetDefaultRecipients(graph.Caches[typeof (NotificationRecipient)], source);
    foreach (NotificationRecipient row1 in objectList)
    {
      NotificationUtility.NavigateRow(graph, (object) row1, false);
      if (row1.Active.GetValueOrDefault())
      {
        ++num;
        if (string.IsNullOrWhiteSpace(row1.Email))
        {
          string email = ((NotificationRecipient) graph.Caches[typeof (NotificationRecipient)].Current).Email;
          if (string.IsNullOrWhiteSpace(email))
          {
            Contact contact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Current<NotificationRecipient.contactID>>>>.Config>.SelectSingleBound(this._graph, new object[1]
            {
              (object) row1
            }, Array.Empty<object>()));
            StringBuilder stringBuilder = new StringBuilder(new NotificationContactType.ListAttribute().ValueLabelDic[row1.ContactType]);
            if (contact != null)
            {
              stringBuilder.Append(" ");
              stringBuilder.Append(contact.DisplayName);
            }
            source1.Add(num.ToString((IFormatProvider) CultureInfo.InvariantCulture), PXMessages.LocalizeFormatNoPrefix("Recipient '{0}': Email is empty.", new object[1]
            {
              (object) stringBuilder
            }));
          }
          else
            row1.Email = email;
        }
        if (!string.IsNullOrWhiteSpace(row1.Email))
        {
          if (recipients == null)
            recipients = new RecipientList();
          recipients.Add(row1);
        }
      }
    }
    if (source1.Any<KeyValuePair<string, string>>())
    {
      NotificationSetup notificationSetup = PXResultset<NotificationSetup>.op_Implicit(PXSelectBase<NotificationSetup, PXSelect<NotificationSetup, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>.Config>.SelectSingleBound(this._graph, new object[1]
      {
        (object) source
      }, Array.Empty<object>()));
      throw new PXOuterException(source1, this._graph.GetType(), row, "{0} of {1} recipients are invalid in notification'{2}', module {3}.", new object[4]
      {
        (object) source1.Count,
        (object) num,
        (object) notificationSetup.NotificationCD,
        (object) notificationSetup.Module
      });
    }
    return recipients;
  }

  private PXGraph CreatePrimaryGraph(string source, object row)
  {
    System.Type type;
    switch (source)
    {
      case "Customer":
        type = typeof (CustomerMaint);
        break;
      case "Vendor":
        type = typeof (VendorMaint);
        if (row != null && this._graph.Caches[row.GetType()].GetValue<BAccount.type>(row) as string == "EP")
        {
          type = typeof (EmployeeMaint);
          break;
        }
        break;
      case "BAccount":
        type = typeof (BusinessAccountMaint);
        break;
      default:
        type = new EntityHelper(this._graph).GetPrimaryGraphType(row, false);
        break;
    }
    if (type == (System.Type) null)
      throw new PXException("There is no primary graph to process the operation.");
    return !(type == this._graph.GetType()) ? PXGraph.CreateInstance(type) : this._graph;
  }

  private static void NavigateRow(PXGraph graph, object row, bool primaryView = true)
  {
    System.Type type = row.GetType();
    PXCache cache = graph.Views[graph.PrimaryView].Cache;
    if (cache.GetItemType().IsAssignableFrom(row.GetType()))
    {
      graph.Caches[type].Current = row;
      graph.Caches[cache.GetItemType()].Current = row;
    }
    else if (row.GetType().IsAssignableFrom(cache.GetItemType()))
    {
      object instance = cache.CreateInstance();
      ((PXCache) Activator.CreateInstance(typeof (PXCache<>).MakeGenericType(row.GetType()), (object) cache.Graph)).RestoreCopy(instance, row);
      cache.Current = instance;
    }
    else if (primaryView)
    {
      object[] objArray = new object[cache.Keys.Count];
      string[] strArray = new string[cache.Keys.Count];
      for (int index = 0; index < cache.Keys.Count; ++index)
      {
        objArray[index] = graph.Caches[type].GetValue(row, cache.Keys[index]);
        strArray[index] = cache.Keys[index];
      }
      int num1 = 0;
      int num2 = 0;
      List<object> objectList = graph.Views[graph.PrimaryView].Select((object[]) null, (object[]) null, objArray, strArray, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2);
      graph.Views[graph.PrimaryView].Cache.Current = objectList == null || objectList.Count <= 0 ? (object) (IBqlTable) null : (object) PXResult.Unwrap(objectList[0], graph.Views[graph.PrimaryView].Cache.GetItemType());
    }
    else
    {
      PXCache cach = graph.Caches[type];
      object instance = cach.CreateInstance();
      ((PXCache) Activator.CreateInstance(typeof (PXCache<>).MakeGenericType(type), (object) cach.Graph)).RestoreCopy(instance, row);
      cach.Current = instance;
    }
  }
}
