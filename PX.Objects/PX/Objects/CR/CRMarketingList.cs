// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.Maintenance.GI;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CR;

[CRCacheIndependentPrimaryGraph(typeof (CRMarketingListMaint), typeof (Select<CRMarketingList, Where<CRMarketingList.marketingListID, Equal<Current<CRMarketingList.marketingListID>>>>))]
[PXCacheName("Marketing List")]
[DebuggerDisplay("{GetType().Name,nq}: MarketingListID = {MarketingListID,nq}, MailListCode = {MailListCode}, Name = {Name}")]
[Serializable]
public class CRMarketingList : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, INotable
{
  private 
  #nullable disable
  string _method;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBIdentity]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual int? MarketingListID { get; set; }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXDimensionSelector("MLISTCD", typeof (Search<CRMarketingList.marketingListID>), typeof (CRMarketingList.mailListCode))]
  [PXFieldDescription]
  public virtual string MailListCode { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Name { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBString]
  [PXDefault("I")]
  [CRMarketingList.status.List]
  [PXUIField]
  public virtual string Status { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXCompanyTreeSelector]
  public virtual int? WorkgroupID { get; set; }

  [Owner(typeof (CRMarketingList.workgroupID))]
  public virtual int? OwnerID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [CRContactMethods]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Contact Method")]
  public virtual string Method
  {
    get => this._method ?? "A";
    set => this._method = value;
  }

  [PXDBString]
  [PXDefault("S")]
  [CRMarketingList.type.List]
  [PXUIField]
  public virtual string Type { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Generic Inquiry")]
  [ContactGISelector]
  [PXForeignReference(typeof (Field<CRMarketingList.gIDesignID>.IsRelatedTo<GIDesign.designID>))]
  public Guid? GIDesignID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Shared Filter")]
  [FilterList(typeof (CRMarketingList.gIDesignID), IsSiteMapIdentityScreenID = false, IsSiteMapIdentityGIDesignID = true)]
  [PXFormula(typeof (Default<CRMarketingList.gIDesignID>))]
  public virtual Guid? SharedGIFilter { get; set; }

  [PXNote(DescriptionField = typeof (CRMarketingList.mailListCode), Selector = typeof (CRMarketingList.marketingListID), ShowInReferenceSelector = true)]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Modified Date")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<CRMarketingList>.By<CRMarketingList.marketingListID>
  {
    public static CRMarketingList Find(PXGraph graph, int? marketingListID, PKFindOptions options = 0)
    {
      return (CRMarketingList) PrimaryKeyOf<CRMarketingList>.By<CRMarketingList.marketingListID>.FindBy(graph, (object) marketingListID, options);
    }
  }

  public class UK : PrimaryKeyOf<CRMarketingList>.By<CRMarketingList.mailListCode>
  {
    public static CRMarketingList Find(PXGraph graph, string mailListCode, PKFindOptions options = 0)
    {
      return (CRMarketingList) PrimaryKeyOf<CRMarketingList>.By<CRMarketingList.mailListCode>.FindBy(graph, (object) mailListCode, options);
    }
  }

  public static class FK
  {
    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRMarketingList>.By<CRMarketingList.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<CRMarketingList>.By<CRMarketingList.workgroupID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRMarketingList.selected>
  {
  }

  public abstract class marketingListID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRMarketingList.marketingListID>
  {
  }

  public abstract class mailListCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingList.mailListCode>
  {
    public const string DimensionName = "MLISTCD";
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingList.name>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingList.description>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingList.status>
  {
    public const string Active = "A";
    public const string Inactive = "I";

    public class List : PXStringListAttribute
    {
      public List()
        : base(new (string, string)[2]
        {
          ("A", "Active"),
          ("I", "Inactive")
        })
      {
      }
    }

    public class active : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRMarketingList.status.active>
    {
      public active()
        : base("A")
      {
      }
    }

    public class inactive : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRMarketingList.status.inactive>
    {
      public inactive()
        : base("I")
      {
      }
    }
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRMarketingList.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRMarketingList.ownerID>
  {
  }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingList.method>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingList.type>
  {
    public const string Static = "S";
    public const string Dynamic = "D";

    public class List : PXStringListAttribute
    {
      public List()
        : base(new (string, string)[2]
        {
          ("S", "Static"),
          ("D", "Dynamic")
        })
      {
      }
    }

    public class @static : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRMarketingList.type.@static>
    {
      public @static()
        : base("S")
      {
      }
    }

    public class dynamic : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRMarketingList.type.dynamic>
    {
      public dynamic()
        : base("D")
      {
      }
    }
  }

  public abstract class gIDesignID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRMarketingList.gIDesignID>
  {
  }

  public abstract class sharedGIFilter : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRMarketingList.sharedGIFilter>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRMarketingList.noteID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingList.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRMarketingList.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRMarketingList.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRMarketingList.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingList.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRMarketingList.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRMarketingList.Tstamp>
  {
  }
}
