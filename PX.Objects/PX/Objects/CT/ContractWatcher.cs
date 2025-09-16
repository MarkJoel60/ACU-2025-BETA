// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractWatcher
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXPrimaryGraph(typeof (ContractMaint))]
[PXHidden]
[Serializable]
public class ContractWatcher : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ContractID;
  protected int? _ContactID;
  protected 
  #nullable disable
  string _WatchTypeID;
  protected string _EMail;

  [PXDBDefault(typeof (Contract.contractID))]
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Contract ID")]
  [PXParent(typeof (Select<Contract, Where<Contract.contractID, Equal<Current<ContractWatcher.contractID>>>>))]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact")]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>>>), Filterable = true)]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Watch Type")]
  [WatchType.List]
  public virtual string WatchTypeID
  {
    get => this._WatchTypeID;
    set => this._WatchTypeID = value;
  }

  [PXDBEmail(IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDefault]
  public virtual string EMail
  {
    get => this._EMail;
    set => this._EMail = value;
  }

  public class PK : 
    PrimaryKeyOf<ContractWatcher>.By<ContractWatcher.contractID, ContractWatcher.eMail>
  {
    public static ContractWatcher Find(
      PXGraph graph,
      int? contractSLAMappingID,
      string eMail,
      PKFindOptions options = 0)
    {
      return (ContractWatcher) PrimaryKeyOf<ContractWatcher>.By<ContractWatcher.contractID, ContractWatcher.eMail>.FindBy(graph, (object) contractSLAMappingID, (object) eMail, options);
    }
  }

  public static class FK
  {
    public class Contact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<ContractWatcher>.By<ContractWatcher.contactID>
    {
    }
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractWatcher.contractID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractWatcher.contactID>
  {
  }

  public abstract class watchTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractWatcher.watchTypeID>
  {
  }

  public abstract class eMail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractWatcher.eMail>
  {
  }
}
