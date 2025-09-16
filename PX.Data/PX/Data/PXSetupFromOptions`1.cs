// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetupFromOptions`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Common;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>
/// A special kind of the <see cref="T:PX.Data.PXSetup`1" /> data view that retrieves its value not from the DB
/// but from the <see href="https://docs.microsoft.com/en-us/dotnet/core/extensions/options">Microsoft.Extensions.Options</see>
/// infrastructure (from <see cref="T:Microsoft.Extensions.Options.IOptionsSnapshot`1" />).
/// </summary>
/// <remarks>Since it is a helper view that allows using values from the Options infrastructure in BQL,
/// access rights for <typeparamref name="Table" /> are not verified.</remarks>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <exception cref="T:PX.Data.PXException"><typeparamref name="Table" /> is a DB-bound DAC.</exception>
/// <seealso href="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options" />
[PXInternalUseOnly]
public sealed class PXSetupFromOptions<Table> : PXSetup<Table> where Table : class, IBqlTable, new()
{
  [InjectDependency]
  private IOptionsSnapshot<Table> Options { get; set; }

  public PXSetupFromOptions(PXGraph graph)
    : base(graph)
  {
    if (this.IsDbTable())
      throw new PXException("The PXSetupFromOptions type accepts only database unbound DACs.");
  }

  private protected override object getRecord()
  {
    if ((object) this._Record == null)
      this._Record = ((IOptions<Table>) this.Options).Value;
    return (object) this._Record;
  }

  private bool IsDbTable()
  {
    BqlCommand bqlSelect = this.Cache.BqlSelect;
    return PXDatabase.Tables.Contains<string>((bqlSelect != null ? ((IEnumerable<System.Type>) bqlSelect.GetTables()).FirstOrDefault<System.Type>()?.Name : (string) null) ?? nameof (Table));
  }
}
