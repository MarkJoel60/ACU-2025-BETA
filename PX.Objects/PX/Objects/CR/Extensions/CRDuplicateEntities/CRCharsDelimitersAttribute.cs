// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRCharsDelimitersAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

/// <summary>
/// Attribute used to set <see cref="T:System.Char" /> array as field value as duplicate validation delimiters.
/// The default value is <see cref="F:PX.Objects.CR.Extensions.CRDuplicateEntities.CRCharsDelimitersAttribute.DefaultDelimiters" />.
/// For customize list of available values attach new version of attribute with not empty construction with specified delimiters string.
/// </summary>
/// <example>
/// This sample shows how to replace <see cref="F:PX.Objects.CR.Extensions.CRDuplicateEntities.CRCharsDelimitersAttribute.DefaultDelimiters" /> with following chars: ' ', '.', ',', '^'.
/// <code>
/// public sealed class CRSetupDelimitersExt : PXCacheExtension&lt;CRSetup&gt;
/// {
///     [PXString]
///     [PX.Objects.CR.Extensions.CRDuplicateEntities.CRCharsDelimiters(" .,^")]
///     public string DuplicateCharsDelimiters { get; set; }
/// }
/// </code>
/// </example>
public class CRCharsDelimitersAttribute : PXEventSubscriberAttribute, IPXRowSelectingSubscriber
{
  public const string DefaultDelimiters = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ ";

  protected string Delimiters { get; set; }

  public CRCharsDelimitersAttribute()
    : this("!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ ")
  {
  }

  public CRCharsDelimitersAttribute(string delimiters)
  {
    this.Delimiters = delimiters ?? throw new ArgumentNullException(nameof (delimiters));
  }

  public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    sender.SetValue(e.Row, this.FieldOrdinal, (object) this.Delimiters);
  }
}
