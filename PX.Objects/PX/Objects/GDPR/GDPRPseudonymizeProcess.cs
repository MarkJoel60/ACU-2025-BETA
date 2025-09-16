// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.GDPRPseudonymizeProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GDPR;

public class GDPRPseudonymizeProcess : GDPRPseudonymizeProcessBase
{
  public 
  #nullable disable
  PXCancel<GDPRPseudonymizeProcess.ObfuscateType> Cancel;
  public PXAction<GDPRPseudonymizeProcess.ObfuscateType> OpenContact;
  public PXFilter<GDPRPseudonymizeProcess.ObfuscateType> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<GDPRPseudonymizeProcess.ObfuscateEntity, GDPRPseudonymizeProcess.ObfuscateType> SelectedItems;

  public GDPRPseudonymizeProcess()
  {
    this.GetPseudonymizationStatus = typeof (PXPseudonymizationStatusListAttribute.notPseudonymized);
    this.SetPseudonymizationStatus = 1;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<GDPRPseudonymizeProcess.ObfuscateEntity>) this.SelectedItems).SetProcessDelegate(GDPRPseudonymizeProcess.\u003C\u003Ec.\u003C\u003E9__2_0 ?? (GDPRPseudonymizeProcess.\u003C\u003Ec.\u003C\u003E9__2_0 = new PXProcessingBase<GDPRPseudonymizeProcess.ObfuscateEntity>.ProcessListDelegate((object) GDPRPseudonymizeProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__2_0))));
    ((PXProcessingBase<GDPRPseudonymizeProcess.ObfuscateEntity>) this.SelectedItems).SetSelected<GDPRPseudonymizeProcess.ObfuscateEntity.selected>();
    ((PXProcessing<GDPRPseudonymizeProcess.ObfuscateEntity>) this.SelectedItems).SetProcessCaption("Pseudonymize");
    ((PXProcessing<GDPRPseudonymizeProcess.ObfuscateEntity>) this.SelectedItems).SetProcessAllCaption("Pseudonymize All");
  }

  [PXButton]
  [PXUIField(DisplayName = "Open Contact", Visible = false)]
  public virtual IEnumerable openContact(PXAdapter adapter)
  {
    if (!(((PXGraph) this).Caches[typeof (GDPRPseudonymizeProcess.ObfuscateEntity)].Current is GDPRPseudonymizeProcess.ObfuscateEntity current))
      return adapter.Get();
    if (current.Deleted.GetValueOrDefault())
      throw new PXSetPropertyException("A deleted contact cannot be opened.");
    foreach (IBqlTable ibqlTable in GDPRPseudonymizeProcess.RemapToPrimary((IEnumerable<GDPRPseudonymizeProcess.ObfuscateEntity>) new List<GDPRPseudonymizeProcess.ObfuscateEntity>()
    {
      current
    }))
    {
      if (ibqlTable is Contact)
        PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<ContactMaint>(), (object) ibqlTable, (PXRedirectHelper.WindowMode) 1);
      else if (ibqlTable is CRContact)
      {
        CROpportunity crOpportunity = ((PXSelectBase<CROpportunity>) new PXSelect<CROpportunity, Where<CROpportunity.opportunityContactID, Equal<Required<CRContact.contactID>>>>((PXGraph) this)).SelectSingle(new object[1]
        {
          (object) (ibqlTable as CRContact).ContactID
        });
        PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<OpportunityMaint>(), (object) crOpportunity, (PXRedirectHelper.WindowMode) 1);
      }
    }
    return adapter.Get();
  }

  public virtual IEnumerable selectedItems()
  {
    List<GDPRPseudonymizeProcess.ObfuscateEntity> obfuscateEntityList = new List<GDPRPseudonymizeProcess.ObfuscateEntity>();
    using (new PXReadDeletedScope(false))
    {
      List<string> fields = new List<string>()
      {
        "BAccount__AcctCD",
        "DisplayName",
        "FirstName",
        "MidName",
        "LastName",
        "Salutation",
        "FullName",
        "EMail",
        "WebSite",
        "Fax",
        "Phone1",
        "Phone2",
        "Phone3"
      };
      foreach (PXResult<Contact, BAccount> selectContact in this.SelectContacts(fields))
      {
        Contact contact = PXResult<Contact, BAccount>.op_Implicit(selectContact);
        BAccount baccount = PXResult<Contact, BAccount>.op_Implicit(selectContact);
        obfuscateEntityList.Add(new GDPRPseudonymizeProcess.ObfuscateEntity()
        {
          ContactID = contact.ContactID,
          ContactType = contact.ContactType,
          AcctCD = baccount.AcctCD,
          DisplayName = contact.DisplayName,
          MidName = contact.MidName,
          LastName = contact.LastName,
          Salutation = contact.Salutation,
          FullName = contact.FullName,
          Email = contact.EMail,
          WebSite = contact.WebSite,
          Fax = contact.Fax,
          Phone1 = contact.Phone1,
          Phone2 = contact.Phone2,
          Phone3 = contact.Phone3,
          Deleted = contact.DeletedDatabaseRecord
        });
      }
      foreach (PXResult<CRContact, BAccount> selectOpportunity in this.SelectOpportunities(fields))
      {
        CRContact crContact = PXResult<CRContact, BAccount>.op_Implicit(selectOpportunity);
        BAccount baccount = PXResult<CRContact, BAccount>.op_Implicit(selectOpportunity);
        obfuscateEntityList.Add(new GDPRPseudonymizeProcess.ObfuscateEntity()
        {
          ContactID = crContact.ContactID,
          ContactType = "OP",
          AcctCD = baccount.AcctCD,
          DisplayName = crContact.DisplayName,
          MidName = crContact.MidName,
          LastName = crContact.LastName,
          Salutation = crContact.Salutation,
          FullName = crContact.FullName,
          Email = crContact.Email,
          WebSite = crContact.WebSite,
          Fax = crContact.Fax,
          Phone1 = crContact.Phone1,
          Phone2 = crContact.Phone2,
          Phone3 = crContact.Phone3
        });
      }
      foreach (GDPRPseudonymizeProcess.ObfuscateEntity obfuscateEntity in obfuscateEntityList)
      {
        object obj = ((PXSelectBase) this.SelectedItems).Cache.Locate((object) obfuscateEntity);
        if (obj != null)
        {
          yield return obj;
        }
        else
        {
          ((PXSelectBase) this.SelectedItems).Cache.SetStatus((object) obfuscateEntity, (PXEntryStatus) 5);
          yield return (object) obfuscateEntity;
        }
      }
    }
    ((PXSelectBase) this.SelectedItems).Cache.IsDirty = false;
  }

  protected virtual IEnumerable SelectContacts(List<string> fields)
  {
    GDPRPseudonymizeProcess.ObfuscateType current = ((PXSelectBase<GDPRPseudonymizeProcess.ObfuscateType>) this.Filter).Current;
    if (current.MasterEntity == "OP")
      return (IEnumerable) new List<PXResult<Contact, BAccount>>();
    PXView pxView = new PXView((PXGraph) this, true, BqlCommand.CreateInstance(new System.Type[10]
    {
      typeof (Select2<,,>),
      typeof (Contact),
      typeof (LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>),
      typeof (Where<,,>),
      typeof (ContactExt.pseudonymizationStatus),
      typeof (Equal<>),
      this.GetPseudonymizationStatus,
      typeof (Or<,>),
      typeof (ContactExt.pseudonymizationStatus),
      typeof (IsNull)
    }));
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    foreach (PXFilterRow filter in PXView.Filters)
      pxFilterRowList.Add(filter);
    if (!string.IsNullOrWhiteSpace(current.Search))
    {
      for (int index = 0; index < fields.Count; ++index)
        pxFilterRowList.Add(new PXFilterRow()
        {
          OrOperator = index != fields.Count - 1,
          OpenBrackets = index == 0 ? 1 : 0,
          DataField = fields[index],
          Condition = (PXCondition) 6,
          Value = (object) current.Search,
          CloseBrackets = index == fields.Count - 1 ? 1 : 0
        });
    }
    bool? nullable = current.ConsentExpired;
    if (nullable.GetValueOrDefault())
      pxView.WhereAnd<Where<Contact.consentExpirationDate, LessEqual<Now>>>();
    nullable = current.NoConsent;
    if (nullable.GetValueOrDefault())
      pxView.WhereAnd<Where<Contact.consentDate, IsNull>>();
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> objectList = pxView.Select(PXView.Currents, PXView.Parameters, PXView.Searches, (string[]) null, (bool[]) null, pxFilterRowList.ToArray(), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  protected virtual IEnumerable SelectOpportunities(List<string> fields)
  {
    GDPRPseudonymizeProcess.ObfuscateType current = ((PXSelectBase<GDPRPseudonymizeProcess.ObfuscateType>) this.Filter).Current;
    if (current.MasterEntity != "OP")
      return (IEnumerable) new List<PXResult<CRContact, BAccount>>();
    PXView pxView = new PXView((PXGraph) this, true, BqlCommand.CreateInstance(new System.Type[10]
    {
      typeof (Select2<,,>),
      typeof (CRContact),
      typeof (LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRContact.bAccountID>>>),
      typeof (Where<,,>),
      typeof (CRContactExt.pseudonymizationStatus),
      typeof (Equal<>),
      this.GetPseudonymizationStatus,
      typeof (Or<,>),
      typeof (CRContactExt.pseudonymizationStatus),
      typeof (IsNull)
    }));
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    foreach (PXFilterRow filter in PXView.Filters)
      pxFilterRowList.Add(filter);
    if (!string.IsNullOrWhiteSpace(current.Search))
    {
      for (int index = 0; index < fields.Count; ++index)
        pxFilterRowList.Add(new PXFilterRow()
        {
          OrOperator = true,
          OpenBrackets = index == 0 ? 1 : 0,
          DataField = fields[index],
          Condition = (PXCondition) 6,
          Value = (object) current.Search,
          CloseBrackets = index == fields.Count - 1 ? 1 : 0
        });
    }
    bool? nullable = current.ConsentExpired;
    if (nullable.GetValueOrDefault())
      pxView.WhereAnd<Where<CRContact.consentExpirationDate, LessEqual<Now>>>();
    nullable = current.NoConsent;
    if (nullable.GetValueOrDefault())
      pxView.WhereAnd<Where<CRContact.consentDate, IsNull>>();
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> objectList = pxView.Select(PXView.Currents, PXView.Parameters, PXView.Searches, (string[]) null, (bool[]) null, pxFilterRowList.ToArray(), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  public static void Process(
    IEnumerable<GDPRPseudonymizeProcess.ObfuscateEntity> entities,
    GDPRPersonalDataProcessBase graph)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      using (new PXReadDeletedScope(false))
      {
        graph.ProcessImpl(GDPRPseudonymizeProcess.RemapToPrimary(entities), true, new Guid?());
        transactionScope.Complete();
      }
    }
  }

  protected static IEnumerable<IBqlTable> RemapToPrimary(
    IEnumerable<GDPRPseudonymizeProcess.ObfuscateEntity> entities)
  {
    foreach (GDPRPseudonymizeProcess.ObfuscateEntity entity in entities)
    {
      switch (entity.ContactType)
      {
        case "PN":
        case "SP":
        case "AP":
        case "EP":
        case "LD":
          yield return (IBqlTable) ((PXSelectBase<Contact>) new PXSelect<Contact, Where<Contact.contactID, Equal<Required<Contact.contactID>>>>(new PXGraph())).SelectSingle(new object[1]
          {
            (object) entity.ContactID
          });
          continue;
        case "OP":
          yield return (IBqlTable) ((PXSelectBase<CRContact>) new PXSelect<CRContact, Where<CRContact.contactID, Equal<Required<CRContact.contactID>>>>(new PXGraph())).SelectSingle(new object[1]
          {
            (object) entity.ContactID
          });
          continue;
        default:
          continue;
      }
    }
  }

  protected override void TopLevelProcessor(string combinedKey, Guid? topParentNoteID, string info)
  {
    this.StoreRestorableSearchIndex(combinedKey, info);
    this.DeleteSearchIndex(topParentNoteID);
  }

  protected override void ChildLevelProcessor(
    PXGraph processingGraph,
    System.Type childTable,
    IEnumerable<PXPersonalDataFieldAttribute> fields,
    IEnumerable<object> childs,
    Guid? topParentNoteID)
  {
    this.StoreChildsValues(processingGraph, childTable, fields, childs, topParentNoteID);
    this.PseudonymizeChilds(processingGraph, childTable, fields, childs);
    this.WipeAudit(processingGraph, childTable, fields, childs);
  }

  protected bool StoreRestorableSearchIndex(string combinedKey, string info)
  {
    return PXDatabase.Insert<SMPersonalDataIndex>(new PXDataFieldAssign[4]
    {
      (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalDataIndex.combinedKey>((object) combinedKey),
      (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalDataIndex.indexID>((object) Guid.NewGuid()),
      (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalDataIndex.content>((object) info.ToString()),
      (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalDataIndex.createdDateTime>((object) PXTimeZoneInfo.UtcNow)
    });
  }

  private void StoreChildsValues(
    PXGraph processingGraph,
    System.Type childTable,
    IEnumerable<PXPersonalDataFieldAttribute> fields,
    IEnumerable<object> childs,
    Guid? topParentNoteID)
  {
    foreach (object child in childs)
    {
      Guid? topParentNoteID1 = processingGraph.Caches[childTable].GetValue(child, "NoteID") as Guid?;
      foreach (PXPersonalDataFieldAttribute field in fields)
      {
        object obj = processingGraph.Caches[childTable].GetValue(child, ((PXEventSubscriberAttribute) field).FieldName);
        if (obj != null && (field.DefaultValue == null || obj.GetType().IsAssignableFrom(field.DefaultValue.GetType()) && !obj.Equals(field.DefaultValue)))
          PXDatabase.Insert<SMPersonalData>(new List<PXDataFieldAssign>()
          {
            (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalData.table>((object) childTable.FullName),
            (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalData.field>((object) ((PXEventSubscriberAttribute) field).FieldName),
            (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalData.entityID>((object) topParentNoteID1),
            (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalData.topParentNoteID>((object) topParentNoteID),
            (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalData.value>(obj),
            (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalData.createdDateTime>((object) PXTimeZoneInfo.UtcNow)
          }.ToArray());
      }
      this.DeleteSearchIndex(topParentNoteID1);
    }
  }

  [PXHidden]
  [Serializable]
  public class ObfuscateEntity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    [PXDefault(false)]
    [PXUIField]
    public virtual bool? Selected { get; set; }

    [PXUIField(DisplayName = "Contact ID", Enabled = false)]
    [PXInt(IsKey = true)]
    public virtual int? ContactID { get; set; }

    [PXStringList(new string[] {"PN", "SP", "AP", "EP", "LD", "OP"}, new string[] {"Contact", "Sales Person", "Business Account", "Employee", "Lead", "Opportunity"})]
    [PXUIField(DisplayName = "Entity Type", Enabled = false)]
    [PXString(IsKey = true)]
    public virtual string ContactType { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Business Account")]
    public virtual string AcctCD { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Display Name", Enabled = false)]
    public virtual string DisplayName { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Middle Name", Enabled = false)]
    public virtual string MidName { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Last Name", Enabled = false)]
    public virtual string LastName { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Job Title", Enabled = false)]
    public virtual string Salutation { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Full Name", Enabled = false)]
    public virtual string FullName { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Email", Enabled = false)]
    public virtual string Email { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Web Site", Enabled = false)]
    public virtual string WebSite { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Fax", Enabled = false)]
    public virtual string Fax { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Phone 1", Enabled = false)]
    public virtual string Phone1 { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Phone 2", Enabled = false)]
    public virtual string Phone2 { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Phone 3", Enabled = false)]
    public virtual string Phone3 { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Deleted")]
    public virtual bool? Deleted { get; set; }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GDPRPseudonymizeProcess.ObfuscateEntity.selected>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class ObfuscateType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    [PXUIField(DisplayName = "Search")]
    public virtual string Search { get; set; }

    [PXStringList(new string[] {"CT", "OP"}, new string[] {"Contact", "Opportunity"})]
    [PXUIField(DisplayName = "Entity")]
    [PXDefault("CT")]
    [PXString]
    public virtual string MasterEntity { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "No Consent")]
    [PXDefault(false)]
    public virtual bool? NoConsent { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Consent Expired")]
    [PXDefault(true)]
    public virtual bool? ConsentExpired { get; set; }

    public abstract class search : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GDPRPseudonymizeProcess.ObfuscateType.search>
    {
    }

    public abstract class masterEntity : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GDPRPseudonymizeProcess.ObfuscateType.masterEntity>
    {
    }

    public abstract class noConsent : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GDPRPseudonymizeProcess.ObfuscateType.noConsent>
    {
    }

    public abstract class consentExpired : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GDPRPseudonymizeProcess.ObfuscateType.consentExpired>
    {
    }
  }
}
