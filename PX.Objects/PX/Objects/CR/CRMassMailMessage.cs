// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMassMailMessage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Mass Mail Message")]
[Serializable]
public class CRMassMailMessage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _MassMailID;
  protected Guid? _MessageID;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (CRMassMail.massMailID))]
  [PXParent(typeof (Select<CRMassMail, Where<CRMassMail.massMailID, Equal<Current<CRMassMailMessage.massMailID>>>>))]
  public virtual int? MassMailID
  {
    get => this._MassMailID;
    set => this._MassMailID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? MessageID
  {
    get => this._MessageID;
    set => this._MessageID = value;
  }

  public abstract class massMailID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CRMassMailMessage.massMailID>
  {
  }

  public abstract class messageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRMassMailMessage.messageID>
  {
  }
}
