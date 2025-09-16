// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRLeadBackwardCompatibility
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.Attributes;
using System;

#nullable enable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[PXHidden]
[Serializable]
public sealed class CRLeadBackwardCompatibility : PXCacheExtension<
#nullable disable
CRLead>
{
  [PXDBInt(BqlField = typeof (Contact.contactID))]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.contactID)})]
  [PXUIField(Visible = false)]
  [NoUpdateDBField(NoInsert = true)]
  public int? LeadContactID => this.Base.ContactID;

  [PXDBGuid(false, BqlField = typeof (Contact.noteID))]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.noteID)})]
  [PXUIField(Visible = false)]
  [NoUpdateDBField(NoInsert = true)]
  public Guid? LeadNoteID => this.Base.NoteID;

  [PXDBCreatedByID(BqlField = typeof (Contact.createdByID))]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.createdByID)})]
  [NoUpdateDBField(NoInsert = true)]
  public Guid? LeadCreatedByID => this.Base.CreatedByID;

  [PXDBCreatedByScreenID(BqlField = typeof (Contact.createdByScreenID))]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.createdByScreenID)})]
  [NoUpdateDBField(NoInsert = true)]
  public string LeadCreatedByScreenID => this.Base.CreatedByScreenID;

  [PXDBCreatedDateTime(BqlField = typeof (Contact.createdDateTime))]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.createdDateTime)})]
  [NoUpdateDBField(NoInsert = true)]
  public DateTime? LeadCreatedDateTime => this.Base.CreatedDateTime;

  [PXDBLastModifiedByID(BqlField = typeof (Contact.lastModifiedByID))]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.lastModifiedByID)})]
  [NoUpdateDBField(NoInsert = true)]
  public Guid? LeadLastModifiedByID => this.Base.LastModifiedByID;

  [PXDBLastModifiedByScreenID(BqlField = typeof (Contact.lastModifiedByScreenID))]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.lastModifiedByScreenID)})]
  [NoUpdateDBField(NoInsert = true)]
  public string LeadLastModifiedByScreenID => this.Base.LastModifiedByScreenID;

  [PXDBLastModifiedDateTime(BqlField = typeof (Contact.lastModifiedDateTime))]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.lastModifiedDateTime)})]
  [NoUpdateDBField(NoInsert = true)]
  public DateTime? LeadLastModifiedDateTime => this.Base.LastModifiedDateTime;

  public abstract class leadContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRLeadBackwardCompatibility.leadContactID>
  {
  }

  public abstract class leadNoteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRLeadBackwardCompatibility.leadNoteID>
  {
  }

  public abstract class leadCreatedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRLeadBackwardCompatibility.leadCreatedByID>
  {
  }

  public abstract class leadCreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRLeadBackwardCompatibility.leadCreatedByScreenID>
  {
  }

  public abstract class leadCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRLeadBackwardCompatibility.leadCreatedDateTime>
  {
  }

  public abstract class leadLastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRLeadBackwardCompatibility.leadLastModifiedByID>
  {
  }

  public abstract class leadLastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRLeadBackwardCompatibility.leadLastModifiedByScreenID>
  {
  }

  public abstract class leadLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRLeadBackwardCompatibility.leadLastModifiedDateTime>
  {
  }
}
