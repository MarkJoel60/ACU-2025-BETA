// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PMInstance
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXHidden]
[PXCacheName("Payment Method Instance")]
[Serializable]
public class PMInstance : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? PMInstanceID { get; set; }

  [PXDBTimestamp]
  public virtual 
  #nullable disable
  byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<PMInstance>.By<PMInstance.pMInstanceID>
  {
    public static PMInstance Find(PXGraph graph, int? pMInstanceID, PKFindOptions options = 0)
    {
      return (PMInstance) PrimaryKeyOf<PMInstance>.By<PMInstance.pMInstanceID>.FindBy(graph, (object) pMInstanceID, options);
    }
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMInstance.pMInstanceID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMInstance.Tstamp>
  {
  }
}
