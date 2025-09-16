// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCostSite
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
public class INCostSite : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? CostSiteID { get; set; }

  [PXDBForeignIdentityType]
  public virtual 
  #nullable disable
  string CostSiteType { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<INCostSite>.By<INCostSite.costSiteID>
  {
    public static INCostSite Find(PXGraph graph, int? costSiteID, PKFindOptions options = 0)
    {
      return (INCostSite) PrimaryKeyOf<INCostSite>.By<INCostSite.costSiteID>.FindBy(graph, (object) costSiteID, options);
    }
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostSite.costSiteID>
  {
  }

  public abstract class costSiteType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCostSite.costSiteType>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INCostSite.Tstamp>
  {
  }
}
