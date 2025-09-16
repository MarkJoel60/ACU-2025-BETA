// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OUActivity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXHidden]
[Serializable]
public class OUActivity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int OpportunityIDLength = 15;

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault(typeof (OUMessage.subject))]
  [PXUIField]
  public virtual 
  #nullable disable
  string Subject { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Contact")]
  public virtual bool? IsLinkContact
  {
    get => new bool?(this.Type == typeof (Contact).FullName);
    set
    {
      if (!value.GetValueOrDefault())
        return;
      this.Type = typeof (Contact).FullName;
    }
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Case")]
  public virtual bool? IsLinkCase
  {
    get => new bool?(this.Type == typeof (CRCase).FullName);
    set
    {
      if (value.GetValueOrDefault())
      {
        this.Type = typeof (CRCase).FullName;
      }
      else
      {
        if (!this.IsLinkCase.GetValueOrDefault())
          return;
        this.IsLinkContact = new bool?(true);
      }
    }
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Opportunity")]
  public virtual bool? IsLinkOpportunity
  {
    get => new bool?(this.Type == typeof (CROpportunity).FullName);
    set
    {
      if (value.GetValueOrDefault())
      {
        this.Type = typeof (CROpportunity).FullName;
      }
      else
      {
        if (!this.IsLinkOpportunity.GetValueOrDefault())
          return;
        this.IsLinkContact = new bool?(true);
      }
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Type", Required = true)]
  [PXEntityTypeList]
  [PXDefault]
  public virtual string Type { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDefault]
  [PXSelector(typeof (Search<CRCase.caseCD, Where<CRCase.contactID, Equal<Current<OUSearchEntity.contactID>>, Or<CRCase.customerID, Equal<Current<OUSearchEntity.contactBaccountID>>>>, OrderBy<Desc<CRCase.caseCD>>>), new System.Type[] {typeof (CRCase.caseCD), typeof (CRCase.subject), typeof (CRCase.status), typeof (CRCase.priority), typeof (CRCase.severity), typeof (CRCase.caseClassID), typeof (BAccount.acctName)}, Filterable = true, DescriptionField = typeof (CRCase.subject))]
  public virtual string CaseCD { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDefault]
  [PXSelector(typeof (Search<CROpportunity.opportunityID, Where<CROpportunity.contactID, Equal<Current<OUSearchEntity.contactID>>, Or<CROpportunity.bAccountID, Equal<Current<OUSearchEntity.contactBaccountID>>>>, OrderBy<Desc<CROpportunity.opportunityID>>>), new System.Type[] {typeof (CROpportunity.opportunityID), typeof (CROpportunity.subject), typeof (CROpportunity.status), typeof (CROpportunity.curyAmount), typeof (CROpportunity.curyID), typeof (CROpportunity.closeDate), typeof (CROpportunity.stageID), typeof (CROpportunity.classID)}, Filterable = true, DescriptionField = typeof (CROpportunity.subject))]
  [PXFieldDescription]
  public virtual string OpportunityID { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUActivity.subject>
  {
  }

  public abstract class isLinkContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OUActivity.isLinkContact>
  {
  }

  public abstract class isLinkCase : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OUActivity.isLinkCase>
  {
  }

  public abstract class isLinkOpportunity : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OUActivity.isLinkOpportunity>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUActivity.type>
  {
  }

  public abstract class caseCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUActivity.caseCD>
  {
  }

  public abstract class opportunityID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUActivity.opportunityID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  OUActivity.noteID>
  {
  }
}
